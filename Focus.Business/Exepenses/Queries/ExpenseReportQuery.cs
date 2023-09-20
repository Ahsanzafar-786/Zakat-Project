using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Focus.Business.Exepenses.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Exepenses.Queries
{
    public class ExpenseReportQuery : IRequest<ExpenseReportModel>
    {
       

        public class Handler : IRequestHandler<ExpenseReportQuery, ExpenseReportModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ExpenseReportQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<ExpenseReportModel> Handle(ExpenseReportQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = Context.Expenses.AsNoTracking().Include(x => x.ExpenseCategory).Select(x => new ExpenseLookupModel
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Amount = x.Amount,
                        Code = x.Code,
                        ExpenseCategoryName = x.ExpenseCategory.ExpenseCategoryName,
                        Date = x.Date.ToString("dd/MM/yyyy"),
                    }).ToList();




                    return new ExpenseReportModel
                    {
                        ExpenseList = query,
                        ExpenseTotal = query.Sum(x => x.Amount),
                        

                    };


                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
