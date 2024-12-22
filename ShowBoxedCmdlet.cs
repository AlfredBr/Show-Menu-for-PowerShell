using System.Management.Automation;

namespace AlfredBr;

[Cmdlet(VerbsCommon.Show, "Boxed")]
public class ShowBoxedCmdlet : Cmdlet
{
	[Parameter(Mandatory = false)] public string[] Contents { get; set; } = default!;
	[Parameter(Mandatory = false)] public ConsoleColor LineColor { get; set; } = ConsoleColor.DarkYellow;
	protected override void ProcessRecord()
	{
		if (Contents == null || Contents.Length == 0)
		{
			return;
		}
		Contents = Contents.Where(t => t?.Length > 0).ToArray();
		var lengths = Contents.Select(t => t.Length).ToArray();
		Console.SetCursorPosition(0, Console.CursorTop);
		Console.ForegroundColor = LineColor;
		Console.Write("┌");
		for (var l = 0; l < lengths.Length - 1; l++)
		{
			var length = lengths[l];
			Console.Write("─".PadRight(length + 2, '─'));
			Console.Write("┬");
		}
		Console.Write("─".PadRight(lengths.Last() + 2, '─'));
		Console.WriteLine("┐");

		foreach (var content in Contents)
		{
			Console.Write("│ ");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(content);
			Console.ForegroundColor = LineColor;
			Console.Write(" ");
		}
		Console.WriteLine("│");

		Console.Write("└");
		for (var l = 0; l < lengths.Length - 1; l++)
		{
			var length = lengths[l];
			Console.Write("─".PadRight(length + 2, '─'));
			Console.Write("┴");
		}
		Console.Write("─".PadRight(lengths.Last() + 2, '─'));
		Console.WriteLine("┘");
		Console.ForegroundColor = ConsoleColor.White;
	}
}
