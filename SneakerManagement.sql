CREATE DATABASE SneakerManagement
GO

USE SneakerManagement
GO

CREATE TABLE City(
	ID int not null,
	Name nvarchar(max) not null,
	constraint PK_City primary key(ID)
)
GO
CREATE TABLE District(
	ID int not null,
	Name nvarchar(max) not null,
	IDCity int not null,
	constraint PK_District primary key(ID),
	constraint FK_District_City foreign key(IDCity) references City(ID)
)
GO
CREATE TABLE Ward(
	ID int not null,
	Name nvarchar(max) not null,
	IDDistrict int not null,
	constraint PK_Ward primary key(ID),
	constraint FK_Ward_District foreign key(IDDistrict) references District(ID)
)
GO

CREATE TABLE tbl_Client (
	ID int identity not null,
	Name nvarchar(max) not null,
	DateOfBirth Date,
	Gender nvarchar(3),
	Phone char(10) not null unique,
	IDWard int,
	constraint PK_Client primary key(ID),
	constraint FK_Client_Address foreign key(IDWard) references Ward(ID)
)
GO

CREATE TABLE tbl_Account(
	ID int identity not null,
	Username varchar(100) not null unique,
	Password varchar(30) not null,
	constraint PK_Account primary key(ID)
)
GO

CREATE TABLE tbl_Account_Client (
	ID int identity not null,
	IDAccount int not null,
	IDClient int not null,
	constraint PK_AC primary key(ID),
	constraint FK_AC_Account foreign key(IDAccount) references tbl_Account(ID),
	constraint FK_AC_Client foreign key(IDClient) references tbl_Client(ID)
)
GO

CREATE TABLE tbl_Staff (
	ID int identity not null,
	Name nvarchar(max) not null,
	DateOfBirth Date,
	Gender nvarchar(3),
	Phone char(10) not null unique,
	IDWard int,
	constraint PK_Staff primary key(ID),
	constraint FK_Staff_Address foreign key(IDWard) references Ward(ID)
)
GO

CREATE TABLE tbl_Account_Staff (
	ID int identity not null,
	IDAccount int not null,
	IDStaff int not null,
	constraint PK_AS primary key(ID),
	constraint FK_AS_Account foreign key(IDAccount) references tbl_Account(ID),
	constraint FK_AS_Staff foreign key(IDStaff) references tbl_Staff(ID)
)
GO


CREATE TABLE tbl_SneakerType(
	ID int identity not null,
	Name nvarchar(255) not null unique,
	constraint PK_SneakerType primary key(ID)
)
GO

CREATE TABLE tbl_Sneaker(
	ID char(20) not null,
	Name nvarchar(255) not null unique,
	LinkPicture varchar(max) not null,
	LinkPictureDetails varchar(max) not null,
	Price float not null check(Price > 0),
	Discount int check(Discount >= 0 and Discount < 100) default 0, -- % giảm giá
	PriceAfterDiscount float,
	IDSneakerType int,
	constraint PK_Sneaker primary key(ID),
	constraint FK_Sneaker_Type foreign key(IDSneakerType) references tbl_SneakerType(ID)
)
GO


CREATE TABLE tbl_Size(
	ID int identity not null,
	Size int not null,
	constraint PK_Size primary key(ID)
)
GO

CREATE TABLE tbl_SizeDetails(
	ID int identity not null,
	IDSize int,
	IDSneaker char(20),
	Amount int not null check(Amount >= 0),
	constraint PK_SizeDetails primary key(ID),
	constraint FK_SizeDetails_Size foreign key(IDSize) references tbl_Size(ID),
	constraint FK_SizeDetails_Sneaker foreign key(IDSneaker) references tbl_Sneaker(ID)
)
GO

CREATE TABLE tbl_Bill(
	ID int identity not null,
	IDClient int,
	OrderDate date not null default GetDate(),
	TotalMoney float,
	constraint PK_Bill primary key(ID),
	constraint FK_Bill_Client foreign key(IDClient) references tbl_Client(ID)
)
GO

CREATE TABLE tbl_BillDetails(
	ID int identity not null,
	IDBill int,
	IDSneaker char(20),
	AmountBuy int not null check(AmountBuy > 0),
	constraint PK_BillDetails primary key(ID),
	constraint FK_BillDetails_Sneaker foreign key(IDSneaker) references tbl_Sneaker(ID),
	constraint FK_BillDetails_Bill foreign key(IDBill) references tbl_Bill(ID)
)
GO


-- ================= insert data =================

INSERT INTO tbl_SneakerType
VALUES (N'Jordan'),
		(N'Giày Nike'),
		(N'Adidas'),
		(N'Converse'),
		(N'Giày Luxury'),
		(N'Vans'),
		(N'MLB'),
		(N'Dép siêu cấp')
GO

INSERT INTO tbl_Sneaker(ID, Name, LinkPicture, LinkPictureDetails, Price, Discount, IDSneakerType)
VALUES ('GNAF1LWB', N'Giày Nike Air Force 1 Low White Brown', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2500000, 68, 2),
		('NAF1TFWLA', N'Nike Air Force 1 Trắng Full White Like Auth', '1.jpeg;2.jpg', '1.jpeg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2500000, 68, 2),
		('NKPGD', N'Nike Kwondo1 x Peaceminusone G-Dragon', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg;', 3000000, 50, 2),
		('NKAF1AWBSR', N'Giày Nike Air Force 1 ’07 AN20 White Black Swoosh Rep 1:1', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2500000, 68, 2),
		('GNAF1TFWSC', N'Giày Nike Air Force 1 Trắng Full White Siêu Cấp', '1.jpeg;2.jpg', '1.jpeg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2000000, 60, 2),
		('NAF1LSF', N'Nike Air Force 1 Low Stussy Fossil', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2500000, 52, 2),
		('GNAF1LPSR', N'Giày Nike Air Force 1 Low Paisley Swoosh Rep 1:1', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2500000, 52, 2),
		('NAF1LU', N'Nike Air Force 1 ’07 Lv8 Utility’', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2500000, 68, 2),
		('NAF1QSVDLL', N'Nike Air Force 1 07 QS Valentine’s Day Love Letter', '1.jpg;2.jpg', '', 3000000, 64, 2),
		('NXAF1LXTARSR', N'Nike Xé Air Force 1 LX Tear Away Red Swoosh Rep 1:1', '1.jpg;2.jpg', '', 2500000, 68, 2),
		('NAF1LFR', N'Nike Air Force 1 Low Flax Rep 1:1', '1.jpg;2.jpg', '', 2000000, 55, 2),
		('NAF1STW', N'Nike Air Force 1 Shadow Triple White', '1.jpg;2.jpg', '', 2000000, 60, 2),
		('NAFJDITR', N'Nike Air Force Just do It trắng Rep 1:1', '1.jpg;2.jpg', '', 1700000, 53, 2),
		('NAF1LOWVR', N'Nike Air Force 1 Low Off White Volt Rep 1:1', '1.jpg;2.jpg', '', 3000000, 60, 2),
		('NAF1LTSCJR', N'Nike Air Force 1  Low Travis Scott Cactus Jack Rep 1:1', '1.jpg;2.jpg', '', 3000000, 60, 2),
		('NAF1SPI7M', N'Nike Air Force 1 Shadow Pale Ivory (7 màu)', '1.jpg;2.jpg', '', 1800000, 61, 2),
		('NAF1VSMG', N'Nike Air Force 1 Vandalized Sail Mystic Green', '1.jpg;2.jpg', '', 1400000, 43, 2),
		('NAF1SBPI', N'Nike Air Force 1 Shadow Beige Pale Ivory', '1.jpg;2.jpg', '', 1400000, 43, 2),
		('NAF1SDSAC', N'Nike Air Force 1 Shadow ‘Daisy’ Spruce Aura Custom', '1.jpeg;2.jpg', '', 2000000, 60, 2),
		('NAF1LTSS', N'Nike Air Force 1 Low Travis Scott Sail', '1.jpg;2.jpg', '', 2500000, 52, 2),
		('NAF1GDPPN', N'Nike Air Force 1 G-Dragon Peaceminusone Para-noise', '1.jpg;2.jpg', '', 2000000, 51, 2),
		('NAF1GDKE', N'Nike Air Force 1 G-Dragon Korea Exclusive', '1.jpg;2.jpg', '', 2000000, 51, 2),
		('NAF1CCT', N'Nike Air Force 1 cao cổ trắng', '1.jpg;2.jpg', '', 1500000, 47, 2),
		('GNAJ1LBT', N'Giày Nike Air Jordan 1 Low ‘Bred Toe’', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 3000000, 60, 2),
		('GNAJ1LPR', N'Giày Nike Air Jordan 1 Low Panda Rep 1:1', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 3000000, 64, 2),
		('NBL77VWB', N'Nike Blazer Low 77 Vintage White Black', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2000000, 63, 2),
		('GNAJ1RHBW', N'Giày Nike Air Jordan 1 Retro High Black White (Đen Trắng)', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2000000, 63, 2),


		('GAYB350V2DS', N'Giày Adidas Yeezy Boost 350 V2 Desert Sage', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg', 2500000, 52, 3),
		('GAYB350V2TL', N'Giày Adidas Yeezy Boost 350 V2 Tail Light', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2500000, 52, 3),
		('GAY350V2YR', N'Giày Adidas Yeezy 350 V2 Yecheil Reflective', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg', 2500000, 52, 3),
		('GAY350V2CW', N'Giày Adidas Yeezy 350 V2 Cloud White', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2500000, 52, 3),
		('GAY350V2S', N'Giày Adidas Yeezy 350 V2 Sesame', '1.jpg;2.jpg', '', 2000000, 41, 3),
		('GAY350V2CRW', N'Giày Adidas Yeezy 350 V2 Cream White', '1.jpg;2.jpg', '', 2000000, 41, 3),
		('GAY350V2SDFPQ', N'Giày Adidas Yeezy 350 V2 Static Đen Full Phản Quang', '1.jpg;2.jpg', '', 2000000, 41, 3),
		('GAY350V2C', N'Giày Adidas Yeezy 350 V2 Cinder', '1.jpg;2.jpg', '', 2000000, 41, 3),
		('GAY700V3A', N'Giày Adidas Yeezy 700 V3 Azael', '1.jpg;2.jpg', '', 2500000, 58, 3),
		('GAY700V2V', N'Giày Adidas Yeezy 700 V2 Vanta', '1.jpg;2.jpg', '', 2000000, 48, 3),
		('GAY700V2G', N'Giày Adidas Yeezy 700 V2 Geode', '1.jpg;2.jpg', '', 2000000, 48, 3),
		('GAY700V2I', N'Giày Adidas Yeezy 700 V2 Inertia', '1.jpg;2.jpg', '', 2000000, 48, 3),
		('GAY700V2T', N'Giày Adidas Yeezy 700 V2 Tephra', '1.jpg;2.jpg', '', 2000000, 48, 3),
		('GAY700V2HB', N'Giày Adidas Yeezy 700 V2 Hospital Blue', '1.jpg;2.jpg', '', 2000000, 48, 3),
		('GAY700M', N'Giày Adidas Yeezy 700 Magnet', '1.jpg;2.jpg', '', 2000000, 48, 3),
		('GAY700S', N'Giày Adidas Yeezy 700 Salt', '1.jpg;2.jpg', '', 2000000, 48, 3),
		('GAY700A', N'Giày Adidas Yeezy 700 Analog', '1.jpg;2.jpg', '', 2000000, 48, 3),


		('GAMGDLA', N'Giày Alexander Mcqueen gót đen Like Auth', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 4000000, 45, 5),
		('GAMRPQ', N'Giày Alexander Mcqueen reflective (phản quang)', '1.jpeg;2.jpg', '1.jpeg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2000000, 51, 5),
		('GAMTGDR', N'Giày Alexander McQueen trắng gót đen Rep 1:1', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2000000, 51, 5),


		('GMLBBCMHBRS', N'Giày MLB Bigball Chunky Mono Heel Boston Red Sox', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2000000, 65, 7),
		('GMLBBPRSR', N'Giày MLB Boston P Red Sox Rep 1:1', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 1400000, 57, 7),
		('GMLBBBCLLDBR', N'Giày MLB Big Ball Chunky LITE LA Dodgers Black Rep 1:1', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2200000, 64, 7),
		('GMLBBCLLDGR', N'Giày MLB BigBall Chunky Lite LA Dodgers Grey Rep 1:1', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2200000, 64, 7),
		('MLBBCMLNY', N'MLB Bigball Chunky Monogram LT New York Yankees Rep 1:1', '1.jpg;2.jpg', '', 2500000, 72, 7),
		('MLBNTCVR', N'MLB NY trắng chữ vàng Rep 1:1', '1.jpg;2.jpg', '', 1400000, 50, 7),
		('GMLBNTCDDNR', N'Giày MLB NY trắng chữ đen đế nâu rep 1:1', '1.jpg;2.jpg', '', 1400000, 50, 7),
		('GMLBBCLNYYIR', N'Giày MLB BigBall Chunky Lite New York Yankees Ivory Rep 1:1', '1.jpg;2.jpg', '', 2200000, 64, 7),
		('MLBPMMNYYBR', N'MLB Playball Mule Monogram New York Yankees Black Rep 1:1', '1.jpg;2.jpg', '', 2000000, 68, 7),
		('MLBPMMNYYWR', N'MLB Playball Mule Monogram New York Yankees White Rep 1:1', '1.jpg;2.jpg', '', 2000000, 68, 7),
		('GMLBPMMDNYYR', N'Giày MLB PlayBall Mule Mono Denim New York Yankees Rep 1:1', '1.jpg;2.jpg', '', 2000000, 68, 7),

		
		('DBPSBFG', N'Dép Balenciaga Pool Slide Black Fluo Green', '1.jpg', '', 2500000, 52, 8),
		('DBPSBWD', N'Dép Balenciaga Pool Slide Black White (Đen)', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg', 2500000, 72, 8),
		('DBPSGX', N'Dép Balenciaga Pool Slide Grey Xám', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg', 2500000, 72, 8),
		('DBPSL', N'Dép Balenciaga Pool Slide Lime', '1.jpg', '', 2500000, 64, 8),
		('DBPSLB', N'Dép Balenciaga Pool Slide Logo Black', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg', 2500000, 64, 8),
		('DBPSPH', N'Dép Balenciaga Pool Slide Pink (Hồng)', '1.jpg', '', 2500000, 64, 8),
		('DBPSWT', N'Dép Balenciaga Pool Slide White (Trắng)', '1.jpg', '1.jpg;2.jpg;3.jpg', 2500000, 64, 8),
		('DGWSSBLA', N'Dép Gucci Web Slide Sandal Black Like Auth', '1.jpg;2.jpg', '1.jpg;2.jpg;3.jpg;4.jpg;5.jpg;6.jpg;7.jpg', 2500000, 64, 8)
GO

INSERT INTO tbl_Size
VALUES (38),(39),(40),(41),(42),(43)
GO

INSERT INTO tbl_SizeDetails
VALUES (3, 'GNAF1LWB', 20),
		(4, 'GNAF1LWB', 5),
		(5, 'GNAF1LWB', 10),
		(2, 'NAF1TFWLA', 10),
		(4, 'NAF1TFWLA', 5),
		(6, 'NAF1TFWLA', 4),
		(1, 'NKPGD', 10),
		(2, 'NKPGD', 5),
		(3, 'NKPGD', 4),
		(1, 'NKAF1AWBSR', 3),
		(2, 'NKAF1AWBSR', 15),
		(3, 'NKAF1AWBSR', 4),
		(3, 'GAYB350V2DS', 10),
		(4, 'GAYB350V2DS', 12),
		(6, 'GAYB350V2DS', 6),
		(3, 'GAY350V2YR', 10),
		(4, 'GAY350V2YR', 12),
		(5, 'GAY350V2YR', 6),
		(3, 'GAY350V2CW', 10),
		(4, 'GAY350V2CW', 12),
		(5, 'GAY350V2CW', 6),
		(3, 'GAMGDLA', 10),
		(4, 'GAMGDLA', 10),
		(5, 'GAMGDLA', 10),
		(3, 'GAMRPQ', 10),
		(4, 'GAMRPQ', 10),
		(5, 'GAMRPQ', 10),
		(3, 'GAMTGDR', 10),
		(4, 'GAMTGDR', 10),
		(5, 'GAMTGDR', 10),
		(3, 'GMLBBCMHBRS', 10),
		(4, 'GMLBBCMHBRS', 10),
		(5, 'GMLBBCMHBRS', 10),
		(3, 'GMLBBPRSR', 10),
		(4, 'GMLBBPRSR', 10),
		(5, 'GMLBBPRSR', 10),
		(3, 'GMLBBCLLDGR', 10),
		(4, 'GMLBBCLLDGR', 10),
		(5, 'GMLBBCLLDGR', 10),
		(3, 'MLBBCMLNY', 10),
		(4, 'MLBBCMLNY', 10),
		(5, 'MLBBCMLNY', 10),
		(3, 'DBPSBFG', 10),
		(4, 'DBPSBFG', 10),
		(5, 'DBPSBFG', 10),
		(3, 'DBPSBWD', 10),
		(4, 'DBPSBWD', 10),
		(5, 'DBPSBWD', 10),
		(3, 'DBPSL', 10),
		(4, 'DBPSL', 10),
		(5, 'DBPSL', 10)
GO

INSERT INTO tbl_Staff
VALUES (N'Châu Thịnh', '2002/05/24', N'Nam', '0972714788', 29329)
GO

INSERT INTO tbl_Account
VALUES ('cvt', 'cvtadmin') --admin
GO

INSERT INTO tbl_Account_Staff
VALUES (1, 1)
GO

Select * from tbl_Sneaker, tbl_SizeDetails, tbl_Size where tbl_Sneaker.ID = tbl_SizeDetails.IDSneaker and tbl_SizeDetails.IDSize = tbl_Size.ID


select * from Ward, City, District where Ward.IDDistrict = District.ID and District.IDCity = City.ID and City.Name like N'%Trà Vinh%'

select * from tbl_Account
select * from tbl_Staff
select * from tbl_Account_Staff

select * from tbl_Sneaker, tbl_SneakerType where tbl_SneakerType.Name like '%Luxury%' and LinkPictureDetails != ''

Alter PROC GiamGia
AS
	UPDATE tbl_Sneaker
	SET PriceAfterDiscount = Price - (Price * (Discount / 100.0))
GO

EXEC GiamGia