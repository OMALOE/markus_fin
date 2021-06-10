-- проверка товара на наличие
drop TRIGGER if exists availability_check;
DELIMITER //
CREATE TRIGGER availability_check
BEFORE INSERT
ON order_products FOR EACH ROW
BEGIN
    select products.available into @available from products 
		where products.ID = new.productid;
    IF @available < new.amount THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Not enough products in stock';
	ELSE
		UPDATE products 
		SET products.available = products.available - new.amount 
			where products.id = new.productid;
	END IF;
END//
DELIMITER ;


-- проверка на существование аккаунта
drop TRIGGER if exists new_customer;
DELIMITER //
CREATE TRIGGER new_customer
BEFORE INSERT
ON customers FOR EACH ROW
BEGIN
    IF new.email in (select email from customers) then
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Email already used';
	elseif new.phone in (select phone from customers) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Phone already used';
	END IF;
END//
DELIMITER ;


-- проверка на существование промокода с таким же названием
drop TRIGGER if exists new_promocode;
DELIMITER //
CREATE TRIGGER new_promocode
BEFORE INSERT
ON promocodes FOR EACH ROW
BEGIN
    IF new.name in (select promocodes.name from promocodes where promocodes.companyid = new.companyid) then
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Promocode with this name already exists';
	END IF;
END//
DELIMITER ;


-- самый частый в заказах (компания)
drop procedure if exists company_most_ordered;
DELIMITER //
create procedure company_most_ordered (company varchar(64))
BEGIN
    SELECT public_name, productid, products.fullname, COUNT(*) AS ordered_times, price
	FROM orders join order_products on order_products.orderid = orders.id 
		join products on order_products.productid = products.id 
		join companies on products.companyid = companies.id
    WHERE orders.isPaid = 1 and products.companyid = company
	GROUP BY productid
	ORDER BY ordered_times DESC, price DESC
	LIMIT 1;
END//
DELIMITER ;


-- больше всего продано (компания)
drop procedure if exists company_most_saled;
DELIMITER //
create procedure company_most_saled (company varchar(64))
BEGIN
    SELECT public_name, productid, products.fullname, SUM(amount) AS ordered_items, price
	FROM orders join order_products on order_products.orderid = orders.id 
    join products on order_products.productid = products.id 
    join companies on products.companyid = companies.id
    WHERE orders.isPaid = 1 and products.companyid = company
	GROUP BY productid
	ORDER BY ordered_items DESC, price DESC
	LIMIT 1;
END//
DELIMITER ;


-- выручка за день (компания, день) 
drop procedure if exists company_revenue_day;
DELIMITER //
create procedure company_revenue_day (company varchar(64), sday timestamp)
BEGIN
    SELECT public_name, companyid, SUM(total_price) AS revenue_day, count(*) AS orders
	FROM orders join order_products on order_products.orderid = orders.id 
		join products on order_products.productid = products.id 
        join companies on products.companyid = companies.id
    WHERE orders.isPaid = 1 
		and products.companyid = company 
        and date(orders.order_date) = sday;
END//
DELIMITER ;

    
-- отчет по товарам за всё время (компания) 
drop procedure if exists company_sold_products;
DELIMITER //
create procedure company_sold_products (company varchar(64))
BEGIN
    SELECT public_name, productid, products.fullname, 
		SUM(total_price) AS revenue, SUM(amount) AS number_sold
	FROM orders join order_products on order_products.orderid = orders.id 
		join products on order_products.productid = products.id 
        join companies on products.companyid = companies.id
    WHERE orders.isPaid = 1 
		and products.companyid = company
	GROUP BY productid
	ORDER BY revenue DESC;
END//
DELIMITER ;

SET GLOBAL log_bin_trust_function_creators = 1;
-- сумма заказа (номер заказа)
drop function if exists order_sum;
DELIMITER //
create function order_sum (norder varchar(64))
returns decimal(10,2)
BEGIN
    declare sum decimal(10,2);
    select SUM(total_price)	
		FROM orders join order_products on order_products.orderid = orders.id 
        and order_products.orderid = norder
		into sum;
	return sum;
END//
DELIMITER ;


-- все заказы (покупатель) // процедуры в mysql не возвращают таблицы
drop procedure if exists customer_all_orders;
DELIMITER //
create procedure customer_all_orders (customer varchar(64))
BEGIN
    SELECT DISTINCT firstname, lastname, order_number, order_date, order_sum(orderid) as order_sum
	FROM orders join order_products on order_products.orderid = orders.id 
		join customers on orders.customerid = customers.id
		and customers.id = customer
	ORDER BY order_date DESC;
END//
DELIMITER ;


-- возвращает в сток товары из удаленного заказа
drop TRIGGER if exists after_order_product_delete;
DELIMITER //
CREATE TRIGGER after_order_product_delete
AFTER DELETE
ON order_products FOR EACH ROW
BEGIN
	declare products_no int;
    UPDATE products
		SET available = available + old.amount
		where products.id = old.productid;
        
	select count(*) into products_no from order_products 
		where orderid = OLD.orderid;
    IF products_no = 0 THEN
		delete from orders where id = OLD.orderid;
	END IF;
END//
DELIMITER ;


-- меняет запас товара после изменения 
drop TRIGGER if exists after_order_product_update;
DELIMITER //
CREATE TRIGGER after_order_product_update
AFTER UPDATE
ON order_products FOR EACH ROW
BEGIN
	select products.available into @available from products 
		where products.ID = new.productid;
    IF @available < new.amount THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Out of stock, cannot update';
	ELSE
		UPDATE products 
		SET products.available = products.available + (old.amount - new.amount)
			where products.id = new.productid;
	END IF;
END//
DELIMITER ;

