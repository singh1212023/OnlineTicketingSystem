using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using OTS.Common.Enums;
using OTS.Common.Helpers;
using OTS.Common.RequestModels.Ticket;
using OTS.Common.RequestModels.Tickets;
using OTS.Common.ResponseModel;
using OTS.Core.Entities;
using OTS.Core.Entities.Account;
using OTS.Infrastructure.Data;
using OTS.Repository.Interface.Email;
using OTS.Repository.Interface.Ticket;
using System.Linq;

namespace OTS.Repository.Services.Tickets
{
    public class TicketService : ITicketRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment enviornment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IEmailRepository email;

        private readonly IMapper mapper;

        public TicketService(ApplicationDbContext _context, IMapper _mapper, IHostingEnvironment _enviornment,
            UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager,
            IEmailRepository _email)
        {
            context = _context;
            mapper = _mapper;
            enviornment = _enviornment;
            userManager = _userManager;
            roleManager = _roleManager;
            email = _email;
        }



        public async Task<GenericBaseResult<List<GetAllTicketByIdRequestModel>>> GetAllTicketsByUserId(string Id)
        {
            var response = new GenericBaseResult<List<GetAllTicketByIdRequestModel>>(null);

            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    response.Message = ResponseMessage.NotFound;
                    return response;
                }
                var tickets = await context.Ticket.Where(t => t.RequestorId == Id).ToListAsync();

                if (tickets == null || tickets.Count == 0)
                {
                    response.Message = ResponseMessage.NotFound;
                    return response;
                }
                var ticketModels = mapper.Map<List<GetAllTicketByIdRequestModel>>(tickets);

                response.Result = ticketModels;
                response.Message = ResponseMessage.Success;

            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<List<GetAllTicketByIdRequestModel>>(null);
                result.AddExceptionLog(ex);
                return result;
            }
            return response;
        }

        public async Task<GenericBaseResult<List<GetAllTicketsRequestModel>>> GetAllTickets(SortingModel Request)
        {
            var response = new GenericBaseResult<List<GetAllTicketsRequestModel>>(null);
            try
            { 
                var ticketsQuery =  context.Ticket.AsQueryable();
                if (ticketsQuery == null)
                {
                    response.Message = ResponseMessage.NotFound;
                    return response;
                }
                if(!string.IsNullOrWhiteSpace(Request.SearchValue))
                {
                    var enumDepartment= EnumHelper.FindEnumsUsingSearchValue<Department>(Request.SearchValue);
                    var enumPriority = EnumHelper.FindEnumsUsingSearchValue<Priority>(Request.SearchValue);
                    var enumStatus = EnumHelper.FindEnumsUsingSearchValue<Status>(Request.SearchValue);

                    // Enum parsing succeeded; filter by department and other criteria
                    ticketsQuery = ticketsQuery.Where(x => x.TicketName.ToLower().Contains(Request.SearchValue) || 
                                                        enumDepartment.Contains(x.Department) || enumPriority.Contains(x.Priority)||
                                                        enumStatus.Contains(x.Status));
                   
                    //ticketsQuery = ticketsQuery.Where(t => t.TicketName.ToLower().Contains(Request.SearchValue) ||
                    //t.Department.ToString().Contains(Request.SearchValue) || t.Status.ToString().Contains(Request.SearchValue)|| t.LastUpdatedDate.ToString().Contains(Request.SearchValue));
                }
                else
                {
                    ticketsQuery = context.Ticket.AsQueryable();
                }

                ticketsQuery = Request.SortingKey switch
                {

                    "Department" => Request.IsDescending ? ticketsQuery.OrderByDescending(t => t.Department): ticketsQuery.OrderBy(t => t.Department),
                    "TicketName" => Request.IsDescending ? ticketsQuery.OrderByDescending(t => t.TicketName) : ticketsQuery.OrderBy(t => t.TicketName),
                    "Status" => Request.IsDescending ? ticketsQuery.OrderByDescending(t => t.Status) : ticketsQuery.OrderBy(t => t.Status),
                    "LastUpdatedDate" => Request.IsDescending ? ticketsQuery.OrderByDescending(t => t.LastUpdatedDate) : ticketsQuery.OrderBy(t => t.LastUpdatedDate),
                    _ => ticketsQuery.OrderBy(t => t.LastUpdatedDate),
                };

              //  var ticketModels = mapper.Map<List<GetAllTicketsRequestModel>>(tickets);

                // for pagination
                var totalRecords = await ticketsQuery.CountAsync();
                var pagedTickets = await ticketsQuery.Skip((Request.PageNumber - 1) * Request.PageSize).Take(Request.PageSize).ToListAsync();
                var ticketModels = mapper.Map<List<GetAllTicketsRequestModel>>(pagedTickets);
                response.Result = ticketModels;
                response.Message = ResponseMessage.Success;

            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<List<GetAllTicketsRequestModel>>(null);
                result.AddExceptionLog(ex);
                return result;
            }
            return response;
        }
                                            
        public async Task<GenericBaseResult<GetTicketByIdRequestModel>> GetTicketById(string Id)
        {
            var response = new GenericBaseResult<GetTicketByIdRequestModel>(null);
            try
            {
                if (string.IsNullOrEmpty(Id))
                {
                    response.Message = ResponseMessage.NotFound;
                    return response;
                }
                var user = context.Ticket.FirstOrDefault(u => u.Id == Id);
                var result = mapper.Map<GetTicketByIdRequestModel>(user);

                if (user == null)
                {
                    response.Message = ResponseMessage.NotFound;
                }
                else
                {
                    response.Message = ResponseMessage.Success;
                }
                response.Result = result;
                return response;

            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<GetTicketByIdRequestModel>(null);
                result.AddExceptionLog(ex);
                return result;
            }

        }

        public async Task<GenericBaseResult<SaveTicketRequestModel>> AddTicketDetails(SaveTicketRequestModel Request)
        {
            var response = new GenericBaseResult<SaveTicketRequestModel>(null);
            try
            {
                var randomTicketNo = GenerateTicketNo();
                var ticket = mapper.Map<Ticket>(Request);
                ticket.TicketNo = randomTicketNo;
                ticket.SubmittedDate = DateTime.Now;
                context.Ticket.Add(ticket);
               // await context.SaveChangesAsync();
                response.Message = ResponseMessage.RecordSaved;
                await SendEmailsToAdminsAsync(ticket);
                return response;
            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<SaveTicketRequestModel>(null);
                result.AddExceptionLog(ex);
                return result;

            }
        }
        //email TASK For Single User Working
        public async Task<GenericBaseResult<EmailResponseModel>> SendEmailsToAdminsAsync(Ticket ticket)
        {
            try
            {
                var response = new GenericBaseResult<EmailResponseModel>(null);


                var user = await userManager.FindByIdAsync(ticket.RequestorId);

                string userName = $"{user?.FirstName ?? "Unknown"} {user?.LastName ?? "Unknown"}".Trim();
                var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
                if (adminUsers.Count == 0)
                {
                    response.Message = ResponseMessage.NotFound;
                    return response;
                }
                var adminUser = adminUsers.First();

                string subject = ResponseMessage.TicketRaised;
                string emailHtml = $"A new ticket has been created with the following details:<br><br>" +
                    $"<b>Ticket Number:</b> {ticket.TicketNo}<br>" +
                    $"<b>Ticket Name:</b> {ticket.TicketName}<br>" +
                    $"<b>Description:</b> {ticket.Description}<br><br>" +
                    $"<b>Raised By:</b> {userName}";

                var emailResponse = email.SendEmailToAdmin(adminUser.Email, subject, emailHtml);
                if (emailResponse == ResponseMessage.EmailSentSucces)
                {
                    response.Message = ResponseMessage.EmailSentSucces;
                    //return response;
                }
                else
                {
                    response.Message = ResponseMessage.EmailSentFailure;
                    // return response;

                }

                return response;

            }
            catch (Exception ex)
            {
                var result = new GenericBaseResult<EmailResponseModel>(null);
                result.AddExceptionLog(ex);
                return result;

            }

        }


        //email TASK For Multiple UsersWorking

        //private async Task SendEmailsToAdminsAsync(Ticket ticket)
        //{
        //    try
        //    {

        //        var user = await userManager.FindByIdAsync(ticket.RequestorId);
        //        string userName = $"{user?.FirstName ?? "Unknown"} {user?.LastName ?? "Unknown"}".Trim();
        //        var adminUsers = await userManager.GetUsersInRoleAsync("Admin");
        //        var emailTasks = new List<Task>();
        //        foreach (var adminUser in adminUsers)
        //        {

        //            string subject = "New Ticket Created";
        //            string emailHtml = $"A new ticket has been created with the following details:<br><br>" +
        //                $"<b>Ticket Number:</b> {ticket.TicketNo}<br>" +
        //                $"<b>Ticket Name:</b> {ticket.TicketName}<br>" +
        //                $"<b>Description:</b> {ticket.Description}<br><br>" +
        //                $"<b>Raised By:</b> {userName}";


        //            emailTasks.Add(email.SendEmailToAdminAsync(adminUser.Email, subject, emailHtml));
        //        }


        //        await Task.WhenAll(emailTasks);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        private string GenerateTicketNo()
        {
            var random = new Random();
            int randomNumber = random.Next(100000, 999999);
            return randomNumber.ToString();
        }
    }
}
