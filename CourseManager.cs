using System.Collections;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml;
using CourseView.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CourseView;

public enum EndPointType
{ 
    All,
    Query,
    QueryMatch,
    TermGet
}

public class CourseManager
{
    protected static HttpClient WebClient;
    
    public static CourseViewConfig Config;

    public static Course[] Courses;

    public static Term[] Terms;
    
    public static string DefaultConfigFilePath = "config.json";

    public static string BaseURL = "http://localhost:5157";
    
    public static string[] APIUrls = new string[] {
        $"{CourseManager.BaseURL}/courses",
        $"{CourseManager.BaseURL}/courses/query",
        $"{CourseManager.BaseURL}/courses/querymatch",
        $"{CourseManager.BaseURL}/terms",
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

    public static async Task<Term[]> GetTerms()
    {
        string[] termStrings = JsonSerializer.Deserialize<string[]>(JsonSerializer.Deserialize<Hashtable>(await CourseManager.WebClient.GetStringAsync(CourseManager.APIUrls[(int)EndPointType.TermGet]))["Payload"].ToString());

        Term[] terms = new Term[termStrings.Length];

        for (int x = 0; x < termStrings.Length; x++)
            terms[x] = new Term(termStrings[x]);
        
        return (CourseManager.Terms = terms);
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
    
    public static async Task<Course[]> FetchCoursesByQueryMatch(Hashtable queryHash)
    {
        CourseManager.WebClient = new HttpClient();
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, CourseManager.APIUrls[(int)EndPointType.QueryMatch]);

        Hashtable response, queryHashFormatted = new Hashtable();
    
        foreach (string key in queryHash.Keys)
            if (queryHash[key] != null)
            {
                if ((queryHash[key] is int && (int)queryHash[key] == 0))
                    continue;
                
                queryHashFormatted.Add(key, queryHash[key]);
            }

        requestMessage.Headers.Add("query", JsonConvert.SerializeObject(queryHashFormatted));
        
        Console.WriteLine(JsonConvert.SerializeObject(requestMessage));
        
        response = JsonSerializer.Deserialize<Hashtable>(await (await CourseManager.WebClient.SendAsync(requestMessage)).Content.ReadAsStringAsync());
        
        if (response["Type"].ToString() == "0")
            return null;

        if (response["Payload"] != null)
            CourseManager.Courses = JsonSerializer.Deserialize<Course[]>(response["Payload"].ToString());  
        
        return CourseManager.Courses;
    }

    public static async Task<Course[]> FetchCourses()
    {
        CourseManager.WebClient = new HttpClient();
        
        return await Task.Run(async ()=>
        {
            return JsonSerializer.Deserialize<Course[]>(
                    (await JsonSerializer.DeserializeAsync<Hashtable>((await CourseManager.WebClient.GetStreamAsync($"http://localhost:5157/courses"))))["Payload"].ToString());
        }); 
    }


    public static void Initialize()
    {
        CourseManager.WebClient = new HttpClient();
    }
} 
