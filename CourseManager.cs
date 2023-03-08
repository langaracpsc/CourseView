using System.Collections;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace CourseView;

public class CourseManager
{
    protected static HttpClient WebClient;
    
    public static CourseViewConfig Config;

    public static Course[] Courses;

    public static string DefaultConfigFilePath = "config.json";
    
    public static void LoadConfig()
    {
        // CourseManager.Config = CourseViewConfig.LoadFromFile(CourseManager.DefaultConfigFilePath);
    }

    protected static List<Course> GetCoursesFromJson(string[] json)
    {
        return new List<Course>();
    }

    public static Course GetCourseBySubjectCode(string subject, int courseNumber)
    {
        for (int x = 0; x < CourseManager.Courses.Length; x++)
            if (CourseManager.Courses[x].Subject == subject && CourseManager.Courses[x].CourseNumber == courseNumber)
                return CourseManager.Courses[x];

        return null;
    }

    public static async Task<Course[]> FetchCourses()
    {
        CourseManager.WebClient = new HttpClient();
        // Console.WriteLine(JsonConvert.DeserializeObject<Hashtable>(CourseManager.WebClient.GetAsync($"http://localhost:5157/courses").Result.Content.ReadAsStringAsync().Result)["Payload"].ToString());

        // Console.WriteLine(CourseManager.WebClient.GetStringAsync($"http://localhost:5157/courses").Result);//.Content.ReadAsStringAsync().Result);
        
        return await Task.Run(async ()=>
        {
            return JsonConvert.DeserializeObject<Course[]>(
                    JsonConvert.DeserializeObject<Hashtable>(
                        (await CourseManager.WebClient.GetStringAsync($"http://localhost:5157/courses")))["Payload"]
                    .ToString());
        }); 
    }
}