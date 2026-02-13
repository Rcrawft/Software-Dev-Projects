#Ryan Crawford
#This file contains the DDL and DML for the Bookstore Database. 
#This database keeps track of all available books in the book store, along with
#information about each book such as author, genre, etc. Employees, customers, orders,
#and placed orders along with relevant information for each are also all recorded 
#by the database, making it easy to query information related to each of these related
#entities. 

#drops tables
set foreign_key_checks = 0;
drop table Book_t;
drop table Order_t;
drop table Customer_t;
drop table Employee_t;
drop table Placed_Order_t;
set foreign_key_checks = 1;

/********** DDL **********/

#creates table for books
CREATE TABLE Book_t 
(
	#book ID primary key
    Book_ID integer    not null,
    Book_Title  varchar(50),
    Author_Name VARCHAR(25),
    Genre VARCHAR(25),
    Publish_Date DATE,
    Publisher VARCHAR(50),
    Edition VARCHAR(50),
constraint Book_PK primary key (Book_ID));

#creates table for employees
create table Employee_t
#
(
 #employee ID primary key
 Employee_ID		integer		not null,
 Employee_Name		varchar(25),
 Employee_Address   varchar(50),
 Employee_Email     varchar(50),
constraint Employee_PK primary key (Employee_ID));

#creates table for customers
create table Customer_t
(
 #customer ID primary key
 Customer_ID		integer		not null,
 Customer_Name		varchar(25),
 Customer_Address   varchar(30),
	Customer_City        VARCHAR(20)    ,              
	Customer_State       CHAR(2)         ,
	Customer_PostalCode  VARCHAR(10)    ,
 Customer_Email		varchar(50),
constraint Customer_PK primary key (Customer_ID));

#creates table for orders
CREATE TABLE Order_t (
    #order ID primary key
    Order_ID INTEGER not null,
    #employee ID from Employee_t foreign key 1
    Employee_ID INTEGER not null,
    #customer ID from Customer_t foreign key 2
    Customer_ID INTEGER not null,
constraint Order_PK primary key (Order_ID),
constraint Order_FK1 foreign key (Employee_ID) references Employee_t(Employee_ID),
constraint Order_FK2 foreign key (Customer_ID) references Customer_t(Customer_ID));

#creates table for placed orders
CREATE TABLE Placed_Order_t (
    #order ID primary key, foreign key 1
    Order_ID INTEGER not null,
	#book ID primary key, foreign key 2
    Book_ID INTEGER not null,
    Order_Date DATE,
constraint Placed_Order_PK primary key (Order_ID, Book_ID),
constraint Placed_Order_FK1 FOREIGN KEY (Order_ID) REFERENCES Order_t(Order_ID),
constraint Placed_Order_FK2 FOREIGN KEY (Book_ID) REFERENCES Book_t(Book_ID));
