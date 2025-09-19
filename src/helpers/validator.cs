using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;

namespace Chat_Application.src.Helpers
{
    public class ValidateRequest<TRequest> : IEndpointFilter
    {
        private readonly AbstractValidator<TRequest> _validator;

        public ValidateRequest(AbstractValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var request = context.GetArgument<TRequest>(0);

            if (request == null)
            {
                Log.Error("Error in finding the request");
                return TypedResults.BadRequest();
            }

            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                Log.Information($"Validation failed: {string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}");
                return TypedResults.ValidationProblem(
                    validationResult.Errors.ToDictionary(
                        e => e.PropertyName,
                        e => new[] { e.ErrorMessage }
                    )
                );
            }

            return await next(context);
        }
    }
}
