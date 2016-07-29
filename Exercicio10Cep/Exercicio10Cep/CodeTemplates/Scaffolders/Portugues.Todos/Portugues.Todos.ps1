[T4Scaffolding.Scaffolder(Description = "Enter a description of Portugues.Todos here")][CmdletBinding()]
param(        
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

@("Pais", "Estado", "Cidade", "Bairro", "Logradouro") | %{
	Scaffold Portugues.ControllersWithViews $_ -ModelType:Exercicio10Cep.Models.$_ -Force -Verbose -DbContextType:Exercicio10Cep.Models.ApplicationDbContext
}