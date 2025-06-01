using Ardalis.Specification;
using InterfazPFMCFIC.Models;
using InterfazPFMCFIC.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;



public class SeguimientoSolicitudModel : PageModel
{
    private readonly IRepositoryBase<InterfazPfmCficConfirmacionEnvio> _repo;

    public SeguimientoSolicitudModel(IRepositoryBase<InterfazPfmCficConfirmacionEnvio> repo)
    {
        _repo = repo;
    }

    [BindProperty(SupportsGet = true)]
    public int ActoID { get; set; }

    public List<InterfazPfmCficConfirmacionEnvio> Confirmaciones { get; set; } = new();

    public async Task OnGetAsync()
    {
        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
        }
        var spec = new ConfirmacionEnvioPorActoIdSpec(ActoID);
        Confirmaciones = (await _repo.ListAsync(spec)).ToList();
    }
}