using Focus.Business.Interface;
using Focus.Business.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Noble.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using InputModel = Noble.Api.Models.InputModel;
using Focus.Business.Models;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Focus.Business.Extensions;
using Microsoft.EntityFrameworkCore;
using Focus.Business;
using Focus.Business.Claims.Command.UpdateClaims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Net;
using Focus.Business.Common;

namespace Noble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserComponent _userComponent;
        private readonly ICompanyComponent _companyComponent;
        private readonly ISendEmail _sendEmail;
        private readonly IPrincipal _principal;
        private readonly IApplicationDbContext _context;
        public readonly IConfiguration _configuration;


        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            IApplicationDbContext context, IUserComponent userComponent, ISendEmail sendEmail,
            IPrincipal principal, ICompanyComponent companyComponent, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
            _sendEmail = sendEmail;
            _userComponent = userComponent;
            _principal = principal;
            _companyComponent = companyComponent;
            _configuration = configuration;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> LogInAsync([FromBody] InputModel input)
        {
            SignInResult result = new SignInResult();
            ApplicationUser user;

            if (ModelState.IsValid)
            {
                if (input.Email.IndexOf('@') > -1)
                {
                    //Validate email format
                    string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                    Regex re = new Regex(emailRegex);
                    if (!re.IsMatch(input.Email))
                    {
                        result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, lockoutOnFailure: false);
                        user = await _userManager.Users.Select(x => new ApplicationUser
                        {
                            Id = x.Id,
                            UserName = x.UserName,
                            CompanyId = x.CompanyId,
                            Email = x.Email,
                            EmployeeId = x.EmployeeId,
                            FirstName = x.FirstName,
                            ImagePath = x.ImagePath,
                            EmailConfirmed = x.EmailConfirmed,
                            PhoneNumber = x.PhoneNumber,
                        }).FirstOrDefaultAsync(x => x.UserName == input.Email);

                        if (user != null)
                            input.Email = user.Email;

                        if (user != null && user.IsActive)
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                            return Ok(new LoginModel());
                        }
                    }
                    else
                    {
                        user = await _userManager.Users.Select(x => new ApplicationUser
                        {
                            Id = x.Id,
                            UserName = x.UserName,
                            CompanyId = x.CompanyId,
                            Email = x.Email,
                            EmployeeId = x.EmployeeId,
                            FirstName = x.FirstName,
                            ImagePath = x.ImagePath,
                            EmailConfirmed = x.EmailConfirmed,
                            PhoneNumber = x.PhoneNumber,
                        }).FirstOrDefaultAsync(x => x.Email == input.Email);

                        if (user != null)
                            result = await _signInManager.PasswordSignInAsync(user.UserName, input.Password, input.RememberMe, lockoutOnFailure: false);

                        if (user != null && user.IsActive)
                        {
                            ModelState.AddModelError(string.Empty, "User is Not Active.Please Activate User");
                            return Ok(new LoginModel());
                        }
                    }

                }
                else
                {
                    result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, input.RememberMe, lockoutOnFailure: false);
                    user = await _userManager.Users.Select(x => new ApplicationUser
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        CompanyId = x.CompanyId,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        ImagePath = x.ImagePath,
                        EmailConfirmed = x.EmailConfirmed,
                        PhoneNumber = x.PhoneNumber,
                    }).FirstOrDefaultAsync(x => x.UserName == input.Email);

                    if (user != null)
                        input.Email = user.Email;
                    if (user is { IsActive: true })
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Ok(new LoginModel());
                    }
                }

                if (result.RequiresTwoFactor)
                {
                    return Ok(true);
                }

                if (result.Succeeded)
                {

                    var token = await GenerateJwtToken(user);

                    var role = await _userComponent.GetRoleByUser(user.Id);


                    return Ok(new LoginModel()
                    {


                        Token = token,

                        CompanyId = user.CompanyId,
                        FullName = user.FirstName,
                        RoleName = role,
                        ImagePath = user.ImagePath,
                        UserId = user.Id,
                        UserName = user.Email,
                        LoginUserName = user.UserName,
                        EmployeeId = user.EmployeeId,
                        EmailConfirmed = user.EmailConfirmed,
                        PhoneNo = user.PhoneNumber,

                    });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Ok(new LoginModel());
                }
            }

            return Ok(null);
        }


        private async Task<object> GenerateJwtToken(ApplicationUser user)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                identity.AddClaim(new Claim("UserId", user.Id));
                identity.AddClaim(new Claim("CompanyId", user.CompanyId.ToString()));
            }

            var token = await Mediator.Send(new UpdateClaimsCommand
            {
                User = user.Id,
                ApplicationUser = user,

            });

            //var current = HttpContext;
            //var permissionList = new ModuleWiseClaimsLookupModel();
            //permissionList.Token = token;
            //permissionList.CompanyId = User.Identity.CompanyId();
            //permissionList.TokenName = current.Request.Scheme + "://" + current.Request.Host + "/api";
            //var reportPath = _configuration.GetSection("ReportServer:Path").Value;

            //using var ping = new Ping();
            //using var httpClient = new HttpClient();
            //StringContent content = new StringContent(JsonConvert.SerializeObject(permissionList), Encoding.UTF8, "application/json");
            //using var response = httpClient.PostAsync(reportPath, content);
            //string errorResponse = response.Result.ToString();

            return token;

        }



        [Route("api/account/DuplicateEmail")]
        [HttpGet("DuplicateEmail")]
        public async Task<IActionResult> DuplicateEmail(string email)
        {
            var duplicate = await _userManager.FindByEmailAsync(email);
            if (duplicate == null)
            {
                return Ok(false);
            }
            return Ok(true);
        }

       
        [Route("api/account/AuthenticatorCode")]
        [HttpGet("AuthenticatorCode")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticatorCode(string email, string password, string code, string recoveryCode)

        {


            var userWith2Fa = await _userManager.FindByEmailAsync(email);

            if (userWith2Fa == null)
            {
                return Ok(false);
            }
            if (!ModelState.IsValid)
            {
                return Ok(null);
            }
            // Strip spaces and hypens
            //if (recoveryCode != stri)
            //{
            //    var codeRecovery = recoveryCode.Replace(" ", string.Empty);
            //    var ok = await _signInManager.TwoFactorRecoveryCodeSignInAsync(codeRecovery);
            //    if (!ok.Succeeded)
            //        return Ok(false);
            //}
            {


                var verificationCode = code.Replace(" ", string.Empty).Replace("-", string.Empty);
                var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                    userWith2Fa, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);
                if (!is2faTokenValid)
                {

                    return Ok(false);
                }
            }

            userWith2Fa.TwoFactorEnabled = false;
            var result = await _signInManager.PasswordSignInAsync(email, password, true, lockoutOnFailure: false);
            if (result.RequiresTwoFactor)
            {

                return Ok(true);
            }
            userWith2Fa.TwoFactorEnabled = true;
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == user.Email);
                var userCompany = _companyComponent.GetCompanyById(user.CompanyId);
                var role = await _userComponent.GetRoleByUser(user.Id);

                return Ok(new LoginModel()
                {
                    CompanyId = userCompany.Id,
                    FullName = user.FirstName,
                    RoleName = role,
                    ImagePath = user.ImagePath,
                    UserId = user.Id,
                    UserName = user.Email,
                    EmployeeId = user.EmployeeId,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    EmailConfirmed = user.EmailConfirmed,
                });
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Ok(new LoginModel());
            }

        }
      

        [Route("api/account/ResetAuthenticator")]
        [HttpGet("ResetAuthenticator")]
        public async Task<IActionResult> ResetAuthenticator(string id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            if (applicationUser == null)
            {
                return Ok(false);
            }
            await _userManager.SetTwoFactorEnabledAsync(applicationUser, false);
            await _userManager.ResetAuthenticatorKeyAsync(applicationUser);
            await _signInManager.RefreshSignInAsync(applicationUser);
            return Ok(true);

        }

        [Route("api/account/Disable2Fa")]
        [HttpGet("Disable2Fa")]

        public async Task<IActionResult> Disable2Fa(string id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            applicationUser.TwoFactorEnabled = false;

            IdentityResult result = await _userManager.UpdateAsync(applicationUser);
            if (result.Succeeded)
            { return Ok(true); }

            return Ok(false);

        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }
            return result.ToString().ToLowerInvariant();
        }
    

       
        [Route("api/account/logout")]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            var result = "Success";
            return Ok(result);
        }

        [Route("api/account/UserList")]
        [HttpGet("UserList")]
        public IActionResult UserList()
        {
            try
            {
                var listUsers = _userComponent.ListingUsers();
                return Ok(listUsers);
            }
            catch (Exception e)
            {


                throw new ApplicationException("List Error" + e);
            }
        }
        [Route("api/account/ForRoleUsersList")]
        [HttpGet("ForRoleUsersList")]

        public IActionResult ForRoleUsersList(bool isHistory, bool istransferTerminal, bool isSupervisorTerminal, bool alluser)
        {
            try
            {
                if (alluser)
                {
                    var listUsers = _userComponent.ListingUsers();
                    return Ok(listUsers);
                }
                else
                {
                    var listUsers = _userComponent.ForRoleUsersList(isHistory, istransferTerminal, isSupervisorTerminal);
                    return Ok(listUsers);
                }

            }
            catch (Exception e)
            {


                throw new ApplicationException("List Error" + e);
            }
        }
        [Route("api/account/UserDetail")]
        [HttpGet("UserDetail")]

        public async Task<IActionResult> UserDetail(Guid id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                {
                    var role = await _userManager.GetRolesAsync(user);

                    string roleName="";
                    if (role.Count > 0)
                    {
                        roleName= role[0].ToString();
                    }

                    var loginUser = new LoginVm
                    {
                        UserId = Guid.Parse(user.Id),
                        FirstName = user.FirstName,
                        IsActive = user.IsActive,
                        LastName = user.LastName,
                        Email = user.Email,
                        UserName = user.UserName,
                        IsProceed = user.IsProceed,
                        Remarks = user.Remarks,
                        RoleName= roleName
                    };
                    return Ok(loginUser);
                }
               

            }
            catch (Exception e)
            {
                throw new ApplicationException("List Error" + e);
            }
        }





        [Route("api/account/SaveMobileCustomer")]
        [HttpPost("SaveMobileCustomer")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveMobileCustomer([FromBody] LoginVm loginVm)
        {
            if (loginVm.Id == "00000000-0000-0000-0000-000000000000" || loginVm.Id == null)
            {

                var isloginEmailExist = await _userManager.FindByEmailAsync(loginVm.Email);
                if (isloginEmailExist == null)
                {
                    var register = new UserDetailDto()
                    {
                        FirstName = loginVm.FirstName,
                        LastName = loginVm.LastName,
                        Password = loginVm.Password,
                        Email = loginVm.Email,
                        PhoneNumber = loginVm.PhoneNumber,
                        CompanyId = loginVm.CompanyId
                    };
                    var user = new ApplicationUser
                    {
                        UserName = register.Email,
                        Email = register.Email,
                        EmployeeId = loginVm.EmployeeId,
                        CompanyId = register.CompanyId.Value
                    };
                    var result = await _userManager.CreateAsync(user, register.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, register.RoleName = "Mobile Customer");
                        var claims = new List<Claim>
                        {   new Claim("Email",user.Email),
                            //new Claim("FullName",null),
                            //new Claim("Organization",null),
                            new Claim("CompanyId",loginVm.CompanyId.ToString()),
                       };
                        await _userManager.AddClaimsAsync(user, claims);
                        await _context.SaveChangesAsync();
                        return Ok(new { value = true, check = "Add" });
                    }
                }
            }

            return Ok(new { value = false, check = "Already Exists" });
        }

        [Route("api/account/RolesList")]
        [HttpGet("RolesList")]
        public  IActionResult RolesList()
        {
            var role =  _userComponent.GetAllRoles();


            return Ok(role);
        }


        [Route("api/account/SaveUser")]
        [HttpPost("SaveUser")]
        //[Authorize(Roles = "User")]

        public async Task<IActionResult> SaveUser([FromBody] LoginVm loginVm)
        {
            if (loginVm.Id == "00000000-0000-0000-0000-000000000000")
            {
                //var loginEmail = await _context.LoginPermissions.FindAsync(loginVm.Email);

                var isloginEmailExist = await _userManager.FindByEmailAsync(loginVm.Email);
                var isloginUserIdExist = await _userManager.FindByNameAsync(loginVm.UserName);
                if (isloginEmailExist == null && isloginUserIdExist == null)
                {
                    var userForCode = _userManager.Users.OrderBy(x => x.Code).LastOrDefault();
                    string code = null;
                    if (userForCode != null)
                    {
                        if (string.IsNullOrEmpty(userForCode.Code))
                        {
                            code = "U" + "-" + "00001";
                        }
                        else
                        {
                            string fetchNo = userForCode.Code.Substring(userForCode.Code.Length - 5);
                            //fetchNo =  fetchNo.Substring(fetchNo.Length-5);
                            Int32 autoNo = Convert.ToInt32((fetchNo));
                            var format = "00000";
                            autoNo++;
                            code = "U" + "-" + autoNo.ToString(format);
                        }
                    }

                    var register = new UserDetailDto()
                    {
                        FirstName = loginVm.FirstName,
                        LastName = loginVm.LastName,
                        Password = loginVm.Password,
                        Email = loginVm.Email,
                        UserId = loginVm.UserName,
                        PhoneNumber = loginVm.PhoneNumber,
                        IsActive = loginVm.IsActive,
                        Code = code,
                        Remarks = loginVm.Remarks,
                        CreatedDate = DateTime.Now
                        
                    };

                    var user = new ApplicationUser
                    {
                        UserName = register.UserId,
                        Email = register.Email,
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        EmployeeId = loginVm.EmployeeId,
                        CompanyId = User.Identity.CompanyId(),
                        IsActive = register.IsActive,
                        Code = register.Code,
                        Remarks = register.Remarks,
                        CreatedDate = register.CreatedDate

                    };



                   

                    var result = await _userManager.CreateAsync(user, register.Password);

                    await _context.SaveChangesAsync();

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, loginVm.RoleName);
                        var claims = new List<Claim>
                        {   new Claim("Email",user.Email),
                            new Claim("FullName",$"{user.FirstName}{user.LastName}"),
                            new Claim("Organization",_principal.Identity.Organization()),
                            new Claim("CompanyId",_principal.Identity.CompanyId().ToString()),
                            new Claim("NobleCompanyId",_principal.Identity.CompanyId().ToString()),
                        };
                        await _userManager.AddClaimsAsync(user, claims);
                      
                        await _context.SaveChangesAsync();
                        return Ok(new { value = true, check = "Add" });
                    }
                }
            }
            else
            {
                
                var currentUser = await _userManager.FindByIdAsync(loginVm.Id.ToString());

              
               
                currentUser.UserName = loginVm.UserName;
                currentUser.FirstName = loginVm.UserName;
                currentUser.NormalizedUserName = loginVm.UserName.ToUpper();
                currentUser.IsActive = loginVm.IsActive;
                currentUser.Remarks = loginVm.Remarks;

                await _userManager.UpdateAsync(currentUser);

                var role = await _userManager.GetRolesAsync(currentUser);

                string roleName = "";
                if (role.Count > 0)
                {
                    roleName = role[0];
                    IdentityResult result = await _userManager.RemoveFromRoleAsync(currentUser, roleName);


                    await _userManager.AddToRoleAsync(currentUser, loginVm.RoleName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(loginVm.RoleName))
                    {
                        await _userManager.AddToRoleAsync(currentUser, loginVm.RoleName);

                    }
                }
               
              




                {

                    var user = await _userManager.FindByIdAsync(User.Identity.UserId());
                  
                    await _userManager.RemoveClaimAsync(user, new Claim("Email", user.Email, ClaimValueTypes.Boolean));
                    var claims = new List<Claim>
                    {   new Claim("Email",user.Email),
                        new Claim("FullName",$"{user.FirstName}{user.LastName}"),
                        new Claim("Organization",_principal.Identity.Organization()),
                        new Claim("CompanyId",_principal.Identity.CompanyId().ToString()),
                        new Claim("NobleCompanyId",_principal.Identity.CompanyId().ToString()),
                    };
                    await _userManager.AddClaimsAsync(user, claims);
                }

                //Save changes to database
                await _context.SaveChangesAsync();

            }
            return Ok("Already Exists");
        }


        [Route("api/account/DeleteUser")]
        [HttpGet("DeleteUser")]
        public async Task<Message> DeleteUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                var userPayment = await _context.Payments.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (userPayment == null)
                {
                   await _userManager.DeleteAsync(user);

                    return new Message
                    {
                        IsSuccess = true,
                        IsAddUpdate = "User Deleted Successfully",
                    };
                }
                else
                {
                    return new Message
                    {
                        IsSuccess = false,
                        IsAddUpdate = "User is used in payments",
                    };
                }
                
            }
            catch (Exception)
            {
                return new Message
                {
                    IsSuccess= false,
                    IsAddUpdate = "Something Went Wrong",
                };
            }
        }

        #region For ForgetPassword
        [Route("api/account/SendUserEmail")]
        [HttpGet("SendUserEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> SendUserEmail(string emailTo, string appLink)
        {
            var isloginEmailExist = await _userManager.FindByEmailAsync(emailTo);
            if (isloginEmailExist != null)
            {
                var sendEmailDto = new SendEmailDto()
                {
                    EmailTo = emailTo,
                    Subject = "Password Reset"
                };
                var result = _sendEmail.SendEmailAsync(sendEmailDto, isloginEmailExist.UserName, isloginEmailExist.Id, appLink);
                return Ok(result);
            }
            return Ok("Error");
        }
        [Route("api/account/UpdatePassword")]
        [HttpPost("UpdatePassword")]


        public async Task<IActionResult> UpdatePassword([FromBody] PasswordVerifyVm passwordVerify)
        {
            bool result = await _userComponent.UpdatePassword(passwordVerify.UserId, passwordVerify.Password);
            if (result)
            {
                return Ok(result);
            }
            return Ok("Error");
        }
        #endregion


     



       

    }
}
