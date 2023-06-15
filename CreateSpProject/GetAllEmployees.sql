DROP PROCEDURE IF EXISTS GetAllEmployees;
CREATE PROCEDURE GetAllEmployees()
BEGIN
    SELECT id, name, salary FROM employees;
END