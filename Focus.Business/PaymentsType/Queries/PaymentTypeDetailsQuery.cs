using Focus.Business.BenificiariesNotes.Model;
using Focus.Business.BenificiariesNotes.Queries;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.PaymentsType.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.PaymentsType.Queries
{
    public class PaymentTypeDetailsQuery : IRequest<PaymentTypeLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<PaymentTypeDetailsQuery, PaymentTypeLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentTypeDetailsQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PaymentTypeLookupModel> Handle(PaymentTypeDetailsQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    var query = await Context.PaymentTypes.Select(x => new PaymentTypeLookupModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        NameAr = x.NameAr,
                        Description = x.Description,
                        Code = x.Code,
                        IsActive = x.IsActive,
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
