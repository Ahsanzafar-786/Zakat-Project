using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Extensions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.AdditionalCompany.Commands.AddUpdateAdditionalCompany
{
    public class AddUpdateAdditionalCompanyCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        public string NameArabic { get; set; }
        public string CommercialRegNo { get; set; }
        public string VatRegistrationNo { get; set; }
        public string CityArabic { get; set; }
        public string CountryArabic { get; set; }
        public string Mobile { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string AddressArabic { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CompanyId { get; set; }


        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateAdditionalCompanyCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;
            private readonly IPrincipal _principal;

            //Create logger interface variable for log error
            public readonly ILogger Logger;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateAdditionalCompanyCommand> logger, IPrincipal principal)
            {
                Context = context;
                Logger = logger;
                _principal = principal;
            }

            public async Task<Guid> Handle(AddUpdateAdditionalCompanyCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Id != Guid.Empty)
                    {
                        //Data already exist ; Update data
                        //get data from database using id

                        var additionalCompanyInformations = await Context.companyInformations.FindAsync(request.Id);

                        var isadditionalCompanyNameExist = await Context.companyInformations
                            .Where(x => x.NameArabic == request.NameArabic)
                            .AnyAsync(cancellationToken);

                        if (additionalCompanyInformations == null)
                            throw new NotFoundException(request.NameArabic, request.Id);

                        if (additionalCompanyInformations.NameArabic != request.NameArabic && isadditionalCompanyNameExist)
                            return Guid.Empty;

                        additionalCompanyInformations.NameArabic = request.NameArabic;
                        additionalCompanyInformations.CommercialRegNo = request.CommercialRegNo;
                        additionalCompanyInformations.VatRegistrationNo = request.VatRegistrationNo;
                        additionalCompanyInformations.CityArabic = request.CityArabic;
                        additionalCompanyInformations.CountryArabic = request.CountryArabic;
                        additionalCompanyInformations.Mobile = request.Mobile;
                        additionalCompanyInformations.PhoneNo = request.PhoneNo;
                        additionalCompanyInformations.Email = request.Email;
                        additionalCompanyInformations.Website = request.Website;
                        additionalCompanyInformations.AddressArabic = request.AddressArabic;
                        additionalCompanyInformations.CreatedDate = request.CreatedDate;

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        // Return Additional Company Information id after successfully updating data
                        return additionalCompanyInformations.Id;
                        //Check Additional Company Information exist

                    }
                    else
                    {

                        //Check Additional Company Information name is already exists
                        var isadditionalCompanyNameExist = await Context.companyInformations
                            .Where(x => x.NameArabic == request.NameArabic)
                            .AnyAsync(cancellationToken);

                        if (!isadditionalCompanyNameExist)
                        {
                            // Create a object of Additional Company Information class that made in entity

                           
                                var SadditionalCompanyName = new CompanyInformation
                                {
                                    NameArabic = request.NameArabic,
                                    CommercialRegNo = request.CommercialRegNo,
                                    VatRegistrationNo = request.VatRegistrationNo,
                                    CityArabic = request.CityArabic,
                                    CountryArabic = request.CountryArabic,
                                    Mobile = request.Mobile,
                                    PhoneNo = request.PhoneNo,
                                    Email = request.Email,
                                    Website = request.Website,
                                    AddressArabic = request.AddressArabic,
                                    CreatedDate = DateTime.UtcNow,
                                    CompanyId = _principal.Identity.CompanyId(),
                                };

                                //Add Additional Company Information to database
                                await Context.companyInformations.AddAsync(SadditionalCompanyName, cancellationToken);
                                await Context.SaveChangesAsync(cancellationToken);
                                return SadditionalCompanyName.Id;
                          
                        }

                        return Guid.Empty;
                    }

                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("AdditionalCompany Name Already Exist");
                }
            }
        }
    }
}
