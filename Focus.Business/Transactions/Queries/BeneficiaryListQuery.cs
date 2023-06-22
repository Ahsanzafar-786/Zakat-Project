using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Transactions.Models;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Transactions.Queries
{
    public class BeneficiaryListQuery : IRequest<List<CharityTransactionLookupModel>>
    {
        public Guid? AuthorizationPersonId { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public string Registered { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public class Handler : IRequestHandler<BeneficiaryListQuery, List<CharityTransactionLookupModel>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<BeneficiaryListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<List<CharityTransactionLookupModel>> Handle(BeneficiaryListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var benific = Context.Beneficiaries.AsNoTracking().ToList();
                    
                    //if (request.AuthorizationPersonId != Guid.Empty && request.AuthorizationPersonId != null)
                    //{
                    //    benific = benific.Where(x => x.BenificayId == request.BenificayId).ToList();
                    //}
                    if (request.ApprovalPersonId != Guid.Empty && request.ApprovalPersonId != null)
                    {
                        benific = benific.Where(x => x.ApprovalPersonId== request.ApprovalPersonId).ToList();
                    }
                    if (request.FromDate != null)
                    {
                        benific.Where(x => x.StartDate == request.FromDate).ToList();
                    }
                    if (request.ToDate != null)
                    {
                        benific = benific.Where(x => x.StartDate == request.ToDate).ToList();
                    }

                    return null;

                    //return charity;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
            }
        }
    }
}
