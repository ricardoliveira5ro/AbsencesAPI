using AbsencesAPI.Business.Exceptions;
using AbsencesAPI.Business.Validation.Absence;
using AbsencesAPI.Common.DTOS.Absence;
using AbsencesAPI.Common.DTOS.Employee;
using AbsencesAPI.Common.DTOS.Management;
using AbsencesAPI.Common.DTOS.Stats;
using AbsencesAPI.Common.Interfaces;
using AbsencesAPI.Common.Model;
using AutoMapper;
using FluentValidation;
using System.Linq.Expressions;

namespace AbsencesAPI.Business.Services;

public class AbsenceService : IAbsenceService
{
    private IGenericRepository<Absence> AbsenceRepository { get; }
    private IGenericRepository<Employee> EmployeeRepository { get; }
    private IGenericRepository<Stats> StatsRepository { get; }
    private IMapper Mapper { get; }
    private AbsenceCreateValidator CreateValidator { get; }
    private AbsenceUpdateValidator UpdateValidator { get; }

    public AbsenceService(IGenericRepository<Absence> absenceRepository,
                            IGenericRepository<Employee> employeeRepository,
                            IGenericRepository<Stats> statsRepository,
                            IMapper mapper,
                            AbsenceCreateValidator createValidator,
                            AbsenceUpdateValidator updateValidator)
    {
        AbsenceRepository = absenceRepository;
        EmployeeRepository = employeeRepository;
        StatsRepository = statsRepository;
        Mapper = mapper;
        CreateValidator = createValidator;
        UpdateValidator = updateValidator;
    }


    public async Task<int> CreateAbsenceAsync(AbsenceCreate absenceCreate)
    {
        await CreateValidator.ValidateAndThrowAsync(absenceCreate);

        Expression<Func<Employee, bool>> employeeFilter = (employee) => absenceCreate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] {employeeFilter}, null, null);

        var statistic = await StatsRepository.GetByIdAsync(absenceCreate.StatsId);

        var entity = Mapper.Map<Absence>(absenceCreate);
        entity.Employees = employees;
        entity.Statistic = statistic;

        await AbsenceRepository.InsertAsync(entity);
        await AbsenceRepository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAbsenceAsync(AbsenceDelete absenceDelete)
    {
        var entity = await AbsenceRepository.GetByIdAsync(absenceDelete.Id);

        if (entity == null)
            throw new NotFoundException(absenceDelete.Id, "Absence");

        if (entity.Employees.Count > 0)
            throw new DependentEntitiesException(entity.Employees.Select(a => a.Id).ToList(), "Employees");

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
        await UpdateValidator.ValidateAndThrowAsync(absenceUpdate);

        Expression<Func<Employee, bool>> employeeFilter = (employee) => absenceUpdate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);
        
        var existingEntity = await AbsenceRepository.GetByIdAsync(absenceUpdate.Id, (absence) => absence.Employees);
        Mapper.Map(absenceUpdate, existingEntity);
        existingEntity.Employees = employees;

        AbsenceRepository.Update(existingEntity);
        await AbsenceRepository.SaveChangesAsync();
    }
}
