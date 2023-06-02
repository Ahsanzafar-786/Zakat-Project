using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Payments.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Payments.Queries
{
    public class PaymentDetailsQuery : IRequest<PaymentLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<PaymentDetailsQuery, PaymentLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentDetailsQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PaymentLookupModel> Handle(PaymentDetailsQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    var query = await Context.Payments.Select(x => new PaymentLookupModel
                    {
                        Id = x.Id, 
                        BenificayId = x.BenificayId,
                        Amount = x.Amount,
                        Month = x.Month,
                        PaymentCode = x.PaymentCode,
                        Year = x.Year,
                        Period = x.Period,
                    }).FirstOrDefaultAsync(x => x.Id == request.Id);

                    if (query == null)
                        throw new NotFoundException("Benificary Note Not Found", "");


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
