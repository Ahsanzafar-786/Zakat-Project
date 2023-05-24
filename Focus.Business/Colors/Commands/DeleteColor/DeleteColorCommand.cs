using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.Colors.Commands.DeleteBrand
{
    public class DeleteColorCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteColorCommand, Guid>
        {
            //Create an object of IApplicationDbContext for delete the data from database
            public readonly IApplicationDbContext _context;

            //Create object of logger for all type of message show
            public readonly ILogger _logger;

            //Constructor
            public Handler(IApplicationDbContext context, ILogger<DeleteColorCommand> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<Guid> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    //Get Data from database in specific id which we want to delete
                    var colors = await _context.Colors.FindAsync(request.Id);

                    //Check data is exist
                    if (colors != null)
                    {
                        _context.Colors.Remove(colors);
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
