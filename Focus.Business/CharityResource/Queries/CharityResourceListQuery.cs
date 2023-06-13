using Focus.Business.AuthorizPersons.Model;
using Focus.Business.AuthorizPersons.Queries;
using Focus.Business.CharityResource.Model;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Focus.Business.CharityResource.Queries
{
    public class CharityResourceListQuery : PagedRequest, IRequest<PagedResult<List<CharityResourcesLookupModel>>>
    {
        public bool IsDropDown { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<CharityResourceListQuery, PagedResult<List<CharityResourcesLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<CharityResourceListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<CharityResourcesLookupModel>>> Handle(CharityResourceListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var query = await Context.CharityResources.AsNoTracking().Select(x => new CharityResourcesLookupModel
                        {
                            Id = x.Id,
                            Name = x.ChartiyId + " - " + x.Name,
                        }).ToListAsync();

                        return new PagedResult<List<CharityResourcesLookupModel>>
                        {
                            Results = query
                        };
                    }
                    else
                    {
                        var query = Context.CharityResources.AsNoTracking().Select(x => new CharityResourcesLookupModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            IsActive = x.IsActive,
                            Business = x.Business,
                            ContactPerson = x.ContactPerson,
                            ChartiyId= x.ChartiyId,
                        }).AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();
                            query = query.Where(x => x.Name.ToLower().Contains(searchTerm)
                                                  || x.ChartiyId.ToString().Contains(searchTerm)
                                                  || x.ContactPerson.ToString().ToLower().Contains(searchTerm)
                                                  );
                        }

                        var count = await query.CountAsync();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var queryList = await query.ToListAsync();

                        return new PagedResult<List<CharityResourcesLookupModel>>
                        {
                            Results = queryList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = queryList.Count / request.PageSize
                        };
                    }
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
