

Create table Customer
(
CustName varchar(50) not null,
CustEmailId varchar(20) not null Primary Key,
[Password] varchar(20) not null,
ContactNo numeric(10,0) not null,
BuildingNo varchar(10) not null,
StreetNo varchar(10) not null,
Locality varchar(20) not null,
PinCode numeric(6,0) not null
)
go

create table [Address]
(
[AddressId] int identity(1,1) primary key,
EmailId varchar(20) not null,
[Address] varchar(50) not null
)
go

create table ScheduledPickUp
(
Id int identity primary Key,
EmailId varchar(20) not null foreign key references [Customer](CustEmailId),
Pickup_PinCode numeric(6,0) not null,
delivery_PinCode numeric(6,0) not null,
ShipmentType varchar(30) check (ShipmentType in('Heavy','Perishable','Fragile')) not null,
PackageLen numeric not null,
PackageBreadth numeric not null,
PackageHeight numeric not null,
PackageWeight numeric not null,
PackingReq smallint check (PackingReq in('0','1')) not null,
DeliveryOpt varchar(30) check(DeliveryOpt in('Overnight','Express','Standard')) not null,
TimeSlot datetime not null,
[Pickup_Address] varchar(50) not null,
[Delivery_Address] varchar(50) not null,
Insurance smallint check (Insurance in('0','1')) not null,
Amount decimal(10,2) not null,
Payment varchar(10) check(Payment in('card','cash')) not null,
[AWB Number] bigint foreign key references ReceivePackage([AWB Number])
)
go

CREATE TABLE [CardDetails](
	[CardNumber] [numeric](16, 0) primary key NOT NULL,
	[NameOnCard] [varchar](40) NOT NULL,
	[CardType] [char](6) NOT NULL,
	[CVVNumber] [numeric](3, 0) NOT NULL,
	[ExpiryDate] [date] NOT NULL,
	[Balance] [decimal](10, 2) NULL
	)
	go

create table savedCards
(
EmailId varchar(20) not null,
CardNumber numeric(16,0) not null foreign key references [CardDetails]([CardNumber]),
primary key(EmailId,CardNumber)
)
go

create table AvailableDeliveryPickup
(
Pincode numeric(6,0) not null primary key,
City varchar(30) not null,
)
go

create table orders
(
OrderNo int identity primary key ,
[AWB Number] bigint ,
EmailId varchar(20) not null foreign key references Customer(CustEmailId),
DeliveryAddress varchar(50) not null,
[status] varchar(30) check(status in('detached','shipped','delivered')) not null
)
go

--create table OrderHistory
--(
--OrderNo int not null foreign key references Orders(OrderNo),
--EmailId varchar(20) not null foreign key references Customer(CustEmailId),
--[DelAddress] varchar(50) not null,
--[AWB Number] bigint ,
--[status] varchar(30) not null
--)
--go

create table FeedBack
(
EmailId varchar(20) not null foreign key references Customer(CustEmailId),
FeedbackType varchar(20) check (FeedbackType in('Complaint','Appreciation')) not null,
comments varchar(50),
primary key(EmailId,FeedbackType)
)
go

create table BranchOfficer
(
BOEmailId varchar(20) primary key,
Password varchar(10) not null,

)
go

create table ReceivePackage
(
[AWB Number] bigint identity(100000,1) primary key,
CustomerName varchar(50) not null,
FromLocation varchar(20) not null,
ToAddress varchar(50) not null,
UpdatedStatus varchar(10) not null check (UpdatedStatus in('Delivered','Not Delivered','PickUp','PickUp Failed'))
)
go


create table Pick_Del_Representatives
(
RepresentativeId int identity primary key,
Task_Assign varchar(20) check (Task_Assign in('PickUp','Delivery')),
TaskId int,
PickUpAdd varchar(50),
DeliveryAdd varchar(50),
AWBNumber bigInt
)
go
