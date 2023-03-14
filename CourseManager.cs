using System.Collections;
using System.Net;
using System.Runtime.CompilerServices;
using System.Xml;
using CourseView.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CourseView;

public enum EndPointType
{ 
    All,
    Query
}

public class CourseManager
{
    protected static HttpClient WebClient;
    
    public static CourseViewConfig Config;

    public static Course[] Courses;

    public static string DefaultConfigFilePath = "config.json";

    public static string[] APIUrls = new string[] {
        "http://localhost:5157/courses",
        "http://localhost:5157/courses/query"
    };
    
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
    public static async Task<Course[]> FetchCoursesByQuery(Hashtable queryHash)
    {
        CourseManager.WebClient = new HttpClient();
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, CourseManager.APIUrls[(int)EndPointType.Query]);

        Hashtable response;



        requestMessage.Headers.Add("query", JsonConvert.SerializeObject(queryHash));


        Console.WriteLine(JsonConvert.SerializeObject(requestMessage));
        
        response = JsonSerializer.Deserialize<Hashtable>(await (await CourseManager.WebClient.SendAsync(requestMessage)).Content.ReadAsStringAsync());

        if (response["Type"].ToString() == "0")
            return null;

        CourseManager.Courses = JsonSerializer.Deserialize<Course[]>(response["Payload"].ToString());  
        
        return CourseManager.Courses;
    }

    public static async Task<Course[]> FetchCourses()
    {
        CourseManager.WebClient = new HttpClient();
        // Console.WriteLine(JsonConvert.DeserializeObject<Hashtable>(CourseManager.WebClient.GetAsync($"http://localhost:5157/courses").Result.Content.ReadAsStringAsync().Result)["Payload"].ToString());

        // Console.WriteLine(CourseManager.WebClient.GetStringAsync($"http://localhost:5157/courses").Result);//.Content.ReadAsStringAsync().Result);
        
        return await Task.Run(async ()=>
        {
            return JsonSerializer.Deserialize<Course[]>(
                    JsonSerializer.Deserialize<Hashtable>(
                        (await CourseManager.WebClient.GetStringAsync($"http://localhost:5157/courses")))["Payload"]
                    .ToString());
        }); 
    }
}