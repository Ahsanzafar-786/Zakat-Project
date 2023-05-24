using System;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Accounting.Commands.RemoveAccount
{
    public class RemoveAccountCommand : IRequest<bool>
    {
        public Guid? Id { get; set; }
        public class Handler : IRequestHandler<RemoveAccountCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<RemoveAccountCommand> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<bool> Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var account = await _context.Accounts.FindAsync(request.Id);
                    if (account == null)
                        throw new NotFoundException(request.Id.ToString(), request.Id);

                    //if (account..Any() || account.Budget.Any())
                    //    throw new NotFoundException(request.Id.ToString(), request.Id);

                    _context.Accounts.Remove(account);
                    await _context.SaveChangesAsync(cancellationToken);

                    return true;
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