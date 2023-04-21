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
    public class EmployeeTypeService : IEmployeeTypeService
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository;
        public EmployeeTypeService(IEmployeeTypeRepository employeeTypeRepository)
        {
            _employeeTypeRepository = employeeTypeRepository;
        }
        public async Task<int> AddEmployeeTypeAsync(EmployeeTypeRequestModel model)
        {
            var existingEmployeeType = await _employeeTypeRepository.GetEmployeeTypeByTypeName(model.TypeName);
            if (existingEmployeeType != null)
            {
                throw new Exception("Employee Type already exists");
            }
            EmployeeType employeeType = new EmployeeType();
            if (model != null)
            {
                employeeType.TypeName = model.TypeName.ToLower();
            }
            //returns number of rows affected, typically 1
            return await _employeeTypeRepository.InsertAsync(employeeType);
        }

        public async Task<int> DeleteEmployeeTypeAsync(int id)
        {
            return await _employeeTypeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EmployeeTypeResponseModel>> GetAllEmployeeTypes()
        {
            var empTypes = await _employeeTypeRepository.GetAllAsync();
            var response = empTypes.Select(x => x.ToEmployeeTypeResponseModel());
            return response;
        }

        public async Task<EmployeeTypeResponseModel> GetEmployeeTypeByIdAsync(int id)
        {
            var empType = await _employeeTypeRepository.GetByIdAsync(id);
            if (empType != null)
            {
                var response = empType.ToEmployeeTypeResponseModel();
                return response;
            }
            else
            {
                throw new NotFoundException("EmployeeType", id);
            }
        }

        public async Task<int> UpdateEmployeeTypeAsync(EmployeeTypeRequestModel model)
        {
            var existingEmployeeType = await _employeeTypeRepository.GetEmployeeTypeByTypeName(model.TypeName);
            if (existingEmployeeType == null)
            {
                throw new Exception("EmployeeType does not exist");
            }
            EmployeeType employeeType = new EmployeeType();
            if (model != null)
            {
                employeeType.TypeName = model.TypeName.ToLower();
                return await _employeeTypeRepository.UpdateAsync(employeeType);
            }
            else
            {
                //unsuccessful update
                return -1;
            }
        }
    }
}
