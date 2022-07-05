using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaDomain.Entities
{
     public class Reservation
    {
        public int Id { get; set; }
        public int customer_id { get; set; } 


        public string seance_id { get; set; } = null!;


        public string place_id { get; set; } = null!;


        public DateTime reserv_date { get; set; }

      
    }
}
