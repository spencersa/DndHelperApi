﻿CREATE TABLE [dbo].[VillianScheme](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Scheme] [nvarchar](255) NULL,
	[VillanObjectiveId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VillianScheme]  WITH CHECK ADD FOREIGN KEY([VillanObjectiveId])
REFERENCES [dbo].[VillianObjective] ([Id])
GO