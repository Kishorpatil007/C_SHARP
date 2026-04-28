using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Major { get; set; }
    public double GPA { get; set; }
}

public class StudentDataAccess
{
    private readonly string _connectionString = "Server=.;Database=StudentDB;Integrated Security=true;";

    public StudentDataAccess()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Students' and xtype='U')
                    CREATE TABLE Students (
                        StudentId INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(100) NOT NULL,
                        Email NVARCHAR(100) UNIQUE NOT NULL,
                        Phone NVARCHAR(15),
                        Major NVARCHAR(50),
                        GPA FLOAT
                    )";

                using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database initialization error: {ex.Message}");
        }
    }

    public void Create(Student student)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Students (Name, Email, Phone, Major, GPA) VALUES (@Name, @Email, @Phone, @Major, @GPA)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", student.Name ?? "");
                    command.Parameters.AddWithValue("@Email", student.Email ?? "");
                    command.Parameters.AddWithValue("@Phone", student.Phone ?? "");
                    command.Parameters.AddWithValue("@Major", student.Major ?? "");
                    command.Parameters.AddWithValue("@GPA", student.GPA);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Student created successfully.");
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
        }
    }

    public List<Student> ReadAll()
    {
        List<Student> students = new List<Student>();

        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                StudentId = (int)reader["StudentId"],
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Major = reader["Major"].ToString(),
                                GPA = Convert.ToDouble(reader["GPA"])
                            };
                            students.Add(student);
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
        }

        return students;
    }

    public Student ReadById(int studentId)
    {
        Student student = null;

        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Students WHERE StudentId = @StudentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new Student
                            {
                                StudentId = (int)reader["StudentId"],
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Major = reader["Major"].ToString(),
                                GPA = Convert.ToDouble(reader["GPA"])
                            };
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
        }

        return student;
    }

    public void Update(Student student)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Students SET Name = @Name, Email = @Email, Phone = @Phone, Major = @Major, GPA = @GPA WHERE StudentId = @StudentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", student.StudentId);
                    command.Parameters.AddWithValue("@Name", student.Name ?? "");
                    command.Parameters.AddWithValue("@Email", student.Email ?? "");
                    command.Parameters.AddWithValue("@Phone", student.Phone ?? "");
                    command.Parameters.AddWithValue("@Major", student.Major ?? "");
                    command.Parameters.AddWithValue("@GPA", student.GPA);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Student updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
        }
    }

    public void Delete(int studentId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Students WHERE StudentId = @StudentId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentId", studentId);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Student deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
        }
    }

    public void DisplayAllStudents()
    {
        List<Student> students = ReadAll();

        if (students.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }

        Console.WriteLine("\n========== All Students ==========");
        Console.WriteLine("{0,-5} {1,-20} {2,-25} {3,-15} {4,-15} {5,-8}", 
            "ID", "Name", "Email", "Phone", "Major", "GPA");
        Console.WriteLine(new string('-', 88));

        foreach (var student in students)
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-25} {3,-15} {4,-15} {5,-8:F2}", 
                student.StudentId, student.Name, student.Email, student.Phone, student.Major, student.GPA);
        }
        Console.WriteLine();
    }
}

public class Program
{
    public static void Main()
    {
        StudentDataAccess dataAccess = new StudentDataAccess();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n========== Student Management System (ADO.NET) ==========");
            Console.WriteLine("1. Create Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. View Student by ID");
            Console.WriteLine("4. Update Student");
            Console.WriteLine("5. Delete Student");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateStudent(dataAccess);
                    break;
                case "2":
                    dataAccess.DisplayAllStudents();
                    break;
                case "3":
                    ViewStudentById(dataAccess);
                    break;
                case "4":
                    UpdateStudent(dataAccess);
                    break;
                case "5":
                    DeleteStudent(dataAccess);
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Exiting application...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void CreateStudent(StudentDataAccess dataAccess)
    {
        Console.Write("Enter student name: ");
        string name = Console.ReadLine();

        Console.Write("Enter student email: ");
        string email = Console.ReadLine();

        Console.Write("Enter student phone: ");
        string phone = Console.ReadLine();

        Console.Write("Enter student major: ");
        string major = Console.ReadLine();

        Console.Write("Enter student GPA: ");
        if (!double.TryParse(Console.ReadLine(), out double gpa))
        {
            Console.WriteLine("Invalid GPA.");
            return;
        }

        Student student = new Student
        {
            Name = name,
            Email = email,
            Phone = phone,
            Major = major,
            GPA = gpa
        };

        dataAccess.Create(student);
    }

    private static void ViewStudentById(StudentDataAccess dataAccess)
    {
        Console.Write("Enter student ID: ");
        if (int.TryParse(Console.ReadLine(), out int studentId))
        {
            Student student = dataAccess.ReadById(studentId);
            if (student != null)
            {
                Console.WriteLine($"\nStudent ID: {student.StudentId}");
                Console.WriteLine($"Name: {student.Name}");
                Console.WriteLine($"Email: {student.Email}");
                Console.WriteLine($"Phone: {student.Phone}");
                Console.WriteLine($"Major: {student.Major}");
                Console.WriteLine($"GPA: {student.GPA:F2}\n");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private static void UpdateStudent(StudentDataAccess dataAccess)
    {
        Console.Write("Enter student ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int studentId))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Student student = dataAccess.ReadById(studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Console.Write($"Enter new name (current: {student.Name}): ");
        string name = Console.ReadLine();
        if (!string.IsNullOrEmpty(name)) student.Name = name;

        Console.Write($"Enter new email (current: {student.Email}): ");
        string email = Console.ReadLine();
        if (!string.IsNullOrEmpty(email)) student.Email = email;

        Console.Write($"Enter new phone (current: {student.Phone}): ");
        string phone = Console.ReadLine();
        if (!string.IsNullOrEmpty(phone)) student.Phone = phone;

        Console.Write($"Enter new major (current: {student.Major}): ");
        string major = Console.ReadLine();
        if (!string.IsNullOrEmpty(major)) student.Major = major;

        Console.Write($"Enter new GPA (current: {student.GPA:F2}): ");
        if (double.TryParse(Console.ReadLine(), out double gpa)) student.GPA = gpa;

        dataAccess.Update(student);
    }

    private static void DeleteStudent(StudentDataAccess dataAccess)
    {
        Console.Write("Enter student ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int studentId))
        {
            dataAccess.Delete(studentId);
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }
}
