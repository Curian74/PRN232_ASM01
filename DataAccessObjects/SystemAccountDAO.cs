using BusinessObjects;
using DataAccessObjects.Dtos;
using DataAccessObjects.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessObjects
{
    public class SystemAccountDAO
    {
        public static async Task<List<SystemAccountDto>> GetAccounts(SystemAccountQuery systemAccountQuery)
        {
            try
            {
                using (var context = new FunewsManagementContext())
                {
                    var accounts = context.SystemAccounts.AsQueryable();

                    if (!string.IsNullOrEmpty(systemAccountQuery.SearchTerm))
                    {
                        accounts = accounts.Where(a =>
                            (a.AccountName != null && a.AccountName.Contains(systemAccountQuery.SearchTerm)) ||
                            (a.AccountEmail != null && a.AccountEmail.Contains(systemAccountQuery.SearchTerm)));
                    }

                    var dtoEntities = await accounts.Select(x => new SystemAccountDto
                    {
                        AccountId = x.AccountId,
                        AccountEmail = x.AccountEmail,
                        AccountName = x.AccountName,
                        AccountPassword = x.AccountPassword,
                        AccountRole = x.AccountRole,
                    })
                        .ToListAsync();

                    return dtoEntities;
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task DeleteAccount(short id)
        {
            try
            {
                using (var context = new FunewsManagementContext())
                {
                    var account = await context.SystemAccounts
                        .Include(a => a.NewsArticles)
                        .FirstOrDefaultAsync(a => a.AccountId == id);

                    if (account == null)
                    {
                        throw new Exception("Cannot find the account.");
                    }

                    if (account.NewsArticles.Count > 0)
                    {
                        throw new InvalidOperationException("Cannot delete an account with new articles posted.");
                    }

                    context.SystemAccounts.Remove(account);
                    context.SaveChanges();
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<SystemAccount> FindUserByEmailAndPassword(string email, string password, IConfiguration _configuration)
        {
            try
            {
                var adminEmail = _configuration["AdminAccount:AccountEmail"];
                var adminPassword = _configuration["AdminAccount:AccountPassword"];
                var adminName = _configuration["AdminAccount:AccountName"];
                var adminRoleStr = _configuration["AdminAccount:AccountRole"];

                int.TryParse(adminRoleStr, out int adminRole);

                using var context = new FunewsManagementContext();

                // Check if admin account needs to be inserted
                var existingAdmin = await context.SystemAccounts
                    .FirstOrDefaultAsync(a => a.AccountEmail == adminEmail);

                if (existingAdmin == null)
                {

                    int latestAccountId = await context.SystemAccounts
                        .OrderByDescending(x => x.AccountId)
                        .Select(x => (int)x.AccountId)
                        .FirstOrDefaultAsync();

                    var newAdmin = new SystemAccount
                    {
                        AccountId = (short)(latestAccountId + 1),
                        AccountEmail = adminEmail,
                        AccountPassword = adminPassword,
                        AccountName = adminName,
                        AccountRole = adminRole
                    };

                    await context.SystemAccounts.AddAsync(newAdmin);
                    await context.SaveChangesAsync();
                }

                // Find the login user normally
                var account = await context.SystemAccounts
                    .FirstOrDefaultAsync(a => a.AccountEmail == email && a.AccountPassword == password);

                if (account == null)
                {
                    throw new Exception("Invalid credentials.");
                }

                return account;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<SystemAccountDto> Create(CreateAccountDto dto)
        {
            using var context = new FunewsManagementContext();

            if (dto.ConfirmPass != dto.AccountPassword)
            {
                throw new Exception("Password must match!");
            }

            bool emailExists = await context.SystemAccounts
                .AnyAsync(x => x.AccountEmail == dto.AccountEmail);

            if (emailExists)
            {
                throw new Exception("Email already exists!");
            }

            int latestAccountId = await context.SystemAccounts
                .OrderByDescending(x => x.AccountId)
                .Select(x => (int)x.AccountId)
                .FirstOrDefaultAsync();

            var account = new SystemAccount
            {
                AccountId = (short)(latestAccountId + 1),
                AccountEmail = dto.AccountEmail,
                AccountName = dto.AccountName,
                AccountRole = dto.AccountRole,
                AccountPassword = dto.AccountPassword,
            };

            await context.SystemAccounts.AddAsync(account);
            await context.SaveChangesAsync();

            return new SystemAccountDto
            {
                AccountId = account.AccountId,
                AccountEmail = account.AccountEmail,
                AccountName = account.AccountName,
                AccountRole = account.AccountRole,
                AccountPassword = account.AccountPassword,
            };
        }

        public static async Task<SystemAccountDto> FindById(short id)
        {
            try
            {
                using (var context = new FunewsManagementContext())
                {
                    var account = await context.SystemAccounts
                        .Include(a => a.NewsArticles)
                        .FirstOrDefaultAsync(a => a.AccountId == id);

                    if (account == null)
                    {
                        return null;
                    }

                    return new SystemAccountDto
                    {
                        AccountId = account.AccountId,
                        AccountEmail = account.AccountEmail,
                        AccountName = account.AccountName,
                        AccountRole = account.AccountRole,
                        AccountPassword = account.AccountPassword,
                    };
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static async Task<SystemAccountDto> Edit(EditAccountDto dto)
        {
            using (var context = new FunewsManagementContext())
            {
                var account = await FindById(dto.AccountId);

                if (account == null)
                {
                    throw new KeyNotFoundException();
                }

                bool emailExists = await context.SystemAccounts
                .AnyAsync(x => x.AccountEmail == dto.AccountEmail && dto.AccountEmail != account.AccountEmail);

                if (emailExists)
                {
                    throw new Exception("Email already exists!");
                }

                if (dto.ConfirmPass != dto.AccountPassword)
                {
                    throw new InvalidOperationException("Passwords must match!");
                }

                var entity = new SystemAccount
                {
                    AccountId = dto.AccountId,
                    AccountEmail = dto.AccountEmail,
                    AccountName = dto.AccountName,
                    AccountRole = dto.AccountRole,
                    AccountPassword = dto.AccountPassword == null ? account.AccountPassword : dto.AccountPassword,
                };

                context.SystemAccounts.Update(entity);
                await context.SaveChangesAsync();

                return new SystemAccountDto
                {
                    AccountId = dto.AccountId,
                    AccountEmail = dto.AccountEmail,
                    AccountName = dto.AccountName,
                    AccountRole = dto.AccountRole,
                    AccountPassword = dto.AccountPassword
                };
            }
        }
    }
}
