using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbsencesAPI.Common.DTOS;

namespace AbsencesAPI.Common.Interfaces;

public interface IManangementService
{
    Task<int> CreateManagementAsync(ManagementCreate managementCreate);
    Task UpdateManagementAsync(ManagementUpdate managementUpdate);
    Task DeleteManagementAsync(ManagementDelete managementDelete);
    Task<ManagementGet> GetManagementByIdAsync(int id);
    Task<List<ManagementGet>> GetManagementAsync();
}
