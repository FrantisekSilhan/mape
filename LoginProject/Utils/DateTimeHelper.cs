namespace LoginProject.Utils {
    public class DateTimeHelper {
        public static string ConvertTimeToString(DateTime time) {
            TimeSpan timeSpan = DateTime.UtcNow - time;

            if (timeSpan.TotalMinutes < 1)
                return "Just now";
            else if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes}m";
            else if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours}h";
            else {
                if (time.Year != DateTime.Now.Year) {
                    return time.ToString("dd.MM.yyyy");
                } else {
                    return time.ToString("dd.MM.");
                }
            }
        }
    }
}
