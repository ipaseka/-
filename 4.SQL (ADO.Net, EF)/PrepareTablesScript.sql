create table CustomerType
(
	Id int not null identity primary key,
	Name nvarchar(128) not null unique
)
create table Customer
(
	Id int not null identity primary key,
	TypeId int not null references CustomerType(Id),
	Surname nvarchar(256) not null,
	[Name] nvarchar(128) null,
	FatherName nvarchar(128) null,
	IdentCode nvarchar(10) null check(IdentCode is null or len(IdentCode) in (8, 10)),
	DateOfBorn datetime null check(DateOfBorn is null or DateOfBorn < getdate()),
	DateCreate datetime not null default(getdate())
)
create table Seller
(
	Id int not null identity primary key,
	Surname nvarchar(256) not null,
	[Name] nvarchar(128) null,
	FatherName nvarchar(128) null
)
create table [Status]
(
	Id int not null identity primary key,
	Name nvarchar(128) not null unique
)
create table Contract
(
	Id int not null identity primary key,
	Number nvarchar(64) null unique,
	DateSign datetime not null check(DateSign <= getdate()),
	DateBegin datetime not null,
	DateEnd datetime not null,
	Payment money not null check(Payment > 0),
	CustomerId int not null references Customer(Id),
	SellerId int not null references Seller(Id),
	DateCreate datetime not null default(getdate())
)
create table StatusHistory
(
	Id int not null identity primary key,
	StatusId int not null references [Status](Id),
	ContractId int not null references Contract(Id),
	DateCreate datetime not null default(getdate())
)