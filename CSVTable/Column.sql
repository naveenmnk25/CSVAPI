If(Object_Id('Column') Is Not Null)
Begin
	Drop Table [Column]
End
GO

Create  Table DBO.[Column]
(
	 Id					Int  Identity(1, 1)	Not Null
	,[Name]				Nvarchar(100)		
	,CompanyId			Int
	,CreatedBy			Int
	,CreatedDate		DateTime
	,ModifiedDate		DateTime
	,ModifiedBy			Int
)
GO

Alter Table dbo.[Column]
Add Constraint dbo_Column Primary Key (Id)

Alter Table dbo.[Column]
Add Constraint DF_Column_CreatedDate
Default GetDate() FOR CreatedDate
Go

Alter Table dbo.[Column]
Add Constraint DF_Column_ModifiedDate
Default GetDate() FOR ModifiedDate
Go

