namespace Nutaku.Unity
{
    /// <summary>
    /// Holds profile information for use with the People API
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Nutaku UserID (Basic info)
        /// </summary>
        public string id;

        /// <summary>
        /// Nickname (Basic info)
        /// </summary>
        public string nickname;

        /// <summary>
        /// Profile URL(Basic info)
        /// </summary>
        public string profileUrl;

        /// <summary>
        /// Standard thumbnail URL (Basic info)
        /// </summary>
        public string thumbnailUrl;

        /// <summary>
        /// User Type (Basic info)
        /// Null or empty string represents a normal player. Anything else means it's a staff or developer account, so payments should be ignored from financial metrics
        /// </summary>
        public string userType;

        /// <summary>
        /// User Grade
        /// 0: guest, 1: registered, 2: registered and has verified the email address
        /// </summary>
        public string grade;

        /// <summary>
        /// User's language code written in ISO639-1 format (the language he is viewing the site in)
        /// </summary>
        public string languagesSpoken;

        /// <summary>
        /// If queried, indicates if the user has an active subscription. "1" for yes, "0" for no.
        /// </summary>
        public string activeSub;

        public static class Fields
        {
            public static readonly string Id = "id";

            public static readonly string Nickname = "nickname";

            public static readonly string ProfileUrl = "profileUrl";

            public static readonly string ThumbnailUrl = "thumbnailUrl";

            public static readonly string UserType = "userType";

            public static readonly string Grade = "grade";

            public static readonly string LanguagesSpoken = "languagesSpoken";

            public static readonly string ActiveSub = "activeSub";
        }
    }
}
