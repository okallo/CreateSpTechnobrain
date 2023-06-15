// See https://aka.ms/new-console-template for more information

using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace CreateSpProject;
public class Program
{

    public static void Main(string[] args)
    {
        using (var context = new CreateSpProjectContext())
        {
            // Apply pending migrations and create the database if it doesn't exist
            context.Database.EnsureCreated();
            Console.WriteLine("Database created or already exists.");

            // Apply migrations to update the schema if needed
            context.Database.Migrate();
            Console.WriteLine("Migrations applied successfully.");
            UpdateStoredProcedures(context, "./AddEmployee.sql");
            Console.WriteLine("Add employee stroed procedure added successfully");
            UpdateStoredProcedures(context, "./DeleteEmployee.sql");
            Console.WriteLine("Delete employee stroed procedure added successfully");
            UpdateStoredProcedures(context, "./GetAllEmployees.sql");
            Console.WriteLine("GetAllEmployees stroed procedure added successfully");
            UpdateStoredProcedures(context, "./GetEmployeeSalary.sql");
            Console.WriteLine("GetEmployeeSalary stroed procedure added successfully");
            UpdateStoredProcedures(context, "./UpdateEmployeeSalary_sp.sql");
            Console.WriteLine("UpdateEmployeeSalary_sp stroed procedure added successfully");



            Console.WriteLine("Stored procedure created or already exists.");
            //ExecuteQueries
            var newEmployee = new Employee
            {
                Name = "John",
                Salary = 50000
            };

            context.AddNewEmployee(newEmployee);
            //
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. View all employees");
                Console.WriteLine("2. Delete an employee");
                Console.WriteLine("3. Update an employee's salary");
                Console.WriteLine("4. Add New Employee");
                Console.WriteLine("0. Exit");

                string? choice = Console.ReadLine();
                Program program = new Program();
                switch (choice)
                {
                    case "1":
                        program.ViewAllEmployees(context);
                        break;
                    case "2":
                        program.DeleteEmployee(context);
                        break;
                    case "3":
                        program.UpdateEmployeeSalary(context);
                        break;
                    case "4":
                        program.AddNewEmployee(context);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

    }

    private static void UpdateStoredProcedures(CreateSpProjectContext context, string s)
    {
        try
        {
            var scriptFilePath = s;
            var scriptContent = File.ReadAllText(scriptFilePath);
            context.Database.ExecuteSqlRaw(scriptContent);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddNewEmployee(CreateSpProjectContext context)
    {
        try
        {
            Console.Write("Enter the employee Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter the employee Salary: ");
            int salary = int.Parse(Console.ReadLine());
            var newEmployee = new Employee
            {
                Name = name,
                Salary = salary
            };

            context.AddNewEmployee(newEmployee);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void DeleteEmployee(CreateSpProjectContext context)
    {
        try
        {
            Console.Write("Enter the employee ID to delete: ");
            int employeeId = int.Parse(Console.ReadLine());
            string conn = context.Database.GetConnectionString();

            MySqlConnection connection = new MySqlConnection(conn);
           
            var employee = context.Employees.Any(m => m.Id == employeeId);

            if (!employee.Equals(null))
            {
                EmployeeDAL dal = new EmployeeDAL(connection);

                dal.DeleteEmployee(employeeId);
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ViewAllEmployees(CreateSpProjectContext context)
    {
        try
        {
            string? conn = context.Database.GetConnectionString();
            
            MySqlConnection connection = new MySqlConnection(conn);

            EmployeeDAL dal = new EmployeeDAL(connection);
            List<Employee> j = dal.GetAllEmployees();
            foreach(var k in j){
                Console.WriteLine("Id: "+ k.Id + " ,Name: " + k.Name+ " ,Salary: "+k.Salary);
            }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void UpdateEmployeeSalary(CreateSpProjectContext context)
    {
        try
        {
            Console.Write("Enter the employee ID to update: ");
            int employeeId = int.Parse(Console.ReadLine());
            string? conn = context.Database.GetConnectionString();

            MySqlConnection connection = new MySqlConnection(conn);
            //connection.Open();
            //
            var employee = context.Employees.Any(m => m.Id == employeeId);

            if (!employee.Equals(null))
            {
                Console.Write("Enter the new salary: ");
                int newSalary = int.Parse(Console.ReadLine());
                EmployeeDAL dal = new EmployeeDAL(connection);

                dal.UpdateEmployeeSalary(employeeId, newSalary);
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}