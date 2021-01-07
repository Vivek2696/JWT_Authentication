/*
CREATE DATABASE JWT_Auth and then run this script to create these table
*/

use JWT_Auth;

GO

/****** Object:  Table [dbo].[Users]    Script Date: 12/29/2020 9:51:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](100) NULL,
	[lastName] [nvarchar](100) NULL,
	[dob] [date] NULL,
	[email] [nvarchar](100) NOT NULL UNIQUE,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

GO

/****** Object:  Table [dbo].[Passwords]    Script Date: 12/29/2020 9:52:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Passwords](
	[pswdId] [int] IDENTITY(1,1) NOT NULL,
	[password] [nvarchar](500) NULL,
	[password_salt] [nvarchar](500) NULL,
	[password_hash_algorithm] [nvarchar](100) NULL,
	[userId] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Passwords]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([id])
GO



