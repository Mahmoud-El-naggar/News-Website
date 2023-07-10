using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TahliliTask.BL;

public interface IUserManager
{
	IEnumerable<IdentityError> CreateUser();
	TokenDto? CreateToken(List<Claim> claims);
	IdentityUser? GetUser(LoginDto loginDTO);
	List<Claim>? GetClaims(IdentityUser user);
}

