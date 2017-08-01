----------------- Создание процедур --------- -------------------------------
---- Свойства воды 
---- Температура
---- Вязкость
---- Плотность
---- Коэффициент поверхностного натяжения
-----------------------------------------------------------------------------
---- Добавление значения температуры воды и соответствующих свойств
---- Удаление значения температуры воды и соответствующих свойств
---- Получение значения температуры воды и соответствующих свойств по коду
---- Получение списка значений температуры воды и соответствующих свойств
---- Обновление значения температуры воды и соответствующих свойств
---- Получение следующего кода для значения температуры воды и соответствующих свойств
---- Получение ближайшего большего значения температуры воды и соответствующих свойств по температуре
-----------------------------------------------------------------------------
use egh;
go

drop procedure EGH.CreateWaterProperties;
drop procedure EGH.DeleteWaterProperties; 
drop procedure EGH.GetWaterPropertiesByCode;
drop procedure EGH.GetWaterPropertiesList;
drop procedure EGH.UpdateWaterProperties;
drop procedure EGH.GetNextWaterPropertiesCode;
drop procedure EGH.GetWaterNearTemp;
go
------------------------------------

-- Добавление значения температуры воды и соответствующих свойств
create procedure EGH.CreateWaterProperties (
	@КодПоказателяВоды int, 
	@Температура real,
	@Вязкость real,
	@Плотность real,
	@КоэфПовНат real)
as begin 
declare @rc int  = @КодПоказателяВоды;
	begin try
		insert into dbo.Вода(КодПоказателяВоды, Температура, Вязкость, Плотность, КоэфПовНат) 
		values(@КодПоказателяВоды, @Температура, @Вязкость, @Плотность, @КоэфПовНат);
	end try
	begin catch
	    set @rc = -1;
	end catch 
  return @rc;  
end;
go

-- Удаление значения температуры воды и соответствующих свойств
create procedure EGH.DeleteWaterProperties (@КодПоказателяВоды int)
as begin 
    declare @rc int  = @КодПоказателяВоды;
    begin try 
	 delete Вода where КодПоказателяВоды = @КодПоказателяВоды;
	end try
	begin catch
	    set @rc = -1;
	end catch   
	return @rc;
end; 
go


-- Получение значения температуры воды и соответствующих свойств по коду
create  procedure EGH.GetWaterPropertiesByCode (@КодПоказателяВоды int) 
as begin 
    declare @rc int = -1;
	select 
		КодПоказателяВоды,
		Температура,
		Вязкость,
		Плотность,
		КоэфПовНат
	from dbo.Вода where КодПоказателяВоды = @КодПоказателяВоды;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go


-- Получение списка значений температуры воды и соответствующих свойств
create procedure EGH.GetWaterPropertiesList
 as begin
	declare @rc int = -1;
	select 
		КодПоказателяВоды, 
		Температура,
		Вязкость, 
		Плотность, 
		КоэфПовНат
	from dbo.Вода;
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Обновление значения температуры воды и соответствующих свойств
create  procedure EGH.UpdateWaterProperties(
					@КодПоказателяВоды int, 
					@Температура real,
					@Вязкость real,
					@Плотность real,
					@КоэфПовНат real) 
as begin 
    declare @rc int = -1;
	update  dbo.Вода set 
		Температура = @Температура, 
		Вязкость = @Вязкость,
		Плотность = @Плотность,
		КоэфПовНат = @КоэфПовНат
	where КодПоказателяВоды = @КодПоказателяВоды;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Получение следующего кода значения температуры воды и соответствующих свойств
create procedure EGH.GetNextWaterPropertiesCode(@КодПоказателяВоды int output)
 as begin
	declare @rc int = -1;
	set @КодПоказателяВоды= (select max(КодПоказателяВоды)+1 from dbo.Вода);
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Получение значения температуры воды и соответствующих свойств по ближайшей температуре
create procedure EGH.GetWaterNearTemp(@Температура real)
 as begin
	declare @rc int = -1;
select 
		КодПоказателяВоды, 
		Температура,
		Вязкость, 
		Плотность, 
		КоэфПовНат,
		Температура - @Температура delta
	from dbo.Вода
	where Температура - @Температура  = (
			select min(Температура-@Температура) 
			from dbo.Вода where Температура > @Температура);
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go