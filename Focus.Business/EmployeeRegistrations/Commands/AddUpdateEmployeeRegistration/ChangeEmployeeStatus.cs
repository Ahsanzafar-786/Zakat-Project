using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.Users;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Focus.Business.EmployeeRegistrations.Commands.AddUpdateEmployeeRegistration
{
    public class ChangeEmployeeStatus : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsUser { get; set; }

        public class Handler : IRequestHandler<ChangeEmployeeStatus, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;

            //Create logger interface variable for log error
            public readonly ILogger Logger;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<ChangeEmployeeStatus> logger, UserManager<ApplicationUser> userManager)
            {
                Context = context;
                Logger = logger;
                _userManager = userManager;
            }

            public async Task<Guid> Handle(ChangeEmployeeStatus request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Id!=Guid.Empty)
                    {
                       


                        var employee =  Context.EmployeeRegistrations.FirstOrDefault(x => x.Id == request.Id);
                        if(employee!=null)
                        {
                            employee.IsActive = request.IsActive;
                            Context.EmployeeRegistrations.Update(employee);


                            var payAbleAccount = Context.LedgerAccounts.Where(x => x.AccountLedgerId == employee.Id).ToList();

                            foreach (var account in payAbleAccount)
                            {
                                account.IsActive = request.IsActive;
                            }

                        }

                        var userData = await _userManager.FindByEmailAsync(employee.Email);
                        if (userData != null)
                        {
                            userData.IsActive = request.IsActive;
                            await _userManager.UpdateAsync(userData);
                        }

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        // Return Employee id after successfully updating data
                        return employee.Id;

                    }

                    return Guid.Empty;

                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Employee Name Already Exist");
                }
            }
        }

    }
}
