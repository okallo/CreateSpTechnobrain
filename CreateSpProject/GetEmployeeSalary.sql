DROP PROCEDURE IF EXISTS GetEmployeeSalary;
CREATE PROCEDURE GetEmployeeSalary(IN empID INT)
BEGIN
    SELECT salary FROM employees where id=empId;
END