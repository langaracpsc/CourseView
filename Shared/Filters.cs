using System.Collections;

namespace CourseView.Shared;

public struct Filters
{
    public string Term { get; set; }

    public string Subject { get; set; }

    public string CourseNumber { get; set; }

    public static Filters FromQueryString(string queryString)
    {
        string[] split = queryString.Split(" ");

        Console.WriteLine(queryString);
        
        return new Filters(split[0], split[1], split[2]);
    }

    public Hashtable ToHashmap()
    {
        Hashtable hashMap = new Hashtable();

        hashMap.Add("Term", this.Term);
        hashMap.Add("Subj", this.Subject);
        hashMap.Add("CourseNumber", this.CourseNumber);
        
        return hashMap;
    }

    public Filters(string term = null, string subject = null, string courseNumber = null)
    {
        this.Term = term;
        this.Subject = subject;
        this.CourseNumber = courseNumber;
    }
}

