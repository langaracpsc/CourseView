using System.Collections;
using Microsoft.AspNetCore.Components;
//using Newtonsoft.Json;
using System.Text.Json;
using CourseView;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace CourseView.Shared;

public partial class CourseStack
{
    public Filters CourseFilters;

    protected override void OnInitialized()
    {
    }

    public CourseStack()
    {
    }
} 
