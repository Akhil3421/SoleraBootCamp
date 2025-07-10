using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDirectorWebClient.Models;
using MovieDirectorWebClient.Services;

public class CreateModel : PageModel
{
    private readonly ApiService _apiService;

    public CreateModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public MovieDTO Movie { get; set; } = new MovieDTO();

    [BindProperty]
    public DirectorDTO Director { get; set; } = new DirectorDTO();

    [BindProperty]
    public List<int> SelectedDirectors { get; set; } = new List<int>();

    public List<DirectorDTO> AllDirectors { get; set; } = new List<DirectorDTO>();

    public async Task OnGetAsync()
    {
        AllDirectors = await _apiService.GetDirectorsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        //if (!ModelState.IsValid)
        //{
        //    // Re-fetch directors in case of error
        //    AllDirectors = await _apiService.GetDirectorsAsync();
        //    return Page();
        //}

        Movie.DirectorIds = SelectedDirectors ?? new List<int>();

        var response = await _apiService.CreateMovieAsync(Movie);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("Index");
        }

        // Re-fetch directors in case of failure
        //AllDirectors = await _apiService.GetDirectorsAsync();

        return Page();
    }
}
