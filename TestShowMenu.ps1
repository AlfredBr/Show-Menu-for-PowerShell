Write-Host "PowerShell" $psVersionTable.PSVersion -ForegroundColor Green

Import-Module ".\bin\Debug\net8.0\Show-Menu.dll"

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

$yesNo = @(@{Name="Yes"}, @{Name="No"})
$choice = Show-Menu -Prompt "Did you like this little cmdlet?" -MenuItems $yesNo
if ($yesNo[$choice].Name -eq "Yes") {
	Show-Boxed -Contents @("❤️", "Thank you!") -LineColor Red
}

Show-Boxed -Contents @("Foo", "Bar", "Baz")

Show-Boxed -Contents "https://github.com/AlfredBr/Show-Menu-for-PowerShell" -LineColor Blue

