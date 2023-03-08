using System.Collections;
using Microsoft.AspNetCore.Components;
//using Newtonsoft.Json;
using System.Text.Json;
using CourseView;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace CourseView.Shared;

public partial class CourseStack : ComponentBase
{
    [Parameter] 
    public Course[] Courses { get; set; }

    public string Content;
    
    public async Task UpdateAsync()
    { 
        this.Courses = await CourseManager.FetchCourses();
    }

    public bool Initialized;
   
    public async ValueTask<ItemsProviderResult<Course>> GetCoures(ItemsProviderRequest request)
    {
        if (!this.Initialized)
            await this.OnInitializedAsync();
        
        return new ItemsProviderResult<Course>(this.Courses, this.Courses.Length);
    }
    
    
    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine("Enter");
        this.Courses = new Course[0]; 
        
        this.Content = await (new HttpClient().GetStringAsync("http://localhost:5157/courses"));
        this.Courses = JsonSerializer.Deserialize<Course[]>(JsonSerializer.Deserialize<Hashtable>(this.Content)["Payload"].ToString());

        this.Initialized = true;  
        Console.WriteLine("Exit");
    }

    public CourseStack()
    {
    }
} 
