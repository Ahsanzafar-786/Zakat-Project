using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Payments.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Focus.Domain.Entities;
using NPOI.SS.Formula.Functions;

namespace Focus.Business.PaymentByAuthPerson.Command
{
    public class PaymentByAuthoirzeCommand : IRequest<Message>
    {
        public PaymentByAuthorizeLookUpModel Payment { get; set; }
        public Guid? PaymentId { get; set; }
        public class Handler : IRequestHandler<PaymentByAuthoirzeCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentByAuthoirzeCommand> logger)
            {
                Context = context;
                Logger = logger;
            }
            public async Task<Message> Handle(PaymentByAuthoirzeCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var paymentAuthorize = new PaymentAuthorizePerson
                    {
                        Date = DateTime.Now,
                        AuthorizePersonId = request.Payment.AuthorizationPersonId,
                        Code = request.Payment.Code,

                        PaymentAuthorizePersonItems = request.Payment.Benificarylist.Select(x =>
                            new PaymentAuthorizePersonItem()
                            {
                                BenificayId = x.Id,
                                
                            }).ToList(),



                    };
                    await Context.PaymentAuthorizePersons.AddAsync(paymentAuthorize, cancellationToken);

                   

                    {
                        //For Payment Code
                        var paymentCode = await Context.Payments
                            .OrderBy(x => x.Code)
                            .LastOrDefaultAsync(cancellationToken: cancellationToken);

                        int code=0;

                        if (paymentCode != null)
                        {
                            if (paymentCode.Code == 0)
                            {
                                 code = 1;
                            }
                            else
                            {

                                Int32 autoNo = Convert.ToInt32((paymentCode.Code));
                                autoNo++;
                                 code = autoNo;
                            }

                        }


                        foreach (var paymentrecord in request.Payment.Benificarylist)
                        {

                            var beneficary = Context.Beneficiaries.FirstOrDefault(x => x.Id == paymentrecord.Id);

                            int paymentType = beneficary.PaymentTypes.Code;
                            var payment = new Payment
                            {
                                BenificayId = paymentrecord.Id,
                                Amount = beneficary.AmountPerMonth,
                                Month = DateTime.Now,
                                Note = "",
                                Code = code,
                                PaymentCode ="",
                                Year = DateTime.Now.Year.ToString(),
                                Period = DateTime.Now.Year.ToString(),
                                UserId = request.Payment.UserId,
                                Date = DateTime.Now,
                                PaymentByAuthorizePerson = true,
                                TotalAmount = paymentType* beneficary.AmountPerMonth,
                                PaymentAuthorizePersonId = paymentAuthorize.Id
                            };

                            Context.Payments.Add(payment);
                            code++;
                            request.PaymentId = payment.Id;
                            var selectedMonth = new List<SelectedMonth>();
                            if (beneficary.CurrentPaymentMonth != null)
                            {
                                var date= beneficary.CurrentPaymentMonth.Value.AddMonths(1);

                                for (int i = 0; i < paymentType; i++)
                                {

                                    selectedMonth.Add(new SelectedMonth
                                    {
                                        PaymentId = payment.Id,
                                        SelectMonth = date,
                                        Amount = beneficary.AmountPerMonth,
                                    });
                                    date = date.AddMonths(1);

                                }
                            }


                            await Context.SelectedMonths.AddRangeAsync(selectedMonth);
                            if (selectedMonth.Count > 0)
                            {
                               
                               
                                    beneficary.CurrentPaymentMonth = selectedMonth.LastOrDefault()?.SelectMonth;
                                    beneficary.LastPaymentAmount = beneficary.AmountPerMonth * selectedMonth.Count;

                                    Context.Beneficiaries.Update(beneficary);

                                
                            }
                            else
                            {
                              
                                {
                                    beneficary.CurrentPaymentMonth = selectedMonth.LastOrDefault()?.SelectMonth;
                                    beneficary.LastPaymentAmount = beneficary.AmountPerMonth * selectedMonth.Count;

                                    Context.Beneficiaries.Update(beneficary);

                                }

                            }

                            foreach (var item in selectedMonth)
                            {
                                var charityTransaction = new CharityTransaction
                                {
                                    DoucmentId = payment.Id,
                                    CharityTransactionDate = payment.Date,
                                    DoucmentDate = DateTime.Now,
                                    DoucmentCode = payment.Code.ToString(),
                                    BenificayId = payment.BenificayId,
                                    Month = item.SelectMonth,
                                    Amount = payment.Amount,
                                    Year = payment.Year,
                                    PaymentByAuthorizePerson = true,

                                };

                                await Context.CharityTransaction.AddAsync(charityTransaction);
                            }

                            paymentAuthorize.Amount += payment.TotalAmount;


                        }





                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = Guid.Empty,
                            IsSuccess = true,
                            PaymentId = paymentAuthorize.Id,
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
