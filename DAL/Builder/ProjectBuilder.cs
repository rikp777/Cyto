using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DAL.Builder
{
    public class ProjectBuilder
    {
        private const string TableName = "Project";
        
        public ProjectBuilder(EntityTypeConfiguration<ProjectEntity> modelBuilder)
        {
            modelBuilder.ToTable(TableName);
        }
    }
}