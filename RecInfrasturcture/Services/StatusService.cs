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
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly ISubmissionRepository _submissionRepository;
        public StatusService(IStatusRepository statusRepository, ISubmissionRepository submissionRepository)
        {
            _statusRepository = statusRepository;
            _submissionRepository = submissionRepository;

        }
        public async Task<int> AddStatusAsync(StatusRequestModel model)
        {
            //Looks for the associated submission to compare status states.If it isnt changed, reject status addition.
            var relevantSubmission = await _submissionRepository.FirstOrDefaultWithIncludesAsync(s => s.CandidateId == model.CandidateId &&
                s.JobRequirementId == model.JobRequirementId, s => s.Status);
            //Last changed status
            var statusList = relevantSubmission.Status.Count - 1;
            var existingStatus = relevantSubmission.Status.FirstOrDefault(s => s.Id == relevantSubmission.Status[statusList].Id);
            if (existingStatus != null && existingStatus.State == model.State)
            {
                throw new Exception("Status is not changing");
            }
            Status status = new Status();
            if (model != null)
            {
                status.SubmissionId = relevantSubmission.Id;
                status.State = model.State;
                status.ChangedOn = DateTime.Now;
                status.StatusMessage = model.StatusMessage;
                status.Submission = relevantSubmission;
            }
            //returns number of rows affected, typically 1
            return await _statusRepository.InsertAsync(status);
        }

        public async Task<int> DeleteStatusAsync(int id)
        {
            //returns number of rows affected, typically 1
            return await _statusRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<StatusResponseModel>> GetAllStatus()
        {
            var statuses = await _statusRepository.GetAllAsync();
            var response = statuses.Select(x => x.ToStatusResponseModel());
            return response;
        }

        public async Task<StatusResponseModel> GetStatusByIdAsync(int id)
        {
            var stat = await _statusRepository.GetByIdAsync(id);
            if (stat != null)
            {
                var response = stat.ToStatusResponseModel();
                return response;
            }
            else
            {
                throw new NotFoundException("Status", id);
            }
        }

        public async Task<int> UpdateStatusAsync(StatusRequestModel model)
        {
            var existingStatus = await _statusRepository.GetByIdAsync(model.Id);
            if (existingStatus == null)
            {
                throw new Exception("Status does not exist");
            }
            if (existingStatus.State == model.State)
            {
                throw new Exception("Status is not changing");
            }
            if (model != null)
            {
                Status status = new Status();
                status.Id = model.Id;
                status.SubmissionId = model.SubmissionId;
                status.State = model.State;
                status.ChangedOn = DateTime.Now;
                status.StatusMessage = model.StatusMessage;
                return await _statusRepository.UpdateAsync(status);
            }
            else
            {
                return -1;
            }
        }
    }
}
