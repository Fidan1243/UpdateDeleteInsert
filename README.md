# UpdateDeleteInsert
# UpdateDeleteInsert
Create Database ShopDb
USE ShopDb

create table Products(
Id int primary key identity(1,1) Not Null,
ProductName nvarchar(max) Not Null,
Price int not null
)
create table Customers(
Id int primary key identity(1,1) Not Null,
FirstName nvarchar(30) Not Null,
LastName nvarchar(30) Not Null
)
create table Orders(
Id int primary key identity(1,1) Not Null,
ProductId int foreign key references Products(Id) Not Null,
CustomerId int foreign key references Customers(Id) Not Null
)
create table OrderDetails(
Id int primary key identity(1,1) Not Null,
OrderQuantity int Not Null,
DateOfOrder datetime default(GetDate()),
OrderId int foreign key references Orders(Id) Not Null
)

aLTER Procedure InsertProducts
@ProductName nvarchar(max),
@Price nvarchar(max)
as
Begin

Insert Into Products(ProductName,Price)
Values (@ProductName,@Price)

End
------------------------------------------
aLTER Procedure InsertCustomers
@FirstName nvarchar(30),
@LastName nvarchar(30)
as
Begin

Insert Into Customers(FirstName,LastName)
Values (@FirstName,@LastName)

End

----------------------------------------------

aLTER Procedure InsertOrderDetails
@OrdersQuantity int,
@OrderId int
as
Begin

Insert into OrderDetails(OrderQuantity,OrderId,DateOfOrder)
Values (@OrdersQuantity,@OrderId,GETDATE())

End

-----------------------------------------------
-----------------------------------------------
Alter Procedure UpdateOrderDetails
@OrdersQuantity int,
@DateOfOrder nvarchar(30),
@Id int
as
Begin

Update OrderDetails
Set OrderDetails.OrderQuantity = @OrdersQuantity,DateOfOrder = Cast(@DateOfOrder as datetime)
Where OrderDetails.Id = @Id

End

---------------------------------------------

Create Procedure UpdateProducts
@ProductName nvarchar(max),
@Price nvarchar(max),
@Id int
as
Begin

Update Products
Set ProductName = @ProductName,Price = @Price
Where Id = @Id

End

-------------------------------------------------

Create Procedure UpdateCustomers
@FirstName nvarchar(30),
@LastName nvarchar(30),
@Id int
as
Begin

Update Customers
Set FirstName = @FirstName,LastName = @LastName
Where Id = @Id

End

--------------------------------------------

Create Procedure UpdateOrders
@ProductId int ,
@CustomerId int,
@Id int
as
Begin

Update Orders
Set ProductId = @ProductId,CustomerId = @CustomerId
Where Id = @Id

End

-----------------------------------------------
-----------------------------------------------

Create Procedure DeleteOrderDetails
@Id int
as
Begin

Delete OrderDetails
Where OrderDetails.Id = @Id

End

---------------------------------------------

Create Procedure DeleteProducts
@Id int
as
Begin

Delete Products
Where Id = @Id

End

-------------------------------------------------

Create Procedure DeleteCustomers
@Id int
as
Begin

Delete Customers
Where Id = @Id

End

--------------------------------------------

Create Procedure DeleteOrders
@Id int
as
Begin

Delete Orders
Where Id = @Id

End

