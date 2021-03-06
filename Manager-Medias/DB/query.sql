CREATE TABLE Users
(
	Email varchar(200) NOT NULL UNIQUE,
	Password varchar(MAX) NOT NULL,
	Level int,
	Code varchar(MAX),
	NumberCard varchar(16),
	Exp varchar(8),
	roleId INT,
	CreateAt Date

	CONSTRAINT PK_Users PRIMARY KEY (Email)
)

CREATE TABLE Roles
(
	Id int IDENTITY,
	Name NVARCHAR(20),

	CONSTRAINT PK_Roles PRIMARY KEY (Id)
)

CREATE TABLE Profiles
(
	Id int NOT NULL IDENTITY(1,1),
	Email varchar(200),
	Name NVARCHAR(20),
	Avatar varchar(200),
	Status int,

	CONSTRAINT PK_Profiles PRIMARY KEY (Id)
)

CREATE TABLE Payment_History
(
	Id INT IDENTITY(1,1),
	Email varchar(200),
	DateOfPayment Date,
	Note Nvarchar(MAX),
	Price numeric(18,0)

	CONSTRAINT PK_PaymentHistory PRIMARY KEY (Id)
)
CREATE TABLE Levels
(
	Id INT NOT NULL IDENTITY(1,1),
	Name NVARCHAR(20),
	Price NUMERIC(18,0),
	Quality NVARCHAR(20),
	Resolution NVARCHAR(20)

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

CREATE TABLE Movie_classifies
(
	Id INT NOT NULL IDENTITY(1,1),
	Name NVARCHAR(20)

	CONSTRAINT PK_MovieClassifies PRIMARY KEY (Id)
)

CREATE TABLE Movies
(
	Id INT NOT NULL UNIQUE,
	IdCategory INT,
	IdClassifiles INT,
	Name NVARCHAR(255),
	Description NTEXT,
	IMDB Float,
	Poster VARCHAR(MAX),
	Likes INT,
	Age INT,
	NumberOfViews INT,
	Video VARCHAR(MAX),
	Season VARCHAR(10), --generate
	Time varchar(8),
	Directors NVARCHAR(100),
	Nation NVARCHAR(20),
	CreateAt Date,

	CONSTRAINT PK_Movie PRIMARY KEY (Id)
)


CREATE TABLE Albums
(
	Id INT NOT NULL UNIQUE,
	Name NVARCHAR(200),
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
	Name NVARCHAR(200),

	CONSTRAINT PK_AudioCategory PRIMARY KEY (Id)
)




CREATE TABLE Audios
(
	Id INT UNIQUE NOT NULL,
	IdCategory INT,
	Name NVARCHAR(200),
	Image VARCHAR(MAX),
	Mp3 VARCHAR(MAX),
	Singer NVARCHAR(200),

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

ALTER TABLE Users ADD CONSTRAINT FK_Users_Levels FOREIGN KEY (Level) REFERENCES Levels(Id)
ALTER TABLE Users ADD CONSTRAINT FK_Users_Roles FOREIGN KEY (roleId) REFERENCES Roles(Id)
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
ALTER TABLE Movies add CONSTRAINT fk_movies_MovieClassifies FOREIGN KEY (IdClassifiles) REFERENCES Movie_classifies(Id)

--SELECT
Select * from Roles
select * from Media_categories
select * from Audios
select * from Audio_Categories
select * from Movie_classifies
select * from Likes 
select * from My_Lists
select * from levels
select * from Media_Categories 
select * from Medias 
select * from Movies mo join Medias m on mo.Id = m.Id
select * from Movie_Categories
select * from Album_Details
select * from Albums

Select * from Profiles
select * from View_History
select * from Movies
select * from Users 
select * from Profiles

update Profiles set Email = 'user@gmail.com' where id = 17
select * from Payment_History
delete from Payment_History where Id =9
update Payment_History set DateOfPayment ='2020-12-05' where Id = 4

update Profiles set Name = N'Nguyễn Văn A'
insert into Users values ('user2019@gmail.com','G9BBbVflIID4fJFh4ljRqrOgtzA33ztt4q474rYmz8IhcOqD', 2, null, '6911057352415364', '01/22', 1, '2021-07-05')
insert into Profiles values ( 'user2019@gmail.com', N'Võ Minh', 'pr1.jpg', 1)
insert into Profiles values ( 'user1@gmail.com', N'Võ Minh', 'pr1.jpg', 1)
insert into Profiles values ( 'user2019@gmail.com', N'Võ Minh Văn', 'pr3.jpg', 1)

insert into Users values ('vovanthong1@gmail.com','G9BBbVflIID4fJFh4ljRqrOgtzA33ztt4q474rYmz8IhcOqD', 3, null, '6911057352410011', '05/22', 1, '2021-06-05')
insert into Profiles values ( 'vovanthong1@gmail.com', N'Lê Thị C', 'pr2.jpg', 1)
insert into Profiles values ( 'vovanthong1@gmail.com', N'Trần B', 'default_avatar.png', 1)
insert into Profiles values ( 'vovanthong1@gmail.com', N'Trần Minh M', 'default_avatar.png', 1)
insert into Profiles values ( 'vovanthong1@gmail.com', N'Nguyễn T', 'pr5.png', 1)
select * from Payment_History
update Payment_History set Price = 180000
update Payment_History set Note = N'Thanh toán cấp độ Cơ bản' where Note is null
insert into Payment_History values ('vovanthong1@gmail.com', '2021-07-11', N'Thanh toán cấp độ Cơ bản', 180000)
insert into Payment_History values ('user1@gmail.com', '2021-07-02', N'Thanh toán cấp độ Cơ bản', 180000)
insert into Payment_History values ('user2019@gmail.com', '2021-05-01', N'Thanh toán cấp độ Cơ bản', 180000)
insert into Payment_History values ('user2019@gmail.com', '2020-06-05', N'Thanh toán cấp độ Tiêu chuẩn', 220000)
insert into Payment_History values ('vovanthong1@gmail.com', '2021-06-09', N'Thanh toán cấp độ Cao cấp', 260000)


delete Movies where id between 32 and 37
update Albums set Id = 2 where Name=N'Thiên nhiên'
update Album_Details set IdAlbum = 2
update Users set Password = 'G9BBbVflIID4fJFh4ljRqrOgtzA33ztt4q474rYmz8IhcOqD' where Email = 'vovanminh@gmail.com'
update Movie_Categories set Name=N'Tình cảm, lãng mạn' where Id=5

update users set CreateAt = '2021-06-29'
update users set CreateAt = '2021-05-29' where Email = 'vovanminh@gmail.com'
update users set CreateAt = '2021-05-29' where Email = 'nghiadx2001@gmail.c'
update users set CreateAt = '2021-05-29', Level = 3 where Email = 'eric2019@gmail.com'

update Movies set CreateAt = '2021-07-05' where Id % 3 = 0
update Movies set CreateAt = '2021-06-05' where Id % 3 = 1
update Movies set CreateAt = '2021-08-05' where Id % 3 = 2

insert into View_History  values (1, 20, '2021-05-03', 21)



update Movies set Season = 1 
--insert roles
insert into Roles(Name) values ('User'),('Admin')
--insert cấp độ
INSERT INTO Levels (Name, Price) values
(N'Cơ bản', 180000),
(N'Tiêu chuẩn', 220000),
(N'Cao cấp', 260000)
update Levels set Quality = N'Trung', Resolution= N'480p' where Id = 1
update Levels set Quality = N'Tốt' , Resolution= N'1080p'where Id = 2
update Levels set Quality = N'Cao Cấp', Resolution= N'4K+HDR' where Id = 3

update Medias set Lvl = 2 where Id between 1 and 2
update Medias set Lvl = 1 where Id between 3 and 5
update Medias set Lvl = 2 where Id between 6 and 8
update Medias set Lvl = 1 where Id between 9 and 13
update Medias set Lvl = 3 where Id between 14 and 18
update Medias set Lvl = 3 where Id between 19 and 22
update Medias set Lvl = 1 where Id between 23 and 25
update Medias set Lvl = 3 where Id between 26 and 30
update Medias set Lvl = 1 where Id between 31 and 34
update Medias set Lvl = 1 where Id between 35 and 37

update Medias set IdCategory = 3 where Id between 1 and 5

--update lv movie
--category 2
update Medias set Lvl = 1 where Id = 7
update Medias set Lvl = 1 where Id = 29
update Medias set Lvl = 2 where Id = 18
update Medias set Lvl = 2 where Id = 20
update Medias set Lvl = 3 where Id = 19
--category 1
update Medias set Lvl = 1 where Id = 21
update Medias set Lvl = 1 where Id = 24
update Medias set Lvl = 1 where Id = 30
update Medias set Lvl = 2 where Id = 23
update Medias set Lvl = 3 where Id = 22
--category 3
update Medias set Lvl = 1 where Id = 27
update Medias set Lvl = 1 where Id = 26
update Medias set Lvl = 2 where Id = 25
update Medias set Lvl = 2 where Id = 31
update Medias set Lvl = 3 where Id = 28
--Insert user 
INSERT INTO Users VALUES ('thongars22222@gmail.com', '123abc', 1, 'CDCD', '7352222288839364', '10/20', 2)
INSERT INTO Users (Email, Password) VALUES ('1@gmail.c', '123')
Insert into Users(Email,Password,NumberCard,Level)values ('user@gmail.com', 'G9BBbVflIID4fJFh4ljRqrOgtzA33ztt4q474rYmz8IhcOqD', '1253871235', 1) 

Insert into Users(Email,Password,NumberCard,Level,Code)values ('ali125@gmail.com', '123matkhau123', '6911000003591177', 1, 'QAZXC1'),
('beatriz211@gmail.com', '123matkhau123', '6911000003911077', 2, 'QYYXC1'), ('12charles@gmail.com', '123matkhau123', '6911000003599911', 1, 'QPPXC1'), 
('diya0001@gmail.com', '123matkhau123', '6911000051391177', 3, 'QIIQXC1'), ('eric2019@gmail.com', '123matkhau123', '6911852613591177', 1, '123ZXC1'),
('gabriel@gmail.com', '123matkhau123', '6946810003591177', 1, 'Q84331'), ('avxHhnna@gmail.com', '123matkhau123', '9622200003591177', 2, 'Q45633') 

insert into profiles(Email,Name,Status) values ('ali125@gmail.com', 'Alice', 1), ('ali125@gmail.com', 'Alice', 1), ('beatriz211@gmail.com', 'beatriz', 1), ('12charles@gmail.com', 'Chattlotte', 1),
('diya0001@gmail.com', 'diana 123', 1), ('eric2019@gmail.com', 'Eric gaxia', 1), ('ali125@gmail.com', 'Alice', 1), ('gabriel@gmail.com', 'Gabriel', 1), ('avxHhnna@gmail.com', 'Hanana',0)

select * from users
delete from Profiles where Email != 'user@gmail.com' and Email != 'vovanminh@gmail.com'
delete from Users where Email != 'user@gmail.com' and Email != 'vovanminh@gmail.com'

---
insert into Payment_History (Email, DateOfPayment, Price) values ('ali125@gmail.com', '2021-06-25', 180000)
insert into Payment_History (Email, DateOfPayment, Price) values ('avxHhnna@gmail.com', '2021-06-25', 180000)
insert into Payment_History (Email, DateOfPayment, Price) values ('ali125@gmail.com', '2021-06-25', 200000)
insert into Payment_History (Email, DateOfPayment, Price) values ('eric2019@gmail.com', '2021-06-25', 180000)
insert into Payment_History (Email, DateOfPayment, Price) values ('beatriz211@gmail.com', '2021-05-25', 180000)
--insert profile

INSERT INTO Profiles (Email, Name) VALUES ('nghiadx2001@gmail.c', N'Nguyễn H Nghĩa')
insert into profiles(Email,Name,Status) values ('user@gmail.com', '5 coder bị đơ', 1)
insert into profiles(Email,Name,Status) values ('nghiadx22222@gmail.com', 'Thêm được không', 1)
--MEDIA_CAT// chỉ có 3 cái thui, không thêm- sửa -xóa nữa
INSERT INTO Media_Categories (Name) VALUES (N'Hinh Ảnh'), (N'Phim'), (N'Âm Nhạc')

--MEDIA
INSERT INTO Medias (IdCategory) values (2),(2),(2),(2),(2),(2),(2),(2),(2),(2),
(2),(2),(2),(2),(2),(2),(2),(2),(2),(2),(2),(2),(2),(2),(2)
update Medias set Lvl = 1 where Id between 52 and 70
update Medias set Lvl = 2 where Id between 71 and 76
INSERT INTO Medias (IdCategory, Lvl) values (2, 3),(2, 3),(2, 3)
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

insert into Likes (IdProfile, IdMedia, Date) values 
(1, 20, '01-01-2021')

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
select top 10 Name, NumberOfViews from Movies Order by NumberOfViews DeSC
select * from Movie_Categories
update Movie_Categories set Name=N'Cổ Trang' where Id=2
update Movie_Categories set Name=N'Siêu Anh Hùng' where Id=3
update movies set Age=16, Video='movievideo_LinhLungLangTam.mp4', Time='00:01:19' where Id = 7
update movies set IdCategory=2 where Id = 7
--phim cổ trang
update Movies set IdCategory=2, Name=N'Nhạn Quy Tây Song Nguyệt',
				  Description=N'Nhạn Quy Tây Song Nguyệt kể về mối tình "ghét trước yêu sau" vô cùng thú vị của quận vương Triệu Hiếu Khiêm và cô nàng Tạ Tiểu Mãn. Trong một lần dạo chơi, Tạ Tiểu Mãn vô tình đắc tội với quận vương bá đạo Triệu Hiếu Khiêm. Hơn nữa, cô còn bị người khác hãm hại tính kế bắt cô ký khế ước hôn nhân với Triệu Hiếu Khiêm. Oan gia ngõ hẹp, 2 con người luôn đối đầu nhau nay bị ràng buộc bởi một tờ khế ước, họ dần dần hiểu nhau và dành tình cảm cho nhau. Cứ thế Triệu Hiếu Khiêm và Tạ Tiểu Mãn dắt tay nhau qua bao thăng trầm, sóng gió của cuộc sống ...',
				  IMDB = 8, Poster='movieposter_NhatQuyTaySongNguyet.jpg', Likes = 16,
				  Age=16, NumberOfViews=106, Video='movievideo_NhanQuyTaySongNguyen.mp4',
				  Season='asce', Time='00:01:58', Directors= N'Tôn Khải Khải',
				  Nation = N'Trung Quốc' Where Id = 18
update Movies set IdCategory=2, Name=N'Đại Đường Nữ Nhi Hành',
				  Description=N'Đại Đường Nữ Nhi Hành lấy bối cảnh thời kỳ nhà Đường, giai đoạn mà hoàng đế Lý Thế Dân đang trị vì. Câu chuyện sẽ kể về mối tình khắc cốt ghi tâm của nữ quan Phó Nhu (Lý Nhất Đồng) và vị tướng quân Trình Xử Mặc (Hứa Khải). Phó Nhu là một tiểu thư khuê các, am hiểu cầm kỳ thi họa, thêu thùa rất giỏi; khi tiến cung nàng đã gặp gỡ và kết duyên với vị tướng quân anh dũng Trình Xử Mặc.',
				  IMDB = 9, Poster='movieposter_DaiDuongNuNhiHanh.jpg', Likes = 50,
				  Age=16, NumberOfViews=656, Video='movievideo_DaiDuongNuNhiHanh.mp4',
				  Season='asce', Time='00:03:23', Directors= N'Lý Tuệ Châu',
				  Nation = N'Trung Quốc' Where Id = 19
update Movies set IdCategory=2, Name=N'Nhà Ảo Thuật Thời Joseon',
				  Description=N'Công chúa Cheong Myung (do Go Ah Ra thủ vai) của Joseon Dynasty trên đường đi tới Qing Dynasty để kết hôn với người được lựa chọn phải kết hôn với mình. Vô tình, trên đường đi, nàng đã gặp một pháp sư trẻ tuổi Hwan Hee (do Yoo Seung Ho thủ vai) và rồi phải lòng chàng trai này.',
				  IMDB = 7, Poster='movieposter_NhaAoThuatThoiJoseon.jpg', Likes = 20,
				  Age=16, NumberOfViews=356, Video='movievideo_NhaAoThuatThoiJoseon.mp4',
				  Season='asdxv', Time='00:00:42', Directors= N'Dae-seung Kim',
				  Nation = N'Hàn Quốc' Where Id = 20
update Movies set IdCategory=2, Name=N'Thanh Kiếm Vô Danh',
				  Description=N'Bước vào tuổi con gái đẹp tựa hương hoa, nàng Min Ja Young (Soo Ae) khi đó vẫn chưa trở thành hoàng hậu Myeong Seong nổi tiếng có công lao lãnh đạo nhân dân Triều Tiên chống lại Nhật Bản, cuộc gặp gỡ tình cờ đã đưa nàng và chàng kiếm khách tên Vô Danh (Jo Seung Woo) cùng cảm mến nhau ngay từ cái nhìn đầu tiên. Nhưng rồi sự khác biệt về thân phận trở thành rào cản mà cả hai người không thể vượt qua được, Ja Young kiên định bước theo con đường mà số phận đã an bài, trở thành Hoàng hậu Myeong Seong. Vì muốn được gặp Ja Young và bảo vệ nàng, Vô Danh tình nguyện từ bỏ cuộc sống tiêu dao tự tại của chàng kiếm sĩ giang hồ để gia nhập hoàng cung, trở thành một thị vệ chốn cung cấm. Để giúp cho Ja Young có thể sống cuộc sống an toàn trong thâm cung loạn lạc, Vô Danh cứ thế âm thầm ở bên cạnh bảo vệ và che chở nàng, dù có phải hy sinh tính mạng mình.',
				  IMDB = 5.5, Poster='movieposter_ThanhKiemVoDanh.jpg', Likes = 80,
				  Age=16, NumberOfViews=356, Video='movievideo_ThanhKiemVoDanh.mp4',
				  Season='asdbvv', Time='00:03:15', Directors= N'Yong-gyun Kim',
				  Nation = N'Hàn Quốc' Where Id = 29
--phim Hành Động.
update Movies set IdCategory=1, Name=N'Vùng Chiến Sự Hiểm Nguy',
				  Description=N'Trong tương lai gần, một phi công máy bay không người lái được cử đến một vùng chiến sự thấy mình được ghép nối với một sĩ quan Android tối mật trong nhiệm vụ ngăn chặn một cuộc tấn công hạt nhân.',
				  IMDB = 6, Poster='movieposter_VungChienSuHiemNguy.jpg', Likes = 50,
				  Age=18, NumberOfViews=556, Video='movievideo_VungChienSuHiemNguy.mp4',
				  Season='abasxv', Time='00:01:05', Directors= N'Mikael Håfström',
				  Nation = N'Mỹ' Where Id = 21
update Movies set IdCategory=1, Name=N'Thẩm Mỹ Viện Chết Chóc',
				  Description=N'Micheal và Alison là một cặp đôi cùng với mẹ của Alison đi tới bệnh viện phẫu thuật thẩm mĩ để giúp cho cô phẫu thuật giảm kích cỡ ngực của mình. Không ngờ rằng nơi đây lại là lũ xác sống trỗi dậy và sự việc bắt đầu nảy nở khiến cho họ phải đấu tranh cho sự sống của mình.',
				  IMDB = 8.5, Poster='movieposter_ThamMyVienChetChoc.jpg', Likes = 200,
				  Age=18, NumberOfViews=556, Video='movievideo_ThamMyVienChetChoc.mp4',
				  Season='abasxv', Time='00:02:28', Directors= N'Lars Damoiseaux',
				  Nation = N'Đang Cập Nhật' Where Id = 22
update Movies set IdCategory=1, Name=N'Đêm Sống Còn',
				  Description=N'Survive the Night (2020): Những tên cướp đang bị thương và chạy trốn khỏi sự săn lùng của cảnh sát. Để tránh bị bắt, chúng không vào bệnh viện mà chọn cách theo chân một bác sĩ về nhà riêng để ép buộc bác sĩ đó chữa trị cho chúng. Sau khi đột nhập vào nhà, chúng đã bắt trói và uy hiếp cả gia đình.',
				  IMDB = 7.5, Poster='movieposter_DemSongCon.jpg', Likes = 127,
				  Age=18, NumberOfViews=856, Video='movievideo_DemSongCon.mp4',
				  Season='abagfv', Time='00:02:09', Directors= N'Matt Eskandari',
				  Nation = N'Đang Cập Nhật' Where Id = 23
update Movies set IdCategory=1, Name=N'Underworld (2003)',
				  Description=N'Là bộ phim hành động kinh dị về bí mật giữa 2 dòng họ Vampires (ma cà rồng) và Lycans (người sói). Câu chuyện chủ yếu xoay quanh Selene (Kate Beckinsale), một Vampire được gọi là "Death Dealer", hay gọi là thợ săn Lycans. Cô ấy bị quyến rũ bởi Michael Corvin (Scott Speedman) - người bị bọn Lycans săn đuổi. Micheal bị Lycans cắn và anh sẽ biến trở thành Lycan, Selence không cho điều đó xảy ra.',
				  IMDB = 5.5, Poster='movieposter_Underworld2003.jpg', Likes = 67,
				  Age=18, NumberOfViews=256, Video='movievideo_Underworld2003.mp4',
				  Season='cagfv', Time='00:02:18', Directors= N'Len Wiseman',
				  Nation = N'Mỹ' Where Id = 30
update Movies set IdCategory=1, Name=N'Kẻ Vô Danh',
				  Description=N'Đôi khi người đàn ông mà bạn không để ý lại là người nguy hiểm nhất. Hutch Mansell, một người cha và người chồng bị đánh giá thấp và bị coi thường, luôn coi thường sự phẫn nộ của cuộc đời và không bao giờ lùi bước. Một kẻ vô danh.',
				  IMDB = 8.5, Poster='movieposter_KeVoDanh.jpg', Likes = 427,
				  Age=18, NumberOfViews=856, Video='movievideo_KeVoDanh.mp4',
				  Season='caaxgfv', Time='00:02:52', Directors= N'Ilya Naishuller',
				  Nation = N'Mỹ' Where Id = 24
--Phim Sieu anh hung.
update Movies set IdCategory=3, Name=N'Liên Minh Công Lý (bản của Zack Snyder)',
				  Description=N'Để cái chết của Siêu Nhân không phí hoài, Người Dơi cùng Nữ thần Chiến binh lên kế hoạch chiêu mộ một nhóm người cường hóa để bảo vệ thế giới khỏi mối đe dọa khủng khiếp sắp tới.',
				  IMDB = 8.5, Poster='movieposter_LienMinhCongLy.jpg', Likes = 147,
				  Age=16, NumberOfViews=856, Video='movievideo_LienMinhCongLy.mp4',
				  Season='cagacfv', Time='00:02:34', Directors= N'Zack Snyder',
				  Nation = N'Mỹ' Where Id = 25
update Movies set IdCategory=3, Name=N'Siêu Anh Hùng Shazam',
				  Description=N'Chỉ nhờ câu thần chú "SHAZAM!", cậu bé 14 tuổi - Billy Batson, có thể hóa thân thành siêu anh hùng cao lớn với sức mạnh vĩ đại của một vị thần nhưng tập hợp quyền năng tối thượng của 6 vị thần bao gồm trí tuệ của vua Solomon, sức mạnh của anh hùng Hercules, sức bền của người khổng lồ Atlas, phép thuật của thần Zeus, lòng can đảm của anh hùng Achilles và tốc độ của thần Mercury, và bản thân cái tên Shazam cũng là tập hợp chữ cái đầu tiên của tên 6 huyền thoại đó.',
				  IMDB = 6.5, Poster='movieposter_SieuAnhHungShazam.jpg', Likes = 147,
				  Age=16, NumberOfViews=686, Video='movievideo_SieuAnhHungShazam.mp4',
				  Season='cmncfv', Time='00:02:04', Directors= N'David F. Sandberg',
				  Nation = N'Mỹ' Where Id = 26
update Movies set IdCategory=3, Name=N'Cái Chết Của Siêu Nhân',
				  Description=N'Cái Chết Của Siêu NhânMột sinh vật to lớn, hung dữ tên Doomsday đến Trái đất, đe dọa phá hủy thế giới. Đội Liên Minh Công Lý đến ngăn chặn nhưng các siêu anh hùng như Wonder Woman, Green Lantern, Batman... đều bị đánh bại. Chỉ có Superman (Jerry O Connell lồng tiếng) đủ khỏe để chống trả con quái vật hùng mạnh. Anh và Doomsday vừa chiến đấu vừa di chuyển khắp nước Mỹ.',
				  IMDB = 6.5, Poster='movieposter_CaiChetCuaSieuNhan.jpg', Likes = 247,
				  Age=16, NumberOfViews=986, Video='movievideo_CaiChetCuaSieuNhan.mp4',
				  Season='cmncfv', Time='00:01:40', Directors= N'Jake Castorena, Sam Liu',
				  Nation = N'Mỹ' Where Id = 27
update Movies set IdCategory=3, Name=N'Đại Úy Mỹ: Siêu Anh Hùng Đầu Tiên',
				  Description=N'Năm 1942, Steve Rogers (Chris Evans) bị cho là không đủ sức khỏe để gia nhập quân đội Mỹ và chiến đấu chống lại Đức Quốc Xã trong Thế chiến II. Anh tình nguyện tham gia vào một dự án mang tên “Tái sinh”, một hoạt động quân sự bí mật. Anh được tiêm vào người một chất để biến đổi anh thành một siêu chiến binh gọi là Captain America. Ban đầu, Captain America - Siêu Anh Hùng Đầu Tiên của hãng Marvel được tính quay ở Los Angeles, nhưng sau đó nhà sản xuất đã chuyển đến London, nơi là một phần trong bối cảnh của truyện. Điều đó khiến nhà sản xuất có lợi về kinh tế vì ở đó có chính sách khuyến khích thuế. Nhưng quyết định đó đồng thời cũng là một đòn giáng mạnh vào cộng đồng nghèo khó của Los Angeles bởi họ đã tin tưởng dự án này sẽ thuê hàng trăm nhân viên đoàn phim tại thời điểm khi chỉ có rất ít phim kinh phí lớn quay tại địa phương, do cạnh tranh tăng cao với các bang và các nước khác.',
				  IMDB = 9.5, Poster='movieposter_DaiuyMy-SieuAnhHungDauTien.jpg', Likes = 547,
				  Age=16, NumberOfViews=986, Video='movievideo_DaiuyMy-SieuAnhHungDauTien.mp4',
				  Season='cmncfv', Time='00:02:21', Directors= N'Joe Johnston',
				  Nation = N'Mỹ' Where Id = 28
update Movies set IdCategory=3, Name=N'Phù Thủy Tối Thượng',
				  Description=N'Doctor Strange kể về Stephen Strange - một trong những bác sĩ phẫu thuật tài năng nhất trên thế giới. Tuy nhiên, một tai nạn ô tô xảy ra khiến đôi tay của Strange vỡ nát và trở nên vô dụng. Dồn hết tài năng và kinh nghiệm của mình để hàn gắn cơ thể nhưng không thành công, vị bác sĩ tài ba trở nên thất vọng và chán nản. Anh nghĩ anh đã mất tất cả cho đến khi được một thiền sư bí ẩn truyền niềm tin & chữa lành vết thương cho anh ở Tibet. Nhưng trước khi lành vết thương, Strange phải dùng những khả năng đặc biệt mới để cứu lấy cả cõi giới, và học cách chấp nhận những điều tưởng chừng không thể chấp nhận được...',
				  IMDB = 8.5, Poster='movieposter_PhuThuyToiThuong.jpg', Likes = 547,
				  Age=16, NumberOfViews=986, Video='movievideo_PhuThuyToiThuong.mp4',
				  Season='cmncfvx', Time='00:02:23', Directors= N'Scott Derrickson',
				  Nation = N'Mỹ' Where Id = 31

update Movies set IdClassifiles = 1 where id between 18 and 23
update Movies set IdClassifiles = 2 where id between 24 and 26
update Movies set IdClassifiles = 3 where id between 27 and 37
-- Phim viễn Tưởng ( Bắt đầu Id = 32 -> 37 là dùng update vì có data test trong db r)

--BAI HÁT
select * from Media_categories
select * from Medias 
select * from Audios
select * from Audio_Categories
--update level
update Medias set Lvl = 3 where id = 38 and id = 47 and id = 44
update Medias set Lvl = 1 where id between 39 and 43
update Medias set Lvl = 2 where id between 45 and 46
update Audio_Categories set Name = 'KPOP' where Id = 3
update Medias set IdCategory = 3 where Id between 38 and 47
-- 1. BALLAD
update Audios set
	Name = N'Chiều thu họa bóng nàng',
	Image = 'audioposter_avatar_CTHBN.jpg',
	Mp3 = 'audiomp3_mp3_CTHBN.mp3' 
	where Id = 1
update Audios set
	Name = N'Họ đã yêu ai mất rồi',
	Image = 'audioposter_avatar_HDYAMR.jpg',
	Mp3 = 'audiomp3_mp3_HDYAMR.mp3' 
	where Id = 2
update Audios set
	Name = N'Hẹn yêu',
	Image = 'audioposter_avatar_HY.jpg',
	Mp3 = 'audiomp3_mp3_HY.mp3',
	IdCategory = 1, 
	Time = '00:00:36',
	Singer = N'Duy Zuno'
	where Id = 3
update Audios set
	Name = N'Chỉ là không cùng nhau',
	Image = 'audioposter_avatar_CLKCN.jpg',
	Mp3 = 'audiomp3_mp3_CLKCN.mp3',
	IdCategory = 1, 
	Time = '00:00:33',
	Singer = N'TĂNG PHÚC ft TRƯƠNG THẢO NHI'
	where Id = 4
update Audios set
	Name = N'Đêm lao xao',
	Image = 'audioposter_avatar_DLX.jpg',
	Mp3 = 'audiomp3_mp3_DLX.mp3',
	IdCategory = 1, 
	Time = '00:00:33',
	Singer = N'Hòa Minzy x Anh Tú'
	where Id = 5

-- 2. SÔI ĐỘNG (mediaId 38 - 42)
insert into Audios values
			(38, 2, N'Sóng gió', 'audioposter_avatar_SG.jpg', 'audiomp3_mp3_SG.mp3', '00:00:43', 'JACK x K-ICM'),
			(39, 2, N'Bạc phận', 'audioposter_avatar_BP.jpg', 'audiomp3_mp3_BP.mp3', '00:00:32', 'JACK x K-ICM'),
			(40, 2, N'Muộn rồi mà sao còn', 'audioposter_avatar_MRMSC.jpg', 'audiomp3_mp3_MRMSC.mp3', '00:00:30', N'Sơn Tùng M-TP'),
			(41, 2, N'LayLaLay', 'audioposter_avatar_LLL.jpg', 'audiomp3_mp3_LLL.mp3', '00:00:45', N'JACK'),
			(42, 2, N'Dân chơi xóm', 'audioposter_avatar_DCX.jpg', 'audiomp3_mp3_DCX.mp3', '00:00:29', N'RPT MCK, JustaTee'),
-- 3. KPOP (mediaID 43 - 47)
			(43, 3, N'DDU-DU DDU-DU', 'audioposter_avatar_DUDUDU.jpg', 'audiomp3_mp3_DUDUDU.mp3', '00:00:15', N'BLACKPINK'),
			(44, 3, N'Trap', 'audioposter_avatar_Trap.jpg', 'audiomp3_mp3_Trap.mp3', '00:00:34', N'Henry Lau'),
			(45, 3, N'ToNight', 'audioposter_avatar_ToNight.jpg', 'audiomp3_mp3_ToNight.mp3', '00:00:44', N'BTS'),
			(46, 3, N'Bang Bang Bang', 'audioposter_avatar_BBB.jpg', 'audiomp3_mp3_BBB.mp3', '00:00:34', N'BIG BANG'),
			(47, 3, N'Fire', 'audioposter_avatar_Fire.jpg', 'audiomp3_mp3_Fire.mp3', '00:00:49', N'BTS')

--PHÂN LOẠI
insert into Movie_classifies (Name) values (N'Trẻ em'), ('13+'), (N'16+')

--HÌNH ẢNH
--CHIẾN TRANH
INSERT INTO ALBUM_DETAILS VALUES (9, 'album_VietnamWar-01.jpg'), 
(9, 'album_VietnamWar-02.jpg'), (9, 'album_VietnamWar-04.jpg'), 
(9, 'album_VietnamWar-05.jpg'), (9, 'album_VietnamWar-06.jpg'), 
(9, 'album_VietnamWar-08.jpg'), (9, 'album_VietnamWar-10.jpg')

INSERT INTO MEDIAS VALUES (1, 1),(1, 2),(1, 3),(1, 1)

INSERT INTO ALBUMS VALUES (48, N'Fun In The Sun', 'album_4b826a1d19_b.jpg')
INSERT INTO ALBUM_DETAILS VALUES (48, 'album_ac007f554c_b.jpg'), 
(48, 'album_8341a509cf_b.jpg'), (48, 'album_50972179388_9292fe8874_c.jpg'), 
(48, 'album_c6e6360239_c.jpg'), (48, 'album_c04fb7ecf9_k.jpg'), 
(48, 'album_48981b89be_b.jpg')
INSERT INTO ALBUM_DETAILS VALUES (48, 'album_4b826a1d19_b.jpg')

INSERT INTO ALBUMS VALUES (49, N'Sports', 'album_361580616e_k.jpg')
INSERT INTO ALBUM_DETAILS VALUES (49, 'album_50804109577_745d147545_k.jpg'), 
(49, 'album_3e753f6858_b.jpg'), (49, 'album_50840611392_f504faa228_b.jpg'), 
(49, 'album_da28e79bcb_k.jpg'), (49, 'album_50868057872_245c53956e_k.jpg'), 
(49, 'album_50832289746_8030dbc474_c.jpg')
INSERT INTO ALBUM_DETAILS VALUES (49, 'album_361580616e_k.jpg')


INSERT INTO ALBUMS VALUES (50, N'Street Photography', 'album_4525f25bab_b.jpg')
INSERT INTO ALBUM_DETAILS VALUES (50, 'album_50747940021_030d3388f4_c.jpg'), 
(50, 'album_d02d5bf4a5_b.jpg'), (50, 'album_50553974526_f90ff1ff81_k.jpg'), 
(50, 'album_2bda543d53_b.jpg'), (50, 'album_73601e37e2_b.jpg')
INSERT INTO ALBUM_DETAILS VALUES (50, 'album_4525f25bab_b.jpg')

INSERT INTO ALBUMS VALUES (51, N'Girls', 'album_4ce37f19e23ee5b7e1555f3cd1c0cac0_9a13a622_1280.jpg')
INSERT INTO ALBUM_DETAILS VALUES (51, 'album_5f797735ddf5a548ffd9eb34582a646f_fa843b9b_1280.jpg'), 
(51, 'album_3cf4d8b35cb768e752be461de39a6a63_0863e971_1280.jpg'), (51, 'album_c04b49a192ee0c4f405c307f136b43df_7611641d_1280.jpg'), 
(51, 'album_ee4b87f321586d31ce7a47f569a8e843_f44dba1b_1280.jpg'), (51, 'album_d22a40d8fc9fee0d2c2034aea969db65_52141edf_1280.jpg'), 
(51, 'album_d2a70dcaa1727b2f1ba4ffecc2299f60_0fe9a8b1_1280.jpg'),(51, 'album_3e50fcc3e1d7389aaf9ac4c3ff23fe35_391bf108_1280.jpg')
INSERT INTO ALBUM_DETAILS VALUES (51, 'album_4ce37f19e23ee5b7e1555f3cd1c0cac0_9a13a622_1280.jpg')

--INSERT PHIM TỪ 52 - 56 
SELECT * FROM Medias 
INSERT INTO Movies VALUES 
(52, 5, N'Cửa Hàng Tiện Lợi Saetbyul', 
		N'Cửa Hàng Tiện Lợi Saetbyul (Cửa Hàng Tiện Lợi Saet Byul) kể câu chuyện về Choi Dae Hyun (Ji Chang Wook), một anh chàng có ngoại hình xuất chúng và làm việc tại một tập đoàn lớn. Một lần, Choi Dae Hyun tình cờ gặp phải một nhóm nữ sinh quậy phá và bị cô nàng Jung Saet Byul (Kim Yoo Jung) nhờ mua giúp một bao thuốc lá trong cửa hàng tiện lợi. ',
 		9.0, 
		'movieposter_seatbyul.jpg', 
		853, 14, 1001, 
		'movievideo_seatbyul.mp4', 
		1, 
		'00:00:25', 
		N'Ji Chang Wook', 
		N'Hàn Quốc', 
		'2019-06-08', 2),
(53, 5, N'Người Thầy Y Đức 2', 
		N'Phim Người Thầy Y Đức 2 là một bộ phim y tế về thiên tài, nhưng bác sĩ lập dị Boo Yong Joo, hoặc giáo viên Kim ( Han Suk Kyu ), đặt trong bối cảnh của Bệnh viện Doldam. ', 
		9.0, 
		'movieposter_yduc2.jpg', 
		1000, 12, 1100, 
		'movievideo_yduc2.mp4', 1, 
		'00:00:40', N'Han Suk Kyu', 
		N'Hàn Quốc', '2018-06-01', 2),
(54, 5, N'Weathering with You', 
		N'Đứa con của thời tiết - Weathering with You là bộ phim mới nhất của đạo diễn lừng danh Makoto Shinkai, người đã từng khuấy động thế giới anime với bộ phim Your Name ăn khách.', 
		7.2, 
		'movieposter_thoitiet.jpg',
		700, 14, 901, 
		'movievideo_thoitiet.mp4', 1, 
		'00:01:02', N'Makoto Shinkai', 
		N'Nhật Bản', '2010-03-04', 2),
(55, 5, N'Thiên Thần Áo Trắng', 
		N'Một người đàn ông trẻ tuổi tên Park Shi On mắc chứng bệnh Savant và chậm phát triển. Tuổi trí tuệ của anh là 10 nhưng anh đã trở thành một bác sĩ phẫu thuật cho trẻ em. Trong khi đó, Kim Do Han bác sĩ phẫu thuật cho trẻ em tốt nhất luôn tìm thấy mình trong cuộc đối đầu với Park Shi On', 
		5.0, 
		'movieposter_aotrang.jpg', 
		120, 16, 600, 
		'movievideo_aotrang.mp4', 1, '00:01:27', 
		N'Gi Min Soo', N'Hàn Quốc', '2013-06-01', 2),
(56, 5, N'BỐN NĂM, HAI CHÀNG, MỘT TÌNH YÊU', 
		N'Phim 4 Năm, 2 Chàng, 1 Tình Yêu xoay quanh chuyện tình của Quỳnh (Midu), Tuấn (Harry Lu) và Thành (Anh Tú). Tại một ngôi trường cấp ba, chàng thiếu gia Tuấn phải lòng cô bạn học dễ thương Quỳnh. Anh giở đủ ngón nghề để chinh phục trái tim người đẹp nhưng cô vẫn còn e dè. Cùng lúc đó, Quỳnh cũng bị theo đuổi bởi Thành, một nam sinh nhỏ tuổi hơn và quen biết cô từ trước. Biến cố xảy ra khiến một trong hai chàng trai biến mất, để rồi tái xuất đầy bí ẩn nhiều năm sau đó.', 
		9.2, 
		'movieposter_421.jpg', 
		853, 14, 1001, 
		'movievideo_421.mp4', 1, '00:02:50', 
		N'Quốc Trung', N'Việt Nam', '2019-06-08', 2),
(57, 5, N'Hậu Duệ Mặt Trời', 
		N'Hậu Duệ Mặt Trời (Descendants of the Sun) có nội dung phim xoay quanh hai nhân vật Yoo Shi Jin và Kang Mo Yeon. Yoo Shi Jin là đội trưởng lực lượng gìn giữ hòa bình của Liên Hợp Quốc. Trong khi đó, Kang Mo Yeon là thành viên của tổ chức Bác sĩ không biên giới. ', 
		9.0, 
		'movieposter_mattroi.jpg', 
		1003, 14, 991, 
		'movievideo_mattroi.mp4', 1, 
		'00:01:57', N'Kang Mo Yeon', 
		N'Hàn Quốc', '2021-02-01', 2),
(58, 5, N'Khởi Nghiệp', 
		N'Phim Khởi Nghiệp (Start Up) là bộ phim tình cảm Hàn Quốc, nội dung xoay quanh câu chuyện lập nghiệp của Seo Dal Mi (Suzy thủ vai) với tham vọng trở thành Steve Job tương lai tại Sandbox - nơi được mệnh danh là “thung lũng Silicon của Hàn Quốc”. Bên cạnh đó cô còn muốn vực dậy công ty công nghệ của anh giám đốc trẻ Nam Do San (Nam Joo Hyuk thủ vai).', 
		9.9, 'movieposter_startup.jpg', 
		9000, 16, 9999, 
		'movievideo_startup.mp4', 1, '00:00:29', 
		N'Oh Choong-Hwan', N'Hàn Quốc', '2021-07-03', 2)
update Movies set Video = 'movievideo_thoitiet.mp4' where id=54
update Movies set Video = 'movievideo_startup.mp4' where id=58
--update create cho cái movie cũ.
update Movies set CreateAt ='2019-06-08', IdClassifiles=2 Where Id = 7
update Movies set CreateAt ='2019-06-08' Where Id BETWEEN 18 and 31

--insert movie từ 59 đến 65
SELECT * FROM Movies 
update Movie_Categories set Name ='Drama' where id =4
INSERT INTO Movies VALUES 
(59, 4, N'Thưa Mẹ Con Đi', 
		N'Thưa mẹ con đi  là câu chuyện xoay quanh Văn và Ian. Văn là Việt kiều Mỹ, từng theo học và đang làm việc tại xứ sở cờ hoa. Ian là người Việt quốc tịch Mỹ, theo gia đình sang đó định cư từ khi con nhỏ. Số phận rui rủi cả hai gặp gỡ và nảy sinh tình cảm với nhau, họ đã có với nhau những kỉ niệm thật đẹp cho đến khi Ian quyết định theo Văn về quê nhân dịp bốc mộ cha và ông nội.',
 		7.5, 
		'movieposter_thuamediacon.jpg', 
		853, 14, 1201, 
		'movievideo_thuamediacon.mp4', 
		1, 
		'00:02:09', 
		N'Trịnh Đình Lê Minh', 
		N'Việt Nam', 
		'2019-06-08', 2),
(60, 4, N'Bố Già 2', 
		N'Phim sẽ xoay quanh lối sống thường nhật của một xóm lao động nghèo, ở đó có bộ tứ anh em Giàu – Sang – Phú – Quý với Ba Sang sẽ là nhân vật chính, hay lo chuyện bao đồng nhưng vô cùng thương con cái. Câu chuyện phim tập trung về hai cha con Ba Sang (Trấn Thành) và Quắn (Tuấn Trần)', 
		6.8, 
		'movieposter_bogia2.jpg', 
		1200, 13, 3100, 
		'movievideo_BoGia2.mp4', 1, 
		'00:02:52', N'Trấn Thành', 
		N'Việt Nam', '2018-06-01', 2),
(61, 4, N'Mùi Đu Đủ Xanh', 
		N'Mùi, một cô bé đến giúp việc cho một gia đình ở Sài Gòn . Bối cảnh trở về những năm 1950 với một gia đình thương gia khá giả và không gian đậm nét cổ xưa...Mùi hàng ngày phụ giúp việc gia đình, công việc cứ thế trôi qua với những buồn vui và đối mặt với sự láu cá của đứa con trai út của bà chủ.', 
		7.4, 
		'movieposter_muaduduxanh.jpg',
		800, 14, 901, 'movievideo_muiduduxanh.mp4', 1, 
		'00:01:35', N'Trần Anh Hùng', 
		N'Việt Nam', '2010-03-04', 2),
(62, 4, N'HAI PHƯỢNG', 
		N'Hai Phượng là phim kể về hành trình nghẹt thở và căng thẳng của bà mẹ đơn thân Hai Phượng (Ngô Thanh Vân) khi phải đối đầu với cả 1 đường dây tội phạm bắt cóc và buôn bán nội tạng xuyên quốc gia để cứu đứa con gái bé bỏng Mai (Mai Cát Vi). Để cứu được con, Hai Phượng chỉ có 14 tiếng đồng hồ rượt đuổi từ Cần Thơ, Sài Gòn cho đến Phan Thiết, và phải đối đầu với rất nhiều giang hồ cộm cán, sẵn sàng tiêu diệt ai dám cản đường chúng. Hành trình cứu con của Hai Phượng càng trở nên gian nan và khó khăn hơn khi bất kỳ một sơ xuất nhỏ nào cũng sẽ phải trả giá bằng chính mạng sống của chính cô và bé Mai.', 
		6.3, 
		'movieposter_haiphuong.jpg', 
		520, 14, 800, 
		'movievideo_haiphuong.mp4', 1, '00:01:48', 
		N'Lê Văn Kiệt', N'Việt Nam', '2013-06-01', 2),
(63, 4, N'Vợ ba', 
		N'Người Vợ Ba là phim lấy bối cảnh vùng thôn quê Việt Nam cuối thế kỷ 19. Phim xoay quanh câu chuyện về cô gái tên Mây được gả làm vợ ba cho một gia đình giàu có. Tưởng như bắt đầu một cuộc sống sung túc, đầy đủ, cô gái trẻ không ngờ mình bị lôi vào một cuộc tranh đấu ngầm trong gia đình mới với vợ cả và vợ hai để có được vị trí quan trọng ở nhà chồng. Dựa trên những câu chuyện có thật về thân phận người phụ nữ Việt Nam trong xã hội xưa, phim cũng đề cập đến các vấn đề của xã hội lúc đó như hôn nhân sắp đặt, tục đa thê, trọng nam khinh nữ.', 
		6.7, 
		'movieposter_voba.jpg', 
		153, 14, 2001, 
		'movievideo_voba.mp4', 1, '00:02:13', 
		N'Nguyễn Phương Anh', N'Việt Nam', '2019-06-08', 2),
(64, 4, N'TIỆC TRĂNG MÁU', 
		N'Nội dung chính của “Tiệc Trăng Máu” phiên bản Việt Nam không có gì thay đổi nhiều nhưng những tình tiết ẩn dụ, mang đậm góc nhìn cá nhân được đạo diễn Nguyễn Quang Dũng khéo lèo lồng ghép vào. Bộ phim xoay quanh câu chuyện về một bữa tiệc “đẫm máu” của một nhóm bạn thân đã chơi với nhau hơn 40 năm cuộc đời. Bữa tiệc diễn ra rất vui vẻ cho đến khi cô gái có tên Nguyệt Ánh (do Hồng Ánh thủ vai) đề nghị các bạn mình chơi một trò chơi. Họ quyết định công khai tất cả những tin nhắn và điện thoại gọi tới trong bữa tiệc. Nhưng khi các bí mật cá nhân dần được khui ra thì họ còn có cảm thông và tình cảm vợ chồng, bạn bè có được vẹn nguyên.', 
		7.6, 
		'movieposter_tiettrangmau.jpg', 
		4003, 14, 6591, 
		'movievideo_tiettrangmau.mp4', 1, 
		'00:01:32', N'Nguyễn Quang Dũng', 
		N'Việt Nam', '2021-02-01', 2),
(65, 4, N'Song lang', 
		N'Song Lang 2018 là phim điện ảnh chính kịch đầu tay tại Việt Nam của đạo diễn Leon Quang Lê chính thức công chiếu vào ngày 17 tháng 8 năm 2018. Tên phim được đặt theo một loại nhạc cụ cùng tên có vai trò giữ nhịp trong dàn nhạc tài tử và cải lương và cũng mang thêm nghĩa kép là hai chàng trai.', 
		7.8, 'movieposter_songlang.jpg', 
		5000, 14, 8099, '
		movievideo_songlang.mp4', 1, '00:02:30', 
		N'Leon Quang Lê', N'Việt Nam', '2021-07-03', 2)



insert into Movie_classifies (Name) values (N'5+'), ('17+')
-- 66 - 72
select * from movies
update movies set video = 'movievideo_songlang.mp4' where id = 65
INSERT INTO Movies VALUES 
(66, 4, N'Yêu Cuồng Si', 
	N'Begin Again là câu chuyện về một mối tình âm nhạc. Tất cả bắt đầu từ cuộc gặp gỡ giữa Dan Mulligan, một nhà sản xuất rơi vào khủng hoảng vì cạn kiệt cảm hứng nghệ thuật cùng rắc rối trong hôn nhân và Greta, cô gái Anh còn chưa hết đau buồn vì cuộc chia tay với bạn trai tại New York. Như hai vì sao lạc lối, họ...',
 	6.2, 
	'movieposter_yeucuongsi_1267_712.jpg', 
	62, 17, 233, 
	'movievideo_beginagain.mp4', 
	1, 
	'00:01:31', 
	N'John Carney', 
	N'Mỹ', 
	'2020-04-08', 5),
(67, 4, N'Tình Địch Không Lối Thoát', 
	N'Bộ phim kể về tình bạn của hai ông bố khi còn trẻ cùng yêu một người. Nhưng người con gái đã chọn bố của Phong. 17 năm sau, hai ông bố lại chạm trán nhau khi hai người con của họ học cùng trường.',
 	4.5, 
	'movieposter_40831_c_2048.jpg', 
	1023, 16, 600, 
	'movievideo_tdklt.mp4', 
	1, 
	'00:01:41', 
	N'Trần Mạnh Hùng', 
	N'Việt Nam', 
	'2021-04-08', 3),
(68, 4, N'Penthouse: Cuộc Chiến Thượng Lưu', 
	N'Cuộc chiến thượng lưu kể về câu chuyện của những gia đình giàu có sống ở Hera Palace và những đứa con của họ tại trường nghệ thuật Cheong-ah.',
 	7.0, 
	'movieposter_poster-cuoc-chien-thuong-luu-phan-3.jpg', 
	6012, 16, 14290, 
	'movievideo_WarInLifeOfficial.mp4', 
	1, 
	'00:02:10', 
	N'Joo Dong-min', 
	N'Hàn Quốc', 
	'2021-07-02', 3),
(69, 2, N'Thiên Cổ Quyết Trần', 
	N'Thiên Cổ Quyết Trần chuyển thể từ tiểu thuyết Thượng Cổ của Tác giả Tinh Linh. Thiên thần Thượng cổ – một vị thần có thật sau khi mất đi trí nhớ đã chờ đợi người trong mộng trong sự cô đơn. Nàng được chúng sinh tôn kính vô cùng, một lòng sùng bái. Mặc dù bị ba vị thần còn lại ghen ghét nhưng Thượng cổ vẫn ung dung tự tại. "Nàng đã mất đi ký ức ba trăm năm thời hỗn độn, quên đi người mà nàng yêu nhất. Bây giờ, Cửu Châu tịch mịch, tam giới tuyệt vọng, Càn Khôn chỉ còn lại bóng dáng cô độc của nàng. Lần này, đổi lại là nàng đợi người ấy trở lại',
 	6.0, 
	'movieposter_poster-thien-co-quyet-tran.jpg', 
	298, 5, 642, 
	'movievideo_thincoquyetthan_480p.mp4', 
	1, 
	'00:02:07', 
	N'Lâm Kiến Long, Trần Quốc Hoa', 
	N'Trung Quốc', 
	'2020-09-02', 4),
(70, 5, N'Cùng Em Đến Tận Cùng Thế Giới - Reset in July', 
	N'Cùng Em Đến Tận Cùng Thế Giới (Reset in JuLy 2021) bộ phim truyền hình dài tập Trung Quốc thuộc thể loại thanh xuân vườn trường. Phim xoay quanh câu chuyện của các bạn trẻ đối mặt với áp lực học tập, tình cảm đôi lứa để cùng nhau vượt qua những thăng trầm.',
 	5.5, 
	'movieposter_poster-cung-em-den-tan-cung-the-gioi.jpg', 
	2493, 16, 8239, 
	'movievideo_restinjuly.mp4', 
	1, 
	'00:01:44', 
	N'Lý Minh Thần', 
	N'Trung Quốc', 
	'2021-01-02', 3),
(71, 1, N'Tokyo Revengers - Anh Sẽ Làm Giang Hồ Vì Em', 
	N'Tokyo Revenger (Anh Sẽ Làm Giang Hồ Vì Em) là tác phẩm mang thể loại băng đảng, xã hội đen - yakuza Nhật Bản sau nhiều năm vắng bóng nay đã trở lại. Anime Tokyo Revengers đang được khán giả trong nước lẫn châu Á quan tâm cực lớn bởi cốt truyện và kỹ xảo lôi cuốn người xem.',
 	7.1, 
	'movieposter_poster-tokyo-revengers.jpg', 
	18374, 2, 34072, 
	'movievideo_tokyorevengers.mp4', 
	1, 
	'00:01:42', 
	N'Hatsumi Kouichi', 
	N'Nhật Bản', 
	'2021-06-30', 2),
(72, 1, N'Ngụy Tạo (JiYeon T-ara) - Imitation (2021)', 
	N'Ngụy Tạo (Imitation 2021) bộ phim của đạo diễn Han Hyun Hee sử dụng chất liệu là ngành công nghiệp giải trí Hàn Quốc, một đề tài quá quen thuộc với fan hâm mộ Kpop nhưng ít được dựng thành phim truyền hình. Imitation kể về câu chuyện vươn lên tỏa sáng và cuộc sống hàng ngày của các nhóm nhạc thần tượng là Sparkling, Tea Party, Shax hay nữ thần solo La Ri Ma.',
 	6.1, 
	'movievideo_poster-nguy-tao-imitation-2021.jpg', 
	490, 16, 9384, 
	'movievideo_nguytao.mp4', 
	1, 
	'00:01:42', 
	N'Han Hyun Hee', 
	N'Hàn Quốc', 
	'2021-07-02', 3)
update movies set poster = 'movieposter_poster-nguy-tao-imitation-2021.jpg' where id = 72

--INSERT 73 - 79
INSERT INTO Movies VALUES 
(73, 4, N'Thỏ Peter 2: Cuộc Trốn Chạy', 
		N'Thỏ Peter 2 : Cuộc Chạy Trốn (Peter Rabbit 2: The Runaway) là một bộ phim hoạt hình hài hước của Mỹ do đạo diễn Will Gluck chỉ đạo sản xuất, và phần kịch bản được chắp bút bởi Patrick Burleigh và Gluck. Đây là phần tiếp theo của Thỏ Peter, dựa trên những câu chuyện được tạo bởi Beatrix Potter trong phần trước. Trong phim, những kẻ lừa đảo đáng yêu đã trở lại. Bea, Thomas và những con thỏ đã tạo ra một gia đình tạm thời, nhưng mặc dù đã cố gắng hết sức, Peter dường như không thể làm suy suyển những "thành tích" nghịch ngợm tinh quái của cậu. Quyết định mở rộng cuộc phiêu lưu ra khỏi khu vườn, Peter tìm thấy chính mình ở nơi mà bản chất thật của cậu được trân trọng. Nhưng rồi gia đình cậu nhất quyết đánh đổi bằng mọi giá để tìm cậu về. Peter buộc phải tìm ra lối đi cho riêng mình và trở thành một người mà cậu luôn mong muốn.',
 		5.6, 
		'movieposter_peterrabbit2.jpg', 
		16, 14, 123, 
		'movievideo_peterrabbit2.mp4', 
		1, 
		'00:02:37', 
		N'Will Gluck', 
		N'Mỹ', 
		'2021-03-07', 2),
(74, 4, N'Cậu Bé Cá Sấu', 
		N'Arlo lạc quan rời ngôi nhà nơi đầm lầy phương Nam đến Thành phố New York để đi tìm người bố chưa từng gặp, đồng thời kết bạn trên đường. Một cuộc phiêu lưu đầy chất nhạc.',
 		6.5, 
		'movieposter_caubecasau.jpg', 
		45, 23, 147, 
		'movievideo_caubecasau.mp4', 
		1, 
		'00:01:57', 
		N'Ryan Crego', 
		N'Mỹ', 
		'2021-04-07', 2),
(75, 4, N'Đừng Chọc Anh Nữa Mà Nagatoro', 
		N'Học sinh trung học Hayase Nagatoro thích dành thời gian rảnh để làm một việc, đó là bắt nạt Senpai của cô ấy! Sau khi Nagatoro và những người bạn của cô tình cờ nhìn thấy những bức vẽ của một họa sĩ đầy tham vọng, họ cảm thấy thích thú khi bắt nạt Senpai nhút nhát một cách không thương tiếc. Nagatoro quyết tâm tiếp tục trò chơi tàn ác của mình và đến thăm anh hàng ngày để cô có thể buộc Senpai làm bất cứ điều gì có lợi cho cô vào thời điểm đó, đặc biệt nếu điều đó khiến anh không thoải mái. Hơi bị kích thích và có phần sợ hãi Nagatoro, Senpai liên tục bị cuốn vào những trò hề của cô khi sở thích, sở thích, ngoại hình và thậm chí cả tính cách của anh đều bị lợi dụng để chống lại anh khi cô giải trí với chi phí của anh. Thời gian trôi qua, Senpai nhận ra rằng anh ta không thích sự hiện diện của Nagatoro, và hai người họ phát triển một tình bạn không êm đềm khi một người kiên nhẫn bày ra những trò hề của người kia. ',
 		5.4, 
		'movieposter_dungchocanhnuama.jpg', 
		26, 24, 323, 
		'movievideo_dungchocanhnua.mp4', 
		1, 
		'00:00:55', 
		N'Đang cập nhật', 
		N'Nhật Bản', 
		'2021-05-07', 2),
(76, 4, N'Ước Gì Ngày Mai Tận Thế', 
		N'Phim bắt đầu trong một khu ký túc xá của nhiều sinh viên đến từ khắp nơi trên thế giới, điều đáng nói là tại đây những vấn đề xảy ra tương tự như ở ngoài đời thật của chúng ta thường thấy, vì khác xa nền văn hóa với nhau nên có nhiều tình huống khó đỡ xảy ra giữa các cô cậu sinh viên. Đặc biệt là những trò đùa “trời ơi đất hỡi” chắc chắn sẽ gây bất ngờ với khán giả khi xem phim Ước Gì Ngày Mai Tận Thế.
		Có thể đây là một bộ phim có đề tài học đường trên màn ảnh những năm gần đây không mới lạ nhưng phim lại sở hữu dàn sao trẻ, mới nổi và tài năng ít xuất hiện trước công chúng ắt hẳn sẽ mang lại cảm giác bất ngờ ngay ở tập 1. Thông qua bộ phim Ước Gì Ngày Mai Tận Thế , các fan K-drama có thể hiểu thêm về cuộc sống đại học ở Hàn Quốc cùng những trò đùa tinh nghịch. Đồng thời còn mở ra các câu chuyện về định hướng trưởng thành, tình yêu, tuổi trẻ, khát vọng và tình bạn của một nhóm học sinh.',
 		5.5, 
		'movieposter_uocgingaymaitanthe.jpg', 
		22, 34, 223, 
		'movievideo_uocgingaymaitanthe.mp4', 
		1, 
		'00:01:33', 
		N'Kwon Ik Joon, Kim Jung Shik', 
		N'Hàn Quốc', 
		'2021-06-07', 2),
(77, 4, N'Võ Trạng Nguyên Tô Khất Nhi: Thánh Dụ Trời Ban', 
		N'Phim kể vào cuối đời nhà Thanh, các quan đại thần liên tiếp chết một cách bất đắc kỳ tử, tin đồn về nhà Thanh sẽ diệt vong được lưu truyền trong dân gian. Vì để ngăn chặn lời đồn của quần chúng, Hoàng thượng quyết định sẽ tổ chức nghi lễ tế trời, đồng thời bí mật ra lệnh cho Phú Sát Ngự Hằng điều tra vụ án này. Phú Sát Ngự Hằng biết được mức độ nghiêm trọng của vụ án, đành phải tìm đến người bạn tốt của mình là Võ trạng nguyên đời trước, cũng chính là bang chủ Cái Bang Tô Xán nhờ giúp đỡ. Nhưng sau đó những việc ngoài ý muốn lại kéo nhau mà đến, đầu tiên là Tô Xán và Phú Sát Ngự Hằng bị ám sát, tiếp đến là cái chết bất ngờ của nhiều vị đại thần, mà thân thể họ đã bị ăn mòn gần hết. Khiến cho Tô Xán nhận ra rằng vụ án này rất khó giải quyết, phía sau nó nhất định ẩn chứa một bí mật động trời…"',
 		5.6, 
		'movieposter_thanhdutroiban.jpg', 
		35, 43, 223, 
		'movievideo_thanhdutroiban.mp4', 
		1, 
		'00:01:13', 
		N'Đang cập nhật', 
		N'Trung Quốc', 
		'2021-07-07', 2),
(78, 4, N'101 Chú Chó Đốm', 
		N'101 Chú Chó Đốm - Cruella (2021) kể về câu chuyện của nhân vật phản diện huyền thoại trong phim hoạt hình của Disney - Cruella de Vil. Lấy bối cảnh London những năm 1970, cô lao công Estella quyết tâm đổi đời từ các thiết kế sáng tạo độc đáo của mình. Estella quen biết với hai gã trộm, ba người trở thành một nhóm cùng nhau xây dựng tên tuổi ở London. Một ngày nọ, năng khiếu thời trang của Estella lọt vào mắt xanh của huyền thoại thời trang von Hallman, bà ta là người phụ nữ đáng ngưỡng mộ và đáng sợ không kém. Từ đây Estella dấn thân vào con đường trở thành biểu tượng thời trang nhưng tàn nhẫn và đầy hận thù - Cruella.',
 		6.6, 
		'movieposter_101chodom.jpg', 
		66, 73, 253, 
		'movievideo_101chodom.mp4', 
		1, 
		'00:01:32', 
		N'Craig Gillespie', 
		N'Mỹ', 
		'2021-07-06', 2),
(79, 4, N'Những Người Bạn Thân - Tập Đặc Biệt', 
		N'"Friends: The Reunion", còn được gọi là "The One Where They Get Back Together", là một bộ phim sitcom Friends đặc biệt về cuộc tái hợp năm 2021 của truyền hình Mỹ.',
 		6.2, 
		'movieposter_nhungnguoiban.jpg', 
		23, 83, 353, 
		'movievideo_nhungnguoiban.mp4', 
		1, 
		'00:02:00', 
		N'Marta Kauffman, David Cran, Kevin S.', 
		N'Mỹ', 
		'2021-07-16', 2)