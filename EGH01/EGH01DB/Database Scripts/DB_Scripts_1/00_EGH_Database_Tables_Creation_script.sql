----------------- �������� ������ ���� ������ -------------------------------
----			����� ������� ������� ��������
---- ������� ������������� �����
---- ����������� ������  
---- ��������������� ������
---- ��������    

------------------------------------------------------------------------------
USE EGH;
GO

DROP TABLE dbo.�������������������������;
DROP TABLE dbo.�����������������;
DROP TABLE dbo.���������������������;
DROP TABLE dbo.��������;

--- �������� ������� -- ������������� �����
------------------------------------------------------------------------------------------------------
---------------------- �������� ����� ������ ��� �����!!!! ����������!!!!!
------------------------------------------------------------------------------------------------------
create TABLE [dbo].[�������������������������](
	[Id�������������������������] [int] NOT NULL,
	[����������] [float] NOT NULL,
	[�����������] [float] NOT NULL,
	[���������] [int] NOT NULL,
	[�������������������] [float] NOT NULL,
	[�����������������] [float] NOT NULL
 CONSTRAINT [PK_������������������] PRIMARY KEY CLUSTERED ([Id�������������������������]));
GO

ALTER TABLE [dbo].[�������������������������]  WITH CHECK ADD CONSTRAINT [FK_�������������������������_���������] FOREIGN KEY([���������])
REFERENCES [dbo].[���������] ([�������������]);
GO

ALTER TABLE [dbo].[�������������������������] CHECK CONSTRAINT [FK_�������������������������_���������];
GO
--- �������� ������� -- ����������� ������
------------------------------------------------------------------------------------------------------
---------------------- �������� ����� ������ ��� ������������ �������!!!!
------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[�����������������](
	[Id�������������������] int NOT NULL,
	[��������������������������] int NOT NULL,
	[�����������������������] int NOT NULL,
	[�������������������������������] nvarchar(max) NOT NULL,
	[������������������������] nvarchar(max) NOT NULL,
	[����������] float NOT NULL,
	[�����������] float NOT NULL,
	[���������] int NOT NULL,
	[�������������������] float NOT NULL,
	[�����������������]  float NOT NULL
 CONSTRAINT [PK_�����������������] PRIMARY KEY CLUSTERED ([Id�������������������]));
GO

ALTER TABLE [dbo].[�����������������]  WITH CHECK ADD  CONSTRAINT [FK_�����������������_����������������] FOREIGN KEY([�����������������������])
REFERENCES [dbo].[����������������] ([�������������������])
GO

ALTER TABLE [dbo].[�����������������] CHECK CONSTRAINT [FK_�����������������_����������������]
GO

ALTER TABLE [dbo].[�����������������]  WITH CHECK ADD  CONSTRAINT [FK_�����������������_����������������������] FOREIGN KEY([��������������������������])
REFERENCES [dbo].[����������������������] ([��������������������������]);
GO

ALTER TABLE [dbo].[�����������������] CHECK CONSTRAINT [FK_�����������������_����������������������];
GO

ALTER TABLE [dbo].[�����������������]  WITH CHECK ADD  CONSTRAINT [FK_�����������������_���������] FOREIGN KEY([���������])
REFERENCES [dbo].[���������] ([�������������]);
GO

ALTER TABLE [dbo].[�����������������] CHECK CONSTRAINT [FK_�����������������_���������];
GO


---- �������� ������� -- ��������������� ������ 
------------------------------------------------------------------------------------------------------
---------------------- �������� ����� ������ ��� ���������������� �������!!!!
------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[���������������������](
	[Id�����������������������] int NOT NULL,
	[�����������������������������������] nvarchar(50) NOT NULL,
	[������������������������������] int NOT NULL,
	[�����������������������] int NOT NULL,
	[����������] float NOT NULL,
	[�����������] float NOT NULL,
	[���������] int NOT NULL,
	[�������������������] float NOT NULL,
	[�����������������] float NOT NULL
 CONSTRAINT [PK_Id�����������������������] PRIMARY KEY ([Id�����������������������]))
GO

ALTER TABLE [dbo].[���������������������]  WITH CHECK ADD  CONSTRAINT [FK_���������������������_����������������] FOREIGN KEY([�����������������������])
REFERENCES [dbo].[����������������] ([�������������������])
GO

ALTER TABLE [dbo].[���������������������] CHECK CONSTRAINT [FK_���������������������_����������������]
GO

ALTER TABLE [dbo].[���������������������]  WITH CHECK ADD  CONSTRAINT [FK_���������������������_���������] FOREIGN KEY([���������])
REFERENCES [dbo].[���������] ([�������������])
GO

ALTER TABLE [dbo].[���������������������] CHECK CONSTRAINT [FK_���������������������_���������]
GO

ALTER TABLE [dbo].[���������������������]  WITH CHECK ADD  CONSTRAINT [FK_���������������������_��������������������������] FOREIGN KEY([������������������������������])
REFERENCES [dbo].[��������������������������] ([������������������������������])
GO

ALTER TABLE [dbo].[���������������������] CHECK CONSTRAINT [FK_���������������������_��������������������������]
GO

---- �������� ������� -- ��������
CREATE TABLE [dbo].[��������](
	[Id���������] int NOT NULL,
	[����������������] int NULL,
	[����] datetime NULL,
	[�������������] datetime NULL,
	[�����������������������] int NOT NULL,
	[����������] float NOT NULL,
	[�����������] float NOT NULL,
	[���������] int NOT NULL,
	[�������������������] float NOT NULL,
	[�����������������] float NOT NULL
	CONSTRAINT [PK_Id���������] PRIMARY KEY ([Id���������]));
GO
ALTER TABLE [dbo].[��������]  WITH CHECK ADD  CONSTRAINT [FK_��������_����������������] FOREIGN KEY([�����������������������])
REFERENCES [dbo].[����������������] ([�������������������])
GO

ALTER TABLE [dbo].[��������] CHECK CONSTRAINT [FK_��������_����������������]
GO

ALTER TABLE [dbo].[��������]  WITH CHECK ADD  CONSTRAINT [FK_��������_���������] FOREIGN KEY([���������])
REFERENCES [dbo].[���������] ([�������������])
GO

ALTER TABLE [dbo].[��������] CHECK CONSTRAINT [FK_��������_���������]
GO

ALTER TABLE [dbo].[��������]  WITH CHECK ADD  CONSTRAINT [FK_��������_������������] FOREIGN KEY([����������������])
REFERENCES [dbo].[������������] ([�������])
GO

ALTER TABLE [dbo].[��������] CHECK CONSTRAINT [FK_��������_������������]
GO
------------------------------------------------------------------------------------------------------
----- �������� ��������� ��������� ������� -----------------------------------------------------------
	--[������������] [int] NOT NULL,
	--[������������������������] [datetime2](7) NOT NULL,
	--[�������] [int] NOT NULL,
	--[����������] [int] NULL,
	--[��������] [int] NOT NULL,
	--[��������������������������] [int] NULL,
	--[����������] [nvarchar](max) NULL
------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------
---- �������� ������� ����������� ������
CREATE TABLE [dbo].[�����������������](
	[Id�������������������] [int] NOT NULL,
	[��������������������������] [int] NOT NULL,
	[�����������������������] [int] NOT NULL,
	[�������������������������������] [nvarchar](max) NOT NULL,
	[������������������������] [nvarchar](max) NULL,
	[����������] [float] NOT NULL,
	[�����������] [float] NOT NULL,
	[���������] [int] NOT NULL,
	[�������������������] [float] NULL,
	[�����������������] [float] NULL,
 CONSTRAINT [PK_�����������������] PRIMARY KEY CLUSTERED 
(
	[Id�������������������] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[�����������������]  WITH CHECK ADD  CONSTRAINT [FK_�����������������_����������������] FOREIGN KEY([�����������������������])
REFERENCES [dbo].[����������������] ([�������������������])
GO

ALTER TABLE [dbo].[�����������������] CHECK CONSTRAINT [FK_�����������������_����������������]
GO

ALTER TABLE [dbo].[�����������������]  WITH CHECK ADD  CONSTRAINT [FK_�����������������_���������] FOREIGN KEY([���������])
REFERENCES [dbo].[���������] ([�������������])
GO

ALTER TABLE [dbo].[�����������������] CHECK CONSTRAINT [FK_�����������������_���������]
GO

ALTER TABLE [dbo].[�����������������]  WITH CHECK ADD  CONSTRAINT [FK_�����������������_����������������������] FOREIGN KEY([��������������������������])
REFERENCES [dbo].[����������������������] ([��������������������������])
GO

ALTER TABLE [dbo].[�����������������] CHECK CONSTRAINT [FK_�����������������_����������������������]
GO
