using System.Runtime.Serialization;

namespace AbsencesAPI.Business.Exceptions;

[Serializable]
public class StatNotFoundException : Exception
{
    public int Id { get; }

    public StatNotFoundException()
    {
    }

    public StatNotFoundException(int id)
    {
        Id = id;
    }

    public StatNotFoundException(string? message) : base(message)
    {
    }

    public StatNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected StatNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}