CREATE DATABASE University
USE University

/*University Database*/

CREATE TABLE Majors
( MajorID INT PRIMARY KEY IDENTITY,
  [Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Subjects
( SubjectID INT PRIMARY KEY IDENTITY,
  SubjectName VARCHAR(50) NOT NULL
)

CREATE TABLE Students
( StudentID INT PRIMARY KEY IDENTITY,
  StudentNumber VARCHAR(50)NOT NULL,
  StudentName NVARCHAR(100) NOT NULL,
  MajorID INT REFERENCES Majors(MajorID)
)

CREATE TABLE Payments
( PaymentID INT PRIMARY KEY IDENTITY,
  PaymentDate DATE,
  PaymentAmount DECIMAL(5,2),
  StudentID INT REFERENCES Students(StudentID)
)

CREATE TABLE Agenda
( StudentID INT REFERENCES Students(StudentID),
   SubjectID INT REFERENCES Subjects(SubjectID),
   PRIMARY KEY (StudentID, SubjectID)
)