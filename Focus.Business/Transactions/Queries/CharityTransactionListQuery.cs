using Focus.Business.CharityResource.Model;
using Focus.Business.CharityResource.Queries;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Transactions.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Focus.Business.Payments.Models;
using Syncfusion.CompoundFile.Net;
using Microsoft.EntityFrameworkCore;
using Focus.Domain.Entities;
using DocumentFormat.OpenXml.InkML;

namespace Focus.Business.Transactions.Queries
{
    public class CharityTransactionListQuery : IRequest<List<CharityTransactionLookupModel>>
    {
        public Guid? BenificayId { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string PaymentType { get; set; }


        public class Handler : IRequestHandler<CharityTransactionListQuery, List<CharityTransactionLookupModel>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<CharityTransactionListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<List<CharityTransactionLookupModel>> Handle(CharityTransactionListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = Context.CharityTransaction.AsNoTracking().Include(x => x.Beneficiaries)
                         .Where(x => x.DocumentName == null && !x.IsVoid);
                       
                    if (request.BenificayId.HasValue && request.BenificayId != Guid.Empty)
                    {
                        query = query.Where(x => x.BenificayId == request.BenificayId);
                    }
                    if (request.PaymentType != null)
                    {
                        query = query.Where(x => x.Beneficiaries.PaymentTypes.Name == request.PaymentType);
                    }

                    if (request.Month.HasValue)
                    {
                        query = query.Where(x => x.CharityTransactionDate.Value.Month == request.Month.Value.Month &&
                      x.CharityTransactionDate.Value.Year == request.Month.Value.Year);
                    }

                    if (request.FromDate.HasValue && request.ToDate.HasValue)
                    {
                        query = query.Where(x => x.CharityTransactionDate.Value.Date >= request.FromDate.Value.Date && x.CharityTransactionDate.Value.Date <= request.ToDate.Value.Date);
                    }



                    var charity = query.Select(x => new CharityTransactionLookupModel
                    {
                        Id = x.Id,
                        DoucmentId = x.DoucmentId,
                        benificaryName = x.Beneficiaries.NameAr==null || x.Beneficiaries.NameAr==""
                        ?x.Beneficiaries.Name : x.Beneficiaries.NameAr,
                        DoucmentCode = x.DoucmentCode,
                        HijriYear = x.Beneficiaries.BeneficiaryId.ToString(),
                        Amount = x.Amount,
                        CharityTransactionDate = x.CharityTransactionDate,
                        DoucmentDate = x.DoucmentDate,
                        Month = x.Month,
                        BenificayId = x.BenificayId,
                        Year = x.Year,
                    }).ToList();

                  

                    return charity;
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
