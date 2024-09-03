using FluentValidation;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CompanyManagement.Application.CustomValidators;

public static partial class StrongPasswordValidator
{
    public static IRuleBuilderOptions<T, TElement> MustBeStrongPassword<T, TElement>(this IRuleBuilder<T, TElement> ruleBuilder)
    {
        return ruleBuilder.Must(p => p is not null && StrongPasswordRegex().IsMatch(p.ToString()!));
    }

    [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", RegexOptions.Compiled)]
    private static partial Regex StrongPasswordRegex();
}
