using MySqlConnector;
using CreateSpProject;
using NUnit.Framework;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CreateSpTest;

public class EmployeeDALTests
{
    private MySqlConnection connection;
    private CreateSpProjectContext context;
    private EmployeeDAL dal;
    [SetUp]
    public void Setup()
    {
        //string conn = context.Database.GetConnectionString();
        //reviewer, please edit the connection string to your local db
        string conn = "server=localhost;user=root;password=R00t@2023;database=EmployeeSp";

        connection = new MySqlConnection(conn);
        dal = new EmployeeDAL(connection);
        connection.Open();
        // TODO: Setup code to create a connection to the test database
        // and initialize the data access layer
    }
    [TearDown]
    public void TearDown()
    {
        connection.Close();
        // TODO: Teardown code to clean up the test database
    }
    [Test]
    public void TestUpdateEmployeeSalary()
    {
        //int empID = 1;
        int newSalary = 208080;
        List<Employee> employees = new List<Employee>();
        string sql = "SELECT * FROM employees";

        using (var command = new MySqlCommand(sql, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    Employee employee = new Employee();
                    employee.Id = reader.GetInt32("id");
                    employee.Name = reader.GetString("name");
                    employee.Salary = reader.GetInt32("salary");
                    employees.Add(employee);
                }
            }
        }
        Employee employee1 = employees[0];
        int i = employee1.Salary;
        int k = dal.UpdateEmployeeSalary(employee1.Id, newSalary);
        Assert.AreEqual(i, k);
        // TODO: Write test cases to ensure that the UpdateEmployeeSalary returns old value
    }
    [Test]
    public void TestAddEmployee()
    {
        string Name = "John weba";
        int Salary = 287654;
        bool b = dal.AddEmployee(Name, Salary);
        Assert.IsTrue(b);
        // Write test cases to ensure that AddEmployee adds employee
    }
    [Test]
    public void TestViewAllEmployees()
    {

        List<Employee> employees = new List<Employee>();
        string sql = "SELECT * FROM employees";

        using (var command = new MySqlCommand(sql, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    Employee employee = new Employee();
                    employee.Id = reader.GetInt32("id");
                    employee.Name = reader.GetString("name");
                    employee.Salary = reader.GetInt32("salary");
                    employees.Add(employee);
                }
            }
        }
        var k = dal.GetAllEmployees();
        int e = employees.Count();
        int j = k.Count();
        Assert.AreEqual(e, j);
        //Write test cases to ensure All employees and fetched from db
    }
    [Test]
    public void TestGetEmployeeSalary()
    {
        List<Employee> employees = new List<Employee>();
        string sql = "SELECT * FROM employees";

        using (var command = new MySqlCommand(sql, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    Employee employee = new Employee();
                    employee.Id = reader.GetInt32("id");
                    employee.Name = reader.GetString("name");
                    employee.Salary = reader.GetInt32("salary");
                    employees.Add(employee);
                }
            }
        }
        Employee employee1 = employees[1];
        int w = dal.GetEmployeeSalary(employee1.Id);

        Assert.AreEqual(employee1.Salary, w);
        // TODO: Write test cases to ensure that the GetEmployeeSaray retuns employee salary
    }
    [Test]
    public void TestDeleteEmployee()
    {
        List<Employee> employees = new List<Employee>();
        string sql = "SELECT * FROM employees";

        using (var command = new MySqlCommand(sql, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    Employee employee = new Employee();
                    employee.Id = reader.GetInt32("id");
                    employee.Name = reader.GetString("name");
                    employee.Salary = reader.GetInt32("salary");
                    employees.Add(employee);
                    //Console.WriteLine($"ID: {id}, Name: {name}, Salary: {salary}");
                }
            }
        }
        Employee employee1 = employees[0];
        bool l = dal.DeleteEmployee(employee1.Id);
        Assert.IsTrue(l);

        // TODO: Write test cases to ensure that the employee is deleted from db
    }
}