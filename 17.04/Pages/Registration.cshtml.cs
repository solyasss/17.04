using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _17._04.Repository;
using _17._04.Services;
using _17._04.Models;

namespace _17._04.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly igb_repository _repo;
        private readonly ihash_service _hash;
        [BindProperty]
        public register_model model { get; set; } = new();

        public RegistrationModel(igb_repository repo, ihash_service hash)
        {
            _repo = repo;
            _hash = hash;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (await _repo.check_user_exist(model.login!))
            {
                ModelState.AddModelError(string.Empty, "this login already exists"); return Page();
            }
            string salt = _hash.generate_salt();
            string pwd = _hash.compute_hash(salt, model.password!);
            await _repo.add_user_async(new user_model { name = model.login, pwd = pwd, salt = salt });
            await _repo.save_changes_async();
            return RedirectToPage("/Login");
        }
    }
}