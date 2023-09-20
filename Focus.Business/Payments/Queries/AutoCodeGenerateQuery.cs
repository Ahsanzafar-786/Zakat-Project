using Focus.Business.Exceptions;
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

namespace Focus.Business.Payments.Queries
{
    public class AutoCodeGenerateQuery : IRequest<string>
    {
        public string Name { get; set; }
        public class Handler : IRequestHandler<AutoCodeGenerateQuery, string>
        {
            public readonly IApplicationDbContext Context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<AutoCodeGenerateQuery> logger)
            {
                Context = context;
                _logger = logger;
            }
            public async Task<string> Handle(AutoCodeGenerateQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    string code = "";
                    if (request.Name == "Funds")
                    {
                        code = await AutoGenerateFunds();
                    }
                    else if(request.Name == "AuthorizePerson")
                    {
                        code = await AutoGenerateAuthorizePerson();
                    }
                    else if(request.Name == "ApprovalPerson")
                    {
                        code = await AutoGenerateApprovalPerson();
                    }
                    else if(request.Name == "CharityResources")
                    {
                        code = await AutoGenerateCharityResources();
                    }
                    else if (request.Name == "Benificaries")
                    {
                        code = await AutoGenerateBenificaries();
                    }
                    else if (request.Name == "ExpenseCategory")
                    {
                        code = await AutoGenerateExpenseCategory();
                    }
                    else if (request.Name == "Expense")
                    {
                        code = await AutoGenerateExpense();
                    }
                    else
                    {
                        code = await AutoGenerateCashCustomer();
                    }


                    return code;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Something Went Wrong.");
                }
            }
            // Charity Resources Auto ID
            public async Task<string> AutoGenerateBenificaries()
            {
                var authorize = await Context.Beneficiaries
                       .OrderBy(x => x.BeneficiaryId)
                       .LastOrDefaultAsync();

                if (authorize != null)
                {
                    if (authorize.BeneficiaryId == 0)
                    {
                        return GenerateCodeFirstTimeBenificaries();
                    }

                    return GenerateNewCodeBenificaries(authorize.BeneficiaryId);
                }

                return GenerateCodeFirstTimeBenificaries();
            }

             public string GenerateCodeFirstTimeBenificaries()
            {
                int code = 1;
                return code.ToString();
            }
            public string GenerateNewCodeBenificaries(int soNumber)
            {
                Int32 autoNo = Convert.ToInt32((soNumber));
                autoNo++;
                var newCode = autoNo.ToString();
                return newCode;
            }
            
            
            // Charity Resources Auto ID
            public async Task<string> AutoGenerateCharityResources()
            {
                var authorize = await Context.CharityResources
                       .OrderBy(x => x.ChartiyId)
                       .LastOrDefaultAsync();

                if (authorize != null)
                {
                    if (authorize.ChartiyId == 0)
                    {
                        return GenerateCodeFirstTimeCharityResources();
                    }

                    return GenerateNewCodeCharityResources(authorize.ChartiyId);
                }

                return GenerateCodeFirstTimeCharityResources();
            }

             public string GenerateCodeFirstTimeCharityResources()
            {
                int code = 1;
                return code.ToString();
            }
            public string GenerateNewCodeCharityResources(int soNumber)
            {
                Int32 autoNo = Convert.ToInt32((soNumber));
                autoNo++;
                var newCode = autoNo.ToString();
                return newCode;
            } 
            
            // Approval Person Auto ID
                public async Task<string> AutoGenerateApprovalPerson()
            {
                var authorize = await Context.ApprovalPersons
                       .OrderBy(x => x.AprovalPersonId)
                       .LastOrDefaultAsync();

                if (authorize != null)
                {
                    if (authorize.AprovalPersonId == 0)
                    {
                        return GenerateCodeFirstTimeApprovalPerson();
                    }

                    return GenerateNewCodeApprovalPerson(authorize.AprovalPersonId);
                }

                return GenerateCodeFirstTimeAuthorizePerson();
            }

             public string GenerateCodeFirstTimeApprovalPerson()
            {
                int code = 1;
                return code.ToString();
            }
            public string GenerateNewCodeApprovalPerson(int soNumber)
            {
                Int32 autoNo = Convert.ToInt32((soNumber));
                autoNo++;
                var newCode = autoNo.ToString();
                return newCode;
            } 
            
            // Authorize Person Auto ID
                public async Task<string> AutoGenerateAuthorizePerson()
            {
                var authorize = await Context.AuthorizedPersons
                       .OrderBy(x => x.AuthorizedPersonCode)
                       .LastOrDefaultAsync();

                if (authorize != null)
                {
                    if (authorize.AuthorizedPersonCode == 0)
                    {
                        return GenerateCodeFirstTimeAuthorizePerson();
                    }

                    return GenerateNewCodeAuthorizePerson(authorize.AuthorizedPersonCode);
                }

                return GenerateCodeFirstTimeAuthorizePerson();
            }

             public string GenerateCodeFirstTimeAuthorizePerson()
            {
                int code = 1;
                return code.ToString();
            }
            public string GenerateNewCodeAuthorizePerson(int soNumber)
            {
                Int32 autoNo = Convert.ToInt32((soNumber));
                autoNo++;
                var newCode = autoNo.ToString();
                return newCode;
            }

            //Funds Auto Code
            public async Task<string> AutoGenerateFunds()
            {
                var funds = await Context.Funds
                       .OrderBy(x => x.Code)
                       .LastOrDefaultAsync();

                if (funds != null)
                {
                    if (string.IsNullOrEmpty(funds.Code))
                    {
                        return GenerateCodeFirstTimeFunds();
                    }

                    return GenerateNewCodeFunds(funds.Code);
                }

                return GenerateCodeFirstTimeFunds();
            }
            public string GenerateCodeFirstTimeFunds()
            {
                return "FU-00001";
            }
            public string GenerateNewCodeFunds(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "FU-" + autoNo.ToString(format);
                return newCode;
            }


            //expeseCaterory
            public async Task<string> AutoGenerateExpenseCategory()
            {
                var expecat = await Context.ExpenseCategories
                       .OrderBy(x => x.Code)
                       .LastOrDefaultAsync();

                if (expecat != null)
                {
                    if (string.IsNullOrEmpty(expecat.Code))
                    {
                        return GenerateCodeFirstTimeExpenseCategory();
                    }

                    return GenerateNewCodeExpenseCategory(expecat.Code);
                }

                return GenerateCodeFirstTimeExpenseCategory();
            }
            public string GenerateCodeFirstTimeExpenseCategory()
            {
                return "EC-00001";
            }
            public string GenerateNewCodeExpenseCategory(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "EC-" + autoNo.ToString(format);
                return newCode;
            }

            //expese
            public async Task<string> AutoGenerateExpense()
            {
                var expecat = await Context.Expenses
                       .OrderBy(x => x.Code)
                       .LastOrDefaultAsync();

                if (expecat != null)
                {
                    if (string.IsNullOrEmpty(expecat.Code))
                    {
                        return GenerateCodeFirstTimeExpense();
                    }

                    return GenerateNewCodeExpense(expecat.Code);
                }

                return GenerateCodeFirstTimeExpense();
            }
            public string GenerateCodeFirstTimeExpense()
            {
                return "E-00001";
            }
            public string GenerateNewCodeExpense(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "E-" + autoNo.ToString(format);
                return newCode;
            }



            // Payments Auto Code
            public async Task<string> AutoGenerateCashCustomer()
            {
                var payment = await Context.Payments
                       .OrderBy(x => x.Code)
                       .LastOrDefaultAsync();

                if (payment != null)
                {
                    if (payment.Code == 0)
                    {
                        return GenerateCodeFirstTime();
                    }

                    return GenerateNewCode(payment.Code);
                }

                return GenerateCodeFirstTime();
            }
            public string GenerateCodeFirstTime()
            {
                int code = 1;
                return code.ToString();
            }
            public string GenerateNewCode(int soNumber)
            {
                Int32 autoNo = Convert.ToInt32((soNumber));
                autoNo++;
                var newCode = autoNo.ToString();
                return newCode;
            }
        }
    }
}
