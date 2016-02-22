IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_NAME = N'FixItTasks')
BEGIN
  IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'Email' AND Object_ID = Object_ID(N'FixItTasks'))
	BEGIN
		alter table FixItTasks add Email nvarchar(200) NULL
	END
	
END
