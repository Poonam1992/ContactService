
/****** Object: Table [dbo].[Contact] Script Date: 10/1/2019 2:05:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Contact];


GO
CREATE TABLE [dbo].[Contact] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50) NOT NULL,
    [LastName]    NVARCHAR (50) NULL,
    [Email]       NVARCHAR (50) NOT NULL,
    [PhoneNumber] NVARCHAR (20) NULL,
    [Status]      BIT           NULL
);


INSERT INTO [dbo].[Contact] (FirstName, LastName, Email, PhoneNumber,Status)
VALUES
('Poonam','Suryawanshi', 'poonam@gmail.com', '77445213621',1),
('John','Stark', 'John@gmail.com', '7744521374',0),
('Michle','Jack', 'Jack@gmail.com', '88445213621',1)


