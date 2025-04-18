using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _17._04.Repository;
using _17._04.Models;
using System.Linq;

namespace _17._04.Pages
{
    public class IndexModel : PageModel
    {
        private readonly igb_repository _repo;
        public IList<msg_vm> messages { get; private set; } = new List<msg_vm>();
        public string welcome_message { get; private set; } = "";
        public bool can_post { get; private set; }

        [BindProperty]
        [Required(ErrorMessage = "message cannot be empty")]
        public string new_message { get; set; } = "";

        public IndexModel(igb_repository repo)
        {
            _repo = repo;
        }

        public async Task OnGetAsync() => await load_state();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await load_state();
                return Page();
            }

            int? uid = HttpContext.Session.GetInt32("user_id");
            if (uid == null)
            {
                ModelState.AddModelError(string.Empty, "you are not authorised");
                await load_state();
                return Page();
            }

            await _repo.add_message_async(new message_model
            {
                id_user = uid.Value,
                message = new_message.Trim(),
                message_date = DateTime.Now
            });
            await _repo.save_changes_async();

            new_message = "";
            ModelState.Clear();
            await load_state();
            return Page();
        }

        private async Task load_state()
        {
            var msgs = await _repo.get_all_messages_async();
            var users = _repo.get_all_users_query().ToDictionary(u => u.id, u => u.name);

            messages = msgs.Select(m => new msg_vm(users.GetValueOrDefault(m.id_user) ?? "unknown",
                                                   m.message ?? "",
                                                   m.message_date))
                           .ToList();

            bool is_user = !string.IsNullOrEmpty(HttpContext.Session.GetString("user_name"));
            bool is_guest = !is_user && HttpContext.Session.GetString("is_guest") == "true";

            welcome_message = is_user
                ? $"hello, {HttpContext.Session.GetString("user_name")}!"
                : is_guest ? "hello, guest!" : "hello!";
            can_post = is_user;
        }
    }
}
