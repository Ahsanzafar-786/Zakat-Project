using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Payments.Models;
using Focus.Domain.Entities;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using NPOI.SS.Formula.Functions;

namespace Focus.Business.Payments.Commands
{
    public class PaymentsAddUpdateCommand : IRequest<Message>
    {
        public PaymentLookupModel Payment { get; set; }
        public class Handler : IRequestHandler<PaymentsAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentsAddUpdateCommand> logger)
            {
                Context = context;
                Logger = logger;
            }
            public async Task<Message> Handle(PaymentsAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Payment.Id == Guid.Empty)
                    {
                        var benficary = await Context.Beneficiaries.FirstOrDefaultAsync(x => x.Id == request.Payment.BenificayId);

                        var payment = new Payment
                        {
                            BenificayId = request.Payment.BenificayId,
                            Amount = request.Payment.Amount,
                            Month = request.Payment.Month,
                            Year = request.Payment.Year,
                            Period = request.Payment.Period,
                        };

                        Context.Payments.Add(payment);
                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = Guid.Empty,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                    else
                    {
                        var paymentDetails = await Context.Payments.FirstOrDefaultAsync(x => x.Id != request.Payment.Id);
                        if (paymentDetails == null)
                            throw new NotFoundException("Payment Not Found","");

                        paymentDetails.BenificayId = request.Payment.BenificayId;
                        paymentDetails.Amount = request.Payment.Amount;
                        paymentDetails.Month = request.Payment.Month;
                        paymentDetails.Year = request.Payment.Year;
                        paymentDetails.Period = request.Payment.Period;

                        await Context.SaveChangesAsync();
                        return new Message
                        {
                            Id = Guid.Empty,
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
