using Focus.Business.AuthorizPersons.Model;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.AuthorizPersons.Queries
{
    public class GetAuthorizedPersonDetailsQuery : IRequest<AuthorizedPersonsLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetAuthorizedPersonDetailsQuery, AuthorizedPersonsLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetAuthorizedPersonDetailsQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<AuthorizedPersonsLookupModel> Handle(GetAuthorizedPersonDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await Context.AuthorizedPersons.Select(x => new AuthorizedPersonsLookupModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        NameAr = x.NameAr,
                        AuthorizedPersonCode= x.AuthorizedPersonCode,
                        PhoneNo = x.PhoneNo,
                        Address = x.Address,
                        Nationality = x.Nationality,
                        PassportNo = x.PassportNo,
                        Gender= x.Gender,
                        IqamaNo = x.IqamaNo,
                        IsActive = x.IsActive,
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
