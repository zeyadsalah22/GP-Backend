using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using GPBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Models
{
    [ModelMetadataType(typeof(CompanyMetaData))]
    public partial class Company
    {
    }

    public class CompanyMetaData
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(255)]
        public string? Location { get; set; }

        [StringLength(255)]
        public string? CareersLink { get; set; }

        [StringLength(255)]
        public string? LinkedinLink { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] Rowversion { get; set; } = null!;

        public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

        public virtual ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>();
    }


    public static class CompanyEndpoints
    {
        public static void MapCompanyEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Company").WithTags(nameof(Company));

            group.MapGet("/", async (GPDBContext db) =>
            {
                return await db.Companies.ToListAsync();
            })
            .WithName("GetAllCompanies")
            .WithOpenApi();

            group.MapGet("/{id}", async Task<Results<Ok<Company>, NotFound>> (int companyid, GPDBContext db) =>
            {
                return await db.Companies.AsNoTracking()
                    .FirstOrDefaultAsync(model => model.CompanyId == companyid)
                    is Company model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
            .WithName("GetCompanyById")
            .WithOpenApi();

            group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int companyid, Company company, GPDBContext db) =>
            {
                var affected = await db.Companies
                    .Where(model => model.CompanyId == companyid)
                    .ExecuteUpdateAsync(setters => setters
                      .SetProperty(m => m.CompanyId, company.CompanyId)
                      .SetProperty(m => m.Name, company.Name)
                      .SetProperty(m => m.Location, company.Location)
                      .SetProperty(m => m.CareersLink, company.CareersLink)
                      .SetProperty(m => m.LinkedinLink, company.LinkedinLink)
                      .SetProperty(m => m.CreatedAt, company.CreatedAt)
                      .SetProperty(m => m.UpdatedAt, company.UpdatedAt)
                      .SetProperty(m => m.IsDeleted, company.IsDeleted)
                      .SetProperty(m => m.Rowversion, company.Rowversion)
                      );
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("UpdateCompany")
            .WithOpenApi();

            group.MapPost("/", async (Company company, GPDBContext db) =>
            {
                db.Companies.Add(company);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Company/{company.CompanyId}", company);
            })
            .WithName("CreateCompany")
            .WithOpenApi();

            group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int companyid, GPDBContext db) =>
            {
                var affected = await db.Companies
                    .Where(model => model.CompanyId == companyid)
                    .ExecuteDeleteAsync();
                return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
            })
            .WithName("DeleteCompany")
            .WithOpenApi();
        }
    }
}