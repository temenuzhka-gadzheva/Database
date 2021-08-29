USE SoftUni

/*Find Names of All Employees by First Name*/
SELECT FirstName,LastName
     FROM Employees
	   WHERE LEFT(FirstName,2) ='SA'

/*Find Names of All employees by Last Name */
SELECT FirstName,LastName 
     FROM Employees
	 WHERE LastName LIKE '%ei%'

/*Find First Names of All Employees*/
SELECT FirstName
     FROM Employees
	 WHERE  DepartmentID = 3 OR DepartmentID = 10
	 AND YEAR(HireDate) BETWEEN 1995 AND 2005


/*Find All Employees Except Engineers*/
SELECT FirstName, LastName
     FROM Employees
	 WHERE NOT (JobTitle LIKE '%engineer%')
	 
/*Find Towns with Name Length*/
SELECT [Name] 
      FROM Towns
	  WHERE LEN([Name]) = 5 
	     OR LEN([Name]) = 6
      ORDER BY [Name] ASC

/*Find Towns Starting With*/
SELECT *
     FROM Towns
	 WHERE Left([Name],1) = 'M' OR Left([Name],1) = 'K'
	 OR Left([Name],1) = 'B' OR Left([Name],1) = 'E'
	 ORDER BY [Name] ASC

/* Find Towns Not Starting With*/
SELECT *
     FROM Towns
	 WHERE [Name] NOT  LIKE  N'R%'
	     AND [Name] NOT LIKE N'B%'
	     AND [Name] NOT LIKE N'D%'
	 ORDER BY [Name] ASC

/*Create View Employees Hired After 2000 Year*/
CREATE VIEW  V_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName 
    FROM Employees
	WHERE YEAR(HireDate) > 2000

/*Length of Last Name*/
SELECT FirstName, LastName 
     FROM Employees
	 WHERE LEN(LastName) = 5

/*Rank Employees by Salary*/
SELECT  EmployeeID, FirstName,LastName,Salary,
		DENSE_RANK() OVER (
	PARTITION BY Salary
	ORDER BY EmployeeID) AS [Rank]
          FROM Employees
              WHERE Salary BETWEEN 10000 AND 50000
              ORDER BY Salary DESC


/*Find All Employees with Rank 2 */
SELECT * 
     FROM (SELECT EmployeeID,
		FirstName,
		LastName,
		Salary, 
		DENSE_RANK() OVER (
	               PARTITION BY Salary
	               ORDER BY EmployeeID) AS [Rank]
     FROM  Employees
            WHERE Salary BETWEEN 10000 AND 50000) s
            WHERE [Rank] = 2
            ORDER BY Salary DESC


