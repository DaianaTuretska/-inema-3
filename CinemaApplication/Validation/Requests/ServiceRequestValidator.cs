using FluentValidation;
using CinemaApplication.DTO.Requests;

namespace CinemaApplication.Validation.Requests
{
    public class ServiceRequestValidator : AbstractValidator<ReservationRequest>
    {
        public ServiceRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Id)}  can't be empty.");

            RuleFor(request => request.customer_id)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.customer_id)}  can't be empty.");

            RuleFor(request => request.seance_id)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.seance_id)}  can't be empty.");

            RuleFor(request => request.place_id)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.place_id)}  can't be empty.");
                
           }
    }
}