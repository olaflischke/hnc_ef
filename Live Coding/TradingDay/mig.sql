BEGIN TRANSACTION;
GO

ALTER TABLE [TradingDays] ADD [TradingLocation] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220628133422_TradingLocation', N'6.0.6');
GO

COMMIT;
GO

