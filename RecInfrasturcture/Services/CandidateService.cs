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
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }
        public async Task<int> AddCandidateAsync(CandidateRequestModel model)
        {
            var existingCandidate = await _candidateRepository.GetUserByEmail(model.Email);
            if (existingCandidate != null)
            {
                //this email has been registered
                throw new Exception("Email is already used");
            }
            Candidate candidate = new Candidate();
            if (model != null)
            {
                candidate = model.CandidateRequestToCandidate(false);
            }
            return await _candidateRepository.InsertAsync(candidate);
        }

        public async Task<int> DeleteCandidateAsync(int id)
        {
            return await _candidateRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CandidateResponseModel>> GetAllCandidates()
        {
            var candidates = await _candidateRepository.GetAllAsync();
            var response = candidates.Select(x => x.ToCandidateResponseModel());
            return response;
        }

        public async Task<CandidateResponseModel> GetCandidateByIdAsync(int id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);
            if (candidate != null)
            {
                var response = candidate.ToCandidateResponseModel();
                return response;
            }
            else
            {
                throw new NotFoundException("Candidate", id);
            }
        }

        public async Task<int> UpdateCandidateAsync(CandidateRequestModel model)
        {
            var existingCandidate = await _candidateRepository.GetByIdAsync(model.Id);
            if (existingCandidate == null)
            {
                throw new Exception("Candidate does not exist");
            }
            
            if (model != null)
            {
                Candidate candidate = model.CandidateRequestToCandidate(true);
                return await _candidateRepository.UpdateAsync(candidate);
            }
            else
            {
                //unsuccessful update
                return -1;
            }
        }
    }
}
