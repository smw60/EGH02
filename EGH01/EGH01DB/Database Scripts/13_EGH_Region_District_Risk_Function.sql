use EGH;
go

if OBJECT_ID('EGH.Область_Район_ТехногенныйОбъект') is not null  drop function EGH.Область_Район_ТехногенныйОбъект;
go

create function EGH.Область_Район_ТехногенныйОбъект(@p nvarchar(200))
returns table as
return (
	     with likefind as 
	     ( 
            select concat(О.Область,                         Р.Район,                           ТО.НаименованиеТехногенногоОбъекта)  ort,
                   concat(О.Область,                         ТО.НаименованиеТехногенногоОбъекта,Р.Район)                             otr,
                   concat(Р.Район,                           О.Область,                         ТО.НаименованиеТехногенногоОбъекта)  rot,
                   concat(Р.Район,                           ТО.НаименованиеТехногенногоОбъекта,О.Область)                           rto,
                   concat(ТО.НаименованиеТехногенногоОбъекта,О.Область,                         Р.Район)                             tor,
                   concat(ТО.НаименованиеТехногенногоОбъекта,Р.Район,                           О.Область)                           tro, 
                   О.Область, 
                   Р.Район,
                   ТО.IdТехногенногоОбъекта  
            from Область О join Район Р               on о.КодОбласти =  р.Область 
                           join ТехногенныйОбъект ТО  on ТО.ОбластьТехногенногоОбъекта = О.КодОбласти 
                                                     and ТО.РайонТехногенногоОбъекта   = Р.КодРайона
         )
         select ort, otr, rot, rto, tor, tro, Область,Район, IdТехногенногоОбъекта  
         from likefind                                       
         where ort like @p or otr like @p or rot like @p or rto like @p or tor like @p or tro like @p 
       ); 

go
--select Область, Район, IdТехногенногоОбъекта  from dbo.Область_Район_ТехногенныйОбъект('%%Брест%азс%')  
--select Область, Район, IdТехногенногоОбъекта  from dbo.Область_Район_ТехногенныйОбъект('%%Брест%%Баран%азс%')
----select Область, Район, IdТехногенногоОбъекта  from dbo.Область_Район_ТехногенныйОбъект('%Бар%Брест%азс%') 
--select Область, Район, IdТехногенногоОбъекта  from dbo.Область_Район_ТехногенныйОбъект('%Бар%%')

if OBJECT_ID('EGH.GetRiskObjectListByLike') is not null  drop procedure EGH.GetRiskObjectListByLike;
go
create procedure EGH.GetRiskObjectListByLike
   @findstring nvarchar(200)
as begin
   declare @p nvarchar(200) = concat('%', replace(rtrim(ltrim(@findstring)), ' ', '%'), '%');  
   
   select Область, Район, IdТехногенногоОбъекта 
   from EGH.Область_Район_ТехногенныйОбъект(concat('%', replace(rtrim(ltrim(@findstring)), ' ', '%'), '%'));
   
   return @@rowcount;  
end;     
go

--declare @rc int = -1;   
--exec @rc = EGH.GetRiskObjectListByLike 'Брест';
--select @rc
--exec @rc = EGH.GetRiskObjectListByLike 'азс';
--select @rc
--go


   


                                       

