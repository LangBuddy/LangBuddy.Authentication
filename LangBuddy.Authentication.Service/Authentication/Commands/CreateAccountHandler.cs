﻿using LangBuddy.Authentication.Models.Commands;
using LangBuddy.Authentication.Service.Http.Common;
using LangBuddy.Authentication.Service.Mappers;
using MediatR;
using System.Text;

namespace LangBuddy.Authentication.Service.Authentication.Commands
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand>
    {
        private readonly IHttpService _httpService;

        public CreateAccountHandler(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            var accountCreateRequest = request.ToAccountCreateRequest(passwordHash, passwordSalt);

            var res = await _httpService.SendCreateAccountRequest(accountCreateRequest);

            if (!res)
                throw new ArgumentException("Registration error");
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
