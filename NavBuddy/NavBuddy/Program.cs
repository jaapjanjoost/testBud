using System;
using System.Windows.Forms;

namespace NavBuddy;

internal static class Program
{
	public static FormMain formMain = null;

	[STAThread]
	private static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		formMain = new FormMain();
		Application.Run(formMain);
	}
}
