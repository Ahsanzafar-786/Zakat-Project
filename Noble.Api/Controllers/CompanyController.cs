using Focus.Business.Extensions;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.Users;
using Focus.Domain.Entities;
using Focus.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;



using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;



namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        public readonly ApplicationDbContext _Context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICompanyComponent _companyComponent;
        private readonly IPrincipal _principal;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public readonly IConfiguration _configuration;

        public CompanyController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICompanyComponent companyComponent,
            IPrincipal principal,
            IHostingEnvironment hostingEnvironment,
            IConfiguration configuration
           )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _Context = context;
            _companyComponent = companyComponent;
            _principal = principal;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        #region For Company

        [Route("api/Company/GetBaseImage")]
        [HttpGet("GetBaseImage")]

        public async Task<IActionResult> GetBaseImage(string filePath)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath) + filePath;
            if (System.IO.File.Exists(path))
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(path);
                var base64 = Convert.ToBase64String(bytes);
                return Ok(base64);


            }
            return Ok(null);

        }

        //[Route("api/Company/DeleteAttachment")]
        //[HttpGet("DeleteAttachment")]

        //public async Task<IActionResult> DeleteAttachment(string path)
        //{

        //    var filePath = Path.Combine(_hostingEnvironment.WebRootPath) + path;
        //    System.IO.File.Delete(Path.Combine(filePath));
        //    return Ok(true);
        //}

        [Route("api/Company/EditCompany")]
        [HttpPost("EditCompany")]
       
        public IActionResult Edit(Guid Id)
        {
            var company = _companyComponent.GetCompanyById(Id);
            var logoBase64 = "";
            var imagePath = $"{_hostingEnvironment.WebRootPath}" + company.LogoPath;
            if (System.IO.File.Exists(imagePath))
            {
                var bytes = System.IO.File.ReadAllBytesAsync(imagePath);
                logoBase64 = Convert.ToBase64String(bytes.Result);
            }
            company.Base64Logo = logoBase64;
            return Ok(company);
        }

        [Route("api/Company/GetCompanyDetail")]
        [HttpGet("GetCompanyDetail")]
        public IActionResult GetCompanyDetail(Guid Id)
        {
            var company = _companyComponent.GetCompanyById(Id);
            var logoBase64 = "";
            var imagePath = $"{_hostingEnvironment.WebRootPath}" + company.LogoPath;
            if (System.IO.File.Exists(imagePath))
            {
                var bytes = System.IO.File.ReadAllBytesAsync(imagePath);
                logoBase64 = Convert.ToBase64String(bytes.Result);
            }
            company.Base64Logo = logoBase64;
            return Ok(company);
        }

        [Route("api/Company/PasswordVerify")]
        [HttpPost("PasswordVerify")]
        public async Task<IActionResult> PasswordVerifyAsync([FromBody] PasswordVerifyVm passwordVerify)
        {
            var user = "";
            var loginUser = new ApplicationUser();
            if (passwordVerify.UserName.IndexOf('@') > -1)
            {
                //Validate email format
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(passwordVerify.UserName))
                {
                    loginUser = await _userManager.FindByNameAsync(passwordVerify.UserName);
                    if (loginUser != null)
                    {
                        user = loginUser.UserName;
                        loginUser.TwoFactorEnabled = false;
                    }

                }
                else
                {
                    loginUser = await _userManager.FindByEmailAsync(passwordVerify.UserName);
                    if (loginUser != null)
                    {
                        user = loginUser.UserName;
                        loginUser.TwoFactorEnabled = false;
                    }
                }

            }
            else
            {
                loginUser = await _userManager.FindByNameAsync(passwordVerify.UserName);
                if (loginUser != null)
                {
                    user = loginUser.UserName;
                    loginUser.TwoFactorEnabled = false;
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, passwordVerify.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                loginUser.TwoFactorEnabled = true;
                return Ok(true);
            }
            return Ok(false);



        }
        [Route("api/Company/SaveCompany")]
        [HttpPost("SaveCompany")]
        public async Task<IActionResult> SaveCompany([FromBody] CompanyAndUserVm companyUser)
        {

            if (companyUser.Id == String.Empty)
            {
                var company = new Company
                {
                    NameArabic = companyUser.NameArabic,
                    NameEnglish = companyUser.NameEnglish,
                    AddressArabic = companyUser.AddressArabic,

                    AddressEnglish = companyUser.AddressEnglish,
                    VatRegistrationNo = companyUser.VatRegistrationNo,
                    CityArabic = companyUser.CityArabic,
                    CityEnglish = companyUser.CityEnglish,
                    CompanyRegNo = companyUser.ComercialRegNo,
                    CompanyEmail = companyUser.CompanyEmail,
                    CountryArabic = companyUser.CountryArabic,
                    Landline = companyUser.LandLine,
                    CountryEnglish = companyUser.CountryEnglish,
                    HouseNumber = companyUser.HouseNumber,
                    CreatedDate = DateTime.UtcNow,
                    Website = companyUser.Website,
                    PhoneNo = companyUser.PhoneNo,
                    ClientNo = companyUser.ClientNo,
                    Blocked = false,
                    ParentId = _principal.Identity.CompanyId(),
                    Postcode = companyUser.Postcode,
                    Town = companyUser.Town,

                };
                await _Context.Companies.AddAsync(company);
                var register = new UserDetailDto()
                {
                    FirstName = companyUser.FirstName,
                    LastName = companyUser.LastName,
                    Password = companyUser.Password,
                    UserId = companyUser.FirstName,
                    Email = companyUser.Email,
                    PhoneNumber = companyUser.PhoneNumber,
                };
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = register.UserId,
                        Email = register.Email,
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        CompanyId = company.Id,
                        IsProceed = true,

                    };

                    //GENERATE NEW USER PASSWORD

                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        //var sendEmailDto = new SendEmailDto()
                        //{
                        //    EmailTo = user.Email,
                        //    Subject = "Create Password"
                        //};
                        //await _sendEmail.SendEmailAsync(sendEmailDto, user.UserName, user.Id);
                        await _userManager.AddToRoleAsync(user, register.RoleName = "Admin");
                       
                        var claims = new List<Claim>
                        {
                            new Claim("Email",user.Email),
                            new Claim("FullName",$"{user.FirstName}{user.LastName}"),
                            new Claim("Organization",company.NameEnglish),
                            new Claim("CompanyId",company.Id.ToString()),
                            new Claim("NobleCompanyId",_principal.Identity.CompanyId().ToString())
                        };
                        await _userManager.AddClaimsAsync(user, claims);
                        await _Context.SaveChangesAsync();
                        //if (company.Id != Guid.Empty)
                        //{
                        //    _Context.SaveChangesAfter();
                        //}
                        return Ok(new { value = true, check = "Add" });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return null;
            }
            else
            {
                var company = new CompanyDto
                {
                    Id = Guid.Parse(companyUser.Id),
                    NameArabic = companyUser.NameArabic,
                    NameEnglish = companyUser.NameEnglish,
                    CompanyRegNo = companyUser.ComercialRegNo,
                    CityEnglish = companyUser.CityEnglish,
                    CityArabic = companyUser.CityArabic,
                    AddressArabic = companyUser.AddressArabic,
                    AddressEnglish = companyUser.AddressEnglish,
                    VatRegistrationNo = companyUser.VatRegistrationNo,
                    PhoneNo = companyUser.PhoneNo,
                    Website = companyUser.Website,
                    LandLine = companyUser.LandLine,
                    CountryEnglish = companyUser.CountryEnglish,
                    CountryArabic = companyUser.CountryArabic,
                    Postcode = companyUser.Postcode,
                    Town = companyUser.Town,
                    LogoPath = companyUser.LogoPath,
                    HouseNumber = companyUser.HouseNumber,
                    ClientNo = companyUser.ClientNo,
                };
                _companyComponent.UpdateCompany(company);
                return Ok(new { value = true, check = "Update" });
            }
        }

        [Route("api/Company/SaveMultiUnit")]
        [HttpPost("SaveMultiUnit")]
        //[Authorize(Roles = "Noble Admin")]
        public async Task<IActionResult> SaveMultiUnit([FromBody] CompanyDto company)
        {
            var response = _companyComponent.UpdateCompany(company);
            return Ok(new { value = true, check = "Update" });
        }
        [Route("api/Company/SaveDayStart")]
        [HttpPost("SaveDayStart")]
        //[Authorize(Roles = "Noble Admin")]
        public IActionResult SaveDayStart([FromBody] CompanyDto company)
        {
            var response = _companyComponent.UpdateCompany(company);
            return Ok(new { value = true, check = "Update" });
        }

        [Route("api/Company/CompanyList")]
        [HttpGet("CompanyList")]
        public IActionResult CompanyList(Guid id)
        {
            return Ok(_companyComponent.GetCompaniesList(id));
        }
        [Route("api/Company/EditCompany")]
        [HttpGet("EditCompany")]
        public async Task<IActionResult> EditCompanyAsync(Guid Id)
        {
            var company = _companyComponent.GetCompanyById(Id);

           

          

            var companyDetails = new CompanyAndUserVm
            {
                NameEnglish = company.NameEnglish,
                NameArabic = company.NameArabic,
                AddressEnglish = company.AddressEnglish,
                AddressArabic = company.AddressArabic,
                ComercialRegNo = company.CompanyRegNo,
                VatRegistrationNo = company.VatRegistrationNo,
                CityEnglish = company.CityEnglish,
                CityArabic = company.CityArabic,
                ClientNo = company.ClientNo,
                CompanyEmail = company.CompanyEmail,
                CountryEnglish = company.CountryEnglish,
                CountryArabic = company.CountryArabic,
                LogoPath = company.LogoPath,
                Website = company.Website,
                LandLine = company.LandLine,
                PhoneNo = company.PhoneNo,
                CategoryEnglish = company.CategoryEnglish,
                CategoryArabic = company.CategoryArabic,
                CompanyNameEnglish = company.CompanyNameEnglish,
                CompanyNameArabic = company.CompanyNameArabic,
                Id = company.Id.ToString(),
            };
            return Ok(companyDetails);
        }
        [Route("api/Company/UploadFilesAsync")]
        [HttpPost("UploadFilesAsync")]
        public IActionResult UploadFilesAsync(List<IFormFile> files)
        {
            string dbFilePath;
            string filePath;
            var dbPath = string.Empty;
            if (files != null && files.Any())
            {
                //check if Attachment folder is not created
                var pathWithFolderName = Path.Combine(_hostingEnvironment.WebRootPath, "Attachment");
                if (!Directory.Exists(pathWithFolderName))
                {
                    var di = Directory.CreateDirectory(pathWithFolderName);
                }
                foreach (var file in files)
                {
                    //save file in attachment folder
                    if (file.Length > 0)
                    {
                        var extenstion = Path.GetExtension(file.FileName);
                        var myUniqueFileName = Guid.NewGuid().ToString();
                        dbFilePath = myUniqueFileName + extenstion;
                        dbPath = "/Attachment/" + dbFilePath;
                        filePath = Path.Combine(pathWithFolderName, dbFilePath);
                        var fileStream = new FileStream(filePath, FileMode.Create);
                        file.CopyTo(fileStream);
                        fileStream.Close();
                    }
                }
            }
            return Ok(dbPath);
        }

        [Route("api/Company/BusinessList")]
        [HttpGet("BusinessList")]
        [Authorize(Roles = "Admin")]
        public IActionResult BusinessList()
        {
            return Ok(_companyComponent.GetCompaniesList(Guid.Empty));
        }
        [Route("api/Company/BusinessDetails")]
        [HttpGet("BusinessDetails")]
        [Authorize(Roles = "Admin")]
        public IActionResult BusinessDetails(Guid id)
        {
            return Ok(_companyComponent.GetCompanyById(id));
        }

        [Route("api/Company/SaveBusiness")]
        [HttpPost("SaveBusiness")]
        [Authorize(Roles = "Admin, Noble Admin")]
        public async Task<IActionResult> SaveBusiness([FromBody] BusinessVm business)
        {
            if (business.Id == string.Empty)
            {
                var clientParentId = _principal.Identity.CompanyId();
                var ParentId = _principal.Identity.NobelCompanyId();
                if (business.ClientParentId != Guid.Empty)
                {
                    clientParentId = business.ClientParentId;
                    ParentId = _principal.Identity.CompanyId();

                }
                var company = new Company
                {
                    NameArabic = business.NameInArabic,
                    NameEnglish = business.NameInEnglish,
                    AddressArabic = business.AddressInArabic,
                    AddressEnglish = business.AddressInEnglish,

                    CityArabic = business.CityInArabic,
                    CityEnglish = business.CityInEnglish,

                    CategoryInArabic = business.CategoryInArabic,
                    CategoryInEnglish = business.CategoryInEnglish,

                    CountryArabic = business.CountryInArabic,
                    CountryEnglish = business.CountryInEnglish,

                    PhoneNo = business.PhoneNumber,//FOR ENGLISH
                    Landline = business.LandLine,//FOR ARABIC
                    CompanyEmail = business.Email,
                    CreatedDate = DateTime.UtcNow,
                    Blocked = false,
                    ParentId = ParentId,
                    ClientParentId = clientParentId,
                };
                await _Context.Companies.AddAsync(company);
                var register = new UserDetailDto()
                {
                    FirstName = business.FirstName,
                    LastName = business.LastName,
                    Password = business.Password,
                    Email = business.Email,
                    UserId = business.UserId,
                    PhoneNumber = business.PhoneNumber,
                };
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = register.UserId,
                        Email = register.Email,
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        CompanyId = company.Id
                    };
                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, register.RoleName = "Admin");

                        var claims = new List<Claim>
                        {
                            new Claim("Email",user.Email),
                            new Claim("FullName",$"{user.FirstName}{user.LastName}"),
                            new Claim("Organization",company.NameEnglish),
                            new Claim("CompanyId",company.Id.ToString()),
                            new Claim("NobleCompanyId",_principal.Identity.NobelCompanyId().ToString()),
                            new Claim("ClientParentId",company.Id.ToString()),

                        };
                        await _userManager.AddClaimsAsync(user, claims);
                        await _Context.SaveChangesAsync();
                        await _userManager.AddClaimsAsync(user, claims);
                        await _Context.SaveChangesAsync();
                       
                        return Ok(new { value = true, check = "Add" });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return null;
            }
            else
            {
                var company = new CompanyDto
                {

                    NameArabic = business.NameInArabic,
                    NameEnglish = business.NameInEnglish,

                    CityEnglish = business.CityInEnglish,
                    CityArabic = business.CityInArabic,
                    AddressArabic = business.AddressInArabic,
                    AddressEnglish = business.AddressInEnglish,

                    CountryEnglish = business.CountryInEnglish,
                    LandLine = business.LandLine,
                    CountryArabic = business.CountryInArabic,

                };
                _companyComponent.UpdateCompany(company);
                return Ok(new { value = true, check = "Update" });
            }
        }
      
        [Route("api/Company/SaveLocation")]
        [HttpPost("SaveLocation")]
        
        public async Task<IActionResult> SaveLocation([FromBody] BusinessVm business)
        {
            if (business.Id == string.Empty)
            {
                var clientParentId = _principal.Identity.ClientParentId();
                var ParentId = _principal.Identity.NobelCompanyId();
                var busId = _principal.Identity.CompanyId();
                if (business.ClientParentId != Guid.Empty)
                {
                    clientParentId = business.ClientParentId;
                    ParentId = _principal.Identity.CompanyId();
                    busId = business.BusinessParentId;

                }
                var company = new Company
                {
                    NameArabic = business.NameInArabic,
                    NameEnglish = business.NameInEnglish,

                    AddressArabic = business.AddressInArabic,
                    AddressEnglish = business.AddressInEnglish,

                    CityArabic = business.CityInArabic,
                    CityEnglish = business.CityInEnglish,

                    CountryArabic = business.CountryInArabic,
                    CountryEnglish = business.CountryInEnglish,

                    Landline = business.LandLine,
                    PhoneNo = business.PhoneNumber,

                    CompanyEmail = business.Email,
                    Website = business.Website,

                    CompanyRegNo = business.CompanyRegNo,
                    VatRegistrationNo = business.VatRegistrationNo,

                    CreatedDate = DateTime.UtcNow,

                    Blocked = false,
                    ParentId = ParentId,
                    ClientParentId = clientParentId,
                    BusinessParentId = busId,

                };
                await _Context.Companies.AddAsync(company);



                var register = new UserDetailDto()
                {
                    FirstName = business.FirstName,
                    LastName = business.LastName,
                    Password = business.Password,
                    Email = business.UserEmail,
                    UserId = business.FirstName,
                    PhoneNumber = "2323",
                };
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = register.UserId,
                        Email = register.Email,
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        CompanyId = company.Id,
                        PhoneNumber = register.PhoneNumber
                    };
                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, register.RoleName = "User");
                       
                        var claims = new List<Claim>
                        {
                            new Claim("Email",user.Email),
                            new Claim("FullName",$"{user.FirstName}{user.LastName}"),
                            new Claim("Organization",company.NameEnglish),
                            new Claim("CompanyId",company.Id.ToString()),
                            new Claim("NobleCompanyId",_principal.Identity.NobelCompanyId().ToString()),
                            new Claim("ClientParentId",company.Id.ToString()),

                        };
                        await _userManager.AddClaimsAsync(user, claims);

                        await _Context.SaveChangesAsync();
                       



                        return Ok(new { value = true, check = "Add" });


                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                return null;
            }
            else
            {
                var company = new CompanyDto
                {
                    Id = Guid.Parse(business.Id),
                    NameArabic = business.NameInArabic,
                    NameEnglish = business.NameInEnglish,

                    CityEnglish = business.CityInEnglish,
                    CityArabic = business.CityInArabic,
                    AddressArabic = business.AddressInArabic,
                    AddressEnglish = business.AddressInEnglish,

                    CountryEnglish = business.CountryInEnglish,
                    CountryArabic = business.CountryInArabic,
                    LandLine = business.LandLine,

                    LogoPath = business.LogoPath,
                    CategoryEnglish = business.CategoryInEnglish,
                    CategoryArabic = business.CategoryInArabic,
                    CompanyNameEnglish = business.CompanyNameEnglish,
                    CompanyNameArabic = business.CompanyNameArabic,

                    CompanyRegNo = business.CompanyRegNo,
                    VatRegistrationNo = business.VatRegistrationNo,

                };
                _companyComponent.UpdateCompany(company);

              
                await _Context.SaveChangesAsync();
                return Ok(new { value = true, check = "Update" });
            }
        }

        [Route("api/Company/UpdateUser")]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] RegisterVm model)
        {
            var applicationUser = await _userManager.FindByIdAsync(model.Id);
            applicationUser.ImagePath = model.ImagePath;
            applicationUser.FirstName = model.FirstName;
            applicationUser.LastName = model.LastName;

            await _userManager.UpdateAsync(applicationUser);


            return Ok(new { value = true });
        }

        [Route("api/Company/ImageSearch")]
        [HttpGet("ImageSearch")]
        public async Task<IActionResult> ImageSearch(string id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            return Ok(applicationUser);
        }

        [Route("api/Company/LocationList")]
        [HttpGet("LocationList")]
        [Authorize(Roles = "Admin")]
        public IActionResult LocationList()
        {
            return Ok(_companyComponent.GetCompaniesList(Guid.Empty));
        }
        [Route("api/Company/LocationDetails")]
        [HttpGet("LocationDetails")]
        public IActionResult LocationDetails()
        {
            Guid companyId = _principal.Identity.CompanyId();
            return Ok(_companyComponent.GetCompanyById(companyId));
        }

        
     

      

      
        
        #endregion
       

        [Route("api/Company/TermsAndConditionAgreed")]
        [HttpGet("TermsAndConditionAgreed")]
        public IActionResult TermsAndConditionAgreed(Guid companyId, bool termsCondition)
        {
            var company = _Context.Companies.AsNoTracking().FirstOrDefault(x => x.Id == companyId);
            if (company == null)
            {
                return Ok(false);

            }
            company.TermsCondition = termsCondition;
            _Context.Update(company);
            _Context.SaveChanges();
            return Ok(true);
        }

        #region Permission


        



      

       
        #endregion

    





   


     




     

       


    }
}