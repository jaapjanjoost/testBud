using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NavBuddy.FormsAndControls;

public static class PanelCompassPainter
{
	public static bool drawBuddyPilotReference = false;

	public static bool drawCustomReference = false;

	public static string trueOrMagnetic = "TRUE";

	public static void BuddyPilotCompass_Paint(object sender, PaintEventArgs e)
	{
		Panel panel = (Panel)sender;
		if (panel.Tag == null)
		{
			return;
		}
		SimulatorConnectionManager.Struct2 @struct = (SimulatorConnectionManager.Struct2)panel.Tag;
		int num = (int)((double)e.ClipRectangle.Height * 0.75);
		if (num < 10)
		{
			return;
		}
		Point point = new Point(e.ClipRectangle.Width / 2, e.ClipRectangle.Height + 10);
		Brush brush = new SolidBrush(Color.White);
		Pen pen = new Pen(brush);
		Brush brush2 = new SolidBrush(Color.Lime);
		Pen pen2 = new Pen(brush2);
		Brush brush3 = new SolidBrush(Color.Cyan);
		Pen pen3 = new Pen(brush3);
		Brush brush4 = new SolidBrush(Color.Black);
		Pen pen4 = new Pen(brush4);
		Font font = new Font(panel.Font.FontFamily, 14f);
		Font font2 = new Font(panel.Font.FontFamily, 10f);
		Graphics graphics = e.Graphics;
		graphics.SmoothingMode = SmoothingMode.AntiAlias;
		double num2 = 0.0;
		graphics.ResetTransform();
		graphics.TranslateTransform(point.X, point.Y);
		e.Graphics.DrawArc(pen, new Rectangle(-num, -num, 2 * num, 2 * num), 180f, 180f);
		num2 = 0.0 - Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_TRUE);
		graphics.RotateTransform((float)num2);
		for (int i = 0; i < 360; i += 10)
		{
			double num3 = num;
			double num4 = num - 3;
			if (i % 30 == 0)
			{
				num4 = num - 6;
			}
			double num5 = Utility.DegToRad(i);
			Point pt = new Point((int)(num3 * Math.Cos(num5)), (int)(num3 * Math.Sin(num5)));
			Point pt2 = new Point((int)(num4 * Math.Cos(num5)), (int)(num4 * Math.Sin(num5)));
			e.Graphics.DrawLine(pen, pt, pt2);
		}
		graphics.DrawString("N", font2, brush, 0f - graphics.MeasureString("N", font2).Width / 2f, -num);
		graphics.RotateTransform(90f);
		graphics.DrawString("E", font2, brush, 0f - graphics.MeasureString("E", font2).Width / 2f, -num);
		graphics.RotateTransform(90f);
		graphics.DrawString("S", font2, brush, 0f - graphics.MeasureString("S", font2).Width / 2f, -num);
		graphics.RotateTransform(90f);
		graphics.DrawString("W", font2, brush, 0f - graphics.MeasureString("W", font2).Width / 2f, -num);
		graphics.ResetTransform();
		graphics.TranslateTransform(point.X, point.Y);
		num2 = BuddyPilot.targetGeoHeadingDegree - Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_TRUE);
		graphics.RotateTransform((float)num2);
		int num6 = num - 10;
		int num7 = num - 20;
		e.Graphics.DrawLine(pen3, new Point(0, -num7), new Point(0, -num6));
		SizeF sizeF = graphics.MeasureString(BuddyPilot.targetGeoHeadingDegree.ToString("F0"), font2);
		graphics.DrawString(BuddyPilot.targetGeoHeadingDegree.ToString("F0"), font2, brush3, 0f - sizeF.Width / 2f, -num7);
		graphics.DrawArc(pen3, new Rectangle(-num6, -num6, 2 * num6, 2 * num6), -90f, (int)(0.0 - BuddyPilot.WindCompensationAngleDegree));
		graphics.ResetTransform();
		graphics.TranslateTransform(point.X, 0f);
		string text = ((int)Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_TRUE)).ToString("D3");
		sizeF = graphics.MeasureString(text, font);
		graphics.FillRectangle(brush4, new RectangleF((0f - sizeF.Width) / 2f, 0f, sizeF.Width, sizeF.Height));
		graphics.DrawString(text, font, brush, 0f - sizeF.Width / 2f, 0f);
		graphics.ResetTransform();
		graphics.TranslateTransform(20f, 20f);
		e.Graphics.DrawEllipse(pen, -18, -18, 36, 36);
		num2 = @struct.AMBIENT_WIND_DIRECTION - Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_TRUE);
		graphics.RotateTransform((float)num2);
		e.Graphics.DrawLine(pen, new Point(0, 18), new Point(0, -18));
		e.Graphics.DrawLine(pen, new Point(0, 18), new Point(3, 12));
		e.Graphics.DrawLine(pen, new Point(0, 18), new Point(-3, 12));
		string text2 = @struct.AMBIENT_WIND_VELOCITY.ToString("F0");
		graphics.ResetTransform();
		graphics.TranslateTransform(20f, 20f);
		sizeF = graphics.MeasureString(text2, font2);
		graphics.FillRectangle(brush4, new RectangleF((0f - sizeF.Width) / 2f, (0f - sizeF.Height) / 2f, sizeF.Width, sizeF.Height));
		graphics.DrawString(text2, font2, brush, 0f - sizeF.Width / 2f, (0f - sizeF.Height) / 2f);
		if (SimulatorInformationProcessing.customLocation != null)
		{
			double num8 = SimulatorInformationProcessing.currentAircraft.position.CurrentBearingDegree(SimulatorInformationProcessing.customLocation);
			graphics.ResetTransform();
			graphics.TranslateTransform(panel.Width - 20, 20f);
			e.Graphics.DrawEllipse(pen2, -18, -18, 36, 36);
			num2 = num8 - Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_TRUE);
			graphics.RotateTransform((float)num2);
			e.Graphics.DrawLine(pen2, new Point(0, 18), new Point(0, -18));
			e.Graphics.DrawLine(pen2, new Point(0, -18), new Point(3, -12));
			e.Graphics.DrawLine(pen2, new Point(0, -18), new Point(-3, -12));
			double num9 = SimulatorInformationProcessing.currentAircraft.position.DistanceFromMiles(SimulatorInformationProcessing.customLocation);
			string text3 = "";
			text3 = ((!(num9 > 9.99)) ? num9.ToString("F1") : num9.ToString("F0"));
			graphics.ResetTransform();
			graphics.TranslateTransform(panel.Width - 20, 20f);
			sizeF = graphics.MeasureString(text3, font2);
			graphics.FillRectangle(brush4, new RectangleF((0f - sizeF.Width) / 2f, (0f - sizeF.Height) / 2f, sizeF.Width, sizeF.Height));
			graphics.DrawString(text3, font2, brush2, 0f - sizeF.Width / 2f, (0f - sizeF.Height) / 2f);
		}
	}

	public static void Compass_Paint(object sender, PaintEventArgs e)
	{
		Panel panel = (Panel)sender;
		if (panel.Tag == null)
		{
			return;
		}
		SimulatorConnectionManager.Struct2 @struct = (SimulatorConnectionManager.Struct2)panel.Tag;
		int num = (int)((double)Math.Min(e.ClipRectangle.Height, e.ClipRectangle.Width) * 0.37);
		if (num < 10)
		{
			return;
		}
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
		Font font = new Font(panel.Font.FontFamily, 14f);
		Font font2 = new Font(panel.Font.FontFamily, 11f);
		Font font3 = new Font(panel.Font.FontFamily, 8f);
		Graphics graphics = e.Graphics;
		graphics.SmoothingMode = SmoothingMode.AntiAlias;
		double num2 = 0.0;
		graphics.ResetTransform();
		graphics.TranslateTransform(point.X, point.Y);
		e.Graphics.DrawEllipse(pen, new Rectangle(-num, -num, 2 * num, 2 * num));
		double num3 = (double)num * 0.1;
		int num4 = 3;
		e.Graphics.DrawLine(pen2, new Point(-num - num4, 0), new Point((int)((double)(-num) - num3 - (double)num4), 0));
		e.Graphics.DrawLine(pen2, new Point(num + num4, 0), new Point((int)((double)num + num3 + (double)num4), 0));
		e.Graphics.DrawLine(pen2, new Point(0, -num - num4), new Point(0, (int)((double)(-num) - num3 - (double)num4)));
		e.Graphics.DrawLine(pen2, new Point(0, num + num4), new Point(0, (int)((double)num + num3 + (double)num4)));
		if (trueOrMagnetic == "TRUE")
		{
			num2 = 0.0 - Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_TRUE);
		}
		else if (trueOrMagnetic == "MAG")
		{
			num2 = 0.0 - Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_MAGNETIC);
		}
		graphics.RotateTransform((float)num2);
		for (int i = 0; i < 360; i += 10)
		{
			double num5 = num;
			double num6 = (double)num * 0.96;
			if (i % 30 == 0)
			{
				num6 = (double)num * 0.89;
			}
			double num7 = Utility.DegToRad(i);
			Point pt = new Point((int)(num5 * Math.Cos(num7)), (int)(num5 * Math.Sin(num7)));
			Point pt2 = new Point((int)(num6 * Math.Cos(num7)), (int)(num6 * Math.Sin(num7)));
			if (i % 90 != 0)
			{
				e.Graphics.DrawLine(pen, pt, pt2);
			}
		}
		graphics.DrawString("N", font2, brush2, 0f - graphics.MeasureString("N", font2).Width / 2f, -num);
		graphics.RotateTransform(90f);
		graphics.DrawString("E", font2, brush, 0f - graphics.MeasureString("E", font2).Width / 2f, -num);
		graphics.RotateTransform(90f);
		graphics.DrawString("S", font2, brush, 0f - graphics.MeasureString("S", font2).Width / 2f, -num);
		graphics.RotateTransform(90f);
		graphics.DrawString("W", font2, brush, 0f - graphics.MeasureString("W", font2).Width / 2f, -num);
		SizeF sizeF;
		if (drawBuddyPilotReference)
		{
			graphics.ResetTransform();
			graphics.ResetTransform();
			graphics.TranslateTransform(point.X, point.Y);
			num2 = BuddyPilot.targetGeoHeadingDegree - Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_TRUE);
			graphics.RotateTransform((float)num2);
			int num8 = num - 10;
			int num9 = num - 20;
			e.Graphics.DrawLine(pen5, new Point(0, -num9), new Point(0, -num8));
			sizeF = graphics.MeasureString(BuddyPilot.targetGeoHeadingDegree.ToString("F0"), font2);
			graphics.DrawString(BuddyPilot.targetGeoHeadingDegree.ToString("F0"), font2, brush3, 0f - sizeF.Width / 2f, -num9);
			graphics.DrawArc(pen5, new Rectangle(-num8, -num8, 2 * num8, 2 * num8), -90f, (int)(0.0 - BuddyPilot.WindCompensationAngleDegree));
		}
		graphics.ResetTransform();
		graphics.TranslateTransform(point.X, 0f);
		string text = "";
		if (trueOrMagnetic == "TRUE")
		{
			text = ((int)Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_TRUE)).ToString("D3") + "° GEO";
		}
		else if (trueOrMagnetic == "MAG")
		{
			text = ((int)Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_MAGNETIC)).ToString("D3") + "° MAG";
		}
		sizeF = graphics.MeasureString(text, font);
		graphics.FillRectangle(brush4, new RectangleF((0f - sizeF.Width) / 2f, 0f, sizeF.Width, sizeF.Height));
		graphics.DrawString(text, font, brush, 0f - sizeF.Width / 2f, 0f);
		graphics.ResetTransform();
		graphics.TranslateTransform(20f, 20f);
		e.Graphics.DrawEllipse(pen, -18, -18, 36, 36);
		num2 = @struct.AMBIENT_WIND_DIRECTION - Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_TRUE);
		graphics.RotateTransform((float)num2);
		e.Graphics.DrawLine(pen, new Point(0, 18), new Point(0, -18));
		e.Graphics.DrawLine(pen, new Point(0, 18), new Point(3, 12));
		e.Graphics.DrawLine(pen, new Point(0, 18), new Point(-3, 12));
		string text2 = @struct.AMBIENT_WIND_VELOCITY.ToString("F0");
		graphics.ResetTransform();
		graphics.TranslateTransform(20f, 20f);
		sizeF = graphics.MeasureString(text2, font2);
		graphics.FillRectangle(brush4, new RectangleF((0f - sizeF.Width) / 2f, (0f - sizeF.Height) / 2f, sizeF.Width, sizeF.Height));
		graphics.DrawString(text2, font2, brush, 0f - sizeF.Width / 2f, (0f - sizeF.Height) / 2f);
		sizeF = graphics.MeasureString("WND", font3);
		graphics.DrawString("WND", font3, brush, 0f - sizeF.Width / 2f, (0f - sizeF.Height) / 2f + 27f);
		if (SimulatorInformationProcessing.customLocation != null)
		{
			double num10 = SimulatorInformationProcessing.currentAircraft.position.CurrentBearingDegree(SimulatorInformationProcessing.customLocation);
			graphics.ResetTransform();
			graphics.TranslateTransform(point.X, point.Y);
			num2 = num10 - Utility.RadToDeg(@struct.PLANE_HEADING_RADIANT_TRUE);
			graphics.RotateTransform((float)num2);
			int num11 = (int)((double)num * 0.78);
			int num12 = (int)((double)num * 0.03);
			e.Graphics.DrawLine(pen3, new Point(num12, -num11 + num12), new Point(0, num11));
			e.Graphics.DrawLine(pen3, new Point(-num12, -num11 + num12), new Point(0, num11));
			e.Graphics.DrawLine(pen3, new Point(0, -num11), new Point(2 * num12, -num11 + 2 * num12));
			e.Graphics.DrawLine(pen3, new Point(0, -num11), new Point(-2 * num12, -num11 + 2 * num12));
			double num13 = SimulatorInformationProcessing.currentAircraft.position.DistanceFromMiles(SimulatorInformationProcessing.customLocation);
			string text3 = SimulatorInformationProcessing.customLocation.Id + "\r\n";
			text3 = ((!(num13 > 9.99)) ? (text3 + num13.ToString("F1") + "nm") : (text3 + num13.ToString("F0") + "nm"));
			graphics.ResetTransform();
			graphics.TranslateTransform(point.X, point.Y);
			sizeF = graphics.MeasureString(text3, font2);
			graphics.FillRectangle(brush4, new RectangleF((0f - sizeF.Width) / 2f, (0f - sizeF.Height) / 2f, sizeF.Width, sizeF.Height));
			graphics.DrawRectangle(pen3, new Rectangle((int)((0f - sizeF.Width) / 2f), (int)((0f - sizeF.Height) / 2f), (int)sizeF.Width, (int)sizeF.Height));
			graphics.DrawString(text3, font2, brush2, 0f - sizeF.Width / 2f, (0f - sizeF.Height) / 2f);
		}
	}
}
