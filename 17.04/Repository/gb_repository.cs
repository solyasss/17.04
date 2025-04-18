using _17._04.Models;
using Microsoft.EntityFrameworkCore;

namespace _17._04.Repository
{
    public class gb_repository : igb_repository
    {
        private readonly user_context _db;

        public gb_repository(user_context db)
        {
            _db = db;
        }

        public async Task<user_model?> get_user_by_name_async(string login)
        {
            return await _db.users.FirstOrDefaultAsync(u => u.name == login);
        }

        public async Task add_user_async(user_model user)
        {
            await _db.users.AddAsync(user);
        }

        public async Task<bool> check_user_exist(string login)
        {
            return await _db.users.AnyAsync(u => u.name == login);
        }

        public async Task<List<message_model>> get_all_messages_async()
        {
            return await _db.messages.OrderByDescending(m => m.message_date).ToListAsync();
        }

        public async Task add_message_async(message_model message)
        {
            await _db.messages.AddAsync(message);
        }

        public async Task save_changes_async()
        {
            await _db.SaveChangesAsync();
        }

        public IQueryable<user_model> get_all_users_query()
        {
            return _db.users.AsQueryable();
        }
    }
}