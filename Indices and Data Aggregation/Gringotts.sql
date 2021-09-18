USE Gringotts

/* Records’ Count */
SELECT COUNT(*) AS [Count] 
     FROM WizzardDeposits 

/* Longest Magic Wand */

SELECT MAX(MagicWandSize) AS LongestMagicWand 
     FROM WizzardDeposits

/* Longest Magic Wand per Deposit Groups */
SELECT DepositGroup, MAX(MagicWandSize) AS LongestMagicWand
     FROM WizzardDeposits
GROUP BY DepositGroup

/* Smallest Deposit Group Per Magic Wand Size */
SELECT Top(2) DepositGroup
     FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY AVG(MagicWandSize)

/* Deposits Sum */
SELECT DepositGroup, SUM(DepositAmount) AS TotalSum
      FROM WizzardDeposits
GROUP BY DepositGroup

/* Deposits Sum for Ollivander Family */
SELECT DepositGroup, SUM(DepositAmount) AS TotalSum
     FROM WizzardDeposits w
WHERE w.MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup


/* Deposits Filter */
SELECT DepositGroup, SUM(DepositAmount) AS TotalSum
     FROM WizzardDeposits w
WHERE w.MagicWandCreator = 'Ollivander family'  
GROUP BY DepositGroup
 HAVING  SUM(DepositAmount) < 150000 
 ORDER BY  TotalSum  DESC

 /*Deposit Charge */
SELECT DepositGroup, MagicWandCreator, MIN(DepositCharge)  AS MinDepositCharge
      FROM WizzardDeposits
GROUP BY DepositGroup, MagicWandCreator
ORDER BY MagicWandCreator, DepositGroup


/* Age Groups */
SELECT AllAges AS AgeGroups, COUNT(*) AS WisardCount
     FROM( SELECT
     CASE
	    WHEN Age BETWEEN 0 AND 10 THEN '[0-10]'
	    WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
	    WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
	    WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
	    WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
	    WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
	    WHEN Age >= 61 THEN '[61+]'
		END AS AllAges
     FROM WizzardDeposits wd ) AS DataAge
GROUP BY AllAges


/*First Letter*/
SELECT LEFT(FirstName, 1) AS FirstLetter
      FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest' 
GROUP BY LEFT(FirstName, 1)


/* Average Interest */

SELECT DepositGroup, IsDepositExpired, AVG(DepositInterest) AS AverageInterest
     FROM WizzardDeposits wd
WHERE DepositStartDate > '1/1/1985'
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired

/* Rich Wizard, Poor Wizard */

