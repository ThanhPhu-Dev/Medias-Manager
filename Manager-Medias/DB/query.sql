CREATE TABLE Users
(
	Email varchar(20) NOT NULL UNIQUE,
	Password varchar(MAX) NOT NULL,
	Level int,
	Code varchar(MAX),
	NumberCard varchar(16),
	Exp varchar(8)

	CONSTRAINT PK_Users PRIMARY KEY (Email)
)

CREATE TABLE Profiles
(
	Id int NOT NULL IDENTITY(1,1),
	Email varchar(20),
	Name NVARCHAR(20),
	Avatar varchar(200),
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
	Image VARCHAR(MAX)

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
	Singer NVARCHAR(20),

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
select * from Media_Categories 
select * from Likes 
select * from My_Lists
select * from Movies
select * from Movie_Categories
select * from levels
select * from Album_Details
select * from Albums

--insert cấp độ
INSERT INTO Levels (Name, Price) values
(N'Cơ bản', 180000),
(N'Tiêu chuẩn', 220000),
(N'Cao cấp', 260000)
update Levels set Price = 180000 where Id = 1
update Levels set Price = 220000 where Id = 2
update Levels set Price = 260000 where Id = 3

--Insert user 
INSERT INTO Users VALUES ('nghiadx2001@gmail.c', '123', 1, 'CDCD', '362')
INSERT INTO Users (Email, Password) VALUES ('1@gmail.c', '123')
Insert into Users(Email,Password,NumberCard,Level)values ('user@gmail.com', 'G9BBbVflIID4fJFh4ljRqrOgtzA33ztt4q474rYmz8IhcOqD', '1253871235', 1) 

--insert profile
INSERT INTO Profiles (Email, Name) VALUES ('nghiadx2001@gmail.c', N'Nguyễn H Nghĩa')
insert into profiles(Email,Name,Status) values ('user@gmail.com', '5 coder bị đơ', 1)

--MEDIA_CAT// chỉ có 3 cái thui, không thêm- sửa -xóa nữa
INSERT INTO Media_Categories (Name) VALUES (N'Hinh Ảnh'), (N'Phim'), (N'Âm Nhạc')

--MEDIA
INSERT INTO Medias (IdCategory) values (1), (1), (1), (1), (1), (1), (1), (1), (1), (1)
update Medias set IdCategory = 2 where Id between 28 and 37 

--INSERT AUDIO_Cat
INSERT INTO Audio_Categories(Name) values (N'Ballad'), (N'Sôi động'), (N'Trữ tình')

--INSERT AUDIO
INSERT INTO Audios VALUES 
	(1, 1, N'Chiều thu ', '1', null),
	(2, 1, N'Người đã ', '2', null),
	(3, 2, N'Sóng gió', '3', null),
	(4, 2, N'Bạc phận', '4', null),
	(5, 3, N'Nhỏ ơi', '5', null)
update Audios set Singer='DATKAA X QT BEATZ' where Id = 1
update Audios set Singer=N'Doãn Hiếu' where Id = 2
update Audios set Singer='K-ICM x JACK' where Id = 3
update Audios set Singer='K-ICM x JACK' where Id = 4
update Audios set Singer=N'Chí Tài' where Id = 5

--INSERT ALBUM_CAT
select * from Albums
INSERT INTO Albums values 
(8, N'Thiên nhiên'),
(9, N'Chiến tranh')
update albums set Image = 'album_nature.jpg' where Id = 8
update albums set Image = 'album_chientranh.jpg' where Id = 9
--INSERT ALBUM
INSERT INTO Album_Details (IdAlbum, Image) VALUES
(8, 'album_PT_TN_1.jpg'),
(8, 'album_PT_TN_2.jpg'),
(8, 'album_PT_TN_3.jpg'),
(8, 'album_PT_TN_4.jpg'),
(8, 'album_PT_TN_5.jpg')

--them media yeu thích và mylist
insert into Likes (IdProfile, IdMedia, Date) values 
(1, 2, '01-01-2021'),
(1, 5, '01-03-2021')

insert into My_Lists(IdProfile, IdMedia, Date) values 
(1, 1, '01-01-2021'),
(1, 2, '01-03-2021'),
(1, 7, '01-03-2021')

--insert payment history
insert into payment_history(Email,DateOfPayment,Note,Price) values ('nghiadx2001@gmail.c', '2021-05-29', '',180000)
insert into payment_history(Email,DateOfPayment,Note,Price) values ('nghiadx2001@gmail.c', '2021-05-28', '',170000)
insert into payment_history(Email,DateOfPayment,Note,Price) values ('nghiadx2001@gmail.c', '2021-05-30', '',200000)

select * from users
select * from Movie_Categories
insert into Movie_Categories (Name) values 
--(N'Tình cảm'), 
(N'Phiêu lưu'), 
(N'Viễn tưởng'), 
(N'Kinh dị')
--insert phim 
insert into Movies (Id, IdCategory, IMDB, Likes, Name, NumberOfViews, Poster, Age)
			values (18, 1, 6.5, 4, N'Iron Man 1', 23, 'movieposter_action_ironman_1.jpg', 18),
				   (19, 1, 5, 0, N'Iron Man 1', 23, 'movieposter_action_ironman_2.jpg', 18),
				   (20, 1, 6, 0, N'Nhiệm Vụ Bất KT', 103, 'movieposter_action_NVBKT_5.jpg', 18),
				   (21, 1, 7, 0, N'Người Nhện ', 23, 'movieposter_action_SD.jpg', 18),
				   (22, 1, 4, 0, N'Quá Nhanh Quá NH', 1000, 'movieposter_action_ff_9.jpg', 18)
insert into Movies (Id, IdCategory, IMDB, Likes, Name, NumberOfViews, Poster, Age)
			values (23, 2, 6.5, 4, N'Your Name', 23, 'movieposter_action_ironman_1.jpg', 18),
				   (24, 2, 5, 0, N'Chúng ta', 23, 'movieposter_action_ironman_2.jpg', 18),
				   (25, 2, 6, 0, N'Người thầy y đức', 103, 'movieposter_action_NVBKT_5.jpg', 14),
				   (26, 2, 7, 0, N'Cửa hàng tiện lợi', 23, 'movieposter_action_SD.jpg', 15),
				   (27, 2, 4, 0, N'Hình dáng thanh âm', 1000, 'movieposter_action_ff_9.jpg', 10)
insert into Movies (Id, IdCategory, IMDB, Likes, Name, NumberOfViews, Poster, Age)
			values (28, 3, 6.5, 4, N'Điểm mù 2', 23, 'movieposter_action_ironman_1.jpg', 18),
				   (29, 3, 5, 0, N'Gozzila', 23, 'movieposter_action_ironman_2.jpg', 18),
				   (30, 3, 6, 0, N'Edens Zero', 103, 'movieposter_action_NVBKT_5.jpg', 14),
				   (31, 4, 6, 0, N'Đồi tuyết máu', 103, 'movieposter_action_NVBKT_5.jpg', 14),
				   (32, 4, 6.5, 4, N'Bạn trai tôi', 23, 'movieposter_action_ironman_1.jpg', 18),
				   (33, 4, 5, 0, N'Doctor Animal', 23, 'movieposter_action_ironman_2.jpg', 18),
				   (34, 4, 6, 0, N'Thảm họa toàn cầu', 103, 'movieposter_action_NVBKT_5.jpg', 14),
				   (35, 5, 6.5, 4, N'Lời ru tử thần', 23, 'movieposter_action_SD.jpg', 18),
				   (36, 5, 5, 0, N'Nuốt chửng', 23, 'movieposter_action_ironman_2.jpg', 18),
				   (37, 5, 6, 0, N'Bùa chú', 103, 'movieposter_action_ff_9.jpg', 14)

insert into Movies (Id, IdCategory,IMDB,Likes,Name,NumberOfViews,Poster,Age,Description,Season,Time,Video)
values (7,1,6.5,4,N'Super Hero', 23, 'movieposter_postermovieHuter.jpg', 18,N'Super hero hân hạnh tài trợ', 'kn92','00:12:30','movievideo_video.mp4')

