using AbsencesAPI.Common.DTOS.Absence;
using AbsencesAPI.Common.DTOS.Management;

namespace AbsencesAPI.Common.DTOS.Employee;

public record EmployeeDetails(int id, string Name, int? EmployeeNumber, string Department, List<AbsenceGet> Absences, ManagementGet Management);