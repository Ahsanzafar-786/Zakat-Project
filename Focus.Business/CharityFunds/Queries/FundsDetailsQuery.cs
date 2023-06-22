using Focus.Business.BenificiariesNotes.Queries;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Focus.Business.CharityFunds.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.CharityFunds.Queries
{
    public class FundsDetailsQuery : IRequest<FundsLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<FundsDetailsQuery, FundsLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<FundsDetailsQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<FundsLookupModel> Handle(FundsDetailsQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    var query = await Context.Funds.Select(x => new FundsLookupModel
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Amount = x.Amount,
                        Code = x.Code,
                        CharityResouceId = x.CharityResouceId,
                        TypeOfTransaction=x.TypeOfTransaction
                    }).FirstOrDefaultAsync(x => x.Id == request.Id);

                    if (query == null)
                        throw new NotFoundException("Funds Not Found", "");


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
