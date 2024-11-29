namespace ydai5.Domain
{
    public class DatabaseUser
    {
        public string CurrentUser { get; set; } = string.Empty;
        public string SystemUser { get; set; } = string.Empty;
        public string SessionUser { get; set; } = string.Empty;
    }
}