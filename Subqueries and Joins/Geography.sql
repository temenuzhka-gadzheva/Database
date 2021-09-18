USE Geography

/*Highest Peaks in Bulgaria*/
SELECT c.CountryCode, m.MountainRange, p.PeakName, p.Elevation 
     FROM Peaks p
 LEFT JOIN Mountains m ON p.MountainId = m.Id
 LEFT JOIN MountainsCountries mc ON m.Id = mc.MountainId
 LEFT JOIN Countries c ON mc.CountryCode = c.CountryCode
 WHERE c.CountryCode  LIKE 'BG' AND p.Elevation > 2835
 ORDER BY p.Elevation DESC

 /*Count Mountain Ranges*/

SELECT mc.CountryCode ,
COUNT(*) AS MountainRanges
FROM Mountains  m
JOIN MountainsCountries  mc ON mc.MountainId = m.Id
WHERE mc.CountryCode ='BG'
   OR mc.CountryCode ='US'
   OR mc.CountryCode = 'RU'
GROUP BY mc.CountryCode 

/*Countries with  or without Rivers*/
SELECT Top(5) c.CountryName, r.RiverName 
     FROM  Rivers r
RIGHT JOIN CountriesRivers cr ON cr.RiverId = r.Id
RIGHT JOIN Countries c ON c.CountryCode = cr.CountryCode
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName  ASC

/*Continents and Currencies*/
SELECT ContinentCode, CurrencyCode, Total AS CurrencyUsage
     FROM 
	 (SELECT ContinentCode, CurrencyCode, COUNT(CurrencyCode) AS Total,
	   DENSE_RANK() OVER(PARTITION BY ContinentCode ORDER BY COUNT(CurrencyCode) DESC) AS Ranked
           FROM Countries
       GROUP BY ContinentCode, CurrencyCode) AS k
	   WHERE Ranked = 1 AND Total > 1
	  ORDER BY ContinentCode

/*Countries without any Mountains*/
SELECT COUNT(*) AS [Count]
    FROM Countries c
LEFT JOIN MountainsCountries mc ON mc.CountryCode = c.CountryCode
WHERE mc.MountainId  IS NULL

/*Highest peak and longest river by country*/
SELECT TOP(5) c.CountryName, MAX(p.Elevation)AS HighestPeakElevation, MAX(r.Length) AS LongestRiverLength
      FROM Countries c
LEFT JOIN MountainsCountries mc ON mc.CountryCode = c.CountryCode
LEFT JOIN Mountains m ON m.Id = mc.MountainId
LEFT JOIN Peaks p ON p.MountainId = m.Id
LEFT JOIN CountriesRivers cr ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers r ON r.Id = cr.RiverId
GROUP BY c.CountryName
ORDER BY HighestPeakElevation DESC, LongestRiverLength DESC, c.CountryName

/*Highest peak name and elevation by country*/
SELECT  TOP(5) k.CountryName, k.[Highest Peak Name], k.[Highest Peak Elevation], k.Mountain
      FROM (
	  SELECT CountryName,
      ISNULL( p.PeakName, '(no highest peak)') AS [Highest Peak Name],
	  ISNULL(m.MountainRange,'(no mountain)') AS Mountain, 
	  ISNULL(MAX(p.Elevation), 0) AS [Highest Peak Elevation], 
	  DENSE_RANK() OVER (PARTITION BY CountryName ORDER BY MAX(p.Elevation) DESC) AS Ranked
      FROM Countries c
LEFT JOIN MountainsCountries mc ON mc.CountryCode = c.CountryCode
LEFT JOIN Mountains m ON m.Id = mc.MountainId
LEFT JOIN Peaks p ON p.MountainId = m.Id
GROUP BY CountryName,p.PeakName,m.MountainRange) AS k
WHERE Ranked = 1
ORDER BY CountryName, [Highest Peak Name]


