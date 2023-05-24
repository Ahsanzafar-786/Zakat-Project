using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.EmployeeRegistrations.Commands.DeleteBrand
{
    public class DeleteEmployeeRegistrationCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteEmployeeRegistrationCommand, Guid>
        {
            //Create an object of IApplicationDbContext for delete the data from database
            public readonly IApplicationDbContext _context;

            //Create object of logger for all type of message show
            public readonly ILogger _logger;

            //Constructor
            public Handler(IApplicationDbContext context, ILogger<DeleteEmployeeRegistrationCommand> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<Guid> Handle(DeleteEmployeeRegistrationCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    //Get Data from database in specific id which we want to delete
                    var employee = await _context.EmployeeRegistrations.FindAsync(request.Id);

                    //Check data is exist
                    if (employee != null)
                    {
                        _context.EmployeeRegistrations.Remove(employee);
                        await _context.SaveChangesAsync(cancellationToken);
                        return request.Id;
                    }
                    //throw exception which data not exist
                    throw new NotFoundException("Not Found", request.Id);
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
