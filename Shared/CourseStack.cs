using System.Collections;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
//using Newtonsoft.Json;
using System.Text.RegularExpressions;
using CourseView;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CourseView.Shared;



public partial class CourseStack
{
    public Filters CourseFilters;
    public Course[] Courses { get; set; }

    public string Content;

    public string QueryString;

    public HttpClient Client;

    public CourseParametersClassifier Classifier;

    public async Task UpdateAsync()
    {
        CourseManager.Courses = await CourseManager.FetchCourses();
    }

    public bool Initialized;

    public async ValueTask<ItemsProviderResult<Course>> GetCoures(ItemsProviderRequest request)
    {
        if (!this.Initialized)
            await this.OnInitializedAsync();

        return new ItemsProviderResult<Course>(CourseManager.Courses, this.Courses.Length);
    }

    public async Task Classify()
    {
        
        Console.WriteLine(JsonConvert.SerializeObject(this.Classifier.ClassifyMap));
    }

    public async Task Search()
    {
        await Task.Run(async () =>
        {
            this.ResultsVisible = (CourseManager.Courses.Length > 0);
            
            if (Tools.IsEmpty(this.QueryString))
                return;
            else
            {
                Filters filters;
                this.Classifier.Clear();
                await this.Classifier.ClassifyString(this.QueryString);
               
                filters =  new Filters((this.Classifier.ClassifyMap[0].Count != 0) ? this.Classifier.ClassifyMap[0][this.Classifier.ClassifyMap[0].Count - 1] : null,
                    (this.Classifier.ClassifyMap[1].Count != 0) ? this.Classifier.ClassifyMap[1][this.Classifier.ClassifyMap[1].Count - 1] : null,
                    (this.Classifier.ClassifyMap[2].Count != 0) ? int.Parse(this.Classifier.ClassifyMap[2][this.Classifier.ClassifyMap[2].Count - 1]) : 0);
                
                Console.WriteLine(JsonConvert.SerializeObject(this.Classifier.ClassifyMap));
                if (!filters.IsNull())
                    await CourseManager.FetchCoursesByQueryMatch(filters.ToHashmap());
            }

            this.StateHasChanged();
        });
    }

    protected override async Task OnInitializedAsync()
    {
        this.Classifier = new CourseParametersClassifier();
        
        if (CourseManager.Terms == null)
        {
            await CourseManager.GetTerms();
        }

        CourseManager.Courses = new Course[0];
            
        /*if (CourseManager.Courses == null)
        {
            Console.WriteLine("Enter");
            this.Content = await (new HttpClient().GetStringAsync("http://localhost:5157/courses"));
            CourseManager.Courses =
                JsonSerializer.Deserialize<Course[]>(JsonSerializer.Deserialize<Hashtable>(this.Content)["Payload"].ToString());
            Console.WriteLine("Exit");
        }*/

        this.Initialized = true;
    }

    protected override void OnInitialized()
    {
    }

    public CourseStack()
    {
    }
} 
