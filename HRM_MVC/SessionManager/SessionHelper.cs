using System.Text.Json;

namespace HRM_MVC.SessionManager
{
    public class SessionHelper
    {
        public static void SerializeObjectToSession<T>(ISession session, T obj, string key)
        {
            if (obj == null)
            {
                return;
            }
            string value = JsonSerializer.Serialize<T>(obj);
            session.SetString(key, value);
        }
        public static T? GetObjectFromSession<T>(ISession session, string key)
        {
           string? value = session.GetString(key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}
