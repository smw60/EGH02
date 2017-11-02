------------- Проверка процедур Region  ----------------------------------
-- create
declare @rc int = -1;
exec @rc = EGH.CreateRegion @Область = 'Test';
select @rc;
go
-- delete
declare @rc int = -1;
exec @rc = EGH.DeleteRegion @КодОбласти= 7;
select @rc;
go
-- by code
declare @rc int = -1;
exec @rc = EGH.GetRegionByCode @КодОбласти= 5;
select @rc;
go
-- list
exec EGH.GetRegionList;
go

-- update
declare @rc int = -1;
exec @rc = EGH.UpdateRegion @КодОбласти= 1, @Область ='Брестская';
select @rc;
go
