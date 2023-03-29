using ErrorOr;

namespace DiscriminatedUnions.API.Common;

public class BaseResult
{

    public List<Error> Errors { get; set; }
    public object Response { get; set; }

    public BaseResult()
    {
        Errors = new List<Error>();
        Response = null;
    }
    public bool IsValid()
    {
        return !Errors.Any();
    }

    public string GetErrorsMessages()
    {
        return string.Concat(Errors.ToArray());
    }

}
