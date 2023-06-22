using Focus.Business.ApprovalsPerson.Model;
using Focus.Business.AuthorizPersons.Commands;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace Focus.Business.ApprovalsPerson.Command
{
    public class ApprovalSystemAddUpdateCommand : IRequest<Message>
    {
        public ApprovalPersonLookupModel ApprovalsPerson { get; set; }

        public class Handler : IRequestHandler<ApprovalSystemAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<ApprovalSystemAddUpdateCommand> logger)
            {
                Context = context;
                Logger = logger;
            }
            public async Task<Message> Handle(ApprovalSystemAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.ApprovalsPerson.Id == Guid.Empty)
                    {
                        var approve = new ApprovalPerson
                        {
                            AprovalPersonId = request.ApprovalsPerson.AprovalPersonId,
                            Name = request.ApprovalsPerson.Name,
                            NameAr = request.ApprovalsPerson.NameAr,
                            PhoneNo = request.ApprovalsPerson.PhoneNo,
                            Email = request.ApprovalsPerson.Email,
                            IsActive = request.ApprovalsPerson.IsActive
                        };

                        await Context.ApprovalPersons.AddAsync(approve);
                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = approve.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                    else
                    {
                        var approvalPersonDetail = await Context.ApprovalPersons.FindAsync(request.ApprovalsPerson.Id);
                        if (approvalPersonDetail == null)
                            throw new NotFoundException("Authorized Persons Not Found", "");

                        approvalPersonDetail.AprovalPersonId = request.ApprovalsPerson.AprovalPersonId;
                        approvalPersonDetail.Name = request.ApprovalsPerson.Name;
                        approvalPersonDetail.NameAr = request.ApprovalsPerson.NameAr;
                        approvalPersonDetail.PhoneNo = request.ApprovalsPerson.PhoneNo;
                        approvalPersonDetail.Email = request.ApprovalsPerson.Email;
                        approvalPersonDetail.IsActive = request.ApprovalsPerson.IsActive;


                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = approvalPersonDetail.Id,
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
