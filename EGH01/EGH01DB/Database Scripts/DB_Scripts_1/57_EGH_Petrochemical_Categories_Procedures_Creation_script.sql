----------------- Создание процедур --------- -------------------------------
---- Категория нефтепродукта - PetrochemicalCategories
-----------------------------------------------------------------------------
---- Добавление категории нефтепродукта
---- Удаление категории нефтепродукта
---- Получение категории нефтепродукта по коду
---- Получение списка типов нефтепродукта
---- Обновление категории нефтепродукта
---- Получение следующего кода категории нефтепродукта
-----------------------------------------------------------------------------
use egh;
go
--drop procedure EGH.CreatePetrochemicalCategories;
--drop procedure EGH.DeletePetrochemicalCategories;
--drop procedure EGH.GetPetrochemicalCategoriesByCode;
--drop procedure EGH.GetPetrochemicalCategoriesList;
--drop procedure EGH.UpdatePetrochemicalCategories;
--drop procedure EGH.GetNextPetrochemicalCategoriesCode;
--go
------------------------------------

-- Добавление категории нефтепродукта
create procedure EGH.CreatePetrochemicalCategories(
						@КодКатегорииНефтепродукта int,  
						@НаименованиеКатегорииНефтепродукта nvarchar(max))
as begin 
declare @rc int  = @КодКатегорииНефтепродукта;
	begin try
		insert into dbo.Категория_Нефтепродукта(
							КодКатегорииНефтепродукта,
							НаименованиеКатегорииНефтепродукта) 
				values(@КодКатегорииНефтепродукта,
					   @НаименованиеКатегорииНефтепродукта); 
	end try
	begin catch
	    set @rc = -1;
	end catch 
  return @rc;  
end;
go

-- Удаление категории нефтепродукта
create procedure EGH.DeletePetrochemicalCategories (@КодКатегорииНефтепродукта int)
as begin 
    declare @rc int  = @КодКатегорииНефтепродукта;
    begin try 
	 delete dbo.Категория_Нефтепродукта 
	 where КодКатегорииНефтепродукта = @КодКатегорииНефтепродукта;
	end try
	begin catch
	    set @rc = -1;
	end catch   
	return @rc;
end; 
go

-- Получение категории нефтепродукта по ID
create  procedure EGH.GetPetrochemicalCategoriesByCode(@КодКатегорииНефтепродукта int) 
as begin 
    declare @rc int = -1;
	select  НаименованиеКатегорииНефтепродукта
	from dbo.Категория_Нефтепродукта 
	where КодКатегорииНефтепродукта = @КодКатегорииНефтепродукта;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

-- Получение списка типов нефтепродукта
create procedure EGH.GetPetrochemicalCategoriesList
 as begin
	declare @rc int = -1;
	select	КодКатегорииНефтепродукта,
			НаименованиеКатегорииНефтепродукта
	from dbo.Категория_Нефтепродукта;
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

-- Получение следующего значения категории нефтепродукта
create procedure EGH.GetNextPetrochemicalCategoriesCode(@КодКатегорииНефтепродукта int output)
 as begin
	declare @rc int = -1;
	set @КодКатегорииНефтепродукта = 
	(select	max(КодКатегорииНефтепродукта) +1 from dbo.Категория_Нефтепродукта);
	set @rc = @@ROWCOUNT;
	if @КодКатегорииНефтепродукта is null 
	begin
		set @КодКатегорииНефтепродукта = 1;
		set @rc = 1;
	end;
	return @rc;    
end;
go
---- Обновление категории нефтепродукта
create  procedure EGH.UpdatePetrochemicalCategories(
						@КодКатегорииНефтепродукта int, 
						@НаименованиеКатегорииНефтепродукта nvarchar(max)) 
as begin 
    declare @rc int = -1;
	update  dbo.Категория_Нефтепродукта set
			НаименованиеКатегорииНефтепродукта = @НаименованиеКатегорииНефтепродукта
	from dbo.Категория_Нефтепродукта 
	where КодКатегорииНефтепродукта = @КодКатегорииНефтепродукта;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go


