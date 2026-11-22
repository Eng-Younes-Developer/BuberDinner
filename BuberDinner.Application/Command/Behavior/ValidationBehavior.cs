using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using BuberDinner.Application.Authintication.Queries.Login;
using FluentValidation;
using BuberDinner.Application.Authintication.ResponseAuth;
using BuberDinner.Application.Authintication.Commands.Register;
using BuberDinner.Application.Command.interfaces;



namespace BuberDinner.Application.Command.Behavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>  where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehavior(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return await next();
        }
    }
}
