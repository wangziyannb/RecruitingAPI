using Microsoft.EntityFrameworkCore;
using RecCore.Contracts.Repositories;
using RecCore.Entities;
using RecInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RecInfrastructure.Repositories
{
    public class JobRequirementRepository : BaseRepository<JobRequirement>, IJobRequirementRepository
    {
        public JobRequirementRepository(RecDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<JobRequirement>> GetJobRequirementsIncludingCategory(Expression<Func<JobRequirement, bool>> filter)
        {
            return await _dbContext.JobRequirements.Include("JobCategory").Where(filter).ToListAsync();
        }
    }
}
