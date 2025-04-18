using Microsoft.AspNetCore.Mvc.RazorPages;
using _17._04.Repository;
using _17._04.Models;
using System.Linq;

namespace _17._04.Pages
{
    public class MessagesModel : PageModel
    {
        private readonly igb_repository _repo;
        public IList<msg_vm> messages { get; private set; } = new List<msg_vm>();

        public MessagesModel(igb_repository repo)
        {
            _repo = repo;
        }

        public async Task OnGetAsync()
        {
            var msgs = await _repo.get_all_messages_async();
            var users = _repo.get_all_users_query().ToDictionary(u => u.id, u => u.name);

            messages = msgs.Select(m => new msg_vm(users.GetValueOrDefault(m.id_user) ?? "unknown",
                    m.message ?? "",
                    m.message_date))
                .ToList();
        }
    }
}