using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.Extensions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.Components
{
    public class CompanyComponent : ICompanyComponent
    {
        private readonly IApplicationDbContext _context;
        private readonly IPrincipal _principal;
        private readonly UserManager<ApplicationUser> _userManager;
        public CompanyComponent(IApplicationDbContext context,
            IPrincipal principal,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _principal = principal;
      
            _userManager = userManager;
        }


        //Get company Data

        public bool VatRegistrationNumberExists(string vrn)
        {
            var data = _context.Companies.FirstOrDefault(o => o.VatRegistrationNo == vrn);
            return data == null;
        }
        public List<CompanyDto> GetCompaniesList(Guid id)
        {
            var companies = new List<Company>();

            var companyId = id == Guid.Empty ? _principal.Identity.CompanyId() : id;
            var companyInfo = _context.Companies.AsNoTracking().FirstOrDefault(x=>x.Id==companyId);
            if (companyInfo.NameEnglish == "noble@gmail.com")

            {
                companies = _context.Companies.AsNoTracking().Where(x => x.ParentId == companyInfo.Id && x.ClientParentId == null)
                    .ToList();
            }
            else if ((companyInfo?.ParentId != Guid.Empty || companyInfo?.ParentId != null)
                    && companyInfo.ClientParentId == null && companyInfo.BusinessParentId == null)
            {
                companies = _context.Companies.AsNoTracking().Where(x => x.ParentId == companyInfo.ParentId && x.ClientParentId == companyInfo.Id).ToList();
            }
            else if ((companyInfo?.ParentId != Guid.Empty || companyInfo?.ParentId != null)
                     && companyInfo.ClientParentId != null && companyInfo.BusinessParentId == null)
            {
                companies = _context.Companies.AsNoTracking().Where(x => x.ClientParentId == companyInfo.ClientParentId && x.BusinessParentId == companyInfo.Id).ToList();
            }

            //var companies = _context.Companies.Where(x => x.ParentId == id).ToList();

            var companyList = new List<CompanyDto>();
            _context.DisableTenantFilter = true;
            foreach (var company in companies)
            {
                
                
                var companyDto = new CompanyDto
                {
                    Id = company.Id,
                    NameEnglish = company.NameEnglish,
                    NameArabic = company.NameArabic,
                    VatRegistrationNo = company.VatRegistrationNo,
                    CompanyEmail = company.CompanyEmail,
                    CityEnglish = company.CityEnglish,
                    CityArabic = company.CityArabic,
                    CategoryEnglish = company.CategoryInEnglish,
                    CategoryArabic = company.CategoryInArabic,
                    CountryEnglish = company.CountryEnglish,
                    CountryArabic = company.CountryArabic,
                    CompanyRegNo = company.CompanyRegNo,
                    AddressEnglish = company.AddressEnglish,
                    AddressArabic = company.AddressArabic,
                    PhoneNo = company.PhoneNo,
                    Website = company.Website,
                    Postcode = company.Postcode,
                    LandLine = company.Landline,
                    ClientNo = company.ClientNo,
                    ParentId = company?.ParentId,
                    BusinessId = company?.BusinessParentId,
                    ClientParentId = company?.ClientParentId,
                    IsMultiUnit = company.IsMultiUnit,
                    IsProduction = company.IsProduction,
                    InvoiceWoInventory = company.InvoiceWoInventory,
                    IsArea = company.IsArea,
                    English = company.English,
                    Arabic = company.Arabic,
                    Terminal = company.Terminal,
                    DayStart = company.DayStart,
                    CashVoucher = company.CashVoucher,
                    IsOpenDay = company.IsOpenDay,
                    DailyExpense = company.ExpenseAccount,
                    IsTransferAllow = company.IsTransferAllow,
                    MasterProduct = company.MasterProduct,
                    SaleOrder = company.SaleOrder,
                    SimpleInvoice = company.SimpleInvoice,
                    SoInventoryReserve = company.SoInventoryReserve,
                    PurchaseOrder = company.PurchaseOrder,
                    InternationalPurchase = company.InternationalPurchase,
                 

                };
               
                companyList.Add(companyDto);
            }
            return companyList;
        }
        public CompanyDto GetCompanyByName(string companyName)
        {
            if (companyName == null) throw new ArgumentNullException(nameof(companyName));
            var company = _context.Companies.FirstOrDefault(x =>
                string.Equals(x.NameEnglish.ToLower(), companyName.ToLower(), StringComparison.OrdinalIgnoreCase));
            if (company == null) throw new NotFoundException(companyName, "Company");
            return new CompanyDto
            {
                Id = company.Id,
                NameEnglish = company.NameEnglish,
                CreatedDate = company.CreatedDate.ToString("dd/MMM/yyyy"),
                VatRegistrationNo = company.VatRegistrationNo,
                LogoPath = company.LogoPath
            };
        }
        public CompanyDto GetCompanyById(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            var dbCompany = _context.Companies.FirstOrDefault(x => x.Id == companyId);
            if (dbCompany != null)
            {
                return new CompanyDto
                {
                    Id = dbCompany.Id,
                    NameEnglish = dbCompany.NameEnglish,
                    NameArabic = dbCompany.NameArabic,
                    CreatedDate = dbCompany.CreatedDate.ToString("dd/MMM/yyyy"),
                    VatRegistrationNo = dbCompany.VatRegistrationNo,
                    CompanyRegNo = dbCompany.CompanyRegNo,
                    LogoPath = dbCompany.LogoPath,
                    AddressEnglish = dbCompany.AddressEnglish,
                    AddressArabic = dbCompany.AddressArabic,
                    CategoryEnglish = dbCompany.CategoryInEnglish,
                    CategoryArabic = dbCompany.CategoryInArabic,
                    Postcode = dbCompany.Postcode,
                    CountryArabic = dbCompany.CountryArabic,
                    Town = dbCompany.Town,
                    HouseNumber = dbCompany.HouseNumber,
                    CityArabic = dbCompany.CityArabic,
                    CityEnglish = dbCompany.CityEnglish,
                    ParentId = dbCompany.ParentId,
                    ClientParentId = dbCompany.ClientParentId,
                    Website = dbCompany.Website,
                    LandLine = dbCompany.Landline,
                    PhoneNo = dbCompany.PhoneNo,
                    ClientNo = dbCompany.ClientNo,
                    CompanyEmail = dbCompany.CompanyEmail,
                    CountryEnglish = dbCompany.CountryEnglish,
                    BusinessId = dbCompany.BusinessParentId,
                    IsMultiUnit = dbCompany.IsMultiUnit,
                    IsProduction = dbCompany.IsProduction,
                    CompanyNameEnglish = dbCompany.CompanyNameEnglish,
                    CompanyNameArabic = dbCompany.CompanyNameArabic,
                    IsProceed = dbCompany.IsProceed,
                    Step1=dbCompany.Step1,
                    Step2=dbCompany.Step2,
                    Step3=dbCompany.Step3,
                    Step4=dbCompany.Step4,
                    Step5=dbCompany.Step5,
                    
                };
            }
            return new CompanyDto();
        }
        public CompanyDto UpdateCompany(CompanyDto company)
        {
            var dbCompany = _context.Companies.FirstOrDefault(x => x.Id == company.Id);
            if (dbCompany == null) return new CompanyDto();
            //Check if user is changing VAT Registration number
           

            dbCompany.VatRegistrationNo = company.VatRegistrationNo;
            dbCompany.NameEnglish = company.NameEnglish;
            dbCompany.NameArabic = company.NameArabic;
            dbCompany.CompanyRegNo = company.CompanyRegNo;
            dbCompany.AddressEnglish = company.AddressEnglish;
            dbCompany.AddressArabic = company.AddressArabic;
            dbCompany.CategoryInEnglish = company.CategoryEnglish;
            dbCompany.CategoryInArabic = company.CategoryArabic;
            dbCompany.CountryEnglish = company.CountryEnglish;
            dbCompany.CountryArabic = company.CountryArabic;
            dbCompany.LogoPath = company.LogoPath;
            dbCompany.CityArabic = company.CityArabic;
            dbCompany.CityEnglish = company.CityEnglish;
            dbCompany.Landline = company.LandLine;
            dbCompany.CompanyNameEnglish = company.CompanyNameEnglish;
            dbCompany.CompanyNameArabic = company.CompanyNameArabic;

            _context.Companies.Update(dbCompany);
            _context.SaveChanges();
            return company;


        }
        public string GetCompanyLogo(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            var company = GetCompanyById(companyId);
            if (company != null)
            {
                if (!string.IsNullOrEmpty(company.LogoPath))
                {
                    return company.LogoPath;
                }
                else
                {
                    return "/ProfileImages/user-no-image.png";
                }
            }
            else
            {
                return "/ProfileImages/user-no-image.png";
            }
        }
        public void UpdateCompanyLogo(CompanyDto company)
        {
            if (company.Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(company.Id));
            }
            var companyDb = _context.Companies.FirstOrDefault(x => x.Id == company.Id);
            if (companyDb != null)
            {
                companyDb.LogoPath = company.LogoPath;
                _context.Companies.Update(companyDb);
                _context.SaveChanges();
            }
        }


        private static string GetStatus(bool value)
        {
            return value
                ?
                "<span class='badge badge-pill badge-danger'> Blocked </span>"
                : "<span class='badge badge-pill badge-success'> Active </span>";
        }


        public async Task<bool> DeleteCompany(Guid companyId)
        {
            var company = _context.Companies.FirstOrDefault(x => x.Id == companyId);
            var userList = _userManager.Users.Where(x => x.CompanyId == company.Id).ToList();
            if (company != null)
            {
                foreach (var user in userList)
                {
                    //Remove Claim
                    var claims = await _userManager.GetClaimsAsync(user);
                    var c = await _userManager.RemoveClaimsAsync(user, claims);

                    // Remover Role
                    var role = await _userManager.GetRolesAsync(user);
                    var r1 = await _userManager.RemoveFromRoleAsync(user, role[0]);

                    //Delete User
                    var u = await _userManager.DeleteAsync(user);
                }
                var cmp = _context.Companies.Remove(company);
                if (cmp != null)
                {

                    await _context.SaveChangesAsync();
                    return true;
                }

            }

            return false;

        }

        public void GetRoleByUser(UserDetailDto userDetailDto)
        {
            var user = new ApplicationUser
            {
                Id = userDetailDto.Id
            };
        }

      
    }
}