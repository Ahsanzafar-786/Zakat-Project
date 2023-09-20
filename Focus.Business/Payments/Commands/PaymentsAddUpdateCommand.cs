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
using System.Collections.Generic;
using NPOI.POIFS.Properties;

namespace Focus.Business.Payments.Commands
{
    public class PaymentsAddUpdateCommand : IRequest<Message>
    {
        public PaymentLookupModel Payment { get; set; }
        public Guid? PaymentId { get; set; }
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

                        if(request.Payment.SelectedMonth.Count == 0)
                        {

                            var payment = new Payment
                            {
                                BenificayId = request.Payment.BenificayId,
                                Amount = request.Payment.Amount,
                                Month = request.Payment.Month,
                                Code = request.Payment.Code,
                                Note = request.Payment.Note,
                                PaymentCode = request.Payment.PaymentCode,
                                Year = DateTime.Now.Year.ToString(),
                                Period = DateTime.Now.Year.ToString(),
                                Date = DateTime.Now,
                                UserId = request.Payment.UserId,
                                TotalAmount=request.Payment.Amount
                            };

                            Context.Payments.Add(payment);

                            var selectedMonth = new List<SelectedMonth>();
                            
                                selectedMonth.Add(new SelectedMonth
                                {
                                    PaymentId = payment.Id,
                                    SelectMonth = request.Payment.Month,
                                    Amount=request.Payment.Amount
                                    
                                    
                                });
                            

                            await Context.SelectedMonths.AddRangeAsync(selectedMonth);

                            var beneficary = Context.Beneficiaries.AsNoTracking()
                                .FirstOrDefault(x => x.Id == request.Payment.BenificayId);
                            if (beneficary != null)
                            {
                                beneficary.CurrentPaymentMonth = request.Payment.Month;
                                beneficary.LastPaymentAmount= request.Payment.Amount;
                                Context.Beneficiaries.Update(beneficary);

                            }

                            request.PaymentId= payment.Id;

                            var charityTransaction = new CharityTransaction
                            {
                                DoucmentId = payment.Id,
                                CharityTransactionDate = payment.Date,
                                DoucmentDate = DateTime.Now,
                                DoucmentCode = payment.PaymentCode,
                                BenificayId = payment.BenificayId,
                                Month = payment.Date,
                                Amount = payment.Amount,
                                Year = payment.Year,
                            };

                            await Context.CharityTransaction.AddAsync(charityTransaction);
                        }
                        else
                        {
                            var payment = new Payment
                            {
                                BenificayId = request.Payment.BenificayId,
                                Amount = request.Payment.Amount,
                                Month = request.Payment.Month,
                                Note = request.Payment.Note,
                                Code = request.Payment.Code,
                                PaymentCode = request.Payment.PaymentCode,
                                Year = DateTime.Now.Year.ToString(),
                                Period = DateTime.Now.Year.ToString(),
                                UserId = request.Payment.UserId,
                                Date = DateTime.Now,
                                TotalAmount = request.Payment.Amount * request.Payment.SelectedMonth.Count
                            };

                            Context.Payments.Add(payment);
                            request.PaymentId = payment.Id;

                            var selectedMonth = new List<SelectedMonth>();
                            foreach (var item in request.Payment.SelectedMonth)
                            {
                                selectedMonth.Add(new SelectedMonth
                                {
                                    PaymentId = payment.Id,
                                    SelectMonth = item.SelectedMonth,
                                    Amount=request.Payment.Amount,
                                });
                            }

                            await Context.SelectedMonths.AddRangeAsync(selectedMonth);
                            if (request.Payment.SelectedMonth.Count > 0)
                            {
                                var beneficary = Context.Beneficiaries.AsNoTracking()
                                    .FirstOrDefault(x => x.Id == request.Payment.BenificayId);
                                if (beneficary != null)
                                {
                                    beneficary.CurrentPaymentMonth =
                                        request.Payment.SelectedMonth.LastOrDefault()?.SelectedMonth;
                                        beneficary.LastPaymentAmount =
                                        request.Payment.Amount * request.Payment.SelectedMonth.Count;

                                     Context.Beneficiaries.Update(beneficary);

                                }
                            }
                            else
                            {
                                var beneficary = Context.Beneficiaries.AsNoTracking()
                                    .FirstOrDefault(x => x.Id == request.Payment.BenificayId);
                                if (beneficary != null)
                                {
                                    beneficary.CurrentPaymentMonth = request.Payment.Month;
                                    Context.Beneficiaries.Update(beneficary);

                                }

                            }

                            foreach (var item in request.Payment.SelectedMonth)
                            {
                                var charityTransaction = new CharityTransaction
                                {
                                    DoucmentId = payment.Id,
                                    CharityTransactionDate = payment.Date,
                                    DoucmentDate = DateTime.Now,
                                    DoucmentCode = payment.PaymentCode,
                                    BenificayId = payment.BenificayId,
                                    Month = item.SelectedMonth,
                                    Amount = payment.Amount,
                                    Year = payment.Year,
                                };

                                await Context.CharityTransaction.AddAsync(charityTransaction);
                            }
                        }
                        
                       

                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = Guid.Empty,
                            IsSuccess = true,
                            PaymentId = request.PaymentId==null?Guid.Empty: request.PaymentId.Value,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                    else
                    {
                        var paymentDetails =  Context.Payments.AsNoTracking().Include(x=> x.SelectedMonth).FirstOrDefault(x => x.Id == request.Payment.Id);
                        if (paymentDetails == null)
                            throw new NotFoundException("Payment Not Found","");

                        var charityTransactions =  Context.CharityTransaction.Where(x=> x.DoucmentId== request.Payment.Id).ToList();
                        Context.CharityTransaction.RemoveRange(charityTransactions);

                        paymentDetails.BenificayId = request.Payment.BenificayId;
                        paymentDetails.Amount = request.Payment.Amount;
                        paymentDetails.Month = request.Payment.Month;
                        paymentDetails.PaymentCode = request.Payment.PaymentCode; 
                        paymentDetails.Year = DateTime.Now.Year.ToString();
                        paymentDetails.Period = DateTime.Now.Year.ToString();
                        paymentDetails.Date = DateTime.Now;
                        paymentDetails.Note = request.Payment.Note;
                        paymentDetails.UserId = request.Payment.UserId;
                        paymentDetails.Code = request.Payment.Code;

                        Context.SelectedMonths.RemoveRange(paymentDetails.SelectedMonth);
                        var selectedMonth = new List<SelectedMonth>();
                        foreach (var item in request.Payment.SelectedMonth)
                        {
                            selectedMonth.Add(new SelectedMonth
                            {
                                PaymentId = request.Payment.Id,
                                SelectMonth = item.SelectedMonth,
                                Amount = request.Payment.Amount,
                            });
                        }

                        Context.SelectedMonths.AddRange(selectedMonth);

                        Context.Payments.Update(paymentDetails);

                        foreach (var item in request.Payment.SelectedMonth)
                        {
                            var charityTransaction = new CharityTransaction
                            {
                                DoucmentId = request.Payment.Id,
                                CharityTransactionDate = request.Payment.Date,
                                DoucmentDate = DateTime.Now,
                                DoucmentCode = request.Payment.PaymentCode,
                                BenificayId = request.Payment.BenificayId,
                                Month = item.SelectedMonth,
                                Amount = request.Payment.Amount,
                                Year = request.Payment.Year,
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
