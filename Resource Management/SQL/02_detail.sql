

/****** Object:  Table [dbo].[t_resource_detail]    Script Date: 12/28/2015 10:21:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_resource_detail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[type_id] [int] NULL,
	[type] [nvarchar](50) NULL,
	[url] [nvarchar](100) NULL,
	[memo] [nvarchar](200) NULL,
	[upload_people] [nvarchar](50) NULL,
	[insert_people] [nvarchar](50) NULL,
	[insert_time] [datetime] NULL,
	[update_time] [datetime] NULL,
	[update_people] [nvarchar](50) NULL,
	[delete_flag] [int] NULL,
	[delete_time] [datetime] NULL,
	[dalete_people] [nvarchar](50) NULL,
 CONSTRAINT [PK_t_resource_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[t_resource_detail] ADD  CONSTRAINT [DF_t_resource_detail_delete_flag]  DEFAULT ((0)) FOR [delete_flag]
GO


