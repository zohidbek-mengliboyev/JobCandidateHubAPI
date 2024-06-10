using CandidateAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CandidateAPI.Data
{
    public class JobCandidateHubDbContext : DbContext
    {
        public JobCandidateHubDbContext(DbContextOptions<JobCandidateHubDbContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }
    }

}
