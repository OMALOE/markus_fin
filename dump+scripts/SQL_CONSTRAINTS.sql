alter table products add constraint check_available_more_than_zero check(available >= 0);
alter table products add constraint check_price_more_than_zero check(price >= 0);
alter table products add constraint check_views_more_than_zero check(views >= 0);

alter table order_products add constraint check_amount_more_than_zero check(amount > 0);

ALTER TABLE orders ADD CONSTRAINT check_order_status CHECK (order_status in ('sent', 'assembly', 'processing', 'delivered'));
ALTER TABLE companies ADD CONSTRAINT check_delivery_region CHECK (delivery_region in ('country', 'city', 'global'));
ALTER TABLE company_admins ADD CONSTRAINT check_admin_role CHECK (role in ('owner', 'admin'));
ALTER TABLE companies ADD CONSTRAINT check_company_type CHECK (legal_type in ('ООО', 'ПАО', 'ИП'));