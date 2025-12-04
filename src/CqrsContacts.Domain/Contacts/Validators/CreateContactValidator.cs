namespace CqrsContacts.Domain.Contacts.Validators;

using CqrsContacts.Domain.Contacts.Commands.Requests;
using FluentValidation;

public class CreateContactValidator : AbstractValidator<CreateContactRequest>
{
    public CreateContactValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required.");
    }
}
