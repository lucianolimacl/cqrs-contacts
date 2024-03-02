namespace CqrsContacts.Domain.Contacts.Validators
{
    using CqrsContacts.Domain.Contacts.Commands.Requests;
    using FluentValidation;

    public class UpdateContactValidator : AbstractValidator<UpdateContactRequest>
    {
        public UpdateContactValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone is required.");
        }
    }
}
