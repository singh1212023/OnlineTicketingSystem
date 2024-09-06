using AutoMapper;
using OTS.Common.RequestModels.Ticket;
using OTS.Common.RequestModels.Tickets;
using OTS.Common.RequestModels.User;
using OTS.Core.Entities;
using OTS.Core.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace OTS.WebAPIs.MappingProfile
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApplicationUser, GetUserByIdRequestModel>().ReverseMap();
            CreateMap<ApplicationUser, SaveUserRequestModel>().ReverseMap();
            CreateMap<ApplicationUser, UpdateUserRequestModel>().ReverseMap();
            CreateMap<ApplicationUser, GetAllUserRequestModel>();
            CreateMap<SaveTicketRequestModel, Ticket>();
            CreateMap<Ticket, GetTicketByIdRequestModel>();
            CreateMap<Ticket, GetAllTicketByIdRequestModel>();
            CreateMap<Ticket, GetAllTicketsRequestModel>();
         


        }
    }
}
