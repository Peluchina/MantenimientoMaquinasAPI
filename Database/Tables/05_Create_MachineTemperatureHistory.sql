CREATE TABLE MachineTemperatureHistory (
    TemperatureHistoryId BIGINT IDENTITY(1,1) PRIMARY KEY,
    MachineId INT NOT NULL,
    UserId INT NULL,
    Temperature DECIMAL(6,2) NOT NULL,
    RecordedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT FK_Temperature_Machines
        FOREIGN KEY (MachineId) REFERENCES Machines(MachineId),

    CONSTRAINT FK_Temperature_Users
        FOREIGN KEY (UserId) REFERENCES Users(UserId),

    CONSTRAINT CK_Temperature_Value
        CHECK (Temperature >= 0)
);
