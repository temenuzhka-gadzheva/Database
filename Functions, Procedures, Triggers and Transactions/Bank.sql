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
