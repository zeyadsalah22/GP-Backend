using GPBackend.Models;

namespace GPBackend.Data
{
    public class IndustrySeedData
    {
        public static Industry[] GetIndustries()
        {
            return
            [
                new Industry { IndustryId = 1, Name = "Technology", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false },
                new Industry { IndustryId = 2, Name = "Banking & Finance", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false },
                new Industry { IndustryId = 3, Name = "Healthcare", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false },
                new Industry { IndustryId = 4, Name = "Real Estate", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false },
                new Industry { IndustryId = 5, Name = "Construction", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false },
                new Industry { IndustryId = 6, Name = "Manufacturing", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false },
                new Industry { IndustryId = 7, Name = "Retail & E-commerce", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false },
                new Industry { IndustryId = 8, Name = "Logistics & Transportation", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false },
                new Industry { IndustryId = 9, Name = "Consulting", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false },
                new Industry { IndustryId = 10, Name = "Education", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false },
                new Industry { IndustryId = 11, Name = "Government", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false },
                new Industry { IndustryId = 12, Name = "Other", CreatedAt = new DateTime(2025, 10, 14), UpdatedAt = new DateTime(2025, 10, 14), IsDeleted = false }
            ];
        }
    }
}
