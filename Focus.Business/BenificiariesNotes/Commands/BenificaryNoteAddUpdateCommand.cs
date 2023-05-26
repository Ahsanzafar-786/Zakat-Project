using Focus.Business.BenificiariesNotes.Model;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using Focus.Business.Exceptions;
using System;
using Focus.Domain.Entities;

namespace Focus.Business.BenificiariesNotes.Commands
{
    public class BenificaryNoteAddUpdateCommand : IRequest<Message>
    {
        public BenificaryNoteLookupModel benificaryNote { get; set; }

        public class Handler : IRequestHandler<BenificaryNoteAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<BenificaryNoteAddUpdateCommand> logger)
            {
                Context = context;
                Logger = logger;
            }
            public async Task<Message> Handle(BenificaryNoteAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.benificaryNote.Id == Guid.Empty)
                    {
                        var benificary = new BenificaryNote
                        {
                            BenificaryId = request.benificaryNote.BenificaryId,
                            Note = request.benificaryNote.Note,
                            Date = DateTime.Now,
                        };

                        await Context.BenificaryNotes.AddAsync(benificary);

                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = benificary.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                    else
                    {
                        var benificary = await Context.BenificaryNotes.FindAsync(request.benificaryNote.Id);
                        if (benificary == null)
                            throw new NotFoundException("Benificary Note Not Found", "");

                        benificary.Note = request.benificaryNote.Note;
                        benificary.BenificaryId = request.benificaryNote.BenificaryId;
                        benificary.Date = DateTime.Now;

                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = benificary.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    return new Message
                    {
                        Id = Guid.Empty,
                        IsSuccess = false,
                        IsAddUpdate = exception.Message
                    };
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    return new Message
                    {
                        Id = Guid.Empty,
                        IsSuccess = false,
                        IsAddUpdate = exception.Message
                    };
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    return new Message
                    {
                        Id = Guid.Empty,
                        IsSuccess = false,
                        IsAddUpdate = exception.Message
                    };
                }
            }
        }
    }
}
