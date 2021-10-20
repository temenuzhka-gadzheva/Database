use MinionsDB
/*from second ex*/
SELECT [Name], COUNT(mv.MinionId) FROM Villains AS v
LEFT JOIN MinionsVillains  AS mv ON MV.VillainId = v.Id
GROUP BY v.Id, v.[Name]
HAVING COUNT(mv.MinionId) > 3

/*form 3 ex*/
SELECT [Name] FROM Villains WHERE Id = @Id
SELECT ROW_NUMBER() OVER (ORDER BY m.[Name]) AS RowNum, m.[Name], m.Age
     FROM MinionsVillains AS mv
 JOIN Minions AS m ON mv.MinionId = m.Id
	 WHERE mv.VillainId = @Id
	 ORDER BY m.[Name]

/*from ex 4*/
SELECT Id FROM Villains WHERE [Name] = @Name
SELECT Id FROM Minions WHERE [Name] = @Name
INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)
INSERT INTO Villains ([Name],EvilnessFactorId) VALUES (@villainNAME,4)
INSERT INTO Minions ([Name],Age,TownId) VALUES (@name,@age, @townId)
INSERT INTO Towns ([Name]) VALUES (@townName)
SELECT Id FROM Towns WHERE [Name] = @townName

/*from ex 5*/

UPDATE Towns
   SET [Name] = UPPER([Name])
   WHERE CountryCode = 
   ( SELECT c.Id FROM Countries AS c WHERE c.[Name] = @countryCode)

   SELECT t.[Name]
       FROM Towns AS t
	   JOIN Countries AS c ON c.Id = t.CountryCode
	   WHERE c.[Name] = @countryName

	   /*from ex 6*/

	   SELECT [Name] FROM Villains WHERE Id = @villainId
	   DELETE FROM MinionsVillains WHERE VillainId = @villainId
	   DELETE FROM Villains WHERE Id = @villainId"


/*from ex 7*/

SELECT [Name] FROM Minions

/*from ex 8*/
UPDATE Minion 
  SET [Name] = UPPER(LEFT([Name],1)) + 
   SUBSTRING([Name], 2,LEN([Name])), Age+=1
     WHERE Id  = @Id"

	 SELECT [Name], Age FROM Minions

/*from ex 9*/
CREATE PROC usp_GetOlder @id INT
AS 
UPDATE Minions
 SET Age += 1
 WHERE Id = @id

 SELECT [Name], Age FROM Minions WHERE Id = @Id