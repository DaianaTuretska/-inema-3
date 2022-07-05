using MediatR;

namespace CinemaApplication.Commands.Reservations.DeleteReservation
{
    public class DeleteReservationCommand : IRequest
    {
        public int Id { get; set; }
    }
}