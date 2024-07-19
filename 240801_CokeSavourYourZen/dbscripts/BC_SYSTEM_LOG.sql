/****** Object:  Table [dbo].[BC_SYSTEM_LOG]    Script Date: 4/26/2024 3:35:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BC_SYSTEM_LOG](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[Level] [nvarchar](10) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[StackTrace] [nvarchar](max) NOT NULL,
	[Exception] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](250) NULL,
	[Contest] [nvarchar](250) NOT NULL,
	[Url] [nvarchar](250) NOT NULL,
	[RequestIP] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_BC_SYSTEM_LOG] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


