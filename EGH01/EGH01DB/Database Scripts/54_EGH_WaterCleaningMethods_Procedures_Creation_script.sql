----------------- Создание процедур --------- -------------------------------
---- Категории методов ликвидации загрязнения грунтовых вод (WaterCleaningMethods)
-----------------------------------------------------------------------------
---- Добавление категории методов ликвидации загрязнения грунтовых вод
---- Удаление категории методов ликвидации загрязнения грунтовых вод
---- Получение категории методов ликвидации загрязнения грунтовых вод
---- Получение категории методов ликвидации загрязнения грунтовых вод
---- Обновление категории методов ликвидации загрязнения грунтовых вод
---- Получение следующего кода категории методов ликвидации загрязнения грунтовых вод
-----------------------------------------------------------------------------
use egh;
go

drop procedure EGH.CreateWaterCleaningMethods;
drop procedure EGH.DeleteWaterCleaningMethods; 
drop procedure EGH.GetWaterCleaningMethodsByCode;
drop procedure EGH.GetWaterCleaningMethodsList;
drop procedure EGH.UpdateWaterCleaningMethods;
drop procedure EGH.GetNextWaterCleaningMethodsCode;
go
------------------------------------

---- Добавление категории методов ликвидации загрязнения грунтовых вод
create procedure EGH.CreateWaterCleaningMethods (
					@КодТипаКатегории int,  
					@ОписаниеМетода nvarchar(MAX))
as begin 
declare @rc int  = @КодТипаКатегории;
	begin try
		insert into dbo.КатегорияМЛЗагрязненияГВ(
		КодТипаКатегории, 
		ОписаниеМетода) 
		values(@КодТипаКатегории, 
				@ОписаниеМетода); 
	end try
	begin catch
	    set @rc = -1;
	end catch 
  return @rc;  
end;
go

---- Удаление категории методов ликвидации загрязнения грунтовых вод
create procedure EGH.DeleteWaterCleaningMethods (@КодТипаКатегории int)
as begin 
    declare @rc int  = @КодТипаКатегории;
    begin try 
	 delete dbo.КатегорияМЛЗагрязненияГВ where КодТипаКатегории = @КодТипаКатегории;
	end try
	begin catch
	    set @rc = -1;
	end catch   
	return @rc;
end; 
go

---- Получение категории методов ликвидации загрязнения грунтовых вод по коду
create  procedure EGH.GetWaterCleaningMethodsByCode(@КодТипаКатегории int) 
as begin 
    declare @rc int = -1;
	select  
		КодТипаКатегории,
		ОписаниеМетода
	from dbo.КатегорияМЛЗагрязненияГВ 
	where КодТипаКатегории = @КодТипаКатегории;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

-- Получение списка категории методов ликвидации загрязнения грунтовых вод
create procedure EGH.GetWaterCleaningMethodsList
 as begin
	declare @rc int = -1;
	select	КодТипаКатегории,
			ОписаниеМетода
	from dbo.КатегорияМЛЗагрязненияГВ;
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Обновление категории методов ликвидации загрязнения грунтовых вод
create  procedure EGH.UpdateWaterCleaningMethods(
						@КодТипаКатегории int, 
						@ОписаниеМетода nvarchar(max)) 
as begin 
    declare @rc int = -1;
	update  dbo.КатегорияМЛЗагрязненияГВ set
	 ОписаниеМетода = @ОписаниеМетода
	 where КодТипаКатегории = @КодТипаКатегории;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Получение следующего кода категории методов ликвидации загрязнения грунтовых вод
create procedure EGH.GetNextWaterCleaningMethodsCode(@КодТипаКатегории int output)
 as begin
	declare @rc int = -1;
	set @КодТипаКатегории = 
		(select max(КодТипаКатегории)+1 from dbo.КатегорияМЛЗагрязненияГВ);
	set @rc = @@ROWCOUNT;
	if @КодТипаКатегории is null 
	begin
		set @КодТипаКатегории = 1;
		set @rc = 1;
	end;
	return @rc;    
end;
go


