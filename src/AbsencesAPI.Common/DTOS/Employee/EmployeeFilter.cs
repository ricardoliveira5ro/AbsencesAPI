namespace AbsencesAPI.Common.DTOS.Employee;

public record EmployeeFilter(string? Name, int? EmployeeNumber, string? Department, string? Manager, string? Absence, int? Skip, int? Take);