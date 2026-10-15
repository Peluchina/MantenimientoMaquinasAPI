CREATE TABLE MaintenanceRecords (
    MaintenanceId INT IDENTITY(1,1) PRIMARY KEY,
    MachineId INT NOT NULL,
    UserId INT NOT NULL,
    MaintenanceDate DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    MaintenanceType NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    Cost DECIMAL(12,2) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT FK_Maintenance_Machines
        FOREIGN KEY (MachineId) REFERENCES Machines(MachineId),

    CONSTRAINT FK_Maintenance_Users
        FOREIGN KEY (UserId) REFERENCES Users(UserId),

    CONSTRAINT CK_Maintenance_Cost
        CHECK (Cost IS NULL OR Cost >= 0)
);