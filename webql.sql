create database QLSP
use QLSP
Create table ProductCatalog
(
	ID int primary key identity(101,1),
	Name varchar(20) not null
);

Create table Brand
(
	ID int primary key identity(1,1),
	Name varchar(20) not null
);

create table Product
(
	Prod_ID char(10),
	Prod_Name varchar(100) not null,
	Prod_Price_Out decimal(18,2) not null,
	Prod_Price_In decimal(18,2) not null,
	UrlImg varchar(200) not null,
	Quantity int not null,
	Catalog_ID int not null,
	Brand_ID int not null,
	isDelete bit not null default (0),
	Primary key (Prod_ID),
	Foreign key (Catalog_ID) references ProductCatalog(ID),
	Foreign key (Brand_ID) references Brand(ID)
);

create table Supplier
(
	Sup_ID char(10) not null,
	Sup_Name nvarchar(100),
	Sup_Address nvarchar(200),
	Sup_Phone nvarchar(20),
	isDelete bit not null default (0),
	Primary Key (Sup_ID)	
);
Create table Employee
(
	Emp_ID int identity(208001,1),
	Emp_Name varchar(50) not null,
	Emp_Phone varchar(10) not null,
	Primary key (Emp_ID),
);


create table InvoiceIn
(
	Inv_ID char(10),
	Inv_DateIn date not null,
	Sup_ID char(10) not null,
	isDelete bit not null default (0),
	Primary key (Inv_ID),
	Foreign key (Sup_ID) references Supplier(Sup_ID),
	Emp_ID int  Foreign key references Employee(Emp_ID),	
)

create table Customer
(
	Cus_ID int identity(1001,1),
	Cus_Name varchar(20) not null,
	Cus_Phone char(10) not null,
	isDelete bit not null default (0),
	primary key (Cus_ID)
);

Create table InvoiceOut
(
	Inv_ID char(10),
	Inv_DateOut date not null,
	Cus_ID int not null,
	isDelete bit not null default (0),
	Primary key (Inv_ID),
	Foreign key (Cus_ID) references Customer(Cus_ID),	
	Emp_ID int  Foreign key references Employee(Emp_ID),	
);
create table DetailInvoiceIn
(
	ID int identity(1,1),
	Inv_ID char(10) not null,
	Prod_ID char(10) not null,
	Quantity int not null,
	Inv_Total decimal(18,2) not null,
	Primary key (ID),
	Foreign key (Inv_ID) references InvoiceIn(Inv_ID), 
	Foreign key (Prod_ID) references Product(Prod_ID)
);

create table DetailInvoiceOut
(
	ID int identity(1,1),
	Inv_ID char(10) not null,
	Prod_ID char(10) not null,
	Quantity int not null,
	Inv_Total decimal(18,2) not null,
	Primary key (ID),
	Foreign key (Inv_ID) references InvoiceOut(Inv_ID), 
	Foreign key (Prod_ID) references Product(Prod_ID)
);
create table Account
(
	username varchar(50) primary key,
	pass varchar(50) not null,
	Emp_ID int  Foreign key references Employee(Emp_ID),	
)


insert into Account values('admin','1',null)

insert into Brand values('Iphone')
insert into Brand values('Samsung')
insert into Brand values('Dell')
insert into Brand values('Asus')
insert into Brand values('Lenovo')
insert into Brand values('Xioami')

insert into ProductCatalog values('Dien thoai')
insert into ProductCatalog values('Laptop')
insert into ProductCatalog values('Tai nghe')
insert into ProductCatalog values('Sac')
insert into ProductCatalog values('Pin du phong')


