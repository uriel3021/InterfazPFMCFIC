using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize]
public class TestJwtModel : PageModel
{
    public void OnGet()
    {
        Console.WriteLine("�Entr� a TestJwt con JWT v�lido!");
    }
}