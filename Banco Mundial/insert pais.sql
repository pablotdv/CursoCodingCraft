INSERT INTO [ex06].[Pais]
           ([Codigo]
           ,[Regiao]
           ,[GrupoEconomico]
           ,[Notas]
           ,[Nome])
SELECT [Country Code]
      ,[Region]
      ,[IncomeGroup]
      ,[SpecialNotes]
      ,[TableName]
  FROM [WorldBank].[dbo].[Countries]


