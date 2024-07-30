using Domain.Common;

namespace Domain.Errors;

public class Error : ValueObject
{
    public Error(string code)
    {
        Code = code;
    }

    public string Code { get; set; }

    protected override IEnumerable<object?> GetPropertiesForComparison()
    {
        yield return Code;
    }
}
