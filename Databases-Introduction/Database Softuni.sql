CREATE DATABASE SoftUni

USE SoftUni

CREATE TABLE Towns (
	Id INT IDENTITY ,
    [Name] VARCHAR(45)
)
 
CREATE TABLE Addresses (
	Id INT IDENTITY ,
    AddressText VARCHAR(45),
    TownId INT
);
 
CREATE TABLE Departments (
	Id INT IDENTITY ,
    [Name] VARCHAR(45)
);
 
CREATE TABLE Employees (
	Id INT IDENTITY ,
   FirstName VARCHAR(30) NOT NULL,
   MiddleName VARCHAR(30) NOT NULL,
   LastName VARCHAR(30) NOT NULL,
   JobTitle VARCHAR(20) NOT NULL,
   DepartmentId INT NOT NULL,
   HireDate DATE,
   Salary DECIMAL(19, 2),
    AddressId INT
);
 
INSERT INTO Towns([Name])
VALUES
('Sofia'),
('Plovdiv'),
('Varna'),
('Burgas');
 
INSERT INTO Departments([Name])
VALUES
('Engineering'), 
('Sales'),
('Marketing'),
('Software Development'),
('Quality Assurance');
 
INSERT INTO Employees (FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary, AddressId)
VALUES
('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013-02-01', 3500.00, 3),
('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004-03-02', 4000.00,4),
('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016-08-28', 525.25, 1),
('Georgi', 'Terziev', 'Ivanov', 'CEO', 2, '2007-12-09', 3000.00,5),
('Peter', 'Pan', 'Pan', 'Intern', 3, '2016-08-28', 599.88, 2);

/*select all fields*/

SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

/*select all fields alphabetically by name for Towns and Deparrtments*/

SELECT * FROM Towns
 ORDER BY [Name] ASC

 SELECT * FROM Departments
  ORDER BY [Name] ASC

  /*select all fields descending by salary*/

  SELECT * FROM Employees
  ORDER BY Salary DESC

  /*selsct some fileds from table*/

  SELECT [Name] FROM Towns
  ORDER BY [Name] ASC

  SELECT [Name] FROM Departments
    ORDER BY [Name] ASC

  SELECT FirstName, LastName, JobTitle, Salary FROM Employees
  ORDER BY Salary DESC

  /*increase employees salary*/

  UPDATE Employees
  SET Salary *=  1.1 

  SELECT Salary FROM Employees
