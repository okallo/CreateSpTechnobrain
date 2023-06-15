using System.Data;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace CreateSpProject;
public class EmployeeDAL
{
    private MySqlConnection connection;
    public EmployeeDAL(MySqlConnection connection)
    {
        this.connection = connection;
    }

    public int UpdateEmployeeSalary(int empID, int newSalary)
    {
        try
        {
            if (this.connection.State != ConnectionState.Open)
                connection.Open();

            using (var command = this.connection.CreateCommand())
            {

                try
                {
                    command.CommandText = "UpdateEmployeeSalary";
                    command.CommandType = CommandType.StoredProcedure;

                    var empIdParam = command.CreateParameter();
                    empIdParam.ParameterName = "@empID";
                    empIdParam.DbType = DbType.Int32;
                    empIdParam.Value = empID;
                    command.Parameters.Add(empIdParam);

                    var newSalaryParam = command.CreateParameter();
                    newSalaryParam.ParameterName = "@newSalary";
                    newSalaryParam.DbType = DbType.Int32;
                    newSalaryParam.Value = newSalary;
                    command.Parameters.Add(newSalaryParam);

                    var oldSalaryParam = command.CreateParameter();
                    oldSalaryParam.ParameterName = "@oldSalary";
                    oldSalaryParam.DbType = DbType.Int32;
                    oldSalaryParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(oldSalaryParam);


                    command.ExecuteNonQuery();
                    int oldSalary = (int)oldSalaryParam.Value;
                    Console.WriteLine($"Old salary: {oldSalary}");
                    return oldSalary;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    
                }

            
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return 0;
    }
    //Get Salary of employee
    public int GetEmployeeSalary(int employeeId)
    {
        if (this.connection.State != ConnectionState.Open)
                connection.Open();
        try
        {
            using (MySqlCommand command = this.connection.CreateCommand())
            {
                command.CommandText = "GetEmployeeSalary";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empID", employeeId);

                object result = command.ExecuteScalar();
                return Convert.ToInt32(result);


            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }

    }
    //Get Employee List
    public List<Employee> GetAllEmployees()
    {
        if (this.connection.State != ConnectionState.Open)
                connection.Open();
        List<Employee> employees = new List<Employee>();
        try
        {

            using (MySqlCommand command = this.connection.CreateCommand())
            {
                command.CommandText = "GetAllEmployees";
                command.CommandType = CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string name = reader.GetString("name");
                        int salary = reader.GetInt32("salary");

                        Employee employee = new Employee
                        {
                            Id = id,
                            Name = name,
                            Salary = salary
                        };

                        employees.Add(employee);
                    }
                }
            }



        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

        }
        return employees;

    }
    //delete employee
    public bool DeleteEmployee(int employeeId)
    {
        if (this.connection.State != ConnectionState.Open)
                connection.Open();
        try
        {
            using (MySqlCommand command = this.connection.CreateCommand())
            {
                command.CommandText = "DeleteEmployee";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empID", employeeId);

                command.ExecuteNonQuery();
            }
            Console.WriteLine("Deleted successfully");
            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    //Add a new employee
    public bool AddEmployee(string employeeName, int employeeSalary)
    {
        if (this.connection.State != ConnectionState.Open)
                connection.Open();
        try
        {

            using (MySqlCommand command = this.connection.CreateCommand())
            {
                command.CommandText = "AddEmployee";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@empName", employeeName);
                command.Parameters.AddWithValue("@empSalary", employeeSalary);

                command.ExecuteNonQuery();
            }
            Console.WriteLine("Employee Added Successfully");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

}