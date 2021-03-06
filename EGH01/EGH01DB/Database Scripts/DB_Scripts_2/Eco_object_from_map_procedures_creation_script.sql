------------------------------------------------------
create PROCEDURE [MAP].EcoObjectLocalPoint (@point varchar(50))
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText(@point, 4326);
DECLARE @rc int = -1;

BEGIN
SELECT Obj_Id, name FROM [MAP].[Карта_Экообъектов_лок_point] WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT;
RETURN @rc;
END;
go
------------------------------------------------------
create PROCEDURE [MAP].EcoObjectLocalPoly (@point varchar(50))
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText(@point, 4326);
DECLARE @rc int = -1;

BEGIN
SELECT Obj_Id, name FROM [MAP].[Карта_Экообъектов_лок_poly] WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT;
RETURN @rc;
END;
go
------------------------------------------------------
create PROCEDURE [MAP].EcoObjectNational (@point varchar(50))
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText(@point, 4326);
DECLARE @rc int = -1;

BEGIN
SELECT Obj_Id, name FROM [MAP].[Карта_Экообъектов_нацпарки] WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT;
RETURN @rc;
END;
go
------------------------------------------------------
create PROCEDURE [MAP].EcoObjectRepublicPoint (@point varchar(50))
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText(@point, 4326);
DECLARE @rc int = -1;

BEGIN
SELECT Obj_Id, name FROM [MAP].Карта_Экообъектов_респ_значения_point WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT;
RETURN @rc;
END;
go
------------------------------------------------------
create PROCEDURE [MAP].EcoObjectRepublicPoly (@point varchar(50))
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText(@point, 4326);
DECLARE @rc int = -1;

BEGIN
SELECT Obj_Id, name FROM [MAP].[Карта_Экообъектов_респ_значения_poly] WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT;
RETURN @rc;
END;
go

------------------------------------------------------
alter PROCEDURE [MAP].EcoObjectInBuffer (@point varchar(50), @buffer int)
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText(@point, 4326);
DECLARE @distance real = @buffer/111000;
DECLARE @rc int = -1;

BEGIN
SELECT Obj_Id, name, type FROM [MAP].[EcoObjectMap] WHERE (SELECT @geometry_point.STBuffer(@distance).STIntersects(geom))=1;
SET @rc = @@ROWCOUNT;
RETURN @rc;
END;
go