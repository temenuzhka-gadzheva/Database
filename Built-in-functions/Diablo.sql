USE Diablo

/*Games From 2011 and 2012 Year*/
SELECT  TOP(50) [Name], 
      Format([Start], 'yyy-MM-dd') AS [Start] 
      FROM Games 
	  WHERE YEAR([Start]) BETWEEN 2011 AND 2012
	 ORDER BY [Start], [Name]

/*User Email Providers*/
SELECT Username,
      SUBSTRING(Email,
	          CHARINDEX('@',Email,1) + 1, 
			  LEN(Email)) AS [Email Provider]
      FROM Users
	  ORDER BY [Email Provider] ASC,
	            Username ASC	

/*Get Users with IPAdress Like Pattern*/
SELECT Username,IpAddress AS [IP Address]
     FROM Users
     WHERE IpAddress LIKE '___.1%.%.___'
     ORDER BY Username

/*Show All Games with Duration and Part of the Day*/

SELECT [Name] AS Game,[Part of the Day] = 
		CASE
		    WHEN DATEPART(HOUR, Start) < 12 
			     THEN 'Morning'
		    WHEN DATEPART(HOUR, Start) < 18 
			     THEN 'Afternoon'
		    ELSE 'Evening'
		END
		,Duration =  
		CASE
		   WHEN Duration <= 3 
		      THEN 'Extra Short'
		   WHEN Duration <= 6 
		        THEN 'Short'
		   WHEN Duration > 6 
		        THEN 'Long'
		   ELSE 'Extra Long'
		END
    FROM Games
       ORDER BY Game, Duration, [Part of the Day]