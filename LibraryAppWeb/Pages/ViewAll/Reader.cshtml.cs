using LibraryAppWeb.Handlers;
using LibraryAppWeb.Features;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryAppWeb.Pages.ViewAll
{
    [Authorize]
    public class ReaderModel : PageModel
    {
        public Dictionary<long, Reader> Readers = 
        new Dictionary<long, Reader>();

        public async Task<IActionResult> OnGetAsync()
        {
            var selectResult = await ReaderBaseHandler.GetAllReaders();

            if (!selectResult.success)
                return RedirectToPage("/ControlPanel", new { errorMessage = $"Ошибка при выполнении запроса:\n{selectResult.exception}" });

            var dbReader = selectResult.reader;

            try
            {
                while (await dbReader.ReadAsync())
                {
                    var readerId = dbReader.GetInt64(0);
                    var lastName = dbReader.GetString(1);
                    var firstName = dbReader.GetString(2);
                    var patronymic = dbReader.GetString(3);
                    var issuedDate = dbReader.GetDateTime(4);
                    var birthdate = dbReader.GetDateTime(5);

                    var humanReader = new Reader(lastName, firstName,
                    patronymic, issuedDate, birthdate);

                    Readers.Add(readerId, humanReader);
                }

                await dbReader.CloseAsync();
                return Page();

            }
            catch (Exception ex)
            {
                await dbReader.CloseAsync();
                return RedirectToPage("/ControlPanel", new { errorMessage = $"Ошибка во время чтения дата базы:\n{ex}"});
            }
            
        }
    }
}