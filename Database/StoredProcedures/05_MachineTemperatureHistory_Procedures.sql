-- REGISTRAR TEMPERATURA
CREATE PROCEDURE MachineTemperatureHistory_Create
    @MachineId INT,
    @UserId INT = NULL,
    @Temperature DECIMAL(6,2)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Temperature < 0
        THROW 50203, 'Temperatura no valida.', 1;

    IF NOT EXISTS (
        SELECT 1 FROM Machines WHERE MachineId = @MachineId
    )
        THROW 50204, 'Maquina no encontrada.', 1;

    IF @UserId IS NOT NULL AND NOT EXISTS (
        SELECT 1
        FROM Users
        WHERE UserId = @UserId
          AND UserStatus = 'ACTIVO'
    )
        THROW 50205, 'Usuario inexistente o inactivo.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO MachineTemperatureHistory (
            MachineId, UserId, Temperature
        )
        VALUES (
            @MachineId, @UserId, @Temperature
        );

        UPDATE Machines
        SET CurrentTemperature = @Temperature,
            UpdatedAt = SYSDATETIME()
        WHERE MachineId = @MachineId;

        DECLARE @TemperatureHistoryId BIGINT =
            CONVERT(BIGINT, SCOPE_IDENTITY());

        COMMIT TRANSACTION;

        SELECT @TemperatureHistoryId AS TemperatureHistoryId;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

-- CONSULTAR HISTORIAL DE TEMPERATURA
CREATE PROCEDURE MachineTemperatureHistory_GetByMachine
    @MachineId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        TemperatureHistoryId,
        MachineId,
        UserId,
        Temperature,
        RecordedAt
    FROM MachineTemperatureHistory
    WHERE MachineId = @MachineId
    ORDER BY RecordedAt DESC;
END;
GO