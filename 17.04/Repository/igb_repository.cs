using _17._04.Models;

namespace _17._04.Repository
{
    public interface igb_repository
    {
        Task<user_model?> get_user_by_name_async(string login);
        Task add_user_async(user_model user);
        Task<bool> check_user_exist(string login);
        Task<List<message_model>> get_all_messages_async();
        Task add_message_async(message_model message);
        Task save_changes_async();
        IQueryable<user_model> get_all_users_query();
    }
}