using AbsencesAPI.Common.DTOS.Absence;
using AbsencesAPI.Common.Interfaces;
using AbsencesAPI.Common.Model;
using AutoMapper;
using System.Linq.Expressions;

namespace AbsencesAPI.Business.Services;

public class AbsenceService : IAbsenceService
{
    private IGenericRepository<Absence> AbsenceRepository { get; }
    private IGenericRepository<Employee> EmployeeRepository { get; }
    private IMapper Mapper { get; }

    public AbsenceService(IGenericRepository<Absence> absenceRepository,
                            IGenericRepository<Employee> employeeRepository,
                            IMapper mapper)
    {
        AbsenceRepository = absenceRepository;
        EmployeeRepository = employeeRepository;
        Mapper = mapper;
    }


    public async Task<int> CreateAbsenceAsync(AbsenceCreate absenceCreate)
    {
        Expression<Func<Employee, bool>> employeeFilter = (employee) => absenceCreate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] {employeeFilter}, null, null);

        var entity = Mapper.Map<Absence>(absenceCreate);
        entity.Employees = employees;

        await AbsenceRepository.InsertAsync(entity);
        await AbsenceRepository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAbsenceAsync(AbsenceDelete absenceDelete)
    {
        var entity = await AbsenceRepository.GetByIdAsync(absenceDelete.Id);
        AbsenceRepository.Delete(entity);
        await AbsenceRepository.SaveChangesAsync();
    }

    public async Task<AbsenceGet> GetAbsenceByIdAsync(int id)
    {
        var entity = await AbsenceRepository.GetByIdAsync(id, (absence) => absence.Employees);

        return Mapper.Map<AbsenceGet>(entity);
    }

    public async Task<List<AbsenceGet>> GetAbsencesAsync()
    {
        var entities = await AbsenceRepository.GetAsync(null, null, (absence) => absence.Employees);

        return Mapper.Map<List<AbsenceGet>>(entities);
    }

    public async Task UpdateAbsenceAsync(AbsenceUpdate absenceUpdate)
    {
        Expression<Func<Employee, bool>> employeeFilter = (employee) => absenceUpdate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);
        
        var existingEntity = await AbsenceRepository.GetByIdAsync(absenceUpdate.Id, (absence) => absence.Employees);
        Mapper.Map(absenceUpdate, existingEntity);
        existingEntity.Employees = employees;

        AbsenceRepository.Update(existingEntity);
        await AbsenceRepository.SaveChangesAsync();
    }
}
