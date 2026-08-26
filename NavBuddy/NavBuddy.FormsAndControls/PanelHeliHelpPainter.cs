using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NavBuddy.FormsAndControls;

public static class PanelHeliHelpPainter
{
	public static SimulatorConnectionManager.Struct2 lastSimulatorData;

	public static double zeroLevel;

	public static void HeliHelper_Paint(object sender, PaintEventArgs e)
	{
		Panel panel = (Panel)sender;
		if (panel.Tag == null)
		{
			return;
		}
		SimulatorConnectionManager.Struct2 @struct = (lastSimulatorData = (SimulatorConnectionManager.Struct2)panel.Tag);
		Point point = new Point(e.ClipRectangle.Width / 2, e.ClipRectangle.Height / 2);
		Brush brush = new SolidBrush(Color.White);
		Pen pen = new Pen(brush, 2f);
		Pen pen2 = new Pen(brush, 3f);
		Brush brush2 = new SolidBrush(Color.Lime);
		Pen pen3 = new Pen(brush2, 2f);
		Pen pen4 = new Pen(brush2, 10f);
		Brush brush3 = new SolidBrush(Color.Cyan);
		Pen pen5 = new Pen(brush3, 2f);
		Brush brush4 = new SolidBrush(Color.Black);
		Pen pen6 = new Pen(brush4, 2f);
		Font font = new Font(panel.Font.FontFamily, 22f);
		Font font2 = new Font(panel.Font.FontFamily, 11f);
		Font font3 = new Font(panel.Font.FontFamily, 8f);
		Graphics graphics = e.Graphics;
		graphics.SmoothingMode = SmoothingMode.AntiAlias;
		double num = 0.0;
		double num2 = @struct.VERTICAL_SPEED * 60.0;
		double num3 = @struct.PLANE_ALT_ABOVE_GROUND - zeroLevel;
		Color fillcolor = Color.Green;
		if (@struct.COLLECTIVE_POSITION > 0.85)
		{
			fillcolor = Color.Orange;
		}
		else if (@struct.COLLECTIVE_POSITION > 0.95)
		{
			fillcolor = Color.Red;
		}
		PointF center = new PointF((float)e.ClipRectangle.Width * 0.075f, (float)e.ClipRectangle.Height * 0.5f);
		DrawSquareIndicator(graphics, center, Color.White, fillcolor, new SizeF(-5f + (float)e.ClipRectangle.Width * 0.15f, -12f + (float)e.ClipRectangle.Height * 1f), @struct.COLLECTIVE_POSITION);
		DrawCenteredString(graphics, center, Math.Round(100.0 * @struct.COLLECTIVE_POSITION) + " %", font, brush);
		float num4 = (float)e.ClipRectangle.Width * 0.1f;
		double num5 = @struct.PLANE_ALT_ABOVE_GROUND - zeroLevel;
		if (num5 < 50.0)
		{
			PointF center2 = new PointF((float)e.ClipRectangle.Width - num4 * 0.5f, (float)e.ClipRectangle.Height * 0.5f);
			Color fillcolor2 = Color.White;
			if (num2 > -50.0)
			{
				fillcolor2 = Color.Green;
			}
			else if (num2 > -100.0)
			{
				fillcolor2 = Color.Orange;
			}
			else if (num2 > -100.0)
			{
				fillcolor2 = Color.Red;
			}
			DrawSquareIndicator(graphics, center2, Color.White, fillcolor2, new SizeF(num4 - 5f, -12 + e.ClipRectangle.Height), num3 / 50.0);
			DrawCenteredString(graphics, center2, Math.Round(num3) + " ft", font2, brush);
		}
		PointF center3 = new PointF((float)e.ClipRectangle.Width - num4 * 1.5f, (float)e.ClipRectangle.Height * 0.5f);
		DrawSquareSignedindicator(graphics, center3, Color.White, Color.Green, new SizeF(num4 - 5f, -12 + e.ClipRectangle.Height), num2 / 400.0);
		DrawCenteredString(graphics, center3, Math.Round(num2) + " fpm", font2, brush);
		double num6 = 1.944;
		PointF pointF = new PointF((float)e.ClipRectangle.Width * 0.5f, (float)e.ClipRectangle.Height * 0.82f);
		graphics.ResetTransform();
		graphics.TranslateTransform(pointF.X, pointF.Y);
		double num7 = (double)e.ClipRectangle.Width * 0.12;
		graphics.DrawEllipse(pen, new Rectangle(-(int)num7, -(int)num7, (int)(2.0 * num7), (int)(2.0 * num7)));
		graphics.DrawLine(pen, new Point(-(int)num7, 0), new Point((int)num7, 0));
		graphics.DrawLine(pen, new Point(0, -(int)num7), new Point(0, (int)num7));
		PointF pointF2 = new PointF((float)((double)((float)e.ClipRectangle.Width * 0.5f) + num7 * @struct.ROTOR_LATERAL_TRIM_PCT / num6), (float)((double)((float)e.ClipRectangle.Height * 0.82f) + num7 * @struct.ROTOR_LONGITUDINAL_TRIM_PCT / num6));
		graphics.ResetTransform();
		graphics.TranslateTransform(pointF2.X, pointF2.Y);
		graphics.DrawLine(pen3, new Point(-10, 0), new Point(10, 0));
		graphics.DrawLine(pen3, new Point(0, -10), new Point(0, 10));
		PointF pointF3 = new PointF((float)e.ClipRectangle.Width * 0.5f, (float)e.ClipRectangle.Height * 0.5f);
		graphics.ResetTransform();
		graphics.TranslateTransform(pointF3.X, pointF3.Y);
		double num8 = (double)e.ClipRectangle.Width * 0.2;
		graphics.DrawEllipse(pen, new Rectangle(-(int)num8, -(int)num8, (int)(2.0 * num8), (int)(2.0 * num8)));
		if (Math.Abs(@struct.VELOCITY_BODY_X) < 10.0 && Math.Abs(@struct.VELOCITY_BODY_Z) < 10.0)
		{
			graphics.DrawLine(pen3, new Point(0, 0), new Point((int)(num8 * (@struct.VELOCITY_BODY_X / 10.0)), (int)((0.0 - num8) * (@struct.VELOCITY_BODY_Z / 10.0))));
		}
		PointF pointF4 = new PointF((float)e.ClipRectangle.Width * 0.5f, (float)e.ClipRectangle.Height * 0.15f);
		float num9 = Math.Min((float)e.ClipRectangle.Width * 0.08f, (float)e.ClipRectangle.Height * 0.08f);
		graphics.ResetTransform();
		graphics.TranslateTransform(pointF4.X, pointF4.Y);
		e.Graphics.DrawEllipse(pen, 0f - num9, 0f - num9, 2f * num9, 2f * num9);
		graphics.DrawLine(pen, new Point(-(int)num9, 0), new Point((int)num9, 0));
		graphics.DrawLine(pen, new Point(0, -(int)num9), new Point(0, (int)num9));
		num = @struct.AMBIENT_WIND_DIRECTION - Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_TRUE);
		graphics.RotateTransform((float)num);
		int num10 = (int)(Utility.Clamp(Math.Log(@struct.AMBIENT_WIND_VELOCITY + 1.0) / 3.0, 0.0, 1.0) * (double)num9);
		int num11 = Utility.Clamp(num10 / 5, 2, 5);
		e.Graphics.DrawLine(pen3, new Point(0, 0), new Point(0, num10));
		e.Graphics.DrawLine(pen3, new Point(0, num10), new Point(num11, num10 - num11));
		e.Graphics.DrawLine(pen3, new Point(0, num10), new Point(-num11, num10 - num11));
		string text = "wind: " + @struct.AMBIENT_WIND_VELOCITY.ToString("F1") + " kt";
		graphics.ResetTransform();
		graphics.TranslateTransform(pointF4.X, pointF4.Y - num9);
		SizeF sizeF = graphics.MeasureString(text, font2);
		graphics.FillRectangle(brush4, new RectangleF((0f - sizeF.Width) / 2f, (0f - sizeF.Height) / 2f, sizeF.Width, sizeF.Height));
		graphics.DrawRectangle(pen, new Rectangle((int)((0f - sizeF.Width) / 2f), (int)((0f - sizeF.Height) / 2f), (int)sizeF.Width, (int)sizeF.Height));
		graphics.DrawString(text, font2, brush, 0f - sizeF.Width / 2f, (0f - sizeF.Height) / 2f);
	}

	public static void DrawCenteredString(Graphics g, PointF center, string text, Font font, Brush brush)
	{
		g.ResetTransform();
		g.TranslateTransform(center.X, center.Y);
		g.DrawString(text, font, brush, Multiply(g.MeasureString(text, font), -0.5f));
	}

	public static void DrawSquareIndicator(Graphics g, PointF center, Color bordercolor, Color fillcolor, SizeF size, double fillpart)
	{
		g.ResetTransform();
		g.TranslateTransform(center.X, center.Y);
		int num = (int)((double)((0f - size.Height) / 2f) + (double)size.Height * (1.0 - fillpart));
		int num2 = (int)((double)size.Height * fillpart);
		g.FillRectangle(rect: new RectangleF((int)((0f - size.Width) / 2f), num, size.Width, num2), brush: new SolidBrush(fillcolor));
		g.DrawRectangle(rect: new Rectangle((int)((0f - size.Width) / 2f), (int)((0f - size.Height) / 2f), (int)size.Width, (int)size.Height), pen: new Pen(bordercolor));
	}

	public static void DrawSquareSignedindicator(Graphics g, PointF center, Color bordercolor, Color fillcolor, SizeF size, double fillpart)
	{
		g.ResetTransform();
		g.TranslateTransform(center.X, center.Y);
		g.FillRectangle(rect: (!(fillpart > 0.0)) ? new RectangleF((int)((0f - size.Width) / 2f), 0f, size.Width, (float)((0.0 - fillpart) * (double)size.Height)) : new RectangleF((int)((0f - size.Width) / 2f), (float)((0.0 - fillpart) * (double)size.Height), size.Width, (float)(fillpart * (double)size.Height)), brush: new SolidBrush(fillcolor));
		g.DrawRectangle(rect: new Rectangle((int)((0f - size.Width) / 2f), (int)((0f - size.Height) / 2f), (int)size.Width, (int)size.Height), pen: new Pen(bordercolor));
	}

	public static PointF Multiply(SizeF s, float scalar)
	{
		return new PointF(s.Width * scalar, s.Height * scalar);
	}
}
