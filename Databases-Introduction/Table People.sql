USE Minions

/*People table*/
  CREATE TABLE People 
  ( Id INT PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(200) NOT NULL,
    Picture VARCHAR(MAX),
    Height FLOAT(2),
    [Weight] FLOAT(2),
    Gender CHAR NOT NULL CHECK (Gender = 'm' OR Gender = 'f'),
    Birthdate DATETIME NOT NULL,
    Biography NVARCHAR(MAX)
  )

  INSERT INTO People ([Name], Picture, Height, [Weight],Gender,Birthdate,Biography)
   VALUES 
  ('Pesho','https://github.com/rothja.png?size=32',1.65,65.00,'m','4/10/2020','doctor'),
  ('Alex','https://github.com/rothja.png?size=32',1.23,45.00,'m','6/09/2023','programmer'),
  ('Bob','https://github.com/rothja.png?size=32',1.98,64.00,'m','12/07/2017','dentist'),
  ('Kevin','https://github.com/rothja.png?size=32',1.55,56.00,'m','8/03/2004','pilot'),
  ('Samanta','https://github.com/rothja.png?size=32',1.12,78.00,'f','1/10/2015','avendure')