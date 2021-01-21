using ChurrasTrinca.Domain.Entities;
using ChurrasTrinca.Domain.Interfaces.Repository;
using ChurrasTrinca.Domain.Interfaces.Services;
using ChurrasTrinca.Domain.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ChurrasTrinca.Services
{
    public class LoginService : ILoginService
    {
        private ILoginRepository _loginRepository;
        private SigningConfiguration _signingConfiguration;
        private TokenConfiguration _tokenConfiguration;
        private IConfiguration _configuration { get; }

        public LoginService(ILoginRepository loginRepository, SigningConfiguration signingConfiguration, TokenConfiguration tokenConfiguration, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginEntity loginEntity)
        {
            var user = new LoginEntity();
            if (loginEntity != null && !string.IsNullOrWhiteSpace(loginEntity.Email) && !string.IsNullOrWhiteSpace(loginEntity.Name) && !string.IsNullOrWhiteSpace(loginEntity.Password))
            {
                user = await _loginRepository.FindByLogin(loginEntity.Email, loginEntity.Name, loginEntity.Password);
                
                if(user == null)
                {
                    return new 
                    { 
                        authenticated = false,
                        message = "Falha ao autenticar: verifique se o email, nome e senha estão corretos"
                    };
                }
                else
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(loginEntity.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, "1"),
                            new Claim(JwtRegisteredClaimNames.UniqueName, loginEntity.Name),
                            new Claim(JwtRegisteredClaimNames.UniqueName, loginEntity.Email)
                        });

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    return SuccessObject(createDate, expirationDate, token, user);
                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar: verifique se o email, nome e senha estão corretos"
                };
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                Audience = Environment.GetEnvironmentVariable("Audience"),
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginEntity loginEntity)
        {
            return new
            {
                authenticated = true,
                create = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                logador = loginEntity.Email,
                message = $"{loginEntity.Name} Logado com sucesso"
            };
        }
    }
}
