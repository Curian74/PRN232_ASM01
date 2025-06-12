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

                    short latestAccountId = await context.SystemAccounts
                        .OrderByDescending(x => x.AccountId)
                        .Select(x => x.AccountId)
                        .FirstOrDefaultAsync();

                    var newAdmin = new SystemAccount
                    {
                        AccountId = latestAccountId,
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
            try
            {
                using var context = new FunewsManagementContext();

                if (dto.ConfirmPass != dto.AccountPassword)
                {
                    throw new Exception("Password must match!");
                }

                {
                    var account = new SystemAccount
                    {
                        AccountId = dto.AccountId,
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
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
