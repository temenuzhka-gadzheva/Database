USE SoftUni

/*Find All Information About Departments*/
SELECT * FROM Departments

/*Find all Deparment Names*/
SELECT [Name] FROM Departments

/* Find Salary of Each Employee*/
SELECT  FirstName, LastName,Salary
      FROM Employees

/*Find Full Name of Each Employee*/

SELECT FirstName, MiddleName, LastName
     FROM Employees
 
 /*Find Email Address of Each Employee*/
 SELECT FirstName + '.' + LastName + '@' + 'softuni.bg' AS [Full Email Address]
      FROM Employees

/*Find All Different Employee’s Salaries*/	
SELECT DISTINCT Salary
      FROM Employees
 
/*Find all Information About Employees*/
SELECT * 
      FROM Employees
	  WHERE JobTitle = 'Sales Representative'

/*Find Names of All Employees by Salary in Range*/
SELECT FirstName, LastName, JobTitle
     FROM Employees
	 WHERE Salary >= 20000
	 AND Salary <= 30000

/*Find Names of All Employees*/
SELECT FirstName + ' ' + MiddleName +  ' ' + LastName AS [Full Name]
      FROM Employees
      WHERE Salary IN (25000, 14000, 12500, 23600)

/*Find All Employees Without Manager*/
SELECT FirstName,LastName 
  FROM Employees
 WHERE ManagerID IS NULL
	

/*Find All Employees with Salary More Than 50000*/
SELECT FirstName, LastName, Salary
     FROM Employees
	 WHERE Salary > 50000 
	 ORDER BY Salary DESC

/*Find 5 Best Paid Employees*/
SELECT TOP(5) FirstName, LastName 
     FROM Employees
	 ORDER BY Salary DESC

/*Find All Employees Except Marketing*/
SELECT FirstName, LastName
     FROM Employees 
	 WHERE DepartmentId != 4

/*Sort Employees Table*/
SELECT * 
      FROM Employees
	  ORDER BY Salary DESC,
	   FirstName ASC,
	   LastName DESC,
	   MiddleName ASC

/*Create View Employees with Salaries*/
CREATE VIEW  V_EmployeesSalaries  AS
SELECT FirstName, LastName, Salary
     FROM Employees

/*Create View Employees with Job Titles*/
CREATE VIEW V_EmployeeNameJobTitle AS
SELECT FirstName + ' ' +  ISNULL(MiddleName, '') + ' ' + LastName AS [Full Name], JobTitle 
      FROM Employees

/*Distinct Job Titles*/
SELECT DISTINCT JobTitle
      FROM Employees

/*Find First 10 Started Projects*/
SELECT TOP(10) *
      FROM Projects
	  ORDER BY StartDate, [Name]

/*Last 7 Hired Employees*/
SELECT TOP(7) FirstName, LastName, HireDate
       FROM Employees
	   ORDER BY HireDate DESC
	
/*Increase Salaries*/

UPDATE [Employees]
     SET [Salary] *= 1.12
     WHERE [DepartmentID] IN (1, 2, 4, 11);
     SELECT [Salary] 
           FROM [Employees];
	
	  