using System.Text.Json;
using GPBackend.DTOs.InterviewFeedback;
using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.Services.Interfaces;

namespace GPBackend.Services.Implements
{
    public class InterviewFeedbackService : IInterviewFeedbackService
    {
        private readonly IInterviewFeedbackClient _client;
        private readonly IInterviewRepository _interviewRepository;
        private readonly IInterviewQuestionRepository _interviewQuestionRepository;
        private readonly IInterviewQuestionFeedbackRepository _questionFeedbackRepository;
        private readonly IInterviewVideoFeedbackRepository _videoFeedbackRepository;
        private readonly ILogger<InterviewFeedbackService> _logger;

        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        public InterviewFeedbackService(
            IInterviewFeedbackClient client,
            IInterviewRepository interviewRepository,
            IInterviewQuestionRepository interviewQuestionRepository,
            IInterviewQuestionFeedbackRepository questionFeedbackRepository,
            IInterviewVideoFeedbackRepository videoFeedbackRepository,
            ILogger<InterviewFeedbackService> logger)
        {
            _client = client;
            _interviewRepository = interviewRepository;
            _interviewQuestionRepository = interviewQuestionRepository;
            _questionFeedbackRepository = questionFeedbackRepository;
            _videoFeedbackRepository = videoFeedbackRepository;
            _logger = logger;
        }

        public async Task<InterviewQuestionFeedbackResponseDto> GradeInterviewQuestionAsync(
            int userId,
            int interviewId,
            int interviewQuestionId,
            string? context,
            CancellationToken cancellationToken = default)
        {
            await EnsureInterviewOwnedAsync(userId, interviewId, cancellationToken);

            var interviewQuestions = await _interviewQuestionRepository.GetByInterviewIdAsync(userId, interviewId);
            var question = interviewQuestions
                .Where(q => q != null && !q.IsDeleted)
                .FirstOrDefault(q => q!.QuestionId == interviewQuestionId);

            if (question == null)
            {
                throw new KeyNotFoundException("Interview question not found.");
            }

            if (string.IsNullOrWhiteSpace(question.Answer))
            {
                throw new ArgumentException("Interview question has no answer to grade.");
            }

            var fastApiResponse = await _client.GradeAnswerAsync(new GradeAnswerRequestDto
            {
                Question = question.Question,
                Answer = question.Answer,
                Context = context
            }, cancellationToken);

            var strengthsJson = JsonSerializer.Serialize(fastApiResponse.Strengths ?? new List<string>(), JsonOptions);
            var improvementsJson = JsonSerializer.Serialize(fastApiResponse.Improvements ?? new List<string>(), JsonOptions);
            var rawResponseJson = JsonSerializer.Serialize(fastApiResponse, JsonOptions);

            var now = DateTime.UtcNow;
            var entity = await _questionFeedbackRepository.GetByInterviewQuestionIdAsync(interviewQuestionId);

            if (entity == null)
            {
                entity = new InterviewQuestionFeedback
                {
                    InterviewQuestionId = interviewQuestionId,
                    CreatedAt = now
                };
                entity.Score = fastApiResponse.Score;
                entity.Feedback = fastApiResponse.Feedback ?? string.Empty;
                entity.StrengthsJson = strengthsJson;
                entity.ImprovementsJson = improvementsJson;
                entity.Context = context;
                entity.RawResponseJson = rawResponseJson;
                entity.UpdatedAt = now;
                entity.IsDeleted = false;

                await _questionFeedbackRepository.CreateAsync(entity);
            }
            else
            {
                entity.Score = fastApiResponse.Score;
                entity.Feedback = fastApiResponse.Feedback ?? string.Empty;
                entity.StrengthsJson = strengthsJson;
                entity.ImprovementsJson = improvementsJson;
                entity.Context = context;
                entity.RawResponseJson = rawResponseJson;
                entity.UpdatedAt = now;
                entity.IsDeleted = false;

                await _questionFeedbackRepository.UpdateAsync(entity);
            }

            return new InterviewQuestionFeedbackResponseDto
            {
                InterviewId = interviewId,
                InterviewQuestionId = question.QuestionId,
                Question = question.Question,
                Answer = question.Answer,
                Score = entity.Score,
                Feedback = entity.Feedback,
                Strengths = DeserializeStringList(entity.StrengthsJson),
                Improvements = DeserializeStringList(entity.ImprovementsJson),
                Context = entity.Context,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public async Task<List<InterviewQuestionFeedbackResponseDto>> GradeInterviewQuestionsBatchAsync(
            int userId,
            int interviewId,
            List<int> interviewQuestionIds,
            string? context,
            CancellationToken cancellationToken = default)
        {
            await EnsureInterviewOwnedAsync(userId, interviewId, cancellationToken);

            if (interviewQuestionIds == null || interviewQuestionIds.Count == 0)
            {
                throw new ArgumentException("InterviewQuestionIds list is required.");
            }

            var distinctIds = interviewQuestionIds.Distinct().ToList();

            var interviewQuestions = await _interviewQuestionRepository.GetByInterviewIdAsync(userId, interviewId);
            var questions = interviewQuestions
                .Where(q => q != null && !q.IsDeleted && distinctIds.Contains(q!.QuestionId))
                .Select(q => q!)
                .ToList();

            if (questions.Count != distinctIds.Count)
            {
                throw new KeyNotFoundException("One or more interview questions were not found for this interview.");
            }

            // Preserve caller ordering for deterministic mapping to FastAPI results.
            var orderedQuestions = distinctIds
                .Select(id => questions.First(q => q.QuestionId == id))
                .ToList();

            if (orderedQuestions.Any(q => string.IsNullOrWhiteSpace(q.Answer)))
            {
                throw new ArgumentException("All interview questions must have a non-empty answer to grade.");
            }

            var request = new GradeAnswersBatchRequestDto
            {
                Context = context,
                Items = orderedQuestions.Select(q => new GradeAnswersBatchItemDto
                {
                    Question = q.Question,
                    Answer = q.Answer!
                }).ToList()
            };

            var fastApiResponse = await _client.GradeAnswersBatchAsync(request, cancellationToken);

            if (fastApiResponse.Results == null || fastApiResponse.Results.Count != orderedQuestions.Count)
            {
                _logger.LogWarning(
                    "FastAPI grade-answers-batch results count mismatch. Expected={Expected} Actual={Actual}",
                    orderedQuestions.Count,
                    fastApiResponse.Results?.Count ?? 0);
                throw new InvalidOperationException("Interview Feedback service returned an unexpected results count.");
            }

            var now = DateTime.UtcNow;
            var responseDtos = new List<InterviewQuestionFeedbackResponseDto>(orderedQuestions.Count);

            for (int i = 0; i < orderedQuestions.Count; i++)
            {
                var q = orderedQuestions[i];
                var r = fastApiResponse.Results[i];

                // We persist per-question feedback as latest-only (upsert).
                var entity = await _questionFeedbackRepository.GetByInterviewQuestionIdAsync(q.QuestionId);

                if (entity == null)
                {
                    entity = new InterviewQuestionFeedback
                    {
                        InterviewQuestionId = q.QuestionId,
                        CreatedAt = now
                    };
                    entity.Score = r.Score;
                    entity.Feedback = r.Feedback ?? string.Empty;
                    entity.StrengthsJson = null; // batch result has no strengths/improvements in current FastAPI contract
                    entity.ImprovementsJson = null;
                    entity.Context = context;
                    entity.RawResponseJson = JsonSerializer.Serialize(r, JsonOptions);
                    entity.UpdatedAt = now;
                    entity.IsDeleted = false;

                    await _questionFeedbackRepository.CreateAsync(entity);
                }
                else
                {
                    entity.Score = r.Score;
                    entity.Feedback = r.Feedback ?? string.Empty;
                    entity.StrengthsJson = null; // batch result has no strengths/improvements in current FastAPI contract
                    entity.ImprovementsJson = null;
                    entity.Context = context;
                    entity.RawResponseJson = JsonSerializer.Serialize(r, JsonOptions);
                    entity.UpdatedAt = now;
                    entity.IsDeleted = false;

                    await _questionFeedbackRepository.UpdateAsync(entity);
                }

                responseDtos.Add(new InterviewQuestionFeedbackResponseDto
                {
                    InterviewId = interviewId,
                    InterviewQuestionId = q.QuestionId,
                    Question = q.Question,
                    Answer = q.Answer!,
                    Score = entity.Score,
                    Feedback = entity.Feedback,
                    Strengths = new List<string>(),
                    Improvements = new List<string>(),
                    Context = entity.Context,
                    UpdatedAt = entity.UpdatedAt
                });
            }

            return responseDtos;
        }

        public async Task<InterviewVideoFeedbackResponseDto> AnalyzeInterviewVideoAsync(
            int userId,
            int interviewId,
            Stream videoStream,
            string fileName,
            string? contentType,
            CancellationToken cancellationToken = default)
        {
            await EnsureInterviewOwnedAsync(userId, interviewId, cancellationToken);

            var fastApiResponse = await _client.AnalyzeVideoAsync(videoStream, fileName, contentType, cancellationToken);

            var now = DateTime.UtcNow;
            var entity = await _videoFeedbackRepository.GetByInterviewIdAsync(interviewId);

            if (entity == null)
            {
                entity = new InterviewVideoFeedback
                {
                    InterviewId = interviewId,
                    CreatedAt = now
                };
            }

            entity.Confidence = fastApiResponse.Metrics?.Confidence ?? 0;
            entity.Engagement = fastApiResponse.Metrics?.Engagement ?? 0;
            entity.Stress = fastApiResponse.Metrics?.Stress ?? 0;
            entity.Authenticity = fastApiResponse.Metrics?.Authenticity ?? 0;
            entity.Summary = fastApiResponse.Feedback?.Summary;
            entity.StrengthsJson = JsonSerializer.Serialize(fastApiResponse.Feedback?.Strengths ?? new List<VideoFeedbackItemDto>(), JsonOptions);
            entity.ImprovementsJson = JsonSerializer.Serialize(fastApiResponse.Feedback?.Improvements ?? new List<VideoFeedbackItemDto>(), JsonOptions);
            entity.RecommendationsJson = JsonSerializer.Serialize(fastApiResponse.Feedback?.Recommendations ?? new List<VideoRecommendationDto>(), JsonOptions);
            entity.ReportPath = fastApiResponse.ReportPath;
            entity.RawResponseJson = JsonSerializer.Serialize(fastApiResponse, JsonOptions);
            entity.UpdatedAt = now;
            entity.IsDeleted = false;

            if (entity.Id == 0)
            {
                await _videoFeedbackRepository.CreateAsync(entity);
            }
            else
            {
                await _videoFeedbackRepository.UpdateAsync(entity);
            }

            return ToVideoResponseDto(interviewId, entity, fastApiResponse);
        }

        public async Task<List<InterviewQuestionFeedbackResponseDto>> GetAnswersFeedbackAsync(
            int userId,
            int interviewId,
            CancellationToken cancellationToken = default)
        {
            await EnsureInterviewOwnedAsync(userId, interviewId, cancellationToken);

            var questions = (await _interviewQuestionRepository.GetByInterviewIdAsync(userId, interviewId))
                .Where(q => q != null && !q.IsDeleted)
                .Select(q => q!)
                .OrderBy(q => q.QuestionId)
                .ToList();

            var questionIds = questions.Select(q => q.QuestionId).ToList();

            var feedbacks = await _questionFeedbackRepository.GetByInterviewQuestionIdsAsync(questionIds);

            var feedbackByQuestionId = feedbacks.ToDictionary(f => f.InterviewQuestionId, f => f);

            var result = new List<InterviewQuestionFeedbackResponseDto>(questions.Count);
            foreach (var q in questions)
            {
                feedbackByQuestionId.TryGetValue(q.QuestionId, out var f);

                result.Add(new InterviewQuestionFeedbackResponseDto
                {
                    InterviewId = interviewId,
                    InterviewQuestionId = q.QuestionId,
                    Question = q.Question,
                    Answer = q.Answer ?? string.Empty,
                    Score = f?.Score ?? 0,
                    Feedback = f?.Feedback ?? string.Empty,
                    Strengths = DeserializeStringList(f?.StrengthsJson),
                    Improvements = DeserializeStringList(f?.ImprovementsJson),
                    Context = f?.Context,
                    UpdatedAt = f?.UpdatedAt ?? DateTime.MinValue
                });
            }

            return result;
        }

        public async Task<InterviewVideoFeedbackResponseDto?> GetVideoFeedbackAsync(
            int userId,
            int interviewId,
            CancellationToken cancellationToken = default)
        {
            await EnsureInterviewOwnedAsync(userId, interviewId, cancellationToken);

            var entity = await _videoFeedbackRepository.GetByInterviewIdAsync(interviewId);

            if (entity == null)
            {
                return null;
            }

            AnalyzeVideoResponseDto? parsed = null;
            if (!string.IsNullOrWhiteSpace(entity.RawResponseJson))
            {
                try
                {
                    parsed = JsonSerializer.Deserialize<AnalyzeVideoResponseDto>(entity.RawResponseJson, JsonOptions);
                }
                catch (JsonException)
                {
                    // ignore; we'll fall back to columns
                }
            }

            return ToVideoResponseDto(interviewId, entity, parsed);
        }

        private async Task EnsureInterviewOwnedAsync(int userId, int interviewId, CancellationToken cancellationToken)
        {
            var interview = await _interviewRepository.GetInterviewByIdAsync(userId, interviewId);
            if (interview == null)
            {
                throw new KeyNotFoundException("Interview not found.");
            }
        }

        private static List<string> DeserializeStringList(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<string>();
            }

            try
            {
                return JsonSerializer.Deserialize<List<string>>(json, JsonOptions) ?? new List<string>();
            }
            catch (JsonException)
            {
                return new List<string>();
            }
        }

        private static InterviewVideoFeedbackResponseDto ToVideoResponseDto(
            int interviewId,
            InterviewVideoFeedback entity,
            AnalyzeVideoResponseDto? parsed)
        {
            if (parsed != null)
            {
                return new InterviewVideoFeedbackResponseDto
                {
                    InterviewId = interviewId,
                    Status = parsed.Status,
                    VideoPath = parsed.VideoPath,
                    Metrics = parsed.Metrics ?? new VideoMetricsDto(),
                    Feedback = parsed.Feedback ?? new VideoFeedbackDto(),
                    ReportPath = parsed.ReportPath,
                    UpdatedAt = entity.UpdatedAt
                };
            }

            // Fallback from persisted columns
            return new InterviewVideoFeedbackResponseDto
            {
                InterviewId = interviewId,
                Status = string.Empty,
                VideoPath = string.Empty,
                Metrics = new VideoMetricsDto
                {
                    Confidence = entity.Confidence,
                    Engagement = entity.Engagement,
                    Stress = entity.Stress,
                    Authenticity = entity.Authenticity
                },
                Feedback = new VideoFeedbackDto
                {
                    Summary = entity.Summary ?? string.Empty,
                    Strengths = DeserializeVideoItems(entity.StrengthsJson),
                    Improvements = DeserializeVideoItems(entity.ImprovementsJson),
                    Recommendations = DeserializeRecommendations(entity.RecommendationsJson)
                },
                ReportPath = entity.ReportPath ?? string.Empty,
                UpdatedAt = entity.UpdatedAt
            };
        }

        private static List<VideoFeedbackItemDto> DeserializeVideoItems(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<VideoFeedbackItemDto>();
            }

            try
            {
                return JsonSerializer.Deserialize<List<VideoFeedbackItemDto>>(json, JsonOptions) ?? new List<VideoFeedbackItemDto>();
            }
            catch (JsonException)
            {
                return new List<VideoFeedbackItemDto>();
            }
        }

        private static List<VideoRecommendationDto> DeserializeRecommendations(string? json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<VideoRecommendationDto>();
            }

            try
            {
                return JsonSerializer.Deserialize<List<VideoRecommendationDto>>(json, JsonOptions) ?? new List<VideoRecommendationDto>();
            }
            catch (JsonException)
            {
                return new List<VideoRecommendationDto>();
            }
        }
    }
}


