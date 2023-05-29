using Focus.Business.BenificiariesNotes.Model;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Focus.Business.Exceptions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.BenificiariesNotes.Queries
{
    public class GetBenificaryNoteDetailsQuery : IRequest<BenificaryNoteLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetBenificaryNoteDetailsQuery, BenificaryNoteLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetBenificaryNoteDetailsQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<BenificaryNoteLookupModel> Handle(GetBenificaryNoteDetailsQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    var query = await Context.BenificaryNotes.Select(x => new BenificaryNoteLookupModel
                    {
                        Id = x.Id,
                        Note = x.Note,
                        BenificaryId = x.BenificaryId,
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
