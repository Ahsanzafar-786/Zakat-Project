using Focus.Business.Benificary.Models;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Transactions.Models;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Benificary.Queries
{
    public class BenificariesPaymentReAssignQuery  : IRequest<Message>
    {


        public class Handler : IRequestHandler<BenificariesPaymentReAssignQuery, Message>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<BenificariesPaymentReAssignQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<Message> Handle(BenificariesPaymentReAssignQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    var charityTransactions = await Context.CharityTransaction.AsNoTracking().ToListAsync();

                    
                    var beneficiaries = await Context.Beneficiaries.AsNoTracking().ToListAsync();

                    

                    foreach (var beneficiary in beneficiaries)
                    {
                        if (beneficiary != null && beneficiary.CurrentPaymentMonth == null)
                        {

                            var records = charityTransactions.Where(x => x.BenificayId == beneficiary.Id).LastOrDefault();

                            if (records != null && beneficiary.CurrentPaymentMonth == null)
                            {
                                beneficiary.CurrentPaymentMonth = records.Month;
                                // beneficiary.Total = records.Amount;
                            }
                        }
                      
                    }
                     Context.Beneficiaries.UpdateRange(beneficiaries);
                    
                  //  await Context.SaveChangesAsync(cancellationToken);

                    
                    return new Message
                    {
                        Id = Guid.Empty, 
                        IsSuccess = true,
                        IsAddUpdate = "Data has been Added successfully"
                    };
                }
                
                catch (Exception exception)
                {
                    // Handle other exceptions
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Something Went Wrong.");
                }

               
            }
        }
    }
}
