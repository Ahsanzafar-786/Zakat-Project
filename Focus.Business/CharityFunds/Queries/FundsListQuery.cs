using Focus.Business.BenificiariesNotes.Model;
using Focus.Business.BenificiariesNotes.Queries;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Focus.Business.CharityFunds.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Focus.Business.CharityFunds.Queries
{
    public class FundsListQuery : PagedRequest, IRequest<PagedResult<List<FundsLookupModel>>>
    {
        public bool IsDropDown { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<FundsListQuery, PagedResult<List<FundsLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<FundsListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<FundsLookupModel>>> Handle(FundsListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = Context.Funds.AsNoTracking().Include(x => x.CharityResources).Select(x => new FundsLookupModel
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Amount = x.Amount,
                        Code = x.Code,
                        CharityResouceName = x.CharityResources.Name,
                        Date = x.Date.ToString("dd/MM/yyyy"),
                        TypeOfTransaction = x.TypeOfTransaction,
                    }).AsQueryable();

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        query = query.Where(x => x.CharityResouceName.ToLower().Contains(searchTerm)
                                              || x.Amount.ToString().Contains(searchTerm));
                    }

                    var count = await query.CountAsync();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var queryList = await query.ToListAsync();

                    return new PagedResult<List<FundsLookupModel>>
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
