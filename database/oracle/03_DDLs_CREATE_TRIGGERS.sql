-- ============================================================
-- Sellers Application - Oracle Database
-- File: 03_DDLs_CREATE_TRIGGERS.sql
-- Description: Triggers used by the application.
-- ============================================================

-- Updates UPDATED_AT automatically before each user update.
CREATE OR REPLACE TRIGGER TRG_T_USER_UPDATED_AT
BEFORE UPDATE ON T_USER
FOR EACH ROW
BEGIN
    :NEW.UPDATED_AT := CURRENT_TIMESTAMP;
END;
/

-- Enable trigger
ALTER TRIGGER TRG_T_USER_UPDATED_AT ENABLE;

-- End of script