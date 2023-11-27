
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Focus.Business.PaymentByAuthPerson.Queries
{
    public class PaymentByAuthPersonListQuery : PagedRequest, IRequest<PagedResult<List<PaymentByAuthorizeLookUpModel>>>
    {
        public string BeneficiaryName { get; set; }
        public string Code { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? Year { get; set; }
        public Guid? AuthorizationPersonId { get; set; }
        public class Handler : IRequestHandler<PaymentByAuthPersonListQuery, PagedResult<List<PaymentByAuthorizeLookUpModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentByAuthPersonListQuery> logger)
            {
                Context = context;
                _logger = logger;

            }
            public async Task<PagedResult<List<PaymentByAuthorizeLookUpModel>>> Handle(PaymentByAuthPersonListQuery request, CancellationToken cancellationToken)
            {
                try
                {
               

              

                    var query = Context.PaymentAuthorizePersons.AsNoTracking()
                                .Include(x => x.AuthorizedPerson)

                                .Select(x => new PaymentByAuthorizeLookUpModel
                                {
                                    Id = x.Id,
                                    DateTime = x.Date,
                                    AuthorizationPersonId = x.AuthorizePersonId,
                                    AuthorizePersonNameAr = x.AuthorizedPerson.Name,
                                    AuthorizedPersonCode = x.AuthorizedPerson.AuthorizedPersonCode,
                                    Code = x.Code,
                                    Amount = x.Amount,
                                })
                                .OrderByDescending(x => x.Code).AsQueryable();
                    if (request.Amount != null && request.Amount > 0)
                    {
                        query = query.Where(x => x.Amount == request.Amount);
                    }
                    if (request.Code != null )
                    {
                        query = query.Where(x => x.Code == request.Code);
                    }
                    if (request.AuthorizationPersonId != null)
                    {
                        query = query.Where(x => x.AuthorizationPersonId == request.AuthorizationPersonId);
                    }
                    if (request.FromDate.HasValue && request.ToDate.HasValue)
                    {
                        query = query.Where(x => x.DateTime.Value.Date >= request.FromDate.Value.Date && x.DateTime.Value.Date <= request.ToDate.Value.Date);
                    }
                    if (request.Month != null)
                    {
                        query = query.Where(x => x.DateTime.Value.Month == request.Month.Value.Month && x.DateTime.Value.Year == request.Month.Value.Year);
                    }
                    if (request.Year != null)
                    {
                        query = query.Where(x => x.DateTime.Value.Year == request.Year.Value.Year);
                    }



                    var pagesize = 100;
                    request.PageSize = pagesize;

                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var count = query.Count();

                    return new PagedResult<List<PaymentByAuthorizeLookUpModel>>
                    {
                        Results = query.ToList(),
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = query.ToList().Count / request.PageSize
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
