using Focus.Business.AuthorizPersons.Model;
using Focus.Business.AuthorizPersons.Queries;
using Focus.Business.BenificiariesNotes.Model;
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

namespace Focus.Business.BenificiariesNotes.Queries
{
    public class GetBenificaryNoteListQuery : PagedRequest, IRequest<PagedResult<List<BenificaryNoteLookupModel>>>
    {
        public bool IsDropDown { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<GetBenificaryNoteListQuery, PagedResult<List<BenificaryNoteLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetBenificaryNoteListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<BenificaryNoteLookupModel>>> Handle(GetBenificaryNoteListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var query = await Context.BenificaryNotes.AsNoTracking().Select(x => new BenificaryNoteLookupModel
                        {
                            Id = x.Id,
                            Note = x.Note,
                        }).ToListAsync();

                        return new PagedResult<List<BenificaryNoteLookupModel>>
                        {
                            Results = query
                        };
                    }
                    else
                    {
                        var query = Context.BenificaryNotes.AsNoTracking().Include(x => x.Beneficiaries).Select(x => new BenificaryNoteLookupModel
                        {
                            Id = x.Id,
                            Note = x.Note,
                            BenificaryName = x.Beneficiaries.BeneficiaryId.ToString() + " " + x.Beneficiaries.Name,
                            Date = x.Date.ToString("dd/MM/yyyy"),
                        }).AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();
                            query = query.Where(x => x.Note.ToLower().Contains(searchTerm)
                                                  || x.BenificaryName.ToString().Contains(searchTerm));
                        }

                        var count = await query.CountAsync();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var queryList = await query.ToListAsync();

                        return new PagedResult<List<BenificaryNoteLookupModel>>
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
