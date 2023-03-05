namespace WorkWaveAPI.ApiRequestModels
{
    public class AddPortfolioProjectModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public IFormFile PhotoFile { get; set;}
        public string ProjectCategoryName { get; set; }
    }
}
