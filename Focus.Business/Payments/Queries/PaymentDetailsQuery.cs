﻿using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Payments.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using Focus.Domain.Entities;
using System.Collections.Generic;
using NPOI.POIFS.Properties;

namespace Focus.Business.Payments.Queries
{
    public class PaymentDetailsQuery : IRequest<PaymentLookupModel>
    {
        public Guid Id { get; set; }
        public bool IsVoid { get; set; }
        public bool AllowVoid { get; set; }


        public class Handler : IRequestHandler<PaymentDetailsQuery, PaymentLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentDetailsQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PaymentLookupModel> Handle(PaymentDetailsQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    if (request.IsVoid)
                    {
                        var query = await Context.Payments.Select(x => new Payment
                        {
                            Id = x.Id,
                            BenificayId = x.BenificayId,
                            Amount = x.Amount,
                            Month = x.Month,
                            Date = x.Date,
                            PaymentCode = x.PaymentCode,
                            Year = x.Year,
                            Period = x.Period,
                            UserId = x.UserId,
                            SelectedMonth= x.SelectedMonth,
                            Note = x.Note,
                            IsVoid = x.IsVoid,
                        }).FirstOrDefaultAsync(x => x.Id == request.Id);

                        if (query == null)
                            throw new NotFoundException("Benificary Note Not Found", "");

                        query.IsVoid = request.IsVoid;

                        Context.Payments.Update(query);

                        var charity = await Context.CharityTransaction.Where(x => x.DoucmentId == request.Id).ToListAsync();

                        Context.CharityTransaction.RemoveRange(charity);

                        var charitTransaction = new List<CharityTransaction>();

                        foreach (var item in query.SelectedMonth)
                        {
                            charitTransaction.Add(new CharityTransaction()
                            {
                                DoucmentId = query.Id,
                                CharityTransactionDate = query.Date,
                                DoucmentDate = DateTime.Now,
                                DoucmentCode = query.PaymentCode,
                                BenificayId = query.BenificayId,
                                Month = item.SelectMonth,
                                Amount = query.Amount,
                                Year = query.Year,
                                IsVoid = true
                            });
                        }


                        await Context.CharityTransaction.AddRangeAsync(charitTransaction);

                        await Context.SaveChangesAsync();

                        return null;
                    }
                    if(request.AllowVoid)
                    {
                        var query = await Context.Payments.Select(x => new Payment
                        {
                            Id = x.Id,
                            BenificayId = x.BenificayId,
                            Amount = x.Amount,
                            Month = x.Month,
                            Date = x.Date,
                            PaymentCode = x.PaymentCode,
                            Year = x.Year,
                            Period = x.Period,
                            UserId = x.UserId,
                            SelectedMonth = x.SelectedMonth,
                            Note = x.Note,
                            IsVoid = x.IsVoid,
                        }).FirstOrDefaultAsync(x => x.Id == request.Id);

                        if (query == null)
                            throw new NotFoundException("Benificary Note Not Found", "");

                        query.AllowVoid = request.AllowVoid;

                        Context.Payments.Update(query);

                        await Context.SaveChangesAsync();
                        
                        return null;
                    }
                    else
                    {
                        var query = await Context.Payments.Select(x => new PaymentLookupModel
                        {
                            Id = x.Id,
                            BenificayId = x.BenificayId,
                            Amount = x.Amount,
                            BenificaryName = x.Beneficiaries.Name,
                            Month = x.Month,
                            PaymentCode = x.PaymentCode,
                            Year = x.Year,
                            Period = x.Period,
                            Note = x.Note,
                            IsVoid = x.IsVoid,
                            AllowVoid = x.AllowVoid,
                        }).FirstOrDefaultAsync(x => x.Id == request.Id);

                        if (query == null)
                            throw new NotFoundException("Benificary Note Not Found", "");

                        return query;
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Something Went Wrong.");
                }
            }
        }
    }
}
