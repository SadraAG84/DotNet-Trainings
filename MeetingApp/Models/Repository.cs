namespace MeetingApp.Models
{
    public static class Repository
    {
        private static List<UserInfo> _userInfos = new List<UserInfo>();

        static Repository()
        {
            // add some initial data
            _userInfos.Add(
                new UserInfo
                {
                    Name = "Alice Johnson",
                    Email = "alice@example.com",
                    Phone = "123-456-7890",
                    WillAttend = true,
                }
            );
            _userInfos.Add(
                new UserInfo
                {
                    Name = "Sadra Alizadeh",
                    Email = "sadra@example.com",
                    Phone = "111-222-3333",
                    WillAttend = true,
                }
            );
            _userInfos.Add(
                new UserInfo
                {
                    Name = "Bob Smith",
                    Email = "bob@example.com",
                    Phone = "555-666-7777",
                    WillAttend = false,
                }
            );
        }

        public static List<UserInfo> Users
        {
            get { return _userInfos; }
        }

        public static void CreateUser(UserInfo userInfo)
        {
            _userInfos.Add(userInfo);
        }
    }
}
