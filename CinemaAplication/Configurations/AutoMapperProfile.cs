using AutoMapper;
using CinemaApplication.Commands;
using CinemaApplication.DTO.Requests;
using CinemaApplication.DTO.Responses;
using CinemaDomain.Entities;

namespace Application.Configurations
{
    public class AutoMapperProfile : Profile
    {

        private void CreateReviewMaps()
        {
            CreateMap<ReviewRequest, Review>();
            
            CreateMap<Review, ReviewResponse>();
        }

        public AutoMapperProfile()
        {
            CreateReviewMaps();
        }
    }
}
