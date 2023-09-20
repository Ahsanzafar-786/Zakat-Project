using Focus.Business.Interface;
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

namespace Focus.Business.Reports.Payments.Queries
{
    public class TransactionReportQuery : IRequest<PaymentWiseOpeningClosingModel>
    {
        public Guid? BenificayId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? SelectedDate { get; set; }

        public class Handler : IRequestHandler<TransactionReportQuery, PaymentWiseOpeningClosingModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<TransactionReportQuery> logger, UserManager<ApplicationUser> userManager)
            {
                Context = context;
                _logger = logger;
                _userManager = userManager;
            }
            public async Task<PaymentWiseOpeningClosingModel> Handle(TransactionReportQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    //DateTime openingBalanceDate = request.SelectedDate?.AddDays(-1) ?? DateTime.Now.AddDays(-1);
                    var Transaction =  Context.CharityTransaction;

                    var funds = await Transaction.Where(x => x.CharityTransactionDate.Value.Date < request.FromDate.Value.Date && x.BenificayId == null).SumAsync(x => x.Amount);
                    var charity = await Transaction.Where(x => x.CharityTransactionDate.Value.Date < request.FromDate.Value.Date && x.BenificayId != null).SumAsync(x => x.Amount);

                    var openingBalance = funds - charity;

                     var fundList = await Transaction
                    .Where(j => j.BenificayId == null)
                    .Include(x => x.Beneficiaries.PaymentTypes)
                    .ToListAsync();

          
                    var cashiers = await _userManager.Users.ToListAsync();
                    var fundsList = Context.Funds;

                    var fundslist = fundList.Select(x => new PaymentWiseListLookupModel
                    {
                        Id = x.Id,
                        Amount = x.Amount,
                        Date = Convert.ToDateTime(x.CharityTransactionDate),
                        PaymentDate = Convert.ToDateTime(x.CharityTransactionDate).ToString("dd/MM/yy"),
                        PaymentMonth = Convert.ToDateTime(x.Month).ToString("MMMM"),
                        CashierName = cashiers.FirstOrDefault(c => c.Id == x.UserId)?.UserName ?? "",
                        Description= fundsList.FirstOrDefault(j=> j.Id == x.DoucmentId).Description,
                        Transactiontype= fundsList.FirstOrDefault(j=> j.Id == x.DoucmentId).TypeOfTransaction,
                    }).ToList();


                    var charitylist = await Transaction.Where(j => j.BenificayId != null).Include(x => x.Beneficiaries.PaymentTypes)
                        .Select(x => new PaymentWiseListLookupModel()
                        {
                            Id = x.Id,

                            Beneficary = x.Beneficiaries != null ? x.Beneficiaries.Id : Guid.Empty,
                            BeneficaryId = x.Beneficiaries != null ? x.Beneficiaries.BeneficiaryId.ToString() : "",
                            BeneficaryName = (x.Beneficiaries.Name == "" || x.Beneficiaries.Name == null) ? x.Beneficiaries.NameAr : x.Beneficiaries.Name,
                            PaymentIntervalMonth = x.Beneficiaries.PaymentIntervalMonth,
                            IsRegistered = x.Beneficiaries.IsRegister ? "Registered" : "Not Registered",
                            Amount = x.Amount,
                            PaymentType = x.Beneficiaries.PaymentTypes.Name,
                            Date = Convert.ToDateTime(x.CharityTransactionDate),
                            PaymentDate = Convert.ToDateTime(x.CharityTransactionDate).ToString("dd/MM/yy"),
                            PaymentMonth = Convert.ToDateTime(x.Month).ToString("MMMM"),

                        }).ToListAsync();

                    //if (request.BenificayId.HasValue && request.BenificayId != Guid.Empty)
                    //{
                    //    query = query.Where(x => x.Beneficary == request.BenificayId).ToList();
                    //}
                    
                    //if (request.UserId.HasValue && request.UserId != Guid.Empty)
                    //{
                    //    query = query.Where(x => x.UserId == request.UserId).ToList();
                    //}

                    if (request.FromDate.HasValue && request.ToDate.HasValue)
                    {
                        charitylist = charitylist.Where(x => x.Date.Value.Date >= request.FromDate.Value.Date && x.Date.Value.Date <= request.ToDate.Value.Date).ToList();
                        fundslist = fundslist.Where(x => x.Date.Value.Date >= request.FromDate.Value.Date && x.Date.Value.Date <= request.ToDate.Value.Date).ToList();

                    }

                


                    return new PaymentWiseOpeningClosingModel
                    {
                        OpeningBalance= openingBalance,
                        Charitylist = charitylist,
                        PaymentList = fundslist,
                        TransactionTotal= charitylist.Sum(x => x.Amount),
                        FundsTotal= fundslist.Sum(x => x.Amount),
                        ClosingBalance = openingBalance + fundslist.Sum(x => x.Amount) - charitylist.Sum(x => x.Amount) ,
                        Closing = openingBalance - charitylist.Sum(x => x.Amount)

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
