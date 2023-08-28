If(Object_Id('ColumnValues') Is Not Null)
Begin
	Drop Table [ColumnValues]
End
GO

Create  Table DBO.[ColumnValues]
(
	 Id					Int  Identity(1, 1)	Not Null
	,ColumnId			Int
	,[Value]			Nvarchar(100)		
	,CreatedBy			Int
	,CreatedDate		DateTime
	,ModifiedDate		DateTime
	,ModifiedBy			Int
)
GO

Alter Table dbo.[ColumnValues]
Add Constraint dbo_ColumnValues Primary Key (Id)

Alter Table dbo.[ColumnValues]
Add Constraint DF_ColumnValues_CreatedDate
Default GetDate() FOR CreatedDate
Go

Alter Table dbo.[ColumnValues]
Add Constraint DF_ColumnValues_ModifiedDate
Default GetDate() FOR ModifiedDate
Go

