/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [id]
      ,[userId]
      ,[fullName]
      ,[phoneNumber]
      ,[email]
      ,[latitude]
      ,[longitude]
      ,[zipCode]
  FROM [SimplyHelp].[dbo].[UserMembers]

  --Updating for testing James Doe's location to Miami
  --Update [SimplyHelp].[dbo].UserMembers set latitude = '27.972572', longitude = '-82.796745' Where fullName like 'James Doe'