using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Attachments.Commands
{
    public class AddAttachmentCommand : IRequest<Guid>
    {
        public List<AttachmentLookUpModel> Attachment { get; set; }

        public class Handler : IRequestHandler<AddAttachmentCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddAttachmentCommand> logger)
            {
                Context = context;
                Logger = logger;
            }


            public async Task<Guid> Handle(AddAttachmentCommand request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.Attachment.Count > 0)
                    {
                        var documentId = request.Attachment.FirstOrDefault()?.DocumentId;
                        var attachments = Context.Attachments
                            .AsNoTracking()
                            .Where(x => x.DocumentId == documentId)
                            .AsQueryable();

                        if (attachments.Any())
                        {
                            Context.Attachments.RemoveRange(attachments);
                        }

                        foreach (var item in request.Attachment)
                        {
                            var attachment = new Attachment()
                            {
                                Date = item.Date,
                                DocumentId = item.DocumentId,
                                Description = item.Description,
                                FileName = item.FileName,
                                Path = item.Path
                            };
                            //Add Department to database
                            await Context.Attachments.AddAsync(attachment, cancellationToken);

                        }
                    }


                    await Context.SaveChangesAsync(cancellationToken);
                    return Guid.NewGuid();

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
