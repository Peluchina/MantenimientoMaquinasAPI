CREATE TABLE Machines (
    MachineId INT IDENTITY(1,1) PRIMARY KEY,
    MachineName NVARCHAR(100) NOT NULL,
    Brand NVARCHAR(100) NOT NULL,
    CurrentTemperature DECIMAL(6,2) NOT NULL DEFAULT 0,
    MaxTemperature DECIMAL(6,2) NOT NULL,
    MachineStatus NVARCHAR(20) NOT NULL DEFAULT 'OPERATIVA',
    LastMaintenanceDate DATETIME2 NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    UpdatedAt DATETIME2 NULL,

    CONSTRAINT CK_Machines_Temperature
        CHECK (
            MaxTemperature > 0
            AND CurrentTemperature >= 0
        ),

    CONSTRAINT CK_Machines_Status
        CHECK (
            MachineStatus IN (
                'OPERATIVA',
                'MANTENIMIENTO',
                'INACTIVA',
                'FALLA'
            )
        )
);
