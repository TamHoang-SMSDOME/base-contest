/****** Object:  Table [dbo].[BC_240501_COKENCP_LOG]    Script Date: 12/12/2022 10:37:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BC_240501_COKENCP_LOG](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime2](7) NOT NULL,
	[Recipient] [nvarchar](100) NOT NULL,
	[LogType] [nvarchar](500) NOT NULL,
	[Content] [nvarchar](500) NOT NULL,
	[CreditsUsed] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_BC_240501_COKENCP_LOG] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


