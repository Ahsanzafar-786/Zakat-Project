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
        public string BeneficiaryName { get; set; }
        public string BeneficiaryNote { get; set; }
        public string BenificaryCode { get; set; }
        public string NationalId { get; set; }
        public string ContactNo { get; set; }

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
                        var query = await Context.BenificaryNotes.AsNoTracking().Include(x => x.Beneficiaries).Select(x => new BenificaryNoteLookupModel
                        {
                            Id = x.Id,
                            Note = x.Note,
                            BenificaryCode = x.Beneficiaries==null?"":x.Beneficiaries.BeneficiaryId.ToString(),
                            BenificaryName = (x.Beneficiaries.NameAr == null ? x.Beneficiaries.Name : x.Beneficiaries.NameAr),
                            Date = x.Date.ToString("dd/MM/yyyy"),
                            NationalId=x.Beneficiaries.UgamaNo,
                            ContactNo=x.Beneficiaries.PhoneNo,
                            Nationality=x.Beneficiaries.Nationality,
                        }).ToListAsync();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();

                            query = query.Where(x => x.BenificaryName== searchTerm).ToList();
                        }
                        if (!string.IsNullOrEmpty(request.BeneficiaryNote))
                        {
                            query = query.Where(x => x.Note == request.BeneficiaryNote).ToList();
                        }
                        if (!string.IsNullOrEmpty(request.BenificaryCode))
                        {
                            query = query.Where(x => x.BenificaryCode == request.BenificaryCode).ToList();
                        }
                        if (!string.IsNullOrEmpty(request.NationalId))
                        {
                            query = query.Where(x => x.NationalId == request.NationalId).ToList();
                        }
                        if (!string.IsNullOrEmpty(request.ContactNo))
                        {
                            query = query.Where(x => x.ContactNo == request.ContactNo).ToList();
                        }

                        var count =  query.Count();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                        //var queryList = await query.ToListAsync();

                        return new PagedResult<List<BenificaryNoteLookupModel>>
                        {
                            Results = query,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = query.Count / request.PageSize
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
