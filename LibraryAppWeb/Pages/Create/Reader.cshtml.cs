using LibraryAppWeb.Features;
using LibraryAppWeb.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace LibraryAppWeb.Pages.Create
{
    [Authorize]
    public class ReaderModel : PageModel
    {
        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string Patronymic { get; set; }

        [BindProperty]
        public DateTime IssuedDate { get; set; } = DateTime.Now;

        [BindProperty]
        public DateTime Birthday { get; set; } = DateTime.Now;

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(FirstName) ||
                string.IsNullOrWhiteSpace(Patronymic))
            {
                ErrorMessage = "Ни одно из полей не должно быть пустым!";
                return Page();
            }

            var reader = new Reader(LastName, FirstName, Patronymic, IssuedDate, Birthday);

            var result = await ReaderBaseHandler.AddReaderAsync(reader);

            if (!result.success)
            {
                ErrorMessage = $"Ошибка при выполнение запроса: {result.exception}";
                return Page();
            }

            return RedirectToPage("/ControlPanel", new { successMessage = "Читатель был успешно добавлен!" });
        }
    }
}
