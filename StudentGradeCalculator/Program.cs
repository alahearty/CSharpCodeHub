using StudentGradeCalculator.Core;
using StudentGradeCalculator.Services;
using StudentGradeCalculator.Models;

namespace StudentGradeCalculator;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("🎓 Student Grade Calculator System");
        Console.WriteLine("==================================\n");

        var gradeCalculator = new GradeCalculator();
        var studentManager = new StudentManager();
        var reportGenerator = new ReportGenerator();
        
        bool isRunning = true;
        
        while (isRunning)
        {
            ShowMainMenu();
            var choice = Console.ReadLine()?.Trim();
            
            switch (choice)
            {
                case "1":
                    studentManager.AddStudent();
                    break;
                case "2":
                    studentManager.AddGrades();
                    break;
                case "3":
                    studentManager.ViewStudent();
                    break;
                case "4":
                    studentManager.ListAllStudents();
                    break;
                case "5":
                    gradeCalculator.CalculateClassStatistics();
                    break;
                case "6":
                    gradeCalculator.FindTopStudents();
                    break;
                case "7":
                    gradeCalculator.FindStudentsNeedingHelp();
                    break;
                case "8":
                    reportGenerator.GenerateClassReport();
                    break;
                case "9":
                    reportGenerator.GenerateStudentReport();
                    break;
                case "0":
                    isRunning = false;
                    Console.WriteLine("👋 Goodbye!");
                    break;
                default:
                    Console.WriteLine("❌ Invalid choice. Please try again.");
                    break;
            }
            
            if (isRunning)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    static void ShowMainMenu()
    {
        Console.WriteLine("🎓 STUDENT GRADE CALCULATOR");
        Console.WriteLine("===========================");
        Console.WriteLine("1. 👤 Add New Student");
        Console.WriteLine("2. 📝 Add Grades");
        Console.WriteLine("3. 👁️  View Student");
        Console.WriteLine("4. 📋 List All Students");
        Console.WriteLine("5. 📊 Class Statistics");
        Console.WriteLine("6. 🏆 Top Students");
        Console.WriteLine("7. ❗ Students Needing Help");
        Console.WriteLine("8. 📄 Generate Class Report");
        Console.WriteLine("9. 📄 Generate Student Report");
        Console.WriteLine("0. 🚪 Exit");
        Console.WriteLine("===========================");
        Console.Write("Choose option (0-9): ");
    }
}
