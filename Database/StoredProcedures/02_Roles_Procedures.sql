-- LISTAR ROLES
CREATE PROCEDURE Roles_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT RoleId, RoleName, Description
    FROM Roles
    ORDER BY RoleId;
END;