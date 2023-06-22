using Focus.Business.ApprovalsPerson.Model;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ApprovalsPerson.Queries
{
    public class ApprovalPersonDetailsQuery : IRequest<ApprovalPersonLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<ApprovalPersonDetailsQuery, ApprovalPersonLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ApprovalPersonDetailsQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<ApprovalPersonLookupModel> Handle(ApprovalPersonDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await Context.ApprovalPersons.Select(x => new ApprovalPersonLookupModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        AprovalPersonId = x.AprovalPersonId,
                        PhoneNo = x.PhoneNo,
                        Email = x.Email,
                        NameAr = x.NameAr,
                        IsActive = x.IsActive
                    }).FirstOrDefaultAsync(x => x.Id == request.Id);

                    if (query == null)
                        throw new NotFoundException("Benificary Not Found", "");


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
