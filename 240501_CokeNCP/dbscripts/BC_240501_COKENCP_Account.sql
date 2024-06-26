SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BC_240501_COKENCP_Account](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[AccountType] [nvarchar](250) NOT NULL,
	[Password] [varchar](250) NOT NULL,
 CONSTRAINT [PK_BC_240501_COKENCP_Account] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [BC_240501_COKENCP_Account]([AccountType], [Password])
VALUES ('ADMIN', 'AQAAAAEAACcQAAAAEI/c1aSY8r6pckSc9QWdMqxqZBG62UeYPeMZsmP+fFWKgvjxqNT0zvykCjJVuRCljg==');

INSERT INTO [BC_240501_COKENCP_Account]([AccountType], [Password])
VALUES ('USER', 'AQAAAAEAACcQAAAAEEn2rmnDSsCu6EXCKwOrw1lCEtpSa0/XqmtUcMDutzi/00t/XCvKsAvRwg87LV5AnQ==');

INSERT INTO [BC_240501_COKENCP_Account]([AccountType], [Password])
VALUES ('IdentityPW', 'f3b396r6-b95e-4z84-lde1-f475f42sa893');