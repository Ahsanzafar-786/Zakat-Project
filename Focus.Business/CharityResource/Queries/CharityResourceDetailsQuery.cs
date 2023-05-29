using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Focus.Business.AuthorizPersons.Model;
using Focus.Business.AuthorizPersons.Queries;
using Focus.Business.CharityResource.Model;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.CharityResource.Queries
{
    public class CharityResourceDetailsQuery  : IRequest<CharityResourcesLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<CharityResourceDetailsQuery, CharityResourcesLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<CharityResourceDetailsQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<CharityResourcesLookupModel> Handle(CharityResourceDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await Context.CharityResources.Select(x => new CharityResourcesLookupModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ChartiyId = x.ChartiyId,
                        Phone = x.Phone,
                        IsActive = x.IsActive,
                        City = x.City,
                        Business = x.Business,
                        ContactPerson = x.ContactPerson,
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
