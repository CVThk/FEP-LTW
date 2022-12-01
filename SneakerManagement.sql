CREATE DATABASE SneakerManagement
GO

USE SneakerManagement
GO

-- ============================================================================= CREATE TABLE =============================================================================

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
	Phone varchar(10) not null unique,
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
	Phone varchar(10) not null unique,
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
	ID varchar(20) not null,
	Name nvarchar(255) not null unique,
	--LinkPicture varchar(max) not null,
	--LinkPictureDetails varchar(max) not null,
	Price float not null check(Price > 0),
	Discount int check(Discount >= 0 and Discount < 100) default 0, -- % giảm giá
	--PriceAfterDiscount float,
	IDSneakerType int,
	constraint PK_Sneaker primary key(ID),
	constraint FK_Sneaker_Type foreign key(IDSneakerType) references tbl_SneakerType(ID)
)
GO

CREATE TABLE tbl_CoverImage(
	ID int identity,
	IDSneaker varchar(20),
	Link varchar(max),
	constraint PK_CoverImage primary key(ID),
	constraint FK_CoverImage_Sneaker foreign key(IDSneaker) references tbl_Sneaker(ID)
)
GO

CREATE TABLE tbl_DetailsImage(
	ID int identity,
	IDSneaker varchar(20),
	Link varchar(max),
	constraint PK_DetailsImage primary key(ID),
	constraint FK_DetailsImage_Sneaker foreign key(IDSneaker) references tbl_Sneaker(ID)
)
GO

CREATE TABLE tbl_Size(
	ID int identity not null,
	Size int not null,
	constraint PK_Size primary key(ID)
)
GO

CREATE TABLE tbl_Inventory(
	ID int identity not null,
	IDSize int,
	IDSneaker varchar(20),
	Amount int not null check(Amount >= 0),
	constraint PK_Inventory primary key(ID),
	constraint FK_Inventory_Size foreign key(IDSize) references tbl_Size(ID),
	constraint FK_Inventory_Sneaker foreign key(IDSneaker) references tbl_Sneaker(ID)
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
	IDSize int,
	IDSneaker varchar(20),
	AmountBuy int not null check(AmountBuy > 0),
	constraint PK_BillDetails primary key(ID),
	constraint FK_BillDetails_Sneaker foreign key(IDSneaker) references tbl_Sneaker(ID),
	constraint FK_BillDetails_Bill foreign key(IDBill) references tbl_Bill(ID),
	constraint FK_BillDetails_Size foreign key(IDSize) references tbl_Size(ID)
)
GO

CREATE TABLE tbl_Cart(
	ID int identity,
	IDClient int,
	IDSneaker varchar(20),
	IDSize int,
	AmountBuy int not null check(AmountBuy > 0),
	constraint PK_Cart primary key(ID),
	constraint FK_Cart_Sneaker foreign key(IDSneaker) references tbl_Sneaker(ID),
	constraint FK_Cart_Client foreign key(IDClient) references tbl_Client(ID),
	constraint FK_Cart_Size foreign key(IDSize) references tbl_Size(ID)
)
GO

CREATE TABLE tbl_ImportInvoice(
	ID int identity,
	IDStaff int,
	TotalMoney float,
	ImportDate date default GETDATE(),
	constraint PK_ImportInvoice primary key(ID),
	constraint FK_ImportInvoice_Staff foreign key(IDStaff) references tbl_Staff(ID)
)
GO

CREATE TABLE tbl_ImportInvoiceDetails(
	ID int identity,
	IDImportInvoice int,
	IDSneaker varchar(20),
	IDSize int,
	ImportDate date default GETDATE(),
	Amount int,
	constraint CK_AmountImportInvoiceDetails check(Amount > 0),
	constraint PK_ImportInvoiceDetails primary key(ID),
	constraint FK_ImportInvoiceDetails_Sneaker foreign key(IDSneaker) references tbl_Sneaker(ID),
	constraint FK_ImportInvoiceDetails_ImportInvoice foreign key(IDImportInvoice) references tbl_ImportInvoice(ID),
	constraint FK_Import_Size foreign key(IDSize) references tbl_Size(ID)
)
GO

-- ============================================================================= INSERT DATA =============================================================================
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

INSERT INTO tbl_Sneaker(ID, Name, Price, Discount, IDSneakerType)
VALUES ('GNAF1LWB', N'Giày Nike Air Force 1 Low White Brown', 2500000, 68, 2),
		('NAF1TFWLA', N'Nike Air Force 1 Trắng Full White Like Auth', 2500000, 68, 2),
		('NKPGD', N'Nike Kwondo1 x Peaceminusone G-Dragon', 3000000, 50, 2),
		('NKAF1AWBSR', N'Giày Nike Air Force 1 ’07 AN20 White Black Swoosh Rep 1:1', 2500000, 68, 2),
		('GNAF1TFWSC', N'Giày Nike Air Force 1 Trắng Full White Siêu Cấp', 2000000, 60, 2),
		('NAF1LSF', N'Nike Air Force 1 Low Stussy Fossil', 2500000, 52, 2),
		('GNAF1LPSR', N'Giày Nike Air Force 1 Low Paisley Swoosh Rep 1:1', 2500000, 52, 2),
		('NAF1LU', N'Nike Air Force 1 ’07 Lv8 Utility’', 2500000, 68, 2),
		('NAF1QSVDLL', N'Nike Air Force 1 07 QS Valentine’s Day Love Letter', 3000000, 64, 2),
		('NXAF1LXTARSR', N'Nike Xé Air Force 1 LX Tear Away Red Swoosh Rep 1:1', 2500000, 68, 2),
		('NAF1LFR', N'Nike Air Force 1 Low Flax Rep 1:1', 2000000, 55, 2),
		('NAF1STW', N'Nike Air Force 1 Shadow Triple White', 2000000, 60, 2),
		('NAFJDITR', N'Nike Air Force Just do It trắng Rep 1:1', 1700000, 53, 2),
		('NAF1LOWVR', N'Nike Air Force 1 Low Off White Volt Rep 1:1', 3000000, 60, 2),
		('NAF1LTSCJR', N'Nike Air Force 1  Low Travis Scott Cactus Jack Rep 1:1', 3000000, 60, 2),
		('NAF1SPI7M', N'Nike Air Force 1 Shadow Pale Ivory (7 màu)', 1800000, 61, 2),
		('NAF1VSMG', N'Nike Air Force 1 Vandalized Sail Mystic Green', 1400000, 43, 2),
		('NAF1SBPI', N'Nike Air Force 1 Shadow Beige Pale Ivory', 1400000, 43, 2),
		('NAF1SDSAC', N'Nike Air Force 1 Shadow ‘Daisy’ Spruce Aura Custom', 2000000, 60, 2),
		('NAF1LTSS', N'Nike Air Force 1 Low Travis Scott Sail', 2500000, 52, 2),
		('NAF1GDPPN', N'Nike Air Force 1 G-Dragon Peaceminusone Para-noise', 2000000, 51, 2),
		('NAF1GDKE', N'Nike Air Force 1 G-Dragon Korea Exclusive', 2000000, 51, 2),
		('NAF1CCT', N'Nike Air Force 1 cao cổ trắng', 1500000, 47, 2),
		('GNAJ1LBT', N'Giày Nike Air Jordan 1 Low ‘Bred Toe’', 3000000, 60, 2),
		('GNAJ1LPR', N'Giày Nike Air Jordan 1 Low Panda Rep 1:1', 3000000, 64, 2),
		('NBL77VWB', N'Nike Blazer Low 77 Vintage White Black', 2000000, 63, 2),
		('GNAJ1RHBW', N'Giày Nike Air Jordan 1 Retro High Black White (Đen Trắng)', 2000000, 63, 2),


		('GAYB350V2DS', N'Giày Adidas Yeezy Boost 350 V2 Desert Sage', 2500000, 52, 3),
		('GAYB350V2TL', N'Giày Adidas Yeezy Boost 350 V2 Tail Light', 2500000, 52, 3),
		('GAY350V2YR', N'Giày Adidas Yeezy 350 V2 Yecheil Reflective', 2500000, 52, 3),
		('GAY350V2CW', N'Giày Adidas Yeezy 350 V2 Cloud White', 2500000, 52, 3),
		('GAY350V2S', N'Giày Adidas Yeezy 350 V2 Sesame', 2000000, 41, 3),
		('GAY350V2CRW', N'Giày Adidas Yeezy 350 V2 Cream White', 2000000, 41, 3),
		('GAY350V2SDFPQ', N'Giày Adidas Yeezy 350 V2 Static Đen Full Phản Quang', 2000000, 41, 3),
		('GAY350V2C', N'Giày Adidas Yeezy 350 V2 Cinder', 2000000, 41, 3),
		('GAY700V3A', N'Giày Adidas Yeezy 700 V3 Azael', 2500000, 58, 3),
		('GAY700V2V', N'Giày Adidas Yeezy 700 V2 Vanta', 2000000, 48, 3),
		('GAY700V2G', N'Giày Adidas Yeezy 700 V2 Geode', 2000000, 48, 3),
		('GAY700V2I', N'Giày Adidas Yeezy 700 V2 Inertia', 2000000, 48, 3),
		('GAY700V2T', N'Giày Adidas Yeezy 700 V2 Tephra', 2000000, 48, 3),
		('GAY700V2HB', N'Giày Adidas Yeezy 700 V2 Hospital Blue', 2000000, 48, 3),
		('GAY700M', N'Giày Adidas Yeezy 700 Magnet', 2000000, 48, 3),
		('GAY700S', N'Giày Adidas Yeezy 700 Salt', 2000000, 48, 3),
		('GAY700A', N'Giày Adidas Yeezy 700 Analog', 2000000, 48, 3),


		('GAMGDLA', N'Giày Alexander Mcqueen gót đen Like Auth', 4000000, 45, 5),
		('GAMRPQ', N'Giày Alexander Mcqueen reflective (phản quang)', 2000000, 51, 5),
		('GAMTGDR', N'Giày Alexander McQueen trắng gót đen Rep 1:1', 2000000, 51, 5),


		('GMLBBCMHBRS', N'Giày MLB Bigball Chunky Mono Heel Boston Red Sox', 2000000, 65, 7),
		('GMLBBPRSR', N'Giày MLB Boston P Red Sox Rep 1:1', 1400000, 57, 7),
		('GMLBBBCLLDBR', N'Giày MLB Big Ball Chunky LITE LA Dodgers Black Rep 1:1', 2200000, 64, 7),
		('GMLBBCLLDGR', N'Giày MLB BigBall Chunky Lite LA Dodgers Grey Rep 1:1', 2200000, 64, 7),
		('MLBBCMLNY', N'MLB Bigball Chunky Monogram LT New York Yankees Rep 1:1', 2500000, 72, 7),
		('MLBNTCVR', N'MLB NY trắng chữ vàng Rep 1:1', 1400000, 50, 7),
		('GMLBNTCDDNR', N'Giày MLB NY trắng chữ đen đế nâu rep 1:1', 1400000, 50, 7),
		('GMLBBCLNYYIR', N'Giày MLB BigBall Chunky Lite New York Yankees Ivory Rep 1:1', 2200000, 64, 7),
		('MLBPMMNYYBR', N'MLB Playball Mule Monogram New York Yankees Black Rep 1:1', 2000000, 68, 7),
		('MLBPMMNYYWR', N'MLB Playball Mule Monogram New York Yankees White Rep 1:1', 2000000, 68, 7),
		('GMLBPMMDNYYR', N'Giày MLB PlayBall Mule Mono Denim New York Yankees Rep 1:1', 2000000, 68, 7),

		
		('DBPSBFG', N'Dép Balenciaga Pool Slide Black Fluo Green', 2500000, 52, 8),
		('DBPSBWD', N'Dép Balenciaga Pool Slide Black White (Đen)', 2500000, 72, 8),
		('DBPSGX', N'Dép Balenciaga Pool Slide Grey Xám', 2500000, 72, 8),
		('DBPSL', N'Dép Balenciaga Pool Slide Lime', 2500000, 64, 8),
		('DBPSLB', N'Dép Balenciaga Pool Slide Logo Black', 2500000, 64, 8),
		('DBPSPH', N'Dép Balenciaga Pool Slide Pink (Hồng)', 2500000, 64, 8),
		('DBPSWT', N'Dép Balenciaga Pool Slide White (Trắng)', 2500000, 64, 8),
		('DGWSSBLA', N'Dép Gucci Web Slide Sandal Black Like Auth', 2500000, 64, 8)
GO

INSERT INTO tbl_CoverImage
VALUES ('GNAF1LWB', '1.jpg'),
		('GNAF1LWB', '2.jpg'),

		('NAF1TFWLA', '1.jpeg'),
		('NAF1TFWLA', '2.jpg'),

		('NKPGD', '1.jpg'),
		('NKPGD', '2.jpg'),

		('NKAF1AWBSR', '1.jpg'),
		('NKAF1AWBSR', '2.jpg'),

		('GNAF1TFWSC', '1.jpeg'),
		('GNAF1TFWSC', '2.jpg'),
		('NAF1LSF', '1.jpg'),
		('NAF1LSF', '2.jpg'),

		('GNAF1LPSR', '1.jpg'),
		('GNAF1LPSR', '2.jpg'),

		('NAF1LU', '1.jpg'),
		('NAF1LU', '2.jpg'),

		('NAF1QSVDLL', '1.jpg'),
		('NAF1QSVDLL', '2.jpg'),

		('NXAF1LXTARSR', '1.jpg'),
		('NXAF1LXTARSR', '2.jpg'),

		('NAF1LFR', '1.jpg'),
		('NAF1LFR', '2.jpg'),

		('NAF1STW', '1.jpg'),
		('NAF1STW', '2.jpg'),

		('NAFJDITR', '1.jpg'),
		('NAFJDITR', '2.jpg'),

		('NAF1LOWVR', '1.jpg'),
		('NAF1LOWVR', '2.jpg'),

		('NAF1LTSCJR', '1.jpg'),
		('NAF1LTSCJR', '2.jpg'),

		('NAF1SPI7M', '1.jpg'),
		('NAF1SPI7M', '2.jpg'),

		('NAF1VSMG', '1.jpg'),
		('NAF1VSMG', '2.jpg'),

		('NAF1SBPI', '1.jpg'),
		('NAF1SBPI', '2.jpg'),

		('NAF1SDSAC', '1.jpeg'),
		('NAF1SDSAC', '2.jpg'),

		('NAF1LTSS', '1.jpg'),
		('NAF1LTSS', '2.jpg'),

		('NAF1GDPPN', '1.jpg'),
		('NAF1GDPPN', '2.jpg'),

		('NAF1GDKE', '1.jpg'),
		('NAF1GDKE', '2.jpg'),

		('NAF1CCT', '1.jpg'),
		('NAF1CCT', '2.jpg'),

		('GNAJ1LBT', '1.jpg'),
		('GNAJ1LBT', '2.jpg'),

		('GNAJ1LPR', '1.jpg'),
		('GNAJ1LPR', '2.jpg'),

		('NBL77VWB', '1.jpg'),
		('NBL77VWB', '2.jpg'),

		('GNAJ1RHBW', '1.jpg'),
		('GNAJ1RHBW', '2.jpg'),

		('GAYB350V2DS', '1.jpg'),
		('GAYB350V2DS', '2.jpg'),

		('GAYB350V2TL', '1.jpg'),
		('GAYB350V2TL', '2.jpg'),

		('GAY350V2YR', '1.jpg'),
		('GAY350V2YR', '2.jpg'),

		('GAY350V2CW', '1.jpg'),
		('GAY350V2CW', '2.jpg'),

		('GAY350V2S', '1.jpg'),
		('GAY350V2S', '2.jpg'),

		('GAY350V2CRW', '1.jpg'),
		('GAY350V2CRW', '2.jpg'),

		('GAY350V2SDFPQ', '1.jpg'),
		('GAY350V2SDFPQ', '2.jpg'),

		('GAY350V2C', '1.jpg'),
		('GAY350V2C', '2.jpg'),

		('GAY700V3A', '1.jpg'),
		('GAY700V3A', '2.jpg'),

		('GAY700V2V', '1.jpg'),
		('GAY700V2V', '2.jpg'),

		('GAY700V2G', '1.jpg'),
		('GAY700V2G', '2.jpg'),

		('GAY700V2I', '1.jpg'),
		('GAY700V2I', '2.jpg'),

		('GAY700V2T', '1.jpg'),
		('GAY700V2T', '2.jpg'),

		('GAY700V2HB', '1.jpg'),
		('GAY700V2HB', '2.jpg'),

		('GAY700M', '1.jpg'),
		('GAY700M', '2.jpg'),

		('GAY700S', '1.jpg'),
		('GAY700S', '2.jpg'),

		('GAY700A', '1.jpg'),
		('GAY700A', '2.jpg'),

		('GAMGDLA', '1.jpg'),
		('GAMGDLA', '2.jpg'),

		('GAMRPQ', '1.jpeg'),
		('GAMRPQ', '2.jpg'),

		('GAMTGDR', '1.jpg'),
		('GAMTGDR', '2.jpg'),

		('GMLBBCMHBRS', '1.jpg'),
		('GMLBBCMHBRS', '2.jpg'),

		('GMLBBPRSR', '1.jpg'),
		('GMLBBPRSR', '2.jpg'),

		('GMLBBBCLLDBR', '1.jpg'),
		('GMLBBBCLLDBR', '2.jpg'),

		('GMLBBCLLDGR', '1.jpg'),
		('GMLBBCLLDGR', '2.jpg'),

		('MLBBCMLNY', '1.png'),
		('MLBBCMLNY', '2.jpg'),

		('MLBNTCVR', '1.jpg'),
		('MLBNTCVR', '2.jpg'),

		('GMLBNTCDDNR', '1.jpg'),
		('GMLBNTCDDNR', '2.jpg'),

		('GMLBBCLNYYIR', '1.jpg'),
		('GMLBBCLNYYIR', '2.jpg'),

		('MLBPMMNYYBR', '1.jpg'),
		('MLBPMMNYYBR', '2.jpg'),

		('MLBPMMNYYWR', '1.jpg'),
		('MLBPMMNYYWR', '2.jpg'),

		('GMLBPMMDNYYR', '1.jpg'),
		('GMLBPMMDNYYR', '2.jpg'),

		('DBPSBFG', '1.jpg'),

		('DBPSBWD', '1.jpg'),
		('DBPSBWD', '2.jpg'),

		('DBPSGX', '1.jpg'),
		('DBPSGX', '2.jpg'),

		('DBPSL', '1.jpg'),

		('DBPSLB', '1.jpg'),
		('DBPSLB', '2.jpg'),

		('DBPSPH', '1.jpg'),

		('DBPSWT', '1.jpg'),

		('DGWSSBLA', '1.jpg'),
		('DGWSSBLA', '2.jpg')
GO

INSERT INTO tbl_DetailsImage
VALUES ('GNAF1LWB', '1.jpg'),
		('GNAF1LWB', '2.jpg'),
		('GNAF1LWB', '3.jpg'),
		('GNAF1LWB', '4.jpg'),
		('GNAF1LWB', '5.jpg'),
		('GNAF1LWB', '6.jpg'),
		('GNAF1LWB', '7.jpg'),

		('NAF1TFWLA', '1.jpeg'),
		('NAF1TFWLA', '2.jpg'),
		('NAF1TFWLA', '3.jpg'),
		('NAF1TFWLA', '4.jpg'),
		('NAF1TFWLA', '5.jpg'),
		('NAF1TFWLA', '6.jpg'),
		('NAF1TFWLA', '7.jpg'),

		('NKPGD', '1.jpg'),
		('NKPGD', '2.jpg'),
		('NKPGD', '3.jpg'),
		('NKPGD', '4.jpg'),
		('NKPGD', '5.jpg'),
		('NKPGD', '6.jpg'),
		('NKPGD', '7.jpg'),

		('NKAF1AWBSR', '1.jpg'),
		('NKAF1AWBSR', '2.jpg'),
		('NKAF1AWBSR', '3.jpg'),
		('NKAF1AWBSR', '4.jpg'),
		('NKAF1AWBSR', '5.jpg'),
		('NKAF1AWBSR', '6.jpg'),
		('NKAF1AWBSR', '7.jpg'),

		('GNAF1TFWSC', '1.jpeg'),
		('GNAF1TFWSC', '2.jpg'),
		('GNAF1TFWSC', '3.jpg'),
		('GNAF1TFWSC', '4.jpg'),
		('GNAF1TFWSC', '5.jpg'),
		('GNAF1TFWSC', '6.jpg'),
		('GNAF1TFWSC', '7.jpg'),

		('NAF1LSF', '1.jpg'),
		('NAF1LSF', '2.jpg'),
		('NAF1LSF', '3.jpg'),
		('NAF1LSF', '4.jpg'),
		('NAF1LSF', '5.jpg'),
		('NAF1LSF', '6.jpg'),
		('NAF1LSF', '7.jpg'),

		('GNAF1LPSR', '1.jpg'),
		('GNAF1LPSR', '2.jpg'),
		('GNAF1LPSR', '3.jpg'),
		('GNAF1LPSR', '4.jpg'),
		('GNAF1LPSR', '5.jpg'),
		('GNAF1LPSR', '6.jpg'),
		('GNAF1LPSR', '7.jpg'),

		('NAF1LU', '1.jpg'),
		('NAF1LU', '2.jpg'),
		('NAF1LU', '3.jpg'),
		('NAF1LU', '4.jpg'),
		('NAF1LU', '5.jpg'),
		('NAF1LU', '6.jpg'),
		('NAF1LU', '7.jpg'),

		('GNAJ1LBT', '1.jpg'),
		('GNAJ1LBT', '2.jpg'),
		('GNAJ1LBT', '3.jpg'),
		('GNAJ1LBT', '4.jpg'),
		('GNAJ1LBT', '5.jpg'),
		('GNAJ1LBT', '6.jpg'),
		('GNAJ1LBT', '7.jpg'),

		('GNAJ1LPR', '1.jpg'),
		('GNAJ1LPR', '2.jpg'),
		('GNAJ1LPR', '3.jpg'),
		('GNAJ1LPR', '4.jpg'),
		('GNAJ1LPR', '5.jpg'),
		('GNAJ1LPR', '6.jpg'),
		('GNAJ1LPR', '7.jpg'),

		('NBL77VWB', '1.jpg'),
		('NBL77VWB', '2.jpg'),
		('NBL77VWB', '3.jpg'),
		('NBL77VWB', '4.jpg'),
		('NBL77VWB', '5.jpg'),
		('NBL77VWB', '6.jpg'),
		('NBL77VWB', '7.jpg'),

		('GNAJ1RHBW', '1.jpg'),
		('GNAJ1RHBW', '2.jpg'),
		('GNAJ1RHBW', '3.jpg'),
		('GNAJ1RHBW', '4.jpg'),
		('GNAJ1RHBW', '5.jpg'),
		('GNAJ1RHBW', '6.jpg'),
		('GNAJ1RHBW', '7.jpg'),

		('GAYB350V2DS', '1.jpg'),
		('GAYB350V2DS', '2.jpg'),
		('GAYB350V2DS', '3.jpg'),
		('GAYB350V2DS', '4.jpg'),
		('GAYB350V2DS', '5.jpg'),

		('GAYB350V2TL', '1.jpg'),
		('GAYB350V2TL', '2.jpg'),
		('GAYB350V2TL', '3.jpg'),
		('GAYB350V2TL', '4.jpg'),
		('GAYB350V2TL', '5.jpg'),
		('GAYB350V2TL', '6.jpg'),
		('GAYB350V2TL', '7.jpg'),

		('GAY350V2YR', '1.jpg'),
		('GAY350V2YR', '2.jpg'),
		('GAY350V2YR', '3.jpg'),
		('GAY350V2YR', '4.jpg'),
		('GAY350V2YR', '5.jpg'),
		('GAY350V2YR', '6.jpg'),

		('GAY350V2CW', '1.jpg'),
		('GAY350V2CW', '2.jpg'),
		('GAY350V2CW', '3.jpg'),
		('GAY350V2CW', '4.jpg'),
		('GAY350V2CW', '5.jpg'),
		('GAY350V2CW', '6.jpg'),
		('GAY350V2CW', '7.jpg'),

		('GAMGDLA', '1.jpg'),
		('GAMGDLA', '2.jpg'),
		('GAMGDLA', '3.jpg'),
		('GAMGDLA', '4.jpg'),
		('GAMGDLA', '5.jpg'),
		('GAMGDLA', '6.jpg'),
		('GAMGDLA', '7.jpg'),

		('GAMRPQ', '1.jpeg'),
		('GAMRPQ', '2.jpg'),
		('GAMRPQ', '3.jpg'),
		('GAMRPQ', '4.jpg'),
		('GAMRPQ', '5.jpg'),
		('GAMRPQ', '6.jpg'),
		('GAMRPQ', '7.jpg'),

		('GAMTGDR', '1.jpg'),
		('GAMTGDR', '2.jpg'),
		('GAMTGDR', '3.jpg'),
		('GAMTGDR', '4.jpg'),
		('GAMTGDR', '5.jpg'),
		('GAMTGDR', '6.jpg'),
		('GAMTGDR', '7.jpg'),

		('GMLBBCMHBRS', '1.jpg'),
		('GMLBBCMHBRS', '2.jpg'),
		('GMLBBCMHBRS', '3.jpg'),
		('GMLBBCMHBRS', '4.jpg'),
		('GMLBBCMHBRS', '5.jpg'),
		('GMLBBCMHBRS', '6.jpg'),
		('GMLBBCMHBRS', '7.jpg'),

		('GMLBBPRSR', '1.jpg'),
		('GMLBBPRSR', '2.jpg'),
		('GMLBBPRSR', '3.jpg'),
		('GMLBBPRSR', '4.jpg'),
		('GMLBBPRSR', '5.jpg'),
		('GMLBBPRSR', '6.jpg'),
		('GMLBBPRSR', '7.jpg'),

		('GMLBBBCLLDBR', '1.jpg'),
		('GMLBBBCLLDBR', '2.jpg'),
		('GMLBBBCLLDBR', '3.jpg'),
		('GMLBBBCLLDBR', '4.jpg'),
		('GMLBBBCLLDBR', '5.jpg'),
		('GMLBBBCLLDBR', '6.jpg'),
		('GMLBBBCLLDBR', '7.jpg'),

		('GMLBBCLLDGR', '1.jpg'),
		('GMLBBCLLDGR', '2.jpg'),
		('GMLBBCLLDGR', '3.jpg'),
		('GMLBBCLLDGR', '4.jpg'),
		('GMLBBCLLDGR', '5.jpg'),
		('GMLBBCLLDGR', '6.jpg'),
		('GMLBBCLLDGR', '7.jpg'),

		('DBPSBWD', '1.jpg'),
		('DBPSBWD', '2.jpg'),
		('DBPSBWD', '3.jpg'),
		('DBPSBWD', '4.jpg'),

		('DBPSGX', '1.jpg'),
		('DBPSGX', '2.jpg'),
		('DBPSGX', '3.jpg'),

		('DBPSLB', '1.jpg'),
		('DBPSLB', '2.jpg'),
		('DBPSLB', '3.jpg'),

		('DBPSWT', '1.jpg'),
		('DBPSWT', '2.jpg'),
		('DBPSWT', '3.jpg'),

		('DGWSSBLA', '1.jpg'),
		('DGWSSBLA', '2.jpg'),
		('DGWSSBLA', '3.jpg'),
		('DGWSSBLA', '4.jpg'),
		('DGWSSBLA', '5.jpg'),
		('DGWSSBLA', '6.jpg'),
		('DGWSSBLA', '7.jpg')
GO

INSERT INTO tbl_Size
VALUES (38),(39),(40),(41),(42),(43)
GO

INSERT INTO tbl_Inventory
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

-- ============================================================================= STORE PROCEDURE =============================================================================

-- Procedure lấy mã loại bằng tên gần giống
CREATE PROC sp_GetIDSneaker
@name nvarchar(max)
AS
	select ID from tbl_SneakerType where Name like '%' + @name + '%'
go

--exec sp_GetIDSneaker 'NIKE'


CREATE PROC sp_GetAmountInventorySneaker
@idSneaker varchar(20)
AS
	declare @amount int = 0
	if (select count(*) from tbl_Inventory where IDSneaker = @idSneaker) > 0
		select @amount = SUM(Amount) from tbl_Inventory where IDSneaker = @idSneaker
	return @amount
go

--declare @sl int
--exec @sl = sp_GetAmountInventorySneaker 'GNAF1LWB'
--select @sl

CREATE PROC sp_GetSizeInventory
@idSneaker varchar(20)
AS
	declare @amount int = 0
	if (select count(*) from tbl_Inventory where IDSneaker = @idSneaker) > 0
		select Size from tbl_Size, tbl_Inventory where tbl_Inventory.IDSize = tbl_Size.ID and IDSneaker = @idSneaker
	else select @amount
go

--exec sp_GetSizeInventory 'GNAF1LWB'


alter PROC sp_AddAccountClient
@name nvarchar(max), @username varchar(100), @password varchar(30), @phoneClient varchar(11), @dateOfBirth date, @gender nvarchar(3), @idWard int
AS
	if(select count(*) from tbl_Client where Phone = @phoneClient) > 0
		return 0
	if (select count(*) from tbl_Account where Username = @username) > 0
		return 0
	insert into tbl_Client (Name, DateOfBirth, Gender, Phone, IDWard)
	values (@name, @dateOfBirth, @gender, @phoneClient, @idWard)

	insert into tbl_Account
	values (@username, @password)

	declare @idClient int, @idAccount int
	select @idClient = ID from tbl_Client where Phone = @phoneClient
	select @idAccount = ID from tbl_Account where Username = @username
	insert into tbl_Account_Client (IDAccount, IDClient)
	values (@idAccount, @idClient)
	return 1
go

--declare @result int
--exec sp_AddAccountClient '', '', '', ''
--select @result

CREATE PROC sp_AccountLogin
@username varchar(max), @password varchar(30)
AS
	declare @idAccount int
	select @idAccount = ID from tbl_Account where Username = @username and Password = @password
	if @idAccount is null
		return 0
	declare @id int
	if (select count(*) from tbl_Account_Client where IDAccount = @idAccount) > 0
		select @id = IDClient from tbl_Account_Client where IDAccount = @idAccount
	else if (select count(*) from tbl_Account_Staff where IDAccount = @idAccount) > 0
			select @id = IDStaff from tbl_Account_Staff where IDAccount = @idAccount
	return @id
GO

--declare @id int
--exec @id = sp_AccountLogin 'cvt', 'cvtadmin'
--print convert(varchar(max), @id)

CREATE PROC sp_GetIDAccount
@username varchar(max), @password varchar(30)
AS
	declare @idAccount int
	select @idAccount = ID from tbl_Account where Username = @username and Password = @password
	if @idAccount is null
		return 0
	return @idAccount
GO

--declare @idAccount int
--exec @idAccount = sp_GetIDAccount 'cvt','cvtadmin'
--print @idAccount

CREATE PROC sp_GetIDClientByIDAccount
@idAccount int
AS
	declare @id int
	select @id = IDClient from tbl_Account_Client where IDAccount = @idAccount
	if @id is null
		return 0
	return @id
GO

--declare @id int
--exec @id = sp_GetIDClientByIDAccount 4
--print convert(varchar(max), @id)
CREATE PROC sp_GetIDStaffByIDAccount
@idAccount int
AS
	declare @id int
	select @id = IDStaff from tbl_Account_Staff where IDAccount = @idAccount
	if @id is null
		return 0
	return @id
GO
--declare @id int
--exec @id = sp_GetIDStaffByIDAccount 1
--print convert(varchar(max), @id)



--declare @idAccount int
--select @idAccount = ID from tbl_Account where Username = '' and Password = ''
--if @idAccount is null
--	print 'true'
--print convert(varchar(max), @idAccount)

-- ============================================================================= QUERY TEST =============================================================================

--Select * from tbl_Sneaker, tbl_SizeDetails, tbl_Size where tbl_Sneaker.ID = tbl_SizeDetails.IDSneaker and tbl_SizeDetails.IDSize = tbl_Size.ID


select * from Ward, City, District where Ward.IDDistrict = District.ID and District.IDCity = City.ID and City.Name like N'%Trà Vinh%'

select * from tbl_Account
select * from tbl_Staff
select * from tbl_Account_Staff

--select * from tbl_Sneaker, tbl_SneakerType where tbl_SneakerType.Name like '%Luxury%' and LinkPictureDetails != ''

--select Link from tbl_CoverImage where tbl_CoverImage.IDSneaker = 'NAF1TFWLA'
--select Link from tbl_DetailsImage where tbl_DetailsImage.IDSneaker = 'NAF1TFWLA'

--select Size from tbl_Size
--select Size from tbl_Size, tbl_Inventory where tbl_Inventory.IDSize = tbl_Size.ID and IDSneaker = 'GNAF1LPSR'

select * from tbl_Sneaker where ID = 'GNAF1LWB'

select * from City, District, Ward where City.ID = District.IDCity and District.ID = Ward.IDDistrict and Ward.ID = 26773
