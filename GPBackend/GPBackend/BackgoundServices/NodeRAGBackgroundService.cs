using System.Threading.Channels;
using GPBackend.Models;
using GPBackend.Services.Interfaces;
using GPBackend.DTOs.NodeRAG;

namespace GPBackend.BackgoundServices
{
    public class NodeRAGBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NodeRAGBackgroundService> _logger;
        private readonly IConfiguration _configuration;
        private readonly Channel<NodeRAGBackgroundJob> _jobQueue;
        
        public NodeRAGBackgroundService(
            IServiceProvider serviceProvider, 
            ILogger<NodeRAGBackgroundService> logger,
            IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _configuration = configuration;
            _jobQueue = Channel.CreateUnbounded<NodeRAGBackgroundJob>();
        }
        
        public async Task QueueJobAsync(NodeRAGBackgroundJob job)
        {
            await _jobQueue.Writer.WriteAsync(job);
            _logger.LogInformation("NodeRAG job queued: JobId={JobId}, Type={JobType}, UserId={UserId}", 
                job.JobId, job.JobType, job.UserId);
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("NodeRAG Background Service started");
            
            await foreach (var job in _jobQueue.Reader.ReadAllAsync(stoppingToken))
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var nodeRAGClient = scope.ServiceProvider.GetRequiredService<INodeRAGClient>();
                    
                    _logger.LogInformation("Processing NodeRAG job: JobId={JobId}, Type={JobType}", 
                        job.JobId, job.JobType);
                    
                    switch (job.JobType)
                    {
                        case NodeRAGJobType.BuildGraph:
                            await ProcessBuildJobAsync(nodeRAGClient, job, stoppingToken);
                            break;
                        case NodeRAGJobType.SyncQAPair:
                            await ProcessQASyncJobAsync(nodeRAGClient, job);
                            break;
                    }
                    
                    _logger.LogInformation("NodeRAG job completed: JobId={JobId}", job.JobId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing NodeRAG job: JobId={JobId}, Type={JobType}", 
                        job.JobId, job.JobType);
                    
                    // Retry logic
                    if (job.RetryCount < job.MaxRetries)
                    {
                        job.RetryCount++;
                        var delaySeconds = Math.Pow(2, job.RetryCount);
                        _logger.LogInformation("Retrying job {JobId} in {Delay} seconds (Attempt {Attempt}/{Max})", 
                            job.JobId, delaySeconds, job.RetryCount, job.MaxRetries);
                        
                        await Task.Delay(TimeSpan.FromSeconds(delaySeconds), stoppingToken);
                        await QueueJobAsync(job);
                    }
                    else
                    {
                        _logger.LogError("Job {JobId} failed after {MaxRetries} retries", job.JobId, job.MaxRetries);
                    }
                }
            }
        }
        
        private async Task ProcessBuildJobAsync(INodeRAGClient client, NodeRAGBackgroundJob job, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting graph build for UserId={UserId}", job.UserId);
            
            try
            {
                // Trigger build
                _logger.LogInformation("Calling TriggerBuildAsync for UserId={UserId}", job.UserId);
                var buildResult = await client.TriggerBuildAsync(job.UserId, incremental: true);
                _logger.LogInformation("TriggerBuildAsync completed for UserId={UserId}, Success={Success}, BuildId={BuildId}, Status={Status}", 
                    job.UserId, buildResult.Success, buildResult.BuildId, buildResult.Status);
                
                if (!buildResult.Success)
                {
                    throw new InvalidOperationException($"Failed to trigger build: {buildResult.Message}");
                }
                
                // If build completed synchronously (status is already "completed"), no need to poll
                if (buildResult.Status == "completed")
                {
                    _logger.LogInformation("Build completed synchronously for UserId={UserId}, Duration={Duration}s, Nodes={Nodes}, Edges={Edges}", 
                        job.UserId, buildResult.DurationSeconds, buildResult.NodesCreated, buildResult.EdgesCreated);
                    return;
                }
                
                // If no build ID, we can't poll status
                if (string.IsNullOrEmpty(buildResult.BuildId))
                {
                    _logger.LogWarning("No BuildId returned for UserId={UserId}, assuming build completed", job.UserId);
                    return;
                }
                
                // Poll for completion
                var pollingInterval = TimeSpan.FromSeconds(
                    int.Parse(_configuration["NodeRAG:BuildPollingIntervalSeconds"] ?? "5"));
                var timeout = TimeSpan.FromMinutes(
                    int.Parse(_configuration["NodeRAG:BuildTimeoutMinutes"] ?? "10"));
                var startTime = DateTime.UtcNow;
                
                _logger.LogInformation("Starting build status polling for UserId={UserId}, BuildId={BuildId}, PollInterval={PollInterval}s, Timeout={Timeout}m", 
                    job.UserId, buildResult.BuildId, pollingInterval.TotalSeconds, timeout.TotalMinutes);
                
                while (DateTime.UtcNow - startTime < timeout && !cancellationToken.IsCancellationRequested)
                {
                    var elapsed = DateTime.UtcNow - startTime;
                    _logger.LogInformation("Polling build status (elapsed: {Elapsed}s): UserId={UserId}, BuildId={BuildId}", 
                        elapsed.TotalSeconds, job.UserId, buildResult.BuildId);
                    
                    var status = await client.GetBuildStatusAsync(buildResult.BuildId);
                    
                    _logger.LogInformation("Build status for UserId={UserId}: {Status}, Stage={Stage}, StagesCompleted={StagesCount}", 
                        job.UserId, status.Status, status.CurrentStage, status.StagesCompleted?.Count ?? 0);
                    
                    if (status.Status == "completed")
                    {
                        _logger.LogInformation("Build completed successfully for UserId={UserId}, BuildId={BuildId}", 
                            job.UserId, buildResult.BuildId);
                        return;
                    }
                    else if (status.Status == "failed")
                    {
                        var errorMsg = status.ErrorDetails ?? "Unknown error";
                        _logger.LogError("Build failed for UserId={UserId}, BuildId={BuildId}, Error={Error}", 
                            job.UserId, buildResult.BuildId, errorMsg);
                        throw new InvalidOperationException($"Build failed for UserId {job.UserId}: {errorMsg}");
                    }
                    
                    await Task.Delay(pollingInterval, cancellationToken);
                }
                
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogWarning("Build polling cancelled for UserId={UserId}, BuildId={BuildId}", job.UserId, buildResult.BuildId);
                    return;
                }
                
                throw new TimeoutException($"Build timed out after {timeout.TotalMinutes} minutes for UserId {job.UserId}, BuildId {buildResult.BuildId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProcessBuildJobAsync for UserId={UserId}", job.UserId);
                throw;
            }
        }
        
        private async Task ProcessQASyncJobAsync(INodeRAGClient client, NodeRAGBackgroundJob job)
        {
            if (job.Metadata == null || !job.Metadata.ContainsKey("QAPair"))
            {
                throw new InvalidOperationException("QAPair metadata not found in job");
            }
            
            var qaPair = job.Metadata["QAPair"] as NodeRAGQAPairCreateDto;
            if (qaPair == null)
            {
                throw new InvalidOperationException("Invalid QAPair metadata");
            }
            
            await client.CreateQAPairAsync(qaPair);
            _logger.LogInformation("Q&A pair synced successfully for UserId={UserId}, QuestionId={QuestionId}", 
                job.UserId, qaPair.QuestionId);
        }
    }
}

