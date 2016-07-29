[T4Scaffolding.Scaffolder(Description = "Enter a description of Portugues.ControllersWithViews here")][CmdletBinding()]
param(        
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)][string]$ControllerName,   
	[string]$ModelType,
	[string]$DbContextType,
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)

Scaffold Portugues.Controller $ControllerName -ModelType $ModelType -DbContextType $DbContextType -Force:$Force

Scaffold Portugues.ViewModel $ControllerName -ModelType $ModelType -Force:$Force

Scaffold Portugues.RazorViews $ControllerName -ModelType $ModelType -Force:$Force