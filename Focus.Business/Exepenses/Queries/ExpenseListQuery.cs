
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Focus.Business.Exepenses.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Focus.Business.Exepenses.Queries
{
    public class ExpenseListQuery : PagedRequest, IRequest<PagedResult<List<ExpenseLookupModel>>>
    {
        public bool IsDropDown { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<ExpenseListQuery, PagedResult<List<ExpenseLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ExpenseListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<ExpenseLookupModel>>> Handle(ExpenseListQuery request, CancellationToken cancellationToken)
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
                    }).AsQueryable();

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        query = query.Where(x => x.ExpenseCategoryName.ToLower().Contains(searchTerm)
                                              || x.Amount.ToString().Contains(searchTerm));
                    }

                    var count = await query.CountAsync();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var queryList = await query.ToListAsync();

                    return new PagedResult<List<ExpenseLookupModel>>
                    {
                        Results = queryList,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = queryList.Count / request.PageSize
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
