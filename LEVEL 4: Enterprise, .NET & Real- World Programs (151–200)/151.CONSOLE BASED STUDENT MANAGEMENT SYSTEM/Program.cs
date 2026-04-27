using System;
using System.Collections.Generic;
using System.Linq;

enum StudentStatus
{
    Active,
    Inactive,
    Graduated,
    Suspended
}

enum Grade
{
    A,
    B,
    C,
    D,
    F
}

class Attendance
{
    public DateTime Date { get; set; }
    public bool IsPresent { get; set; }
    public string Remarks { get; set; }

    public Attendance(DateTime date, bool present, string remarks = "")
    {
        Date = date;
        IsPresent = present;
        Remarks = remarks;
    }

    public override string ToString()
    {
        return $"{Date:yyyy-MM-dd} - {(IsPresent ? "Present" : "Absent")} - {Remarks}";
    }
}

class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseCode { get; set; }
    public string Instructor { get; set; }
    public int Credits { get; set; }
    public int MaxCapacity { get; set; }
    public List<int> EnrolledStudents { get; set; }

    public Course(int courseId, string name, string code, string instructor, int credits, int maxCapacity)
    {
        CourseId = courseId;
        CourseName = name;
        CourseCode = code;
        Instructor = instructor;
        Credits = credits;
        MaxCapacity = maxCapacity;
        EnrolledStudents = new List<int>();
    }

    public override string ToString()
    {
        return $"ID: {CourseId} | {CourseName} ({CourseCode}) | Instructor: {Instructor} | Credits: {Credits} | Enrolled: {EnrolledStudents.Count}/{MaxCapacity}";
    }
}

class Grade_Record
{
    public int CourseId { get; set; }
    public Grade LetterGrade { get; set; }
    public decimal NumericScore { get; set; }
    public DateTime GradeDate { get; set; }

    public Grade_Record(int courseId, Grade grade, decimal score)
    {
        CourseId = courseId;
        LetterGrade = grade;
        NumericScore = score;
        GradeDate = DateTime.Now;
    }

    public override string ToString()
    {
        return $"Course: {CourseId} | Grade: {LetterGrade} | Score: {NumericScore:F2}% | Date: {GradeDate:yyyy-MM-dd}";
    }
}

class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Department { get; set; }
    public StudentStatus Status { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public List<int> EnrolledCourses { get; set; }
    public List<Grade_Record> Grades { get; set; }
    public List<Attendance> AttendanceRecords { get; set; }

    public Student(int studentId, string firstName, string lastName, string email, string phoneNumber, 
                   string address, DateTime dob, string department)
    {
        StudentId = studentId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        DateOfBirth = dob;
        Department = department;
        Status = StudentStatus.Active;
        EnrollmentDate = DateTime.Now;
        EnrolledCourses = new List<int>();
        Grades = new List<Grade_Record>();
        AttendanceRecords = new List<Attendance>();
    }

    public decimal GetGPA()
    {
        if (Grades.Count == 0) return 0;

        decimal gradePoints = 0;
        int gradeCount = 0;

        foreach (var grade in Grades)
        {
            switch (grade.LetterGrade)
            {
                case Grade.A: gradePoints += 4.0m; break;
                case Grade.B: gradePoints += 3.0m; break;
                case Grade.C: gradePoints += 2.0m; break;
                case Grade.D: gradePoints += 1.0m; break;
                case Grade.F: gradePoints += 0.0m; break;
            }
            gradeCount++;
        }

        return gradePoints / gradeCount;
    }

    public decimal GetAttendancePercentage()
    {
        if (AttendanceRecords.Count == 0) return 100;
        int presentDays = AttendanceRecords.Count(a => a.IsPresent);
        return (decimal)presentDays / AttendanceRecords.Count * 100;
    }

    public override string ToString()
    {
        return $"ID: {StudentId} | {FirstName} {LastName} | Email: {Email} | Dept: {Department} | Status: {Status} | GPA: {GetGPA():F2}";
    }
}

class StudentManagementSystem
{
    private List<Student> students = new List<Student>();
    private List<Course> courses = new List<Course>();
    private int nextStudentId = 1001;

    public void RegisterStudent(string firstName, string lastName, string email, string phoneNumber, 
                                string address, DateTime dob, string department)
    {
        if (students.Any(s => s.Email == email))
        {
            Console.WriteLine("Student with this email already exists!");
            return;
        }

        var student = new Student(nextStudentId++, firstName, lastName, email, phoneNumber, address, dob, department);
        students.Add(student);
        Console.WriteLine($"Student registered successfully! Student ID: {student.StudentId}");
    }

    public void AddCourse(int courseId, string name, string code, string instructor, int credits, int maxCapacity)
    {
        if (courses.Any(c => c.CourseId == courseId || c.CourseCode == code))
        {
            Console.WriteLine("Course ID or Code already exists!");
            return;
        }

        courses.Add(new Course(courseId, name, code, instructor, credits, maxCapacity));
        Console.WriteLine($"Course added successfully! Course ID: {courseId}");
    }

    public void EnrollStudentInCourse(int studentId, int courseId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        var course = courses.FirstOrDefault(c => c.CourseId == courseId);
        if (course == null)
        {
            Console.WriteLine("Course not found!");
            return;
        }

        if (student.Status != StudentStatus.Active)
        {
            Console.WriteLine("Cannot enroll inactive student!");
            return;
        }

        if (student.EnrolledCourses.Contains(courseId))
        {
            Console.WriteLine("Student already enrolled in this course!");
            return;
        }

        if (course.EnrolledStudents.Count >= course.MaxCapacity)
        {
            Console.WriteLine("Course capacity full!");
            return;
        }

        student.EnrolledCourses.Add(courseId);
        course.EnrolledStudents.Add(studentId);
        Console.WriteLine("Student enrolled successfully!");
    }

    public void UnrollStudentFromCourse(int studentId, int courseId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        var course = courses.FirstOrDefault(c => c.CourseId == courseId);
        if (course == null)
        {
            Console.WriteLine("Course not found!");
            return;
        }

        if (!student.EnrolledCourses.Contains(courseId))
        {
            Console.WriteLine("Student not enrolled in this course!");
            return;
        }

        student.EnrolledCourses.Remove(courseId);
        course.EnrolledStudents.Remove(studentId);
        Console.WriteLine("Student unrolled successfully!");
    }

    public void RecordGrade(int studentId, int courseId, Grade grade, decimal score)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        if (!student.EnrolledCourses.Contains(courseId))
        {
            Console.WriteLine("Student not enrolled in this course!");
            return;
        }

        if (score < 0 || score > 100)
        {
            Console.WriteLine("Score must be between 0 and 100!");
            return;
        }

        var existingGrade = student.Grades.FirstOrDefault(g => g.CourseId == courseId);
        if (existingGrade != null)
        {
            student.Grades.Remove(existingGrade);
        }

        student.Grades.Add(new Grade_Record(courseId, grade, score));
        Console.WriteLine($"Grade recorded! Student GPA: {student.GetGPA():F2}");
    }

    public void RecordAttendance(int studentId, DateTime date, bool isPresent, string remarks = "")
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        var existingRecord = student.AttendanceRecords.FirstOrDefault(a => a.Date.Date == date.Date);
        if (existingRecord != null)
        {
            student.AttendanceRecords.Remove(existingRecord);
        }

        student.AttendanceRecords.Add(new Attendance(date, isPresent, remarks));
        Console.WriteLine($"Attendance recorded! {student.FirstName}'s attendance: {student.GetAttendancePercentage():F2}%");
    }

    public void UpdateStudentStatus(int studentId, StudentStatus status)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        student.Status = status;
        Console.WriteLine($"Student status updated to {status}!");
    }

    public void UpdateStudentInfo(int studentId, string email, string phoneNumber, string address)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        if (students.Any(s => s.StudentId != studentId && s.Email == email))
        {
            Console.WriteLine("Email already in use!");
            return;
        }

        student.Email = email;
        student.PhoneNumber = phoneNumber;
        student.Address = address;
        Console.WriteLine("Student information updated!");
    }

    public void ViewStudentDetails(int studentId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        Console.WriteLine("\n========== Student Details ==========");
        Console.WriteLine($"ID: {student.StudentId}");
        Console.WriteLine($"Name: {student.FirstName} {student.LastName}");
        Console.WriteLine($"Email: {student.Email}");
        Console.WriteLine($"Phone: {student.PhoneNumber}");
        Console.WriteLine($"Address: {student.Address}");
        Console.WriteLine($"DOB: {student.DateOfBirth:yyyy-MM-dd}");
        Console.WriteLine($"Department: {student.Department}");
        Console.WriteLine($"Status: {student.Status}");
        Console.WriteLine($"Enrollment Date: {student.EnrollmentDate:yyyy-MM-dd}");
        Console.WriteLine($"GPA: {student.GetGPA():F2}");
        Console.WriteLine($"Attendance: {student.GetAttendancePercentage():F2}%");
        Console.WriteLine($"Enrolled Courses: {student.EnrolledCourses.Count}");
        Console.WriteLine("====================================\n");
    }

    public void ViewStudentGrades(int studentId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        if (student.Grades.Count == 0)
        {
            Console.WriteLine("No grades recorded!");
            return;
        }

        Console.WriteLine($"\n========== Grades for {student.FirstName} {student.LastName} ==========");
        foreach (var grade in student.Grades.OrderByDescending(g => g.GradeDate))
        {
            var course = courses.FirstOrDefault(c => c.CourseId == grade.CourseId);
            Console.WriteLine($"{course?.CourseName} ({course?.CourseCode}): {grade.LetterGrade} ({grade.NumericScore:F2}%)");
        }
        Console.WriteLine($"GPA: {student.GetGPA():F2}");
        Console.WriteLine("================================================================\n");
    }

    public void ViewStudentAttendance(int studentId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        if (student.AttendanceRecords.Count == 0)
        {
            Console.WriteLine("No attendance records!");
            return;
        }

        Console.WriteLine($"\n========== Attendance for {student.FirstName} {student.LastName} ==========");
        foreach (var record in student.AttendanceRecords.OrderByDescending(a => a.Date))
        {
            Console.WriteLine(record);
        }
        Console.WriteLine($"Overall Attendance: {student.GetAttendancePercentage():F2}%");
        Console.WriteLine("===================================================================\n");
    }

    public void ViewStudentCourses(int studentId)
    {
        var student = students.FirstOrDefault(s => s.StudentId == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found!");
            return;
        }

        if (student.EnrolledCourses.Count == 0)
        {
            Console.WriteLine("Student not enrolled in any courses!");
            return;
        }

        Console.WriteLine($"\n========== Enrolled Courses for {student.FirstName} {student.LastName} ==========");
        int totalCredits = 0;
        foreach (var courseId in student.EnrolledCourses)
        {
            var course = courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course != null)
            {
                Console.WriteLine(course);
                totalCredits += course.Credits;
            }
        }
        Console.WriteLine($"Total Credits: {totalCredits}");
        Console.WriteLine("=================================================================\n");
    }

    public void ViewAllStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students in the system!");
            return;
        }

        Console.WriteLine("\n========== All Students ==========");
        foreach (var student in students.OrderBy(s => s.StudentId))
        {
            Console.WriteLine(student);
        }
        Console.WriteLine("==================================\n");
    }

    public void ViewAllCourses()
    {
        if (courses.Count == 0)
        {
            Console.WriteLine("No courses in the system!");
            return;
        }

        Console.WriteLine("\n========== All Courses ==========");
        foreach (var course in courses)
        {
            Console.WriteLine(course);
        }
        Console.WriteLine("=================================\n");
    }

    public void ViewCourseDetails(int courseId)
    {
        var course = courses.FirstOrDefault(c => c.CourseId == courseId);
        if (course == null)
        {
            Console.WriteLine("Course not found!");
            return;
        }

        Console.WriteLine("\n========== Course Details ==========");
        Console.WriteLine($"ID: {course.CourseId}");
        Console.WriteLine($"Name: {course.CourseName}");
        Console.WriteLine($"Code: {course.CourseCode}");
        Console.WriteLine($"Instructor: {course.Instructor}");
        Console.WriteLine($"Credits: {course.Credits}");
        Console.WriteLine($"Capacity: {course.EnrolledStudents.Count}/{course.MaxCapacity}");
        Console.WriteLine("\nEnrolled Students:");
        foreach (var studentId in course.EnrolledStudents)
        {
            var student = students.FirstOrDefault(s => s.StudentId == studentId);
            if (student != null)
            {
                Console.WriteLine($"  {student.FirstName} {student.LastName} (ID: {student.StudentId})");
            }
        }
        Console.WriteLine("====================================\n");
    }

    public void SearchStudents(string searchTerm)
    {
        var results = students.Where(s =>
            s.FirstName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
            s.LastName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
            s.Email.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
            s.StudentId.ToString().Contains(searchTerm)
        ).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("No students found!");
            return;
        }

        Console.WriteLine("\n========== Search Results ==========");
        foreach (var student in results)
        {
            Console.WriteLine(student);
        }
        Console.WriteLine("====================================\n");
    }

    public void GenerateAcademicReport()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students in the system!");
            return;
        }

        Console.WriteLine("\n========== Academic Report ==========");
        Console.WriteLine($"Total Students: {students.Count}");
        Console.WriteLine($"Active Students: {students.Count(s => s.Status == StudentStatus.Active)}");
        Console.WriteLine($"Inactive Students: {students.Count(s => s.Status == StudentStatus.Inactive)}");
        Console.WriteLine($"Graduated: {students.Count(s => s.Status == StudentStatus.Graduated)}");
        Console.WriteLine($"Suspended: {students.Count(s => s.Status == StudentStatus.Suspended)}");

        var activeStudents = students.Where(s => s.Status == StudentStatus.Active).ToList();
        if (activeStudents.Count > 0)
        {
            decimal avgGPA = activeStudents.Average(s => s.GetGPA());
            decimal avgAttendance = activeStudents.Average(s => s.GetAttendancePercentage());
            Console.WriteLine($"\nActive Students Average GPA: {avgGPA:F2}");
            Console.WriteLine($"Active Students Average Attendance: {avgAttendance:F2}%");
        }

        Console.WriteLine("\nTop 5 Students (by GPA):");
        var topStudents = students.OrderByDescending(s => s.GetGPA()).Take(5);
        foreach (var student in topStudents)
        {
            Console.WriteLine($"  {student.FirstName} {student.LastName}: {student.GetGPA():F2}");
        }

        Console.WriteLine("\n=====================================\n");
    }

    public void GenerateDepartmentReport(string department)
    {
        var deptStudents = students.Where(s => s.Department == department).ToList();

        if (deptStudents.Count == 0)
        {
            Console.WriteLine($"No students in {department} department!");
            return;
        }

        Console.WriteLine($"\n========== {department} Department Report ==========");
        Console.WriteLine($"Total Students: {deptStudents.Count}");
        Console.WriteLine($"Active: {deptStudents.Count(s => s.Status == StudentStatus.Active)}");
        Console.WriteLine($"Graduated: {deptStudents.Count(s => s.Status == StudentStatus.Graduated)}");

        if (deptStudents.Any(s => s.Status == StudentStatus.Active))
        {
            decimal avgGPA = deptStudents.Where(s => s.Status == StudentStatus.Active).Average(s => s.GetGPA());
            Console.WriteLine($"Average GPA: {avgGPA:F2}");
        }

        Console.WriteLine("\n====================================================\n");
    }
}

class Program
{
    static void Main()
    {
        StudentManagementSystem system = new StudentManagementSystem();

        system.AddCourse(101, "C# Programming", "CS101", "Dr. John Smith", 3, 30);
        system.AddCourse(102, "Database Design", "CS102", "Dr. Sarah Johnson", 3, 25);
        system.AddCourse(103, "Web Development", "CS103", "Dr. Mike Brown", 4, 30);
        system.AddCourse(104, "Data Structures", "CS104", "Dr. Emily Davis", 3, 25);
        system.AddCourse(105, "Software Engineering", "CS105", "Dr. David Wilson", 4, 28);

        system.RegisterStudent("John", "Anderson", "john.anderson@student.com", "555-0101", "123 Main St", new DateTime(2003, 5, 15), "Computer Science");
        system.RegisterStudent("Sarah", "Martinez", "sarah.martinez@student.com", "555-0102", "456 Oak Ave", new DateTime(2003, 8, 22), "Computer Science");
        system.RegisterStudent("Michael", "Thompson", "michael.t@student.com", "555-0103", "789 Pine Rd", new DateTime(2002, 11, 10), "Computer Science");
        system.RegisterStudent("Emily", "Garcia", "emily.garcia@student.com", "555-0104", "321 Elm St", new DateTime(2004, 2, 14), "Business");
        system.RegisterStudent("David", "Lee", "david.lee@student.com", "555-0105", "654 Maple Dr", new DateTime(2003, 9, 3), "Business");

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n================ Student Management System ================");
            Console.WriteLine("1. Register Student");
            Console.WriteLine("2. Add Course");
            Console.WriteLine("3. Enroll Student in Course");
            Console.WriteLine("4. Unenroll Student from Course");
            Console.WriteLine("5. Record Grade");
            Console.WriteLine("6. Record Attendance");
            Console.WriteLine("7. Update Student Status");
            Console.WriteLine("8. Update Student Info");
            Console.WriteLine("9. View Student Details");
            Console.WriteLine("10. View Student Grades");
            Console.WriteLine("11. View Student Attendance");
            Console.WriteLine("12. View Student Courses");
            Console.WriteLine("13. View All Students");
            Console.WriteLine("14. View All Courses");
            Console.WriteLine("15. View Course Details");
            Console.WriteLine("16. Search Students");
            Console.WriteLine("17. Generate Academic Report");
            Console.WriteLine("18. Generate Department Report");
            Console.WriteLine("19. Exit");
            Console.WriteLine("===========================================================");
            Console.Write("Enter your choice (1-19): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter First Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Enter Last Name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter Phone Number: ");
                        string phone = Console.ReadLine();
                        Console.Write("Enter Address: ");
                        string address = Console.ReadLine();
                        Console.Write("Enter Date of Birth (yyyy-MM-dd): ");
                        if (DateTime.TryParse(Console.ReadLine(), out DateTime dob))
                        {
                            Console.Write("Enter Department: ");
                            string dept = Console.ReadLine();
                            system.RegisterStudent(firstName, lastName, email, phone, address, dob, dept);
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format!");
                        }
                        break;

                    case 2:
                        Console.Write("Enter Course ID: ");
                        if (int.TryParse(Console.ReadLine(), out int courseId))
                        {
                            Console.Write("Enter Course Name: ");
                            string courseName = Console.ReadLine();
                            Console.Write("Enter Course Code: ");
                            string courseCode = Console.ReadLine();
                            Console.Write("Enter Instructor Name: ");
                            string instructor = Console.ReadLine();
                            Console.Write("Enter Credits: ");
                            if (int.TryParse(Console.ReadLine(), out int credits) && credits > 0)
                            {
                                Console.Write("Enter Max Capacity: ");
                                if (int.TryParse(Console.ReadLine(), out int capacity) && capacity > 0)
                                {
                                    system.AddCourse(courseId, courseName, courseCode, instructor, credits, capacity);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid capacity!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid credits!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Course ID!");
                        }
                        break;

                    case 3:
                        Console.Write("Enter Student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int enrollStudentId))
                        {
                            Console.Write("Enter Course ID: ");
                            if (int.TryParse(Console.ReadLine(), out int enrollCourseId))
                            {
                                system.EnrollStudentInCourse(enrollStudentId, enrollCourseId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Course ID!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID!");
                        }
                        break;

                    case 4:
                        Console.Write("Enter Student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int unenrollStudentId))
                        {
                            Console.Write("Enter Course ID: ");
                            if (int.TryParse(Console.ReadLine(), out int unenrollCourseId))
                            {
                                system.UnrollStudentFromCourse(unenrollStudentId, unenrollCourseId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Course ID!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID!");
                        }
                        break;

                    case 5:
                        Console.Write("Enter Student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int gradeStudentId))
                        {
                            Console.Write("Enter Course ID: ");
                            if (int.TryParse(Console.ReadLine(), out int gradeCourseId))
                            {
                                Console.WriteLine("Grades: 0=A, 1=B, 2=C, 3=D, 4=F");
                                Console.Write("Enter Grade (0-4): ");
                                if (int.TryParse(Console.ReadLine(), out int gradeVal) && gradeVal >= 0 && gradeVal <= 4)
                                {
                                    Console.Write("Enter Numeric Score (0-100): ");
                                    if (decimal.TryParse(Console.ReadLine(), out decimal score) && score >= 0 && score <= 100)
                                    {
                                        system.RecordGrade(gradeStudentId, gradeCourseId, (Grade)gradeVal, score);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid score!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid grade!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Course ID!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID!");
                        }
                        break;

                    case 6:
                        Console.Write("Enter Student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int attStudentId))
                        {
                            Console.Write("Enter Date (yyyy-MM-dd): ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime attDate))
                            {
                                Console.Write("Is Present? (true/false): ");
                                if (bool.TryParse(Console.ReadLine(), out bool isPresent))
                                {
                                    Console.Write("Enter Remarks: ");
                                    string remarks = Console.ReadLine();
                                    system.RecordAttendance(attStudentId, attDate, isPresent, remarks);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid date format!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID!");
                        }
                        break;

                    case 7:
                        Console.Write("Enter Student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int statusStudentId))
                        {
                            Console.WriteLine("Statuses: 0=Active, 1=Inactive, 2=Graduated, 3=Suspended");
                            Console.Write("Enter Status (0-3): ");
                            if (int.TryParse(Console.ReadLine(), out int statusVal) && statusVal >= 0 && statusVal <= 3)
                            {
                                system.UpdateStudentStatus(statusStudentId, (StudentStatus)statusVal);
                            }
                            else
                            {
                                Console.WriteLine("Invalid status!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID!");
                        }
                        break;

                    case 8:
                        Console.Write("Enter Student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int updateStudentId))
                        {
                            Console.Write("Enter New Email: ");
                            string newEmail = Console.ReadLine();
                            Console.Write("Enter New Phone: ");
                            string newPhone = Console.ReadLine();
                            Console.Write("Enter New Address: ");
                            string newAddr = Console.ReadLine();
                            system.UpdateStudentInfo(updateStudentId, newEmail, newPhone, newAddr);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID!");
                        }
                        break;

                    case 9:
                        Console.Write("Enter Student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int detailStudentId))
                        {
                            system.ViewStudentDetails(detailStudentId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID!");
                        }
                        break;

                    case 10:
                        Console.Write("Enter Student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int gradeViewStudentId))
                        {
                            system.ViewStudentGrades(gradeViewStudentId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID!");
                        }
                        break;

                    case 11:
                        Console.Write("Enter Student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int attViewStudentId))
                        {
                            system.ViewStudentAttendance(attViewStudentId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID!");
                        }
                        break;

                    case 12:
                        Console.Write("Enter Student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int courseViewStudentId))
                        {
                            system.ViewStudentCourses(courseViewStudentId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Student ID!");
                        }
                        break;

                    case 13:
                        system.ViewAllStudents();
                        break;

                    case 14:
                        system.ViewAllCourses();
                        break;

                    case 15:
                        Console.Write("Enter Course ID: ");
                        if (int.TryParse(Console.ReadLine(), out int detailCourseId))
                        {
                            system.ViewCourseDetails(detailCourseId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Course ID!");
                        }
                        break;

                    case 16:
                        Console.Write("Enter Search Term (Name/ID/Email): ");
                        string searchTerm = Console.ReadLine();
                        system.SearchStudents(searchTerm);
                        break;

                    case 17:
                        system.GenerateAcademicReport();
                        break;

                    case 18:
                        Console.Write("Enter Department Name: ");
                        string deptName = Console.ReadLine();
                        system.GenerateDepartmentReport(deptName);
                        break;

                    case 19:
                        running = false;
                        Console.WriteLine("Thank you for using Student Management System!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please enter a number between 1 and 19.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
            }
        }
    }
}
