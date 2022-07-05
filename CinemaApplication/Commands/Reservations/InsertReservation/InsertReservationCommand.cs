using MediatR;
using CinemaApplication.DTO.Requests;

namespace CinemaApplication.Commands.Reservations.InsertReservation
{
    public class InsertReservationCommand : IRequest
    {
        public ReservationRequest ReviewRequest { get; set; }
    }
}