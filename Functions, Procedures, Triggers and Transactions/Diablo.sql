USE Diablo
/* Scalar Function: Cash in User Games Odd Rows */

CREATE OR ALTER FUNCTION ufn_CashInUsersGames (@gameName VARCHAR(150))
RETURNS TABLE
 AS
    RETURN  (SELECT SUM(rn.TotalCash)  AS TotalCash
            FROM(SELECT Cash AS TotalCash, 
             ROW_NUMBER() OVER(ORDER BY Cash DESC) AS [Row number]
                  FROM  Games g
                 LEFT JOIN UsersGames ug ON ug.GameId = g.Id
                  WHERE Name = @gameName) AS rn
	          WHERE rn.[Row number] % 2 = 1)