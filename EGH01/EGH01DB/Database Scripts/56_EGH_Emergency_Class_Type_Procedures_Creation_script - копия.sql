----------------- Создание процедур --------- -------------------------------
---- Классификация аварий  (EmergencyClass)
-----------------------------------------------------------------------------
---- Добавление типа аварии
---- Удаление типа аварии 
---- Получение типа аварии
---- Получение типа аварии по коду
---- Обновление типа аварии
---- Получение следующего кода типа аварии 
---- Получение типа аварии по массе
-----------------------------------------------------------------------------
use egh;
go

drop procedure EGH.CreateEmergencyClass;
drop procedure EGH.DeleteEmergencyClass; 
drop procedure EGH.GetEmergencyClassByCode;
drop procedure EGH.GetEmergencyClassList;
drop procedure EGH.UpdateEmergencyClass;
drop procedure EGH.GetNextEmergencyClassCode;
go
--------------------------------------

---- Добавление типа аварии
create procedure EGH.CreateEmergencyClass (
					@КодТипаАварии int,  
					@НаименованиеТипаАварии nvarchar(max),
					@МинМасса real, 
					@МаксМасса real)
as begin 
declare @rc int  = @КодТипаАварии;
	begin try
		insert into dbo.КлассификацияАварий(
		КодТипаАварии, 
		НаименованиеТипаАварии,
		МинМасса,
		МаксМасса) 
		values(@КодТипаАварии, 
				@НаименованиеТипаАварии,
				@МинМасса, 
				@МаксМасса); 
	end try
	begin catch
	    set @rc = -1;
	end catch 
  return @rc;  
end;
go

---- Удаление типа аварии
create procedure EGH.DeleteEmergencyClass (@КодТипаАварии int)
as begin 
    declare @rc int  = @КодТипаАварии;
    begin try 
	 delete dbo.КлассификацияАварий 
	 where КодТипаАварии = @КодТипаАварии;
	end try
	begin catch
	    set @rc = -1;
	end catch   
	return @rc;
end; 
go

---- Получение типа аварии по коду
create  procedure EGH.GetEmergencyClassByCode(@КодТипаАварии int) 
as begin 
    declare @rc int = -1;
	select  
		КодТипаАварии,
		НаименованиеТипаАварии,
		МинМасса,
		МаксМасса
	from dbo.КлассификацияАварий 
	where КодТипаАварии = @КодТипаАварии;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

-- Получение списка типов аварии 
create procedure EGH.GetEmergencyClassList
 as begin
	declare @rc int = -1;
	select	КодТипаАварии,
			НаименованиеТипаАварии,
			МинМасса,
			МаксМасса
	from dbo.КлассификацияАварий;
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Обновление типа аварии 
create  procedure EGH.UpdateEmergencyClass(
						@КодТипаАварии int, 
						@НаименованиеТипаАварии nvarchar(max),
						@МинМасса real, 
						@МаксМасса real) 
as begin 
    declare @rc int = -1;
	update  dbo.КлассификацияАварий set
	 НаименованиеТипаАварии = @НаименованиеТипаАварии,
	 МинМасса = @МинМасса,
	 МаксМасса = @МаксМасса
	 where КодТипаАварии = @КодТипаАварии;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Получение следующего кода типа аварии 
create procedure EGH.GetNextEmergencyClassCode(@КодТипаАварии int output)
 as begin
	declare @rc int = -1;
	set @КодТипаАварии = 
		(select max(КодТипаАварии)+1 from dbo.КлассификацияАварий);
	set @rc = @@ROWCOUNT;
	if @КодТипаАварии is null 
	begin
		set @КодТипаАварии = 1;
		set @rc = 1;
	end;
	return @rc;    
end;
go


---- Получение типа аварии по массе
create  procedure EGH.GetEmergencyClassByMass(@Масса real) 
as begin 
    declare @rc int = -1;
	select  
		КодТипаАварии,
		НаименованиеТипаАварии,
		МинМасса,
		МаксМасса
	from dbo.КлассификацияАварий 
	where (МинМасса <= @Масса)  and (МаксМасса >= @Масса);  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

