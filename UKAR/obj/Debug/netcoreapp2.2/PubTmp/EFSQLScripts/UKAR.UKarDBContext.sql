IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [FullName] nvarchar(max) NULL,
        [PasswordHash] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE TABLE [ActiveTrips] (
        [DriverId] nvarchar(450) NOT NULL,
        [EmployerId] nvarchar(450) NULL,
        [TripType] nvarchar(max) NULL,
        [LatitudeOrigin] float NOT NULL,
        [LongitudeOrigin] float NOT NULL,
        [LatitudeDestination] float NULL,
        [LongitudeDestination] float NULL,
        [StartTime] datetime2 NOT NULL,
        [FinishTime] datetime2 NOT NULL,
        [Distance] float NOT NULL,
        [TimeOffset] datetimeoffset NOT NULL,
        [Discount] float NOT NULL,
        [TotalAmount] Money NOT NULL,
        [Accepted] bit NOT NULL,
        [Canceled] bit NOT NULL,
        [RejectReason] nvarchar(max) NULL,
        [DriverBlackListString] nvarchar(max) NULL,
        CONSTRAINT [PK_ActiveTrips] PRIMARY KEY ([DriverId]),
        CONSTRAINT [FK_ActiveTrips_AspNetUsers_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ActiveTrips_AspNetUsers_EmployerId] FOREIGN KEY ([EmployerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE TABLE [Trips] (
        [TripId] bigint NOT NULL IDENTITY,
        [DriverId] nvarchar(450) NULL,
        [EmployerId] nvarchar(450) NULL,
        [TripType] nvarchar(max) NULL,
        [LatitudeOrigin] float NOT NULL,
        [LongitudeOrigin] float NOT NULL,
        [LatitudeDestination] float NOT NULL,
        [LongitudeDestination] float NOT NULL,
        [StartTime] datetime2 NOT NULL,
        [FinishTime] datetime2 NOT NULL,
        [Distance] float NOT NULL,
        [TimeOffset] datetimeoffset NOT NULL,
        [Discount] float NOT NULL,
        [TotalAmount] Money NOT NULL,
        [Accepted] bit NOT NULL,
        [RejectReason] nvarchar(max) NULL,
        CONSTRAINT [PK_Trips] PRIMARY KEY ([TripId]),
        CONSTRAINT [FK_Trips_AspNetUsers_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Trips_AspNetUsers_EmployerId] FOREIGN KEY ([EmployerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE TABLE [UserLocations] (
        [UserId] nvarchar(450) NOT NULL,
        [Latitude] float NULL,
        [Longitude] float NULL,
        [LocatedTime] datetime2 NOT NULL,
        CONSTRAINT [PK_UserLocations] PRIMARY KEY ([UserId]),
        CONSTRAINT [FK_UserLocations_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE TABLE [ActiveDriver] (
        [UserId] nvarchar(450) NOT NULL,
        [CurrentRole] nvarchar(max) NULL,
        [TripType] nvarchar(max) NULL,
        [UserLocationId] nvarchar(450) NULL,
        CONSTRAINT [PK_ActiveDriver] PRIMARY KEY ([UserId]),
        CONSTRAINT [FK_ActiveDriver_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ActiveDriver_UserLocations_UserLocationId] FOREIGN KEY ([UserLocationId]) REFERENCES [UserLocations] ([UserId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE INDEX [IX_ActiveDriver_UserLocationId] ON [ActiveDriver] ([UserLocationId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE UNIQUE INDEX [IX_ActiveTrips_DriverId] ON [ActiveTrips] ([DriverId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE INDEX [IX_ActiveTrips_EmployerId] ON [ActiveTrips] ([EmployerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE INDEX [IX_Trips_DriverId] ON [Trips] ([DriverId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE INDEX [IX_Trips_EmployerId] ON [Trips] ([EmployerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE INDEX [IX_UserLocations_Latitude] ON [UserLocations] ([Latitude]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    CREATE INDEX [IX_UserLocations_Longitude] ON [UserLocations] ([Longitude]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330113829_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190330113829_Initial', N'2.2.3-servicing-35854');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330172850_nullableOnTrip')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'TimeOffset');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [TimeOffset] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330172850_nullableOnTrip')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'Distance');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [Distance] float NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330172850_nullableOnTrip')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ActiveTrips]') AND [c].[name] = N'TimeOffset');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ActiveTrips] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [ActiveTrips] ALTER COLUMN [TimeOffset] datetimeoffset NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330172850_nullableOnTrip')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ActiveTrips]') AND [c].[name] = N'StartTime');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ActiveTrips] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [ActiveTrips] ALTER COLUMN [StartTime] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330172850_nullableOnTrip')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ActiveTrips]') AND [c].[name] = N'FinishTime');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [ActiveTrips] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [ActiveTrips] ALTER COLUMN [FinishTime] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330172850_nullableOnTrip')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ActiveTrips]') AND [c].[name] = N'Distance');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [ActiveTrips] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [ActiveTrips] ALTER COLUMN [Distance] float NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190330172850_nullableOnTrip')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190330172850_nullableOnTrip', N'2.2.3-servicing-35854');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190402155921_modifyUser')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Role] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190402155921_modifyUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190402155921_modifyUser', N'2.2.3-servicing-35854');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405200357_ModifyTripAndActiveTrip')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'TimeOffset');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [TimeOffset] time NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405200357_ModifyTripAndActiveTrip')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'StartTime');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [StartTime] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405200357_ModifyTripAndActiveTrip')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'LongitudeDestination');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [LongitudeDestination] float NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405200357_ModifyTripAndActiveTrip')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'LatitudeDestination');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [LatitudeDestination] float NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405200357_ModifyTripAndActiveTrip')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trips]') AND [c].[name] = N'FinishTime');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Trips] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Trips] ALTER COLUMN [FinishTime] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405200357_ModifyTripAndActiveTrip')
BEGIN
    ALTER TABLE [Trips] ADD [Canceled] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405200357_ModifyTripAndActiveTrip')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [AvatarBase64] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405200357_ModifyTripAndActiveTrip')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [AvatarFileType] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405200357_ModifyTripAndActiveTrip')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ActiveTrips]') AND [c].[name] = N'TimeOffset');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [ActiveTrips] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [ActiveTrips] ALTER COLUMN [TimeOffset] time NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405200357_ModifyTripAndActiveTrip')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190405200357_ModifyTripAndActiveTrip', N'2.2.3-servicing-35854');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405214718_addCar')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [CarId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405214718_addCar')
BEGIN
    CREATE TABLE [Cars] (
        [CarId] int NOT NULL IDENTITY,
        [Brand] nvarchar(max) NULL,
        [Color] nvarchar(max) NULL,
        [IsConfirmed] bit NOT NULL,
        [CarImage] nvarchar(max) NULL,
        [CarImageFileType] nvarchar(max) NULL,
        [RegistrationDate] datetime2 NOT NULL,
        [PlateNumber] nvarchar(max) NULL,
        [RegistrationImage] nvarchar(max) NULL,
        [RegistrationImageFileType] nvarchar(max) NULL,
        CONSTRAINT [PK_Cars] PRIMARY KEY ([CarId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405214718_addCar')
BEGIN
    CREATE INDEX [IX_AspNetUsers_CarId] ON [AspNetUsers] ([CarId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405214718_addCar')
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Cars_CarId] FOREIGN KEY ([CarId]) REFERENCES [Cars] ([CarId]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190405214718_addCar')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190405214718_addCar', N'2.2.3-servicing-35854');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    ALTER TABLE [ActiveTrips] DROP CONSTRAINT [FK_ActiveTrips_AspNetUsers_DriverId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    ALTER TABLE [ActiveTrips] DROP CONSTRAINT [FK_ActiveTrips_AspNetUsers_EmployerId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    ALTER TABLE [ActiveTrips] DROP CONSTRAINT [PK_ActiveTrips];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    DROP INDEX [IX_ActiveTrips_DriverId] ON [ActiveTrips];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    DROP INDEX [IX_ActiveTrips_EmployerId] ON [ActiveTrips];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ActiveTrips]') AND [c].[name] = N'EmployerId');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [ActiveTrips] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [ActiveTrips] ALTER COLUMN [EmployerId] nvarchar(450) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ActiveTrips]') AND [c].[name] = N'DriverId');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [ActiveTrips] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [ActiveTrips] ALTER COLUMN [DriverId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    ALTER TABLE [ActiveTrips] ADD CONSTRAINT [PK_ActiveTrips] PRIMARY KEY ([EmployerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    CREATE UNIQUE INDEX [IX_ActiveTrips_DriverId] ON [ActiveTrips] ([DriverId]) WHERE [DriverId] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    ALTER TABLE [ActiveTrips] ADD CONSTRAINT [FK_ActiveTrips_AspNetUsers_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    ALTER TABLE [ActiveTrips] ADD CONSTRAINT [FK_ActiveTrips_AspNetUsers_EmployerId] FOREIGN KEY ([EmployerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190406132237_changeActiveTripIDCol')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190406132237_changeActiveTripIDCol', N'2.2.3-servicing-35854');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190407115643_modifyLocation')
BEGIN
    ALTER TABLE [UserLocations] ADD [HasTrip] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190407115643_modifyLocation')
BEGIN
    ALTER TABLE [UserLocations] ADD [OnTrip] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190407115643_modifyLocation')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190407115643_modifyLocation', N'2.2.3-servicing-35854');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190409172019_addLicenseAndDrivingTest')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [DriverTestPassed] bit NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190409172019_addLicenseAndDrivingTest')
BEGIN
    CREATE TABLE [DrivingLicenses] (
        [DriverId] nvarchar(450) NOT NULL,
        [LicenseNumber] nvarchar(max) NULL,
        [Image] nvarchar(max) NULL,
        [ImageFileType] nvarchar(max) NULL,
        [ImageBack] nvarchar(max) NULL,
        [ImageBackFileType] nvarchar(max) NULL,
        CONSTRAINT [PK_DrivingLicenses] PRIMARY KEY ([DriverId]),
        CONSTRAINT [FK_DrivingLicenses_AspNetUsers_DriverId] FOREIGN KEY ([DriverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190409172019_addLicenseAndDrivingTest')
BEGIN
    CREATE TABLE [DrivingTests] (
        [UserId] nvarchar(450) NOT NULL,
        [Date] datetime2 NOT NULL,
        [PracticeScore] int NOT NULL,
        [ExamScore] int NOT NULL,
        [Passed] bit NOT NULL,
        CONSTRAINT [PK_DrivingTests] PRIMARY KEY ([UserId]),
        CONSTRAINT [FK_DrivingTests_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190409172019_addLicenseAndDrivingTest')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190409172019_addLicenseAndDrivingTest', N'2.2.3-servicing-35854');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190418140947_changeUser')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'AvatarFileType');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [AvatarFileType];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190418140947_changeUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190418140947_changeUser', N'2.2.3-servicing-35854');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190418152950_addUserTestDate')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [TestTime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190418152950_addUserTestDate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190418152950_addUserTestDate', N'2.2.3-servicing-35854');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190418155747_changeTesttime')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'TestTime');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [TestTime] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190418155747_changeTesttime')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190418155747_changeTesttime', N'2.2.3-servicing-35854');
END;

GO

