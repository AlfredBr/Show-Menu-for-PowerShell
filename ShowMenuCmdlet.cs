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

    protected override void ProcessRecord()
    {
        if (MenuItems == null ||MenuItems.Length == 0)
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
            Prompt = "Select an option:";
        }
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine();
        Console.WriteLine(Prompt);

        foreach (var item in MenuItems)
        {
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
                        return MenuItems[p].Value??p.ToString();
                    default:
                        // do nothing on other keys (for now)
                        break;
                }
            } while (keyInfo.Key != ConsoleKey.Escape);
            return null;
        }
        finally
        {
            Console.ForegroundColor= ConsoleColor.White;
            Console.CursorVisible = true;
        }
    }
}
