using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize]
public class ActoFormModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int ActoID { get; set; }

    public void OnGet()
    {
        // Aquí puedes usar ActoID recibido por query string
    }
}