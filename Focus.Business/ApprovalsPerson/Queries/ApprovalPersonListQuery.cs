using Focus.Business.ApprovalsPerson.Model;
using Focus.Business.AuthorizPersons.Model;
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
using DocumentFormat.OpenXml.Wordprocessing;

namespace Focus.Business.ApprovalsPerson.Queries
{
    public class ApprovalPersonListQuery :  PagedRequest, IRequest<PagedResult<List<ApprovalPersonLookupModel>>>
    {
        public bool IsDropDown { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<ApprovalPersonListQuery, PagedResult<List<ApprovalPersonLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<ApprovalPersonListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<ApprovalPersonLookupModel>>> Handle(ApprovalPersonListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var query = await Context.ApprovalPersons.AsNoTracking().Where(x => x.IsActive).Select(x => new ApprovalPersonLookupModel
                        {
                            Id = x.Id,
                            Name = x.AprovalPersonId + " - " + x.Name,
                            NameAr = x.AprovalPersonId + " - " + x.NameAr
                        }).ToListAsync();

                        return new PagedResult<List<ApprovalPersonLookupModel>>
                        {
                            Results = query
                        };
                    }
                    else
                    {
                        var query = Context.ApprovalPersons.AsNoTracking().Select(x => new ApprovalPersonLookupModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            NameAr = x.NameAr,
                            AprovalPersonId = x.AprovalPersonId,
                            Email = x.Email,
                            IsActive = x.IsActive,
                            PhoneNo = x.PhoneNo,
                        }).AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();
                            query = query.Where(x => x.Name.ToLower().Contains(searchTerm)
                                                  || x.AprovalPersonId.ToString().Contains(searchTerm)
                                                  || x.NameAr.ToLower().Contains(searchTerm)
                                                  );
                        }

                        var count = await query.CountAsync();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var queryList = await query.ToListAsync();

                        return new PagedResult<List<ApprovalPersonLookupModel>>
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
