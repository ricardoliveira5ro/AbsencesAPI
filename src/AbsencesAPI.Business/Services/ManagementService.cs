using AbsencesAPI.Common.DTOS.Management;
using AbsencesAPI.Common.Interfaces;
using AbsencesAPI.Common.Model;
using AutoMapper;

namespace AbsencesAPI.Business.Services;

public class ManagementService : IManangementService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Management> ManagementRepository { get; }

    public ManagementService(IMapper mapper, IGenericRepository<Management> managementRepository)
    {
        Mapper = mapper;
        ManagementRepository = managementRepository;
    }

    public async Task<int> CreateManagementAsync(ManagementCreate managementCreate)
    {
        var entity = Mapper.Map<Management>(managementCreate);
        await ManagementRepository.InsertAsync(entity);
        await ManagementRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteManagementAsync(ManagementDelete managementDelete)
    {
        var entity = await ManagementRepository.GetByIdAsync(managementDelete.Id);
        ManagementRepository.Delete(entity);
        await ManagementRepository.SaveChangesAsync();
    }

    public async Task<List<ManagementGet>> GetManagementAsync()
    {
        var entities = await ManagementRepository.GetAsync(null, null);
        return Mapper.Map<List<ManagementGet>>(entities);
    }

    public async Task<ManagementGet> GetManagementByIdAsync(int id)
    {
        var entity = await ManagementRepository.GetByIdAsync(id);
        return Mapper.Map<ManagementGet>(entity);
    }

    public async Task UpdateManagementAsync(ManagementUpdate managementUpdate)
    {
        var entity = Mapper.Map<Management>(managementUpdate);
        ManagementRepository.Update(entity);
        await ManagementRepository.SaveChangesAsync();
    }
}
