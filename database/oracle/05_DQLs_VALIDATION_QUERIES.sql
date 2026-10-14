```sql
-- ============================================================
-- Sellers Application - Oracle Database
-- File: 05_DQLs_VALIDATION_QUERIES.sql
-- Description: Queries to validate database initialization.
-- Schema: SELLER_APP
-- ============================================================


-- ============================================================
-- 1. VALIDATE RECORD COUNTS
-- Expected: 56 sellers, 5 buyers, 5 products, 5 sales, 3 users.
-- ============================================================

SELECT 'T_SELLER' AS TABLE_NAME, COUNT(*) AS TOTAL FROM T_SELLER
UNION ALL
SELECT 'T_BUYER', COUNT(*) FROM T_BUYER
UNION ALL
SELECT 'T_PRODUCT', COUNT(*) FROM T_PRODUCT
UNION ALL
SELECT 'T_SALE', COUNT(*) FROM T_SALE
UNION ALL
SELECT 'T_USER', COUNT(*) FROM T_USER
ORDER BY 1;


-- ============================================================
-- 2. VALIDATE SELLERS
-- ============================================================

SELECT ID, NAME, BIRTHDAY
FROM T_SELLER
ORDER BY ID;


-- ============================================================
-- 3. VALIDATE BUYERS
-- ============================================================

SELECT ID, NAME, SOCIAL_SECURITY_NUMBER
FROM T_BUYER
ORDER BY ID;


-- ============================================================
-- 4. VALIDATE PRODUCTS
-- ============================================================

SELECT ID, NAME, PACKAGING_DATE, STOCK, PRICE
FROM T_PRODUCT
ORDER BY ID;


-- ============================================================
-- 5. VALIDATE SALES
-- ============================================================

SELECT
    ID,
    ID_SELLER,
    ID_PRODUCT,
    ID_BUYER,
    SALE_DATE
FROM T_SALE
ORDER BY ID;


-- ============================================================
-- 6. VALIDATE SALES RELATIONSHIPS
-- Expected: 5 sales with their corresponding seller,
-- product and buyer.
-- ============================================================

SELECT
    S.ID AS SALE_ID,
    SE.NAME AS SELLER_NAME,
    P.NAME AS PRODUCT_NAME,
    B.NAME AS BUYER_NAME,
    S.SALE_DATE
FROM T_SALE S
INNER JOIN T_SELLER SE ON SE.ID = S.ID_SELLER
INNER JOIN T_PRODUCT P ON P.ID = S.ID_PRODUCT
INNER JOIN T_BUYER B ON B.ID = S.ID_BUYER
ORDER BY S.ID;


-- ============================================================
-- 7. VALIDATE USERS
-- ============================================================

SELECT
    ID,
    USERNAME,
    CREATED_AT,
    UPDATED_AT
FROM T_USER
ORDER BY ID;


-- ============================================================
-- 8. VALIDATE PRIMARY KEY AND FOREIGN KEY CONSTRAINTS
-- ============================================================

SELECT
    TABLE_NAME,
    CONSTRAINT_NAME,
    CONSTRAINT_TYPE,
    STATUS
FROM USER_CONSTRAINTS
WHERE TABLE_NAME IN (
    'T_SELLER',
    'T_BUYER',
    'T_PRODUCT',
    'T_SALE',
    'T_USER'
)
AND CONSTRAINT_TYPE IN ('P', 'R', 'U')
ORDER BY TABLE_NAME, CONSTRAINT_TYPE, CONSTRAINT_NAME;


-- ============================================================
-- 9. VALIDATE USER UPDATE TRIGGER
-- Expected: TRG_T_USER_UPDATED_AT is ENABLED.
-- ============================================================

SELECT
    TRIGGER_NAME,
    TABLE_NAME,
    STATUS
FROM USER_TRIGGERS
WHERE TRIGGER_NAME = 'TRG_T_USER_UPDATED_AT';


-- ============================================================
-- End of validation queries
-- ============================================================
```