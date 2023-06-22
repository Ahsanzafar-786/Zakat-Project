using Focus.Business.BenificiariesNotes.Model;
using Focus.Business.BenificiariesNotes.Queries;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.BenificiariesNotes.Commands
{
    public class DeleteBenificaryNoteCommand : IRequest<Message>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteBenificaryNoteCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<Message> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<Message> Handle(DeleteBenificaryNoteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await Context.BenificaryNotes.FindAsync(request.Id);

                    if (query == null)
                    {
                        return new Message
                        {
                            IsSuccess = false,
                            IsAddUpdate = "Beneficary Note not found",
                        };
                    }

                    Context.BenificaryNotes.Remove(query);

                    await Context.SaveChangesAsync();

                    return new Message
                    {
                        IsSuccess = true,
                        IsAddUpdate = "Date has been deleted successfully",
                    }; ;
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
