using System;

namespace TwicasDotNet
{
    public class UserObject
    {
        public User user { get; set; }
        

        public class User
        {
            public string id { get; set; }
            public string screen_id { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public string profile { get; set; }
            public int level { get; set; }
            public string last_movie_id { get; set; }
            public bool is_live { get; set; }
            public int created { get; set; }
        }
    }
}