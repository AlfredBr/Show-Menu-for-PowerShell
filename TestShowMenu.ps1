Write-Host "PowerShell" $psVersionTable.PSVersion -ForegroundColor Green

Import-Module "C:\Users\alfre\source\repos\Show-Menu\bin\Debug\net8.0\Show-Menu.dll"

$MenuItems = @(
	@{
		Name = "Option 1"
		Value = 420
	},
	@{
		Name = "Option 2"
		Value = 6+9
	},
	@{
		Name = "Option 3"
		Value = "seggs"
	}
)

$choice = Show-Menu -Prompt "Pick one:" -MenuItems $menuItems
if ($choice) {
	Write-Host "You chose: $choice"
}

$choice = Show-Menu -MenuItems $menuItems
if ($choice) {
	Write-Host "You chose: $choice"
}

$choice = Show-Menu -Prompt "Empty list:" -MenuItems @()
if ($choice) {
	Write-Host "You chose: $choice"
}

$choice = Show-Menu -Prompt "Fuzzy list:" -MenuItems @(@{Name="Foo"}, @{Name="Bar"; Value=69},@{Name="Baz"})
if ($choice) {
	Write-Host "You chose: $choice"
}

$choice = Show-Menu -Prompt "No list:"
if ($choice) {
	Write-Host "You chose: $choice"
}

$choice = Show-Menu
if ($choice) {
	Write-Host "You chose: $choice"
}

Show-Boxed -Contents "This is a test #1"
Show-Boxed -Contents @("❤️", "This is a test #2") -LineColor Green
Show-Boxed -Contents @("Foo", "Bar", "Baz")