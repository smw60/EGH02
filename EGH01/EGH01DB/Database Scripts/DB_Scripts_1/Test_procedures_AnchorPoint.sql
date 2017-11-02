------------- ѕроверка процедур Anchor Point ----------------------------------
-- create
declare @rc int = -1;
exec @rc = EGH.CreateAnchorPoint
		    @Idќпорной√еологической“очки = 1 ,  
			@Ўирота√рад = 26.6, 
			@ƒолгота√рад = 56.8,
			@“ип√рунта = 1,
			@√лубина√рунтовых¬од = 3.7,
			@¬ысота”ровнемћор€ = 150.2,
			@ одЌазначени€«емель = 3;
select @rc;
go
-- delete
declare @rc int = -1;
exec @rc = EGH.DeleteAnchorPoint @Idќпорной√еологической“очки = 2;
select @rc;
go
-- by ID
declare @rc int = -1;
exec @rc = EGH.GetAnchorPointByID @Idќпорной√еологической“очки = 2;
select @rc;
go
-- list
exec EGH.GetAnchorPointList;
go
-- update
declare @rc int = -1;
exec @rc = EGH.UpdateAnchorPoint
			@Idќпорной√еологической“очки =2 ,  
			@Ўирота√рад = 26.6, 
			@ƒолгота√рад = 56.8,
			@“ип√рунта = 1,
			@√лубина√рунтовых¬од = 3.7,
			@¬ысота”ровнемћор€ = 15.2,
			@ одЌазначени€«емель = 2;
select @rc;
go

-- next
declare @rc int = -1;
declare @Idќпорной√еологической“очки int = -1;
exec  @rc = EGH.GetNextAnchorPointId @Idќпорной√еологической“очки output;
select @rc;
select @Idќпорной√еологической“очки;
go


-- less than d
exec EGH.GetListAnchorPointOnDistanceLessThanD
			@Ўирота√рад = 8.1339502334594727, 
			@ƒолгота√рад = 142.68394470214844, 
			@–ассто€ние = 1000000000;
go
-- between d1 and d2
exec EGH.GetListAnchorPointOnDistanceLessThanD2MoreThanD1
			 @Ўирота√рад = 8.6, 
			 @ƒолгота√рад = 142.4, 
			 @–ассто€ние1 = 10, 
			 @–ассто€ние2 = 200000;
go