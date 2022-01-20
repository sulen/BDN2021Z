CREATE TYPE product_sales_by_quarter AS (
    category_name varchar(15), product_name varchar(40), product_sales NUMERIC, shipped_quarter TEXT
                                        );
CREATE OR REPLACE FUNCTION product_sales_for_1997_by_quarter()
RETURNS SETOF product_sales_by_quarter
AS
    $$
    SELECT a.category_name,
                b.product_name,
                ROUND(SUM(c.unit_price * c.Quantity * (1 - c.Discount))::NUMERIC, 2) AS product_sales,
                concat('Qtr ', EXTRACT(quarter FROM d.shipped_date)) AS shipped_quarter
FROM categories a
         INNER JOIN products b ON a.category_id = b.category_id
         INNER JOIN order_details c ON b.product_id = c.product_id
         INNER JOIN orders d ON d.order_id = c.order_id
WHERE d.shipped_date BETWEEN date('1997-01-01') AND date('1997-12-31')
GROUP BY a.category_name,
         b.product_name,
         concat('Qtr ', EXTRACT(quarter FROM d.shipped_date))
ORDER BY a.category_name,
         b.product_name,
         shipped_quarter
$$
LANGUAGE sql;