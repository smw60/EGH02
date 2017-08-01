----------------- Создание процедур --------- -------------------------------
---- Категории проникновения нефтепродукта (PenetrationDepth)
-----------------------------------------------------------------------------
---- Добавление категории проникновения нефтепродукта 
---- Удаление категории проникновения нефтепродукта 
---- Получение категории проникновения нефтепродукта 
---- Получение категории проникновения нефтепродукта 
---- Обновление категории проникновения нефтепродукта 
---- Получение следующего кода категории проникновения нефтепродукта 
---- Получение категории проникновения нефтепродукта по глубине проникновения
-----------------------------------------------------------------------------
use egh;
go

drop procedure EGH.CreatePenetrationDepth;
drop procedure EGH.DeletePenetrationDepth; 
drop procedure EGH.GetPenetrationDepthByCode;
drop procedure EGH.GetPenetrationDepthList;
drop procedure EGH.UpdatePenetrationDepth;
drop procedure EGH.GetNextPenetrationDepthCode;
go
------------------------------------

---- Добавление категории проникновения нефтепродукта 
create procedure EGH.CreatePenetrationDepth (
					@КодТипаКатегории int,  
					@НаименованиеТипаКатегории nvarchar(max),
					@МинДиапазон real, 
					@МаксДиапазон real)
as begin 
declare @rc int  = @КодТипаКатегории;
	begin try
		insert into dbo.КатегорияПроникновенияНефтепродукта(
		КодТипаКатегории, 
		НаименованиеТипаКатегории,
		МинДиапазон,
		МаксДиапазон) 
		values(@КодТипаКатегории, 
				@НаименованиеТипаКатегории,
				@МинДиапазон, 
				@МаксДиапазон); 
	end try
	begin catch
	    set @rc = -1;
	end catch 
  return @rc;  
end;
go

---- Удаление категории проникновения нефтепродукта 
create procedure EGH.DeletePenetrationDepth (@КодТипаКатегории int)
as begin 
    declare @rc int  = @КодТипаКатегории;
    begin try 
	 delete dbo.КатегорияПроникновенияНефтепродукта 
	 where КодТипаКатегории = @КодТипаКатегории;
	end try
	begin catch
	    set @rc = -1;
	end catch   
	return @rc;
end; 
go

---- Получение категории проникновения нефтепродукта  по коду
create  procedure EGH.GetPenetrationDepthByCode(@КодТипаКатегории int) 
as begin 
    declare @rc int = -1;
	select  
		КодТипаКатегории,
		НаименованиеТипаКатегории,
		МинДиапазон,
		МаксДиапазон
	from dbo.КатегорияПроникновенияНефтепродукта 
	where КодТипаКатегории = @КодТипаКатегории;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

-- Получение списка категории проникновения нефтепродукта 
create procedure EGH.GetPenetrationDepthList
 as begin
	declare @rc int = -1;
	select	КодТипаКатегории,
			НаименованиеТипаКатегории,
			МинДиапазон,
			МаксДиапазон
	from dbo.КатегорияПроникновенияНефтепродукта;
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Обновление типа категории проникновения нефтепродукта 
create  procedure EGH.UpdatePenetrationDepth(
						@КодТипаКатегории int, 
						@НаименованиеТипаКатегории nvarchar(max),
						@МинДиапазон real, 
						@МаксДиапазон real) 
as begin 
    declare @rc int = -1;
	update  dbo.КатегорияПроникновенияНефтепродукта set
	 НаименованиеТипаКатегории = @НаименованиеТипаКатегории,
	 МинДиапазон = @МинДиапазон,
	 МаксДиапазон = @МаксДиапазон
	 where КодТипаКатегории = @КодТипаКатегории;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Получение следующего кода категории проникновения нефтепродукта 
create procedure EGH.GetNextPenetrationDepthCode(@КодТипаКатегории int output)
 as begin
	declare @rc int = -1;
	set @КодТипаКатегории = 
		(select max(КодТипаКатегории)+1 from dbo.КатегорияПроникновенияНефтепродукта);
	set @rc = @@ROWCOUNT;
	if @КодТипаКатегории is null 
	begin
		set @КодТипаКатегории = 1;
		set @rc = 1;
	end;
	return @rc;    
end;
go


---- Получение категории проникновения нефтепродукта по глубине проникновения
create  procedure EGH.GetPenetrationDepthByDepth(@Глубина real) 
as begin 
    declare @rc int = -1;
	select  
		КодТипаКатегории,
		НаименованиеТипаКатегории,
		МинДиапазон,
		МаксДиапазон
	from dbo.КатегорияПроникновенияНефтепродукта 
	where (МинДиапазон <= @Глубина) and (МаксДиапазон >= @Глубина);  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go





