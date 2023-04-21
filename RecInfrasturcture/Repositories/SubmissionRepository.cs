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
    public class SubmissionRepository : BaseRepository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(RecDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Submission> FirstOrDefaultWithIncludesAsync(Expression<Func<Submission, bool>> where, params Expression<Func<Submission, object>>[] includes)
        {
            var query=_dbContext.Set<Submission>().AsQueryable();
            if (includes != null) { 
                foreach (var include in includes)
                {
                    query=query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(where);
        }
    }
}
