CREATE DATABASE Hotel

USE Hotel

CREATE TABLE Employees(
Id INT IDENTITY ,
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(50) NOT NULL,
Title VARCHAR(50),
Notes VARCHAR(MAX)
)
  
CREATE TABLE Customers(
Id INT IDENTITY,
AccountNumber BIGINT NOT NULL,
FirstName VARCHAR(50)NOT NULL,
LastName VARCHAR(50) NOT NULL,
PhoneNumber VARCHAR(15) NOT NULL,
EmergencyName VARCHAR(150) NOT NULL,
EmergencyNumber VARCHAR(15) NOT NULL,
Notes VARCHAR(100)
)

 
CREATE TABLE RoomStatus(
Id INT IDENTITY ,
RoomStatus BIT,
Notes VARCHAR(MAX)
)
 
CREATE TABLE RoomTypes(
RoomType VARCHAR(50) PRIMARY KEY,
Notes VARCHAR(MAX)
)
  
CREATE TABLE BedTypes(
BedType VARCHAR(50) PRIMARY KEY,
Notes VARCHAR(MAX)
)
  
CREATE TABLE Rooms (
RoomNumber INT IDENTITY ,
RoomType VARCHAR(50) NOT NULL,
BedType VARCHAR(50) NOT NULL,
Rate DECIMAL(6,2),
RoomStatus NVARCHAR(50),
Notes NVARCHAR(MAX)
)
  
CREATE TABLE Payments(
Id INT IDENTITY ,
EmployeeId INT NOT NULL,
PaymentDate DATE NOT NULL,
AccountNumber BIGINT NOT NULL,
FirstDateOccupied DATE NOT NULL,
LastDateOccupied DATE NOT NULL,
TotalDays AS DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied),
AmountCharged DECIMAL(14,2),
TaxRate DECIMAL(8, 2) NOT NULL,
TaxAmount DECIMAL(8, 2) NOT NULL,
PaymentTotal DECIMAL(15, 2) NOT NULL,
Notes VARCHAR(MAX)
)
 
 
CREATE TABLE Occupancies(
Id INT IDENTITY ,
EmployeeId INT NOT NULL,
DateOccupied DATE,
AccountNumber BIGINT,
RoomNumber INT NOT NULL,
RateApplied DECIMAL(6,2),
PhoneCharge DECIMAL(6,2),
Notes VARCHAR(MAX)
)
 

INSERT INTO Employees(FirstName, LastName, Title, Notes)
VALUES
('Peter', 'Parkar', 'Receptionist', 'Nice customer'),
('Natasha', 'Romanov', 'Concierge', 'Nice one'),
('Elisaveta', 'Bagriana', 'Cleaner', 'Poetesa')

 
INSERT INTO Customers(AccountNumber, FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber, Notes)
VALUES
(123456789, 'Jonny', 'Smith', '0887170702', 'Sistry mi', '7708315342', 'Kinky'),
(123480933, 'Samantha', 'Isaeva', '0893333256', 'Sistry mi', '7708315342', 'Lawer'),
(123454432, 'Billy', 'Bosev', '0882456303', 'Sistry mi', '7708315342', 'Wants a call girl')

INSERT INTO RoomStatus(RoomStatus, Notes)
VALUES
(123,'Refill the minibar'),
(234,'Check the towels'),
(345,'Move the bed for couple')

INSERT INTO RoomTypes (RoomType, Notes)
VALUES
('Suite', 'Two beds'),
('Wedding suite', 'One king size bed'),
('Apartment', 'Up to 3 adults and 2 children')

INSERT INTO BedTypes(BedType, Notes)
VALUES
('Double', 'One adult and one child'),
('King size', 'Two adults'),
('Couch', 'One child')

INSERT INTO Rooms (RoomType, BedType,Rate, Notes)
VALUES
('Suite','King size',12,'Free'),
('Wedding suite','Couch',15, 'Free'),
('Apartment','Double',23, 'Cleaning it')

INSERT INTO Payments (EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, TaxRate, TaxAmount, PaymentTotal)
VALUES
(2,'2014-12-15',123,'2014-12-12','2014-12-13',2450.10,122.33,456.77),
(3,'2016-04-23',234,'2016-04-22','2016-04-25',100.50,45.87,765.79),
(1,'2021-03-01',456,'2021-03-03','2021-03-05',1200.60,567.65,465.92)

INSERT INTO Occupancies (EmployeeId,RoomNumber,RateApplied, Notes) VALUES
(1,3,55.55, 'too'),
(2,1,15.55, 'much'),
(3,2,35.55, 'typing')

/*descrease tax rate*/

UPDATE Payments
SET TaxRate = TaxRate - (TaxRate * 0.03)

SELECT TaxRate FROM Payments


/*delete all records from the Occupancies*/
 DELETE FROM Occupancies