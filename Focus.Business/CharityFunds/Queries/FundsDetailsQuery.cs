using Focus.Business.BenificiariesNotes.Queries;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Focus.Business.CharityFunds.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;

namespace Focus.Business.CharityFunds.Queries
{
    public class FundsDetailsQuery : IRequest<FundsLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<FundsDetailsQuery, FundsLookupModel>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<FundsDetailsQuery> logger, UserManager<ApplicationUser> userManager)
            {
                Context = context;
                _logger = logger; 
                _userManager = userManager;
            }
            public async Task<FundsLookupModel> Handle(FundsDetailsQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    var usersList = _userManager.Users.ToList();
                    var query = await Context.Funds.Select(x => new FundsLookupModel 
                    {
                        Id = x.Id,
                        Date=x.Date.ToString("MM/dd/yyyy"),
                        Description = x.Description,
                        Amount = x.Amount,
                        Code = x.Code,
                        CharityResouceId = x.CharityResouceId,
                        UserId = x.UserId,
                        TypeOfTransaction=x.TypeOfTransaction,
                        
                    }).FirstOrDefaultAsync(x => x.Id == request.Id);

                    query.Cashier =  usersList.FirstOrDefault(y => y.Id == query.UserId).UserName;

                    if (query == null)
                        throw new NotFoundException("Funds Not Found", "");


                    return query;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Something Went Wrong.");
                }
            }
        }
    }
}
