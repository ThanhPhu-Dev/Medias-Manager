﻿CREATE TABLE Users
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

CREATE TABLE Movies
(
	Id INT NOT NULL UNIQUE,
	IdCategory INT,
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
select * from Media_categories
select * from Audios
select * from Audio_Categories
select * from Media_Categories 
select * from Likes 
select * from My_Lists
select * from levels
select * from Medias 
select * from Movies
select * from Movie_Categories
select * from Album_Details
select * from Albums
select * from Users where Email = 'nghiadx2001@gmail.c'
update Albums set Id = 2 where Name=N'Thiên nhiên'
update Album_Details set IdAlbum = 2
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
select * from Movies
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

-- Phim viễn Tưởng ( Bắt đầu Id = 32 -> 37 là dùng update vì có data test trong db r)