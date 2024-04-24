using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnicaCycle.Application.Constants;
using PruebaTecnicaCycle.Application.Interfaces;
using PruebaTecnicaCycle.Domain.Dtos.Identity;
using PruebaTecnicaCycle.Identity.Helpers;
using PruebaTecnicaCycle.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaTecnicaCycle.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;//utiliza para administrar usuarios, incluida la creación, edición, eliminación y autenticación de usuarios
        private readonly SignInManager<ApplicationUser> _signInManager;//Este nos ayuda con la validacion de credenciales
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            //busco el usuario en BD
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null) 
                throw new Exception($"User {request.UserName} doesn't exist.");

            var result=await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded) throw new Exception($"Invalid Credentials");

            //Obtengo el token
            var token = await GenerateToken(user);

            var authResponse = new AuthResponse
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email=user.Email,
                UserName= user.UserName,
            };

            return authResponse;
        }

        public async Task<RegistrationResponse> RegisterUser(RegistrationRequest request)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var role = request.Administrator ? Enums.Administrator.ToString() : Enums.Seller.ToString();

            //valido existencia de usuario
            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
                throw new Exception($"User {request.UserName} already exist.");

            //valido existencia de email
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail != null)
                throw new Exception($"The Email {request.Email} was already registered by another User.");

            //mapeo lo que me envio el cliente al model ApplicationUser
            //var user = _mapper.Map<ApplicationUser>(request);

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                LastName = request.LastName,
                Name = request.Name,
                EmailConfirmed = true,
                PhoneNumberConfirmed=false,
                TwoFactorEnabled= false,
                LockoutEnabled= false,
                AccessFailedCount=0
            };

            //creo el usuario
            var result = await _userManager.CreateAsync(user, hasher.HashPassword(null, request.Password));

            if (!result.Succeeded) throw new Exception($"Error to Create User. {result.Errors}");

            //Creo el role
            await _userManager.AddToRoleAsync(user, role);

            //Obtengo el token
            var token = await GenerateToken(user);

            //var registrationResult = _mapper.Map <RegistrationResponse>(user);

            //asigno el token al usuario.
            //registrationResult.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return new RegistrationResponse
            {
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
                UserName = user.UserName,
            };

       
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            //data para agregar al token

            //Informacion del usuario para el token
            var userClaims = await _userManager.GetClaimsAsync(user);


            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            //inserto los roles en el claims

            foreach(var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            //creo los claims para el token tomando los userclaims, roleclaims, los claims de Jwt y los customerClaims que creo.

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid,user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            //obtengo la llaver de seguirdad para el algoritmo
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            //instancio el algoritmo que va a hacer la validacion del token
            var signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            //generamos el Token
            var jwtSecurityToken=  new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims:claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}