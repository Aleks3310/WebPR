using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using WebPR.Data;
using WebPR.Models;
using WebPR.Hash;


namespace WebPR.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }


        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public void OnGet()
		{

		}

        public IActionResult OnPost()
        {
            string hash = HashPassword.Hash(Password);
            // Ищем пользователя с таким логином и паролем в базе данных
            var user = _context.Users.FirstOrDefault(u => u.Login == Login && u.Password == hash);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                if (user.Role == 1)
                {
                    Console.WriteLine($"UserId из TempData: {TempData["UserId"]}");
                    return RedirectToPage("/HomePage");  // Переходим на главную страницу
                }
                else
                {
                    return RedirectToPage("/ModeratorPage");  // Переходим на страницу модератора
                }
            }
            else
            {
                // Если пользователь не найден, добавляем ошибку в ModelState
                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль.");
                return Page(); // Возвращаем ту же страницу с ошибкой
            }
        }

    }
}
