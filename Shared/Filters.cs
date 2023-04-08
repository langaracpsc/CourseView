using System.Collections;

namespace CourseView.Shared;

public struct Filters
{
    public string Term { get; set; }

    public string Subject { get; set; }

    public int CourseNumber { get; set; }

    public static Filters FromQueryString(string queryString)
    {
        string[] split = queryString.Split(" ");

        Console.WriteLine(queryString);
        
        return new Filters(split[0], split[1], int.Parse(split[2]));
    }

    public Hashtable ToHashmap()
    {
        Hashtable hashMap = new Hashtable();

        hashMap.Add("Term", (this.Term == null) ? this.Term : $"%{this.Term}%");
        hashMap.Add("Subj", (this.Subject == null) ? this.Subject : $"%{Tools.ToUpper(this.Subject)}%");
        hashMap.Add("CourseNumber", this.CourseNumber);
        
        return hashMap;
    }

    public bool IsNull()
    {
        return (this.Term == null && this.Subject == null && this.CourseNumber == 0);
    }

    public Filters(string term = null, string subject = null, int courseNumber = 0)
    {
        this. Term = term;
        this.Subject = subject;
        this.CourseNumber = courseNumber;
    }
}
 

