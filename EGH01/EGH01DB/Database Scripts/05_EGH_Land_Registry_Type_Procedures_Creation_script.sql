----------------- Создание процедур --------- -------------------------------
---- Типы кадастрового назначения земель
-----------------------------------------------------------------------------
---- Добавление типа кадастрового назначения земель
---- Удаление типа кадастрового назначения земель
---- Получение типа кадастрового назначения земель по ID
---- Получение списка типов кадастрового назначения земель
---- Обновление типа кадастрового назначения земель
---- Получение следующего ID типа кадастрового назначения земель
-----------------------------------------------------------------------------

use egh;
go

drop procedure EGH.CreateLandRegistryType;
drop procedure EGH.DeleteLandRegistryType;
drop procedure EGH.GetLandRegistryTypeByCode;
drop procedure EGH.GetLandRegistryTypeList;
drop procedure EGH.UpdateLandRegistryType;
drop procedure EGH.GetNextLandRegistryTypeCode;
go

------------------------------------

-- Добавление типа кадастрового назначения земель
create procedure EGH.CreateLandRegistryType(
						@КодНазначенияЗемель int,  
						@НаименованиеНазначенияЗемель nvarchar(max),
						@ПДК real,
						@ПДКводы real,
						@НормДокументЗемля nvarchar(max),
						@НормДокументВода nvarchar(max))
as begin 
declare @rc int  = @КодНазначенияЗемель;
	begin try
		insert into dbo.НазначениеЗемель(
							КодНазначенияЗемель,
							НаименованиеНазначенияЗемель,
							ПДК,
							ПДКводы,
							НормДокументЗемля,
							НормДокументВода) 
				values (@КодНазначенияЗемель,  
						@НаименованиеНазначенияЗемель,
						@ПДК,
						@ПДКводы,
						@НормДокументЗемля,
						@НормДокументВода); 
	end try
	begin catch
	    set @rc = -1;
	end catch 
  return @rc;  
end;
go
-- Удаление типа кадастрового назначения земель
create procedure EGH.DeleteLandRegistryType (@КодНазначенияЗемель int)
as begin 
    declare @rc int  = @КодНазначенияЗемель;
    begin try 
	 delete dbo.НазначениеЗемель where КодНазначенияЗемель = @КодНазначенияЗемель;
	end try
	begin catch
	    set @rc = -1;
	end catch   
	return @rc;
end; 
go
-- Получение типа кадастрового назначения земель по ID
create  procedure EGH.GetLandRegistryTypeByCode(@КодНазначенияЗемель int)
as begin 
    declare @rc int = -1;
	select  КодНазначенияЗемель,
			НаименованиеНазначенияЗемель,
			ПДК,
			ПДКводы,
			НормДокументЗемля,
			НормДокументВода
		from  dbo.НазначениеЗемель
		where КодНазначенияЗемель = @КодНазначенияЗемель;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go
-- Получение списка типов кадастрового назначения земель
create procedure EGH.GetLandRegistryTypeList
 as begin
	declare @rc int = -1;
	select	КодНазначенияЗемель,
			НаименованиеНазначенияЗемель,
			ПДК,
			ПДКводы,
			НормДокументЗемля,
			НормДокументВода
	from dbo.НазначениеЗемель K
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go
-- Обновление типа кадастрового назначения земель
create  procedure EGH.UpdateLandRegistryType(
					@КодНазначенияЗемель int, 
					@Наименование nvarchar(max) output,
					@ЗначениеПДК real output,
					@ПДКводы real output,
					@НормДокументЗемля nvarchar(max) output,
					@НормДокументВода nvarchar(max) output)
as begin 
    declare @rc int = -1;
	update  dbo.НазначениеЗемель 
	set 
	НаименованиеНазначенияЗемель = @Наименование, 
	ПДК = @ЗначениеПДК,
	ПДКводы = @ПДКводы,
	НормДокументЗемля = @НормДокументЗемля,
	НормДокументВода = @НормДокументВода
	where КодНазначенияЗемель = @КодНазначенияЗемель;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go
-- Получение следующего ID типа кадастрового назначения земель
create procedure EGH.GetNextLandRegistryTypeCode(@КодНазначенияЗемель int output)
 as begin
	declare @rc int = -1;
	set @КодНазначенияЗемель =(select max(КодНазначенияЗемель)+1 from dbo.НазначениеЗемель);
	set @rc = @@ROWCOUNT;
	if @КодНазначенияЗемель is null 
		begin
			set @КодНазначенияЗемель = 1;
			set @rc = 1;
		end;
	return @rc;    
end;
go
