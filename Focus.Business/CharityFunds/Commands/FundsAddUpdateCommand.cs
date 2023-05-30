using Focus.Business.BenificiariesNotes.Commands;
using Focus.Business.CharityFunds.Models;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using DocumentFormat.OpenXml.Spreadsheet;
using NPOI.SS.Formula.Functions;

namespace Focus.Business.CharityFunds.Commands
{
    public class FundsAddUpdateCommand : IRequest<Message>
    {
        public FundsLookupModel Funds { get; set; }
        public class Handler : IRequestHandler<FundsAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<FundsAddUpdateCommand> logger)
            {
                Context = context;
                Logger = logger;
            }
            public async Task<Message> Handle(FundsAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Funds.Id == Guid.Empty)
                    {
                        var fund = new Funds
                        {
                           Description = request.Funds.Description,
                           UserId = request.Funds.UserId,
                           Amount = request.Funds.Amount,
                           CharityResouceId = request.Funds.CharityResouceId,
                           Date = DateTime.Now,
                        };

                        await Context.Funds.AddAsync(fund);

                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = fund.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                    else
                    {
                        var fund = await Context.Funds.FindAsync(request.Funds.Id);
                        if (fund == null)
                            throw new NotFoundException("Funds Not Found", "");

                        fund.Description = request.Funds.Description;
                        fund.UserId = request.Funds.UserId;
                        fund.Amount = request.Funds.Amount;
                        fund.CharityResouceId = request.Funds.CharityResouceId;
                        fund.Date = DateTime.Now;

                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = fund.Id,
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
