CREATE DATABASE Movies

USE Movies

CREATE TABLE Directors
( Id INT PRIMARY KEY IDENTITY,
  DirectorName VARCHAR(100) NOT NULL,
  Notes FLOAT(2)  NOT NULL
)


CREATE TABLE Genres
( Id INT PRIMARY KEY IDENTITY,
  GenreName VARCHAR(100) NOT NULL,
  Notes FLOAT(2) NOT NULL
)

CREATE TABLE Categories
( Id INT PRIMARY KEY IDENTITY,
  CategoryName VARCHAR(100) NOT NULL,
  Notes FLOAT(2) NOT NULL
)

CREATE TABLE Movies
( Id INT PRIMARY KEY IDENTITY,
  Title VARCHAR(500) NOT NULL,
  DirectorId INT NOT NULL,
  CopyrightYear INT NOT NULL,
  [Length] INT NOT NULL,
  GenreId INT NOT NULL,
  CategoryId INT NOT NULL,
  Rating SMALLINT,
  Notes FLOAT(2) NOT NULL
)

INSERT INTO Directors (DirectorName,Notes)
VALUES
  ('Boby',2.60),
  ('Alex',3.90),
  ('Peter',5.75),
  ('Mary',6.00),
  ('Yanna',5.49)

   INSERT INTO Genres(GenreName,Notes)
VALUES
  ('Drama',6.60),
  ('Lovely',7.90),
  ('Mistery',9.75),
  ('Comedy',10.00),
  ('Fantastic',6.49)

 INSERT INTO Categories(CategoryName,Notes)
VALUES
  ('Drama',6.60),
  ('Lovely',7.90),
  ('Mistery',9.75),
  ('Comedy',10.00),
  ('Fantastic',6.49)

  
INSERT INTO Movies(Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes)
VALUES
 ('Wanda',1,1999,200,4,5,22,2.45),
('Spiderman',2,2003,250,1,4,33,5.77),
('Visison',3,2015,199,2,2,44,6.78),
('Batman',4,2005,123,5,1,66,20.00),
('Superman',5,1998,144,3,3,77,18.76)