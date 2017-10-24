------------- Проверка процедур WaterProperties ----------------------------------

-- create
declare @rc int = -1;
exec @rc = EGH.CreateWaterProperties 
				@КодПоказателяВоды = 10, 
				@Температура = 80.0,
				@Вязкость = 130,
				@Плотность =8.98,
				@КоэфПовНат =0.68;
select @rc;
go

insert into dbo.Вода(КодПоказателяВоды, Температура, Вязкость, Плотность, КоэфПовНат) 
		values(10, 80.0, 13, 8.98, .68);
-- delete
declare @rc int = -1;
exec @rc = EGH.DeleteWaterProperties @КодПоказателяВоды = 10;
select @rc;
go
-- by code
declare @rc int = -1;
exec @rc = EGH.GetWaterPropertiesByCode @КодПоказателяВоды= 2;
select @rc;
go
-- list
exec EGH.GetWaterPropertiesList;
-- update
declare @rc int = -1;
exec @rc = EGH.UpdateWaterProperties 
				@КодПоказателяВоды = 10, 
				@Температура = 80.0,
				@Вязкость = 4350,
				@Плотность =8.98,
				@КоэфПовНат =0.68;
select @rc;
go
-- next
declare @rc int = -1;
exec EGH.GetNextWaterPropertiesCode @КодПоказателяВоды=@rc output;
select @rc;
go
--near temp
declare @rc int = -1;
exec @rc = EGH.GetWaterNearTemp @Температура= 17;
select @rc;
go