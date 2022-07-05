using FluentValidation;
using Application.DTO.Requests;

namespace Services_Application.Validation.Requests
{
    public class ServiceRequestValidator : AbstractValidator<ReservationRequest>
    {
        public ServiceRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Id)}  can't be empty.");

            RuleFor(request => request.CarId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.CarId)}  can't be empty.");

            RuleFor(request => request.ClientId)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.ClientId)}  can't be empty.");

            RuleFor(request => request.Rating)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Rating)}  can't be empty.")
                .MaximumLength(1)
                .WithMessage(request => $"{nameof(request.Rating)} value must be between 0 and 5");

            RuleFor(request => request.TextReviews)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.TextReviews)}  can't be empty.")
                .MaximumLength(100)
                .WithMessage(request => $"{nameof(request.TextReviews)} value should be shorter than 100 characters");
        }
    }
}