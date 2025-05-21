using Application.Domain.Models.Response;

namespace Application.External.Response;

public class ExternalResponse : BaseResponse
{
}
public class ExternalResponse<T> : ExternalResponse
{
    public T? Content { get; set; }
}