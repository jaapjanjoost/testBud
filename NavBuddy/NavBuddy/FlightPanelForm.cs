using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.FlightSimulator.SimConnect;
using NavBuddy.FormsAndControls;
using NavBuddy.Properties;

namespace NavBuddy;

public class FlightPanelForm : Form
{
	public double maxG = 1.0;

	public double minG = 1.0;

	public DoubleBufferedPanel pnlCompass = new DoubleBufferedPanel();

	public DoubleBufferedPanel pnlPitch = new DoubleBufferedPanel();

	public double currentG;

	private IContainer components = null;

	public GroupBox grpApMock;

	public Button btnAP_BC_HOLD;

	public Button btnAP_APR_HOLD;

	public Button btnAP_NAV1_HOLD;

	public Label labAUTOPILOT_HEADING_LOCK_DIR;

	public Panel pnlHDGVar;

	public Button btnAP_HDG_HOLD;

	public Label labAUTOPILOT_VERTICAL_HOLD_VAR1;

	public Panel pnlVSVar;

	public Panel pnlAltBig;

	public Panel pnlAltSmall;

	public Label labAUTOPILOT_ALTITUDE_LOCK_VAR1;

	public Button btnAP_MASTER;

	public Button btnAP_ALT_HOLD;

	public Button btnAP_VS_HOLD;

	public ImageList imageList16;

	public Label labIAS;

	public Label label2;

	public Label label4;

	public Label label3;

	public Label label5;

	public Label labGS;

	public Label label8;

	public Label labAltitude;

	public Label label11;

	public Label labVS;

	public GroupBox grpAltitude;

	public GroupBox grpSpeed;

	public GroupBox grpGMeter;

	public Label labGmin;

	public Label labGmax;

	public Button btnResetGmeter;

	public Label labGcurr;

	public Label label7;

	public Label label6;

	public Label label1;

	private Panel plnGmeter;

	public GroupBox grpThrottle;

	public Label labThrottle;

	public TrackBar throtTrack;

	public GroupBox grpOther;

	public Button btnPARKING_BRAKES;

	public Button btnFlapsDown;

	public Button btnFlapsUP;

	public Label labFlaps;

	public Label label9;

	public TrackBar trackBar_Elevator;

	public TrackBar trackBar_Ailerons;

	public TrackBar trackBar_Rudder;

	public GroupBox groupBox1;

	public Button btnResetManualControls;

	public Button btnExpandTabControl;

	public Label label12;

	public Label labAltitudeAGL;

	public Button btnGEAR_TOGGLE;

	public GroupBox groupBox2;

	public Label labAISystem;

	public Label labAI_VNAV;

	public Label labAI_LNAV;

	public Label labAISpeed;

	public Label labAI;

	public Button btnAIPilot;

	public Label labAiDebug;

	public GroupBox grpHeading;

	public Label lblElevator;

	public Label lblRudder;

	public Label lblAilerons;

	public GroupBox grpPitch;

	public GroupBox grpSystems;

	private Button button3;

	private Button button4;

	public FlightPanelForm()
	{
		InitializeComponent();
		DoubleBuffered = true;
		pnlCompass.BackColor = Color.Black;
		pnlCompass.Location = new Point(3, 14);
		pnlCompass.Name = "pnlHeading";
		pnlCompass.Size = new Size(148, 115);
		pnlCompass.TabIndex = 0;
		pnlCompass.Paint += PanelCompassPainter.BuddyPilotCompass_Paint;
		grpHeading.Controls.Add(pnlCompass);
		pnlPitch.BackColor = Color.Black;
		pnlPitch.Location = new Point(3, 14);
		pnlPitch.Name = "pnlSide";
		pnlPitch.Size = new Size(148, 115);
		pnlPitch.TabIndex = 0;
		pnlPitch.Paint += pnlPitch_Paint;
		grpPitch.Controls.Add(pnlPitch);
	}

	private void FlightControls_Load(object sender, EventArgs e)
	{
		pnlAltBig.MouseWheel += panels_MouseWheel;
		pnlAltSmall.MouseWheel += panels_MouseWheel;
		pnlVSVar.MouseWheel += panels_MouseWheel;
		pnlHDGVar.MouseWheel += panels_MouseWheel;
	}

	public void ProcessSmallInfroFromSimulator(SimulatorConnectionManager.Struct2 simulatorData)
	{
		try
		{
			btnToggleStatusWithSim_UpdateStatus(btnAP_VS_HOLD, simulatorData.autopilotverticalhold);
			btnToggleStatusWithSim_UpdateStatus(btnAP_MASTER, simulatorData.autopilotmaster);
			btnToggleStatusWithSim_UpdateStatus(btnAP_ALT_HOLD, simulatorData.autopilotaltitudelock);
			labAUTOPILOT_ALTITUDE_LOCK_VAR1.Text = simulatorData.autopilotaltitudelockvar.ToString();
			labAUTOPILOT_VERTICAL_HOLD_VAR1.Text = simulatorData.autopilotverticalholdvar.ToString();
			labAUTOPILOT_HEADING_LOCK_DIR.Text = simulatorData.autopilotheadinglockdir.ToString("F0");
			btnToggleStatusWithSim_UpdateStatus(btnAP_HDG_HOLD, simulatorData.autopilotheadinglock);
			btnToggleStatusWithSim_UpdateStatus(btnAP_APR_HOLD, simulatorData.autopilotapproachhold);
			btnToggleStatusWithSim_UpdateStatus(btnAP_NAV1_HOLD, simulatorData.autopilotnav1lock);
			btnToggleStatusWithSim_UpdateStatus(btnAP_BC_HOLD, simulatorData.autopillotbackcoursehold);
			btnToggleStatusWithSim_UpdateStatus(btnPARKING_BRAKES, simulatorData.BRAKE_PARKING_INDICATOR);
			btnToggleStatusWithSim_UpdateStatus(btnGEAR_TOGGLE, simulatorData.GEAR_HANDLE_POSITION);
			labIAS.Text = simulatorData.AIRSPEED_INDICATED.ToString("F0");
			labGS.Text = simulatorData.GROUND_VELOCITY.ToString("F0");
			labAltitude.Text = simulatorData.INDICATED_ALTITUDE.ToString("F0");
			labAltitudeAGL.Text = simulatorData.PLANE_ALT_ABOVE_GROUND.ToString("F0");
			labVS.Text = (60.0 * simulatorData.VERTICAL_SPEED).ToString("F0");
			if (simulatorData.gforce > maxG)
			{
				maxG = simulatorData.gforce;
				labGmax.Text = maxG.ToString("F2");
			}
			if (simulatorData.gforce < minG)
			{
				minG = simulatorData.gforce;
				labGmin.Text = minG.ToString("F2");
			}
			labGcurr.Text = simulatorData.gforce.ToString("F2");
			currentG = simulatorData.gforce;
			plnGmeter.Refresh();
			throtTrack.Value = (int)simulatorData.GENERAL_ENG_THROTTLE_LEVER_POSITION_1;
			labThrottle.Text = ((int)simulatorData.GENERAL_ENG_THROTTLE_LEVER_POSITION_1).ToString("F0") + "%";
			labFlaps.Text = simulatorData.FLAPS_HANDLE_PERCENT + "%";
			trackBar_Ailerons.Value = (int)(simulatorData.AILERON_POSITION * 100.0);
			trackBar_Elevator.Value = (int)(simulatorData.ELEVATOR_POSITION * 100.0);
			trackBar_Rudder.Value = (int)(simulatorData.RUDDER_POSITION * 100.0);
			lblAilerons.Text = (simulatorData.AILERON_POSITION * 100.0).ToString("F0");
			lblElevator.Text = (simulatorData.ELEVATOR_POSITION * 100.0).ToString("F0");
			lblRudder.Text = (simulatorData.RUDDER_POSITION * 100.0).ToString("F0");
			pnlCompass.Tag = simulatorData;
			pnlCompass.Refresh();
			pnlPitch.Tag = simulatorData;
			pnlPitch.Refresh();
		}
		catch (Exception)
		{
		}
	}

	private void panels_MouseWheel(object sender, MouseEventArgs e)
	{
		Panel panel = (Panel)sender;
		if (SimulatorConnectionManager.MySim == null)
		{
			return;
		}
		if (panel.Name == pnlAltSmall.Name)
		{
			for (int i = 0; i < Math.Abs(e.Delta / SystemInformation.MouseWheelScrollDelta); i++)
			{
				if (e.Delta > 0)
				{
					SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.AP_ALT_VAR_INC, 0u, SimulatorConnectionManager.GROUP.ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
				}
				else
				{
					SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.AP_ALT_VAR_DEC, 0u, SimulatorConnectionManager.GROUP.ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
				}
			}
		}
		else if (panel.Name == pnlAltBig.Name && !pnlAltSmall.ClientRectangle.Contains(pnlAltSmall.PointToClient(Cursor.Position)))
		{
			for (int j = 0; j < 10 * Math.Abs(e.Delta / SystemInformation.MouseWheelScrollDelta); j++)
			{
				if (e.Delta > 0)
				{
					SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.AP_ALT_VAR_INC, 0u, SimulatorConnectionManager.GROUP.ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
				}
				else
				{
					SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.AP_ALT_VAR_DEC, 0u, SimulatorConnectionManager.GROUP.ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
				}
			}
		}
		else if (panel.Name == pnlVSVar.Name)
		{
			for (int k = 0; k < Math.Abs(e.Delta / SystemInformation.MouseWheelScrollDelta); k++)
			{
				if (e.Delta > 0)
				{
					SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.AP_VS_VAR_INC, 0u, SimulatorConnectionManager.GROUP.ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
				}
				else
				{
					SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.AP_VS_VAR_DEC, 0u, SimulatorConnectionManager.GROUP.ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
				}
			}
		}
		else
		{
			if (!(panel.Name == pnlHDGVar.Name))
			{
				return;
			}
			for (int l = 0; l < Math.Abs(e.Delta / SystemInformation.MouseWheelScrollDelta); l++)
			{
				if (e.Delta > 0)
				{
					SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.HEADING_BUG_INC, 0u, SimulatorConnectionManager.GROUP.ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
				}
				else
				{
					SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.HEADING_BUG_DEC, 0u, SimulatorConnectionManager.GROUP.ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
				}
			}
		}
	}

	public void btnToggleStatusWithSim(object sender, EventArgs e)
	{
		try
		{
			string value = ((Button)sender).Name.Trim().Replace("btn", "");
			int eventValue = (int)Enum.Parse(typeof(SimulatorConnectionManager.EVENTS), value);
			SimulatorConnectionManager.TransmitEvent_ControlledFrequency((SimulatorConnectionManager.EVENTS)eventValue, 1, 2f);
			Button button = (Button)sender;
			if (button.Tag == null || !(bool)button.Tag)
			{
				button.Tag = true;
				button.Image = imageList16.Images["SpyOn"];
			}
			else
			{
				button.Tag = false;
				button.Image = imageList16.Images["SpyOff"];
			}
			button.Refresh();
		}
		catch (Exception ex)
		{
			SimulatorConnectionManager.WriteLogNL(ex.Message);
		}
	}

	public void btnToggleStatusWithSim_UpdateStatus(Button btn, bool status)
	{
		if (btn.Tag == null || (bool)btn.Tag != status)
		{
			btn.Tag = status;
			if (status)
			{
				btn.Image = imageList16.Images["SpyOn"];
			}
			else
			{
				btn.Image = imageList16.Images["SpyOff"];
			}
			btn.Refresh();
		}
	}

	private void plnGmeter_Paint(object sender, PaintEventArgs e)
	{
		try
		{
			e.Graphics.Clear(Color.Black);
			int num = 0;
			int num2 = plnGmeter.Width;
			if (currentG >= 1.0)
			{
				byte b = (byte)Math.Min(255.0, 255.0 * (2.0 - currentG));
				Color color = Color.FromArgb(255 - b, b, 0);
				e.Graphics.FillRectangle(new SolidBrush(color), new Rectangle(num, (int)((2.0 - currentG) * (double)(plnGmeter.Height / 2)), num2, (int)((currentG - 1.0) * (double)(plnGmeter.Height / 2))));
			}
			else
			{
				byte b2 = (byte)Math.Min(255.0, 255.0 * (currentG - 1.0));
				Color color2 = Color.FromArgb(255 - b2, b2, 0);
				e.Graphics.FillRectangle(new SolidBrush(color2), new Rectangle(num, plnGmeter.Height / 2, num2, (int)((1.0 - currentG) * (double)(plnGmeter.Height / 2))));
			}
		}
		catch (Exception)
		{
		}
	}

	private void btnResetGmeter_Click(object sender, EventArgs e)
	{
		maxG = double.MinValue;
		minG = double.MaxValue;
	}

	private void throtTrack_Scroll(object sender, EventArgs e)
	{
		SimulatorConnectionManager.Struct5 @struct = new SimulatorConnectionManager.Struct5
		{
			GENERAL_ENG_THROTTLE_LEVER_POSITION_1 = throtTrack.Value,
			GENERAL_ENG_THROTTLE_LEVER_POSITION_2 = throtTrack.Value,
			GENERAL_ENG_THROTTLE_LEVER_POSITION_3 = throtTrack.Value,
			GENERAL_ENG_THROTTLE_LEVER_POSITION_4 = throtTrack.Value
		};
		labThrottle.Text = ((int)@struct.GENERAL_ENG_THROTTLE_LEVER_POSITION_1).ToString("F0") + "%";
		SimulatorConnectionManager.Sim_TransmitDataToSimConnect(SimulatorConnectionManager.DEFINITIONS1.STRUCT5, @struct);
	}

	public void btnFlapsUP_Click(object sender, EventArgs e)
	{
		SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.FLAPS_DECR, 0u, SimulatorConnectionManager.GROUP.ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
		SimulatorConnectionManager.WriteLog("Event FLAPS_DECR sent");
	}

	public void btnFlapsDown_Click(object sender, EventArgs e)
	{
		SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, SimulatorConnectionManager.EVENTS.FLAPS_INCR, 0u, SimulatorConnectionManager.GROUP.ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
		SimulatorConnectionManager.WriteLog("Event FLAPS_INCR sent");
	}

	private void trackBar2_Scroll(object sender, EventArgs e)
	{
		SimulatorInformationProcessing.SetSurfaceControls((double)trackBar_Ailerons.Value / 100.0, (double)trackBar_Elevator.Value / 100.0, (double)trackBar_Rudder.Value / 100.0);
	}

	private void trackBar3_Scroll(object sender, EventArgs e)
	{
		SimulatorInformationProcessing.SetSurfaceControls((double)trackBar_Ailerons.Value / 100.0, (double)trackBar_Elevator.Value / 100.0, (double)trackBar_Rudder.Value / 100.0);
	}

	private void trackBar1_Scroll(object sender, EventArgs e)
	{
		SimulatorInformationProcessing.SetSurfaceControls((double)trackBar_Ailerons.Value / 100.0, (double)trackBar_Elevator.Value / 100.0, (double)trackBar_Rudder.Value / 100.0);
	}

	private void btnResetManualControls_Click(object sender, EventArgs e)
	{
		trackBar_Elevator.Value = 0;
		trackBar_Ailerons.Value = 0;
		trackBar_Rudder.Value = 0;
		SimulatorInformationProcessing.SetSurfaceControls(0.0, 0.0, 0.0);
	}

	private void btnExpandTabControl_Click(object sender, EventArgs e)
	{
		if (base.Size.Height > 172)
		{
			base.Size = new Size(base.Size.Width, 172);
		}
		else
		{
			base.Size = new Size(base.Size.Width, 308);
		}
	}

	private void btnAIPilot_Click(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		if (button.Tag == null || !(bool)button.Tag)
		{
			if (BuddyPilot.CheckPrecondition())
			{
				BuddyPilot.ActivationProcedure();
				button.Tag = true;
				button.Image = imageList16.Images["SpyOn"];
			}
		}
		else
		{
			BuddyPilot.DeactivationProcedure();
			button.Tag = false;
			button.Image = imageList16.Images["SpyOff"];
		}
		button.Refresh();
	}

	private void pnlPitch_Paint(object sender, PaintEventArgs e)
	{
		if (pnlPitch.Tag != null)
		{
			SimulatorConnectionManager.Struct2 @struct = (SimulatorConnectionManager.Struct2)pnlPitch.Tag;
			Point point = new Point(e.ClipRectangle.Width / 2, e.ClipRectangle.Height / 2);
			Brush brush = new SolidBrush(Color.White);
			Pen pen = new Pen(brush);
			Brush brush2 = new SolidBrush(Color.Lime);
			Pen pen2 = new Pen(brush2);
			Brush brush3 = new SolidBrush(Color.Cyan);
			Pen pen3 = new Pen(brush3);
			Brush brush4 = new SolidBrush(Color.Black);
			Pen pen4 = new Pen(brush4);
			int num = e.ClipRectangle.Width / 2;
			int num2 = num - 5;
			int num3 = num - 1;
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Image cessnaPROJ = Resources.cessnaPROJ;
			graphics.ResetTransform();
			graphics.TranslateTransform(point.X, point.Y);
			double num4 = 0.0 - Utility.RadToDeg(@struct.PLANE_PITCH_RADIANT);
			graphics.RotateTransform((float)num4);
			graphics.DrawImage(cessnaPROJ, -cessnaPROJ.Width / 2, -cessnaPROJ.Height / 2);
			double num5 = Utility.RadToDeg(@struct.INCIDENCE_ALPHA);
			graphics.RotateTransform(0f - (float)num5);
			graphics.DrawLine(pen3, new Point(0, 0), new Point(num, 0));
			graphics.DrawLine(pen3, new Point(num3, 3), new Point(num, 0));
			graphics.DrawLine(pen3, new Point(num3, -3), new Point(num, 0));
			graphics.ResetTransform();
			graphics.TranslateTransform(point.X, point.Y);
			graphics.DrawArc(pen2, new Rectangle(-num, -num, 2 * num, 2 * num), 0f, 360f);
			graphics.RotateTransform(-30f);
			for (int i = 0; i < 7; i++)
			{
				graphics.DrawLine(pen2, new Point(-num3, 0), new Point(-num2, 0));
				graphics.DrawLine(pen2, new Point(num3, 0), new Point(num2, 0));
				graphics.RotateTransform(10f);
			}
			Font font = new Font(Font.FontFamily, 10f);
			graphics.ResetTransform();
			graphics.TranslateTransform(point.X, point.Y + 40);
			string s = "pitch: " + num4.ToString("F1") + "°";
			SizeF sizeF = graphics.MeasureString(s, font);
			graphics.DrawString(s, font, brush2, 0f - sizeF.Width / 2f, (0f - sizeF.Height) / 2f);
			graphics.ResetTransform();
			graphics.TranslateTransform(point.X, point.Y - 40);
			string s2 = "AoA: " + num5.ToString("F1") + "°";
			sizeF = graphics.MeasureString(s2, font);
			graphics.DrawString(s2, font, brush3, 0f - sizeF.Width / 2f, (0f - sizeF.Height) / 2f);
		}
	}

	private Point AnglePoint(Point center, double angle, double distance)
	{
		return center + new Size((int)(distance * Math.Cos(angle)), -(int)(distance * Math.Sin(angle)));
	}

	private void button3_Click(object sender, EventArgs e)
	{
		SimulatorConnectionManager.TransmitEvent_ControlledFrequency(SimulatorConnectionManager.EVENTS.ENGINE_AUTO_SHUTDOWN, 1, 0.2f);
		SimulatorConnectionManager.TransmitEvent_ControlledFrequency(SimulatorConnectionManager.EVENTS.MASTER_BATTERY_OFF, 1, 0.2f);
	}

	private void button4_Click(object sender, EventArgs e)
	{
		SimulatorConnectionManager.TransmitEvent_ControlledFrequency(SimulatorConnectionManager.EVENTS.MASTER_BATTERY_ON, 1, 0.2f);
		SimulatorConnectionManager.TransmitEvent_ControlledFrequency(SimulatorConnectionManager.EVENTS.ENGINE_AUTO_START, 1, 0.2f);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavBuddy.FlightPanelForm));
		this.grpApMock = new System.Windows.Forms.GroupBox();
		this.btnAP_BC_HOLD = new System.Windows.Forms.Button();
		this.btnAP_APR_HOLD = new System.Windows.Forms.Button();
		this.btnAP_NAV1_HOLD = new System.Windows.Forms.Button();
		this.labAUTOPILOT_HEADING_LOCK_DIR = new System.Windows.Forms.Label();
		this.pnlHDGVar = new System.Windows.Forms.Panel();
		this.btnAP_HDG_HOLD = new System.Windows.Forms.Button();
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1 = new System.Windows.Forms.Label();
		this.pnlVSVar = new System.Windows.Forms.Panel();
		this.pnlAltBig = new System.Windows.Forms.Panel();
		this.pnlAltSmall = new System.Windows.Forms.Panel();
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1 = new System.Windows.Forms.Label();
		this.btnAP_MASTER = new System.Windows.Forms.Button();
		this.btnAP_ALT_HOLD = new System.Windows.Forms.Button();
		this.btnAP_VS_HOLD = new System.Windows.Forms.Button();
		this.imageList16 = new System.Windows.Forms.ImageList(this.components);
		this.labIAS = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.labGS = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.labAltitude = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.labVS = new System.Windows.Forms.Label();
		this.grpAltitude = new System.Windows.Forms.GroupBox();
		this.label12 = new System.Windows.Forms.Label();
		this.labAltitudeAGL = new System.Windows.Forms.Label();
		this.grpSpeed = new System.Windows.Forms.GroupBox();
		this.grpGMeter = new System.Windows.Forms.GroupBox();
		this.labGmin = new System.Windows.Forms.Label();
		this.labGmax = new System.Windows.Forms.Label();
		this.btnResetGmeter = new System.Windows.Forms.Button();
		this.labGcurr = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.plnGmeter = new System.Windows.Forms.Panel();
		this.grpThrottle = new System.Windows.Forms.GroupBox();
		this.labThrottle = new System.Windows.Forms.Label();
		this.throtTrack = new System.Windows.Forms.TrackBar();
		this.grpOther = new System.Windows.Forms.GroupBox();
		this.btnGEAR_TOGGLE = new System.Windows.Forms.Button();
		this.labFlaps = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.btnFlapsDown = new System.Windows.Forms.Button();
		this.btnFlapsUP = new System.Windows.Forms.Button();
		this.btnPARKING_BRAKES = new System.Windows.Forms.Button();
		this.trackBar_Elevator = new System.Windows.Forms.TrackBar();
		this.trackBar_Ailerons = new System.Windows.Forms.TrackBar();
		this.trackBar_Rudder = new System.Windows.Forms.TrackBar();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.lblElevator = new System.Windows.Forms.Label();
		this.lblRudder = new System.Windows.Forms.Label();
		this.lblAilerons = new System.Windows.Forms.Label();
		this.btnResetManualControls = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.labAiDebug = new System.Windows.Forms.Label();
		this.btnAIPilot = new System.Windows.Forms.Button();
		this.labAISystem = new System.Windows.Forms.Label();
		this.labAI_VNAV = new System.Windows.Forms.Label();
		this.labAI_LNAV = new System.Windows.Forms.Label();
		this.labAISpeed = new System.Windows.Forms.Label();
		this.labAI = new System.Windows.Forms.Label();
		this.grpHeading = new System.Windows.Forms.GroupBox();
		this.grpPitch = new System.Windows.Forms.GroupBox();
		this.grpSystems = new System.Windows.Forms.GroupBox();
		this.button4 = new System.Windows.Forms.Button();
		this.button3 = new System.Windows.Forms.Button();
		this.btnExpandTabControl = new System.Windows.Forms.Button();
		this.grpApMock.SuspendLayout();
		this.pnlAltBig.SuspendLayout();
		this.grpAltitude.SuspendLayout();
		this.grpSpeed.SuspendLayout();
		this.grpGMeter.SuspendLayout();
		this.grpThrottle.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.throtTrack).BeginInit();
		this.grpOther.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.trackBar_Elevator).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.trackBar_Ailerons).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.trackBar_Rudder).BeginInit();
		this.groupBox1.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.grpSystems.SuspendLayout();
		base.SuspendLayout();
		this.grpApMock.BackColor = System.Drawing.Color.Black;
		this.grpApMock.Controls.Add(this.btnAP_BC_HOLD);
		this.grpApMock.Controls.Add(this.btnAP_APR_HOLD);
		this.grpApMock.Controls.Add(this.btnAP_NAV1_HOLD);
		this.grpApMock.Controls.Add(this.labAUTOPILOT_HEADING_LOCK_DIR);
		this.grpApMock.Controls.Add(this.pnlHDGVar);
		this.grpApMock.Controls.Add(this.btnAP_HDG_HOLD);
		this.grpApMock.Controls.Add(this.labAUTOPILOT_VERTICAL_HOLD_VAR1);
		this.grpApMock.Controls.Add(this.pnlVSVar);
		this.grpApMock.Controls.Add(this.pnlAltBig);
		this.grpApMock.Controls.Add(this.labAUTOPILOT_ALTITUDE_LOCK_VAR1);
		this.grpApMock.Controls.Add(this.btnAP_MASTER);
		this.grpApMock.Controls.Add(this.btnAP_ALT_HOLD);
		this.grpApMock.Controls.Add(this.btnAP_VS_HOLD);
		this.grpApMock.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpApMock.ForeColor = System.Drawing.Color.White;
		this.grpApMock.Location = new System.Drawing.Point(592, 129);
		this.grpApMock.Name = "grpApMock";
		this.grpApMock.Size = new System.Drawing.Size(377, 134);
		this.grpApMock.TabIndex = 28;
		this.grpApMock.TabStop = false;
		this.grpApMock.Text = "AP";
		this.btnAP_BC_HOLD.BackColor = System.Drawing.Color.Gray;
		this.btnAP_BC_HOLD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAP_BC_HOLD.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAP_BC_HOLD.ForeColor = System.Drawing.Color.Black;
		this.btnAP_BC_HOLD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAP_BC_HOLD.Location = new System.Drawing.Point(265, 94);
		this.btnAP_BC_HOLD.Name = "btnAP_BC_HOLD";
		this.btnAP_BC_HOLD.Size = new System.Drawing.Size(52, 29);
		this.btnAP_BC_HOLD.TabIndex = 49;
		this.btnAP_BC_HOLD.Text = "BC";
		this.btnAP_BC_HOLD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAP_BC_HOLD.UseVisualStyleBackColor = false;
		this.btnAP_BC_HOLD.Click += new System.EventHandler(btnToggleStatusWithSim);
		this.btnAP_APR_HOLD.BackColor = System.Drawing.Color.Gray;
		this.btnAP_APR_HOLD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAP_APR_HOLD.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAP_APR_HOLD.ForeColor = System.Drawing.Color.Black;
		this.btnAP_APR_HOLD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAP_APR_HOLD.Location = new System.Drawing.Point(265, 55);
		this.btnAP_APR_HOLD.Name = "btnAP_APR_HOLD";
		this.btnAP_APR_HOLD.Size = new System.Drawing.Size(52, 26);
		this.btnAP_APR_HOLD.TabIndex = 48;
		this.btnAP_APR_HOLD.Text = "APR";
		this.btnAP_APR_HOLD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAP_APR_HOLD.UseVisualStyleBackColor = false;
		this.btnAP_APR_HOLD.Click += new System.EventHandler(btnToggleStatusWithSim);
		this.btnAP_NAV1_HOLD.BackColor = System.Drawing.Color.Gray;
		this.btnAP_NAV1_HOLD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAP_NAV1_HOLD.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAP_NAV1_HOLD.ForeColor = System.Drawing.Color.Black;
		this.btnAP_NAV1_HOLD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAP_NAV1_HOLD.Location = new System.Drawing.Point(265, 19);
		this.btnAP_NAV1_HOLD.Name = "btnAP_NAV1_HOLD";
		this.btnAP_NAV1_HOLD.Size = new System.Drawing.Size(52, 26);
		this.btnAP_NAV1_HOLD.TabIndex = 47;
		this.btnAP_NAV1_HOLD.Text = "NAV";
		this.btnAP_NAV1_HOLD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAP_NAV1_HOLD.UseVisualStyleBackColor = false;
		this.btnAP_NAV1_HOLD.Click += new System.EventHandler(btnToggleStatusWithSim);
		this.labAUTOPILOT_HEADING_LOCK_DIR.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.labAUTOPILOT_HEADING_LOCK_DIR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.labAUTOPILOT_HEADING_LOCK_DIR.Font = new System.Drawing.Font("Calibri", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAUTOPILOT_HEADING_LOCK_DIR.ForeColor = System.Drawing.Color.Aqua;
		this.labAUTOPILOT_HEADING_LOCK_DIR.Location = new System.Drawing.Point(212, 20);
		this.labAUTOPILOT_HEADING_LOCK_DIR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAUTOPILOT_HEADING_LOCK_DIR.Name = "labAUTOPILOT_HEADING_LOCK_DIR";
		this.labAUTOPILOT_HEADING_LOCK_DIR.Size = new System.Drawing.Size(42, 27);
		this.labAUTOPILOT_HEADING_LOCK_DIR.TabIndex = 46;
		this.labAUTOPILOT_HEADING_LOCK_DIR.Text = "000";
		this.labAUTOPILOT_HEADING_LOCK_DIR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.pnlHDGVar.BackColor = System.Drawing.Color.Transparent;
		this.pnlHDGVar.BackgroundImage = (System.Drawing.Image)resources.GetObject("pnlHDGVar.BackgroundImage");
		this.pnlHDGVar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.pnlHDGVar.Location = new System.Drawing.Point(218, 54);
		this.pnlHDGVar.Name = "pnlHDGVar";
		this.pnlHDGVar.Size = new System.Drawing.Size(30, 30);
		this.pnlHDGVar.TabIndex = 45;
		this.btnAP_HDG_HOLD.BackColor = System.Drawing.Color.Gray;
		this.btnAP_HDG_HOLD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAP_HDG_HOLD.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAP_HDG_HOLD.ForeColor = System.Drawing.Color.Black;
		this.btnAP_HDG_HOLD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAP_HDG_HOLD.Location = new System.Drawing.Point(207, 94);
		this.btnAP_HDG_HOLD.Name = "btnAP_HDG_HOLD";
		this.btnAP_HDG_HOLD.Size = new System.Drawing.Size(52, 29);
		this.btnAP_HDG_HOLD.TabIndex = 44;
		this.btnAP_HDG_HOLD.Text = "HDG";
		this.btnAP_HDG_HOLD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAP_HDG_HOLD.UseVisualStyleBackColor = false;
		this.btnAP_HDG_HOLD.Click += new System.EventHandler(btnToggleStatusWithSim);
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1.Font = new System.Drawing.Font("Calibri", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1.ForeColor = System.Drawing.Color.Aqua;
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1.Location = new System.Drawing.Point(137, 20);
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1.Name = "labAUTOPILOT_VERTICAL_HOLD_VAR1";
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1.Size = new System.Drawing.Size(63, 27);
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1.TabIndex = 43;
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1.Text = "0000";
		this.labAUTOPILOT_VERTICAL_HOLD_VAR1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.pnlVSVar.BackColor = System.Drawing.Color.Transparent;
		this.pnlVSVar.BackgroundImage = (System.Drawing.Image)resources.GetObject("pnlVSVar.BackgroundImage");
		this.pnlVSVar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.pnlVSVar.Location = new System.Drawing.Point(153, 54);
		this.pnlVSVar.Name = "pnlVSVar";
		this.pnlVSVar.Size = new System.Drawing.Size(30, 30);
		this.pnlVSVar.TabIndex = 42;
		this.pnlAltBig.BackColor = System.Drawing.Color.Transparent;
		this.pnlAltBig.BackgroundImage = (System.Drawing.Image)resources.GetObject("pnlAltBig.BackgroundImage");
		this.pnlAltBig.Controls.Add(this.pnlAltSmall);
		this.pnlAltBig.Location = new System.Drawing.Point(77, 49);
		this.pnlAltBig.Name = "pnlAltBig";
		this.pnlAltBig.Size = new System.Drawing.Size(40, 40);
		this.pnlAltBig.TabIndex = 40;
		this.pnlAltSmall.BackColor = System.Drawing.Color.Transparent;
		this.pnlAltSmall.BackgroundImage = (System.Drawing.Image)resources.GetObject("pnlAltSmall.BackgroundImage");
		this.pnlAltSmall.Location = new System.Drawing.Point(10, 10);
		this.pnlAltSmall.Name = "pnlAltSmall";
		this.pnlAltSmall.Size = new System.Drawing.Size(20, 20);
		this.pnlAltSmall.TabIndex = 41;
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1.Font = new System.Drawing.Font("Calibri", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1.ForeColor = System.Drawing.Color.Aqua;
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1.Location = new System.Drawing.Point(66, 20);
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1.Name = "labAUTOPILOT_ALTITUDE_LOCK_VAR1";
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1.Size = new System.Drawing.Size(63, 27);
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1.TabIndex = 38;
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1.Text = "00000";
		this.labAUTOPILOT_ALTITUDE_LOCK_VAR1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnAP_MASTER.BackColor = System.Drawing.Color.Gray;
		this.btnAP_MASTER.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAP_MASTER.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAP_MASTER.ForeColor = System.Drawing.Color.Black;
		this.btnAP_MASTER.Image = (System.Drawing.Image)resources.GetObject("btnAP_MASTER.Image");
		this.btnAP_MASTER.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAP_MASTER.Location = new System.Drawing.Point(6, 21);
		this.btnAP_MASTER.Name = "btnAP_MASTER";
		this.btnAP_MASTER.Size = new System.Drawing.Size(52, 102);
		this.btnAP_MASTER.TabIndex = 28;
		this.btnAP_MASTER.Text = "AP";
		this.btnAP_MASTER.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAP_MASTER.UseVisualStyleBackColor = false;
		this.btnAP_MASTER.Click += new System.EventHandler(btnToggleStatusWithSim);
		this.btnAP_ALT_HOLD.BackColor = System.Drawing.Color.Gray;
		this.btnAP_ALT_HOLD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAP_ALT_HOLD.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAP_ALT_HOLD.ForeColor = System.Drawing.Color.Black;
		this.btnAP_ALT_HOLD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAP_ALT_HOLD.Location = new System.Drawing.Point(71, 94);
		this.btnAP_ALT_HOLD.Name = "btnAP_ALT_HOLD";
		this.btnAP_ALT_HOLD.Size = new System.Drawing.Size(52, 29);
		this.btnAP_ALT_HOLD.TabIndex = 27;
		this.btnAP_ALT_HOLD.Text = "ALT";
		this.btnAP_ALT_HOLD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAP_ALT_HOLD.UseVisualStyleBackColor = false;
		this.btnAP_ALT_HOLD.Click += new System.EventHandler(btnToggleStatusWithSim);
		this.btnAP_VS_HOLD.BackColor = System.Drawing.Color.Gray;
		this.btnAP_VS_HOLD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAP_VS_HOLD.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAP_VS_HOLD.ForeColor = System.Drawing.Color.Black;
		this.btnAP_VS_HOLD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAP_VS_HOLD.Location = new System.Drawing.Point(142, 94);
		this.btnAP_VS_HOLD.Name = "btnAP_VS_HOLD";
		this.btnAP_VS_HOLD.Size = new System.Drawing.Size(52, 29);
		this.btnAP_VS_HOLD.TabIndex = 26;
		this.btnAP_VS_HOLD.Text = "VS";
		this.btnAP_VS_HOLD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAP_VS_HOLD.UseVisualStyleBackColor = false;
		this.btnAP_VS_HOLD.Click += new System.EventHandler(btnToggleStatusWithSim);
		this.imageList16.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList16.ImageStream");
		this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList16.Images.SetKeyName(0, "spyOff");
		this.imageList16.Images.SetKeyName(1, "spyOn");
		this.imageList16.Images.SetKeyName(2, "googlelink");
		this.labIAS.BackColor = System.Drawing.Color.Black;
		this.labIAS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.labIAS.Font = new System.Drawing.Font("Calibri", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labIAS.ForeColor = System.Drawing.Color.White;
		this.labIAS.Location = new System.Drawing.Point(50, 23);
		this.labIAS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labIAS.Name = "labIAS";
		this.labIAS.Size = new System.Drawing.Size(82, 45);
		this.labIAS.TabIndex = 39;
		this.labIAS.Text = "000";
		this.labIAS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.White;
		this.label2.Location = new System.Drawing.Point(10, 36);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(32, 18);
		this.label2.TabIndex = 40;
		this.label2.Text = "IAS:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.White;
		this.label4.Location = new System.Drawing.Point(140, 36);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(23, 18);
		this.label4.TabIndex = 42;
		this.label4.Text = "kn";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.White;
		this.label3.Location = new System.Drawing.Point(140, 87);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(23, 18);
		this.label3.TabIndex = 45;
		this.label3.Text = "kn";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.White;
		this.label5.Location = new System.Drawing.Point(10, 87);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(28, 18);
		this.label5.TabIndex = 44;
		this.label5.Text = "GS:";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labGS.BackColor = System.Drawing.Color.Black;
		this.labGS.Font = new System.Drawing.Font("Calibri", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labGS.ForeColor = System.Drawing.Color.Magenta;
		this.labGS.Location = new System.Drawing.Point(50, 74);
		this.labGS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labGS.Name = "labGS";
		this.labGS.Size = new System.Drawing.Size(82, 45);
		this.labGS.TabIndex = 43;
		this.labGS.Text = "000";
		this.labGS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.White;
		this.label8.Location = new System.Drawing.Point(122, 36);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(42, 18);
		this.label8.TabIndex = 47;
		this.label8.Text = "AMSL";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labAltitude.BackColor = System.Drawing.Color.Black;
		this.labAltitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.labAltitude.Font = new System.Drawing.Font("Calibri", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAltitude.ForeColor = System.Drawing.Color.White;
		this.labAltitude.Location = new System.Drawing.Point(11, 23);
		this.labAltitude.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAltitude.Name = "labAltitude";
		this.labAltitude.Size = new System.Drawing.Size(103, 45);
		this.labAltitude.TabIndex = 46;
		this.labAltitude.Text = "00000";
		this.labAltitude.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.ForeColor = System.Drawing.Color.White;
		this.label11.Location = new System.Drawing.Point(208, 27);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(43, 36);
		this.label11.TabIndex = 50;
		this.label11.Text = "VS \r\n(fpm)";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labVS.BackColor = System.Drawing.Color.Black;
		this.labVS.Font = new System.Drawing.Font("Calibri", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labVS.ForeColor = System.Drawing.Color.White;
		this.labVS.Location = new System.Drawing.Point(187, 69);
		this.labVS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labVS.Name = "labVS";
		this.labVS.Size = new System.Drawing.Size(84, 45);
		this.labVS.TabIndex = 49;
		this.labVS.Text = "0000";
		this.labVS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.grpAltitude.BackColor = System.Drawing.Color.Black;
		this.grpAltitude.Controls.Add(this.label12);
		this.grpAltitude.Controls.Add(this.labAltitudeAGL);
		this.grpAltitude.Controls.Add(this.labVS);
		this.grpAltitude.Controls.Add(this.labAltitude);
		this.grpAltitude.Controls.Add(this.label8);
		this.grpAltitude.Controls.Add(this.label11);
		this.grpAltitude.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpAltitude.ForeColor = System.Drawing.Color.White;
		this.grpAltitude.Location = new System.Drawing.Point(192, -2);
		this.grpAltitude.Name = "grpAltitude";
		this.grpAltitude.Size = new System.Drawing.Size(278, 134);
		this.grpAltitude.TabIndex = 51;
		this.grpAltitude.TabStop = false;
		this.grpAltitude.Text = "Altitude";
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.ForeColor = System.Drawing.Color.White;
		this.label12.Location = new System.Drawing.Point(122, 87);
		this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(32, 18);
		this.label12.TabIndex = 52;
		this.label12.Text = "AGL";
		this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labAltitudeAGL.BackColor = System.Drawing.Color.Black;
		this.labAltitudeAGL.Font = new System.Drawing.Font("Calibri", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAltitudeAGL.ForeColor = System.Drawing.Color.Magenta;
		this.labAltitudeAGL.Location = new System.Drawing.Point(11, 72);
		this.labAltitudeAGL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAltitudeAGL.Name = "labAltitudeAGL";
		this.labAltitudeAGL.Size = new System.Drawing.Size(103, 45);
		this.labAltitudeAGL.TabIndex = 51;
		this.labAltitudeAGL.Text = "000";
		this.labAltitudeAGL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.grpSpeed.BackColor = System.Drawing.Color.Black;
		this.grpSpeed.Controls.Add(this.label2);
		this.grpSpeed.Controls.Add(this.labIAS);
		this.grpSpeed.Controls.Add(this.label3);
		this.grpSpeed.Controls.Add(this.label4);
		this.grpSpeed.Controls.Add(this.label5);
		this.grpSpeed.Controls.Add(this.labGS);
		this.grpSpeed.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpSpeed.ForeColor = System.Drawing.Color.White;
		this.grpSpeed.Location = new System.Drawing.Point(4, -2);
		this.grpSpeed.Name = "grpSpeed";
		this.grpSpeed.Size = new System.Drawing.Size(184, 134);
		this.grpSpeed.TabIndex = 52;
		this.grpSpeed.TabStop = false;
		this.grpSpeed.Text = "Speed";
		this.grpGMeter.BackColor = System.Drawing.Color.Black;
		this.grpGMeter.Controls.Add(this.labGmin);
		this.grpGMeter.Controls.Add(this.labGmax);
		this.grpGMeter.Controls.Add(this.btnResetGmeter);
		this.grpGMeter.Controls.Add(this.labGcurr);
		this.grpGMeter.Controls.Add(this.label7);
		this.grpGMeter.Controls.Add(this.label6);
		this.grpGMeter.Controls.Add(this.label1);
		this.grpGMeter.Controls.Add(this.plnGmeter);
		this.grpGMeter.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpGMeter.ForeColor = System.Drawing.Color.White;
		this.grpGMeter.Location = new System.Drawing.Point(4, 129);
		this.grpGMeter.Name = "grpGMeter";
		this.grpGMeter.Size = new System.Drawing.Size(184, 134);
		this.grpGMeter.TabIndex = 52;
		this.grpGMeter.TabStop = false;
		this.grpGMeter.Text = "G-Meter";
		this.labGmin.AutoSize = true;
		this.labGmin.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labGmin.ForeColor = System.Drawing.Color.White;
		this.labGmin.Location = new System.Drawing.Point(39, 102);
		this.labGmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labGmin.Name = "labGmin";
		this.labGmin.Size = new System.Drawing.Size(13, 13);
		this.labGmin.TabIndex = 54;
		this.labGmin.Text = "0";
		this.labGmin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labGmax.AutoSize = true;
		this.labGmax.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labGmax.ForeColor = System.Drawing.Color.White;
		this.labGmax.Location = new System.Drawing.Point(40, 31);
		this.labGmax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labGmax.Name = "labGmax";
		this.labGmax.Size = new System.Drawing.Size(13, 13);
		this.labGmax.TabIndex = 53;
		this.labGmax.Text = "0";
		this.labGmax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnResetGmeter.BackColor = System.Drawing.Color.Gray;
		this.btnResetGmeter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnResetGmeter.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnResetGmeter.ForeColor = System.Drawing.Color.Black;
		this.btnResetGmeter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnResetGmeter.Location = new System.Drawing.Point(86, 59);
		this.btnResetGmeter.Name = "btnResetGmeter";
		this.btnResetGmeter.Size = new System.Drawing.Size(52, 25);
		this.btnResetGmeter.TabIndex = 52;
		this.btnResetGmeter.Text = "RESET";
		this.btnResetGmeter.UseVisualStyleBackColor = false;
		this.btnResetGmeter.Click += new System.EventHandler(btnResetGmeter_Click);
		this.labGcurr.AutoSize = true;
		this.labGcurr.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labGcurr.ForeColor = System.Drawing.Color.White;
		this.labGcurr.Location = new System.Drawing.Point(39, 64);
		this.labGcurr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labGcurr.Name = "labGcurr";
		this.labGcurr.Size = new System.Drawing.Size(13, 13);
		this.labGcurr.TabIndex = 51;
		this.labGcurr.Text = "0";
		this.labGcurr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.White;
		this.label7.Location = new System.Drawing.Point(6, 64);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(29, 13);
		this.label7.TabIndex = 50;
		this.label7.Text = "curr:";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.White;
		this.label6.Location = new System.Drawing.Point(6, 102);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(28, 13);
		this.label6.TabIndex = 49;
		this.label6.Text = "min:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(6, 31);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(30, 13);
		this.label1.TabIndex = 48;
		this.label1.Text = "max:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.plnGmeter.BackColor = System.Drawing.Color.Red;
		this.plnGmeter.Location = new System.Drawing.Point(64, 17);
		this.plnGmeter.Name = "plnGmeter";
		this.plnGmeter.Size = new System.Drawing.Size(16, 112);
		this.plnGmeter.TabIndex = 0;
		this.plnGmeter.Paint += new System.Windows.Forms.PaintEventHandler(plnGmeter_Paint);
		this.grpThrottle.BackColor = System.Drawing.Color.Black;
		this.grpThrottle.Controls.Add(this.labThrottle);
		this.grpThrottle.Controls.Add(this.throtTrack);
		this.grpThrottle.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpThrottle.ForeColor = System.Drawing.Color.White;
		this.grpThrottle.Location = new System.Drawing.Point(797, -2);
		this.grpThrottle.Name = "grpThrottle";
		this.grpThrottle.Size = new System.Drawing.Size(112, 134);
		this.grpThrottle.TabIndex = 53;
		this.grpThrottle.TabStop = false;
		this.grpThrottle.Text = "Throttle";
		this.labThrottle.BackColor = System.Drawing.Color.Black;
		this.labThrottle.Font = new System.Drawing.Font("Calibri", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labThrottle.ForeColor = System.Drawing.Color.White;
		this.labThrottle.Location = new System.Drawing.Point(41, 45);
		this.labThrottle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labThrottle.Name = "labThrottle";
		this.labThrottle.Size = new System.Drawing.Size(63, 45);
		this.labThrottle.TabIndex = 50;
		this.labThrottle.Text = "00%";
		this.labThrottle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.throtTrack.Location = new System.Drawing.Point(6, 15);
		this.throtTrack.Maximum = 100;
		this.throtTrack.Name = "throtTrack";
		this.throtTrack.Orientation = System.Windows.Forms.Orientation.Vertical;
		this.throtTrack.Size = new System.Drawing.Size(45, 115);
		this.throtTrack.TabIndex = 0;
		this.throtTrack.TickFrequency = 10;
		this.throtTrack.Scroll += new System.EventHandler(throtTrack_Scroll);
		this.grpOther.BackColor = System.Drawing.Color.Black;
		this.grpOther.Controls.Add(this.btnGEAR_TOGGLE);
		this.grpOther.Controls.Add(this.labFlaps);
		this.grpOther.Controls.Add(this.label9);
		this.grpOther.Controls.Add(this.btnFlapsDown);
		this.grpOther.Controls.Add(this.btnFlapsUP);
		this.grpOther.Controls.Add(this.btnPARKING_BRAKES);
		this.grpOther.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpOther.ForeColor = System.Drawing.Color.White;
		this.grpOther.Location = new System.Drawing.Point(914, -2);
		this.grpOther.Name = "grpOther";
		this.grpOther.Size = new System.Drawing.Size(152, 134);
		this.grpOther.TabIndex = 54;
		this.grpOther.TabStop = false;
		this.grpOther.Text = "Other";
		this.btnGEAR_TOGGLE.BackColor = System.Drawing.Color.Gray;
		this.btnGEAR_TOGGLE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGEAR_TOGGLE.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGEAR_TOGGLE.ForeColor = System.Drawing.Color.Black;
		this.btnGEAR_TOGGLE.Image = (System.Drawing.Image)resources.GetObject("btnGEAR_TOGGLE.Image");
		this.btnGEAR_TOGGLE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGEAR_TOGGLE.Location = new System.Drawing.Point(6, 52);
		this.btnGEAR_TOGGLE.Name = "btnGEAR_TOGGLE";
		this.btnGEAR_TOGGLE.Size = new System.Drawing.Size(140, 31);
		this.btnGEAR_TOGGLE.TabIndex = 53;
		this.btnGEAR_TOGGLE.Text = "LANDING GEAR";
		this.btnGEAR_TOGGLE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGEAR_TOGGLE.UseVisualStyleBackColor = false;
		this.btnGEAR_TOGGLE.Click += new System.EventHandler(btnToggleStatusWithSim);
		this.labFlaps.BackColor = System.Drawing.Color.Black;
		this.labFlaps.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labFlaps.ForeColor = System.Drawing.Color.White;
		this.labFlaps.Location = new System.Drawing.Point(46, 97);
		this.labFlaps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labFlaps.Name = "labFlaps";
		this.labFlaps.Size = new System.Drawing.Size(40, 21);
		this.labFlaps.TabIndex = 52;
		this.labFlaps.Text = "0";
		this.labFlaps.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Calibri", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.White;
		this.label9.Location = new System.Drawing.Point(3, 100);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(40, 14);
		this.label9.TabIndex = 51;
		this.label9.Text = "Flaps:";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnFlapsDown.BackColor = System.Drawing.Color.Gray;
		this.btnFlapsDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnFlapsDown.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnFlapsDown.ForeColor = System.Drawing.Color.Black;
		this.btnFlapsDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnFlapsDown.Location = new System.Drawing.Point(90, 109);
		this.btnFlapsDown.Name = "btnFlapsDown";
		this.btnFlapsDown.Size = new System.Drawing.Size(54, 20);
		this.btnFlapsDown.TabIndex = 30;
		this.btnFlapsDown.Text = "DOWN";
		this.btnFlapsDown.UseVisualStyleBackColor = false;
		this.btnFlapsDown.Click += new System.EventHandler(btnFlapsDown_Click);
		this.btnFlapsUP.BackColor = System.Drawing.Color.Gray;
		this.btnFlapsUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnFlapsUP.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnFlapsUP.ForeColor = System.Drawing.Color.Black;
		this.btnFlapsUP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnFlapsUP.Location = new System.Drawing.Point(90, 88);
		this.btnFlapsUP.Margin = new System.Windows.Forms.Padding(0);
		this.btnFlapsUP.Name = "btnFlapsUP";
		this.btnFlapsUP.Size = new System.Drawing.Size(54, 20);
		this.btnFlapsUP.TabIndex = 29;
		this.btnFlapsUP.Text = "UP";
		this.btnFlapsUP.UseVisualStyleBackColor = false;
		this.btnFlapsUP.Click += new System.EventHandler(btnFlapsUP_Click);
		this.btnPARKING_BRAKES.BackColor = System.Drawing.Color.Gray;
		this.btnPARKING_BRAKES.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnPARKING_BRAKES.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnPARKING_BRAKES.ForeColor = System.Drawing.Color.Black;
		this.btnPARKING_BRAKES.Image = (System.Drawing.Image)resources.GetObject("btnPARKING_BRAKES.Image");
		this.btnPARKING_BRAKES.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnPARKING_BRAKES.Location = new System.Drawing.Point(6, 16);
		this.btnPARKING_BRAKES.Name = "btnPARKING_BRAKES";
		this.btnPARKING_BRAKES.Size = new System.Drawing.Size(140, 31);
		this.btnPARKING_BRAKES.TabIndex = 28;
		this.btnPARKING_BRAKES.Text = "PARKING BRAKE";
		this.btnPARKING_BRAKES.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnPARKING_BRAKES.UseVisualStyleBackColor = false;
		this.btnPARKING_BRAKES.Click += new System.EventHandler(btnToggleStatusWithSim);
		this.trackBar_Elevator.Location = new System.Drawing.Point(3, 13);
		this.trackBar_Elevator.Maximum = 100;
		this.trackBar_Elevator.Minimum = -100;
		this.trackBar_Elevator.Name = "trackBar_Elevator";
		this.trackBar_Elevator.Orientation = System.Windows.Forms.Orientation.Vertical;
		this.trackBar_Elevator.Size = new System.Drawing.Size(45, 119);
		this.trackBar_Elevator.TabIndex = 55;
		this.trackBar_Elevator.TickFrequency = 20;
		this.trackBar_Elevator.Scroll += new System.EventHandler(trackBar1_Scroll);
		this.trackBar_Ailerons.Location = new System.Drawing.Point(69, 25);
		this.trackBar_Ailerons.Maximum = 100;
		this.trackBar_Ailerons.Minimum = -100;
		this.trackBar_Ailerons.Name = "trackBar_Ailerons";
		this.trackBar_Ailerons.Size = new System.Drawing.Size(258, 45);
		this.trackBar_Ailerons.TabIndex = 56;
		this.trackBar_Ailerons.TickFrequency = 10;
		this.trackBar_Ailerons.Scroll += new System.EventHandler(trackBar2_Scroll);
		this.trackBar_Rudder.Location = new System.Drawing.Point(69, 76);
		this.trackBar_Rudder.Maximum = 100;
		this.trackBar_Rudder.Minimum = -100;
		this.trackBar_Rudder.Name = "trackBar_Rudder";
		this.trackBar_Rudder.Size = new System.Drawing.Size(258, 45);
		this.trackBar_Rudder.TabIndex = 57;
		this.trackBar_Rudder.TickFrequency = 10;
		this.trackBar_Rudder.Scroll += new System.EventHandler(trackBar3_Scroll);
		this.groupBox1.BackColor = System.Drawing.Color.Black;
		this.groupBox1.Controls.Add(this.lblElevator);
		this.groupBox1.Controls.Add(this.lblRudder);
		this.groupBox1.Controls.Add(this.lblAilerons);
		this.groupBox1.Controls.Add(this.btnResetManualControls);
		this.groupBox1.Controls.Add(this.trackBar_Elevator);
		this.groupBox1.Controls.Add(this.trackBar_Ailerons);
		this.groupBox1.Controls.Add(this.trackBar_Rudder);
		this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.ForeColor = System.Drawing.Color.White;
		this.groupBox1.Location = new System.Drawing.Point(194, 129);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(392, 134);
		this.groupBox1.TabIndex = 54;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Manual controls";
		this.lblElevator.BackColor = System.Drawing.Color.Black;
		this.lblElevator.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblElevator.ForeColor = System.Drawing.Color.White;
		this.lblElevator.Location = new System.Drawing.Point(31, 63);
		this.lblElevator.Margin = new System.Windows.Forms.Padding(0);
		this.lblElevator.Name = "lblElevator";
		this.lblElevator.Size = new System.Drawing.Size(42, 19);
		this.lblElevator.TabIndex = 60;
		this.lblElevator.Text = "000";
		this.lblElevator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lblRudder.BackColor = System.Drawing.Color.Black;
		this.lblRudder.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblRudder.ForeColor = System.Drawing.Color.White;
		this.lblRudder.Location = new System.Drawing.Point(177, 104);
		this.lblRudder.Margin = new System.Windows.Forms.Padding(0);
		this.lblRudder.Name = "lblRudder";
		this.lblRudder.Size = new System.Drawing.Size(42, 19);
		this.lblRudder.TabIndex = 59;
		this.lblRudder.Text = "000";
		this.lblRudder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lblAilerons.BackColor = System.Drawing.Color.Black;
		this.lblAilerons.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblAilerons.ForeColor = System.Drawing.Color.White;
		this.lblAilerons.Location = new System.Drawing.Point(177, 53);
		this.lblAilerons.Margin = new System.Windows.Forms.Padding(0);
		this.lblAilerons.Name = "lblAilerons";
		this.lblAilerons.Size = new System.Drawing.Size(42, 19);
		this.lblAilerons.TabIndex = 58;
		this.lblAilerons.Text = "000";
		this.lblAilerons.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnResetManualControls.BackColor = System.Drawing.Color.Gray;
		this.btnResetManualControls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnResetManualControls.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnResetManualControls.ForeColor = System.Drawing.Color.Black;
		this.btnResetManualControls.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnResetManualControls.Location = new System.Drawing.Point(324, 86);
		this.btnResetManualControls.Name = "btnResetManualControls";
		this.btnResetManualControls.Size = new System.Drawing.Size(60, 29);
		this.btnResetManualControls.TabIndex = 53;
		this.btnResetManualControls.Text = "RESET";
		this.btnResetManualControls.UseVisualStyleBackColor = false;
		this.btnResetManualControls.Click += new System.EventHandler(btnResetManualControls_Click);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.BackColor = System.Drawing.Color.Black;
		this.groupBox2.Controls.Add(this.labAiDebug);
		this.groupBox2.Controls.Add(this.btnAIPilot);
		this.groupBox2.Controls.Add(this.labAISystem);
		this.groupBox2.Controls.Add(this.labAI_VNAV);
		this.groupBox2.Controls.Add(this.labAI_LNAV);
		this.groupBox2.Controls.Add(this.labAISpeed);
		this.groupBox2.Controls.Add(this.labAI);
		this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox2.ForeColor = System.Drawing.Color.White;
		this.groupBox2.Location = new System.Drawing.Point(1070, -2);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(259, 134);
		this.groupBox2.TabIndex = 50;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Buddy Pilot";
		this.labAiDebug.AutoSize = true;
		this.labAiDebug.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAiDebug.ForeColor = System.Drawing.Color.White;
		this.labAiDebug.Location = new System.Drawing.Point(7, 146);
		this.labAiDebug.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAiDebug.Name = "labAiDebug";
		this.labAiDebug.Size = new System.Drawing.Size(42, 15);
		this.labAiDebug.TabIndex = 53;
		this.labAiDebug.Text = "XXXXX";
		this.btnAIPilot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnAIPilot.BackColor = System.Drawing.Color.Gray;
		this.btnAIPilot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAIPilot.ForeColor = System.Drawing.Color.Black;
		this.btnAIPilot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAIPilot.Location = new System.Drawing.Point(167, 16);
		this.btnAIPilot.Name = "btnAIPilot";
		this.btnAIPilot.Size = new System.Drawing.Size(83, 31);
		this.btnAIPilot.TabIndex = 52;
		this.btnAIPilot.Text = "ACTIVE";
		this.btnAIPilot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAIPilot.UseVisualStyleBackColor = false;
		this.btnAIPilot.Click += new System.EventHandler(btnAIPilot_Click);
		this.labAISystem.AutoSize = true;
		this.labAISystem.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAISystem.ForeColor = System.Drawing.Color.Cyan;
		this.labAISystem.Location = new System.Drawing.Point(7, 41);
		this.labAISystem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAISystem.Name = "labAISystem";
		this.labAISystem.Size = new System.Drawing.Size(42, 15);
		this.labAISystem.TabIndex = 51;
		this.labAISystem.Text = "XXXXX";
		this.labAI_VNAV.AutoSize = true;
		this.labAI_VNAV.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAI_VNAV.ForeColor = System.Drawing.Color.Cyan;
		this.labAI_VNAV.Location = new System.Drawing.Point(7, 107);
		this.labAI_VNAV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAI_VNAV.Name = "labAI_VNAV";
		this.labAI_VNAV.Size = new System.Drawing.Size(42, 15);
		this.labAI_VNAV.TabIndex = 50;
		this.labAI_VNAV.Text = "XXXXX";
		this.labAI_LNAV.AutoSize = true;
		this.labAI_LNAV.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAI_LNAV.ForeColor = System.Drawing.Color.Cyan;
		this.labAI_LNAV.Location = new System.Drawing.Point(7, 85);
		this.labAI_LNAV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAI_LNAV.Name = "labAI_LNAV";
		this.labAI_LNAV.Size = new System.Drawing.Size(42, 15);
		this.labAI_LNAV.TabIndex = 49;
		this.labAI_LNAV.Text = "XXXXX";
		this.labAISpeed.AutoSize = true;
		this.labAISpeed.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAISpeed.ForeColor = System.Drawing.Color.Cyan;
		this.labAISpeed.Location = new System.Drawing.Point(7, 63);
		this.labAISpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAISpeed.Name = "labAISpeed";
		this.labAISpeed.Size = new System.Drawing.Size(42, 15);
		this.labAISpeed.TabIndex = 48;
		this.labAISpeed.Text = "XXXXX";
		this.labAI.AutoSize = true;
		this.labAI.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAI.ForeColor = System.Drawing.Color.Cyan;
		this.labAI.Location = new System.Drawing.Point(7, 19);
		this.labAI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAI.Name = "labAI";
		this.labAI.Size = new System.Drawing.Size(42, 15);
		this.labAI.TabIndex = 47;
		this.labAI.Text = "XXXXX";
		this.grpHeading.BackColor = System.Drawing.Color.Black;
		this.grpHeading.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpHeading.ForeColor = System.Drawing.Color.White;
		this.grpHeading.Location = new System.Drawing.Point(476, -2);
		this.grpHeading.Name = "grpHeading";
		this.grpHeading.Size = new System.Drawing.Size(155, 134);
		this.grpHeading.TabIndex = 60;
		this.grpHeading.TabStop = false;
		this.grpHeading.Text = "Heading";
		this.grpPitch.BackColor = System.Drawing.Color.Black;
		this.grpPitch.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpPitch.ForeColor = System.Drawing.Color.White;
		this.grpPitch.Location = new System.Drawing.Point(637, -2);
		this.grpPitch.Name = "grpPitch";
		this.grpPitch.Size = new System.Drawing.Size(155, 134);
		this.grpPitch.TabIndex = 61;
		this.grpPitch.TabStop = false;
		this.grpPitch.Text = "Pitch";
		this.grpSystems.BackColor = System.Drawing.Color.Black;
		this.grpSystems.Controls.Add(this.button4);
		this.grpSystems.Controls.Add(this.button3);
		this.grpSystems.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpSystems.ForeColor = System.Drawing.Color.White;
		this.grpSystems.Location = new System.Drawing.Point(975, 129);
		this.grpSystems.Name = "grpSystems";
		this.grpSystems.Size = new System.Drawing.Size(353, 134);
		this.grpSystems.TabIndex = 62;
		this.grpSystems.TabStop = false;
		this.grpSystems.Text = "System";
		this.button4.BackColor = System.Drawing.Color.Gray;
		this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button4.ForeColor = System.Drawing.Color.Black;
		this.button4.Location = new System.Drawing.Point(6, 17);
		this.button4.Name = "button4";
		this.button4.Size = new System.Drawing.Size(158, 31);
		this.button4.TabIndex = 3;
		this.button4.Text = "BATTERY + ENGINE START";
		this.button4.UseVisualStyleBackColor = false;
		this.button4.Click += new System.EventHandler(button4_Click);
		this.button3.BackColor = System.Drawing.Color.Gray;
		this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button3.ForeColor = System.Drawing.Color.Black;
		this.button3.Location = new System.Drawing.Point(6, 53);
		this.button3.Name = "button3";
		this.button3.Size = new System.Drawing.Size(158, 31);
		this.button3.TabIndex = 2;
		this.button3.Text = "BATTERY + ENGINGE STOP";
		this.button3.UseVisualStyleBackColor = false;
		this.button3.Click += new System.EventHandler(button3_Click);
		this.btnExpandTabControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnExpandTabControl.Image = (System.Drawing.Image)resources.GetObject("btnExpandTabControl.Image");
		this.btnExpandTabControl.Location = new System.Drawing.Point(1335, 119);
		this.btnExpandTabControl.Name = "btnExpandTabControl";
		this.btnExpandTabControl.Size = new System.Drawing.Size(21, 13);
		this.btnExpandTabControl.TabIndex = 59;
		this.btnExpandTabControl.UseVisualStyleBackColor = true;
		this.btnExpandTabControl.Click += new System.EventHandler(btnExpandTabControl_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(1357, 268);
		base.Controls.Add(this.grpOther);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.grpSystems);
		base.Controls.Add(this.grpPitch);
		base.Controls.Add(this.grpHeading);
		base.Controls.Add(this.grpThrottle);
		base.Controls.Add(this.grpAltitude);
		base.Controls.Add(this.grpSpeed);
		base.Controls.Add(this.btnExpandTabControl);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.grpGMeter);
		base.Controls.Add(this.grpApMock);
		this.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "FlightPanelForm";
		this.Text = "Flight Panel";
		base.Load += new System.EventHandler(FlightControls_Load);
		this.grpApMock.ResumeLayout(false);
		this.pnlAltBig.ResumeLayout(false);
		this.grpAltitude.ResumeLayout(false);
		this.grpAltitude.PerformLayout();
		this.grpSpeed.ResumeLayout(false);
		this.grpSpeed.PerformLayout();
		this.grpGMeter.ResumeLayout(false);
		this.grpGMeter.PerformLayout();
		this.grpThrottle.ResumeLayout(false);
		this.grpThrottle.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.throtTrack).EndInit();
		this.grpOther.ResumeLayout(false);
		this.grpOther.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.trackBar_Elevator).EndInit();
		((System.ComponentModel.ISupportInitialize)this.trackBar_Ailerons).EndInit();
		((System.ComponentModel.ISupportInitialize)this.trackBar_Rudder).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.grpSystems.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
