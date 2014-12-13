USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [ProductionMan]    Script Date: 12/13/2014 8:55:59 PM ******/
CREATE LOGIN [ProductionMan] WITH PASSWORD=123654*, DEFAULT_DATABASE=[ProductionMan], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [ProductionMan] DISABLE
GO

ALTER SERVER ROLE [dbcreator] ADD MEMBER [ProductionMan]
GO


