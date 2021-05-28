CREATE TABLE Users
(
	Email varchar(20) NOT NULL UNIQUE,
	Password varchar(MAX) NOT NULL,
	Level int,
	Code varchar(MAX),
	NumberCard varchar(16),

	CONSTRAINT PK_Users PRIMARY KEY (Email)
)



CREATE TABLE Profiles
(
	Id int NOT NULL IDENTITY(1,1),
	Email varchar(20),
	Name NVARCHAR(20),
	Avatar varchar(30),
	Status int,

	CONSTRAINT PK_Profiles PRIMARY KEY (Id)
)


CREATE TABLE Payment_History
(
	Id INT IDENTITY(1,1),
	Email varchar(20),
	DateOfPayment Date,
	Note Text,
	Price numeric(18,0)

	CONSTRAINT PK_PaymentHistory PRIMARY KEY (Id)
)

CREATE TABLE Levels
(
	Id INT NOT NULL IDENTITY(1,1),
	Name NVARCHAR(20),
	Price NUMERIC(18,0)

	CONSTRAINT PK_Level PRIMARY KEY (Id)
)

CREATE TABLE Media_Categories
(
	Id INT IDENTITY(1,1) NOT NULL,
	Name NVARCHAR(20)

	CONSTRAINT PK_MediaCategory PRIMARY KEY (Id)
)

CREATE TABLE Medias
(
	Id INT IDENTITY(1,1),
	Lvl INT,
	IdCategory INT

	CONSTRAINT PK_Media PRIMARY KEY (Id)
)

CREATE TABLE Movie_Categories
(
	Id INT NOT NULL IDENTITY(1,1),
	Name NVARCHAR(20)

	CONSTRAINT PK_MovieCategory PRIMARY KEY (Id)
)

CREATE TABLE Movies
(
	Id INT NOT NULL UNIQUE,
	IdCategory INT,
	Name NVARCHAR(20),
	Description NTEXT,
	IMDB Float,
	Poster VARCHAR(MAX),
	Likes INT,
	Age INT,
	NumberOfViews INT,
	Video VARCHAR(MAX),
	Season VARCHAR(10), --generate
	Time varchar(8),
	Directors NVARCHAR(20),
	Nation NVARCHAR(20),

	CONSTRAINT PK_Movie PRIMARY KEY (Id)
)

CREATE TABLE Albums
(
	Id INT NOT NULL UNIQUE,
	Name NVARCHAR(20),

	CONSTRAINT PK_Albums PRIMARY KEY (Id)
)

CREATE TABLE Album_Details
(
	Id INT IDENTITY(1,1),
	IdAlbum INT,
	Image VARCHAR(MAX),

	CONSTRAINT PK_AlbumDetail PRIMARY KEY(Id)
)

CREATE TABLE Audio_Categories
(
	Id INT IDENTITY(1,1),
	Name NVARCHAR(20),

	CONSTRAINT PK_AudioCategory PRIMARY KEY (Id)
)

CREATE TABLE Audios
(
	Id INT UNIQUE NOT NULL,
	IdCategory INT,
	Name NVARCHAR(20),
	Image VARCHAR(MAX),
	Mp3 VARCHAR(MAX),

	CONSTRAINT PK_Audios PRIMARY KEY (Id)
)


CREATE TABLE My_Lists
(
	Id INT IDENTITY(1,1),
	IdProfile INT,
	IdMedia INT,
	Date Date,

	CONSTRAINT PK_MyLists PRIMARY KEY (Id)
)

CREATE TABLE Likes
(
	Id INT IDENTITY(1,1),
	IdProfile INT,
	IdMedia INT,
	Date Date,

	CONSTRAINT PK_Likes PRIMARY KEY (Id)
)

CREATE TABLE View_History
(
	Id INT IDENTITY(1,1),
	IdProfile INT,
	IdMedia INT,
	Date Date,
	time VARCHAR(20)

	CONSTRAINT PK_Likes PRIMARY KEY (Id)
)

ALTER TABLE Profiles ADD CONSTRAINT FK_Profile_User FOREIGN KEY (Email) REFERENCES Users(Email)
ALTER TABLE Payment_History ADD CONSTRAINT FK_PaymentHistory_User FOREIGN KEY (Email) REFERENCES Users(Email)
ALTER TABLE Medias ADD CONSTRAINT FK_Medias_Levels FOREIGN KEY (Lvl) REFERENCES Levels(Id)
ALTER TABLE Medias ADD CONSTRAINT FK_Medias_MediaCategories FOREIGN KEY (IdCategory) REFERENCES Media_Categories(Id)
ALTER TABLE Movies ADD CONSTRAINT FK_Movies_MovieCategories FOREIGN KEY (IdCategory) REFERENCES Movie_Categories(Id)
ALTER TABLE Movies ADD CONSTRAINT FK_Movies_Medias FOREIGN KEY (Id) REFERENCES Medias(Id)
ALTER TABLE Albums ADD CONSTRAINT FK_Albums_Medias FOREIGN KEY (Id) REFERENCES Medias(Id)
ALTER TABLE Album_Details ADD CONSTRAINT FK_AlbumDetails_Albums FOREIGN KEY (IdAlbum) REFERENCES Albums(Id)
ALTER TABLE Audios ADD CONSTRAINT FK_Audios_Medias FOREIGN KEY (Id) REFERENCES Medias(Id)
ALTER TABLE Audios ADD CONSTRAINT FK_Audios_AudioCategories FOREIGN KEY (IdCategory) REFERENCES Audio_Categories(Id)
ALTER TABLE My_Lists ADD CONSTRAINT FK_MyLists_Medias FOREIGN KEY (IdMedia) REFERENCES Medias(Id)
ALTER TABLE My_Lists ADD CONSTRAINT FK_MyLists_Profiles FOREIGN KEY (IdProfile) REFERENCES Profiles(Id)
ALTER TABLE Likes ADD CONSTRAINT FK_Likes_Medias FOREIGN KEY (IdMedia) REFERENCES Medias(Id)
ALTER TABLE Likes ADD CONSTRAINT FK_Likes_Profiles FOREIGN KEY (IdProfile) REFERENCES Profiles(Id)
ALTER TABLE View_History ADD CONSTRAINT FK_ViewHistory_Medias FOREIGN KEY (IdMedia) REFERENCES Medias(Id)
ALTER TABLE View_History ADD CONSTRAINT FK_ViewHistory_Profiles FOREIGN KEY (IdProfile) REFERENCES Profiles(Id)

--SELECT
select * from Audios
select * from Audio_Categories
select * from Medias 
select * from Likes 
select * from My_Lists
select * from Movies

--Insert user 
INSERT INTO Users VALUES ('nghiadx2001@gmail.c', '123', 1, 'CDCD', '362')
INSERT INTO Users (Email, Password) VALUES ('1@gmail.c', '123')
--insert profile
INSERT INTO Profiles (Email, Name) VALUES ('nghiadx2001@gmail.c', N'Nguyễn H Nghĩa')

--MEDIA_CAT// chỉ có 3 cái thui, không thêm- sửa -xóa nữa
INSERT INTO Media_Categories (Name) VALUES (N'Hinh Ảnh'), (N'Phim'), (N'Âm Nhạc')

--MEDIA
INSERT INTO Medias (IdCategory) values (1), (1), (1), (1), (1), (1), (1), (1), (1), (1)

--INSERT AUDIO_Cat
INSERT INTO Audio_Categories(Name) values (N'Ballad'), (N'Sôi động'), (N'Trữ tình')

--INSERT AUDIO
INSERT INTO Audios VALUES 
	(1, 1, N'Chiều thu ', '1', null),
	(2, 1, N'Người đã ', '2', null),
	(3, 2, N'Sóng gió', '3', null),
	(4, 2, N'Bạc phận', '4', null),
	(5, 3, N'Nhỏ ơi', '5', null)
update Audios set Mp3 = 'a_mp3_1.mp3', Image = 'a_avatar_1.jpg' where Id = 1
update Audios set Mp3 = 'a_mp3_2.mp3', Image = 'a_avatar_2.jpg' where Id = 2
update Audios set Mp3 = 'a_mp3_3.mp3', Image = 'a_avatar_3.jpg' where Id = 3
update Audios set Mp3 = 'a_mp3_4.mp3', Image = 'a_avatar_4.jpg' where Id = 4
update Audios set Mp3 = 'a_mp3_5.mp3', Image = 'a_avatar_5.jpg' where Id = 5

--INSERT ALBUM_CAT
select * from Albums
INSERT INTO Albums values 
(8, N'Thiên nhiên'),
(9, N'Chiến tranh')

--INSERT ALBUM
INSERT INTO Album_Details (IdAlbum, Image) VALUES
(8, 'PT_TN_1.jpg'),
(8, 'PT_TN_2.jpg'),
(8, 'PT_TN_3.jpg'),
(8, 'PT_TN_4.jpg'),
(8, 'PT_TN_5.jpg')

--them media yeu thích và mylist
insert into Likes (IdProfile, IdMedia, Date) values 
(1, 2, '01-01-2021'),
(1, 5, '01-03-2021')

insert into My_Lists(IdProfile, IdMedia, Date) values 
(1, 1, '01-01-2021'),
(1, 2, '01-03-2021'),
(1, 7, '01-03-2021')


update Medias set Lvl = 1 where Id = 7
insert into Levels(Name,Price) values('Vàng', 200000)
insert into Movies (Id, IdCategory,IMDB,Likes,Name,NumberOfViews,Poster,Age,Description,Season,Time,Video)
values (7,1,6.5,4,N'Super Hero', 23, 'postermovieHuter.jpg', 18,N'Super hero hân hạnh tài trợ', 'kn92','00:12:30','video.mp4')
Insert into Users(Email,Password,NumberCard,Level)values ('user@gmail.com', 'G9BBbVflIID4fJFh4ljRqrOgtzA33ztt4q474rYmz8IhcOqD', '1253871235', 1) 
insert into profiles(Email,Name,Status) values ('user@gmail.com', '5 coder bị đơ', 1)