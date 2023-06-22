using Focus.Business.AuthorizPersons.Model;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.AuthorizPersons.Commands
{
    public class AuthorizedPersonsAddUpdateCommand : IRequest<Message>
    {
        public AuthorizedPersonsLookupModel authorziedPersons { get; set; }

        public class Handler : IRequestHandler<AuthorizedPersonsAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<AuthorizedPersonsAddUpdateCommand> logger)
            {
                Context = context;
                Logger = logger;
            }
            public async Task<Message> Handle(AuthorizedPersonsAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.authorziedPersons.Id == Guid.Empty)
                    {
                        var authorize = Context.AuthorizedPersons.OrderBy(x => x.Id).LastOrDefault();
                       

                        var auth = new AuthorizedPerson
                        {
                            AuthorizedPersonCode = request.authorziedPersons.AuthorizedPersonCode,
                            Name = request.authorziedPersons.Name,
                            NameAr = request.authorziedPersons.NameAr,
                            PhoneNo = request.authorziedPersons.PhoneNo,
                            Address = request.authorziedPersons.Address,
                            Nationality = request.authorziedPersons.Nationality,
                            Gender = request.authorziedPersons.Gender,
                            IqamaNo = request.authorziedPersons.IqamaNo,
                            PassportNo = request.authorziedPersons.PassportNo,
                            IsActive = request.authorziedPersons.IsActive,
                        };

                        await Context.AuthorizedPersons.AddAsync(auth);
                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = auth.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                    else
                    {
                        var authorziedPersonDetail = await Context.AuthorizedPersons.FindAsync(request.authorziedPersons.Id);
                        if (authorziedPersonDetail == null)
                            throw new NotFoundException("Authorized Persons Not Found", "");

                        authorziedPersonDetail.AuthorizedPersonCode = request.authorziedPersons.AuthorizedPersonCode;
                        authorziedPersonDetail.Name = request.authorziedPersons.Name;
                        authorziedPersonDetail.NameAr = request.authorziedPersons.NameAr;
                        authorziedPersonDetail.PhoneNo = request.authorziedPersons.PhoneNo;
                        authorziedPersonDetail.Address = request.authorziedPersons.Address;
                        authorziedPersonDetail.Nationality = request.authorziedPersons.Nationality;
                        authorziedPersonDetail.Gender = request.authorziedPersons.Gender;
                        authorziedPersonDetail.IqamaNo = request.authorziedPersons.IqamaNo;
                        authorziedPersonDetail.PassportNo = request.authorziedPersons.PassportNo;
                        authorziedPersonDetail.IsActive = request.authorziedPersons.IsActive;

                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = authorziedPersonDetail.Id,
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
