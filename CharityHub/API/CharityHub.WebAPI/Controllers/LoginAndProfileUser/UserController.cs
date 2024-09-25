//using CharityHub.Business.Services.TokenRepository;
//using CharityHub.Business.ViewModels;
//using CharityHub.Data.Models;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
//using PayPal.v1.CustomerDisputes;
//using PayPal.v1.Identity;
//using System.Security.Claims;

//namespace CharityHub.WebAPI.Controllers.Login
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly ITokenRepository tokenRepository;
//        private readonly IHttpContextAccessor _httpContext;
//        private readonly HttpClient _httpClient;
//        private readonly AppSettings _applicationSettings;

//        public UserController(UserManager<User> userManager, 
//            ITokenRepository tokenRepository, 
//            IHttpContextAccessor httpContext,
//            HttpClient _httpClient,
//            IOptions<AppSettings> _applicationSettings)
//        {
//            _userManager = userManager;
//            this.tokenRepository = tokenRepository;
//            _httpContext = httpContext;
//            this._httpClient = _httpClient;
//            this._applicationSettings = _applicationSettings.Value;
//        }

//        //POST: /api/User/Login
//        [HttpPost]
//        [Route("Login")]
//        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
//        {
//            var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);

//            if (user != null && user.IsActive == true)
//            {
//                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
//                if (checkPasswordResult)
//                {
//                    user.LastLoginDate = DateTime.Now;
//                    await _userManager.UpdateAsync(user);

//                    //Get Roles for this user
//                    var roles = await _userManager.GetRolesAsync(user);
//                    var role = roles.FirstOrDefault();
//                    if (roles != null)
//                    {
//                        //Create Token
//                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

//                        var response = new LoginResponseDto
//                        {
//                            UserName = loginRequestDto.UserName,
//                            Role = role,
//                            JwtToken = jwtToken,
//                            Id = user.Id
//                        };

//                        return Ok(response);
//                    }
//                }
//            }

//            return BadRequest("Username hoac password khong dung hoac tai khoan da bi khoa!");
//        }

//    //    [HttpGet("login-facebook")]
//    //    [AllowAnonymous]
//    //    public IActionResult LoginFacebook()
//    //    {
//    //        var redirectUrl = Url.Action("ExternalLoginCallback", "User");
//    //        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
//    //        return Challenge(properties, "Facebook");
//    //    }

//    //    [HttpGet("signin-facebook")]
//    //    public async Task<IActionResult> ExternalLoginCallback()
//    //    {
//    //        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//    //        if (!result.Succeeded)
//    //        {
//    //            return BadRequest("Đăng nhập thất bại");
//    //        }

//    //        var emailClaim = result.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
//    //        var userIdClaim = result.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

//    //        if (emailClaim == null)
//    //        {
//    //            return BadRequest("Không tìm thấy email từ Facebook.");
//    //        }

//    //        var user = await _userManager.FindByEmailAsync(emailClaim);

//    //        if (user == null)
//    //        {
//    //            user = new User
//    //            {
//    //                Email = emailClaim,
//    //                UserName = emailClaim
//    //            };

//    //            var createResult = await _userManager.CreateAsync(user);
//    //            if (!createResult.Succeeded)
//    //            {
//    //                return BadRequest("Không thể tạo tài khoản mới!");
//    //            }

//    //            await _userManager.AddToRoleAsync(user, "User");
//    //        }

//    //        var roles = await _userManager.GetRolesAsync(user);
//    //        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

//    //        // Tạo ClaimsPrincipal cho cookie sign-in
//    //        var claims = new List<Claim>
//    //{
//    //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//    //    new Claim(ClaimTypes.Name, user.UserName),
//    //    new Claim(ClaimTypes.Email, user.Email)
//    //};
//    //        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

//    //        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//    //        var principal = new ClaimsPrincipal(identity);

//    //        // Sign in user using cookies
//    //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

//    //        return Ok(new { token = jwtToken });
//    //    }

//        //[HttpPost("LoginWithFacebook")]
//        //public async Task<IActionResult> LoginWithFacebook([FromBody] string credential)
//        //{
//        //    HttpResponseMessage debugTokenResponse = await _httpClient.GetAsync("https://graph.facebook.com/debug_token?input_token" + credential + $"access_token={this._applicationSettings.FacebookAppId}|{this._applicationSettings.FacebookSecret}");

//        //    var stringThing = await debugTokenResponse.Content.ReadAsStringAsync();
//        //    var userOBJK = JsonConvert.DeserializeObject<FBUser>(stringThing);

//        //    if (userOBJK.Data.IsValid == false)
//        //    {
//        //        return Unauthorized();
//        //    }

//        //    HttpResponseMessage meResponse = await _httpClient.GetAsync("https://graph.facebook.com/me?fields=first_name,last_name,email,id&access_token=" + credential);
//        //    var userContent = await meResponse.Content.ReadAsStringAsync();
//        //    var userContentObj = JsonConvert.DeserializeObject<FBUserInfo>(userContent);

//        //    var user = await _userManager.FindByEmailAsync(userContentObj.Email);
//        //    var roles = await _userManager.GetRolesAsync(user);
//        //    var role = roles.FirstOrDefault();

//        //    if (user != null)
//        //    {
//        //        return Ok(tokenRepository.CreateJWTToken(user, roles.ToList()));
//        //    } else
//        //    {
//        //        return BadRequest();
//        //    }
//        //}

//        // Đổi mật khẩu
//        [Authorize(Roles = "User")]
//        [HttpPut("Change-Password/{userId}")]
//        public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordViewModel passwordViewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _userManager.FindByIdAsync(userId.ToString());

//                if (user == null) return Unauthorized();

//                var result = await _userManager.ChangePasswordAsync(user, passwordViewModel.CurrentPassword, passwordViewModel.NewPassword);

//                if (result.Succeeded)
//                {

//                    return Ok();
//                }

//                var errors = result.Errors.Select(e => e.Description).ToList();
//                return BadRequest(new { errors });
//            }

//            return BadRequest(ModelState);
//        }

//        // Profile
//        [Authorize(Roles = "User")]
//        [HttpGet("Get-Profile/{id}")]
//        public async Task<IActionResult> GetProfile(Guid id)
//        {
//            var user = await _userManager.FindByIdAsync(id.ToString());

//            if (user == null) return NotFound();

//            var userProfile = new ProfileUserDto
//            {
//                UserName = user.UserName,
//                DisplayName = user.DisplayName,
//                PhoneNumber = user.PhoneNumber
//            };

//            return Ok(userProfile);
//        }


//        // Update Profile: DisplayName and PhoneNumber
//        [Authorize(Roles = "User")]
//        [HttpPut("Update-Profile/{id}")]
//        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateProfileRequestDto updateProfileRequestDto)
//        {
//            var user = await _userManager.FindByIdAsync(id.ToString());

//            if (user == null) return NotFound();

//            if (!string.IsNullOrEmpty(updateProfileRequestDto.DisplayName) && updateProfileRequestDto.DisplayName != "string")
//            {
//                user.DisplayName = updateProfileRequestDto.DisplayName;
//            }

//            if (!string.IsNullOrEmpty(updateProfileRequestDto.PhoneNumber) && updateProfileRequestDto.PhoneNumber != "string")
//            {
//                user.PhoneNumber = updateProfileRequestDto.PhoneNumber;
//            }

//            var result = await _userManager.UpdateAsync(user);

//            if (!result.Succeeded)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user profile.");
//            }

//            return Ok("Profile da duoc cap nhat thanh cong.");
//        }
//    }
//}


using CharityHub.Business.Services.TokenRepository;
using CharityHub.Business.ViewModels;
using CharityHub.Data.Data;
using CharityHub.Data.Models;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CharityHub.WebAPI.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration configuration;
        private readonly CharityHubDbContext dbContexct;

        public UserController(UserManager<User> userManager, ITokenRepository tokenRepository, IHttpContextAccessor httpContext, IConfiguration configuration, CharityHubDbContext dbContexct)
        {
            _userManager = userManager;
            this.tokenRepository = tokenRepository;
            _httpContext = httpContext;
            this.configuration = configuration;
            this.dbContexct = dbContexct;
        }

        //POST: /api/User/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user != null && user.IsActive == true)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    user.LastLoginDate = DateTime.Now;
                    await _userManager.UpdateAsync(user);

                    //Get Roles for this user
                    var roles = await _userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault();
                    if (roles != null)
                    {
                        //Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            UserName = loginRequestDto.UserName,
                            Role = role,
                            JwtToken = jwtToken,
                            Id = user.Id
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username hoac password khong dung hoac tai khoan da bi khoa!");
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestDto model)
        {
            var idtoken = model.IdToken;
            var setting = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new string[] { "810107547770-ku9ufi26s2gajocc64u3r66hnnb3lbff.apps.googleusercontent.com" }
            };
            var result = await GoogleJsonWebSignature.ValidateAsync(idtoken, setting);
            
            if (result is null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(result.Email);

            if (user == null)
            {
                user = new User
                {
                    Email = result.Email,
                    UserName = result.Email,
                    DateCreated = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                    DisplayName = result.Name,
                    IsActive = true
                };

                var tempPassword = Guid.NewGuid().ToString();

                var createUserResult = await _userManager.CreateAsync(user, tempPassword);

                if (createUserResult.Succeeded)
                {
                    //Add roles to this User

                    createUserResult = await _userManager.AddToRoleAsync(user, "User");

                    if (!createUserResult.Succeeded)
                    {
                        return BadRequest(createUserResult.Errors);
                    }

                }
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = tokenRepository.CreateJWTToken(user, roles.ToList());

            var response = new LoginResponseDto
            {
                UserName = result.Email,
                Role = roles.FirstOrDefault(),
                JwtToken = token,
                Id = user.Id
            };

            return Ok(response);

            //var jwtkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            //var credential = new SigningCredentials(jwtkey, SecurityAlgorithms.HmacSha256);

            //List<Claim> claims = new List<Claim>()
            //{
            //    new Claim("Email", result.Email)
            //};

            //var sToken = new JwtSecurityToken(
            //    configuration["Jwt:Key"],
            //    configuration["Jwt:Issuer"],
            //    claims,
            //    expires: DateTime.Now.AddMinutes(30),
            //    signingCredentials: credential);
            //var token = new JwtSecurityTokenHandler().WriteToken(sToken);
            //return Ok(new { token = token });

            //var user = await _userManager.FindByEmailAsync(result.Email);
            //if (user == null)
            //{
            //    return BadRequest("User not found.");
            //}

            //// Lấy vai trò của người dùng
            //var roles = await _userManager.GetRolesAsync(user);

            //// Tạo JWT token bằng phương thức từ TokenRepository
            //var token = tokenRepository.CreateJWTToken(user, roles.ToList());

            //return Ok(new { token = token });
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin([FromBody] FacebookLoginRequestDto model)
        {
            var authToken = model.AuthToken;

            // Gửi yêu cầu tới Facebook Graph API để xác thực token
            var fbValidationUrl = $"https://graph.facebook.com/me?access_token={authToken}&fields=id,name,email";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(fbValidationUrl);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Invalid Facebook token.");
            }

            // Đọc nội dung trả về dưới dạng chuỗi và phân tích cú pháp JSON
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var fbInfo = JsonConvert.DeserializeObject<FacebookUserInfo>(jsonResponse);

            var user = await _userManager.FindByEmailAsync(fbInfo.Email);

            if (user == null)
            {
                user = new User
                {
                    Email = fbInfo.Email,
                    UserName = fbInfo.Email,
                    DateCreated = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                    DisplayName = fbInfo.Name,
                    IsActive = true
                };

                var tempPassword = Guid.NewGuid().ToString();

                var createUserResult = await _userManager.CreateAsync(user, tempPassword);

                if (createUserResult.Succeeded)
                {
                    // Thêm vai trò cho người dùng
                    createUserResult = await _userManager.AddToRoleAsync(user, "User");

                    if (!createUserResult.Succeeded)
                    {
                        return BadRequest(createUserResult.Errors);
                    }
                }
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = tokenRepository.CreateJWTToken(user, roles.ToList());

            var responseDto = new LoginResponseDto
            {
                UserName = fbInfo.Email,
                Role = roles.FirstOrDefault(),
                JwtToken = token,
                Id = user.Id
            };

            return Ok(responseDto);
        }


        //Đổi mật khẩu
        [Authorize(Roles = "User")]
       [HttpPut("Change-Password/{userId}")]
        public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordViewModel passwordViewModel)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null) return Unauthorized();

            if (passwordViewModel.CurrentPassword == passwordViewModel.NewPassword)
            {
                return BadRequest("Mat khau moi bij trung voi mat khau cu");
            }

            if (passwordViewModel.NewPassword != passwordViewModel.ConfirmNewPassword)
            {
                return BadRequest("NewPassword khong trung voiw ConfirmNewPassword");
            }

            var result = await _userManager.ChangePasswordAsync(user, passwordViewModel.CurrentPassword, passwordViewModel.NewPassword);

            if (result.Succeeded)
            {

                return Ok();
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(new { errors });
        }


        // Profile
        [Authorize(Roles = "User")]
        [HttpGet("Get-Profile/{id}")]
        public async Task<IActionResult> GetProfile(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) return NotFound();

            var userProfile = new ProfileUserDto
            {
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(userProfile);
        }


        // Update Profile: DisplayName and PhoneNumber
        [Authorize(Roles = "User")]
        [HttpPut("Update-Profile/{id}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] UpdateProfileRequestDto updateProfileRequestDto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null) return NotFound();

            if (!string.IsNullOrEmpty(updateProfileRequestDto.DisplayName) && updateProfileRequestDto.DisplayName != "string")
            {
                user.DisplayName = updateProfileRequestDto.DisplayName;
            }

            if (!string.IsNullOrEmpty(updateProfileRequestDto.PhoneNumber) && updateProfileRequestDto.PhoneNumber != "string")
            {
                user.PhoneNumber = updateProfileRequestDto.PhoneNumber;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user profile.");
            }

            return Ok("Profile da duoc cap nhat thanh cong.");
        }
    }
}