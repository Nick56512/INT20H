namespace WorkWaveAPI.Managers
{
    public class FileManager
    {
        public static async Task<string> CopyToAsync(IFormFile file, string uploads)
        {
            string filePath = Path.Combine(uploads, file.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return filePath;
        }
    }
}
