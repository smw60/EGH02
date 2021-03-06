----------------- �������� �������� --------- -------------------------------
---- ��� ������
-----------------------------------------------------------------------------
---- ���������� ���� ������ 
---- �������� ���� ������ 
---- ��������� ���� ������ �� ����  
---- ��������� ������ ����� ������ 
---- ���������� ���� ������
---- ��������� ���������� ���� ���� ������ 
-----------------------------------------------------------------------------
drop procedure EGH.CreateGroundType;
drop procedure EGH.DeleteGroundType;
drop procedure EGH.GetGroundTypeByCode;
drop procedure EGH.GetGroundTypeList;
drop procedure EGH.UpdateGroundType;
drop procedure EGH.GetNextGroundTypeCode;
GO
------------------------------------

-- ���������� ���� ������  
create procedure EGH.CreateGroundType(
		    @������������� int,  
			@���������������������� nvarchar(50), 
			@�������������� real,
			@�������������������� real,
			@������������������ real,
			@������������ real,
			@����������������� real,
			@����������� real,
			@������������������� real,
			@��������������� real,
			@�������������� real,
			@����������������� real,
			@���������������������� real)
as begin 
declare @rc int  = @�������������;
	begin try
		insert into dbo.���������(
					�������������,
					����������������������,
					��������������,
					��������������������,
					������������������,
					������������,
					�����������������,
					�����������,
					�������������������,
					���������������,
					��������������,
					�����������������,
					����������������������) 
			values (@�������������,  
					@����������������������, 
					@��������������,
					@��������������������,
					@������������������,
					@������������,
					@�����������������,
					@�����������,
					@�������������������,
					@���������������,
					@��������������,
					@�����������������,
					@����������������������); 
	end try
	begin catch
	    set @rc = -1;
	end catch 
  return @rc;  
end;
go

-- �������� ���� ������ 
create procedure EGH.DeleteGroundType (@������������� int)
as begin 
    declare @rc int  = @�������������;
    begin try 
	 delete ��������� where ������������� = @�������������;
	end try
	begin catch
	    set @rc = -1;
	end catch   
	return @rc;
end;
go

-- ��������� ���� ������  �� ���� 
create  procedure EGH.GetGroundTypeByCode(@������������� int) 
as begin 
    declare @rc int = -1;
	select	�������������,
			����������������������,  
			��������������,
			��������������������,
			������������������,
			������������,
			�����������������,
			�����������,
			�������������������,
			���������������,
			��������������,
			�����������������,
			����������������������
	from dbo.��������� where ������������� = @�������������;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

-- ��������� ������ ����� ������  
create procedure EGH.GetGroundTypeList
 as begin
	declare @rc int = -1;
	select	�������������, 
			����������������������,
			��������������,
			��������������������,
			������������������,
			������������,
			�����������������,
			�����������,
			�������������������,
			���������������,
			��������������,
			�����������������,
			����������������������
		from dbo.���������;
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go
-- ���������� ���� ������ 
create  procedure EGH.UpdateGroundType(
					@������������� int, 
					@���������������������� nvarchar(30),
					@�������������� real,
					@�������������������� real,
					@������������������ real,
					@������������ real,
					@����������������� real,
					@����������� real,
					@������������������� real,
					@��������������� real,
					@�������������� real,
					@����������������� real,
					@���������������������� real)
as begin 
    declare @rc int = -1;
	update  dbo.��������� 
	set 
	���������������������� = @����������������������,
	�������������� = @��������������,
	��������������������= @��������������������,
	������������������ = @������������������,
	������������= @������������,
	����������������� = @�����������������,
	����������� = @�����������,
	������������������� = @�������������������,
	��������������� = @���������������,
	�������������� = @��������������,
	����������������� = @�����������������,
	���������������������� = @����������������������
	where ������������� = @�������������; 
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go
-- ��������� ���������� ID ���� ������ 
create procedure EGH.GetNextGroundTypeCode(@������������� int output)
 as begin
	declare @rc int = -1;
	set @������������� = (select max(�������������)+1 from dbo.���������);
	set @rc = @@ROWCOUNT;
	return @rc;    
end;