using AutoMapper;
using CinemaApplication.Commands;
using CinemaApplication.DTO.Requests;
using CinemaApplication.DTO.Responses;
using CinemaDomain.Entities;

namespace CinemaApplication.Configurations
{
    public class AutoMapperProfile : Profile
    {

        private void CreateReviewMaps()
        {
            CreateMap<ReservationRequest, Reservation>();
            
            CreateMap<Reservation, ReservationResponse>();
        }

        public AutoMapperProfile()
        {
            CreateReviewMaps();
        }
    }
}
