using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize]
public class TestJwtModel : PageModel
{
    public void OnGet()
    {
        Console.WriteLine("¡Entró a TestJwt con JWT válido!");
    }
}