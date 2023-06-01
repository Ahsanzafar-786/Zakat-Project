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

using System.Linq;

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
                    if (request.Payment.Id == Guid.Empty || request.Payment.Id==null)
                    {
                        var paymentCode = Context.Payments.OrderBy(x => x.Id).LastOrDefault();
                        var paymentNo = 1;
                        if (paymentCode != null)
                        {
                            paymentNo = paymentCode.Code + 1;
                        }

                        var pay = Context.Payments.AsNoTracking()
                            .Any(x => x.BenificayId == request.Payment.BenificayId);
                        if (pay)
                        {
                            var isRecord = Context.Beneficiaries.Any(x =>
                                x.Id == request.Payment.BenificayId && x.PaymentTypes.Name == "One Time");
                            if (isRecord)
                            {
                                throw new ObjectAlreadyExistsException("You Can Receive Only One Time Amount");
                            }
                        }


                        var payment = new Payment
                        {
                            BenificayId = request.Payment.BenificayId,
                            Amount = request.Payment.Amount,
                            Month = request.Payment.Month,
                            Code = paymentNo,
                            Year = DateTime.Now.Year.ToString(),
                            Period = DateTime.Now.Year.ToString(),
                            Date = DateTime.Now,
                        };

                        Context.Payments.Add(payment);

                        foreach (var item in request.Payment.SelectedMonth)
                        {
                            var charityTransaction = new CharityTransaction
                            {
                                DoucmentId = payment.Id,
                                CharityTransactionDate = payment.Date,
                                DoucmentDate = DateTime.Now,
                                DoucmentCode = payment.Code,
                                BenificayId = payment.BenificayId,
                                Month = payment.Month,
                                Amount = payment.Amount,
                                Year = payment.Year,
                            };

                            await Context.CharityTransaction.AddAsync(charityTransaction);
                        }
                       

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
                        var paymentDetails =  Context.Payments.AsNoTracking().FirstOrDefault(x => x.Id == request.Payment.Id);
                        if (paymentDetails == null)
                            throw new NotFoundException("Payment Not Found","");

                        //var charityTransaction = await Context.CharityTransaction.FindAsync(paymentDetails.Id);

                        paymentDetails.BenificayId = request.Payment.BenificayId;
                        paymentDetails.Amount = request.Payment.Amount;
                        paymentDetails.Month = request.Payment.Month;
                        paymentDetails.Code = request.Payment.Code; 
                        paymentDetails.Year = DateTime.Now.Year.ToString();
                        paymentDetails.Period = DateTime.Now.Year.ToString();
                        paymentDetails.Date = DateTime.Now;

                        Context.Payments.Update(paymentDetails);

                        //if(charityTransaction != null)
                        //{
                        //    charityTransaction.DoucmentId = paymentDetails.Id;
                        //    charityTransaction.DoucmentCode = paymentDetails.Code;

                        //}

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
