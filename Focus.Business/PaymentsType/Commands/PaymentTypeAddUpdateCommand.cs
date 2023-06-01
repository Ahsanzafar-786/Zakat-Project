using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.PaymentsType.Model;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Focus.Business.PaymentsType.Commands
{
    public class PaymentTypeAddUpdateCommand : IRequest<Message>
    {
        public PaymentTypeLookupModel Payments { get; set; }
        public class Handler : IRequestHandler<PaymentTypeAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentTypeAddUpdateCommand> logger)
            {
                Context = context;
                Logger = logger;
            }
            public async Task<Message> Handle(PaymentTypeAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Payments.Id == Guid.Empty)
                    {
                        var paymentType = new PaymentType
                        {
                            Name = request.Payments.Name,
                            NameAr = request.Payments.NameAr,
                            Code = request.Payments.Code,
                            Description = request.Payments.Description,
                            IsActive = request.Payments.IsActive
                        };

                        await Context.PaymentTypes.AddAsync(paymentType);

                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = paymentType.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                    else
                    {
                        var paymentType = await Context.PaymentTypes.FindAsync(request.Payments.Id);
                        if (paymentType == null)
                            throw new NotFoundException("Payment Type Not Found", "");

                        paymentType.Name = request.Payments.Name;
                        paymentType.NameAr = request.Payments.NameAr;
                        paymentType.Code = request.Payments.Code;
                        paymentType.Description = request.Payments.Description;
                        paymentType.IsActive = request.Payments.IsActive;

                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = paymentType.Id,
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
