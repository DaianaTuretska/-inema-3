namespace CinemaApplication.DTO.Responses
{
    public class ReservationResponse
    {
        public int customer_id { get; set; }


        public string seance_id { get; set; } = null!;


        public string place_id { get; set; } = null!;


        public string reserv_date { get; set; } = null!;
    }
}
