using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.PaymentsType.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Focus.Domain.Entities;

namespace Focus.Business.PaymentsType.Queries
{
    public class PayementTypeListQuery : PagedRequest, IRequest<PagedResult<List<PaymentTypeLookupModel>>>
    {
        public bool IsDropDown { get; set; }
        public string SearchTerm { get; set; }

        public class Handler : IRequestHandler<PayementTypeListQuery, PagedResult<List<PaymentTypeLookupModel>>>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PayementTypeListQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<PaymentTypeLookupModel>>> Handle(PayementTypeListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsDropDown)
                    {
                        var data = Context.PaymentTypes.Count();
                        if (data == 0)
                        {
                            var payment = new List<PaymentType>();

                            payment.Add(new PaymentType
                            {
                                Name = "One Time",
                                Code = 0,
                                NameAr = "مره واحده",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Name = "1 Month",
                                NameAr = "1 شهر",
                                Code = 1,
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 2,
                                Name = "2 Month",
                                NameAr = "2 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 3,
                                Name = "3 Month",
                                NameAr = "3 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 4,
                                Name = "4 Month",
                                NameAr = "4 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 5,
                                Name = "5 Month",
                                NameAr = "5 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 6,
                                Name = "6 Month",
                                NameAr = "6 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 7,
                                Name = "7 Month",
                                NameAr = "7 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 8,
                                Name = "8 Month",
                                NameAr = "8 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 9,
                                Name = "9 Month",
                                NameAr = "9 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 10,
                                Name = "10 Month",
                                NameAr = "10 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 11,
                                Name = "11 Month",
                                NameAr = "11 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 12,
                                Name = "12 Month",
                                NameAr = "12 شهر",
                                IsActive = true,
                            });

                            Context.PaymentTypes.AddRange(payment);
                            Context.SaveChanges();
                        }

                        var query = await Context.PaymentTypes.AsNoTracking().OrderBy(x => x.Code).Select(x => new PaymentTypeLookupModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            NameAr = x.NameAr,
                        }).ToListAsync();

                        return new PagedResult<List<PaymentTypeLookupModel>>
                        {
                            Results = query
                        };
                    }
                    else
                    {
                        var data = Context.PaymentTypes.Count();
                        if(data == 0)
                        {
                            var payment = new List<PaymentType>();

                            payment.Add(new PaymentType
                            {
                                Name = "One Time",
                                Code = 0,
                                NameAr = "مره واحده",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Name = "1 Month",
                                NameAr = "1 شهر",
                                Code = 1,
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 2,
                                Name = "2 Month",
                                NameAr = "2 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 3,
                                Name = "3 Month",
                                NameAr = "3 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 4,
                                Name = "4 Month",
                                NameAr = "4 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 5,
                                Name = "5 Month",
                                NameAr = "5 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 6,
                                Name = "6 Month",
                                NameAr = "6 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 7,
                                Name = "7 Month",
                                NameAr = "7 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 8,
                                Name = "8 Month",
                                NameAr = "8 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 9,
                                Name = "9 Month",
                                NameAr = "9 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 10,
                                Name = "10 Month",
                                NameAr = "10 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 11,
                                Name = "11 Month",
                                NameAr = "11 شهر",
                                IsActive = true,
                            });
                            payment.Add(new PaymentType
                            {
                                Code = 12,
                                Name = "12 Month",
                                NameAr = "12 شهر",
                                IsActive = true,
                            });

                            Context.PaymentTypes.AddRange(payment);
                            Context.SaveChanges();
                        }

                        var query = Context.PaymentTypes.AsNoTracking().OrderBy(x => x.Code).Select(x => new PaymentTypeLookupModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            NameAr = x.NameAr,
                            Description = x.Description,
                            IsActive = x.IsActive,
                        }).AsQueryable();

                        if (!string.IsNullOrEmpty(request.SearchTerm))
                        {
                            var searchTerm = request.SearchTerm.ToLower();
                            query = query.Where(x => x.NameAr.ToLower().Contains(searchTerm)
                                                  );
                        }

                        var count = await query.CountAsync();
                        query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                        var queryList = await query.ToListAsync();

                        return new PagedResult<List<PaymentTypeLookupModel>>
                        {
                            Results = queryList,
                            RowCount = count,
                            PageSize = request.PageSize,
                            CurrentPage = request.PageNumber,
                            PageCount = queryList.Count / request.PageSize
                        };
                    }
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
