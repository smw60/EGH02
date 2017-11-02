-----------------------------------------------------------------------------------------------
---        Процедуры проверки попадания точки в область на карте                           ----
---     возвращают атрибуты объекта geom для соответствующей карты                         ----
---		возвращают значения 0, 0.0, пустая строка для соответствующих типов данных         ----
---		при отсутствии такого объекта											           ----
-----------------------------------------------------------------------------------------------
DROP PROCEDURE EGH.InCity;
DROP PROCEDURE EGH.InGroundSelfCleaningMap;
DROP PROCEDURE EGH.InGroundWaterMap;
DROP PROCEDURE EGH.InRegionMap;
DROP PROCEDURE EGH.InSoilMap;
GO

-- Для карты EGH.CitiesMap возвращается Id области, name - наименование города
CREATE PROCEDURE EGH.InCity (@point1 varchar(30), @point2 varchar(30),  @OutId int output,  @OutName varchar(150) output)
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText('POINT('+@point1+' '+@point2+')', 4326);
DECLARE @rc int = -1;

BEGIN
SELECT @OutId = Obj_Id,
	   @OutName = name  
	   FROM EGH.CitiesMap WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT;
RETURN @rc;
END
GO

-- Для карты EGH.GroundSelfCleaningMap возвращается Id области, type - тип самоочищения почвы
CREATE PROCEDURE EGH.InGroundSelfCleaningMap (@point1 varchar(30), @point2 varchar(30),  @OutId int output,  @OutType varchar(150) output)
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText('POINT('+@point1+' '+@point2+')', 4326);
DECLARE @rc int = -1;

BEGIN
SELECT @OutId = Obj_Id,
	   @OutType = type  
	   FROM EGH.GroundSelfCleaningMap WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT; 
RETURN @rc;
END
GO

-- Для карты EGH.GroundWaterMap возвращается Id области, h - глубина грунтовых вод в метрах
CREATE PROCEDURE EGH.InGroundWaterMap (@point1 varchar(30), @point2 varchar(30),  @OutId int output,  @Outh float output)
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText('POINT('+@point1+' '+@point2+')', 4326);
DECLARE @rc int = -1;

BEGIN
SELECT @OutId = Obj_Id,
	   @Outh = h  
	   FROM EGH.GroundWaterMap WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT; 
RETURN @rc;
END
GO

-- Для карты EGH.RegionMap возвращается Id области, district - район, region - область
CREATE PROCEDURE EGH.InRegionMap (@point1 varchar(30), @point2 varchar(30), 
				@OutId int output, @OutDistrict varchar(50) output,  @OutRegion varchar(50) output)
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText('POINT('+@point1+' '+@point2+')', 4326);
DECLARE @rc int = -1;

BEGIN
SELECT @OutId = Obj_Id,
	   @OutDistrict = district,
	   @OutRegion = region
	   FROM EGH.RegionMap WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT; 
RETURN @rc;
END
GO

-- Для карты EGH.SoilMap возвращается Id области, 
-- name - наименование почв, 
-- kf - коэффициент фильтрации,
-- p - коэффициент пористости, 
-- kv - коэффициент влагоемкости,
-- gumus - высота почвенного покрова в см, 
-- klass - класс почв по классификатору,
-- type - тип почв по классификатору

CREATE PROCEDURE EGH.InSoilMap (@point1 varchar(30), @point2 varchar(30), 
				@OutId int output, 
				@OutName varchar(50) output, 
				@OutFiltration float output,
				@OutPoristost float output,
				@OutVlagoemkost float output,
				@OutGumusDepth float output,
				@OutKlass varchar(50) output, 
				@OutType varchar(50) output)
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText('POINT('+@point1+' '+@point2+')', 4326);
DECLARE @rc int = -1;

BEGIN
SELECT	@OutId = Obj_Id,
		@OutName = name, 
		@OutFiltration = kf,
		@OutPoristost = p,
		@OutVlagoemkost = kv,
		@OutGumusDepth = gumus,
		@OutKlass = klass, 
		@OutType = type
FROM EGH.SoilMap WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT; 
RETURN @rc;
END
GO
