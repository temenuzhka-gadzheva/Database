USE Minions


 CREATE TABLE Users 
 ( Id BIGINT PRIMARY KEY IDENTITY,
   Username VARCHAR(30) NOT NULL ,
   [Password] VARCHAR(26) NOT NULL,
   ProfilePicture VARCHAR(MAX),
   LastLoginTime DATETIME,
   IsDeleted BIT
 )


INSERT INTO Users 
(Username,[Password],ProfilePicture,LastLoginTime,IsDeleted)
VALUES
  ('Pesho','123456','https://github.com/rothja.png?size=32','1/10/2020',0),
  ('Alex','alex34','https://github.com/rothja.png?size=32','6/12/2015',1),
  ('Bob','vuvi23','https://github.com/rothja.png?size=32','4/05/2014',0),
  ('Kevin','kev45','https://github.com/rothja.png?size=32','8/04/2018',1),
  ('Samanta','samy21c','https://github.com/rothja.png?size=32','1/03/2001',0)


/*Change primary key and create from to or more colums one unique key*/
  ALTER TABLE Users
  DROP CONSTRAINT PK__Users__3214EC07391CDFD8

  ALTER TABLE Users
  ADD CONSTRAINT PK_IdUsername PRIMARY KEY (Id,Username) 


  /*Check if the password is longer than 5 symbols*/
    ALTER TABLE Users
  ADD CONSTRAINT CH_PasswordIsLongerThan5Symbols CHECK (LEN([Password]) > 5)

  /*Set default value of LastLoginTime*/
    ALTER TABLE Users
   ADD CONSTRAINT DF_LastLoginTime DEFAULT GETDATE() FOR LastLoginTime

   /*Set unique field*/

  ALTER TABLE Users
 DROP CONSTRAINT PK_IdUsername

 ALTER TABLE Users
 ADD CONSTRAINT PK_Id PRIMARY KEY (Id)

  ALTER TABLE Users
  ADD CONSTRAINT CH_UsernameIsAtLeast3Symbols CHECK (LEN(Username) > 3)
