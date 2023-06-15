using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CreateSpProject;
public class CreateSpProjectContext : DbContext
{
    public DbSet<Employee>? Employees { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        Console.Write("Enter MySQL Server username: ");
        string? username = Console.ReadLine();

        Console.Write("Enter MySQL Server password: ");
        string password = ReadPassword();

        Console.Write("Enter MySQL Database name: ");
        string? databaseName = Console.ReadLine();
        // string username = "root";
        // string password ="R00t@2023";
        // string databaseName = "EmployeeSp";

        try
        {
            string connectionString = $"server=localhost;user={username};password={password};database={databaseName}";

            optionsBuilder.UseMySQL(connectionString);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }


    private static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Remove(password.Length - 1);
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();

        return password;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   //Define primary key
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
        });
    }

    public bool EmployeeExists(string name)
    {
        return Employees.Any(e => e.Name == name);
    }

    public void AddNewEmployee(Employee employee)
    {
        if(employee.Name != null || employee.Name != string.Empty){
            if (!EmployeeExists(employee.Name))
        {
            Employees.Add(employee);
            SaveChanges();
            Console.WriteLine("Employee added successfully.");
        }
        else
        {
            Console.WriteLine("Employee already exists.");
        }
        }
        
    }
}