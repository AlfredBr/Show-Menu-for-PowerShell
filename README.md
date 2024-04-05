# Show-Menu Cmdlet for PowerShell

`Show-Menu` is a PowerShell Cmdlet designed to enhance user interaction within scripts by displaying a customizable interactive menu in the console. Users can navigate through the menu using the arrow keys and select an option by pressing Enter. This Cmdlet aims to simplify script options selection, making scripts more user-friendly and accessible.

## Features

- **Customizable Prompt:** Display a custom message above the menu to guide users.
- **Dynamic Menu Items:** Pass an array of menu items dynamically to the Cmdlet.
- **Customizable Foreground Color:** Change the menu's text color for better visibility or to match your script's theme.
- **Keyboard Navigation:** Users can navigate through the menu options using the arrow keys.
- **Flexible Return Values:** Menu items can return a specified object or the menu item's index.

## Installation

To use the `Show-Menu` Cmdlet, ensure you have PowerShell installed on your system. Clone this repo and compile it into a DLL. Then, import the DLL into your PowerShell session using:

```powershell
Import-Module ./path/to/Show-Menu.dll
```

## Usage
### Parameters

* **Prompt** (Optional): A string that displays above the menu. Default is "Select an option:".
* **MenuItems** (Mandatory): An array of MenuItem objects, each representing a menu option.
* **ForegroundColor** (Optional): The color of the menu text. Default is ConsoleColor.White.

### Creating Menu Items
First, define your menu items by creating instances of the MenuItem class. Each MenuItem must have a Name and can optionally have a Value that is returned when the item is selected.

```powershell
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
```

### Displaying the Menu
Pass the menu items to the `Show-Menu` Cmdlet along with any optional parameters.

```powershell
Show-Menu -Prompt "Choose an option:" -MenuItems $MenuItems -ForegroundColor Green
```

## Example
This example demonstrates creating a simple menu that allows the user to choose between two options.

```powershell
# Create menu items
$item1 = New-Object AlfredBr.MenuItem
$item1.Name = "First Option"
$item1.Value = "First"

$item2 = New-Object AlfredBr.MenuItem
$item2.Name = "Second Option"
$item2.Value = "Second"

# Display the menu and capture the selection
$selectedValue = Show-Menu -Prompt "Choose an option:" -MenuItems @($item1, $item2)

if ($selectedValue) {
	Write-Host "You selected: $selectedValue"
} else {
	Write-Host "No option selected."
}
```
## Contributing
Contributions to enhance Show-Menu are welcome. Please feel free to fork the repository, make your changes, and submit a pull request.

## License
This project is licensed under the MIT License. Feel free to use it, modify it, and distribute it as you see fit.