using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDirectorWebClient.Models;
using MovieDirectorWebClient.Services;

public class CreateDirectorModel : PageModel
{
    private readonly ApiService _apiService;

    public CreateDirectorModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public DirectorDTO Director { get; set; } = new DirectorDTO();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        Director.Movies = new List<MovieDTO>();

        var response = await _apiService.CreateDirectorAsync(Director);

        if (response.IsSuccessStatusCode)
            return RedirectToPage("Index");

        return Page();
    }
}
