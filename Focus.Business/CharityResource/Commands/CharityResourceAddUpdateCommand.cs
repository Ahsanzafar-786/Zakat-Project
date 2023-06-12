using Focus.Business.CharityResource.Model;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using Focus.Business.Exceptions;
using System;
using Focus.Domain.Entities;
using System.Linq;
using Focus.Business.AuthorizPersons.Commands;

namespace Focus.Business.CharityResource.Commands
{
    public class CharityResourceAddUpdateCommand : IRequest<Message>
    {
        public CharityResourcesLookupModel charityResources { get; set; }

        public class Handler : IRequestHandler<CharityResourceAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<CharityResourceAddUpdateCommand> logger)
            {
                Context = context;
                Logger = logger;
            }
            public async Task<Message> Handle(CharityResourceAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.charityResources.Id == Guid.Empty)
                    {
                        var charityResource = new CharityResources
                        {
                            ChartiyId = request.charityResources.ChartiyId,
                            Name = request.charityResources.Name,
                            Phone = request.charityResources.Phone,
                            Business = request.charityResources.Business,
                            City = request.charityResources.City,
                            IsActive = request.charityResources.IsActive,
                            ContactPerson = request.charityResources.ContactPerson,
                        };

                        await Context.CharityResources.AddAsync(charityResource);
                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = charityResource.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                    else
                    {
                        var charityResourceDetail = await Context.CharityResources.FindAsync(request.charityResources.Id);
                        if (charityResourceDetail == null)
                            throw new NotFoundException("Authorized Persons Not Found", "");

                        charityResourceDetail.ChartiyId = request.charityResources.ChartiyId;
                        charityResourceDetail.Name = request.charityResources.Name;
                        charityResourceDetail.Phone = request.charityResources.Phone;
                        charityResourceDetail.Business = request.charityResources.Business;
                        charityResourceDetail.IsActive = request.charityResources.IsActive;
                        charityResourceDetail.ContactPerson = request.charityResources.ContactPerson;
                        charityResourceDetail.City = request.charityResources.City;

                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = charityResourceDetail.Id,
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
