CREATE TABLE Pizza.PizzaReceipe(

    
    PizzaID int not null IDENTITY (1,1),
    CrustID int not null,
    SizeID int not null,
    PizzaDescription NVARCHAR(175) not null,
    PizzaCost NUMERIC(3,2)
);
CREATE TABLE Pizza.PizzaTable(

    
    PizzaID int not null IDENTITY (1,1),
    PizzaDescription NVARCHAR(175) not null,
    PizzaCost NUMERIC(3,2)
);
alter table Pizza.PizzaTable
    add CONSTRAINT PK_PizzaTable_PizzaID PRIMARY KEY (PizzaID);


CREATE TABLE Pizza.Crust(

    CrustID int not null IDENTITY (1000,1),
    CrustName nvarchar (50) not null,
    CrustCost NUMERIC(3,2) not null
);

CREATE TABLE Pizza.Size(

    SizeID int not null IDENTITY (100,1),
    SizeName nvarchar (50) not null,
    SizeCost NUMERIC(3,2) not null
);

CREATE TABLE Pizza.Topping(

    ToppingID int not null IDENTITY (10,1),
    ToppingName nvarchar (50) not null,
    ToppingCost NUMERIC(3,2) not null
);

alter table Pizza.Crust
    add CONSTRAINT PK_CrustID PRIMARY KEY (CrustID);
    
alter table Pizza.Size
    add CONSTRAINT PK_SizeID PRIMARY KEY (SizeID);

alter table Pizza.Topping
    add CONSTRAINT PK_ToppingID PRIMARY KEY (ToppingID);     

alter table Pizza.PizzaReceipe
    add CONSTRAINT PK_PizzaID PRIMARY KEY (PizzaID);

alter table Pizza.PizzaReceipe
    add CONSTRAINT FK_Pizza_Crust FOREIGN KEY (CrustID) REFERENCES Pizza.Crust(CrustID);               

alter table Pizza.PizzaReceipe
    add CONSTRAINT FK_Pizza_Size FOREIGN KEY (SizeID) REFERENCES Pizza.Size(SizeID); 

CREATE table PizzaOrder.OrderTable(

    OrderID int not null IDENTITY (100,1),
    LocationID int not null,
    TotalCost NUMERIC (3,2) not null,
    UserID int not null
);

alter table PizzaOrder.OrderTable
    add CONSTRAINT PK_OrderID PRIMARY KEY (OrderID);
          
create table PizzaLocation.LocationAddress(

    AddressID int not null IDENTITY(300,1),
    StreetName NVARCHAR(50) not null,
    City NVARCHAR(25) not null,
    StateProvince NVARCHAR (20) not null,
    Zipcode int not null
);
create table PizzaLocation.PizzaShopLocation(
    LocationID int not null IDENTITY (200,1),
    AddressID int not null,

);

alter table PizzaLocation.LocationAddress
    add CONSTRAINT PK_AddressID PRIMARY KEY (AddressID);
    
alter table PizzaLocation.PizzaShopLocation
    add CONSTRAINT PK_PizzaShopLocationID PRIMARY KEY (LocationID);

alter table PizzaLocation.PizzaShopLocation
    add CONSTRAINT FK_Location_Address FOREIGN KEY (AddressID) REFERENCES PizzaLocation.LocationAddress (AddressID);   

create table Users.UserTable(
    UserID int not null IDENTITY(500,1),
    Username NVARCHAR(50) not null,
    UserPassword NVARCHAR(50) not null,
    UserEmail NVARCHAR(50) not null UNIQUE
);

alter table Users.UserTable
    add CONSTRAINT PK_UserID PRIMARY KEY (UserID);

alter table PizzaOrder.OrderTable
    add CONSTRAINT FK_Order_User FOREIGN KEY (UserID) REFERENCES Users.UserTable (UserID);  

alter table PizzaOrder.OrderTable
    add CONSTRAINT FK_Order_Location FOREIGN KEY (LocationID) REFERENCES PizzaLocation.PizzaShopLocation (LocationID);   

 create table PizzaOrder.PizzasInOrder(
    PizzaID int not null,
    OrderID int not null,
);

alter table PizzaOrder.PizzasInOrder
    add CONSTRAINT FK_Order_ID FOREIGN KEY (OrderID) REFERENCES PizzaOrder.OrderTable (OrderID);  


alter table PizzaOrder.PizzasInOrder
    add CONSTRAINT FK_Pizza_ID FOREIGN KEY (PizzaID) REFERENCES Pizza.PizzaReceipe (PizzaID);  

alter table PizzaOrder.PizzasInOrder
    add CONSTRAINT PK_OrderID_PizzaID PRIMARY KEY (OrderID,PizzaID);

  alter table [PizzaOrder].[OrderTable] add PizzaCount int not null
  alter table  [PizzaOrder].[PizzasInOrder] Drop CONSTRAINT FK_Pizza_ID

alter table PizzaOrder.PizzasInOrder
    add CONSTRAINT FK_PizzaTable_ID FOREIGN KEY (PizzaID) REFERENCES Pizza.PizzaTable (PizzaID);  
alter table [PizzaOrder].[OrderTable] add OrderTotalCost NUMERIC (5,5)
