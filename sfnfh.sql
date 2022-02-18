Declare @id int
Set @id=1
While(@id<100000)
Begin
Insert into MigrationDatabase.dbo.sourceModels values(RAND()*(5000-1+1)+1,RAND()*(5000-1+1)+1)

print @id
Set @id=@id+1
End
Select COUNT(*) from MigrationDatabase.dbo.sourceModels