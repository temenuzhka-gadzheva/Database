USE Geography
/*Peaks in Rila*/
SELECT MountainRange,PeakName,Elevation
  FROM Peaks
  JOIN Mountains ON Peaks.MountainId = Mountains.Id
  WHERE MountainRange = 'Rila'
  ORDER BY Elevation DESC