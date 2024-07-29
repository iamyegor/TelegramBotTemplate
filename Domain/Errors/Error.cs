using Domain.Common;

namespace Domain.Errors;

public class Error : ValueObject
{
    public string Code { get; set; }

    public Error(string code)
    {
        Code = code;
    }

    protected override IEnumerable<object?> GetPropertiesForComparison()
    {
        yield return Code;
    }
}
