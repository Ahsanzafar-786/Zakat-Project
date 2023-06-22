using Focus.Business.AuthorizPersons.Model;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.AuthorizPersons.Queries
{
    public class GetAuthorizedPersonListQuery : PagedRequest, IRequest<PagedResult<List<AuthorizedPersonsLookupModel>>>
    {
        public bool IsDropDown { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<GetAuthorizedPersonListQuery, PagedResult<List<AuthorizedPersonsLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetAuthorizedPersonListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<AuthorizedPersonsLookupModel>>> Handle(GetAuthorizedPersonListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var query = await Context.AuthorizedPersons.AsNoTracking().Where(x => x.IsActive).Select(x => new AuthorizedPersonsLookupModel
                        {
                            Id = x.Id,
                            Name = x.AuthorizedPersonCode + " - " + x.Name,
                            NameAr = x.AuthorizedPersonCode + " - " + x.NameAr,
                        }).ToListAsync();

                        return new PagedResult<List<AuthorizedPersonsLookupModel>>
                        {
                            Results = query
                        };
                    }
                    else
                    {
                        var query = Context.AuthorizedPersons.AsNoTracking().Select(x => new AuthorizedPersonsLookupModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            NameAr = x.NameAr,
                            AuthorizedPersonCode = x.AuthorizedPersonCode,
                            PhoneNo = x.PhoneNo,
                            Address = x.Address,
                            Nationality = x.Nationality,
                            PassportNo = x.PassportNo,
                            Gender = x.Gender,
                            IqamaNo = x.IqamaNo,
                            IsActive=x.IsActive,
                        }).AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();
                            query = query.Where(x => x.Name.ToLower().Contains(searchTerm)
                                                  || x.AuthorizedPersonCode.ToString().Contains(searchTerm));
                        }

                        var count = await query.CountAsync();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var queryList = await query.ToListAsync();

                        return new PagedResult<List<AuthorizedPersonsLookupModel>>
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
