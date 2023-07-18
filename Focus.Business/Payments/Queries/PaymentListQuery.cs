﻿using Focus.Business.BenificiariesNotes.Model;
using Focus.Business.BenificiariesNotes.Queries;
using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.Payments.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Focus.Domain.Entities;
using Focus.Business.Transactions.Models;

namespace Focus.Business.Payments.Queries
{
    public class PaymentListQuery : PagedRequest, IRequest<PagedResult<List<PaymentLookupModel>>>
    {
        public string Gender { get; set; }
        public string ContactNo { get; set; }
        public string Nationality { get; set; }
        public string UqamaNo { get; set; }
        public string Register { get; set; }
        public string Status { get; set; }
        public string SearchTerm { get; set; }
        public string BeneficiaryName { get; set; }
        public int? Code { get; set; }
        public decimal? Amount { get; set; }
        public int? BenificaryCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? Year { get; set; }
        public Guid? ApprovalPersonId { get; set; }
        public Guid? AuthorizationPersonId { get; set; }
        public class Handler : IRequestHandler<PaymentListQuery, PagedResult<List<PaymentLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PaymentListQuery> logger, UserManager<ApplicationUser> userManager)
            {
                Context = context;
                _logger = logger;
                _userManager = userManager;

            }
            public async Task<PagedResult<List<PaymentLookupModel>>> Handle(PaymentListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var charityTransaction = Context.CharityTransaction.AsNoTracking().Select(x => new CharityTransactionLookupModel
                    {
                        Id = x.Id,
                        DoucmentId = x.DoucmentId,
                        Amount = x.Amount,
                        CharityTransactionDate= x.CharityTransactionDate,
                    }).AsQueryable();

                    var query = Context.Payments.AsNoTracking()
                                .Include(x => x.Beneficiaries).ThenInclude(x => x.BenificaryAuthorization).ThenInclude(x => x.AuthorizedPerson)
                                .Include(x => x.Beneficiaries).ThenInclude(x => x.PaymentTypes)
                                .Include(x => x.Beneficiaries).ThenInclude(x => x.ApprovalPersons)
                                .Select(x => new PaymentLookupModel
                                {
                                    Id = x.Id,
                                    BenificayId = x.BenificayId,
                                    Amount = x.Amount,
                                    Month = x.Month,
                                    Year = x.Year,
                                    Date = x.Date,
                                    Period = x.Period,
                                    PaymentCode = x.PaymentCode,
                                    BenificaryName = x.Beneficiaries.Name,
                                    BenificaryCode = x.Beneficiaries.BeneficiaryId,
                                    BenificaryNameAr = x.Beneficiaries.NameAr,
                                    Code = x.Code,
                                    IsVoid = x.IsVoid,
                                    AllowVoid = x.AllowVoid,
                                    IsRegister = x.Beneficiaries.IsRegister,
                                    Nationality = x.Beneficiaries.Nationality,
                                    UgamaNo = x.Beneficiaries.UgamaNo,
                                    Gender = x.Beneficiaries.Gender,
                                    ContactNo = x.Beneficiaries.PhoneNo,
                                    LastPaymentAmount = charityTransaction.OrderBy(charity => charity.CharityTransactionDate).LastOrDefault(charity => charity.DoucmentId == x.Id).Amount ,
                                    LastPaymentDate = charityTransaction.OrderBy(charity => charity.CharityTransactionDate).LastOrDefault(charity => charity.DoucmentId == x.Id).CharityTransactionDate != null ? charityTransaction.OrderBy(charity => charity.CharityTransactionDate).LastOrDefault(charity => charity.DoucmentId == x.Id).CharityTransactionDate : null ,
                                    ApprovalPersonId = x.Beneficiaries.ApprovedPaymentId,
                                    ApprovalPersonName = x.Beneficiaries.ApprovalPersons.Name,
                                    PaymentType = x.Beneficiaries.PaymentTypes.Name != null ? x.Beneficiaries.PaymentTypes.Name : x.Beneficiaries.PaymentTypes.NameAr,
                                    AuthorizePersonId = x.Beneficiaries.BenificaryAuthorization != null ? x.Beneficiaries.BenificaryAuthorization.FirstOrDefault().AuthorizationPersonId : null,
                                    AuthorizePersonName = x.Beneficiaries.BenificaryAuthorization != null ? x.Beneficiaries.BenificaryAuthorization.FirstOrDefault().AuthorizedPerson.Name : null,
                                    Cashier = _userManager.Users.FirstOrDefault(y => y.Id == x.UserId).FirstName + " " + _userManager.Users.FirstOrDefault(y => y.Id == x.UserId).LastName,
                                }).OrderByDescending(x => x.PaymentCode).AsQueryable();

                    //if (!string.IsNullOrEmpty(request.SearchTerm))
                    //{
                    //    var searchTerm = request.SearchTerm.ToLower();
                    //    query = query.Where(x => x.Amount.ToString().Contains(searchTerm) || x.BenificaryNameAr.Contains(searchTerm) 
                    //                          || x.BenificaryName.Contains(searchTerm) || x.BenificayId.ToString().Contains(searchTerm) || x.Code.ToString().Contains(searchTerm));

                    //}

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm.ToLower();
                        query = query.Where(x => x.BenificaryNameAr.Contains(searchTerm)
                                              || x.BenificaryName.Contains(searchTerm));

                    }
                    if (request.Amount != null && request.Amount > 0)
                    {
                        query = query.Where(x => x.Amount == request.Amount);
                    }
                    if (request.Code != null && request.Code > 0)
                    {
                        query = query.Where(x => x.Code == request.Code);
                    }
                    if (request.BenificaryCode != null && request.BenificaryCode > 0)
                    {
                        query = query.Where(x => x.BenificaryCode == request.BenificaryCode);
                    }
                    if (request.FromDate.HasValue && request.ToDate.HasValue)
                    {
                        query = query.Where(x => x.Date.Value >= request.FromDate.Value && x.Date.Value <= request.ToDate.Value.AddDays(1));
                    }
                    if (request.Month != null)
                    {
                        query = query.Where(x => x.Date.Value.Month == request.Month.Value.Month && x.Date.Value.Year == request.Month.Value.Year);
                    }
                    if (request.Year != null)
                    {
                        query = query.Where(x => x.Date.Value.Year == request.Year.Value.Year);
                    }
                    if(request.Register == "Register")
                    {
                        query = query.Where(x => x.IsRegister);
                    }
                    if (request.Register == "Un-Register")
                    {
                        query = query.Where(x => !x.IsRegister);
                    }
                    if(request.UqamaNo != null)
                    {
                        query = query.Where(x => x.UgamaNo == request.UqamaNo);
                    }
                    if(request.Nationality != null)
                    {
                        query = query.Where(x => x.Nationality == request.Nationality);
                    }
                    if(request.Gender != null)
                    {
                        query = query.Where(x => x.Gender == request.Gender);
                    }
                    if(request.ContactNo != null)
                    {
                        query = query.Where(x => x.ContactNo == request.ContactNo);
                    }
                    if(request.ApprovalPersonId != null)
                    {
                        query = query.Where(x => x.ApprovalPersonId == request.ApprovalPersonId);   
                    }
                    if(request.AuthorizationPersonId != null)
                    {
                        query = query.Where(x => x.AuthorizePersonId == request.AuthorizationPersonId);   
                    }


                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var queryList = await query.ToListAsync();

                    return new PagedResult<List<PaymentLookupModel>>
                    {
                        Results = queryList,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = queryList.Count / request.PageSize
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
