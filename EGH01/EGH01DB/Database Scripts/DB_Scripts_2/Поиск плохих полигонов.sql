/****** Скрипт для команды SelectTopNRows из среды SSMS  ******/
SELECT [Obj_Id]
      ,[protection_grade]
      ,[protection_name]
      ,[geom]
  FROM [EGH].[MAP].[Карта_защищенности]


  drop table [EGH].[MAP].[Карта_защищенности];

  select * 
  into [EGH].[MAP].[Карта_защищенности]
  FROM [EGH].[MAP].[Карта_защищенности1] --36774

  -->15000
  -- <=3000 ok
  --> between 5500 and 5990
  --==14564
  delete [EGH].[MAP].[Карта_защищенности] where [Obj_Id]=14564
  delete [EGH].[MAP].[Карта_защищенности] where [Obj_Id]=4286
  delete [EGH].[MAP].[Карта_защищенности] where [Obj_Id]=12969
  delete [EGH].[MAP].[Карта_защищенности] where [Obj_Id]=6415
  delete [EGH].[MAP].[Карта_защищенности] where [Obj_Id]=5983
