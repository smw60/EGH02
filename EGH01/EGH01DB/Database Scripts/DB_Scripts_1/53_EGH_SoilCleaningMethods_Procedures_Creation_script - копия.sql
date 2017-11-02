----------------- Создание процедур --------- -------------------------------
---- Категории методов ликвидации загрязнения грунта (SoilCleaningMethods)
-----------------------------------------------------------------------------
---- Добавление категории методов ликвидации загрязнения грунта
---- Удаление категории методов ликвидации загрязнения грунта
---- Получение категории методов ликвидации загрязнения грунта
---- Получение категории методов ликвидации загрязнения грунта
---- Обновление категории методов ликвидации загрязнения грунта
---- Получение следующего кода категории методов ликвидации загрязнения грунта
-----------------------------------------------------------------------------
use egh;
go

drop procedure EGH.CreateSoilCleaningMethods;
drop procedure EGH.DeleteSoilCleaningMethods; 
drop procedure EGH.GetSoilCleaningMethodsByCode;
drop procedure EGH.GetSoilCleaningMethodsList;
drop procedure EGH.UpdateSoilCleaningMethods;
drop procedure EGH.GetNextSoilCleaningMethodsCode;
go
------------------------------------

---- Добавление категории методов ликвидации загрязнения грунта
create procedure EGH.CreateSoilCleaningMethods (
					@КодТипаКатегории int,
					@ОписаниеМетода nvarchar(MAX))
as begin 
declare @rc int  = @КодТипаКатегории;
	begin try
		insert into dbo.КатегорияМЛЗагрязненияПГ(
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

---- Удаление категории методов ликвидации загрязнения грунта
create procedure EGH.DeleteSoilCleaningMethods (@КодТипаКатегории int)
as begin 
    declare @rc int  = @КодТипаКатегории;
    begin try 
	 delete dbo.КатегорияМЛЗагрязненияПГ where КодТипаКатегории = @КодТипаКатегории;
	end try
	begin catch
	    set @rc = -1;
	end catch   
	return @rc;
end; 
go

---- Получение категории методов ликвидации загрязнения грунта по коду
create  procedure EGH.GetSoilCleaningMethodsByCode(@КодТипаКатегории int) 
as begin 
    declare @rc int = -1;
	select  
		КодТипаКатегории,
		ОписаниеМетода
	from dbo.КатегорияМЛЗагрязненияПГ 
	where КодТипаКатегории = @КодТипаКатегории;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

-- Получение списка категории методов ликвидации загрязнения грунта
create procedure EGH.GetSoilCleaningMethodsList
 as begin
	declare @rc int = -1;
	select	КодТипаКатегории,
			ОписаниеМетода
	from dbo.КатегорияМЛЗагрязненияПГ;
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Обновление категории методов ликвидации загрязнения грунта
create  procedure EGH.UpdateSoilCleaningMethods(
						@КодТипаКатегории int, 
						@ОписаниеМетода nvarchar(max)) 
as begin 
    declare @rc int = -1;
	update  dbo.КатегорияМЛЗагрязненияПГ set
	 ОписаниеМетода = @ОписаниеМетода
	 where КодТипаКатегории = @КодТипаКатегории;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Получение следующего кода категории методов ликвидации загрязнения грунта
create procedure EGH.GetNextSoilCleaningMethodsCode(@КодТипаКатегории int output)
 as begin
	declare @rc int = -1;
	set @КодТипаКатегории = 
		(select max(КодТипаКатегории)+1 from dbo.КатегорияМЛЗагрязненияПГ);
	set @rc = @@ROWCOUNT;
	if @КодТипаКатегории is null 
	begin
		set @КодТипаКатегории = 1;
		set @rc = 1;
	end;
	return @rc;    
end;
go



