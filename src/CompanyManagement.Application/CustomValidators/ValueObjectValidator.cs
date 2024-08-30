using ErrorOr;
using FluentValidation;

namespace CompanyManagement.Application.CustomValidators;

public static class ValueObjectValidator
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(this IRuleBuilder<T, TElement> ruleBuilder, Func<TElement, ErrorOr<TValueObject>> valueObjectFactory)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            var result = valueObjectFactory(value);

            if (!result.IsError)
            {
                return;
            }

            context.AddFailure(
                new FluentValidation.Results.ValidationFailure(
                    nameof(T), 
                    $"{result.FirstError.Code} {result.FirstError.Description}"));
        });
    }
}
