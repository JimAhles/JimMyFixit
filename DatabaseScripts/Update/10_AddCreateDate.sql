﻿IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_NAME = N'FixItTasks')
BEGIN
  IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'CreatedDate' AND Object_ID = Object_ID(N'FixItTasks'))
	BEGIN
		ALTER TABLE FixitTasks
		ADD CreatedDate DATETIME NOT NULL DEFAULT (GETDATE())
	END
END