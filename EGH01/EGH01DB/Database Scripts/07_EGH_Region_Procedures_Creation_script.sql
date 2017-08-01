----------------- Создание процедур --------- -------------------------------
---- Области
-----------------------------------------------------------------------------
---- Добавление области
---- Удаление области
---- Получение кода области
---- Получение списка областей
---- Обновление области
-----------------------------------------------------------------------------
use egh;
go

drop procedure EGH.CreateRegion;
drop procedure EGH.DeleteRegion; 
drop procedure EGH.GetRegionByCode;
drop procedure EGH.GetRegionList;
drop procedure EGH.UpdateRegion;
go
------------------------------------

-- Добавление области
create procedure EGH.CreateRegion(@Область nvarchar(50))
as begin 
declare @rc int  = 1;
	begin try
		insert into dbo.Область(Область) values(@Область);
	end try
	begin catch
	    set @rc = -1;
	end catch 
  return @rc;  
end;
go

-- Удаление области
create procedure EGH.DeleteRegion (@КодОбласти int)
as begin 
    declare @rc int  = @КодОбласти;
    begin try 
	 delete Область where КодОбласти = @КодОбласти;
	end try
	begin catch
	    set @rc = -1;
	end catch   
	return @rc;
end; 
go


-- Получение области по коду
create  procedure EGH.GetRegionByCode(@КодОбласти int) 
as begin 
    declare @rc int = -1;
	select 
			КодОбласти,
			Область 
	from dbo.Область where КодОбласти = @КодОбласти;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go


-- Получение списка областей
create procedure EGH.GetRegionList
 as begin
	declare @rc int = -1;
	select 
			КодОбласти,
			Область
			from dbo.Область 
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

---- Обновление области
create procedure EGH.UpdateRegion(@КодОбласти int, @Область nvarchar(50)) 
as begin 
    declare @rc int = -1;
	update dbo.Область 
	set 
		Область = @Область
	where КодОбласти = @КодОбласти;  
	set @rc = @@ROWCOUNT;
	return @rc;    
end;
go

