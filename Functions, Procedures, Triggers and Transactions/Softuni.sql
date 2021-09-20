USE SoftUni

/* Employees with Salary Above 35000 */

CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000 
 AS
 SELECT e.FirstName, e.LastName FROM Employees e

 WHERE e.Salary > 35000
 GO

 -- from judje

/*CREATE  PROCEDURE usp_GetEmployeesSalaryAbove35000 
 AS
 BEGIN
 SELECT e.FirstName, e.LastName 
      FROM Employees e
  WHERE e.Salary > 35000
END;*/
EXEC usp_GetEmployeesSalaryAbove35000 

/* Employees with Salary Above Number */


  
CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber(@Number DECIMAL(18,4))
AS

 BEGIN 
  SELECT e.FirstName AS [First Name],
	   e.LastName  AS [Last Name]
      FROM Employees  e
   WHERE e.Salary >= @Number
END


 EXEC usp_GetEmployeesSalaryAboveNumber @Number  = 48100

 /* Town Names Starting With */

 CREATE PROCEDURE usp_GetTownsStartingWith (@StringOfTownName NVARCHAR(50))
  AS 
   BEGIN
    SELECT t.Name AS TownName
	      FROM Towns t
	 WHERE SUBSTRING(UPPER(t.Name),1,LEN(@StringOfTownName)) = UPPER(@StringOfTownName)

   END

EXEC usp_GetTownsStartingWith 'BEL'


/*Employees from Town*/

CREATE PROCEDURE usp_GetEmployeesFromTown (@TownName NVARCHAR(100))
AS
 BEGIN
  SELECT e.FirstName, e.LastName FROM Employees e
   LEFT JOIN Addresses a ON a.AddressID = e.AddressID
   LEFT JOIN Towns t ON a.TownID = t.TownID
   WHERE t.Name = @TownName

END

EXEC usp_GetEmployeesFromTown 'Sofia'

-- Functions
/* Salary Level Function */

CREATE  FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4)) 
 RETURNS NVARCHAR(20)
  AS
   BEGIN
      DECLARE @SalaryLevel  NVARCHAR(20)
	    IF(@salary < 30000)
		 SET @SalaryLevel = 'Low'
        ELSE IF(@salary BETWEEN 30000 AND 50000 OR @salary = 30000 OR @salary = 50000)
		  SET @SalaryLevel = 'Average'
		ELSE 
		 SET @SalaryLevel = 'High'
      RETURN @SalaryLevel
  END

SELECT e.Salary, dbo.ufn_GetSalaryLevel(e.Salary) AS [Salary Level]
     FROM Employees e


/* Employees by Salary Level */

CREATE  PROCEDURE usp_EmployeesBySalaryLevel (@SalaryLevel NVARCHAR(20))
 AS 
  BEGIN
  SELECT SalaryOfLevel.FirstName, SalaryOfLevel.LastName
      FROM ( SELECT e.FirstName, 
                    e.LastName,
		            dbo.ufn_GetSalaryLevel(e.Salary) AS [Level] 
		           FROM Employees e ) AS SalaryOfLevel
    WHERE SalaryOfLevel.Level = @SalaryLevel
 END

 EXEC usp_EmployeesBySalaryLevel @SalaryLevel = 'Low'

 /*  */