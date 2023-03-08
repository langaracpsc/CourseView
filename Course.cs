using Newtonsoft.Json;

namespace CourseView
{
    public class Course
    {
        public int Seats { get; set; }

        public int Waitlist { get; set; }

        public int CRN { get; set; }

        public string Location { get; set; }

        public string Subject { get; set; }

        public string Term { get; set; }

        public int CourseNumber { get; set; }

        public string Section { get; set; }

        public double Credits { get; set; }

        public string Title { get; set; }

        public double Fees { get; set; }

        public int RptLimit { get; set; }

        public string Type { get; set; }

        public string Instructor { get; set; }

        public DayOfWeek[] Schedule { get; set; }

        public Time StartTime { get; set; }

        public Time EndTime { get; set; }

        public Course FromRawString(string str)
        {
            return new Course();
        }
        
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public bool IsNull()
        {
            return (this.Seats == 0 &&
                    this.Waitlist == 0 &&
                    this.CRN == null &&
                    this.Location == null &&
                    this.Subject == null &&
                    this.Credits == 0 &&
                    this.Title == null &&
                    this.Fees == 0 &&
                    this.RptLimit == 0 &&
                    this.Type == null &&
                    this.Instructor == null &&
                    this.Schedule == null &&
                    this.StartTime == null &&
                    this.EndTime == null);
        }

    public Course(string term = null, int seats = 0, int waitlist = 0, int crn = 0, string location = null, string subject = null,
            int courseNumber = 0, string section = null, double credits = 0, string title = null, double fees = 0, int rptLimit = 0,
            string type = null, DayOfWeek[] schedule = null, Time startTime = null,
            Time endTime = null, string instructor = null)
        {
            this.Term = term;
            this.Seats = seats;
            this.Waitlist = waitlist;
            this.CRN = crn;
            this.Schedule = schedule;
            this.Location = location;
            this.Subject = subject;
            this.CourseNumber = courseNumber;
            this.Section = section;
            this.Credits = credits;
            this.Title = title;
            this.RptLimit = rptLimit;
            this.Type = type;
            this.Fees = fees;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Instructor = instructor;
        }
    }
    }