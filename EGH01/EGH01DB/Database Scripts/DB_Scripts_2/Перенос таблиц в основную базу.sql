-- Перенос таблиц из тестовых баз в EGH
-- Перевод типа geography в тип geometry
-- Удаление ненужных таблиц
-- Проверка наличия первичного ключа
-- Создание spatial index (geom)

-- Карта водоемов - 874 объекта в EGH.PondMap

SELECT Obj_Id
      ,type
      ,elevation
      ,name
      ,geom
 INTO EGH.PondMap
 FROM EGH_spatial.dbo.Карта_водоемов;
GO

SELECT Obj_Id, type, name, GEOMETRY::STGeomFromText((geom.STAsText()), 4326) AS geom 
INTO EGH.PondMap
 FROM EGH.Pond_Map;
GO

DROP TABLE  EGH.Pond_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.PondMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO


-- Карта водоохранной зоны - 363 объекта в EGH.WaterProtectionMap

SELECT Obj_Id
       ,name
      ,geom
 INTO EGH.WaterProtection_Map
 FROM EGH_spatial.dbo.[Карта_водоохранной_зоны];
GO

SELECT Obj_Id, name, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.WaterProtectionMap
 FROM EGH.WaterProtection_Map;
GO

DROP TABLE  EGH.WaterProtection_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.WaterProtectionMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO


-- Карта Карта_время_миграции_нп_до_уровня_грунтовых_вод - 17802 объекта в EGH.TimeMigrationMap

SELECT Obj_Id
       ,time
      ,geom
 INTO EGH.TimeMigration_Map
 FROM EGH_spatial.dbo.[Карта_время_миграции_нп_до_уровня_грунтовых_вод];
GO

SELECT Obj_Id, time, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.TimeMigrationMap
 FROM EGH.TimeMigration_Map;
GO

DROP TABLE  EGH.TimeMigration_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.TimeMigrationMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO

-- Карта Карта_зоны_водозабора - 94 объекта в EGH.WaterIntakeZoneMap

SELECT Obj_Id
       ,[vodozabor]
	   ,[buff_dist]
      ,geom
 INTO EGH.WaterIntakeZone_Map
 FROM EGH_spatial.dbo.Карта_зоны_водозабора;
GO

SELECT Obj_Id,[vodozabor],[buff_dist], GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.WaterIntakeZoneMap
 FROM EGH.WaterIntakeZone_Map;
GO

DROP TABLE  EGH.WaterIntakeZone_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.WaterIntakeZoneMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO

-- Карта Карта_нас_пункты - 7926 объекта в EGH.CitiesMap

SELECT Obj_Id
       ,name
      ,geom
 INTO EGH.Cities_Map
 FROM EGH_spatial.dbo.Карта_нас_пункты;
GO

SELECT Obj_Id, name, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.CitiesMap
 FROM EGH.Cities_Map;
GO

DROP TABLE  EGH.Cities_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.CitiesMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO

-- Карта Карта_рек - 5830 объекта в EGH.RiversMap

SELECT Obj_Id, wide, name, type, state, geom
 INTO EGH.Rivers_Map
 FROM EGH_spatial.dbo.Карта_рек;
GO

SELECT  Obj_Id, wide, name, type, state, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.RiversMap
 FROM EGH.Rivers_Map;
GO

DROP TABLE  EGH.Rivers_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.RiversMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO


-- Карта Карта_самоочищение_почв - 3 объекта в EGH.GroundSelfCleaningMap

SELECT Obj_Id, [karta_4a], geom
 INTO EGH.GroundSelfCleaning_Map
 FROM EGH_spatial.dbo.Карта_самоочищение_почв;
GO

SELECT  Obj_Id, [karta_4a], GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.GroundSelfCleaningMap
 FROM EGH.GroundSelfCleaning_Map;
GO

DROP TABLE  EGH.GroundSelfCleaning_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.GroundSelfCleaningMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO



-- Карта Карта_уровня_грунтовых_вод - 5 объекта в EGH.GroundWaterMap

SELECT Obj_Id, h, geom
 INTO EGH.GroundWater_Map
 FROM EGH_spatial.dbo.Карта_уровня_грунтовых_вод;
GO

SELECT  Obj_Id, h, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.GroundWaterMap
 FROM EGH.GroundWater_Map;
GO

DROP TABLE  EGH.GroundWater_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.GroundWaterMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO

-- Карта Заказники_местн_знач_point - 307 объекта в EGH.EcoLocalPointMap

SELECT Obj_Id, name, geom
 INTO EGH.EcoLocalPoint_Map
 FROM EGH_spatial.dbo.Заказники_местн_знач_point;
GO

SELECT  Obj_Id, name, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.EcoLocalPointMap
 FROM EGH.EcoLocalPoint_Map;
GO

DROP TABLE  EGH.EcoLocalPoint_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.EcoLocalPointMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO

-- Карта Заказники_местн_знач_poligon - 43 объекта в EGH.EcoLocalPolyMap

SELECT Obj_Id, name, geom
 INTO EGH.EcoLocalPoly_Map
 FROM EGH_spatial.dbo.Заказники_местн_знач_poligon;
GO

SELECT  Obj_Id, name, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.EcoLocalPolyMap
 FROM EGH.EcoLocalPoly_Map;
GO

DROP TABLE  EGH.EcoLocalPoly_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.EcoLocalPolyMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO

-- Карта Заказники_республ_знач_point - 8 объекта в EGH.EcoRepublicPointMap

SELECT Obj_Id, type, name, geom
 INTO EGH.EcoRepublicPoint_Map
 FROM EGH_spatial.dbo.Заказники_республ_знач_point;
GO

SELECT  Obj_Id, type, name, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.EcoRepublicPointMap
 FROM EGH.EcoRepublicPoint_Map;
GO

DROP TABLE  EGH.EcoRepublicPoint_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.EcoRepublicPointMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO

-- Карта Нац_парки_и_Заказники_респ_знач_poligon - 118 объекта в EGH.EcoRepublicPolyMap

SELECT Obj_Id, type, name, geom
 INTO EGH.EcoRepublicPoly_Map
 FROM EGH_spatial.dbo.Нац_парки_и_Заказники_респ_знач_poligon;
GO

SELECT  Obj_Id, type, name, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.EcoRepublicPolyMap
 FROM EGH.EcoRepublicPoly_Map;
GO

DROP TABLE  EGH.EcoRepublicPoly_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.EcoRepublicPolyMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO

-- Карта Пам_природы_point - 333 объекта в EGH.EcoNationalPointMap

SELECT Obj_Id, type, subtype, city, name, geom
 INTO EGH.EcoNationalPoint_Map
 FROM EGH_spatial.dbo.Пам_природы_point;
GO

SELECT  Obj_Id, type, subtype, city, name, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.EcoNationalPointMap
 FROM EGH.EcoNationalPoint_Map;
GO

DROP TABLE  EGH.EcoNationalPoint_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.EcoNationalPointMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO

-- Карта Карта_пористости_влагоемк - 51970 объекта в EGH.KoefMap

SELECT Obj_Id, vlagoemkos, k_filtraci, poristost, geom
 INTO EGH.EGH.Koef_Map
 FROM EGH_spatial.dbo.Карта_пористости_влагоемк;
GO

SELECT  Obj_Id, vlagoemkos, k_filtraci, poristost, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.KoefMap
 FROM EGH.Koef_Map;
GO

DROP TABLE  EGH.Koef_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.KoefMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO

-- Карта Карта_почв - 40 объекта в EGH.SoilMap

SELECT Obj_Id, 
name, 
kf, 
p,
kv,
gumus,
klass,
tip_2, 
geom
 INTO EGH.EGH.Soil_Map
 FROM EGH_spatial.dbo.Карта_почв;
GO
select Obj_Id, 
name, 
kf, 
p,
kv,
gumus,
klass,
tip_2, 
GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.SoilMap
 FROM EGH.Soil_Map;
GO

DROP TABLE  EGH.Soil_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.SoilMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO

-- Карта Карта_районов - 118 объекта в EGH.RegionMap

SELECT Obj_Id, district, regeon, geom
 INTO EGH.EGH.Region_Map
 FROM EGH_spatial.dbo.Карта_районов;
GO

SELECT  Obj_Id, district, regeon, GEOMETRY::STGeomFromText((geom.STAsText()), 0) AS geom 
INTO EGH.Region_Map
 FROM EGH.RegionMap;
GO

DROP TABLE  EGH.Region_Map;
GO

CREATE SPATIAL INDEX Sp_ind_Map ON EGH.RegionMap (geom) 
 USING GEOMETRY_GRID 
 WITH (  
    BOUNDING_BOX = ( xmin=23, ymin=51, xmax=33, ymax=58 ),  
    GRIDS = (LOW, LOW, MEDIUM, HIGH),  
    CELLS_PER_OBJECT = 64,  
    PAD_INDEX  = ON );  
GO