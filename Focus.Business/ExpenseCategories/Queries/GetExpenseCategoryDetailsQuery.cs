using Focus.Business.ExpenseCategories.Model;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ExpenseCategories.Queries
{
    public class GetExpenseCategoryDetailsQuery : IRequest<ExpenseCategoryLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetExpenseCategoryDetailsQuery, ExpenseCategoryLookupModel>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetExpenseCategoryDetailsQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<ExpenseCategoryLookupModel> Handle(GetExpenseCategoryDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await Context.ExpenseCategories.Select(x => new ExpenseCategoryLookupModel
                    {
                        Id = x.Id,
                        CategoryName = x.ExpenseCategoryName,
                        Description = x.Description,
                        ExpenseCategoryCode = x.Code, 
                        IsActive = x.IsActive,
                        
                    }).FirstOrDefaultAsync(x => x.Id == request.Id);

                    if (query == null)
                        throw new NotFoundException("ExpenseCategory Not Found", "");


                    return query;
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
