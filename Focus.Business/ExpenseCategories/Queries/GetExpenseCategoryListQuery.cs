using Focus.Business.ExpenseCategories.Model;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ExpenseCategories.Queries
{
    public class GetExpenseCategoryListQuery : PagedRequest, IRequest<PagedResult<List<ExpenseCategoryLookupModel>>>
    {
        public bool IsDropDown { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<GetExpenseCategoryListQuery, PagedResult<List<ExpenseCategoryLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetExpenseCategoryListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<ExpenseCategoryLookupModel>>> Handle(GetExpenseCategoryListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var query = await Context.ExpenseCategories.AsNoTracking().Where(x => x.IsActive).Select(x => new ExpenseCategoryLookupModel
                        {
                            Id = x.Id,
                            CategoryName = x.Code + " - " + x.ExpenseCategoryName,
                        }).ToListAsync();

                        return new PagedResult<List<ExpenseCategoryLookupModel>>
                        {
                            Results = query
                        };
                    }
                    else
                    {
                        var query = Context.ExpenseCategories.AsNoTracking().Select(x => new ExpenseCategoryLookupModel
                        {
                            Id = x.Id,
                            CategoryName = x.ExpenseCategoryName,
                            Description = x.Description,
                            ExpenseCategoryCode = x.Code,
                            IsActive = x.IsActive,
                        }).AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();
                            query = query.Where(x => x.CategoryName.ToLower().Contains(searchTerm)
                                                  || x.ExpenseCategoryCode.ToString().Contains(searchTerm));
                        }

                        var count = await query.CountAsync();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var queryList = await query.ToListAsync();

                        return new PagedResult<List<ExpenseCategoryLookupModel>>
                        {
                            Results = queryList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = queryList.Count / request.PageSize
                        };
                    }
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
