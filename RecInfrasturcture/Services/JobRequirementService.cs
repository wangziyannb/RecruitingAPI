using RecCore.Contracts.Repositories;
using RecCore.Contracts.Services;
using RecCore.Entities;
using RecCore.Exceptions;
using RecCore.Models;
using RecInfrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecInfrastructure.Services
{
    public class JobRequirementService : IJobRequirementService
    {
        private readonly IJobRequirementRepository _jobRequirementRepository;
        public JobRequirementService(IJobRequirementRepository jobRequirementRepository)
        {
            _jobRequirementRepository = jobRequirementRepository;
        }

        public async Task<int> AddJobRequirementAsync(JobRequirementRequestModel model)
        {
            JobRequirement jobRequirement = new JobRequirement();
            if (model != null)
            {
                jobRequirement.NumberOfPosition = model.NumberOfPosition;
                jobRequirement.Title = model.Title;
                jobRequirement.Description = model.Description;
                jobRequirement.HiringManagerId = model.HiringManagerId;
                jobRequirement.StartDate = model.StartDate;
                jobRequirement.IsActive = model.IsActive;
                jobRequirement.ClosedOn = model.ClosedOn;
                jobRequirement.ClosedReason = model.ClosedReason;
                jobRequirement.CreatedOn = model.CreatedOn;
                //jobRequirement.JobCategory = new JobCategory();
                jobRequirement.Submissions = new List<Submission>();
                jobRequirement.EmployeeRequirementTypes = new List<EmployeeRequirementType>();
            }
            //returns number of rows affected, typically 1
            return await _jobRequirementRepository.InsertAsync(jobRequirement);
        }

        public async Task<int> DeleteJobRequirementAsync(int id)
        {
            //returns number of rows affected, typically 1
            return await _jobRequirementRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<JobRequirementResponseModel>> GetAllJobRequirements()
        {
            var jRTypes = await _jobRequirementRepository.GetAllAsync();
            var response = jRTypes.Select(x => x.ToJobRequirementResponseModel());
            return response;
        }

        public async Task<JobRequirementResponseModel> GetJobRequirementByIdAsync(int id)
        {
            var jR = await _jobRequirementRepository.GetByIdAsync(id);
            if (jR != null)
            {
                var response = jR.ToJobRequirementResponseModel();
                return response;
            }
            else
            {
                throw new NotFoundException("JobRequirement", id);
            }
        }

        public async Task<int> UpdateJobRequirementAsync(JobRequirementRequestModel model)
        {
            var existingJobRequirement = await _jobRequirementRepository
                .GetJobRequirementsIncludingCategory(x => model.Id == x.Id);
            if (existingJobRequirement == null)
            {
                throw new Exception("JobRequirement does not exist");
            }
            JobRequirement jobRequirement = new JobRequirement();
            if (model != null)
            {
                jobRequirement.NumberOfPosition = model.NumberOfPosition;
                jobRequirement.Title = model.Title;
                jobRequirement.Description = model.Description;
                jobRequirement.HiringManagerId = model.HiringManagerId;
                jobRequirement.StartDate = model.StartDate;
                jobRequirement.IsActive = model.IsActive;
                jobRequirement.ClosedOn = model.ClosedOn;
                jobRequirement.ClosedReason = model.ClosedReason;
                jobRequirement.CreatedOn = model.CreatedOn;
                jobRequirement.JobCategory = model.JobCategory;
                return await _jobRequirementRepository.UpdateAsync(jobRequirement);
            }
            else
            {
                //unsuccessful update
                return -1;
            }
        }
    }
}
