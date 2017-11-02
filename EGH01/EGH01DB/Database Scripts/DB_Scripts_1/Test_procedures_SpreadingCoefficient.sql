------------- Проверка процедур Spreading Coefficient ----------------------------------
-- create
declare @rc int = -1;
exec @rc = EGH.CreateSpreadingCoefficient 
			@ТипГрунта = 2,  
			@МинПролива = 0.1,
			@МаксПролива = 100.0,
			@МинУклона = 92.0,
			@МаксУклона =96.0,
			@КоэффициентРазлива = 4.0,
			@КодТипаНефтепродукта = 2;

select @rc;
go
-- delete
declare @rc int = -1;
exec @rc = EGH.DeleteSpreadingCoefficient  
			@ТипГрунта = 2,  
			@МинПролива = 20,
			@МаксПролива = 40,
			@МинУклона =1.5,
			@МаксУклона =4.6,
			@КоэффициентРазлива = 2;
select @rc;

go
-- by code
declare @rc int = -1;
exec @rc = EGH.GetSpreadingCoefficientByDelta 
			@ТипГрунта = 1,  
			@МинПролива = 20,
			@МаксПролива = 40,
			@МинУклона =1.5,
			@МаксУклона = 4.6;
select @rc;

-- list
exec EGH.GetSpreadingCoefficientList;
go
-- update
declare @rc int = -1;
exec @rc = EGH.UpdateSpreadingCoefficient
			@ТипГрунта = 1,  
			@МинПролива = 20,
			@МаксПролива = 60,
			@МинУклона =2,
			@МаксУклона =6,
			@КоэффициентРазлива = 20;
select @rc;
go

declare @rc int = -1;
exec @rc = EGH.UpdateSpreadingCoefficient
			@ТипГрунта = 1,  
			@МинПролива = 20,
			@МаксПролива = 60,
			@МинУклона = 2,
			@МаксУклона = 6,
			@КоэффициентРазлива = 250;
select @rc;
go
--------------------- get by data
declare @rc int = -1;
declare @c float = 0.0;
exec @rc =EGH.GetSpreadingCoefficientByData 
			@ТипГрунта = 1,  
			@Объем = 30,
			@УголНаклона = 4,
			@КоэффициентРазлива =@c output;
select @rc;

select @c;