using Microsoft.AspNetCore.Components;

namespace CourseView.Shared;

public partial class CourseItem : ComponentBase
{
    [Parameter] 
    public Course Instance { get; set; }

    [Parameter] 
    public bool Visible { get; set; }


    public CourseItem()
    {
    }
}

