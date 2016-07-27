[T4Scaffolding.Scaffolder(Description = "Adds ASP.NET MVC views for Create/Read/Update/Delete/Indice scenarios")][CmdletBinding()]
param(        
	[parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true, Position = 0)][string]$Controller,
	[string]$ModelType,
	[string]$Area,
	[string]$ViewName,
	[string]$Template,
	[alias("MasterPage")]$Layout = "",	# If not set, we'll use the default layout
 	[alias("ContentPlaceholderIDs")][string[]]$SectionNames,
	[alias("PrimaryContentPlaceholderID")][string]$PrimarySectionName,
	[switch]$ReferenceScriptLibraries = $false,
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[string]$ViewScaffolder = "View",
	[switch]$Force = $false
)

if($ViewName -eq "Indice" -or !$ViewName) {
	@("Indice") | %{
		Scaffold $ViewScaffolder -Controller $Controller -ViewName $_ -ModelType $ModelType -Template $_ -Area $Area -Layout $Layout -SectionNames $SectionNames -PrimarySectionName $PrimarySectionName -ReferenceScriptLibraries:$ReferenceScriptLibraries -Project $Project -CodeLanguage $CodeLanguage -OverrideTemplateFolders $TemplateFolders -Force:$Force
	}

	if ($Template -eq "bootstrap" -or !$Template) {
		@("Indice_bootstrap") | %{
			Scaffold $ViewScaffolder -Controller $Controller -ViewName $_ -ModelType $ModelType -Template $_ -Area $Area -Layout $Layout -SectionNames $SectionNames -PrimarySectionName $PrimarySectionName -ReferenceScriptLibraries:$ReferenceScriptLibraries -Project $Project -CodeLanguage $CodeLanguage -OverrideTemplateFolders $TemplateFolders -Force:$Force
		}
	}

	if ($Template -eq "foundation" -or !$Template) {
		@("Indice_foundation") | %{
			Scaffold $ViewScaffolder -Controller $Controller -ViewName $_ -ModelType $ModelType -Template $_ -Area $Area -Layout $Layout -SectionNames $SectionNames -PrimarySectionName $PrimarySectionName -ReferenceScriptLibraries:$ReferenceScriptLibraries -Project $Project -CodeLanguage $CodeLanguage -OverrideTemplateFolders $TemplateFolders -Force:$Force
		}
	}

	if ($Template -eq "Ink" -or !$Template) {
		@("Indice_Ink") | %{
			Scaffold $ViewScaffolder -Controller $Controller -ViewName $_ -ModelType $ModelType -Template $_ -Area $Area -Layout $Layout -SectionNames $SectionNames -PrimarySectionName $PrimarySectionName -ReferenceScriptLibraries:$ReferenceScriptLibraries -Project $Project -CodeLanguage $CodeLanguage -OverrideTemplateFolders $TemplateFolders -Force:$Force
		}
	}
}

if($ViewName -eq "Criar" -or !$ViewName) {
	@("Criar") | %{
			Scaffold $ViewScaffolder -Controller $Controller -ViewName $_ -ModelType $ModelType -Template $_ -Area $Area -Layout $Layout -SectionNames $SectionNames -PrimarySectionName $PrimarySectionName -ReferenceScriptLibraries:$ReferenceScriptLibraries -Project $Project -CodeLanguage $CodeLanguage -OverrideTemplateFolders $TemplateFolders -Force:$Force
		}

	if ($Template -eq "bootstrap" -or !$Template) {
		@("Criar_bootstrap", "_CriarOuEditar_bootstrap") | %{
			Scaffold $ViewScaffolder -Controller $Controller -ViewName $_ -ModelType $ModelType -Template $_ -Area $Area -Layout $Layout -SectionNames $SectionNames -PrimarySectionName $PrimarySectionName -ReferenceScriptLibraries:$ReferenceScriptLibraries -Project $Project -CodeLanguage $CodeLanguage -OverrideTemplateFolders $TemplateFolders -Force:$Force
		}
	}

	if ($Template -eq "foundation" -or !$Template) {
		@("Criar_foundation", "_CriarOuEditar_foundation") | %{
			Scaffold $ViewScaffolder -Controller $Controller -ViewName $_ -ModelType $ModelType -Template $_ -Area $Area -Layout $Layout -SectionNames $SectionNames -PrimarySectionName $PrimarySectionName -ReferenceScriptLibraries:$ReferenceScriptLibraries -Project $Project -CodeLanguage $CodeLanguage -OverrideTemplateFolders $TemplateFolders -Force:$Force
		}
	}

	if ($Template -eq "Ink" -or !$Template) {
		@("Criar_Ink", "_CriarOuEditar_Ink") | %{
			Scaffold $ViewScaffolder -Controller $Controller -ViewName $_ -ModelType $ModelType -Template $_ -Area $Area -Layout $Layout -SectionNames $SectionNames -PrimarySectionName $PrimarySectionName -ReferenceScriptLibraries:$ReferenceScriptLibraries -Project $Project -CodeLanguage $CodeLanguage -OverrideTemplateFolders $TemplateFolders -Force:$Force
		}
	}
}
