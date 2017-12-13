---1 -- 0 секунд
exec MAP.InRegionMap @point = 'Point(27.4421796312367 53.9043260781941)';

---2  -- 0 секунд
exec MAP.InPondMap @point = 'Point(27.4421796312367 53.9043260781941)';

---3  -- 0 секунд
exec MAP.InWaterIntakeZoneMap @point = 'Point(27.4421796312367 53.9043260781941)';

---4  --  1 секундв
exec MAP.InWaterProtectionMap @point = 'Point(27.4421796312367 53.9043260781941)';

---5 -- 7 секунд
exec MAP.InTimeMigrationMap @point = 'Point(27.4421796312367 53.9043260781941)';

---6 -- 0 секунд
exec MAP.InGroundWaterMap @point = 'Point(27.4421796312367 53.9043260781941)';

---7 -- 0 секунд
exec MAP.InRiverDensityMap @point = 'Point(27.4421796312367 53.9043260781941)';

---8 -- 0 секунд
exec MAP.InProtectGroundWaterMap @point = 'Point(27.4421796312367 53.9043260781941)';

---9 -- 0 секунд
exec MAP.InAerationrMap @point = 'Point(27.4421796312367 53.9043260781941)';

---10 -- 0 секунд
exec MAP.InCityMap @point = 'Point(27.4421796312367 53.9043260781941)';

---11 -- 0 секунд
exec MAP.InDownfallMap @point = 'Point(27.4421796312367 53.9043260781941)';

---12 -- 1 секунда
exec MAP.InSoilMap @point = 'Point(27.4421796312367 53.9043260781941)';

---13 -- 1 секунда
exec MAP.InVegetationMap @point = 'Point(27.4421796312367 53.9043260781941)';

---14 -- 23-28 секунд
exec MAP.InTopographyMap @point = 'Point(27.4421796312367 53.9043260781941)';

---15 -- 2 секунды
exec MAP.InGeologyMap @point = 'Point(27.4421796312367 53.9043260781941)';

---16 -- 2 секунды
exec MAP.InGeologyMap @point = 'Point(27.4421796312367 53.9043260781941)';

---17 -- 0 секунд
exec MAP.InSunRadiationMap @point = 'Point(27.4421796312367 53.9043260781941)';

---18 -- 0 секунд
exec MAP.InMonthTempMap @point = 'Point(27.4421796312367 53.9043260781941)';

---19 -- 1 секунда
exec MAP.QuatSedimentsMap @point = 'Point(27.4421796312367 53.9043260781941)';

---20 -- 0 секунд
exec MAP.EcoObjectLocalPoint @point = 'Point(27.4421796312367 53.9043260781941)';

---21 -- 0 секунд
exec MAP.EcoObjectLocalPoly @point = 'Point(27.4421796312367 53.9043260781941)';

---22 -- 0 секунд
exec MAP.EcoObjectNational @point = 'Point(27.4421796312367 53.9043260781941)';

---23 -- 0 секунд
exec MAP.EcoObjectRepublicPoint @point = 'Point(27.4421796312367 53.9043260781941)';

---24 -- 0 секунд
exec MAP.EcoObjectRepublicPoly @point = 'Point(27.4421796312367 53.9043260781941)';

---25 -- 5 секунд
exec MAP.InGroundProtectMap @point = 'Point(27.4421796312367 53.9043260781941)';

---26 -- 2 секунды
exec MAP.InGroundCoefMap @point = 'Point(27.4421796312367 53.9043260781941)';

---27 -- 5 секунд
exec MAP.EcoRepublic @point = 'Point(27.4421796312367 53.9043260781941)';

---28 -- 2 секунды
exec MAP.EcoLocal @point = 'Point(27.4421796312367 53.9043260781941)';
--29 buffer
exec [MAP].[EcoObjectInBuffer] @point = 'Point(27.4421796312367 53.9043260781941)', @buffer = 12000;








