USE SoftUni

/*Employee Address*/

SELECT Top(5) EmployeeID, JobTitle, e.AddressID,AddressText
     FROM Employees e
 JOIN Addresses a ON e.AddressID = a.AddressID
 ORDER BY e.AddressID ASC


/*Addresses with Towns*/
SELECT Top(50) e.FirstName, e.LastName, t.Name AS Town,AddressText
     FROM Employees e
 JOIN Addresses a ON e.AddressID = a.AddressID
 JOIN Towns t ON  a.TownID = t.TownID
 ORDER BY e.FirstName ASC, e.LastName 


/*Sales Employees*/

SELECT e.EmployeeID, e.FirstName, e.LastName, d.Name AS DepartmentName 
     FROM Employees e
 JOIN Departments d ON e.DepartmentID = d.DepartmentID
 WHERE d.Name LIKE 'Sales'
 ORDER BY e.EmployeeID ASC 


/*Employee Departmnets*/

SELECT Top(5) e.EmployeeID, e.FirstName, e.Salary, d.Name AS DepartmentName 
      FROM Employees e
JOIN Departments d ON e.DepartmentID = D.DepartmentID
WHERE e.Salary > 15000
ORDER BY d.DepartmentID ASC

/*Employees Without Projects*/

 SELECT  Top(3) e.EmployeeID, e.FirstName 
      FROM Employees e
LEFT  JOIN EmployeesProjects ep ON e.EmployeeID = ep.EmployeeID 
WHERE ep.EmployeeID  IS NULL
ORDER BY e.EmployeeID ASC


/*Employees Hired After*/
SELECT e.FirstName, e.LastName, e.HireDate, d.Name AS DeptName
     FROM Employees e
JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE e.HireDate > '1.1.1999' AND (d.Name = 'Sales' OR d.Name = 'Finance')
ORDER BY e.HireDate ASC

/*Employees With Projects*/

 SELECT TOP(5) e.EmployeeID, e.FirstName, p.Name AS ProjectName
      FROM Employees e
 JOIN EmployeesProjects  ep ON e.EmployeeID = ep.EmployeeID
 JOIN Projects  p ON ep.ProjectID = p.ProjectID
WHERE p.StartDate > '2002-08-13' AND p.EndDate IS NULL
ORDER BY e.EmployeeID ASC


/*Employee 24*/

SELECT e.EmployeeID, e.FirstName, p.Name AS ProjectName 
    FROM Employees e
JOIN EmployeesProjects ep ON e.EmployeeID = ep.EmployeeID
LEFT JOIN Projects p ON ep.ProjectID = p.ProjectID
AND p.StartDate <= '2005-01-01'
WHERE  e.EmployeeID = 24


/*Employee Manager*/
SELECT e.EmployeeID,e.FirstName,em.EmployeeID AS ManagerID,em.FirstName AS ManagerName
    FROM Employees AS e
JOIN Employees  em ON em.EmployeeID = e.ManagerID
WHERE e.ManagerID IN (3,7) 
 ORDER BY e.EmployeeID

/*Employees Summary*/

SELECT Top(50) e.EmployeeID, CONCAT_WS(' ', e.FirstName,e.LastName) AS EmployeeName, CONCAT_WS(' ',em.FirstName,em.LastName) AS ManagerName, d.Name AS DepartmentName 
      FROM Employees e
JOIN Employees em ON em.EmployeeID = e.ManagerID
JOIN Departments d ON e.DepartmentID = d.DepartmentID
ORDER BY e.EmployeeID
/* by judje */
SELECT Top(50) e.EmployeeID, e.FirstName + ' ' + e.LastName AS EmployeeName, em.FirstName + ' ' + em.LastName AS ManagerName, d.Name AS DepartmentName 
      FROM Employees e
JOIN Employees em ON em.EmployeeID = e.ManagerID
JOIN Departments d ON e.DepartmentID = d.DepartmentID
ORDER BY e.EmployeeID

/*Min Average Salary*/
SELECT Top(1)
     (SELECT AVG(Salary)
	       FROM Employees e
		   WHERE e.DepartmentID = d.DepartmentID) AS MinAverageSalary
    FROM Departments d
	ORDER BY MinAverageSalary ASC

