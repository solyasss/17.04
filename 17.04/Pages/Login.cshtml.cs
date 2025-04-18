using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _17._04.Repository;
using _17._04.Services;
using _17._04.Models;

namespace _17._04.Pages
{
    public class LoginModel : PageModel
    {
        private readonly igb_repository _repo;
        private readonly ihash_service _hash;
        [BindProperty]
        public login_model model { get; set; } = new();

        public LoginModel(igb_repository repo, ihash_service hash)
        {
            _repo = repo;
            _hash = hash;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var user = await _repo.get_user_by_name_async(model.login!);
            if (user == null) { ModelState.AddModelError(string.Empty, "wrong login or password"); return Page(); }
            string user_hash = _hash.compute_hash(user.salt ?? "", model.password!);
            if (!string.Equals(user_hash, user.pwd, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError(string.Empty, "wrong login or password"); return Page();
            }
            HttpContext.Session.SetString("user_name", user.name ?? "");
            HttpContext.Session.SetInt32("user_id", user.id);
            HttpContext.Session.Remove("is_guest");
            return RedirectToPage("/Index");
        }
    }
}