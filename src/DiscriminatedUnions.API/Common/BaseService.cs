using ErrorOr;

namespace DiscriminatedUnions.API.Common;

public abstract class BaseService
{

    protected BaseResult result;

    protected BaseService()
    {
        result = new BaseResult();
    }

    protected void AddError(Error error)
    {
        result.Errors.Add(error);
    }

}
