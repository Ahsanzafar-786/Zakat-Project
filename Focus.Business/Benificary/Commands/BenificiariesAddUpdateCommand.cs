
using DocumentFormat.OpenXml.Wordprocessing;
using Focus.Business.Benificary.Models;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Payments.Queries;
using Focus.Business.Transactions.Models;
using Focus.Domain.Entities;
using Focus.Domain.Interface;
using ICSharpCode.SharpZipLib.Zip;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NPOI.POIFS.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Benificary.Commands
{
    public class BenificiariesAddUpdateCommand : IRequest<Message>
    {
        public BenificariesLookupModel benificiaries { get; set; }
        public Guid? PaymentId { get; set; }

        public class Handler : IRequestHandler<BenificiariesAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;
            private readonly IMediator _mediator;
            private readonly IUserHttpContextProvider _contextProvider;

            public Handler(IApplicationDbContext context, ILogger<BenificiariesAddUpdateCommand> logger, IMediator mediator, IUserHttpContextProvider contextProvider)
            {
                Context = context;
                Logger = logger;
                _mediator = mediator;
                _contextProvider = contextProvider;
            }


            public async Task<Message> Handle(BenificiariesAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.benificiaries.Id == Guid.Empty)
                    {
                        if (request.benificiaries.BenificaryAuthorization.Count > 0)
                        {
                            var benifiary = new Beneficiaries
                            {
                                BeneficiaryId = request.benificiaries.BeneficiaryId,
                                ApprovalStatus = request.benificiaries.ApprovalStatus,
                                Name = request.benificiaries.Name,
                                PaymentIntervalMonth = request.benificiaries.PaymentIntervalMonth,
                                AmountPerMonth = request.benificiaries.AmountPerMonth,
                                RecurringAmount = request.benificiaries.RecurringAmount,
                                UgamaNo = request.benificiaries.UgamaNo,
                                PhoneNo = request.benificiaries.PhoneNo,
                                Note = request.benificiaries.Note,
                                IsActive = request.benificiaries.IsActive,
                                ApprovalPersonId = request.benificiaries.ApprovalPersonId,
                                Address = request.benificiaries.Address,
                                AuthorizedPersonId = request.benificiaries.AuthorizedPersonId,
                                PaymentTypeId = request.benificiaries.PaymentTypeId,
                                NameAr = request.benificiaries.NameAr,
                                Nationality = request.benificiaries.Nationality,
                                Gender = request.benificiaries.Gender,
                                PassportNo = request.benificiaries.PassportNo,
                                AdvancePayment = request.benificiaries.AdvancePayment,
                                DurationType = request.benificiaries.DurationType,
                                ApprovedPaymentId = request.benificiaries.ApprovedPaymentId,
                                StartDate = request.benificiaries.StartDate,
                                EndDate = request.benificiaries.EndDate == null ? null : request.benificiaries.EndDate.Value.AddDays(6),
                                StartMonth = request.benificiaries.StartMonth,
                                IsRegister = true,
                                BenificaryAuthorization = request.benificiaries.BenificaryAuthorization.Select(x => new BenificaryAuthorization()
                                {
                                    ApprovalPersonId = x.ApprovalPersonId,
                                    AuthorizationPersonId = x.AuthorizationPersonId,
                                    IsActive = x.IsActive,
                                    Description = x.Description,
                                    Date = DateTime.Now,
                                }).ToList()
                            };

                            await Context.Beneficiaries.AddAsync(benifiary);
                            await Context.SaveChangesAsync();

                            return new Message
                            {
                                Id = benifiary.Id,
                                IsSuccess = true,
                                IsAddUpdate = "Data has been Added successfully." + "BenificaryID is " + " " + benifiary.BeneficiaryId
                            };
                        }
                        else
                        {


                            var charity = await Context.CharityTransaction.Select(x => new CharityTransactionLookupModel()
                            {

                                BenificayId = x.BenificayId,
                                Amount = x.Amount,

                            }).ToListAsync();
                            var openingBalance = charity.Where(x => x.BenificayId == null).Sum(x => x.Amount) - charity.Where(x => x.BenificayId != null).Sum(x => x.Amount);

                            if (openingBalance < request.benificiaries.AmountPerMonth)
                            {
                                return new Message
                                {
                                    Id = Guid.Empty,
                                    IsSuccess = false,
                                    IsAddUpdate = "Insufficient funds"
                                };

                            }

                            else
                            {
                                var benifiary = new Beneficiaries
                                {
                                    BeneficiaryId = request.benificiaries.BeneficiaryId,
                                    ApprovalStatus = request.benificiaries.ApprovalStatus,
                                    Name = request.benificiaries.Name,
                                    PaymentIntervalMonth = request.benificiaries.PaymentIntervalMonth,
                                    AmountPerMonth = request.benificiaries.AmountPerMonth,
                                    
                                   
                                    RecurringAmount = request.benificiaries.RecurringAmount,
                                    UgamaNo = request.benificiaries.UgamaNo,
                                    PhoneNo = request.benificiaries.PhoneNo,
                                    Note = request.benificiaries.Note,
                                    IsActive = request.benificiaries.IsActive,
                                    ApprovalPersonId = request.benificiaries.ApprovalPersonId,
                                    Address = request.benificiaries.Address,
                                    AuthorizedPersonId = request.benificiaries.AuthorizedPersonId,
                                    PaymentTypeId = request.benificiaries.PaymentTypeId,
                                    NameAr = request.benificiaries.NameAr,
                                    Nationality = request.benificiaries.Nationality,
                                    Gender = request.benificiaries.Gender,
                                    PassportNo = request.benificiaries.PassportNo,
                                    AdvancePayment = request.benificiaries.AdvancePayment,
                                    DurationType = request.benificiaries.DurationType,
                                    ApprovedPaymentId = request.benificiaries.ApprovedPaymentId,
                                    StartDate = request.benificiaries.StartDate,
                                    EndDate = request.benificiaries.EndDate == null ? null : request.benificiaries.EndDate.Value.AddDays(6),
                                    StartMonth = request.benificiaries.StartMonth,
                                    IsRegister = false,
                                    BenificaryAuthorization = request.benificiaries.BenificaryAuthorization.Select(x => new BenificaryAuthorization()
                                    {
                                        ApprovalPersonId = x.ApprovalPersonId,
                                        AuthorizationPersonId = x.AuthorizationPersonId,
                                        IsActive = x.IsActive,
                                        Description = x.Description,
                                        Date = DateTime.Now,
                                    }).ToList()
                                };
                                if (request.benificiaries.IsDailyPayment) {


                                    benifiary.LastPaymentAmount = request.benificiaries.AmountPerMonth;
                                    benifiary.CurrentPaymentMonth = DateTime.Now;
                                }
                               

                                await Context.Beneficiaries.AddAsync(benifiary);
                                if (request.benificiaries.DocumentType == "dailyPayment")
                                {
                                    var autoNo = await _mediator.Send(new AutoCodeGenerateQuery
                                    {
                                        Name = "",
                                    });



                                    var payment = new Payment
                                    {
                                        BenificayId = benifiary.Id,
                                        Amount = benifiary.AmountPerMonth,
                                        TotalAmount = benifiary.AmountPerMonth,
                                        Month = DateTime.Now,
                                        Code = Convert.ToInt32(autoNo),
                                        Note = benifiary.Note,
                                        PaymentCode = autoNo,
                                        Year = DateTime.Now.Year.ToString(),
                                        Period = DateTime.Now.Year.ToString(),
                                        Date = DateTime.Now,
                                        UserId = _contextProvider.GetUserId().ToString(),
                                    };

                                    Context.Payments.Add(payment);
                                    var selectedMonth = new List<SelectedMonth>();

                                    selectedMonth.Add(new SelectedMonth
                                    {
                                        PaymentId = payment.Id,
                                        SelectMonth = payment.Month,
                                        Amount = payment.Amount


                                    });


                                    await Context.SelectedMonths.AddRangeAsync(selectedMonth);
                                    request.PaymentId = payment.Id;


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

                                await Context.SaveChangesAsync();

                                return new Message
                                {
                                    Id = benifiary.Id,
                                    PaymentId = request.PaymentId == null ? Guid.Empty : request.PaymentId.Value,
                                    IsSuccess = true,
                                    IsAddUpdate = "Data has been Added successfully." + "BenificaryID is " + " " + benifiary.BeneficiaryId
                                };
                            }
                        }
                        
                    }
                    else
                    {
                        var benifiaryDetail = await Context.Beneficiaries.FindAsync(request.benificiaries.Id);
                        if (benifiaryDetail == null)
                            throw new NotFoundException("Benificary Not Found", "");

                        if(request.benificiaries.BenificaryAuthorization.Count > 0)
                        {
                            benifiaryDetail.BeneficiaryId = request.benificiaries.BeneficiaryId;
                            benifiaryDetail.ApprovalStatus = request.benificiaries.ApprovalStatus;
                            benifiaryDetail.Name = request.benificiaries.Name;
                            benifiaryDetail.PaymentIntervalMonth = request.benificiaries.PaymentIntervalMonth;
                            benifiaryDetail.AmountPerMonth = request.benificiaries.AmountPerMonth;
                            benifiaryDetail.RecurringAmount = request.benificiaries.RecurringAmount;
                            benifiaryDetail.UgamaNo = request.benificiaries.UgamaNo;
                            benifiaryDetail.AuthorizedPersonId = request.benificiaries.AuthorizedPersonId;
                            benifiaryDetail.PhoneNo = request.benificiaries.PhoneNo;
                            benifiaryDetail.Note = request.benificiaries.Note;
                            benifiaryDetail.IsActive = request.benificiaries.IsActive;
                            benifiaryDetail.ApprovalPersonId = request.benificiaries.ApprovalPersonId;
                            benifiaryDetail.Address = request.benificiaries.Address;
                            benifiaryDetail.PaymentTypeId = request.benificiaries.PaymentTypeId;
                            benifiaryDetail.NameAr = request.benificiaries.NameAr;
                            benifiaryDetail.Nationality = request.benificiaries.Nationality;
                            benifiaryDetail.Gender = request.benificiaries.Gender;
                            benifiaryDetail.PassportNo = request.benificiaries.PassportNo;
                            benifiaryDetail.StartDate = request.benificiaries.StartDate;
                            benifiaryDetail.EndDate = request.benificiaries?.EndDate;
                            benifiaryDetail.StartMonth = request.benificiaries.StartMonth;
                            benifiaryDetail.AdvancePayment = request.benificiaries.AdvancePayment;
                            benifiaryDetail.ApprovedPaymentId = request.benificiaries.ApprovedPaymentId;
                            benifiaryDetail.DurationType = request.benificiaries.DurationType;
                            benifiaryDetail.Reason = request.benificiaries.Reason;
                            benifiaryDetail.IsRegister = true;

                            Context.BenificaryAuthorizations.RemoveRange(benifiaryDetail.BenificaryAuthorization);

                            benifiaryDetail.BenificaryAuthorization = request.benificiaries.BenificaryAuthorization.Select(x => new BenificaryAuthorization()
                            {
                                BenficaryId = benifiaryDetail.Id,
                                AuthorizationPersonId = x.AuthorizationPersonId,
                                ApprovalPersonId = x.ApprovalPersonId,
                                IsActive = x.IsActive,
                                Date = DateTime.Now,
                                Description = x.Description

                            }).ToList();

                            await Context.BenificaryAuthorizations.AddRangeAsync(benifiaryDetail.BenificaryAuthorization);

                            if(benifiaryDetail.RecurringAmount != request.benificiaries.RecurringAmount)
                            {
                                var logs = new Logs
                                {
                                    BenificaryId = benifiaryDetail.Id,
                                    Name = request.benificiaries.Name,
                                    NameAr = request.benificiaries.NameAr,
                                    PhoneNo = request.benificiaries.PhoneNo,
                                    AdvancePayment = request.benificiaries.AdvancePayment,
                                    AmountPerMonth = request.benificiaries.AmountPerMonth,
                                    ApprovalPersonId = request.benificiaries.ApprovalPersonId,
                                    ApprovedPaymentId = request.benificiaries.ApprovedPaymentId,
                                    RecurringAmount = request.benificiaries.RecurringAmount,
                                    DurationType = request.benificiaries.DurationType,
                                    EndDate= request.benificiaries.EndDate,
                                    StartDate= request.benificiaries.StartDate,
                                    StartMonth = request.benificiaries.StartMonth,
                                    PaymentTypeId = request.benificiaries.PaymentTypeId,
                                };

                                await Context.Logs.AddAsync(logs);
                            }

                            //Save changes to database
                            await Context.SaveChangesAsync(cancellationToken);

                            return new Message
                            {
                                Id = benifiaryDetail.Id,
                                IsSuccess = true,
                                IsAddUpdate = "Data has been Added successfully." + "BenificaryID is " + " " + benifiaryDetail.BeneficiaryId
                            };
                        }
                        else
                        {
                            benifiaryDetail.BeneficiaryId = request.benificiaries.BeneficiaryId;
                            benifiaryDetail.ApprovalStatus = request.benificiaries.ApprovalStatus;
                            benifiaryDetail.Name = request.benificiaries.Name;
                            benifiaryDetail.PaymentIntervalMonth = request.benificiaries.PaymentIntervalMonth;
                            benifiaryDetail.AmountPerMonth = request.benificiaries.AmountPerMonth;
                            benifiaryDetail.RecurringAmount = request.benificiaries.RecurringAmount;
                            benifiaryDetail.UgamaNo = request.benificiaries.UgamaNo;
                            benifiaryDetail.AuthorizedPersonId = request.benificiaries.AuthorizedPersonId;
                            benifiaryDetail.PhoneNo = request.benificiaries.PhoneNo;
                            benifiaryDetail.Note = request.benificiaries.Note;
                            benifiaryDetail.IsActive = request.benificiaries.IsActive;
                            benifiaryDetail.ApprovalPersonId = request.benificiaries.ApprovalPersonId;
                            benifiaryDetail.Address = request.benificiaries.Address;
                            benifiaryDetail.PaymentTypeId = request.benificiaries.PaymentTypeId;
                            benifiaryDetail.NameAr = request.benificiaries.NameAr;
                            benifiaryDetail.Nationality = request.benificiaries.Nationality;
                            benifiaryDetail.Gender = request.benificiaries.Gender;
                            benifiaryDetail.PassportNo = request.benificiaries.PassportNo;
                            benifiaryDetail.StartDate = request.benificiaries.StartDate;
                            benifiaryDetail.EndDate = request.benificiaries?.EndDate;
                            benifiaryDetail.StartMonth = request.benificiaries.StartMonth;
                            benifiaryDetail.AdvancePayment = request.benificiaries.AdvancePayment;
                            benifiaryDetail.ApprovedPaymentId = request.benificiaries.ApprovedPaymentId;
                            benifiaryDetail.DurationType = request.benificiaries.DurationType;
                            benifiaryDetail.Reason = request.benificiaries.Reason;
                            benifiaryDetail.IsRegister = false;

                            Context.BenificaryAuthorizations.RemoveRange(benifiaryDetail.BenificaryAuthorization);

                            benifiaryDetail.BenificaryAuthorization = request.benificiaries.BenificaryAuthorization.Select(x => new BenificaryAuthorization()
                            {
                                BenficaryId = benifiaryDetail.Id,
                                AuthorizationPersonId = x.AuthorizationPersonId,
                                ApprovalPersonId= x.ApprovalPersonId,
                                IsActive = x.IsActive,
                                Date = DateTime.Now,
                                Description = x.Description
                                
                            }).ToList();

                            await Context.BenificaryAuthorizations.AddRangeAsync(benifiaryDetail.BenificaryAuthorization);

                            if (benifiaryDetail.RecurringAmount != request.benificiaries.RecurringAmount)
                            {
                                var logs = new Logs
                                {
                                    BenificaryId = benifiaryDetail.Id,
                                    Name = request.benificiaries.Name,
                                    NameAr = request.benificiaries.NameAr,
                                    PhoneNo = request.benificiaries.PhoneNo,
                                    AdvancePayment = request.benificiaries.AdvancePayment,
                                    AmountPerMonth = request.benificiaries.AmountPerMonth,
                                    ApprovalPersonId = request.benificiaries.ApprovalPersonId,
                                    ApprovedPaymentId = request.benificiaries.ApprovedPaymentId,
                                    RecurringAmount = request.benificiaries.RecurringAmount,
                                    DurationType = request.benificiaries.DurationType,
                                    EndDate = request.benificiaries.EndDate,
                                    StartDate = request.benificiaries.StartDate,
                                    StartMonth = request.benificiaries.StartMonth,
                                    PaymentTypeId = request.benificiaries.PaymentTypeId,
                                };

                                await Context.Logs.AddAsync(logs);
                            }

                            //Save changes to database
                            await Context.SaveChangesAsync(cancellationToken);

                            return new Message
                            {
                                Id = benifiaryDetail.Id,
                                IsSuccess = true,
                                IsAddUpdate = "Data has been Added successfully." + "BenificaryID is " + " " + benifiaryDetail.BeneficiaryId
                            };
                        }
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
