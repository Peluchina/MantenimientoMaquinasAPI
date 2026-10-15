-- REGISTRAR MANTENIMIENTO
CREATE PROCEDURE MaintenanceRecords_Create
    @MachineId INT,
    @UserId INT,
    @MaintenanceDate DATETIME2,
    @MaintenanceType NVARCHAR(50),
    @Description NVARCHAR(500),
    @Cost DECIMAL(12,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (
            SELECT 1 FROM Machines WHERE MachineId = @MachineId
        )
            THROW 50201, 'Maquina no encontrada.', 1;

        IF NOT EXISTS (
            SELECT 1
            FROM Users
            WHERE UserId = @UserId
              AND UserStatus = 'ACTIVO'
        )
            THROW 50202, 'Usuario inexistente o inactivo.', 1;

        INSERT INTO MaintenanceRecords (
            MachineId, UserId, MaintenanceDate,
            MaintenanceType, Description, Cost
        )
        VALUES (
            @MachineId, @UserId, @MaintenanceDate,
            @MaintenanceType, @Description, @Cost
        );

        UPDATE Machines
        SET LastMaintenanceDate = @MaintenanceDate,
            UpdatedAt = SYSDATETIME()
        WHERE MachineId = @MachineId;

        DECLARE @MaintenanceId INT =
            CAST(SCOPE_IDENTITY() AS INT);

        COMMIT TRANSACTION;

        SELECT @MaintenanceId AS MaintenanceId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- CONSULTAR HISTORIAL DE MANTENIMIENTO
CREATE PROCEDURE MaintenanceRecords_GetByMachine
    @MachineId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        MR.MaintenanceId,
        MR.MachineId,
        M.MachineName,
        MR.UserId,
        U.FirstName + N' ' + U.LastName AS Responsible,
        MR.MaintenanceDate,
        MR.MaintenanceType,
        MR.Description,
        MR.Cost
    FROM MaintenanceRecords MR
    INNER JOIN Machines M ON M.MachineId = MR.MachineId
    INNER JOIN Users U ON U.UserId = MR.UserId
    WHERE MR.MachineId = @MachineId
    ORDER BY MR.MaintenanceDate DESC;
END;
