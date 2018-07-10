using Discord;
using System.Linq;
using System.Threading.Tasks;

namespace NuljiBot.Helpers
{
    public static class InformationHelper
    {
        public async static Task<int> GetNbUsers(IGuild guild)
        {
            var users = await guild.GetUsersAsync();
            return users.Count;
        }

        public async static Task<int> GetNbHumans(IGuild guild)
        {
            var users = await guild.GetUsersAsync();
            return users.Where(o => !o.IsBot).Count();
        }

        public async static Task<int> GetNbBots(IGuild guild)
        {
            var users = await guild.GetUsersAsync();
            return users.Where(o => o.IsBot).Count();
        }

        public async static Task<int> GetNbOnline(IGuild guild)
        {
            var users = await guild.GetUsersAsync();
            return users.Where(o => o.Status != UserStatus.Offline).Count();
        }
    }
}