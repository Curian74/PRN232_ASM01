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
    }
}
