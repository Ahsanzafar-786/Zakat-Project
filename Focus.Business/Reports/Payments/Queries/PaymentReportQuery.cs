﻿using Focus.Business.Interface;
using Focus.Business.Payments.Models;
using Focus.Business.Transactions.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Reports.Payments.Models;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Focus.Domain.Entities;

namespace Focus.Business.Reports.Payments.Queries
{
    public class PaymentReportQuery : IRequest<List<PaymentWiseListLookupModel>>
    {
        public Guid? BenificayId { get; set; }
        public Guid? CashierId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public class Handler : IRequestHandler<PaymentReportQuery, List<PaymentWiseListLookupModel>>
        {
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentReportQuery> logger, UserManager<ApplicationUser> userManager)
            {
                Context = context;
                _logger = logger;
                _userManager = userManager;
            }
            public async Task<List<PaymentWiseListLookupModel>> Handle(PaymentReportQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var users = _userManager.Users.ToList();
                    var query = Context.Payments.Include(x => x.Beneficiaries).ThenInclude(x => x.PaymentTypes)
                         .Select(x => new PaymentWiseListLookupModel
                         {
                             Id = x.Id,
                             PaymentId = x.Code,
                             BeneficaryId = x.Beneficiaries.BeneficiaryId,
                             BeneficaryName = x.Beneficiaries.Name == null ? x.Beneficiaries.NameAr : x.Beneficiaries.Name,
                             CashierName = users.FirstOrDefault(y => y.Id == x.UserId).UserName,
                             Amount = x.Amount,
                             Note = x.Note,
                             PaymentType = x.Beneficiaries.PaymentTypes.Name,

                         }) ;

                    //if (request.BenificayId.HasValue && request.BenificayId != Guid.Empty)
                    //{
                    //    query = query.Where(x => x.BenificayId == request.BenificayId);
                    //}

                    //if (request.Month.HasValue)
                    //{
                    //    query = query.Where(x => x.Month.Value.Month == request.Month.Value.Month);
                    //}

                    //if (request.FromDate.HasValue && request.ToDate.HasValue)
                    //{
                    //    query = query.Where(x => x.Month.Value >= request.FromDate.Value && x.Month.Value <= request.ToDate.Value.AddDays(1));
                    //}



                    //var charity = await query.ToListAsync();

                    //var benific = await Context.Beneficiaries
                    //    .Where(b => charity.Select(c => c.BenificayId).Contains(b.Id))
                    //    .ToListAsync();

                    //foreach (var transaction in charity)
                    //{
                    //    var benificiary = benific.FirstOrDefault(b => b.Id == transaction.BenificayId);
                    //    transaction.benificaryName = (string.IsNullOrEmpty(benificiary?.Name) ? benificiary?.NameAr : benificiary?.Name);
                    //}

                    return query.ToList();
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