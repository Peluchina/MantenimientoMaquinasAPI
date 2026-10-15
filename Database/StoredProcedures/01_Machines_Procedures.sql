-- CREATE
CREATE PROCEDURE Machines_Create
    @MachineName NVARCHAR(100),
    @Brand NVARCHAR(100),
    @CurrentTemperature DECIMAL(6,2),
    @MaxTemperature DECIMAL(6,2),
    @MachineStatus NVARCHAR(20) = 'OPERATIVA',
    @LastMaintenanceDate DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @MaxTemperature <= 0 OR @CurrentTemperature < 0
        THROW 50001, 'Temperaturas no validas.', 1;

    IF @MachineStatus NOT IN (
        'OPERATIVA', 'MANTENIMIENTO', 'INACTIVA', 'FALLA'
    )
        THROW 50002, 'Estado de maquina no valido.', 1;

    INSERT INTO Machines (
        MachineName,
        Brand,
        CurrentTemperature,
        MaxTemperature,
        MachineStatus,
        LastMaintenanceDate
    )
    VALUES (
        @MachineName,
        @Brand,
        @CurrentTemperature,
        @MaxTemperature,
        @MachineStatus,
        @LastMaintenanceDate
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS MachineId;
END;
GO

-- READ: listar todas las maquinas
CREATE PROCEDURE Machines_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        MachineId,
        MachineName,
        Brand,
        CurrentTemperature,
        MaxTemperature,
        MachineStatus,
        LastMaintenanceDate,
        CASE
            WHEN CurrentTemperature > MaxTemperature
                THEN 'SOBRECALENTAMIENTO'
            ELSE 'NORMAL'
        END AS TemperatureStatus,
        CreatedAt,
        UpdatedAt
    FROM Machines
    ORDER BY MachineId DESC;
END;
GO

-- READ: buscar una maquina por ID
CREATE PROCEDURE Machines_GetById
    @MachineId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Machines
    WHERE MachineId = @MachineId;
END;
GO

-- UPDATE
CREATE PROCEDURE Machines_Update
    @MachineId INT,
    @MachineName NVARCHAR(100),
    @Brand NVARCHAR(100),
    @CurrentTemperature DECIMAL(6,2),
    @MaxTemperature DECIMAL(6,2),
    @MachineStatus NVARCHAR(20),
    @LastMaintenanceDate DATETIME2 = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @MaxTemperature <= 0 OR @CurrentTemperature < 0
        THROW 50003, 'Temperaturas no validas.', 1;

    IF @MachineStatus NOT IN (
        'OPERATIVA', 'MANTENIMIENTO', 'INACTIVA', 'FALLA'
    )
        THROW 50004, 'Estado de maquina no valido.', 1;

    UPDATE Machines
    SET
        MachineName = @MachineName,
        Brand = @Brand,
        CurrentTemperature = @CurrentTemperature,
        MaxTemperature = @MaxTemperature,
        MachineStatus = @MachineStatus,
        LastMaintenanceDate = @LastMaintenanceDate,
        UpdatedAt = SYSDATETIME()
    WHERE MachineId = @MachineId;

    IF @@ROWCOUNT = 0
        THROW 50005, 'Maquina no encontrada.', 1;

    SELECT 'Maquina actualizada correctamente' AS Message;
END;
GO

-- DELETE
CREATE PROCEDURE Machines_Delete
    @MachineId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 FROM Machines WHERE MachineId = @MachineId
    )
        THROW 50006, 'Maquina no encontrada.', 1;

    IF EXISTS (
        SELECT 1
        FROM MaintenanceRecords
        WHERE MachineId = @MachineId
    )
        THROW 50007,
            'No se puede eliminar: la maquina tiene historial de mantenimiento.',
            1;

    IF EXISTS (
        SELECT 1
        FROM MachineTemperatureHistory
        WHERE MachineId = @MachineId
    )
        THROW 50008,
            'No se puede eliminar: la maquina tiene historial de temperatura.',
            1;

    DELETE FROM Machines
    WHERE MachineId = @MachineId;

    SELECT 'Maquina eliminada correctamente' AS Message;
END;
GO
