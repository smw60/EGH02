----------------- Создание процедур --------- -------------------------------
---- Категория водоохранной территории - WaterProtectionArea
-----------------------------------------------------------------------------
---- Добавление категории водоохранной территории
---- Удаление категории водоохранной территории
---- Получение категории водоохранной территории по коду
---- Получение списка типов водоохранной территории
---- Обновление категории водоохранной территории
---- Получение следующего кода категории водоохранной территории
-----------------------------------------------------------------------------
use egh;
go
--drop procedure EGH.CreateWaterProtectionArea;
--drop procedure EGH.DeleteWaterProtectionArea;
--drop procedure EGH.GetWaterProtectionAreaByCode;
--drop procedure EGH.GetWaterProtectionAreaList;
--drop procedure EGH.UpdateWaterProtectionArea;
--drop procedure EGH.GetNextWaterProtectionAreaCode;
--go
------------------------------------

-- Добавление категории водоохранной территории
create procedure EGH.CreateWaterProtectionArea(
						@КодТипаКатегории int,  
						@НаименованиеКатегории nvarchar(max))
as begin 
declare @rc int  = @КодТипаКатегории;
	begin try
		insert into dbo.КатегорияВодоохраннойТерритории(
							КодТипаКатегории,
							НаименованиеКатегории) 
				values(@КодТипаКатегории,
					   @НаименованиеКатегории); 
	end try
	begin catch
	    set @rc = -1;
	end catch 
  return @rc;  
end;
go

-- Удаление категории водоохранной территории
create procedure EGH.DeleteWaterProtectionArea (@КодТипаКатегории int)
as begin 
    declare @rc int  = @КодТипаКатегории;
    begin try 
	 delete dbo.КатегорияВодоохраннойТерритории 
	 where КодТипаКатегории = @КодТипаКатегории;
	end try
	begin catch
	    set @rc = -1;
	end catch   
	return @rc;
end; 
go

-- Получение категории водоохранной территории по ID
create  procedure EGH.GetWaterProtectionAreaByCode(@КодТипаКатегории int) 
as begin 
    declare @rc int = -1;
	select  НаименованиеКатегории
	from dbo.КатегорияВодоохраннойТерритории 
	where КодТипаКатегории = @КодТипаКатегории;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

-- Получение списка типов водоохранной территории
create procedure EGH.GetWaterProtectionAreaList
 as begin
	declare @rc int = -1;
	select	КодТипаКатегории,
			НаименованиеКатегории
	from dbo.КатегорияВодоохраннойТерритории;
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

-- Получение следующего значения категории водоохранной территории
create procedure EGH.GetNextWaterProtectionAreaCode(@КодТипаКатегории int output)
 as begin
	declare @rc int = -1;
	set @КодТипаКатегории = 
	(select	max(КодТипаКатегории) +1 from dbo.КатегорияВодоохраннойТерритории);
	set @rc = @@ROWCOUNT;
	if @КодТипаКатегории is null 
	begin
		set @КодТипаКатегории = 1;
		set @rc = 1;
	end;
	return @rc;    
end;
go
---- Обновление категории водоохранной территории
create  procedure EGH.UpdateWaterProtectionArea(
						@КодТипаКатегории int, 
						@НаименованиеКатегории nvarchar(max)) 
as begin 
    declare @rc int = -1;
	update  dbo.КатегорияВодоохраннойТерритории set
			НаименованиеКатегории = @НаименованиеКатегории
	from dbo.КатегорияВодоохраннойТерритории 
	where КодТипаКатегории = @КодТипаКатегории;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go


