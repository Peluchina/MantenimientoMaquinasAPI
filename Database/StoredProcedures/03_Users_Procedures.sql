-- CREATE USER
CREATE PROCEDURE Users_Create
    @RoleId INT,
    @Username NVARCHAR(50),
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Email NVARCHAR(150),
    @PasswordHash NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 FROM Roles WHERE RoleId = @RoleId
    )
        THROW 50101, 'El rol no existe.', 1;

    INSERT INTO Users (
        RoleId, Username, FirstName, LastName,
        Email, PasswordHash
    )
    VALUES (
        @RoleId, @Username, @FirstName, @LastName,
        @Email, @PasswordHash
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS UserId;
END;
GO

-- READ USERS
CREATE PROCEDURE Users_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        U.UserId,
        U.Username,
        U.FirstName,
        U.LastName,
        U.Email,
        U.RoleId,
        R.RoleName,
        U.UserStatus,
        U.CreatedAt
    FROM Users U
    INNER JOIN Roles R ON R.RoleId = U.RoleId
    ORDER BY U.UserId DESC;
END;
GO

-- READ USER BY ID
CREATE PROCEDURE Users_GetById
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        U.UserId,
        U.Username,
        U.FirstName,
        U.LastName,
        U.Email,
        U.RoleId,
        R.RoleName,
        U.UserStatus,
        U.CreatedAt
    FROM Users U
    INNER JOIN Roles R ON R.RoleId = U.RoleId
    WHERE U.UserId = @UserId;
END;
GO

-- UPDATE USER
CREATE PROCEDURE Users_Update
    @UserId INT,
    @RoleId INT,
    @Username NVARCHAR(50),
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Email NVARCHAR(150),
    @UserStatus NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    IF @UserStatus NOT IN ('ACTIVO', 'INACTIVO')
        THROW 50102, 'Estado de usuario no valido.', 1;

    IF NOT EXISTS (
        SELECT 1 FROM Roles WHERE RoleId = @RoleId
    )
        THROW 50103, 'El rol no existe.', 1;

    UPDATE Users
    SET
        RoleId = @RoleId,
        Username = @Username,
        FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        UserStatus = @UserStatus,
        UpdatedAt = SYSDATETIME()
    WHERE UserId = @UserId;

    IF @@ROWCOUNT = 0
        THROW 50104, 'Usuario no encontrado.', 1;

    SELECT 'Usuario actualizado correctamente' AS Message;
END;
GO

-- DESACTIVAR USUARIO
-- Se conserva el registro y su historial.
CREATE PROCEDURE Users_Delete
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET
        UserStatus = 'INACTIVO',
        UpdatedAt = SYSDATETIME()
    WHERE UserId = @UserId;

    IF @@ROWCOUNT = 0
        THROW 50105, 'Usuario no encontrado.', 1;

    SELECT 'Usuario desactivado correctamente' AS Message;
END;
GO
