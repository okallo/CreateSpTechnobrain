
DROP PROCEDURE IF EXISTS UpdateEmployeeSalary;
CREATE PROCEDURE UpdateEmployeeSalary (
    IN empID INT,
    IN newSalary INT,
    OUT oldSalary INT
)
BEGIN
    SELECT salary INTO oldSalary FROM employees WHERE id = empID;
    UPDATE employees SET salary = newSalary WHERE id = empID;
END 



