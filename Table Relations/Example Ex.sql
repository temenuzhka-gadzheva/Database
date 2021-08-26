CREATE DATABASE Example
USE Example

/*One-To-One Relationship*/
CREATE TABLE Passports
( PassportID INT PRIMARY KEY,
  PassportNumber VARCHAR(20) NOT NULL
)

CREATE TABLE Persons
( PersonID INT  PRIMARY KEY IDENTITY,
  FirstName VARCHAR(100) NOT NULL,
  Salary DECIMAL(10,2) NOT NULL,
  PassportID INT REFERENCES Passports(PassportID)
)

INSERT INTO Passports(PassportID, PassportNumber) VALUES
(101,'N34FG21B'),
(102,'K65LO4R7'),
(103,'ZE657QP2')

INSERT INTO Persons (FirstName,Salary,PassportID) VALUES
('Roberto', 43300.00,102),
('Tom', 56100.00,103),
('Yana', 60200.00,101)

/*SELECT * 
     FROM Persons
	 JOIN Passports ON Persons.PassportID = Passports.PassportID*/

/*One-To-Many Relationship*/
CREATE TABLE Manufacturers
( ManufacturerID INT PRIMARY KEY IDENTITY,
  [Name] VARCHAR(100) NOT NULL,
  EstablishedOn DATETIME2 NOT NULL
)

CREATE TABLE Models
( ModelID INT PRIMARY KEY IDENTITY,
  [Name] VARCHAR(100) NOT NULL,
  ManufacturerID INT REFERENCES Manufacturers(ManufacturerID)
)

INSERT INTO Manufacturers ([Name],EstablishedOn) VALUES
('BMW','1916/03/07'),
('Tesla','2003/01/01'),
('Lada','1966/05/01')


INSERT INTO Models ([Name],ManufacturerID) VALUES
('X1',1),
('i6',1),
('Model S',2),
('Model X',2),
('Model 3',2),
('Nova',3)

/*SELECT * FROM Models 
  JOIN Manufacturers ON Manufacturers.ManufacturerID = Models.ManufacturerID*/

  /*Many-To-Many Relationship*/

  CREATE TABLE Students 
  ( StudentID INT PRIMARY KEY IDENTITY,
    [Name] VARCHAR(40) NOT NULL
  )

  CREATE TABLE Exams
  ( ExamID INT PRIMARY KEY IDENTITY(101,1),
    [Name] VARCHAR(40) NOT NULL
  )

  CREATE TABLE StudentsExams
 ( StudentID INT REFERENCES Students (StudentID),
   ExamID INT REFERENCES Exams (ExamID),
   PRIMARY KEY(StudentID, ExamID)
 )


 INSERT INTO Students ([Name]) VALUES
  ('Mila'),  
  ('Toni'), 
  ('Ron')

  INSERT INTO Exams ([Name]) VALUES
  ('SpringMVC'),
  ('Neo4j'),
  ('Oracle 11g')

  INSERT INTO StudentsExams (StudentID,ExamID) VALUES
  (1,101),
  (1,102),
  (2,101),
  (3,103),
  (2,102),
  (2,103)

/*Self-Referencing */

CREATE TABLE Teachers
( TeacherID INT PRIMARY KEY IDENTITY (101,1),
  [Name] VARCHAR(50) NOT NULL,
  ManagerID INT REFERENCES Teachers(TeacherID) 
)

INSERT INTO Teachers ([Name], ManagerID) VALUES
('John',NULL),
('Maya',106),
('Silvia',106),
('Ted',105),
('Mark',101),
('Greta',101)



