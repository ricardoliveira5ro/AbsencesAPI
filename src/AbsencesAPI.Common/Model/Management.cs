using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsencesAPI.Common.Model;

public class Management : BaseEntity
{
    public string Manager { get; set; } = default!;
    public List<Employee> Employees { get; set; } = default!;
}
