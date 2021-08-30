/*Countries Holding ‘A’ 3 or More Times*/
USE Geography

SELECT CountryName AS [Country Name],
       IsoCode	   AS [ISO Code]
      FROM   Countries
      WHERE  CountryName LIKE '%a%a%a%'
      ORDER BY IsoCode

/*Mix of Peak and River Names*/
SELECT PeakName,RiverName,
     LOWER(PeakName + SUBSTRING(RiverName,2,LEN(RiverName))) 
	     AS  [Mixed]
     FROM   Peaks,Rivers
     WHERE  LEFT(RiverName,1) = RIGHT(PeakName,1)
     ORDER BY [Mixed]



