using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Dashboard.Queries
{
    public class GetCashTransactionQuery : IRequest<TransactionLookupModel>
    {
        public DateTime? SelectedDate { get; set; }
        public DateTime? CurrentDate { get; set; }
        public string OverViewFilter { get; set; }

        public class Handler : IRequestHandler<GetCashTransactionQuery, TransactionLookupModel>
        {
            public readonly IApplicationDbContext Context;
            public readonly IMediator Mediator;
            public readonly ILogger Logger;

            public Handler(IMediator mediator, IApplicationDbContext context, ILogger<GetCashTransactionQuery> logger)
            {
                Context = context;
                Mediator = mediator;
                Logger = logger;
            }

            public async Task<TransactionLookupModel> Handle(GetCashTransactionQuery request, CancellationToken cancellationToken)

            {
                try
                {
                    var sales = new List<Sale>();
                    var purchase = new List<PurchasePost>();
                    var earning = new List<EarningLookUpModel>();
                    var salePurchase = new List<SalePurchaseLookUpModel>();
                    var list2 = new SalePurchaseLookUpModel();
                    decimal expeneAmount = 0;

                    #region Filter
                    if (request.OverViewFilter == "Today")
                    {
                        sales = Context.Sales
                           .Include(x => x.SaleItems)
                           .ThenInclude(x => x.TaxRate)
                           .Where(x => x.InvoiceType != 0 && x.Date.Date == DateTime.UtcNow.Date)
                           .ToList();


                        //transactions = Context.Transactions
                        //.Include(x => x.ProductId)
                        //.Where(x => x.Date.Date == DateTime.UtcNow.Date )
                        //.ToList();
                        purchase = Context.PurchasePosts
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.TaxRate)
                        .Where(x => x.Date.Date == DateTime.UtcNow.Date && x.IsPurchasePost || (!x.IsPurchasePost && x.IsPurchaseReturn))
                        .ToList();

                        expeneAmount = Context.DailyExpenses
                        .Include(x => x.DailyExpenseDetails)
                         .Where(x => x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved && x.Date.Date == DateTime.UtcNow.Date)
                           .AsEnumerable()
                         .Sum(y => y.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * (x.TaxRate == null ? 0: x.TaxRate.Rate)) / (100)) + x.Amount : x.Amount));


                    }
                    else if (request.OverViewFilter == "Weekly")
                    {
                        var lastDay = DateTime.UtcNow.Date;
                        var firstDay = lastDay.AddDays(-6);
                        sales = Context.Sales.AsNoTracking()
                           .Include(x => x.SaleItems)
                           .ThenInclude(x => x.TaxRate)
                           .Where(x => x.InvoiceType != 0 && x.Date.Date >= firstDay && x.Date.Date <= lastDay)
                           .ToList();


                        purchase = Context.PurchasePosts.AsNoTracking()
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.TaxRate)
                        .Where(x => x.Date.Date >= firstDay && x.Date.Date <= lastDay && x.IsPurchasePost || (!x.IsPurchasePost && x.IsPurchaseReturn))
                        .ToList();

                        expeneAmount = Context.DailyExpenses.AsNoTracking()
                        .Include(x => x.DailyExpenseDetails)
                         .Where(x => x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved && x.Date.Date >= firstDay && x.Date.Date <= lastDay)
                           .AsEnumerable()
                         .Sum(y => y.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * (x.TaxRate == null ? 0: x.TaxRate.Rate)) / (100)) + x.Amount : x.Amount));



                    }
                    else if (request.OverViewFilter == "Monthly")
                    {
                        sales = Context.Sales.AsNoTracking()
                        .Include(x => x.SaleItems)
                        .ThenInclude(x => x.TaxRate)
                        .Where(x => x.InvoiceType != 0 && x.Date.Month == DateTime.UtcNow.Month)
                        .ToList();


                        purchase = Context.PurchasePosts.AsNoTracking()
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.TaxRate)
                        .Where(x => x.Date.Month == DateTime.UtcNow.Month)
                        .ToList();

                        expeneAmount = Context.DailyExpenses.AsNoTracking()
                       .Include(x => x.DailyExpenseDetails)
                        .Where(x => x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved && x.Date.Month == DateTime.UtcNow.Month)
                        .AsEnumerable()
                         .Sum(y => y.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * (x.TaxRate == null ? 0: x.TaxRate.Rate)) / (100)) + x.Amount : x.Amount));
                    }
                    else if (request.OverViewFilter == "6 Monthly")
                    {
                        var lastDay = DateTime.UtcNow.Date;
                        var firstDay = lastDay.AddMonths(-6);
                        sales = Context.Sales.AsNoTracking()
                           .Include(x => x.SaleItems)
                           .ThenInclude(x => x.TaxRate)
                           .Where(x => x.InvoiceType != 0 && x.Date.Date >= firstDay && x.Date.Date <= lastDay)
                           .ToList();


                        purchase = Context.PurchasePosts.AsNoTracking()
                        .Include(x => x.PurchasePostItems)
                        .ThenInclude(x => x.TaxRate)
                        .Where(x => x.Date.Date >= firstDay && x.Date.Date <= lastDay && x.IsPurchasePost || (!x.IsPurchasePost && x.IsPurchaseReturn))
                        .ToList();

                        expeneAmount = Context.DailyExpenses.AsNoTracking()
                        .Include(x => x.DailyExpenseDetails)
                         .Where(x => x.ApprovalStatus == Domain.Enum.ApprovalStatus.Approved && x.Date.Date >= firstDay && x.Date.Date <= lastDay)
                         .AsEnumerable()
                         .Sum(y => y.DailyExpenseDetails.Sum(x => x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل" ? ((x.Amount * (x.TaxRate==null?0: x.TaxRate.Rate)) / (100)) + x.Amount : x.Amount));
                    }
                    #endregion




                    #region Calculation
                    // For Total Income



                    var income = sales.Where(x => x.SaleInvoiceId == null).Sum(z => z.SaleItems.Sum(item => item.Quantity * item.UnitPrice)) - sales.Where(x => x.SaleInvoiceId == null).Sum(z => z.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                 ? ((item.Quantity * item.UnitPrice) -
                                    ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                                     (item.OfferQuantity == 0
                                         ?
                                         (item.Discount == 0
                                             ? item.Quantity * item.FixDiscount
                                             : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                         : item.Discount == 0
                                             ? item.OfferQuantity * item.FixDiscount
                                             : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                                 item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                 : 0));




                    // For Credit Amount
                    var Credit = (sales.Where(x => x.IsCredit && x.SaleInvoiceId == null).Sum(z => z.SaleItems.Sum(item => item.Quantity * item.UnitPrice)) - sales.Where(x => x.SaleInvoiceId == null && x.IsCredit).Sum(z => z.SaleItems.Sum(item => (item.OfferQuantity == 0 ?
                                      (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                          : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                      : item.Discount == 0 ? item.OfferQuantity * item.FixDiscount
                                          : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) - sales.Where(x => x.SaleInvoiceId != null).Sum(z => z.SaleItems.Sum(item => (item.OfferQuantity == 0 ?
                                        (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                            : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                        : item.Discount == 0 ? item.OfferQuantity * item.FixDiscount
                                            : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100)))) +
                                             sales.Where(x => x.SaleInvoiceId == null && x.IsCredit).Sum(z => z.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                    ? 0
                                    : ((item.Quantity * item.UnitPrice) - (item.OfferQuantity == 0
                                        ?
                                        (item.Discount == 0
                                            ? item.Quantity * item.FixDiscount
                                            : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                        : item.Discount == 0
                                            ? item.OfferQuantity * item.FixDiscount
                                            : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100)) *
                                    item.TaxRate.Rate / 100));

                    var totalInvoices = sales.Count();
                    var unPaidInvoices = sales.Count(x=>x.PartiallyInvoices==Domain.Enum.PartiallyInvoices.UnPaid);
                    var partiallyInvoices = sales.Count(x=>x.PartiallyInvoices==Domain.Enum.PartiallyInvoices.Partially);
                    var fullyInvoices = totalInvoices- unPaidInvoices- partiallyInvoices;
                    var totalCreditInvoices = sales.Count(x => x.IsCredit && x.SaleInvoiceId == null);
                    var totalCashInvoices = sales.Count(x => !x.IsCredit && x.SaleInvoiceId == null);
                    var totalReturns = sales.Count(x => x.SaleInvoiceId != null);


                    // For Purchase


                    var GrossAmount = purchase.Where(x => x.PurchaseInvoiceId == null).Sum(z => z.PurchasePostItems.Sum(item => item.Quantity * item.UnitPrice)) - purchase.Where(x => x.PurchaseInvoiceId == null && (x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل")).Sum(z => z.PurchasePostItems.Sum(item =>

                                  ((item.Quantity * item.UnitPrice) -
                                       (item.Discount == 0
                                              ? item.Quantity * item.FixDiscount
                                              : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                         ) *

                                      item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                      ));
                    decimal Discount = (purchase.Where(x => x.PurchaseInvoiceId == null).Sum(z => z.PurchasePostItems.Sum(item =>
                                  (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                      : (item.Quantity * item.UnitPrice * item.Discount) / 100))));
                    decimal TotalTax = purchase.Where(x => x.PurchaseInvoiceId == null && (x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل")).Sum(z => z.PurchasePostItems.Sum(item =>
                                   ((item.Quantity * item.UnitPrice) -
                                   (item.Discount == 0
                                          ? item.Quantity * item.FixDiscount
                                          : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                     ) *

                                  item.TaxRate.Rate / (100 + item.TaxRate.Rate))) +
                                    purchase.Where(x => x.PurchaseInvoiceId == null && (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل")).Sum(z => z.PurchasePostItems.Sum(item =>
                                    ((item.Quantity * item.UnitPrice) -
                                       (item.Discount == 0
                                           ? item.Quantity * item.FixDiscount
                                           : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                      ) *
                                   item.TaxRate.Rate / 100));
                    var totalPurchases = (GrossAmount - Discount) + TotalTax;
                    #endregion
                    #region Earning Graph
                    foreach (var sale in sales)
                    {
                        if (sale.SaleInvoiceId == null)
                        {
                            earning.Add(new EarningLookUpModel
                            {
                                Date = sale.Date,
                                Amount = Math.Round(sale.SaleItems.Sum(item => item.Quantity * item.UnitPrice) - sale.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                   ? ((item.Quantity * item.UnitPrice) -
                                      ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                                       (item.OfferQuantity == 0
                                           ?
                                           (item.Discount == 0
                                               ? item.Quantity * item.FixDiscount
                                               : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                           : item.Discount == 0
                                               ? item.OfferQuantity * item.FixDiscount
                                               : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                                   item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                   : 0), 2),
                               
                            });

                        }


                    }
                    if (request.OverViewFilter == "Today")
                    {
                        var records = earning.GroupBy(x => x.Date.Hour);
                        var earningRecordFilterWise = records.Select(x => new EarningLookUpModel
                        {
                            DateIn = x.Key,
                            Date = x.FirstOrDefault().Date,
                            Amount = x.Sum(y => y.Amount)
                        }).ToList();

                        earning = earningRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "Weekly")
                    {
                        var records = earning.GroupBy(x => x.Date.DayOfWeek);
                        var earningRecordFilterWise = records.Select(x => new EarningLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            Amount = x.Sum(y => y.Amount)
                        }).ToList();

                        earning = earningRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "Monthly")
                    {
                        var records = earning.GroupBy(x => x.Date.Date);
                        var earningRecordFilterWise = records.Select(x => new EarningLookUpModel
                        {
                            Date = x.Key,
                            Amount = x.Sum(y => y.Amount)
                        }).ToList();

                        earning = earningRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "6 Monthly")
                    {
                        var records = earning.GroupBy(x => x.Date.Month);
                        var earningRecordFilterWise = records.Select(x => new EarningLookUpModel
                        {
                            Date = x.FirstOrDefault().Date,
                            Amount = x.Sum(y => y.Amount)
                        }).ToList();

                        earning = earningRecordFilterWise;
                    }
                    #endregion

                    #region SalePurcahse Comparision Graph
                    var saleList = new List<SalePurchaseLookUpModel>();
                    var list4 = new List<SalePurchaseLookUpModel>();

                    //foreach (var po in purchase)
                    //{
                    //    var totalAmount = 0;
                    //    if (po.PurchaseInvoiceId == null)
                    //    {
                    //        foreach (var amount in po.PurchasePostItems)
                    //        {

                    //        }
                    //    }

                    //    saleList.Add(new SalePurchaseLookUpModel()
                    //    {
                    //        Date = po.Date,
                    //        IsPurchase = false,
                    //        PurchaseAmount = list2.PurchaseAmount,

                    //    });
                    //}
                    var list1 = (from po in purchase
                                 select new SalePurchaseLookUpModel()
                                 {
                                     Date = po.Date,
                                     IsPurchase = true,
                                     PurchaseAmount = (purchase.Where(x => x.PurchaseInvoiceId == null).Sum(z => z.PurchasePostItems.Sum(item => item.Quantity * item.UnitPrice)) - purchase.Where(x => x.PurchaseInvoiceId == null && (x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل")).Sum(z => z.PurchasePostItems.Sum(item =>

                                     ((item.Quantity * item.UnitPrice) -
                                          (item.Discount == 0
                                                 ? item.Quantity * item.FixDiscount
                                                 : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                            ) *

                                         item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                         )) - (purchase.Where(x => x.PurchaseInvoiceId == null).Sum(z => z.PurchasePostItems.Sum(item =>
                                       (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                           : (item.Quantity * item.UnitPrice * item.Discount) / 100))))) + purchase.Where(x => x.PurchaseInvoiceId == null && (x.TaxMethod == "Inclusive" || x.TaxMethod == "شامل")).Sum(z => z.PurchasePostItems.Sum(item =>
                                         ((item.Quantity * item.UnitPrice) -
                                         (item.Discount == 0
                                                ? item.Quantity * item.FixDiscount
                                                : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                           ) *

                                        item.TaxRate.Rate / (100 + item.TaxRate.Rate))) +
                                     purchase.Where(x => x.PurchaseInvoiceId == null && (x.TaxMethod == "Exclusive" || x.TaxMethod == "غير شامل")).Sum(z => z.PurchasePostItems.Sum(item =>
                                     ((item.Quantity * item.UnitPrice) -
                                        (item.Discount == 0
                                            ? item.Quantity * item.FixDiscount
                                            : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                       ) *
                                    item.TaxRate.Rate / 100))


                                 }
                                         );

                    foreach (var sale in sales)
                    {
                        if (sale.SaleInvoiceId == null)
                        {
                            list4.Add(new SalePurchaseLookUpModel
                            {
                                Date = sale.Date,
                                IsPurchase = false,
                                SaleAmount = Math.Round(sale.SaleItems.Sum(item => item.Quantity * item.UnitPrice) - sale.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                   ? ((item.Quantity * item.UnitPrice) -
                                      ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                                       (item.OfferQuantity == 0
                                           ?
                                           (item.Discount == 0
                                               ? item.Quantity * item.FixDiscount
                                               : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                           : item.Discount == 0
                                               ? item.OfferQuantity * item.FixDiscount
                                               : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                                   item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                   : 0), 2),
                                SaleDiscount = Math.Round(sale.SaleItems.Sum(item => (item.OfferQuantity == 0 ?
                                 (item.Discount == 0 ? item.Quantity * item.FixDiscount
                                     : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                 : item.Discount == 0 ? item.OfferQuantity * item.FixDiscount
                                     : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))),
                                SaleVAT = Math.Round(sale.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                                  ? ((item.Quantity * item.UnitPrice) -
                                     ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                                      (item.OfferQuantity == 0
                                          ?
                                          (item.Discount == 0
                                              ? item.Quantity * item.FixDiscount
                                              : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                          : item.Discount == 0
                                              ? item.OfferQuantity * item.FixDiscount
                                              : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                                  item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                                  : ((item.Quantity * item.UnitPrice) - (item.OfferQuantity == 0
                                      ?
                                      (item.Discount == 0
                                          ? item.Quantity * item.FixDiscount
                                          : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                                      : item.Discount == 0
                                          ? item.OfferQuantity * item.FixDiscount
                                          : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100)) *
                                  item.TaxRate.Rate / 100)) 

                            });

                        }


                    }



                    //      var list4 = sales.Select(x => new SalePurchaseLookUpModel
                    //      {
                    //          Date = x.Date,
                    //          IsPurchase = false,

                    //          SaleAmount  = (sales.Where(x => x.SaleInvoiceId == null).Sum(z => z.SaleItems.Sum(item => item.Quantity * item.UnitPrice)) - sales.Where(x => x.SaleInvoiceId == null).Sum(z => z.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                    //? ((item.Quantity * item.UnitPrice) -
                    //   ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                    //    (item.OfferQuantity == 0
                    //        ?
                    //        (item.Discount == 0
                    //            ? item.Quantity * item.FixDiscount
                    //            : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                    //        : item.Discount == 0
                    //            ? item.OfferQuantity * item.FixDiscount
                    //            : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                    //item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                    //: 0)) - sales.Where(x => x.SaleInvoiceId == null).Sum(z => z.SaleItems.Sum(item => (item.OfferQuantity == 0 ?
                    //     (item.Discount == 0 ? item.Quantity * item.FixDiscount
                    //         : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                    //     : item.Discount == 0 ? item.OfferQuantity * item.FixDiscount
                    //         : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100)))) + sales.Where(x => x.SaleInvoiceId == null).Sum(z => z.SaleItems.Sum(item => (item.TaxMethod == "Inclusive" || item.TaxMethod == "شامل")
                    //    ? ((item.Quantity * item.UnitPrice) -
                    //       ((item.BundleId != null ? item.OfferQuantity * item.UnitPrice : 0) +
                    //        (item.OfferQuantity == 0
                    //            ?
                    //            (item.Discount == 0
                    //                ? item.Quantity * item.FixDiscount
                    //                : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                    //            : item.Discount == 0
                    //                ? item.OfferQuantity * item.FixDiscount
                    //                : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100))) *
                    //    item.TaxRate.Rate / (100 + item.TaxRate.Rate)
                    //    : ((item.Quantity * item.UnitPrice) - (item.OfferQuantity == 0
                    //        ?
                    //        (item.Discount == 0
                    //            ? item.Quantity * item.FixDiscount
                    //            : (item.Quantity * item.UnitPrice * item.Discount) / 100)
                    //        : item.Discount == 0
                    //            ? item.OfferQuantity * item.FixDiscount
                    //            : (item.OfferQuantity * item.UnitPrice * item.Discount) / 100)) *
                    //    item.TaxRate.Rate / 100)),

                    //      }).ToList();




                    var mergeList = list1.Union(list4).OrderBy(x => x.Date);


                    foreach (var y in mergeList)
                    {
                        if (list2.Date == y.Date)
                        {
                            if (list2.IsPurchase)
                            {
                                var index = salePurchase.FindIndex(x => x.Date == y.Date);
                                salePurchase.RemoveAt(index);
                                salePurchase.Add(new SalePurchaseLookUpModel()
                                {
                                    Date = list2.Date,
                                    SaleAmount = (y.SaleAmount-y.SaleDiscount)+y.SaleVAT,
                                    PurchaseAmount = list2.PurchaseAmount,

                                });
                            }


                            else
                            {
                                var index = salePurchase.FindIndex(x => x.Date == y.Date);
                                salePurchase.RemoveAt(index);

                                salePurchase.Add(new SalePurchaseLookUpModel()
                                {
                                    SaleAmount = (y.SaleAmount - y.SaleDiscount) + y.SaleVAT,
                                    Date = list2.Date,
                                    PurchaseAmount = y.PurchaseAmount,
                                });
                            }

                        }
                        else
                        {
                            salePurchase.Add(new SalePurchaseLookUpModel()
                            {
                                SaleAmount = (y.SaleAmount - y.SaleDiscount) + y.SaleVAT,
                                Date = y.Date,
                                PurchaseAmount = y.PurchaseAmount,

                            });
                        }
                        list2 = y;


                    }
                    if (request.OverViewFilter == "Today")
                    {
                        var saleRecord = salePurchase.GroupBy(x => x.Date.Hour);
                        var purchaseSaleRecordFilterWise = saleRecord.Select(x => new SalePurchaseLookUpModel
                        {
                            DateIn = x.Key,
                            Date = x.FirstOrDefault().Date,
                            SaleAmount = x.Sum(y => y.SaleAmount),
                            PurchaseAmount = x.Sum(y => y.PurchaseAmount),
                        }).ToList();

                        salePurchase = purchaseSaleRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "Weekly")
                    {
                        var saleRecord = salePurchase.GroupBy(x => x.Date.DayOfWeek);
                        var purchaseSaleRecordFilterWise = saleRecord.Select(x => new SalePurchaseLookUpModel
                        {
                            Week = x.Key.ToString(),
                            Date = x.FirstOrDefault().Date,
                            SaleAmount = x.Sum(y => y.SaleAmount),
                            PurchaseAmount = x.Sum(y => y.PurchaseAmount),
                        }).ToList();

                        salePurchase = purchaseSaleRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "Monthly")
                    {
                        var saleRecord = salePurchase.GroupBy(x => x.Date.Date);
                        var purchaseSaleRecordFilterWise = saleRecord.Select(x => new SalePurchaseLookUpModel
                        {
                            Date = x.Key,
                            SaleAmount = x.Sum(y => y.SaleAmount),
                            PurchaseAmount = x.Sum(y => y.PurchaseAmount),
                        }).ToList();

                        salePurchase = purchaseSaleRecordFilterWise;
                    }
                    else if (request.OverViewFilter == "6 Monthly")
                    {
                        var saleRecord = salePurchase.GroupBy(x => x.Date.Month);
                        var purchaseSaleRecordFilterWise = saleRecord.Select(x => new SalePurchaseLookUpModel
                        {
                            DateIn = x.Key,
                            Date = x.FirstOrDefault().Date,
                            SaleAmount = x.Sum(y => y.SaleAmount),
                            PurchaseAmount = x.Sum(y => y.PurchaseAmount),
                        }).ToList();

                        salePurchase = purchaseSaleRecordFilterWise;
                    }


                    #endregion




                    return new TransactionLookupModel
                    {

                        Income = income,
                        UnPaidInvoices = unPaidInvoices,
                        PartiallyInvoices = partiallyInvoices,
                        FullyInvoices = fullyInvoices,
                        TotalInvoices = totalInvoices,
                        TotalCreditInvoices = totalCreditInvoices,
                        TotalCashInvoices = totalCashInvoices,
                        TotalReturn = totalReturns,
                        CreditAmount = Credit,
                        Expense = expeneAmount,
                        Purchase = totalPurchases,
                        EarningList = earning,
                        SalePurchaseLookUpModel = salePurchase,
                    };
                }


                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }

            }
        }
    }

}

