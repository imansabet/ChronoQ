using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel.CQRS;
using UserService.Contracts;
using UserService.Models.Domain.Entities;

namespace UserService.Features.RegisterCustomer;

public record RegisterCustomerCommand(string Email, string Password, string FullName)
    : ICommand<RegisterCustomerResult>;

public record RegisterCustomerResult(Guid UserId);

public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.FullName).NotEmpty().MinimumLength(2);
    }
}


internal   class RegisterCustomerCommandHandler
    (IUserRepository userRepository , IPasswordHasher<User> hasher )
    : IRequestHandler<RegisterCustomerCommand, RegisterCustomerResult>
{
    public async Task<RegisterCustomerResult> Handle(RegisterCustomerCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByEmailAsync(command.Email))
            throw new Exception("Email already registered.");

        var userId = Guid.NewGuid();
        var user = User.RegisterCustomer(userId, command.Email, string.Empty, command.FullName);

        var hashedPassword = hasher.HashPassword(user, command.Password);
        user.SetHashedPassword(hashedPassword);

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();

        return new RegisterCustomerResult(user.Id);

    }
}