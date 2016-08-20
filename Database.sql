
CREATE DATABASE [RestaurantDB];

/*Table Categories*/
CREATE  TABLE Categories (
  Id NCHAR(38) NOT NULL DEFAULT newid(),
  CategoryName NVARCHAR(45) NOT NULL ,
  PRIMARY KEY (Id) );



/*Table Foods*/
CREATE  TABLE Foods (
  Id NCHAR(38) NOT NULL DEFAULT newid(),
  Category_Id NCHAR(38) NOT NULL ,
  FoodName NVARCHAR(64) NOT NULL ,
  Price FLOAT NULL DEFAULT 0 ,
  PRIMARY KEY (Id) ,
  CONSTRAINT fk_Foods_Categories1
    FOREIGN KEY (Category_Id )
    REFERENCES Categories (Id )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);



/*Table Customers*/
CREATE  TABLE Customers (
  Id NCHAR(38) NOT NULL DEFAULT newid(),
  CustomerId INT NOT NULL ,
  FirstName NVARCHAR(256) NOT NULL ,
  LastName NVARCHAR(256) NOT NULL ,
  PhoneNumber NVARCHAR(20)  NULL ,
  Address NVARCHAR(1024) NULL ,
  PRIMARY KEY (Id) );



/*Table Orders*/
CREATE  TABLE Orders (
  Id NCHAR(38) NOT NULL DEFAULT newid(),
  Customer_Id NCHAR(38) NOT NULL ,
  OrderDate DATETIME NULL ,
  Discount TINYINT NULL ,
  OrderType TINYINT NOT NULL ,
  DeliveryFee FLOAT NOT NULL DEFAULT 0,
  Status TINYINT NULL ,
  PRIMARY KEY (Id) ,
  CONSTRAINT fk_Orders_Customers1
    FOREIGN KEY (Customer_Id )
    REFERENCES Customers (Id )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);



/*Table Foods_Orders*/
CREATE  TABLE Foods_Orders (
  Food_Id NCHAR(38) NOT NULL ,
  Order_Id NCHAR(38) NOT NULL ,
  Quantity INT NULL ,
  PRIMARY KEY (Food_Id, Order_Id) ,
  CONSTRAINT fk_Foods_Orders_Foods
    FOREIGN KEY (Food_Id )
    REFERENCES Foods (Id )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_Foods_Orders_Orders1
    FOREIGN KEY (Order_Id )
    REFERENCES Orders (Id )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);




/*Table Employees*/
CREATE  TABLE Employees (
  Id NCHAR(38) NOT NULL DEFAULT newid(),
  FirstName NVARCHAR(256) NOT NULL ,
  LastName NVARCHAR(256) NOT NULL ,
  Username NVARCHAR(256) NOT NULL ,
  Password NVARCHAR(256) NOT NULL ,
  Role TINYINT NOT NULL ,
  PRIMARY KEY (Id) );

  
  
