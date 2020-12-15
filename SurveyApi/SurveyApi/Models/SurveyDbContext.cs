using Microsoft.EntityFrameworkCore;
using SurveyApi.Models.Entities;

namespace SurveyApi.Models
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnsers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // todo: add more keys, constrains to data access level.
            
            modelBuilder.Entity<Survey>()
                .HasKey(c => c.Id);
            
            modelBuilder.Entity<Question>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Survey)
                .WithMany(s => s.Questions);
            
            modelBuilder.Entity<Option>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<Option>()
                .HasOne(o => o.Question)
                .WithMany(q => q.Options);
            
            modelBuilder.Entity<Answer>()
                .HasKey(u => u.Id);
            
            modelBuilder.Entity<QuestionAnswer>()
                .HasKey(qa => new {qa.QuestionId, qa.AnswerId});
            modelBuilder.Entity<QuestionAnswer>()
                .HasOne(qa => qa.Answer)
                .WithMany(a => a.QuestionAnswers);
            
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}