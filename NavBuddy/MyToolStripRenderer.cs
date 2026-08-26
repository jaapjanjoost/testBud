using System.Drawing;
using System.Windows.Forms;

internal class MyToolStripRenderer : ToolStripProfessionalRenderer
{
	private Brush WhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

	private Brush GreenBrush = new SolidBrush(Color.Lime);

	private Brush DarkGreenBrush = new SolidBrush(Color.FromArgb(0, 128, 0));

	private Pen DarkGreenPen = new Pen(Color.FromArgb(0, 128, 0));

	private Brush BlackBrush = new SolidBrush(Color.FromArgb(0, 0, 0));

	private Pen BlackPen = new Pen(Color.FromArgb(0, 0, 0));

	private Brush TestBrush = new SolidBrush(Color.Red);

	protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
	{
		Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
		if (e.Item.Selected)
		{
			e.Graphics.FillRectangle(GreenBrush, rect);
		}
		else
		{
			e.Graphics.FillRectangle(DarkGreenBrush, rect);
		}
	}

	protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
	{
		e.Graphics.FillRectangle(BlackBrush, e.ToolStrip.ClientRectangle);
	}

	protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
	{
		if (e.Item.Selected)
		{
			e.TextColor = Color.Black;
		}
		else
		{
			e.TextColor = Color.FromArgb(0, 192, 0);
		}
		base.OnRenderItemText(e);
	}

	protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
	{
	}
}
