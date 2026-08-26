using System.Drawing;
using System.Windows.Forms;

namespace NavBuddy;

public class PanelTracker : Panel
{
	public bool refreshRequested = false;

	public int trackerX = 0;

	public double[] acceleration = new double[10];

	protected override void OnPaint(PaintEventArgs e)
	{
		int num = base.Height / 2;
		int num2 = (int)((double)base.Height * 0.45);
		if (refreshRequested)
		{
			refreshRequested = false;
			e.Graphics.FillRectangle(new SolidBrush(Color.Black), e.ClipRectangle);
			e.Graphics.DrawLine(new Pen(Color.DarkGray), new Point(0, num + num2), new Point(base.Width, num + num2));
			e.Graphics.DrawLine(new Pen(Color.DarkGray), new Point(0, num), new Point(base.Width + 1, num));
			e.Graphics.DrawLine(new Pen(Color.DarkGray), new Point(0, num - num2), new Point(base.Width + 1, num - num2));
		}
		trackerX = (trackerX + 1) % base.Width;
		for (int i = 0; i < 10; i++)
		{
			int num3 = (trackerX + i) % base.Width;
			if (i < 9)
			{
				e.Graphics.DrawLine(new Pen(Color.FromArgb(0, 74 + i * 20, 0)), new Point(num3, num - (int)((double)num2 * (acceleration[i] - 1.0))), new Point(num3 + 1, num - (int)((double)num2 * (acceleration[i + 1] - 1.0))));
				acceleration[i] = acceleration[i + 1];
			}
			else
			{
				acceleration[i] = SimulatorInformationProcessing.lastSmallInfoSimulatorData.gforce;
			}
		}
		int num4 = (trackerX + 100) % base.Width;
		e.Graphics.DrawLine(new Pen(Color.Black), new Point(num4, 0), new Point(num4, base.Height));
		e.Graphics.DrawLine(new Pen(Color.DarkGray), new Point(num4, num + num2), new Point(num4 + 1, num + num2));
		e.Graphics.DrawLine(new Pen(Color.DarkGray), new Point(num4, num), new Point(num4 + 1, num));
		e.Graphics.DrawLine(new Pen(Color.DarkGray), new Point(num4, num - num2), new Point(num4 + 1, num - num2));
	}

	protected override void OnPaintBackground(PaintEventArgs e)
	{
	}

	public void Clear()
	{
		refreshRequested = true;
		Refresh();
	}
}
