
/****** Object:  Table [dbo].[t_resource_detail_upload]    Script Date: 12/28/2015 10:21:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_resource_detail_upload](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[resource_detail_id] [int] NULL,
	[files_name] [nvarchar](200) NULL,
	[files_dir] [nvarchar](100) NULL,
 CONSTRAINT [PK_t_resource_detail_upload] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Èí¼þÄ¿Â¼' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_resource_detail_upload', @level2type=N'COLUMN',@level2name=N'files_dir'
GO


