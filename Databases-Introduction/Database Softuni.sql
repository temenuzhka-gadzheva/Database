CREATE DATABASE SoftUni

USE SoftUni

CREATE TABLE Towns (
	Id INT PRIMARY KEY ,
    [Name] VARCHAR(45)
)
 
CREATE TABLE Addresses (
	Id INT PRIMARY KEY ,
    AddressText VARCHAR(45),
    TownId INT,
    CONSTRAINT fk_Addresses_Towns
    FOREIGN KEY (TownId)
    REFERENCES Towns(Id)
);
 
CREATE TABLE Departments (
	Id INT PRIMARY KEY ,
    [Name] VARCHAR(45)
);
 
CREATE TABLE Employees (
   Id INT PRIMARY KEY ,
   FirstName VARCHAR(30) NOT NULL,
   MiddleName VARCHAR(30) NOT NULL,
   LastName VARCHAR(30) NOT NULL,
   JobTitle VARCHAR(20) NOT NULL,
   DepartmentId INT NOT NULL,
   HireDate DATE,
   Salary DECIMAL(19, 2),
    AddressId INT,
    CONSTRAINT fk_Employees_Departments
    FOREIGN KEY (DepartmentId)
    REFERENCES Departments(Id),
    CONSTRAINT fk_Employees_Addresses
    FOREIGN KEY (AddressId)
    REFERENCES Addresses(Id)
);
 
INSERT INTO Towns(Id,[Name])
VALUES
(1, 'Sofia'),
(2, 'Plovdiv'),
(3, 'Varna'),
(4, 'Burgas');
 
INSERT INTO Departments(Id,[Name])
VALUES
(1, 'Engineering'), 
(2, 'Sales'),
(3, 'Marketing'),
(4, 'Software Development'),
(5, 'Quality Assurance');
 
INSERT INTO Employees (Id, FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary, AddressId)
VALUES
(1, 'Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013-02-01', 3500.00, 3),
(2, 'Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004-03-02', 4000.00,4),
(3, 'Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016-08-28', 525.25, 1),
(4, 'Georgi', 'Terziev', 'Ivanov', 'CEO', 2, '2007-12-09', 3000.00,5),
(5, 'Peter', 'Pan', 'Pan', 'Intern', 3, '2016-08-28', 599.88, 2);

