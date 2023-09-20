using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Focus.Business.Exepenses.Models;

namespace Focus.Business.Exepenses.Queries
{
    public class ExpenseDetailsQuery : IRequest<ExpenseLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<ExpenseDetailsQuery, ExpenseLookupModel>
        {
            private readonly UserManager<ApplicationUser> _userManager;
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ExpenseDetailsQuery> logger, UserManager<ApplicationUser> userManager)
            {
                Context = context;
                _logger = logger;
                _userManager = userManager;
            }
            public async Task<ExpenseLookupModel> Handle(ExpenseDetailsQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    var query = await Context.Expenses.Select(x => new ExpenseLookupModel
                    {
                        Id = x.Id,
                        Date = x.Date.ToString("MM/dd/yyyy"),
                        Description = x.Description,
                        Amount = x.Amount,
                        Code = x.Code,
                        ExpenseCategoryId=x.ExpenseCategoryId
                    }).FirstOrDefaultAsync(x => x.Id == request.Id);


                    if (query == null)
                        throw new NotFoundException("Expense Not Found", "");


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
