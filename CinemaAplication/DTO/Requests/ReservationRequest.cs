namespace CinemaApplication.DTO.Requests
{
    public class ReservationRequest
    {

        public int Id { get; set; }
        public int customer_id { get; set; }


        public string seance_id { get; set; } = null!;


        public string place_id { get; set; } = null!;


        public string reserv_date { get; set; } = null!;
    }
}