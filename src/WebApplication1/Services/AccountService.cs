using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using WebApplication1.Authorization;
using WebApplication1.DataAccess.Models.Accounts;
using WebApplication1.Entities;
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


        private void removeOldRefreshTokens(Customer account)
        {
            account.RefreshTokens.RemoveAll(x =>
            !x.IsActive &&
            x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = await _repositoryWrapper.Customers.Include(x => x.ResetToken).AsNoTracking().FirstOrDefaultAsync(x => x.Email == model.Email);

            if (account == null || !account.IsVerified || !BCrypt.Net.BCrypt.Verify(model.NameSurname, account.NameSurname))
                throw new AppException("Email or password is incorrect");
            var jwtToken = _jwtUtils.GenerateJwtToken(account);
            var refreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);

            removeOldRefreshTokens(account);

            _repositoryWrapper.Customers.Update(account);

            await _repositoryWrapper.SaveChangesAsync();

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task<AccountResponse> Create(CreateRequest model)
        {
            var existingUserCount = await _repositoryWrapper.Customers.Where(x => x.Email == model.Email).CountAsync();

            if (existingUserCount > 0)
            {
                throw new AppException($"Email '{model.Email}' is already registered");
            }


            var account = _mapper.Map<Customer>(model);
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;

            account.NameSurname = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _repositoryWrapper.Customers.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

            return _mapper.Map<AccountResponse>(account);

        }

        public async Task Delete(int id)
        {
            var account = await getAccount(id);
            _repositoryWrapper.Customers.Remove(account);
            await _repositoryWrapper.SaveChangesAsync();
        }


      
        private async Task<string> generateResetToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            var tokenIsUnique = await _repositoryWrapper.Customers.AnyAsync(x => x.ResetToken == token);
            if (!tokenIsUnique)
            {
                return await generateResetToken();
            }
            return token;

        }
        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = _repositoryWrapper.Customers.Where(x => x.Email == model.Email).FirstOrDefault();
            if (account == null) return;
            account.ResetToken = await generateResetToken();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
            _repositoryWrapper.Customers.Update(account);
            await _repositoryWrapper.SaveChangesAsync();
        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            
            List<Customer> accounts = await _repositoryWrapper.Customers.ToListAsync();
            return _mapper.Map<IList<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse> GetById(int id)
        {
            var account = await getAccount(id);
            return _mapper.Map<AccountResponse>(account);
        }
        private async Task<Customer> getAccountByRefreshToken(string token)
        {
            var account = await _repositoryWrapper.Customers
                .Where(u => u.RefreshTokens.Any(t => t.Token == token))
                .FirstOrDefaultAsync(); // Используем встроенный LINQ метод.

            if (account == null)
                throw new AppException("InvalidToken");

            return account;
        }
        private async Task<RefreshToken> RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            // Генерируем новый refresh token
            var newRefreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);

            // Используем revokeRefreshToken с предыдущим refresh token
             RevokeRefreshToken(refreshToken, ipAddress, "Replace by new token", newRefreshToken.Token);

            return newRefreshToken;
        }

        private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.MinValue;
            token.RevokedByIp = ipAddress;
            token.ReasoneRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }


        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, Customer account, string ipAddress, string reason)
        {
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = account.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    RevokeRefreshToken(childToken, ipAddress, reason);
                else
                    // Предположим, что вы хотите передать доступ к определенному полю в Customer, например, Id или Email
                    RevokeRefreshToken(childToken, account.Email, ipAddress, reason); // Замените Email на нужное поле
            }
        }



        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var account = await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                revokeDescendantRefreshTokens(refreshToken, account, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                _repositoryWrapper.Customers.Update(account); // Здесь убираем await
                await _repositoryWrapper.SaveChangesAsync();
            }

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            var newRefreshToken = await RotateRefreshToken(refreshToken, ipAddress);
            account.RefreshTokens.Add(newRefreshToken);

            removeOldRefreshTokens(account);

            // Убираем await перед Update
            _repositoryWrapper.Customers.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

            var jwtToken = _jwtUtils.GenerateJwtToken(account);
            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;

            return response;
        }

        //private async Task<string> generateVerificationToken()
        //{
        //    var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

        //    // Проверяем, существует ли токен в базе данных
        //    var tokenIsUnique = !await _repositoryWrapper.Customers
        //        .Where(x => x.Token == token)
        //        .AnyAsync();

        //    // Если токен не уникален, генерируем новый
        //    if (tokenIsUnique)
        //        return token;

        //    return await generateVerificationToken(); // Повторная попытка
        //}

        private async Task<string> generateVerificationToken()
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            // Проверяем, существует ли токен в базе данных
            var tokenIsUnique = !await _repositoryWrapper.Customers
                .Where(x => x.ResetToken == token)
                .AnyAsync();

            // Если токен не уникален, генерируем новый
            if (tokenIsUnique)
                return token;

            return await generateVerificationToken(); // Повторная попытка
        }
        public async Task Register(RegisterRequest model, string origin)
        {
           var existingCustomerCount = await _repositoryWrapper.Customers
                .Where(x => x.Email == model.Email)
                .CountAsync();
            if (existingCustomerCount > 0) { 
            return;
            
            }
            var account = _mapper.Map<Customer>(model);

            var isFirstAccount = (await _repositoryWrapper.Customers.ToListAsync()).Count == 0;
            account.Role = isFirstAccount ? Role.Admin : Role.User;
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;
            account.VerificationToken = await generateVerificationToken();
            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password); 

            
            await _repositoryWrapper.Customers.AddAsync(account);
            await _repositoryWrapper.SaveChangesAsync();
        }

        private async Task<Customer> getAccountByResetToken(string token)
        {
            var account = await _repositoryWrapper.Customers
                .Where(x => x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow)
                .SingleOrDefaultAsync();

            if (account == null)
            {
                throw new AppException("Invalid token");
            }
            return account;
        }
        public async Task ResetPassword(ResetPasswordRequest model)
        {
            var account = await getAccountByResetToken(model.Token);
            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            await _repositoryWrapper.Customers.AddAsync(account);
            await _repositoryWrapper.SaveChangesAsync();

        }
        private async Task<Customer> getAccount(int id)
        {
            var account = (_repositoryWrapper.Customers.Where(x => x.CustomerId == id)).FirstOrDefault();
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        public Task RevokeToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountResponse> Update(int id, UpdateRequest model)
        {
            var account = await getAccount(id);

            if (await _repositoryWrapper.Customers.Where(x => x.Email == model.Email).CountAsync() > 0)
                throw new AppException($"Email '{model.Email} ' is already registered");

            if (!string.IsNullOrEmpty(model.Password))
                account.NameSurname = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _mapper.Map(model, account);
            account.Updated = DateTime.UtcNow;
            _repositoryWrapper.Customers.Update(account);
            await _repositoryWrapper.SaveChangesAsync();

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task ValidateResetToken(ValidateResetTokenRequest model)
        {
           await getAccountByResetToken(model.Token);
        }

        public async Task VerifyEmail(string token)
        {
            var account = (await _repositoryWrapper.Customers
          .Where(x => x.VerificationToken == token)
          .ToListAsync())
          .FirstOrDefault();

            if (account == null)
                throw new AppException("Verification failed");



            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;

            await _repositoryWrapper.Customers.AddAsync(account);
            await _repositoryWrapper.SaveChangesAsync();


        }
    }
}
