CREATE DATABASE CarRental 

USE CarRental

CREATE TABLE Categories
 ( Id INT PRIMARY KEY,
   CategoryName VARCHAR(40) NOT NULL,
   DailyRate SMALLMONEY,
   WeeklyRate SMALLMONEY,
   MonthlyRate SMALLMONEY,
   WeekendRate SMALLMONEY,
 )

 CREATE TABLE Cars 
 ( Id INT PRIMARY KEY,
   PlateNumber INT NOT NULL,
   Manufacturer VARCHAR(40) NOT NULL,
   Model VARCHAR(50),
   CarYear INT NOT NULL,
   CategoryId INT NOT NULL,
   Doors INT,
   Picture VARCHAR(1200),
   Condition VARCHAR(120) NOT NULL,
   Available BIT
 )

  CREATE TABLE Employees  
 ( Id INT PRIMARY KEY,
   FirstName VARCHAR(40) NOT NULL,
   LastName VARCHAR(40) NOT NULL,
   Title VARCHAR(250) NOT NULL,
   Notes FLOAT(2)
 )

  CREATE TABLE Customers  
 ( Id INT PRIMARY KEY,
   DriverLicenceNumber INT NOT NULL,
   FullName VARCHAR(50) NOT NULL,
   [Address] VARCHAR(100) NOT NULL,
   City VARCHAR(70)NOT NULL,
   ZIPCode INT NOT NULL,
   Notes FLOAT(2)
 )

  CREATE TABLE RentalOrders  
 ( Id INT PRIMARY KEY,
   EmployeeId INT NOT NULL,
   CustomerId INT NOT NULL,
   CarId INT NOT NULL,
   TankLevel INT,
   KilometrageStart INT NOT NULL,
   KilometrageEnd INT NOT NULL,
   TotalKilometrage INT NOT NULL,
   Manufacturer VARCHAR(40) NOT NULL,
   StartDate VARCHAR(300) NOT NULL,
   EndDate VARCHAR(300) NOT NULL,
   TotalDays INT NOT NULL,
   RateApplied FLOAT(2),
   TaxRate INT NOT NULL,
   OrderStatus BIT NOT NULL,
   Notes FLOAT(2)
 )


 INSERT INTO Categories(Id,CategoryName,DailyRate,WeeklyRate,MonthlyRate,WeekendRate) VALUES
 (1,'Sports',80,200,600,100),
 (2,'Families',100,400,1000,500),
 (3,'Drfting',120,340,1500,600)

 INSERT INTO Cars(Id, PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available) VALUES
 (1,1073,'MCaran Company','MClaran turbo 200',2021,1,2,'https://1036981-static-assets-portal-production.s3.amazonaws.com/media/images/carousel_videos/mobile/thumb/McLaren_P14RS_Static_Front_34_1.png','to be speed',0),
 (3,2066,'BMV Company','BMV X5',2019,3,5,'https://www.bmw.bg/content/dam/bmw/common/all-models/x-series/x5/2019/highlights/bmw-x5-highlights-gallery-desktop-02.jpg','should drift',1),
 (2,1405,'Audi Company','Audi Q7 SUV',2020,2,4,'https://cdn.audi.ua/media/Kwc_Box_MetaTagsContent_OpenGraphImage_Component/19501-metaTags-ogImage/dh-1800-23ade3/f2edb6a3/1627886241/audi-galerie-4.jpg','to be iconomic',1)

 INSERT INTO Employees(Id, FirstName, LastName, Title, Notes) VALUES
 (1,'Peter','Parker','The best super hero', 20.00), 
 (2,'Natasha','Romanov','The best woman of heroes', 18.50), 
 (3,'Ben','Franklin','The best people', 12.45)

 INSERT INTO Customers(Id, DriverLicenceNumber, FullName, [Address], City, ZIPCode, Notes) VALUES
 (1,123,'Iron Man','Brooklin 5','New York', 12, 23.89),
 (2,345,'Wonder Woman','Siatle 6','Las Vegas', 43, 22.89), 
 (3,543,'Vision','Station 3','Las Vegas', 34, 21.69)

 INSERT INTO RentalOrders (Id, EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, TotalKilometrage, Manufacturer,StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes) VALUES
 (1,2,1,1,10,122,234,555,'MClaran','2020/03/1','2020/03/5',4,12.45,45,1,23.45), 
 (3,1,2,3,8,234,456,543,'BMV','2029/02/19','2020/02/23',5,11.65,34,2,18.24),
 (2,3,3,2,5,12,234,566,'Opel','2018/07/12','2018/07/15',3,7.89,25,3,17.56)

