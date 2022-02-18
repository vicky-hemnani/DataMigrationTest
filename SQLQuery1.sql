Declare @Id int
Set @Id=1

while(@Id<1000000)
Begin
Insert into MigrationDatabase2.dbo.sourceModels values(RAND()*(5000-1+1)+1,RAND()*(5000-1+1)+1)

print @Id
Set @Id=@Id+1
End

select COUNT(*) from MigrationDatabase2.dbo.sourceModels