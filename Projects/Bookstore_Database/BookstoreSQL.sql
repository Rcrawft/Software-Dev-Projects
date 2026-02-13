#Ryan Crawford
#This file contains the SQL for the Bookstore Database, including 
#ten queries for the database.
#This database keeps track of all available books in the book store, along with
#information about each book such as author, genre, etc. Employees, customers, orders,
#and placed orders along with relevant information for each are also all recorded 
#by the database, making it easy to query information related to each of these related
#entities. The employees and customers used are example data.

/********** SQL **********/

#shows all horror books available
select * from book_t
where genre = 'Horror';

#shows all available books written by Joe Abercrombie
select * from book_t
where author_name = 'Joe Abercrombie';

#shows all books written before 2000, from oldest to newest
select * from book_t 
where publish_date < '2000-1-1'
order by publish_date asc;

#shows all available books with special editions 
select * from book_t
where edition not like '%First%';

#shows all orders handled by Employee 1
select order_id, customer_id
from order_t 
where employee_id = 1;

#shows only orders with more than one book, and total number ordered
select order_id, order_date, count(order_id) as 'Total Ordered'
from placed_order_t
group by order_id
having count(order_id) > 1;

#shows all fantasy books that were ordered, ordered by order_id in ascending order
select b.book_id, b.book_title, order_id
from book_t b, placed_order_t p
where b.book_id = p.book_id and genre = 'Fantasy'
order by order_id asc;

#shows all books ordered by Customer 2, ordered by date in ascending order
select b.book_id, book_title, o.order_id, order_date
from book_t b, customer_t c, order_t o, placed_order_t p
where b.book_id = p.book_id and c.customer_ID = o.customer_ID and o.order_ID = p.order_ID 
and c.customer_ID = 2
order by order_date asc;

#shows all books ordered that are not fantasy books and who ordered them,
#ordered by date in ascending order
select b.book_id, book_title, o.order_id, order_date, c.customer_id, customer_name
from book_t b, order_t o, placed_order_t p, customer_t c
where b.book_id = p.book_id and c.customer_id = o.customer_id and o.order_id = p.order_id
and genre not like 'Fantasy'
order by order_date asc;

#shows the different genres of books purchased and the total number of each genre purchased
select genre, count(p.book_id) as 'Books Ordered'
from book_t b, placed_order_t p
where b.book_id = p.book_id 
group by genre;
