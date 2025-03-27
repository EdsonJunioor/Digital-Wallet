using AutoMapper;
using DigitalWallet.DTO;
using DigitalWallet.Models;

namespace DigitalWallet.AutoMapper
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<UserDto, User>();

            CreateMap<TransactionDto, Transaction>();
        }
    }
}
