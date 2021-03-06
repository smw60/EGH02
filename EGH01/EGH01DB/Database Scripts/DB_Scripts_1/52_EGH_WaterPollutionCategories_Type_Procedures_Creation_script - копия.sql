----------------- �������� �������� --------- -------------------------------
---- ��������� ����������� ��������� ��� (WaterPollutionCategories)
-----------------------------------------------------------------------------
---- ���������� ��������� ����������� ��������� ��� 
---- �������� ��������� ����������� ��������� ��� 
---- ��������� ��������� ����������� ��������� ��� 
---- ��������� ��������� ����������� ��������� ��� 
---- ���������� ��������� ����������� ��������� ��� 
---- ��������� ���������� ���� ��������� ����������� ��������� ��� 
-----------------------------------------------------------------------------
use egh;
go

drop procedure EGH.CreateWaterPollutionCategories;
drop procedure EGH.DeleteWaterPollutionCategories; 
drop procedure EGH.GetWaterPollutionCategoriesByCode;
drop procedure EGH.GetWaterPollutionCategoriesList;
drop procedure EGH.UpdateWaterPollutionCategories;
drop procedure EGH.GetNextWaterPollutionCategoriesCode;
drop procedure EGH.GetSoilPollutionCategoriesByVolume_Cadastre;
go
------------------------------------

---- ���������� ��������� ����������� ��������� ��� 
create procedure EGH.CreateWaterPollutionCategories (
					@������������������������� int,  
					@���������������������������������� nvarchar(100),
					@����������� real, 
					@������������ real,
					@������������������� int )
as begin 
declare @rc int  = @�������������������������;
	begin try
		insert into dbo.��������������������������������(
		�������������������������, 
		����������������������������������,
		�����������,
		������������,
		�������������������) 
		values(@�������������������������, 
				@����������������������������������,
				@�����������, 
				@������������,
				@�������������������); 
	end try
	begin catch
	    set @rc = -1;
	end catch 
  return @rc;  
end;
go

---- �������� ��������� ����������� ��������� ��� 
create procedure EGH.DeleteWaterPollutionCategories (@������������������������� int)
as begin 
    declare @rc int  = @�������������������������;
    begin try 
	 delete dbo.�������������������������������� where ������������������������� = @�������������������������;
	end try
	begin catch
	    set @rc = -1;
	end catch   
	return @rc;
end; 
go

---- ��������� ��������� ����������� ��������� ���  �� ����
create  procedure EGH.GetWaterPollutionCategoriesByCode(@������������������������� int) 
as begin 
    declare @rc int = -1;
	select  
		�������������������������,
		����������������������������������,
		�����������,
		������������,
		W.������������������� �������������������,
		����������������������������,
		���,
		�������,
		�����������������,
		����������������
	from dbo.�������������������������������� W
	inner join dbo.���������������� C on W.������������������� = C.�������������������
	where ������������������������� = @�������������������������;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

-- ��������� ������ ��������� ����������� ��������� ��� 
create procedure EGH.GetWaterPollutionCategoriesList
 as begin
	declare @rc int = -1;
	select	�������������������������,
			����������������������������������,
			�����������,
			������������,
			W.������������������� �������������������,
			����������������������������,
			���,
			�������,
			�����������������,
			����������������
	from dbo.�������������������������������� W
	inner join dbo.���������������� C on W.������������������� = C.�������������������;
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- ���������� ���� ��������� ����������� ��������� ���  
create  procedure EGH.UpdateWaterPollutionCategories(
						@������������������������� int, 
						@���������������������������������� nvarchar(100),
						@����������� real, 
						@������������ real,
						@������������������� int) 
as begin 
    declare @rc int = -1;
	update  dbo.�������������������������������� set
	 ���������������������������������� = @����������������������������������,
	 ����������� = @�����������,
	 ������������ = @������������,
	 ������������������� = @�������������������
	 where ������������������������� = @�������������������������;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- ��������� ���������� ���� ��������� ����������� ��������� ���  
create procedure EGH.GetNextWaterPollutionCategoriesCode(@������������������������� int output)
 as begin
	declare @rc int = -1;
	set @������������������������� = 
		(select max(�������������������������)+1 from dbo.��������������������������������);
	set @rc = @@ROWCOUNT;
	if @������������������������� is null 
	begin
		set @������������������������� = 1;
		set @rc = 1;
	end;
	return @rc;    
end;
go

--- ��������� ��������� ����������� ��������� ��� �� ������ ����������� � ���� ��������
create  procedure EGH.GetWaterPollutionCategoriesByVolume_Cadastre(@����� float, @������������������� int) 
as begin 
    declare @rc int = -1;
	select  
		�������������������������,
		����������������������������������,
		�����������,
		������������,
		C.������������������� �������������������,
		����������������������������,
		���,
		�������,
		�����������������,
		����������������
	from dbo.�������������������������������� C
	inner join dbo.���������������� L on C.������������������� = L.�������������������
		where C.������������������� = @�������������������
		and (@����� >= �����������) and (@����� <=������������);  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go




