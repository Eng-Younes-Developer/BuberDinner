using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Command.interfaces;
using Microsoft.IdentityModel.Tokens;
using BuberDinner.Application.Command.service;
using Microsoft.Extensions.Options;

namespace BuberDinner.infrastructure.Authintication
{
    public class JwtTokenGenerator : IJwtGenerator
    {
        private readonly IdataTimeProvider _dataTimeProvider;
        private readonly JwtSetting _jwtSetting;
        public JwtTokenGenerator(IdataTimeProvider dataTimeProvider, IOptions<JwtSetting> JwtOptions) 
        {
            _dataTimeProvider = dataTimeProvider;
            _jwtSetting = JwtOptions.Value; //object of JwtSetting class is created and values are assigned from appsettings.json file //هي الخاصية اللي بتعطيك القيمة الفعلية للإعدادات


        }   
        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            //token = header.payload.signature
            //generate signing credentials and convert string (Super-secert-key ) to Bytes 
            //same key for signing and checking signing
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret)), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, firstName), 
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // id token
               };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSetting.Issuer,
                audience: _jwtSetting.Audience,
                claims: claims,
                expires: _dataTimeProvider.UtcNow.AddMinutes(_jwtSetting.ExpiryMinutes), //token expires after 60 minutes
                signingCredentials: signingCredentials);
            
            return new JwtSecurityTokenHandler().WriteToken(securityToken); //convert token to string to send in http request 

        }
    }
}
