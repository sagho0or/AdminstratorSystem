namespace AdministratorSystem
{
    //A student is registered on a degree programme and becomes a member of a particular student cohort
     //A student cohort is the set of students registered for a given degree programme in a given academic
     //session (academic year)

    public class Student
    {
        // the ID begins with the year of the cohort followed by unique 6 digit string
        public string StundetID { get; set; }
        public int StudentId { get; set; }
        public string UserName { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public int CourseId { get; set; }
        public Course Course {  get; set; }

        //TODO value of score? 
        public int Score { get; set; } = 0;

        public Student(string userName, string firstName, string lastName, Course course)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Course = course;
            StundetID = "sss";

        }

    }
}
