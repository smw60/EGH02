
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



create view Map.EcoObjectLocal
as

SELECT        Obj_Id, name, geom, 'Локальные точечные эко объекты' AS type
FROM            [MAP].[Карта_Экообъектов_лок_point]
UNION ALL
SELECT        Obj_Id, name, geom, 'Локальные эко объекты в виде областей' AS type
FROM            [MAP].[Карта_Экообъектов_лок_poly];


create view Map.EcoObjectRepublic
as

SELECT        Obj_Id, name, geom, 'Точечные эко объекты республиканского значения' AS type
FROM            [MAP].[Карта_Экообъектов_респ_значения_point]
UNION ALL
SELECT        Obj_Id, name, geom, 'Эко объекты республиканского значения в виде областей' AS type
FROM            [MAP].[Карта_Экообъектов_респ_значения_poly]




CREATE PROCEDURE [MAP].[EcoOLocal] (@point varchar(50))
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText(@point, 4326);
DECLARE @rc int = -1;

BEGIN
SELECT Obj_Id, name, type FROM [MAP].[EcoObjectLocal] WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT;
RETURN @rc;
END;


CREATE PROCEDURE [MAP].[EcoRepublic] (@point varchar(50))
AS
DECLARE @geometry_point geometry = geometry::STGeomFromText(@point, 4326);
DECLARE @rc int = -1;

BEGIN
SELECT Obj_Id, name, type FROM [MAP].[EcoObjectRepublic] WHERE (SELECT geom.MakeValid().STContains(@geometry_point))=1;
SET @rc = @@ROWCOUNT;
RETURN @rc;
END;