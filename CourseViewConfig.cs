using Newtonsoft.Json;

namespace CourseView
{
    public struct CourseViewConfig
    {
        public string ApiUrl;

        public static CourseViewConfig LoadFromFile(string path)
        {
            return JsonConvert.DeserializeObject<CourseViewConfig>(FileIO.ReadFile(path));
        }

        public CourseViewConfig(string apiurl)
        {
            this.ApiUrl = apiurl; 
        }
    }
}
