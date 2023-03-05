namespace WorkWaveAPI.ApiRequestModels
{
    public class RegistrationModel
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double WorkExperience { get; set; }
        public string UserDescription { get; set; }

        public IFormFile Avatar { get; set; }
    }
}
