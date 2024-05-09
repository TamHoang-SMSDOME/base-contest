SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BC_240501_COKENCP](
	[EntryID] [int] IDENTITY(1,1) NOT NULL,
	[DateEntry] [datetime2](7) NOT NULL,
	[MobileNo] [varchar](50) NOT NULL,
	[EntryText] [nvarchar](500) NOT NULL,
	[IsValid] [bit] NOT NULL,
	[IsVerified] [bit] NOT NULL,
	[IsRejected] [bit] NOT NULL,
	[Reason] [varchar](250) NOT NULL,
	[Response] [nvarchar](500) NOT NULL,
	[VerificationCode] [varchar](20) NOT NULL,
	[Chances] [int] NOT NULL,
	[EntrySource] [varchar](50) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[NRIC] [varchar](250) NOT NULL,
	[NRIC_NoPrefix] [varchar](250) NOT NULL,
	[ReceiptNo] [varchar](100) NOT NULL,
	[Amount] [money] NOT NULL,
	[DateResent] [datetime2](7) NULL,
	[DateVerified] [datetime2](7) NULL,
	[DateRejected] [datetime2](7) NULL,
	[Email] [nvarchar](250) NOT NULL,
	[Retailer] [nvarchar](250) NOT NULL,
	[FileLink] [varchar](1500) NOT NULL,
 CONSTRAINT [PK_BC_240501_COKENCP] PRIMARY KEY CLUSTERED 
(
	[EntryID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO