using Focus.Business.BenificiariesNotes.Model;
using Focus.Business.BenificiariesNotes.Queries;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Payments.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Focus.Business.Payments.Queries
{
    public class PaymentListQuery : PagedRequest, IRequest<PagedResult<List<PaymentLookupModel>>>
    {
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<PaymentListQuery, PagedResult<List<PaymentLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<PaymentLookupModel>>> Handle(PaymentListQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var query = Context.Payments.AsNoTracking().Include(x => x.Beneficiaries).Select(x => new PaymentLookupModel
                    {
                        Id = x.Id,
                        BenificayId = x.BenificayId,
                        Amount = x.Amount,
                        Month = x.Month,
                        Year = x.Year,
                        Period = x.Period,
                        Code =x.Code,
                        BenificaryName = x.Beneficiaries.Name,
                    }).AsQueryable();

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        query = query.Where(x => x.Amount.ToString().Contains(searchTerm)
                                              || x.BenificaryName.ToString().Contains(searchTerm));
                    }

                    var count = await query.CountAsync();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var queryList = await query.ToListAsync();

                    return new PagedResult<List<PaymentLookupModel>>
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
