
/****** Object:  Table [dbo].[t_resource_type]    Script Date: 12/28/2015 10:20:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_resource_type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[directory] [nvarchar](50) NULL,
	[insert_people] [nvarchar](50) NULL,
	[insert_time] [datetime] NULL,
	[delete_flag] [int] NULL,
	[update_time] [datetime] NULL,
	[update_people] [nvarchar](50) NULL,
	[delete_time] [datetime] NULL,
	[delete_people] [nvarchar](50) NULL,
 CONSTRAINT [PK_t_resource_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_resource_type', @level2type=N'COLUMN',@level2name=N'id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N't_resource_type', @level2type=N'COLUMN',@level2name=N'name'
GO

ALTER TABLE [dbo].[t_resource_type] ADD  CONSTRAINT [DF_t_resource_type_delete_flag]  DEFAULT ((0)) FOR [delete_flag]
GO


