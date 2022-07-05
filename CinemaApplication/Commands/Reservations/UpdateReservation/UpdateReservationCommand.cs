using MediatR;
using CinemaApplication.DTO.Requests;

namespace CinemaApplication.Commands.Reservations.UpdateReservation
{
    public class UpdateReservationCommand : IRequest
    {
        public ReservationRequest ReservationRequest { get; set; }
    }
}