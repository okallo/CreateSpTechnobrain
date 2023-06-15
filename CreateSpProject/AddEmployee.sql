DROP PROCEDURE IF EXISTS AddEmployee;
CREATE PROCEDURE AddEmployee (
    IN empName VARCHAR(50),
    IN empSalary INT
)
BEGIN
    INSERT INTO employees (name, salary) VALUES (empName, empSalary);
END