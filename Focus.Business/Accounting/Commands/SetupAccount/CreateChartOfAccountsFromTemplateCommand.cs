using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Domain.Entities;
using Focus.Domain.Enums;


namespace Focus.Business.Accounting.Commands.SetupAccount
{
    public class CreateChartOfAccountsFromTemplateCommand : IRequest<Guid>
    {
        public TemplateType TemplateType;
        public class Handler : IRequestHandler<CreateChartOfAccountsFromTemplateCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger<CreateChartOfAccountsFromTemplateCommand> _logger;
            private readonly IMediator _mediator;

            public Handler(IApplicationDbContext context, ILogger<CreateChartOfAccountsFromTemplateCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(CreateChartOfAccountsFromTemplateCommand request,
                CancellationToken cancellationToken)
            {
                try
                {
                    if (!_context.Accounts.Any())
                    {
                        var templateObject = await _context.AccountTemplates.Where(x => x.Type == "Business").FirstOrDefaultAsync(cancellationToken);
                        var template = JsonConvert.DeserializeObject<AccountTemplateDto>(templateObject.JsonTemplate);
                        var accountTypes = await _context.AccountTypes.ToListAsync(cancellationToken);

                        foreach (var item in template.AccountsType)
                        {
                            var accountType = new AccountType
                            {
                                Name = item.Name,
                                NameArabic = item.NameArabic,
                                IsActive = true
                            };

                            _context.AccountTypes.Add(accountType);
                            //var checkAccountType = accountTypes.FirstOrDefault(x => x.Name == item.Name);
                            //if (checkAccountType == null)
                            //    throw new ApplicationException("Invalid template. Please check your Account Types");

                            foreach (var costCenter in item.CostCenters)
                            {
                                var cost = new CostCenter
                                {
                                    Name = costCenter.Name,
                                    NameArabic = costCenter.NameArabic,
                                    Description = costCenter.Description,
                                    Code = costCenter.Code,
                                    IsActive = true,
                                    AccountTypeId = accountType.Id
                                };

                                await _context.CostCenters.AddAsync(cost, cancellationToken);

                                foreach (var account in costCenter.Accounts)
                                {
                                    var ac = new Account
                                    {
                                        Name = account.Name,
                                        NameArabic = account.NameArabic,
                                        Description = account.Description,
                                        Code = account.Code,
                                        IsActive = true,
                                        CostCenterId = cost.Id
                                    };
                                    await _context.Accounts.AddAsync(ac, cancellationToken);
                                }
                            }
                        }
                        await _context.SaveChangesAsync(cancellationToken);
                        return templateObject.Id;

                    }
                    else
                    {
                        return Guid.Empty;

                    }


                    //Set up Tax Code


                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }


            }
        }


    }



}
