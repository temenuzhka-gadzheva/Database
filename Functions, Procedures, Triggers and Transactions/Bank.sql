USE Bank

/* Find Full Name */

CREATE  PROCEDURE usp_GetHoldersFullName 
 AS 
   BEGIN 
    SELECT FirstName + ' ' + LastName AS [Full Name] FROM AccountHolders
 END

 EXEC usp_GetHoldersFullName

 /* People with Balance Higher Than */

 CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan (@Number DECIMAL(18,4))
 AS 
  BEGIN
   
 SELECT ah.FirstName, ah.LastName
       FROM AccountHolders ah
  LEFT JOIN  Accounts a ON a.AccountHolderId = ah.Id
  GROUP BY ah.FirstName, ah.LastName
  HAVING SUM(a.Balance) > @Number
  ORDER BY ah.FirstName, ah.LastName
 END


 EXEC usp_GetHoldersWithBalanceHigherThan  @Number = 123

 
/* Future Value Function */

CREATE  FUNCTION ufn_CalculateFutureValue (@sum DECIMAL(18,4), @yearlyInterestRate FLOAT, @theNumberOfYears DECIMAL(10,2))
RETURNS DECIMAL(18,4)
 AS
  BEGIN 
   DECLARE @result  DECIMAL(18,4)
   SELECT @result  = @sum * (POWER((1+ @yearlyInterestRate),@theNumberOfYears))
	
	RETURN @result
 END


SELECT  dbo.ufn_CalculateFutureValue (1000, 0.1,5)


/* Calculating Interest */
 
CREATE OR ALTER PROCEDURE usp_CalculateFutureValueForAccount (@accountId INT,@yearlyInterestRate FLOAT)
 AS 
  BEGIN 
     SELECT
	   a.Id,
	   ah.FirstName, 
	   ah.LastName, 
	   a.Balance,
	    dbo.ufn_CalculateFutureValue (a.Balance,@yearlyInterestRate,5)
	       FROM AccountHolders ah
	   JOIN Accounts a ON ah.Id = a.AccountHolderId
	  WHERE a.Id = @accountId
  END

  EXECUTE usp_CalculateFutureValueForAccount @accountId = 1, @yearlyInterestRate = 0.1

