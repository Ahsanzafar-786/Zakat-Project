using Focus.Business.ExpenseCategories.Model;
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.ExpenseCategories.Commands
{
    public class ExpenseCategoryAddUpdateCommand : IRequest<Message>
    {
        public ExpenseCategoryLookupModel expenseCategories { get; set; }

        public class Handler : IRequestHandler<ExpenseCategoryAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<ExpenseCategoryAddUpdateCommand> logger)
            {
                Context = context;
                Logger = logger;
            }
            public async Task<Message> Handle(ExpenseCategoryAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.expenseCategories.Id == Guid.Empty)
                    {
                        var expenseCate = Context.ExpenseCategories.OrderBy(x => x.Id).LastOrDefault();


                        var cate = new ExpenseCategory
                        {
                            Code = request.expenseCategories.ExpenseCategoryCode,
                            ExpenseCategoryName = request.expenseCategories.CategoryName,
                            Description = request.expenseCategories.Description,
                            IsActive = request.expenseCategories.IsActive,
                        };

                        await Context.ExpenseCategories.AddAsync(cate);
                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = cate.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                    else
                    {
                        var expenseCategoryDetail = await Context.ExpenseCategories.FindAsync(request.expenseCategories.Id);
                        if (expenseCategoryDetail == null)
                            throw new NotFoundException("Authorized Persons Not Found", "");

                        expenseCategoryDetail.Code = request.expenseCategories.ExpenseCategoryCode;
                        expenseCategoryDetail.ExpenseCategoryName = request.expenseCategories.CategoryName;
                        expenseCategoryDetail.Description = request.expenseCategories.Description;
                        expenseCategoryDetail.IsActive = request.expenseCategories.IsActive;

                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = expenseCategoryDetail.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    return new Message
                    {
                        Id = Guid.Empty,
                        IsSuccess = false,
                        IsAddUpdate = exception.Message
                    };
                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    return new Message
                    {
                        Id = Guid.Empty,
                        IsSuccess = false,
                        IsAddUpdate = exception.Message
                    };
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    return new Message
                    {
                        Id = Guid.Empty,
                        IsSuccess = false,
                        IsAddUpdate = exception.Message
                    };
                }
            }
        }
    }
}
