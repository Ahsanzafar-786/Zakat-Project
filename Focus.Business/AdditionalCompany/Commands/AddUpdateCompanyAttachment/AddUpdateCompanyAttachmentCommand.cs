using MediatR;
using System;
using Focus.Business.AdditionalCompany.Queries.GetCompanyAttachments;
using Focus.Business.Interface;
using Microsoft.Extensions.Logging;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Threading;
using Focus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.AdditionalCompany.Commands.AddUpdateCompanyAttachment
{
    public class AddUpdateCompanyAttachmentCommand : IRequest<Guid>
    {
        public CompanyAttachmentLookupModel Attachment { get; set; }

        public class Handler : IRequestHandler<AddUpdateCompanyAttachmentCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;

            //Create logger interface variable for log error
            public readonly ILogger Logger;

            //Constructor
            public Handler(IApplicationDbContext context, ILogger<AddUpdateCompanyAttachmentCommand> logger)
            {
                Context = context;
                Logger = logger;
            }

            public async Task<Guid> Handle(AddUpdateCompanyAttachmentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var company = await Context.Companies
                        .FirstOrDefaultAsync(x => x.Id == request.Attachment.CompanyId, cancellationToken);

                    if (company == null)
                        throw new ApplicationException("Company not found.");

                    var attachment = new CompanyAttachment()
                    {
                        CompanyId = request.Attachment.CompanyId,
                        BusinessLicence = request.Attachment.BusinessLicence,
                        CctvLicence = request.Attachment.CctvLicence,
                        Date = DateTime.UtcNow,
                        CivilDefenceLicense = request.Attachment.CivilDefenceLicense,
                        CommercialRegistration = request.Attachment.CommercialRegistration,
                        Logo = request.Attachment.Logo
                    };

                    company.LogoPath = request.Attachment.Logo;
                    company.CompanyAttachments.Add(attachment);

                    //Save changes to database
                    await Context.SaveChangesAsync(cancellationToken);

                    // Return Additional Company Information id after successfully updating data
                    return attachment.Id;
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
