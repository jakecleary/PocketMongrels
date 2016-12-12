using System.Collections.Generic;

namespace JakeCleary.PocketMongrels.Api.Resourses
{
    public class Error
    {
        public string Message { get; set; }
        public Dictionary<string, string[]> List { get; set; }
    }
}
