using Focus.Business.Interface;
using Focus.Business.Payments.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Focus.Business.Reports.Payments.Models;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Focus.Business.Reports.Payments.Models;

namespace Focus.Business.Reports.Payments.Queries
{
    public class PaymentReportQuery : IRequest<PaymentWiseOpeningClosingModel>
    {
        public Guid? BenificayId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? SelectedDate { get; set; }
        public string PaymentType { get; set; }

        public class Handler : IRequestHandler<PaymentReportQuery, PaymentWiseOpeningClosingModel>
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
            public async Task<PaymentWiseOpeningClosingModel> Handle(PaymentReportQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    //DateTime openingBalanceDate = request.SelectedDate?.AddDays(-1) ?? DateTime.Now.AddDays(-1);


                  

                    var Transaction = Context.CharityTransaction;

                    var funds = await Transaction.Where(x => x.CharityTransactionDate.Value.Date < request.FromDate.Value.Date && x.BenificayId == null && x.DocumentName == "Funds").SumAsync(x => x.Amount);
                    var charity = await Transaction.Where(x => x.CharityTransactionDate.Value.Date < request.FromDate.Value.Date && x.BenificayId != null).SumAsync(x => x.Amount);

                    var openingBalance = funds - charity;

                    var List = Context.Payments.Include(x => x.Beneficiaries).ThenInclude(x => x.PaymentTypes).Include(x => x.SelectedMonth)
                           .ToList();


                    if (request.BenificayId.HasValue && request.BenificayId != Guid.Empty)
                    {
                        List = List.Where(x => x.BenificayId == request.BenificayId).ToList();
                    }
                    
                    if (request.UserId.HasValue && request.UserId != Guid.Empty)
                    {
                        List = List.Where(x => x.UserId == request.UserId.ToString()).ToList();
                    }

                    if (request.PaymentType != null)
                    {
                        List = List.Where(x => x.Beneficiaries.PaymentTypes.Name == request.PaymentType).ToList();
                    }

                    if (request.FromDate.HasValue && request.ToDate.HasValue)
                    {
                        List = List.Where(x => x.Date.Value.Date >= request.FromDate.Value.Date && x.Date.Value.Date <= request.ToDate.Value.Date).ToList();

                    }

                    var query= List.Select(x => new PaymentWiseListLookupModel()
                    {


                        Id = x.Id,
                        PaymentId = x.Code.ToString(),
                        Beneficary = x.Beneficiaries != null ? x.Beneficiaries.Id : Guid.Empty,
                        BeneficaryId = x.Beneficiaries != null ? x.Beneficiaries.BeneficiaryId.ToString() : "",
                        BeneficaryName = (x.Beneficiaries.Name == "" || x.Beneficiaries.Name == null) ? x.Beneficiaries.NameAr : x.Beneficiaries.Name,
                        CashierName = x.UserId != null ? _userManager.Users.FirstOrDefault(ur => ur.Id == x.UserId).UserName : "",
                        UserId = x.UserId != null ? Guid.Parse(x.UserId) : Guid.Empty,
                        Amount = x.TotalAmount,
                        Note = x.Note,
                        SelectedMonth = x.SelectedMonth.Select(y => y.SelectMonth).ToList(),
                        PaymentType = x.Beneficiaries.PaymentTypes.Name,
                        Date = Convert.ToDateTime(x.Month),
                        PaymentDate = Convert.ToDateTime(x.Month).ToString("dd/MM/yy"),
                    }).ToList();


                    return new PaymentWiseOpeningClosingModel
                    {
                        OpeningBalance= openingBalance,
                        PaymentList = query.ToList(),
                        ClosingBalance= openingBalance - query.Sum(x =>x.Amount)

                    };

                    
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
