using System.Text.RegularExpressions;

namespace CourseView.Shared;

public class SearchClassifier
{
    protected List<Regex> Regexes;

    public List<string>[] ClassifyMap { get; set; }

    protected void Initialize(string[] regexes)
    {
        for (int x = 0; x < regexes.Length; x++)
            this.Regexes.Add(new Regex(regexes[x],RegexOptions.Compiled | RegexOptions.IgnoreCase));

        this.ClassifyMap = new List<string>[this.Regexes.Count];

        for (int x = 0; x < this.ClassifyMap.Length; x++)
            this.ClassifyMap[x] = new List<string>();
    }

    public async Task ClassifyString(string str)
    {
        string[] splitString = str.Split(' '); // Tools.ContiguousWhitespaceSplit(str));
        
        for (int x = 0; x < splitString.Length; x++)
            // await Task.Run(() =>
            // {
                for (int y = 0; y < this.Regexes.Count; y++)
                    if (this.Regexes[y].Matches(splitString[x]).Count > 0)
                        this.ClassifyMap[y].Add(splitString[x]);
            // });
    }

    public bool IsEmpty()
    {
        for (int x = 0; x < this.ClassifyMap.Length; x++)
            if (this.ClassifyMap[x].Count > 0)
                return false;
        
        return true;
    }

    public void Clear()
    {
        for (int x = 0; x < this.ClassifyMap.Length; x++)
            this.ClassifyMap[x] = new List<string>();
    }

    public SearchClassifier(string[] regex)
    {
        this.Regexes = new List<Regex>();
        
        this.Initialize(regex);
    }
}

public class CourseParametersClassifier : SearchClassifier
{
    public CourseParametersClassifier() : base(new string[] { @"^[0-9]{6}$", @"^[a-zA-Z]{4}$", @"^[0-9]{4}$", @"^[a-zA-Z]" })
    {
    }
}
