USE Store
/* Orders table */

SELECT ProductName,OrderDate,
	    DATEADD(DAY,3,OrderDate) AS [Pay Due],
	    DATEADD(MONTH,1,OrderDate) AS [Deliver Due]
  FROM Orders

/* People Table */
USE Example
CREATE TABLE People
( ID INT IDENTITY PRIMARY KEY ,
  [Name] NVARCHAR(50) NOT NULL,
  Birthdate DATETIME2 
)

INSERT INTO People VALUES
    ('Maya', '1999-08-05'),
    ('Pesho', '1992-01-01'),
    ('Stamat', '2003-06-12'),
    ('Marty', '2001-03-01')

SELECT [Name],
    DATEDIFF(YEAR,Birthdate, GETDATE()) AS [Age in Years],
    DATEDIFF(MONTH,Birthdate, GETDATE()) AS [Age in Months],
    DATEDIFF(DAY,Birthdate, GETDATE()) AS [Age in Days],
    DATEDIFF(MINUTE,Birthdate, GETDATE()) AS [Age in Minutes]
FROM People	