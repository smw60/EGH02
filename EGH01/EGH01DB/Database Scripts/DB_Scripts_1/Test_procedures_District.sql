------------- Проверка процедур District  ----------------------------------
-- create
declare @rc int = -1;
exec @rc = EGH.CreateDistrict @Область = 2, @Район = 'Test';
select @rc;
go
-- delete
declare @rc int = -1;
exec @rc = EGH.DeleteDistrict @КодРайона= 126;
select @rc;
go
-- by code
declare @rc int = -1;
exec @rc = EGH.GetDistrictByCode @КодРайона= 5;
select @rc;
go
-- list
exec EGH.GetDistrictList @Область = 2;
go

-- update
declare @rc int = -1;
exec @rc = EGH.UpdateDistrict @КодРайона= 1, @Область = 2, @Район = 'Барановичский111';
select @rc;
go

--- full list
exec EGH.GetDistrictListFull;
go
