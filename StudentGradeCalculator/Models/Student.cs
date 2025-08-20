namespace StudentGradeCalculator.Models;

// Student model with grade management
public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Major { get; set; } = string.Empty;
    public int YearLevel { get; set; }
    public Dictionary<string, List<double>> Grades { get; set; }
    public DateTime CreatedDate { get; set; }

    public string FullName => $"{FirstName} {LastName}";
    public int Age => DateTime.Now.Year - DateOfBirth.Year;

    public Student()
    {
        Grades = new Dictionary<string, List<double>>();
        CreatedDate = DateTime.Now;
    }

    public Student(int id, string firstName, string lastName, string email, DateTime dateOfBirth, string major, int yearLevel)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DateOfBirth = dateOfBirth;
        Major = major;
        YearLevel = yearLevel;
        Grades = new Dictionary<string, List<double>>();
        CreatedDate = DateTime.Now;
    }

    public void AddGrade(string subject, double grade)
    {
        if (grade < 0 || grade > 100)
        {
            throw new ArgumentException("Grade must be between 0 and 100");
        }

        if (!Grades.ContainsKey(subject))
        {
            Grades[subject] = new List<double>();
        }

        Grades[subject].Add(grade);
    }

    public double GetAverageGrade(string subject)
    {
        if (!Grades.ContainsKey(subject) || Grades[subject].Count == 0)
        {
            return 0;
        }

        return Grades[subject].Average();
    }

    public double GetOverallAverage()
    {
        if (Grades.Count == 0)
        {
            return 0;
        }

        var allGrades = Grades.Values.SelectMany(g => g);
        return allGrades.Any() ? allGrades.Average() : 0;
    }

    public string GetLetterGrade(double percentage)
    {
        return percentage switch
        {
            >= 93 => "A",
            >= 90 => "A-",
            >= 87 => "B+",
            >= 83 => "B",
            >= 80 => "B-",
            >= 77 => "C+",
            >= 73 => "C",
            >= 70 => "C-",
            >= 67 => "D+",
            >= 63 => "D",
            >= 60 => "D-",
            _ => "F"
        };
    }

    public double GetGPA()
    {
        if (Grades.Count == 0)
        {
            return 0;
        }

        var totalPoints = 0.0;
        var totalCredits = 0;

        foreach (var subject in Grades.Keys)
        {
            var average = GetAverageGrade(subject);
            var letterGrade = GetLetterGrade(average);
            var gradePoints = GetGradePoints(letterGrade);
            totalPoints += gradePoints;
            totalCredits++;
        }

        return totalCredits > 0 ? totalPoints / totalCredits : 0;
    }

    private double GetGradePoints(string letterGrade)
    {
        return letterGrade switch
        {
            "A" => 4.0,
            "A-" => 3.7,
            "B+" => 3.3,
            "B" => 3.0,
            "B-" => 2.7,
            "C+" => 2.3,
            "C" => 2.0,
            "C-" => 1.7,
            "D+" => 1.3,
            "D" => 1.0,
            "D-" => 0.7,
            _ => 0.0
        };
    }

    public List<string> GetSubjects()
    {
        return Grades.Keys.ToList();
    }

    public int GetTotalAssignments()
    {
        return Grades.Values.Sum(g => g.Count);
    }

    public override string ToString()
    {
        return $"ID: {Id} | {FullName} | {Major} | Year: {YearLevel} | GPA: {GetGPA():F2}";
    }
}
