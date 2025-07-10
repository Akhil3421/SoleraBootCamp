namespace MovieDirectorWebClient.Models
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public List<int> DirectorIds { get; set; }
    }

    public class DirectorDTO
    {
        public int DirId { get; set; }
        public string Name { get; set; }
        public List<MovieDTO> Movies { get; set; }
    }

}
