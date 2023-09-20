
using Focus.Business.Common;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using Focus.Business.Exepenses.Models;
using Focus.Domain.Entities;
using NPOI.POIFS.Properties;
using System.Linq;

namespace Focus.Business.Exepenses.Commands
{
    public class ExpenseAddUpdateCommand : IRequest<Message>
    {
        public ExpenseLookupModel expense { get; set; }
        public class Handler : IRequestHandler<ExpenseAddUpdateCommand, Message>
        {
            public readonly IApplicationDbContext Context;
            public readonly ILogger Logger;

            public Handler(IApplicationDbContext context, ILogger<ExpenseAddUpdateCommand> logger)
            {
                Context = context;
                Logger = logger;
            }
            public async Task<Message> Handle(ExpenseAddUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.expense.Id == Guid.Empty)
                    {
                        var expense = new Expense
                        {
                            Id = request.expense.Id,
                            Code = request.expense.Code,
                            Description = request.expense.Description,
                            Amount = request.expense.Amount,
                            Date = DateTime.Now,
                            ExpenseCategoryId=request.expense.ExpenseCategoryId

                        };

                        await Context.Expenses.AddAsync(expense);

                        var charityTransaction = new CharityTransaction
                        {
                            DoucmentId = expense.Id,
                            CharityTransactionDate = expense.Date,
                            DoucmentDate = DateTime.Now,
                            DoucmentCode = expense.Code,
                            Amount = expense.Amount,
                            DocumentName="Expense",
                        };

                        await Context.CharityTransaction.AddAsync(charityTransaction);

                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = expense.Id,
                            IsSuccess = true,
                            IsAddUpdate = "Data has been Added successfully"
                        };
                    }
                    else
                    {
                        var expense = await Context.Expenses.FindAsync(request.expense.Id);
                        if (expense == null)
                            throw new NotFoundException("Funds Not Found", "");

                        expense.Code = request.expense.Code;
                        expense.Description = request.expense.Description;
                        expense.Amount = request.expense.Amount;
                        expense.Date = DateTime.Now;
                        expense.ExpenseCategoryId = request.expense.ExpenseCategoryId;

                        var charitytransaction =  Context.CharityTransaction.FirstOrDefault(x=> x.DoucmentId == request.expense.Id);
                        if (expense == null)
                            throw new NotFoundException("Funds Not Found", "");

                        charitytransaction.DoucmentCode = request.expense.Code;
                        charitytransaction.Amount = request.expense.Amount;
                        charitytransaction.CharityTransactionDate = DateTime.Now;
                        charitytransaction.DoucmentDate = DateTime.Now;
                       



                        await Context.SaveChangesAsync();

                        return new Message
                        {
                            Id = expense.Id,
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
