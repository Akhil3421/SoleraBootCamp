using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDirectorWebClient.Models;
using MovieDirectorWebClient.Services;

public class EditModel : PageModel
{
    private readonly ApiService _apiService;

    public EditModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public MovieDTO Movie { get; set; } = new();

    [BindProperty]
    public List<int> SelectedDirectors { get; set; } = new();

    public List<DirectorDTO> AllDirectors { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Movie = await _apiService.GetMovieAsync(id);
        if (Movie == null)
            return NotFound();

        SelectedDirectors = Movie.DirectorIds ?? new();
        AllDirectors = await _apiService.GetDirectorsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Movie.DirectorIds = SelectedDirectors ?? new();
        var response = await _apiService.UpdateMovieAsync(Movie);

        if (response.IsSuccessStatusCode)
            return RedirectToPage("Index");

        AllDirectors = await _apiService.GetDirectorsAsync();
        return Page();
    }
}
