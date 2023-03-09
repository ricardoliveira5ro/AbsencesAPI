using AbsencesAPI.Common.Model;
using System.Runtime.Serialization;

namespace AbsencesAPI.Business.Exceptions;

[Serializable]
public class DependentAbsencesExistException : Exception
{
    public List<Absence> Absences { get; }

    public DependentAbsencesExistException()
    {
    }

    public DependentAbsencesExistException(List<Absence> absences)
    {
        this.Absences = absences;
    }

    public DependentAbsencesExistException(string? message) : base(message)
    {
    }

    public DependentAbsencesExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DependentAbsencesExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}