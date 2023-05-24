using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Accounting.Commands.AddUpdateAccount
{
    public class AddUpdateAccountCommand : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid? CostCenterId { get; set; }

        public class Handler : IRequestHandler<AddUpdateAccountCommand, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<AddUpdateAccountCommand> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Guid> Handle(AddUpdateAccountCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Id != Guid.Empty)
                    {
                        var accounts = await _context.Accounts.FindAsync(request.Id);
                        if (accounts == null)
                            throw new NotFoundException(request.Code, request.Id);

                        accounts.Code = request.Code.ToUpper();
                        accounts.Name = request.Name;
                        accounts.NameArabic = request.NameArabic;
                        accounts.Description = request.Description;
                        accounts.IsActive = request.IsActive;
                        accounts.CostCenterId = request.CostCenterId;

                        await _context.SaveChangesAsync(cancellationToken);

                        return accounts.Id;
                    }
                    else
                    {
                        var codeExist = await _context.Accounts.Where(x => x.Code == request.Code).FirstOrDefaultAsync(cancellationToken);
                        if (codeExist != null)
                            throw new ObjectAlreadyExistsException(request.Code);

                        var account = new Account
                        {
                            Code = request.Code.ToUpper(),
                            Name = request.Name,
                            NameArabic = request.NameArabic,
                            Description = request.Description,
                            IsActive = request.IsActive,
                            CostCenterId = request.CostCenterId
                        };

                        await _context.Accounts.AddAsync(account, cancellationToken);
                        _context.SaveChanges();

                        return account.Id;
                    }

                }
                catch (ObjectAlreadyExistsException ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Account is already exist.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}