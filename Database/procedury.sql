-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		AAa
-- Create date: 29.11.2016
-- Description:	
-- =============================================

drop procedure InsertKlient

create procedure InsertKlient
@IDPlayer int,
@Name varchar(20),
@Surname varchar(20),
@Heigh int,
@Weight int,
@Nation varchar(20)
as
if @IDPlayer> 100
begin
RAISERROR ('Identyfikator większy niż 100.',16,1)
return
end
if @Heigh> 220
begin
rollback
RAISERROR ('Wzrost większy niż 220 cm.',16,1)
return
end
if @Weight> 130
begin
rollback
RAISERROR ('Waga większa niż 130 kg.',16,1)
return
end
insert into Players
values(@IDPlayer,@Name,@Surname,@Heigh,@Weight,@Nation)
GO

drop procedure DeleteKlient

create procedure DeleteKlient
@IDPlayer int,
@Name varchar(20),
@Surname varchar(20),
@Heigh int,
@Weight int,
@Nation varchar(20)
as
delete from Players
WHERE IDPlayer = @IDPlayer AND
Name = @Name AND
Surname = @Surname AND
Heigh = @Heigh AND
Weight = @Weight AND
Nation = @Nation
GO

drop procedure UpdateKlient

create procedure UpdateKlient
@IDPlayer int,
@Name varchar(20),
@Surname varchar(20),
@Heigh int,
@Weight int,
@Nation varchar(20)
as
if @Heigh> 220
begin
RAISERROR ('Wzrost większy niż 220 cm.',16,1)
return
end
if @Weight> 130
begin
RAISERROR ('Waga większa niż 130 kg.',16,1)
return
end
update Players
set 
Name = @Name,
Surname = @Surname,
Heigh = @Heigh,
Weight = @Weight,
Nation = @Nation
WHERE IDPlayer = @IDPlayer
GO



CREATE TRIGGER countplayers
ON Players
AFTER INSERT
AS
count ++;
GO


BACKUP DATABASE League TO DISK='H:\League2.bak'

use League

create trigger Trig_DeletePlayer
on Players
after DELETE
as
declare @nation varchar(20)
set @nation=(select Nation from deleted)
if @nation in ('Poland')
begin
rollback
RAISERROR ('Nie wolno usuwać zawodników z Polski!', 16, 10);
End

select * from Players

drop trigger Trig_DeleteKlient