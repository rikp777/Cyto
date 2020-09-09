using System.Data.Entity.ModelConfiguration;
using Domain.Entities;

namespace DAL.Builder
{
    public class ExperimentBuilder
    {
        private const string TableName = "Experiment";
        
        public ExperimentBuilder(EntityTypeConfiguration<ExperimentEntity> modelBuilder)
        {
            modelBuilder.ToTable(TableName);
        }
    }
}