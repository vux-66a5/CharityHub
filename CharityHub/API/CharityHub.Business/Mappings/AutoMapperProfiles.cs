

using AutoMapper;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Models;

namespace CharityHub.Business.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<AddCampaignRequestDto, Campaign>().ReverseMap();
            CreateMap<Campaign, CampaignDto>().ReverseMap();
            CreateMap<UpdateCampaignRequestDto, Campaign>().ReverseMap();
            CreateMap<AddDonationRequestDto, Donation>().ReverseMap();
            CreateMap<Donation, DonationDto>().ReverseMap();
        }
    }
}
