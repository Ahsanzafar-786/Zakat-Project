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

            // Payments Auto Code
            public async Task<string> AutoGenerateCashCustomer()
            {
                var payment = await Context.Payments
                       .OrderBy(x => x.PaymentCode)
                       .LastOrDefaultAsync();

                if (payment != null)
                {
                    if (string.IsNullOrEmpty(payment.PaymentCode))
                    {
                        return GenerateCodeFirstTime();
                    }

                    return GenerateNewCode(payment.PaymentCode);
                }

                return GenerateCodeFirstTime();
            }
            public string GenerateCodeFirstTime()
            {
                return "PA-00001";
            }
            public string GenerateNewCode(string soNumber)
            {
                string fetchNo = soNumber.Substring(3);
                Int32 autoNo = Convert.ToInt32((fetchNo));
                var format = "00000";
                autoNo++;
                var newCode = "PA-" + autoNo.ToString(format);
                return newCode;
            }
        }
    }
}
