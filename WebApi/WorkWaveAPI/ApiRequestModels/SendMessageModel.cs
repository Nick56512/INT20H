namespace WorkWaveAPI.ApiRequestModels
{
    public class SendMessageModel
    {
        public string UserId { get; set; }  
        public string Message { get; set; }
        public int ChatId { get; set; }

    }
}
