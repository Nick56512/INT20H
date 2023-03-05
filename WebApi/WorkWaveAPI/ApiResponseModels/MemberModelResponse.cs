using DAL.Models;

namespace WorkWaveAPI.ApiResponseModels
{
    public class MemberModelResponse
    {
        public User User { get; set; }
        public int MemberId { get; set; }
        public string ProjectName { get; set; }
        public int ProjectId { get; set; }
    }
}
