using DocumentFormat.OpenXml.Bibliography;
using Focus.Business.Benificary.Models;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Benificary.Commands
{
    public class BenificiariesAddUpdateCommand : IRequest<Message>
    {
        public BenificariesLookupModel benificiaries { get; set; }

        public class Handler : IRequestHandler<BenificiariesAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<BenificiariesAddUpdateCommand> logger)
            {
                Context = context;
                Logger = logger;
            }


            public async Task<Message> Handle(BenificiariesAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if(request.benificiaries.Id == Guid.Empty)
                    {
                        var benificary = Context.Beneficiaries.OrderBy(x => x.Id).LastOrDefault();
                        var benificaryNo = 1;
                        if (benificary!= null)
                        {
                            benificaryNo = benificary.BeneficiaryId + 1;
                        }


                        var benifiary = new Beneficiaries
                        {
                            BeneficiaryId = benificaryNo,
                            Name = request.benificiaries.Name,
                            PaymentIntervalMonth = request.benificiaries.PaymentIntervalMonth,
                            AmountPerMonth = Convert.ToDecimal(request.benificiaries.AmountPerMonth),
                            UgamaNo = request.benificiaries.UgamaNo,
                            PhoneNo = request.benificiaries.PhoneNo,
                            Note = request.benificiaries.Note,
                            IsActive = request.benificiaries.IsActive,
                            IsRegister = request.benificiaries.IsRegister,
                        };

                        await Context.Beneficiaries.AddAsync(benifiary);
                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = benifiary.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                    else
                    {
                        var benifiaryDetail = await Context.Beneficiaries.FindAsync(request.benificiaries.Id);
                        if (benifiaryDetail == null)
                            throw new NotFoundException("City Not Found", "");

                        benifiaryDetail.BeneficiaryId = request.benificiaries.BeneficiaryId;
                        benifiaryDetail.Name = request.benificiaries.Name;
                        benifiaryDetail.PaymentIntervalMonth = request.benificiaries.PaymentIntervalMonth;
                        benifiaryDetail.AmountPerMonth = Convert.ToDecimal(request.benificiaries.AmountPerMonth);
                        benifiaryDetail.UgamaNo = request.benificiaries.UgamaNo;
                        benifiaryDetail.PhoneNo = request.benificiaries.PhoneNo;
                        benifiaryDetail.Note = request.benificiaries.Note;
                        benifiaryDetail.IsActive = request.benificiaries.IsActive;
                        benifiaryDetail.IsRegister = request.benificiaries.IsRegister;

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        return new Message
                        {
                            Id = benifiaryDetail.Id,
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
