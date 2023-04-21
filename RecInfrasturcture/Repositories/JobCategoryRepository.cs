using RecCore.Contracts.Repositories;
using RecCore.Entities;
using RecInfrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecInfrastructure.Repositories
{
    public class JobCategoryRepository : BaseRepository<JobCategory>, IJobCategoryRepository
    {
        public JobCategoryRepository(RecDbContext dbContext) : base(dbContext)
        {
        }
    }
}
