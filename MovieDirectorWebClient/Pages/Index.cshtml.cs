using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDirectorWebClient.Models;
using MovieDirectorWebClient.Services;

public class IndexModel : PageModel
{
    private readonly ApiService _apiService;

    public IndexModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public List<MovieDTO> Movies { get; set; } = new();
    public List<DirectorDTO> Directors { get; set; } = new();

    public async Task OnGetAsync()
    {
        Movies = await _apiService.GetMoviesAsync() ?? new List<MovieDTO>();
        Directors = await _apiService.GetDirectorsAsync() ?? new List<DirectorDTO>();
    }
}
