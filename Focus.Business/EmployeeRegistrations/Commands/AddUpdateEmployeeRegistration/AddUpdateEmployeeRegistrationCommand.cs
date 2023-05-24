using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Domain.Enum;
using Focus.Business.Attachments.Commands;
using Microsoft.AspNetCore.Identity;
using Focus.Business.Users;

namespace Focus.Business.EmployeeRegistrations.Commands.AddUpdateEmployeeRegistration
{
    public class AddUpdateEmployeeRegistrationCommand : IRequest<Guid>
    {
        //Get all variable in entity
        public Guid Id { get; set; }
        //Personal Information
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string FatherName { get; set; }
        public string ArabicName { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Gender { get; set; }
        public string MartialStatus { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MobileNo { get; set; }
        public string OtherContact { get; set; }
        public string Email { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public string Reason { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string IbanNumber { get; set; }
        public string AccountNumber { get; set; }
        public string BranchName { get; set; }
        public string BankAddress { get; set; }
        public string AccountType { get; set; }
        public string EmployeeAccess { get; set; }
        //Reference Contact Information
        public string PrimaryNameOfPerson { get; set; }
        public string PrimaryRelation { get; set; }
        public string PrimaryContactNumber { get; set; }
        public string PrimaryReferenceEmail { get; set; }
        public string SecondaryNameOfPerson { get; set; }
        public string SecondaryRelation { get; set; }
        public string SecondaryContactNumber { get; set; }
        public string SecondaryReferenceEmail { get; set; }

        //Home Contact Information
        public string HomePersonName { get; set; }
        public string HomeRelation { get; set; }
        public string HomeContactNumber { get; set; }
        public string HomeReferenceEmail { get; set; }
        public string HomeCity { get; set; }
        public string HomeCountry { get; set; }

        //Legal Identity Information
        public string IDNumber { get; set; }
        public string IDType { get; set; }
        public string Title { get; set; }
        public string ExpiryDate { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssueDate { get; set; }
        public string PassportExpiryDate { get; set; }
        public string PassportIssuingAuthority { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public string DrivingLicenseType { get; set; }
        public string DrivingExpiryDate { get; set; }
        public string DrivingIssuingAuthority { get; set; }
        public string MedicalPolicNumber { get; set; }
        public string MedicalPolicType { get; set; }
        public string MedicalPolicProvider { get; set; }
        public DateTime? MedicalPolicExpiryDate { get; set; }
        public string CNIC { get; set; }
        public string Photo { get; set; }
        public string DrivingLicense { get; set; }
        public string Passport { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string SalaryType { get; set; }
        public string NationalOrForeign { get; set; }
        public string SpouseName1 { get; set; }
        public string SpouseName2 { get; set; }
        public string SpouseName3 { get; set; }
        public string SpouseName4 { get; set; }
        public bool TemporaryCashReceiver { get; set; }
        public bool TemporaryCashIssuer { get; set; }
        public bool TemporaryCashRequester { get; set; }
        public bool IsSignup { get; set; }
        public decimal Days { get; set; }
        public decimal Limit { get; set; }
        public bool IsActive { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public decimal? PerDayWorkHour { get; set; }


        //Create Handler class that inherit from IRequestHandler interface

        public class Handler : IRequestHandler<AddUpdateEmployeeRegistrationCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext Context;
            private readonly UserManager<ApplicationUser> _userManager;

            //Create logger interface variable for log error
            public readonly ILogger Logger;

            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateEmployeeRegistrationCommand> logger, UserManager<ApplicationUser> userManager)
            {
                Context = context;
                Logger = logger;
                _userManager = userManager;
            }

            public async Task<Guid> Handle(AddUpdateEmployeeRegistrationCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.Id != Guid.Empty)
                    {
                        //Data already exist ; Update data
                        //get data from database using id

                        var employee = await Context.EmployeeRegistrations.FindAsync(request.Id);
                        if (employee == null)
                            throw new NotFoundException(request.EnglishName, request.Id);


                        if (request.EnglishName != "" && request.ArabicName != "")
                        {
                            //bool isEmployeeNameExist = await Context.EmployeeRegistrations
                            //    .AnyAsync(x => x.EnglishName == request.EnglishName || x.ArabicName == request.ArabicName, cancellationToken: cancellationToken);
                            //if (employee.EnglishName != request.EnglishName && isEmployeeNameExist)
                            //    throw new ObjectAlreadyExistsException("Employee Name " + request.EnglishName + " Already Exist");
                            //if (employee.ArabicName != request.ArabicName && isEmployeeNameExist)
                            //    throw new ObjectAlreadyExistsException("Employee Name " + request.ArabicName + " Already Exist");
                        }
                        else if (request.EnglishName != "")
                        {
                            bool isEmployeeNameExist = await Context.EmployeeRegistrations
                                .AnyAsync(x => x.EnglishName == request.EnglishName && x.Id != employee.Id, cancellationToken: cancellationToken);
                            if (isEmployeeNameExist)
                                throw new ObjectAlreadyExistsException("Employee Name " + request.EnglishName + " Already Exist");
                        }
                        else
                        {
                            bool isEmployeeNameExist = await Context.EmployeeRegistrations
                                .AnyAsync(x => x.ArabicName == request.ArabicName && x.Id != employee.Id, cancellationToken: cancellationToken);
                            if (isEmployeeNameExist)
                                throw new ObjectAlreadyExistsException("Employee Name " + request.ArabicName + " Already Exist");
                        }

                        employee.Code = request.Code;
                        employee.EnglishName = request.EnglishName;
                        employee.FatherName = request.FatherName;
                        employee.ArabicName = request.ArabicName;
                        employee.RegistrationDate = request.RegistrationDate;
                        employee.Gender = request.Gender;
                        employee.SalaryType = request.SalaryType;
                        employee.MartialStatus = request.MartialStatus;
                        employee.Nationality = request.Nationality;
                        employee.DateOfBirth = request.DateOfBirth;
                        employee.MobileNo = request.MobileNo;
                        employee.OtherContact = request.OtherContact;
                        employee.Email = request.Email;
                        employee.BloodGroup = request.BloodGroup;
                        employee.Address = request.Address;
                        employee.City = request.City;
                        employee.Country = request.Country;
                        employee.PrimaryNameOfPerson = request.PrimaryNameOfPerson;
                        employee.PrimaryRelation = request.PrimaryRelation;
                        employee.PrimaryContactNumber = request.PrimaryContactNumber;
                        employee.PrimaryReferenceEmail = request.PrimaryReferenceEmail;
                        employee.SecondaryNameOfPerson = request.SecondaryNameOfPerson;
                        employee.SecondaryRelation = request.SecondaryRelation;
                        employee.SecondaryContactNumber = request.SecondaryContactNumber;
                        employee.SecondaryReferenceEmail = request.SecondaryReferenceEmail;
                        employee.HomePersonName = request.HomePersonName;
                        employee.HomeRelation = request.HomeRelation;
                        employee.HomeContactNumber = request.HomeContactNumber;
                        employee.HomeReferenceEmail = request.HomeReferenceEmail;
                        employee.HomeCity = request.HomeCity;
                        employee.HomeCountry = request.HomeCountry;
                        employee.IDNumber = request.IDNumber;
                        employee.IDType = request.IDType;
                        employee.Title = request.Title;
                        employee.ExpiryDate = request.ExpiryDate;
                        employee.PassportNumber = request.PassportNumber;
                        employee.PassportIssueDate = request.PassportIssueDate;
                        employee.PassportExpiryDate = request.PassportExpiryDate;
                        employee.PassportIssuingAuthority = request.PassportIssuingAuthority;
                        employee.DrivingLicenseNumber = request.DrivingLicenseNumber;
                        employee.DrivingLicenseType = request.DrivingLicenseType;
                        employee.DrivingExpiryDate = request.DrivingExpiryDate;
                        employee.DrivingIssuingAuthority = request.DrivingIssuingAuthority;
                        employee.MedicalPolicNumber = request.MedicalPolicNumber;
                        employee.MedicalPolicType = request.MedicalPolicType;
                        employee.MedicalPolicProvider = request.MedicalPolicProvider;
                        employee.MedicalPolicExpiryDate = request.MedicalPolicExpiryDate;
                        employee.DesignationId = request.DesignationId;
                        employee.DepartmentId = request.DepartmentId;
                        employee.NationalOrForeign = request.NationalOrForeign;
                        employee.SpouseName1 = request.SpouseName1;
                        employee.SpouseName2 = request.SpouseName2;
                        employee.SpouseName3 = request.SpouseName3;
                        employee.SpouseName4 = request.SpouseName4;
                        employee.EmployeeType = request.EmployeeType;
                        employee.PerDayWorkHour = request.PerDayWorkHour;

                        employee.TemporaryCashRequester = request.TemporaryCashRequester;
                        employee.TemporaryCashIssuer = request.TemporaryCashIssuer;
                        employee.TemporaryCashReceiver = request.TemporaryCashReceiver;
                        employee.Days = request.Days;
                        employee.Limit = request.Limit;
                        employee.Reason = request.Reason;
                        employee.BankName = request.BankName;
                        employee.AccountName = request.AccountName;
                        employee.IbanNumber = request.IbanNumber;
                        employee.AccountNumber = request.AccountNumber;
                        employee.BranchName = request.BranchName;
                        employee.BankAddress = request.BankAddress;
                        employee.AccountType = request.AccountType;
                        employee.EmployeeAccess = request.EmployeeAccess;
                        employee.IsActive = request.IsActive;

                        


            //var employeeRegistrationAttachment = new EmployeeRegistrationAttachment()
            //{
            //    EmployeeId = employee.Id,
            //    CNIC = request.CNIC,
            //    Photo = request.Photo,
            //    DrivingLicense = request.DrivingLicense,
            //    Date = DateTime.UtcNow,
            //    Passport = request.Passport,
            //};

            //await Context.EmployeeRegistrationAttachments.AddAsync(employeeRegistrationAttachment, cancellationToken);

            var attachments = Context.Attachments
                            .AsNoTracking()
                            .Where(x => x.DocumentId == employee.Id)
                            .AsQueryable();

                        if (attachments.Any())
                        {
                            Context.Attachments.RemoveRange(attachments);
                        }
                        if (request.AttachmentList != null && request.AttachmentList.Count > 0)
                        {
                            foreach (var attachment in request.AttachmentList.Select(item => new Attachment()
                            {
                                Date = item.Date,
                                DocumentId = employee.Id,
                                Description = item.Description,
                                FileName = item.FileName,
                                Path = item.Path
                            }))
                            {
                                //Add Attachments to database
                                await Context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }

                        //Save changes to database
                        await Context.SaveChangesAsync(cancellationToken);

                        // Return Employee id after successfully updating data
                        return employee.Id;
                        //Check Employee exist

                    }
                    else
                    {
                        //New User Added
                        //Check Employee name is already exists
                        if (request.EnglishName != "" && request.ArabicName != "")
                        {
                            var isEmployeeNameExist = await Context.EmployeeRegistrations.FirstOrDefaultAsync(x => x.EnglishName == request.EnglishName || x.ArabicName == request.ArabicName, cancellationToken);
                            if (isEmployeeNameExist != null)
                                throw new ObjectAlreadyExistsException("Employee Name " + request.EnglishName + " " + request.ArabicName + " Already Exist");
                        }
                        else if (request.EnglishName != "")
                        {
                            var isEmployeeNameExist = await Context.EmployeeRegistrations.FirstOrDefaultAsync(x => x.EnglishName == request.EnglishName, cancellationToken);
                            if (isEmployeeNameExist != null)
                                throw new ObjectAlreadyExistsException("Employee Name " + request.EnglishName + " Already Exist");
                        }
                        else
                        {
                            var isEmployeeNameExist = await Context.EmployeeRegistrations.FirstOrDefaultAsync(x => x.ArabicName == request.ArabicName, cancellationToken);
                            if (isEmployeeNameExist != null)
                                throw new ObjectAlreadyExistsException("Employee Name " + request.ArabicName + " Already Exist");
                        }


                        var accounts = await Context.Accounts
                                .AsNoTracking()
                                .Include(x => x.CostCenter)
                                .Where(x => x.CostCenter.Code == "126000" ||
                                            x.CostCenter.Code == "240000").ToListAsync(cancellationToken);

                        var accEmp = accounts.OrderBy(x => x.Code).LastOrDefault(x => x.CostCenter.Code == "126000");
                        var accPay = accounts.OrderBy(x => x.Code).LastOrDefault(x => x.CostCenter.Code == "240000");

                        if (accEmp == null || accPay == null)
                            throw new ApplicationException("COA is not created yet.");


                        var accountList = new List<Account>()
                            {
                                new Account()
                                {
                                    IsActive = true,
                                    Name = request.EnglishName+(request.EmployeeType == EmployeeType.Contractor?"  Contractor Account":" Employee Account"),
                                    Code = (Convert.ToInt64(accEmp.Code) + 1).ToString(),
                                    CostCenterId = accEmp.CostCenterId,
                                },

                                new Account()
                                {
                                    IsActive = true,
                                    Name = request.EnglishName+ " Employee Payable Account",
                                    Code = (Convert.ToInt64(accPay.Code) + 1).ToString(),
                                    CostCenterId = accPay.CostCenterId,
                                }
                            };
                        await Context.Accounts.AddRangeAsync(accountList, cancellationToken);

                        // Create a object of Department class that made in entity

                        var employee = new EmployeeRegistration
                        {
                            Id = request.Id,
                            Code = request.Code,
                            EnglishName = request.EnglishName,
                            ArabicName = request.ArabicName,
                            FatherName = request.FatherName,
                            RegistrationDate = request.RegistrationDate,
                            Gender = request.Gender,
                            SalaryType = request.SalaryType,
                            MartialStatus = request.MartialStatus,
                            Nationality = request.Nationality,
                            DateOfBirth = request.DateOfBirth,
                            MobileNo = request.MobileNo,
                            IsActive = request.IsActive,
                            OtherContact = request.OtherContact,
                            Email = request.Email,
                            BloodGroup = request.BloodGroup,
                            Address = request.Address,
                            City = request.City,
                            Country = request.Country,
                            PrimaryNameOfPerson = request.PrimaryNameOfPerson,
                            PrimaryRelation = request.PrimaryRelation,
                            PrimaryContactNumber = request.PrimaryContactNumber,
                            PrimaryReferenceEmail = request.PrimaryReferenceEmail,
                            SecondaryNameOfPerson = request.SecondaryNameOfPerson,
                            SecondaryRelation = request.SecondaryRelation,
                            SecondaryContactNumber = request.SecondaryContactNumber,
                            SecondaryReferenceEmail = request.SecondaryReferenceEmail,
                            HomePersonName = request.HomePersonName,
                            HomeRelation = request.HomeRelation,
                            HomeContactNumber = request.HomeContactNumber,
                            HomeReferenceEmail = request.HomeReferenceEmail,
                            HomeCity = request.HomeCity,
                            HomeCountry = request.HomeCountry,
                            IDNumber = request.IDNumber,
                            IDType = request.IDType,
                            Title = request.Title,
                            ExpiryDate = request.ExpiryDate,
                            PassportNumber = request.PassportNumber,
                            PassportIssueDate = request.PassportIssueDate,
                            PassportExpiryDate = request.PassportExpiryDate,
                            PassportIssuingAuthority = request.PassportIssuingAuthority,
                            DrivingLicenseNumber = request.DrivingLicenseNumber,
                            DrivingLicenseType = request.DrivingLicenseType,
                            DrivingExpiryDate = request.DrivingExpiryDate,
                            DrivingIssuingAuthority = request.DrivingIssuingAuthority,
                            MedicalPolicNumber = request.MedicalPolicNumber,
                            MedicalPolicType = request.MedicalPolicType,
                            MedicalPolicProvider = request.MedicalPolicProvider,
                            MedicalPolicExpiryDate = request.MedicalPolicExpiryDate,
                            DepartmentId = request.DepartmentId,
                            DesignationId = request.DesignationId,
                            EmployeeType = request.EmployeeType,
                            EmployeeAccountId = accountList[0].Id,
                            PayableAccountId = accountList[1].Id,
                            NationalOrForeign = request.NationalOrForeign,
                            SpouseName1 = request.SpouseName1,
                            SpouseName2 = request.SpouseName2,
                            SpouseName3 = request.SpouseName3,
                            SpouseName4 = request.SpouseName4,
                            PerDayWorkHour = request.PerDayWorkHour,

                            TemporaryCashRequester = request.TemporaryCashRequester,
                            TemporaryCashIssuer = request.TemporaryCashIssuer,
                            TemporaryCashReceiver = request.TemporaryCashReceiver,
                            Days = request.Days,
                            Limit = request.Limit,
                            Reason = request.Reason,
                            BankName = request.BankName,
                            AccountName = request.AccountName,
                            IbanNumber = request.IbanNumber,
                            AccountNumber = request.AccountNumber,
                            BranchName = request.BranchName,
                            BankAddress = request.BankAddress,
                            AccountType = request.AccountType,
                            EmployeeAccess = request.EmployeeAccess,
                    };

                        //Add Department to database
                        await Context.EmployeeRegistrations.AddAsync(employee, cancellationToken);

                        if (request.IsSignup)
                        {
                            var loginUser = await _userManager.FindByEmailAsync(request.Email);
                            loginUser.EmployeeId = employee.Id;

                            await _userManager.UpdateAsync(loginUser);
                        }


                        //var employeeRegistrationAttachment = new EmployeeRegistrationAttachment()
                        //{
                        //    CNIC = request.CNIC,
                        //    Photo = request.Photo,
                        //    DrivingLicense = request.DrivingLicense,
                        //    Date = DateTime.UtcNow,
                        //    Passport = request.Passport,
                        //    EmployeeId = employee.Id
                        //};

                        //await Context.EmployeeRegistrationAttachments.AddAsync(employeeRegistrationAttachment, cancellationToken);


                        //Add Attachment
                        if (request.AttachmentList != null && request.AttachmentList.Count > 0)
                        {
                            foreach (var attachment in request.AttachmentList.Select(item => new Attachment()
                            {
                                Date = item.Date,
                                DocumentId = employee.Id,
                                Description = item.Description,
                                FileName = item.FileName,
                                Path = item.Path
                            }))
                            {
                                //Add Attachments to database
                                await Context.Attachments.AddAsync(attachment, cancellationToken);
                            }
                        }


                        await Context.SaveChangesAsync(cancellationToken);
                        return employee.Id;

                    }

                }
                catch (ObjectAlreadyExistsException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ObjectAlreadyExistsException(exception.Message);
                }
                catch (NotFoundException exception)
                {
                    Logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception.Message);
                    throw new ApplicationException("Something Went Wrong!");
                }
            }
        }
    }
}
