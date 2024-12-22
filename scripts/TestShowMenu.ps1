Write-Host "PowerShell" $psVersionTable.PSVersion -ForegroundColor Green

$menuItems = @(
	@{
		Name = "Option 1"
		Value = 0x01
	},
	@{
		Name = "Option 2"
		Value = "two"
	},
	@{
		Name = "Option 3"
		Value = 3
	}
)

$foodMenuItems = @(
	@{
		Name = "Pizza 🍕"
	},
	@{
		Name = "Burger 🍔"
	},
	@{
		Name = "Hotdog 🌭"
	},
	@{
		Name = "Fries 🍟"
	},
	@{
		Name = "Drink 🥤"
	},
	@{
		Name = "Ice cream 🍦"
	}
)

$choice = Show-Menu -MenuItems $menuItems -Prompt "Pick one:"
if ($choice) {
	Write-Host "You chose: $choice"
}

$choice = Show-Menu -MenuItems $foodMenuItems -MultiSelect -Prompt "Pick one or more:"
if ($choice) {
	Write-Host "You chose: $choice"
}

$choice = Show-Menu -MenuItems @() -Prompt "Empty list:"
if ($choice) {
	Write-Host "You chose: $choice"
}

$choice = Show-Menu -MenuItems @(@{Name="Foo"}, @{Name="Bar"; Value=69},@{Name="Baz"}) -Prompt "Fuzzy list:"
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
$choice = Show-Menu -Prompt "Did you like this little cmdlet?" -MenuItems $yesNo -ForegroundColor Blue
if ($choice) {
	if ($yesNo[$choice].Name -eq "Yes") {
		Show-Boxed -Contents @("Thank you!", "https://github.com/AlfredBr/Show-Menu-for-PowerShell") -LineColor Red
	}
}
