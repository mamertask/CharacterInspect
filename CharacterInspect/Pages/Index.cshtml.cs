using System.Threading.Tasks;
using CharacterInspect.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

public class IndexModel : PageModel
{
    private readonly IBlizzardApiService _blizzardApiService;

    public IndexModel(IBlizzardApiService blizzardApiService)
    {
        _blizzardApiService = blizzardApiService;
    }

    [BindProperty]
    public string Region { get; set; }
    [BindProperty]
    public string Realm { get; set; }

    [BindProperty]
    public string CharacterName { get; set; }

    public JObject CharacterProfile { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            CharacterProfile = await _blizzardApiService.GetCharacterProfileAsync(Region, Realm, CharacterName);

            if (CharacterProfile != null)
            {
                //
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to fetch character profile data. Please try again.");
            }
        }

        return Page();
    }
}
