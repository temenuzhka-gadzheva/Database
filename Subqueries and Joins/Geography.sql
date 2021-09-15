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

/*Countries with Rivers*/
SELECT Top(5) c.CountryName, r.RiverName 
     FROM Countries c
RIGHT JOIN CountriesRivers cr ON cr.CountryCode = c.CountryCode
RIGHT JOIN Rivers r ON r.Id = cr.RiverId
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName  ASC