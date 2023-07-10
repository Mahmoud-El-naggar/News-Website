using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TahliliTask.BL;

public class UserManager : IUserManager
{
	private readonly IConfiguration configuration;
	private readonly UserManager<IdentityUser> userManager;

	
	public UserManager(IConfiguration configuration, UserManager<IdentityUser> userManager)
	{
		this.configuration = configuration;
		this.userManager = userManager;
		
	}
	public TokenDto? CreateToken(List<Claim> claims)
	{
		var key = configuration.GetSection("SecretKey").Value;
		var keyByteArr = Encoding.ASCII.GetBytes(key!);
		var securityKey = new SymmetricSecurityKey(keyByteArr);
		var signCredintials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

		var exp = DateTime.Now.AddDays(10);
		var token = new JwtSecurityToken(
			claims: claims,
			signingCredentials: signCredintials,
			expires: exp
			);
		var tokenHandler = new JwtSecurityTokenHandler();

		return new TokenDto { Token = tokenHandler.WriteToken(token) };
	}

	public IEnumerable<IdentityError> CreateUser()
	{

		var user = new IdentityUser { UserName = Constants.UserName };

		var userCreation = userManager.CreateAsync(user, Constants.Password).Result;
		if (!userCreation.Succeeded)
			return userCreation.Errors;

		var claims = new List<Claim>
		{
			new (ClaimTypes.NameIdentifier,user.Id),
			new (ClaimTypes.Role,Constants.Role)
		};
		var addUserClaims = userManager.AddClaimsAsync(user, claims).Result;

		if (!addUserClaims.Succeeded)
			return addUserClaims.Errors;

		return null!;
	}

	public List<Claim>? GetClaims(IdentityUser user)
	{
		return userManager.GetClaimsAsync(user).Result.ToList();
	}

	public IdentityUser? GetUser(LoginDto loginDTO)
	{
		var userName = userManager.FindByNameAsync(loginDTO.UserName).Result;
		if (userName is null && loginDTO.UserName == Constants.UserName && loginDTO.Password == Constants.Password)
		{
			//create admin for first valid credentials only
			CreateUser();
			userName = GetUser(loginDTO);

		}
		var correctPass = userManager.CheckPasswordAsync(userName!, loginDTO.Password).Result;
		if (!correctPass)
			return null!;

		return userName;

	}
}
