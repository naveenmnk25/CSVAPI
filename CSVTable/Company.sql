If(Object_Id('Company') Is Not Null)
Begin
	Drop Table [Company]
End
GO

Create  Table DBO.[Company]
(
	 Id					Int  Identity(1, 1)	Not Null
	,[Name]				Nvarchar(100)		
	,CreatedBy			Int
	,CreatedDate		DateTime
	,ModifiedDate		DateTime
	,ModifiedBy			Int
)
GO

Alter Table dbo.[Company]
Add Constraint dbo_Company Primary Key (Id)

Alter Table dbo.[Company]
Add Constraint DF_Company_CreatedDate
Default GetDate() FOR CreatedDate
Go

Alter Table dbo.[Company]
Add Constraint DF_Company_ModifiedDate
Default GetDate() FOR ModifiedDate
Go

