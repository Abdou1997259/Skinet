using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Skinet.API.Authentication;
using Skinet.API.DTOs;
using Skinet.API.Errors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Skinet.API.Controllers
{
    public class AuthController(Jwt _jwt,StoreContext _db) : BaseApiController
    {
        [HttpGet]
        public ActionResult<string> Login(AthuenticatedUser request)
        {
           var user= _db.Users.FirstOrDefault(x=>x.UserName== request.UserName&& x.Password== request.Password);
            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var tokenHandler=new JwtSecurityTokenHandler();
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Audience = _jwt.Audience,
                Issuer = _jwt.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)),
                  SecurityAlgorithms.HmacSha256
                ),
                Subject=new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim(ClaimTypes.Name,request.UserName),
                    new Claim("DateOfBirth","1997-01-01")
                }),
                

            };
            var token= tokenHandler.CreateToken(tokenDecriptor);
           string accessToken=tokenHandler.WriteToken(token);

            return accessToken;

        }
    }
}
