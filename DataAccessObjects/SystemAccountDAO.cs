using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class SystemAccountDAO
    {
        public static async Task<List<SystemAccount>> GetAccounts()
        {
            var systemAccounts = new List<SystemAccount>();

            try
            {
                using (var context = new FunewsManagementContext())
                {
                    systemAccounts = await context.SystemAccounts.ToListAsync();
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return systemAccounts;
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
    }
}
