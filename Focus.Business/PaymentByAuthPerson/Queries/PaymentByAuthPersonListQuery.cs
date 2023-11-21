
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
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string Nationality { get; set; }
        public string UqamaNo { get; set; }
        public string Register { get; set; }
        public string Status { get; set; }
        public string SearchTerm { get; set; }
        public string BeneficiaryName { get; set; }
        public int? Code { get; set; }
        public decimal? Amount { get; set; }
        public int? BenificaryCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? Year { get; set; }
        public Guid? ApprovalPersonId { get; set; }
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
                                    Code = x.Code,
                                    Amount = x.Amount,
                                })
                                .OrderByDescending(x => x.Code).ToList();

               
                    
                    var pagesize = 100;
                    request.PageSize = pagesize;

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();


                    return new PagedResult<List<PaymentByAuthorizeLookUpModel>>
                    {
                        Results = query,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = query.Count / request.PageSize
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
