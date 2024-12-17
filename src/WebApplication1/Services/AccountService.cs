using MapsterMapper;
using Microsoft.Extensions.Options;
using WebApplication1.Authorization;
using WebApplication1.DataAccess.Models.Accounts;
using WebApplication1.Helpers;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class AccountService : IAccountService
    {
        private readonly modelsContext _repositoryWrapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public AccountService(modelsContext repositoryWrapper, 
            IJwtUtils jwtUtils,
            IMapper mapper,
            AppSettings appSettings,
            IEmailService emailService)
        {
            _repositoryWrapper = repositoryWrapper;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings;
            _emailService = emailService;
        }

        public Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> Create(CreateRequest model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task Register(RegisterRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public Task ResetPassword(ResetPasswordRequest model)
        {
            throw new NotImplementedException();
        }

        public Task RevokeToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> Update(int id, UpdateRequest model)
        {
            throw new NotImplementedException();
        }

        public Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            throw new NotImplementedException();
        }

        public Task VerifyEmail(string token)
        {
            throw new NotImplementedException();
        }
    }
}
