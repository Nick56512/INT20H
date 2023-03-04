namespace WorkWaveAPI.ApiRequestModels
{
    public class AddMemberToProjectModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; }
    }
}
