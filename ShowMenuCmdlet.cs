using System.Management.Automation;

namespace AlfredBr;

public class MenuItem
{
	public string? Name
	{
		get; set;
	}
	public object? Value
	{
		get; set;
	}
	public bool IsSelected
	{
		get; set;
	}
}

/// <summary>
/// Displays an interactive menu in the console.
/// <para type="description">This cmdlet displays a customizable interactive menu in the console. Users can select an option using arrow keys.</para>
/// <para type="example">PS&gt; Show-Menu -Prompt "Choose an option:" -MenuItems @($item1, $item2)</para>
/// </summary>
[Cmdlet(VerbsCommon.Show, "Menu")]
public class ShowMenuCmdlet : Cmdlet
{
	[Parameter(Mandatory = false)] public string Prompt { get; set; } = default!;
	[Parameter(Mandatory = false)] public MenuItem[] MenuItems { get; set; } = default!;
	[Parameter(Mandatory = false)] public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;
	[Parameter(Mandatory = false)] public SwitchParameter MultiSelect {	get; set; }

	protected override void ProcessRecord()
	{
		if (MenuItems == null || MenuItems.Length == 0)
		{
			return;
		}

		DisplayMenu();
		SetMenuIndicatorToPosition(0);
		var result = WaitForUserMenuSelection();
		WriteObject(result);
	}

	private void DisplayMenu()
	{
		if (MenuItems == null || MenuItems.Length == 0)
		{
			return;
		}

		if (string.IsNullOrEmpty(Prompt))
		{
			if (MultiSelect)
			{
				Prompt = "Select one or more options:";
			}
			else
			{
				Prompt = "Select an option:";
			}
		}

		Console.ForegroundColor = ForegroundColor;

		Console.WriteLine();
		Console.WriteLine(Prompt);

		Console.ForegroundColor = ConsoleColor.White;

		foreach (var item in MenuItems)
		{
			if (MultiSelect)
			{
				Console.WriteLine($"   [{(item.IsSelected ? "X" : " ")}] {item.Name}");
				continue;
			}
			Console.WriteLine($"   {item.Name}");
		}
	}

	private void SetMenuIndicatorToPosition(int p)
	{
		if (MenuItems == null || MenuItems.Length == 0)
		{
			return;
		}

		Console.SetCursorPosition(0, Math.Max(0, Console.GetCursorPosition().Top - MenuItems.Length));

		for (var i = 0; i < MenuItems.Length; i++)
		{
			var indicator = i == p ? " > " : "   ";
			Console.ForegroundColor = i == p ? ConsoleColor.White : ConsoleColor.DarkGray;
			if (MultiSelect)
			{
				Console.WriteLine($"{indicator}[{(MenuItems[i].IsSelected ? "X" : " ")}] {MenuItems[i].Name}");
				continue;
			}
			Console.WriteLine($"{indicator}{MenuItems[i].Name}");
		}
	}

	public object? WaitForUserMenuSelection()
	{
		if (MenuItems == null || MenuItems.Length == 0)
		{
			return null;
		}

		try
		{
			var p = 0;
			ConsoleKeyInfo keyInfo;
			do
			{
				Console.CursorVisible = false;
				keyInfo = Console.ReadKey(true);

				switch (keyInfo.Key)
				{
					case ConsoleKey.DownArrow:
						p = Math.Min(++p, MenuItems.Length - 1);
						SetMenuIndicatorToPosition(p);
						break;
					case ConsoleKey.UpArrow:
						p = Math.Max(0, --p);
						SetMenuIndicatorToPosition(p);
						break;
					case ConsoleKey.Enter:
						if (MultiSelect)
						{
							return MenuItems.Where(i => i.IsSelected).Select(i => i.Value ?? i.Name).ToArray();
						}
						return MenuItems[p].Value ?? p.ToString();
					case ConsoleKey.Spacebar:
						if (MultiSelect)
						{
							MenuItems[p].IsSelected = !MenuItems[p].IsSelected;
							SetMenuIndicatorToPosition(p);
						}
						break;
					case ConsoleKey.Escape:
						return "[Escape]";
					case ConsoleKey.Q:
						return "[Quit]";
					default:
						// do nothing on other keys (for now)
						break;
				}
			} while (keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.Q);
			return null;
		}
		finally
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.CursorVisible = true;
		}
	}
}
