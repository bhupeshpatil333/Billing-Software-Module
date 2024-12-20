USE [BillingSystem]
GO

/****** Object:  Table [dbo].[Billings]    Script Date: 25-10-2024 18:58:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Billings](
	[BillingID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[TotalAmount] [decimal](18, 2) NULL,
	[BillingDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[BillingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Billings] ADD  DEFAULT (getdate()) FOR [BillingDate]
GO
