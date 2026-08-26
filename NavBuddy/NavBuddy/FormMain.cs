using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.FlightSimulator.SimConnect;
using NavBuddy.BuddyWorld;
using NavBuddy.FormsAndControls;
using NavBuddy.FSE;
using NavBuddy.Positionstack;
using NavBuddy.Properties;

namespace NavBuddy;

public class FormMain : Form
{
	public PanelTracker plnForceTracker = new PanelTracker();

	public FlightPanelForm frmFlightPanelForm = new FlightPanelForm();

	public FormCompass frmCompass = new FormCompass();

	public FormHeliHelp frmHeliHelp = new FormHeliHelp();

	private double MapZoom = 100.0;

	private TimeSpan partial = new TimeSpan(0L);

	private DateTime startTime;

	private DoubleBufferedPanel panelGraphicLog = new DoubleBufferedPanel();

	private int graphicLogX = 0;

	private Image graphicLogImage = null;

	private SimulatorConnectionManager.Struct2 previousLogSimulatorData;

	private IContainer components = null;

	public Label lblPlanDescription;

	public DataGridView dataGridView1;

	public GroupBox groupBox1;

	public Label labCurrentTime;

	public TabControl tabControl1;

	public TabPage tabNavLog;

	public TabPage tabFuelManagement;

	public Panel MapPanel;

	public TabPage tabLog;

	public TextBox txtCommLog;

	public Timer TimerSlow;

	public Label label3;

	public Label labElapsedTime;

	public Label label2;

	public Label labRemainigTime;

	public Label labArrivalTime;

	public Label label6;

	public Label label5;

	public Label labArrivalTimeReal;

	public Label label8;

	public Label labRemainingMiles;

	public Label labElapsedMiles;

	public GroupBox groupBox2;

	public Label labNextWaypointDescription;

	public Label labRemainingMilesWP;

	public Label labRemainigTimeWP;

	public Label label10;

	public Label labArrivalTimeWP;

	public Label label9;

	public Label labCurrentAltitude;

	public Label labCurrentGS;

	public Label label7;

	public Label label12;

	public Label label15;

	public Label labSlopeGoal;

	public Label label13;

	public Label labFPMGoal;

	public Label label11;

	public Label labAltitudeGoal;

	public GroupBox groupBox3;

	public TabPage tabInfo;

	public Label label14;

	public ImageList imageList16;

	public Timer TimerQuick;

	public ImageList imageList40;

	public ImageList imageList20;

	public Button btnExpandTabControl;

	public ImageList imageList7X7;

	public MenuStrip menuStrip1;

	public ToolStripMenuItem flightPlanToolStripMenuItem;

	public ToolStripMenuItem loadToolStripMenuItem;

	public ToolStripMenuItem reloadToolStripMenuItem;

	public ToolStripMenuItem resetFlightToolStripMenuItem;

	public ToolStripMenuItem simulatorToolStripMenuItem;

	public ToolStripMenuItem connectToolStripMenuItem;

	public ToolStripMenuItem infoToolStripMenuItem;

	public ToolStripMenuItem showLogToolStripMenuItem;

	public Label label22;

	public Label label21;

	public Label label20;

	public Label labCurrentPOS;

	public Label label24;

	public Button btnPositionGoogle;

	public TabPage tabForceTrack;

	public GroupBox groupBox5;

	public Button btnMapZoomDec;

	public Button btnMapZoomInc;

	public Label labAirplanetitle;

	public Label label26;

	public Label labFuelQuantity;

	public Label label25;

	public Label labEstimatedResidualFlightTime;

	public Label label29;

	public Label labFuelFlow;

	public Label label30;

	private ToolStripMenuItem exportForGoogleEarthToolStripMenuItem;

	private ToolStripMenuItem flightControlsToolStripMenuItem;

	public PictureBox picBoxConnSpy;

	private ContextMenuStrip AssignmentMenuStrip;

	private ToolStripMenuItem MenuItemTakeAssignment;

	private ContextMenuStrip PayloadMenuStrip;

	private ToolStripMenuItem loadIntoAirplaneToolStripMenuItem;

	public Label label34;

	public Label labFuelWeight;

	private ToolStripMenuItem unloadToolStripMenuItem;

	private ToolStripMenuItem deliverToolStripMenuItem;

	private ToolStripMenuItem MenuItemDismissAssignment;

	private DataGridViewTextBoxColumn Id;

	private DataGridViewTextBoxColumn googleMapLink;

	private DataGridViewTextBoxColumn Type;

	private DataGridViewTextBoxColumn Altitude;

	private DataGridViewTextBoxColumn IAS;

	private DataGridViewTextBoxColumn avgTAS;

	private DataGridViewTextBoxColumn Dist_nm;

	private DataGridViewTextBoxColumn Dist_sum_nm;

	private DataGridViewTextBoxColumn FPM;

	private DataGridViewTextBoxColumn HDG_deg;

	private DataGridViewTextBoxColumn time;

	private DataGridViewTextBoxColumn time_sum;

	private DataGridViewTextBoxColumn arrival_time_expected;

	private DataGridViewTextBoxColumn arrival_time_actual;

	private DataGridViewTextBoxColumn notes;

	private ToolStripMenuItem PayLoadcleanupToolStripMenuItem;

	public TabPage tabPathTrack;

	public NumericUpDown nupFligthTrackRecordInterval;

	public Label label41;

	public Button btnFligthTrackRecord;

	public Button btnExportFligthTrackRecord;

	public Button btnResetFligthTrackRecord;

	public NumericUpDown nupFligthTrackRecordDrawingSamples;

	public Label label42;

	private ContextMenuStrip AircraftMenuStrip;

	private ToolStripMenuItem deleteToolStripMenuItem;

	public TabPage tabStandardManouver;

	public Label label4;

	public Label labCronometer;

	public Button btnResetCronometer;

	public Button btnStartStopCronometer;

	private Timer timerCronometer;

	public Label label43;

	public Label labDeltaHeading;

	public Label label46;

	public Label labHeadingTrue;

	public Label label45;

	public Label labHeadingMag;

	public Label labRefHeading270;

	public Label labRefHeading90;

	public Label labRefHeading180;

	private PictureBox pictureBox1;

	public GroupBox groupBox9;

	public Panel PanelTurnSpeed;

	public GroupBox groupBox11;

	public Label lblST_CurrentTurnRay;

	public Label lblST_ExpectedBanking;

	public Label lblST_CurrentGS;

	public Label label48;

	public Label lblST_CurrentIas;

	public Label label47;

	private PictureBox pictureBox2;

	public GroupBox groupBox10;

	internal Label labRefHeading;

	public Label labVnavDescription;

	public Label label49;

	public GroupBox groupBox7;

	private TabPage tabPlanning;

	public Label label51;

	private ToolStripMenuItem completeCurrentLegToolStripMenuItem;

	public GroupBox groupBox8;

	private Button btnTeleportToRunway;

	public Label label39;

	public NumericUpDown nudAddFeetTeleport;

	public Label labVnavReference;

	public Label label52;

	private ToolStripMenuItem dataToolStripMenuItem;

	private ToolStripMenuItem dataFolderToolStripMenuItem;

	private ToolStripMenuItem performanceToolStripMenuItem;

	private ToolStripMenuItem savePerformanceToolStripMenuItem;

	private ToolStripMenuItem loadPerformanceToolStripMenuItem;

	private TabPage tabParameters;

	public Button DepartureGoogle;

	public Label label27;

	public TextBox txtDepartureIcao;

	public Label label28;

	public TextBox txtDestinationIcao;

	public Button DestinationGoogle;

	public Label label32;

	public Label label31;

	private Button btnLoadFseAirportAsDestination;

	private Button btnSearchRunwayArrival;

	private Button btnSearchRunwayDeparture;

	private Button btnCreateFlightPlan;

	public Label label33;

	public Label label55;

	public NumericUpDown nudPlannedCruiseAltitude;

	public Button LandingRunwayGoogle;

	public Button TakeOffRunwayGoogle;

	public GroupBox grpRunways;

	public Label lblLandingRunway;

	public Label label60;

	public Label lblTakeOffRunway;

	public Label label58;

	public Label labPlannedApproachSlope;

	public NumericUpDown nudPlannedApproachAltitude;

	public Label label57;

	public NumericUpDown nudPlannedApproachDistance;

	private Button btnLoadFseAirportAsDeparture;

	private Button btnSearchTakeOffRunway;

	private Button btnSearchLandingRunway;

	public Label label59;

	public Label labArrivalRunway;

	public Label labDepartureRunway;

	public Label labDepartureDescription;

	public Label labDestinationDescription;

	public Label labDestinationElevation;

	public Label labDepartureElevation;

	private TextBox txtDestinationSessagesimal;

	private TextBox txtDepartureSessagesimal;

	private Button btnLoadOurAirportAsDeparture;

	private Button btnLoadOurAirportAsDestination;

	public Label label61;

	public Label labEstimatedResidualFlightMiles;

	private GroupBox groupBox4;

	public Label label65;

	public Label label64;

	public Label label50;

	public Label labPlanningDistance;

	public Label label69;

	public Label label68;

	private ToolStripMenuItem youtubeChannelToolStripMenuItem;

	private ToolStripMenuItem discordChatToolStripMenuItem;

	public TabPage tabMisc;

	public GroupBox grpTeleport;

	public Label label74;

	public Label label75;

	public NumericUpDown nudTeleportAltitude;

	private Button btnTeleportToCustomLocation;

	public Label label73;

	private TextBox txtTeleportCoordinates;

	public Label label77;

	public Label label76;

	public NumericUpDown nudTeleportHeading;

	public Label label78;

	public Label labPlannedApproachDescentRate;

	public Label label80;

	public Label label79;

	public GroupBox grpPerformance;

	public Label label56;

	public NumericUpDown nudReverseThrust;

	public Label label53;

	public Label label63;

	public NumericUpDown nudMaxBankAngle;

	public Label label70;

	private Panel panel1;

	public Label label85;

	public NumericUpDown nudElevatorDamper;

	public Label label71;

	public NumericUpDown nudElevatorEffect;

	public Label label72;

	public NumericUpDown nudAileronDamper;

	public Label label66;

	public NumericUpDown nudAileronEffect;

	public Label label67;

	public NumericUpDown nudRudderEffect;

	public Label label62;

	public NumericUpDown nudFlapsTakeOffIas;

	public Label label54;

	public NumericUpDown nudFlapsTakeOffPerc;

	public NumericUpDown nudLandingGearDownAGL;

	public NumericUpDown nudLandingGearUpAGL;

	public Label label35;

	public NumericUpDown nudTakeOffCompletedAGL;

	public Label label38;

	public NumericUpDown nudDescFPM;

	public Label label44;

	public NumericUpDown nudRunwayEntAGL;

	public Label label36;

	public NumericUpDown nudClimbFPM;

	public Label label19;

	public NumericUpDown nudDescIas;

	public Label label18;

	public NumericUpDown nudClimbIas;

	public Label label17;

	public Label label16;

	public Label label23;

	public NumericUpDown nudCruiseIas;

	public Label label1;

	public NumericUpDown nudSafeIas;

	public Label label37;

	public NumericUpDown nudLandingIas;

	public NumericUpDown nudVnavHoldAlt;

	public RadioButton radVnavHoldAGL;

	public RadioButton radVnavStandard;

	private Button btnChaseLocation;

	public NumericUpDown nudFlapsLandingPerc;

	public Label label89;

	public Label label88;

	public NumericUpDown nudFlapsLandingIas;

	public Label label87;

	public Label label86;

	public NumericUpDown nudThrottleDamper;

	public Label label90;

	public NumericUpDown nudThrottleEffect;

	public Label label91;

	public NumericUpDown nudLandFPM;

	public TabPage tabGraphicLog;

	public GroupBox grpGraphicLogConfig;

	public GroupBox grpGraphicLog;

	private FlowLayoutPanel flpGraphicLogConfig;

	public GroupBox groupBox12;

	public Label labAirplaneDescription;

	private Button btnAirplaneCheck;

	private TabPage tabBuddyWorld;

	private Button btnGetPlaneQuotation;

	private Button btnBuyQuotedPlane;

	public Label lblAirplaneQuotation;

	protected GroupBox groupBox14;

	private ListBox listAirplanes;

	protected GroupBox groupBox13;

	private Button btnSellAirplane;

	public Label lblSelectedAirplane;

	protected GroupBox groupBox15;

	public Label labCash;

	private Button btnRefuel;

	public NumericUpDown nupRefuel;

	private TextBox txtIcaoNewPosition;

	public Label label83;

	private Button btnTravelToICAO;

	public Button btnPilotPosition;

	protected GroupBox groupBox16;

	public Label labFlightStatus;

	private Button btnstartFlight;

	private Button btnTravelToAirplane;

	public Label label92;

	public Label label95;

	public Label label94;

	public Label labPilotPositionDescription;

	public Label label93;

	public Label labSelectedAirplaneFuel;

	private Button btnEndflight;

	public Label labBuddyWorldFlightCompactStatus;

	public Label labAirplaneCompleteFlights;

	public Label label97;

	public Label lblAirplaneFlightHours;

	public Label label40;

	public Label labAirplaneMileage;

	public Label label98;

	public Label labAirplaneBodyStatus;

	public Label label99;

	private Button btnAirplaneBodyMaitenanceRepair;

	private Panel panelWorldAirplanes;

	public Label labAirplaneMarketPrice;

	public Label label96;

	private TabPage tabActivities;

	protected GroupBox groupBox17;

	private Panel ActivitySearchPanel;

	protected GroupBox grpPayLoad;

	private Panel panelPayload;

	private Button btnAbortFlight;

	private Button btnGenerateActivitiesAtHome;

	private ToolStripMenuItem compassToolStripMenuItem;

	private TabPage tabFinance;

	protected GroupBox groupBox18;

	private ListBox lstTransactions;

	protected GroupBox groupBox19;

	private Button btnAskNewLoan;

	public NumericUpDown nudLoan;

	public Label label100;

	public Label labFinanceLoan;

	private Button btnRepayLoan;

	public Label label101;

	public Label labFinanceCash;

	public Label label103;

	public NumericUpDown nudPilotWeight;

	public Label labFlightRequiredPayload;

	public Label label105;

	private Button btnAirplaneEngineMaitenanceRepair;

	public Label labAirplaneEngineStatus;

	public Label label107;

	private Button btnWaypointSelectedAirplane;

	protected GroupBox groupBoxAssignedActivities;

	private Panel ActivityAssignedPanel;

	public Label labNextAvailableMoment;

	public Label label102;

	private Button btnRentQuotedPlane;

	public Label label81;

	private TextBox txtPositionstackKey;

	private Button btnSetPositionstackKey;

	public Label label82;

	private Button btnInfo;

	public Label label84;

	public Label labReputation;

	public Label label106;

	public Label labFinanceMaxLoan;

	private TabPage tabGoodsTrade;

	private Button btnAdvertisement;

	private TextBox txtHomeBase;

	private Button btnHomeBase;

	public Button button1;

	public Label label104;

	private Button btnGenerateActivitiesAtUserPos;

	private ToolStripMenuItem helicopterHelpToolStripMenuItem;

	public FormMain()
	{
		InitializeComponent();
		FormLayoutManager.ManageLayout(this);
		tabForceTrack.Controls.Add(plnForceTracker);
		plnForceTracker.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		plnForceTracker.BackColor = Color.Black;
		plnForceTracker.Location = new Point(5, 5);
		plnForceTracker.Name = "plnForceTracker";
		plnForceTracker.Size = tabForceTrack.Size - new Size(10, 10);
		menuStrip1.Renderer = new MyToolStripRenderer();
		SimulatorInformationProcessing.F = this;
		grpGraphicLog.Controls.Add(panelGraphicLog);
		panelGraphicLog.Location = new Point(5, 12);
		panelGraphicLog.Name = "panelGraphicLog";
		panelGraphicLog.Size = new Size(grpGraphicLog.Width - 10, grpGraphicLog.Height - 20);
		panelGraphicLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		panelGraphicLog.BackColor = Color.Black;
		panelGraphicLog.TabIndex = 0;
		panelGraphicLog.Paint += panelGraphicLog_Paint;
		SetupLogControls();
		Standard_Control_Setup();
		UpdatePlannedGLideSlope();
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		CheckDataFolder();
		OurAirportsManager.prepareCustomFiles();
		SimulatorConnectionManager.txtLog = txtCommLog;
		SimulatorConnectionManager.myForm1 = this;
		LoadEverything();
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
		Text = "NavBuddy " + versionInfo.FileVersion + " BETA";
		BuddyWorldManager.NotifyRefresh += OnRefreshBuddyWorldManager;
		BuddyWorldManager.NotifyRefreshActivities += OnRefreshBuddyWorldManagerActivities;
	}

	public void Standard_Control_Setup()
	{
		foreach (Control control in base.Controls)
		{
			Standard_Control_Setup(control);
		}
	}

	public void Standard_Control_Setup(Control C)
	{
		if (C.GetType().Name == typeof(DataGridView).Name)
		{
			DataGridView dataGridView = (DataGridView)C;
			dataGridView.CellContentClick += dataGridView_Standard_CellContentClick;
			dataGridView.CellFormatting += datagridview_Standard_CellFormatting;
			dataGridView.CellPainting += dataGridView_Standard_CellPainting;
			dataGridView.CellValidated += dataGridView_Standard_CellValidated;
			dataGridView.CellMouseDown += dgvRightSelect_Standard_CellMouseDown;
			dataGridView.DataError += dataGridView_Standard_DataError;
			dataGridView.SelectionChanged += dataGridView_Standard_SelectionChanged;
			dataGridView.AutoGenerateColumns = false;
			foreach (DataGridViewColumn column in dataGridView.Columns)
			{
				if (column.DataPropertyName == null || column.DataPropertyName == "")
				{
					column.DataPropertyName = column.Name.Replace("_", "");
				}
			}
		}
		if (!C.HasChildren)
		{
			return;
		}
		foreach (Control control in C.Controls)
		{
			Standard_Control_Setup(control);
		}
	}

	private void dataGridView_Standard_SelectionChanged(object sender, EventArgs e)
	{
		DataGridView dataGridView = (DataGridView)sender;
	}

	private void dataGridView_Standard_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
		SimulatorConnectionManager.WriteLogNL(((DataGridView)sender).Name + " data error ");
	}

	private void dataGridView_Standard_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		DataGridView dataGridView = (DataGridView)sender;
		switch (dataGridView.Columns[e.ColumnIndex].DataPropertyName)
		{
		case "googleMapLink":
		case "googleMapLinkFrom":
		case "googleMapLinkTo":
			Process.Start((string)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
			break;
		}
	}

	private void datagridview_Standard_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
	{
		DataGridView dataGridView = (DataGridView)sender;
		if (e.Value == null)
		{
			return;
		}
		switch (dataGridView.Columns[e.ColumnIndex].DataPropertyName)
		{
		case "Dist_nm":
		case "Dist_sum_nm":
			e.Value = ((double)e.Value).ToString("F1");
			return;
		case "arrival_time_expected":
			if (((ATCWaypoint)dataGridView.Rows[e.RowIndex].DataBoundItem).arrival_time_is_actual())
			{
				e.Value = "";
			}
			else
			{
				e.Value = StandardFormatter.FormatDateTime((DateTime)e.Value);
			}
			return;
		case "CompetenceDate":
			if (((DateTime)e.Value).Date != DateTime.Now.Date)
			{
				e.CellStyle.ForeColor = Color.Red;
				e.CellStyle.SelectionForeColor = Color.Red;
			}
			e.Value = ((DateTime)e.Value).ToString("dd/MM/yyyy");
			return;
		}
		string name = e.Value.GetType().Name;
		if (!(name == "DateTime"))
		{
			if (name == "TimeSpan")
			{
				if (e.Value.ToString() == "00:00:00")
				{
					e.Value = "";
				}
				else
				{
					e.Value = StandardFormatter.FormatTimeSpan((TimeSpan)e.Value);
				}
			}
		}
		else if (dataGridView.Name == dataGridView1.Name)
		{
			if (e.Value.ToString() == "01/01/0001 00:00:00")
			{
				e.Value = "";
			}
			else
			{
				e.Value = StandardFormatter.FormatDateTime((DateTime)e.Value);
			}
		}
		else
		{
			e.Value = ((DateTime)e.Value).ToString("dd/MM/yyyy");
		}
	}

	private void dataGridView_Standard_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
	{
		DataGridView dataGridView = (DataGridView)sender;
		if (e.RowIndex > -1)
		{
			switch (dataGridView.Columns[e.ColumnIndex].DataPropertyName)
			{
			case "googleMapLink":
			case "googleMapLinkFrom":
			case "googleMapLinkTo":
				e.Graphics.FillRectangle(new SolidBrush(Color.Black), e.CellBounds);
				e.Graphics.DrawLine(new Pen(Color.White), e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
				e.Graphics.DrawLine(new Pen(Color.White), e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
				e.Graphics.DrawImage(imageList16.Images["googlelink"], e.CellBounds.Location + new Size(2, 2));
				e.Handled = true;
				break;
			default:
				e.Handled = false;
				break;
			}
		}
		else
		{
			e.Handled = false;
		}
	}

	private void dataGridView_Standard_CellValidated(object sender, DataGridViewCellEventArgs e)
	{
		((DataGridView)sender).Refresh();
	}

	private void dgvRightSelect_Standard_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == MouseButtons.Right)
		{
			DataGridViewCell dataGridViewCell = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
			if (!dataGridViewCell.Selected)
			{
				dataGridViewCell.DataGridView.ClearSelection();
				dataGridViewCell.DataGridView.CurrentCell = dataGridViewCell;
				dataGridViewCell.Selected = true;
			}
		}
	}

	private void CheckDataFolder()
	{
		if (!Directory.Exists(DataManager.DataFolder()))
		{
			Directory.CreateDirectory(DataManager.DataFolder());
		}
		string[] files = Directory.GetFiles(DataManager.DataFolder(), "*.prf");
		if (files == null || files.Length == 0)
		{
			MessageBox.Show("Looks like you have no prf files in your data folder. To use NavBuddy AI pilot you need to download some prf files or make your owns. Please refer to Navbuddy discord channel for further informations");
			Process.Start("explorer.exe", DataManager.DataFolder());
		}
	}

	private void TimerSlow_Tick(object sender, EventArgs e)
	{
		if (SimulatorConnectionManager.MySim != null)
		{
			SimulatorConnectionManager.Sim_RequestDataToSimConnect(SimulatorConnectionManager.DATA_REQUESTS1.REQUEST1, SimulatorConnectionManager.DEFINITIONS1.STRUCT1);
		}
		BuddyWorldManager.WorldRun((double)TimerSlow.Interval / 1000.0);
		if (tabControl1.SelectedTab.Name == tabBuddyWorld.Name)
		{
			RefreshFlightStatus();
		}
	}

	private void TimerQuick_Tick(object sender, EventArgs e)
	{
		if (SimulatorConnectionManager.MySim != null)
		{
			SimulatorConnectionManager.Sim_RequestDataToSimConnect(SimulatorConnectionManager.DATA_REQUESTS1.REQUEST2, SimulatorConnectionManager.DEFINITIONS1.STRUCT2);
		}
	}

	private void exitToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void infoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		tabControl1.SelectTab(3);
	}

	public void btnVNAV_Click(object sender, EventArgs e)
	{
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

	private void btnToggleStatusWithSim(object sender, EventArgs e)
	{
	}

	private void Form1_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (SimulatorConnectionManager.MySim != null)
		{
			MessageBox.Show("You need to close connection with simulator before closing");
			e.Cancel = true;
		}
		else
		{
			SaveEverything();
			Application.Exit();
		}
	}

	private void SaveEverything()
	{
		AircraftParameters aircraftParameters = new AircraftParameters();
		FromControlsToData(aircraftParameters);
		DataManager.SaveObjectIntoFile(aircraftParameters);
	}

	private void LoadEverything()
	{
		AircraftParameters aP = ((AircraftParameters)DataManager.LoadObjectFromFile("AircraftParameters", typeof(AircraftParameters))) ?? new AircraftParameters();
		FromDataToControls(aP);
	}

	private void FromDataToControls(AircraftParameters aP)
	{
		foreach (Control control in grpPerformance.Controls)
		{
			if (control.GetType() == typeof(NumericUpDown))
			{
				PropertyInfo property = aP.GetType().GetProperty(control.Name.Replace("nud", ""));
				if (property != null)
				{
					((NumericUpDown)control).Value = (decimal)(double)property.GetValue(aP);
				}
			}
		}
	}

	private void FromControlsToData(AircraftParameters aP)
	{
		foreach (Control control in grpPerformance.Controls)
		{
			if (control.GetType() == typeof(NumericUpDown))
			{
				PropertyInfo property = aP.GetType().GetProperty(control.Name.Replace("nud", ""));
				if (property != null)
				{
					property.SetValue(aP, (double)((NumericUpDown)control).Value);
				}
			}
		}
	}

	private void btnExpandTabControl_Click(object sender, EventArgs e)
	{
		if (base.Size.Height > tabControl1.Location.Y + 50)
		{
			btnExpandTabControl.Tag = base.Size.Height;
			base.Size = new Size(base.Size.Width, tabControl1.Location.Y + 38);
			btnExpandTabControl.Image = imageList7X7.Images["minidown"];
		}
		else
		{
			base.Size = new Size(base.Size.Width, (int)btnExpandTabControl.Tag);
			btnExpandTabControl.Image = imageList7X7.Images["miniup"];
		}
	}

	private void loadToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SelectAndLoadFlightPlan();
	}

	private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (((ToolStripMenuItem)sender).Tag == null)
		{
			MessageBox.Show("No flight plan previously loaded");
		}
		else
		{
			LoadFlightPlan((string)((ToolStripMenuItem)sender).Tag);
		}
	}

	private void resetFlightToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Reset_flight();
	}

	private void connectToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
	{
		ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
		if (toolStripMenuItem.Checked)
		{
			SimulatorConnectionManager.Sim_ConnectToSimulator(Text, base.Handle);
			toolStripMenuItem.Text = "Disconnect simulator";
			picBoxConnSpy.Image = imageList7X7.Images["Spy7x7on"];
		}
		else if (!SimulatorInformationProcessing.currentAircraft.ActivationState || MessageBox.Show("Airplane is still running. Position won't be recorded. Disconnect in any case?", "AIRPLANE RUNNING!", MessageBoxButtons.YesNo) != DialogResult.No)
		{
			SimulatorConnectionManager.Sim_DisconnectFromSimulator();
			toolStripMenuItem.Text = "Connect simulator";
			picBoxConnSpy.Image = imageList7X7.Images["Spy7x7off"];
			SaveEverything();
		}
	}

	private void showLogToolStripMenuItem_Click(object sender, EventArgs e)
	{
		tabControl1.SelectedTab = tabLog;
	}

	private void infoToolStripMenuItem_Click_1(object sender, EventArgs e)
	{
		tabControl1.SelectedTab = tabInfo;
	}

	private void pnlAltSmall_Paint(object sender, PaintEventArgs e)
	{
	}

	private void simulatorToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void tabControl1_Selected(object sender, TabControlEventArgs e)
	{
		IntelligentRefresh();
	}

	public void IntelligentRefresh()
	{
		if (tabControl1.SelectedTab.Name == tabForceTrack.Name)
		{
			plnForceTracker.Clear();
		}
		else if (tabControl1.SelectedTab.Name == tabFuelManagement.Name)
		{
			RefreshFuelManagementTab();
		}
		else if (tabControl1.SelectedTab.Name == tabBuddyWorld.Name)
		{
			RefreshBuddyWorldTab();
		}
		else if (tabControl1.SelectedTab.Name == tabActivities.Name)
		{
			RefreshActivityTab();
		}
		else if (tabControl1.SelectedTab.Name == tabFinance.Name)
		{
			RefreshFinanceTab();
		}
	}

	public void RefreshFuelManagementTab()
	{
		labAirplanetitle.Text = SimulatorInformationProcessing.currentAircraft.title;
		labFuelQuantity.Text = SimulatorInformationProcessing.currentAircraft.fuelquantity.ToString("F2");
		labFuelWeight.Text = SimulatorInformationProcessing.lastBigInfoSimulatorData.FUEL_TOTAL_QUANTITY_WEIGHT.ToString("F1");
		labFuelFlow.Text = (SimulatorInformationProcessing.currentAircraft.last10SecondsFuelflowGalPerSecond * 3600.0).ToString("F2");
		if (SimulatorInformationProcessing.currentAircraft.last10SecondsFuelflowGalPerSecond > 0.0)
		{
			TimeSpan t = new TimeSpan(0, 0, (int)(SimulatorInformationProcessing.currentAircraft.fuelquantity / SimulatorInformationProcessing.currentAircraft.last10SecondsFuelflowGalPerSecond));
			if (t.TotalDays < 1.0)
			{
				labEstimatedResidualFlightTime.Text = StandardFormatter.FormatTimeSpanWithSeconds(t);
			}
			else
			{
				labEstimatedResidualFlightTime.Text = ">1 day";
			}
			double num = t.TotalHours * SimulatorInformationProcessing.lastBigInfoSimulatorData.groundvelocity;
			labEstimatedResidualFlightMiles.Text = num.ToString("F1") + " nm";
		}
		else
		{
			labEstimatedResidualFlightTime.Text = "--";
		}
	}

	private void btnPositionGoogle_Click(object sender, EventArgs e)
	{
		Process.Start(SimulatorInformationProcessing.currentAircraft.position.googleMapLink);
	}

	private void exportForGoogleEarthToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (FlightPlan.ATCWaypoints.Count > 0)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "kml files (*.kml)|*.kml";
			saveFileDialog.FilterIndex = 2;
			saveFileDialog.RestoreDirectory = true;
			saveFileDialog.FileName = FlightPlan.DepartureID + "-" + FlightPlan.DestinationID;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				FlightPlan.ExportToGoogleEarth(saveFileDialog.FileName);
			}
		}
	}

	private void flightControlsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (frmFlightPanelForm.IsDisposed)
		{
			frmFlightPanelForm = new FlightPanelForm();
		}
		frmFlightPanelForm.Show();
		frmFlightPanelForm.Location = new Point(Screen.FromControl(this).Bounds.Left, 0);
		frmFlightPanelForm.TopMost = true;
		frmFlightPanelForm.TopMost = false;
		frmFlightPanelForm.Focus();
	}

	private void btnFligthTrackRecord_Click(object sender, EventArgs e)
	{
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

	private void btnMapZoomInc_Click(object sender, EventArgs e)
	{
		MapZoom *= 1.5;
		MapPanel.Refresh();
	}

	private void btnMapZoomDec_Click(object sender, EventArgs e)
	{
		MapZoom /= 1.5;
		MapPanel.Refresh();
	}

	private void MapPanel_Paint(object sender, PaintEventArgs e)
	{
		WayPoint position = SimulatorInformationProcessing.currentAircraft.position;
		if (position != null)
		{
			foreach (ATCWaypoint aTCWaypoint in FlightPlan.ATCWaypoints)
			{
				if (aTCWaypoint.following != null)
				{
					int num = (int)(MapZoom * (aTCWaypoint.longitude - position.longitude) + (double)(MapPanel.Width / 2));
					int num2 = (int)(MapZoom * (position.latitude - aTCWaypoint.latitude) + (double)(MapPanel.Height / 2));
					Point point = new Point(num, num2);
					int num3 = (int)(MapZoom * (aTCWaypoint.following.longitude - position.longitude) + (double)(MapPanel.Width / 2));
					int num4 = (int)(MapZoom * (position.latitude - aTCWaypoint.following.latitude) + (double)(MapPanel.Height / 2));
					Point point2 = new Point(num3, num4);
					Point point3 = new Point((num + num3) / 2, (num2 + num4) / 2);
					e.Graphics.DrawLine(new Pen(Color.Blue), point, point2);
					e.Graphics.DrawString(aTCWaypoint.Id, Font, new SolidBrush(Color.Red), point);
					e.Graphics.DrawString(aTCWaypoint.following.Id, Font, new SolidBrush(Color.Red), point2);
					e.Graphics.DrawString(aTCWaypoint.following.Dist_nm.ToString("F0") + "nm " + aTCWaypoint.following.time.ToString(), Font, new SolidBrush(Color.Blue), point3);
				}
			}
		}
		e.Graphics.DrawEllipse(new Pen(Color.Green), new Rectangle(MapPanel.Width / 2 - 1, MapPanel.Height / 2 - 1, 3, 3));
		Point point4 = new Point(MapPanel.Width / 2, MapPanel.Height / 2);
		int num5 = SimulatorInformationProcessing.FligthTrackRecord.Count - 1;
		while (num5 >= 1 && (decimal)(SimulatorInformationProcessing.FligthTrackRecord.Count - 1 - num5) <= nupFligthTrackRecordDrawingSamples.Value)
		{
			WayPoint wayPoint = SimulatorInformationProcessing.FligthTrackRecord[num5];
			WayPoint wayPoint2 = SimulatorInformationProcessing.FligthTrackRecord[num5 - 1];
			Point pt = point4 + new Size((int)(MapZoom * (wayPoint.longitude - SimulatorInformationProcessing.currentAircraft.position.longitude)), (int)(MapZoom * -1.0 * (wayPoint.latitude - SimulatorInformationProcessing.currentAircraft.position.latitude)));
			Point pt2 = point4 + new Size((int)(MapZoom * (wayPoint2.longitude - SimulatorInformationProcessing.currentAircraft.position.longitude)), (int)(MapZoom * -1.0 * (wayPoint2.latitude - SimulatorInformationProcessing.currentAircraft.position.latitude)));
			e.Graphics.DrawLine(new Pen(Color.Green), pt, pt2);
			num5--;
		}
	}

	private void btnResetFligthTrackRecord_Click(object sender, EventArgs e)
	{
		SimulatorInformationProcessing.FligthTrackRecord.Clear();
	}

	private void btnExportFligthTrackRecord_Click(object sender, EventArgs e)
	{
		if (SimulatorInformationProcessing.FligthTrackRecord.Count <= 0)
		{
			return;
		}
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "kml files (*.kml)|*.kml";
		saveFileDialog.FilterIndex = 2;
		saveFileDialog.RestoreDirectory = true;
		saveFileDialog.FileName = "Flight " + DateTime.Now.ToShortDateString().Replace("/", "-");
		if (saveFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string text = GoogleEarthExporter.trackTemplate();
		StringBuilder stringBuilder = new StringBuilder();
		foreach (WayPoint item in SimulatorInformationProcessing.FligthTrackRecord)
		{
			stringBuilder.Append(item.googleEarthKmlString());
			stringBuilder.Append(" ");
		}
		string text2 = text.Replace("@COORDINATES@", stringBuilder.ToString().Trim());
		text2 = text2.Replace("@DOCUMENTNAME@", "Flight " + DateTime.Now.ToShortDateString().Replace("/", "-"));
		text2 = text2.Replace("@NAME@", "Flight " + DateTime.Now.ToShortDateString().Replace("/", "-"));
		File.WriteAllText(saveFileDialog.FileName, text2);
		SimulatorConnectionManager.WriteLogNL("Saved " + saveFileDialog.FileName);
	}

	private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		DataGridView dataGridView = (DataGridView)((ContextMenuStrip)deleteToolStripMenuItem.GetCurrentParent()).SourceControl;
		try
		{
			Aircraft aircraft = (Aircraft)dataGridView.SelectedRows[0].DataBoundItem;
			aircraft.DeleteFile();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void TransactionsMenuStrip_Opening(object sender, CancelEventArgs e)
	{
	}

	private void checkGoodsFileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string[] array = File.ReadAllLines("anagraphical resources\\goods_superlist_6.txt");
		StreamWriter streamWriter = new StreamWriter("anagraphical resources\\goods_superlist_7.txt");
		for (int i = 0; i < array.Count(); i++)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array2 = array[i].Split(' ');
			for (int j = 0; j < array2.Length; j++)
			{
				if (array2[j].Length == 5 && int.TryParse(array2[j].Substring(1, 4), out var result))
				{
					result = result;
				}
				else if (array2[j].Length != 1 || j != array2.Length - 1)
				{
					stringBuilder.Append(array2[j] + " ");
				}
			}
			streamWriter.WriteLine(stringBuilder.ToString().Trim());
		}
		streamWriter.Close();
		MessageBox.Show("ok");
	}

	private void btnStartStopCronometer_Click(object sender, EventArgs e)
	{
		timerCronometer.Enabled = !timerCronometer.Enabled;
		if (timerCronometer.Enabled)
		{
			startTime = DateTime.Now;
		}
		else
		{
			partial = startTime - DateTime.Now + partial;
		}
	}

	private void btnResetCronometer_Click(object sender, EventArgs e)
	{
		partial = new TimeSpan(0L);
		startTime = DateTime.Now;
		timerCronometer_Tick(null, null);
	}

	private void timerCronometer_Tick(object sender, EventArgs e)
	{
		TimeSpan timeSpan = startTime - DateTime.Now + partial;
		labCronometer.Text = $"{timeSpan:mm\\:ss\\:f}";
	}

	private void PanelTurnSpeed_Paint(object sender, PaintEventArgs e)
	{
		Rectangle clipRectangle = e.ClipRectangle;
		Brush brush = new SolidBrush(Color.DarkGreen);
		Brush brush2 = new SolidBrush(Color.Green);
		Pen pen = new Pen(Color.White);
		e.Graphics.FillRectangle(brush, clipRectangle);
		if (PanelTurnSpeed.Tag != null)
		{
			double num = (double)PanelTurnSpeed.Tag;
			float num2 = 3f;
			e.Graphics.ResetTransform();
			e.Graphics.TranslateTransform(clipRectangle.Width / 2, clipRectangle.Height / 2);
			float num3 = clipRectangle.Width / 2 - 2;
			float num4 = num3 - 10f;
			float num5 = num3 - 12f;
			e.Graphics.DrawLine(pen, new PointF(0f - num3, 0f), new PointF(0f - num4, 0f));
			e.Graphics.DrawLine(pen, new PointF(num4, 0f), new PointF(num3, 0f));
			e.Graphics.RotateTransform(3f * num2);
			e.Graphics.DrawLine(pen, new PointF(0f - num3, 0f), new PointF(0f - num4, 0f));
			e.Graphics.DrawLine(pen, new PointF(num4, 0f), new PointF(num3, 0f));
			e.Graphics.RotateTransform(-6f * num2);
			e.Graphics.DrawLine(pen, new PointF(0f - num3, 0f), new PointF(0f - num4, 0f));
			e.Graphics.DrawLine(pen, new PointF(num4, 0f), new PointF(num3, 0f));
			e.Graphics.ResetTransform();
			e.Graphics.TranslateTransform(clipRectangle.Width / 2, clipRectangle.Height / 2);
			e.Graphics.RotateTransform((float)num * num2);
			e.Graphics.DrawLine(pen, new PointF(0f - num5, 0f), new PointF(num5, 0f));
		}
	}

	private void btnLoadFseAirportAsDestination_Click(object sender, EventArgs e)
	{
		if (FseDataManager.fseAirports.Count == 0)
		{
			FseDataManager.ReloadFSEAirports("fse//icaodata.csv");
		}
		FSEAirport fSEAirport = FseDataManager.fseAirports.Where((FSEAirport A) => A.icao == txtDestinationIcao.Text).FirstOrDefault();
		if (fSEAirport == null)
		{
			MessageBox.Show("Airport not found");
		}
		else
		{
			txtDestinationIcao.Text = fSEAirport.icao;
			labDestinationDescription.Text = fSEAirport.name;
			DestinationGoogle.Tag = fSEAirport.wayPoint.googleMapLink;
			labDestinationElevation.Text = fSEAirport.wayPoint.Altitude.ToString("F0");
			txtDestinationSessagesimal.Text = fSEAirport.wayPoint.GetSessagesimalCoordinates();
			if (FlightPlan.ATCWaypoints.Count > 0)
			{
				double num = fSEAirport.wayPoint.DistanceFromMiles(FlightPlan.ATCWaypoints.Last());
				MessageBox.Show(fSEAirport.wayPoint.Id + " is " + num.ToString("F1") + " miles from current flight plan arrival");
			}
			FlightPlan.Destination = new ATCWaypoint(fSEAirport.lat, fSEAirport.lon, fSEAirport.icao, "AIRPORT", 0.0);
			labArrivalRunway.Text = "----";
			lblTakeOffRunway.Text = "----";
			FlightPlan.LandingRunway = null;
		}
		UpdateDepartureDestinationInfo();
	}

	private void UpdateDepartureDestinationInfo()
	{
		if (FlightPlan.Departure != null && FlightPlan.Destination != null)
		{
			labPlanningDistance.Text = FlightPlan.Destination.DistanceFromMiles(FlightPlan.Departure).ToString("F1") + " nm";
		}
		else
		{
			labPlanningDistance.Text = "---";
		}
	}

	private void completeCurrentLegToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SimulatorInformationProcessing.WayPointReached(SimulatorInformationProcessing.currentAircraft.SimZulu_Time);
	}

	private void btnTeleportToTakeoffRunway_Click(object sender, EventArgs e)
	{
		RunWay takeOffRunway = FlightPlan.TakeOffRunway;
		WayPoint runwayThreshold = takeOffRunway.runwayThreshold;
		SimulatorConnectionManager.Struct3 @struct = new SimulatorConnectionManager.Struct3
		{
			altitude = (double)runwayThreshold.Altitude + (double)nudAddFeetTeleport.Value,
			latitude = runwayThreshold.latitude,
			longitude = runwayThreshold.longitude,
			plane_heading_degree_true = Utility.DegToRad(takeOffRunway.BearingDegreeTrue())
		};
		SimulatorConnectionManager.Sim_TransmitDataToSimConnect(SimulatorConnectionManager.DEFINITIONS1.STRUCT3, @struct);
	}

	private void dataFolderToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Process.Start("explorer.exe", DataManager.DataFolder());
	}

	private void savePerformanceToolStripMenuItem_Click(object sender, EventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.InitialDirectory = DataManager.DataFolder();
		saveFileDialog.Filter = "performance files (*.prf)|*.prf";
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			AircraftParameters aircraftParameters = new AircraftParameters();
			FromControlsToData(aircraftParameters);
			DataManager.SaveObjectIntoFile(aircraftParameters, Path.GetFileNameWithoutExtension(saveFileDialog.FileName), ".prf");
			grpPerformance.Text = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
		}
	}

	private void loadPerformanceToolStripMenuItem_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.InitialDirectory = DataManager.DataFolder();
		openFileDialog.Filter = "data files (*.prf)|*.prf";
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			AircraftParameters aP = (AircraftParameters)DataManager.LoadObjectFromFile(Path.GetFileNameWithoutExtension(openFileDialog.FileName), typeof(AircraftParameters), ".prf");
			FromDataToControls(aP);
			grpPerformance.Text = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
		}
	}

	private void GoogleMapClick_Click(object sender, EventArgs e)
	{
		Process.Start((string)((Button)sender).Tag);
	}

	private void btnSearchRunwayArrival_Click(object sender, EventArgs e)
	{
		FormSelectRunway formSelectRunway = new FormSelectRunway(OurAirportsManager.GetRunways(txtDestinationIcao.Text), txtDestinationIcao.Text);
		formSelectRunway.ShowDialog();
		if (formSelectRunway.selectedRunway != null)
		{
			labArrivalRunway.Text = formSelectRunway.selectedRunway.id + "  (" + formSelectRunway.selectedRunway.runwayThreshold.Altitude.ToString("F0") + "ft)";
			FlightPlan.LandingRunway = formSelectRunway.selectedRunway;
		}
		UpdatePlannedGLideSlope();
	}

	private void btnSearchRunwayDeparture_Click(object sender, EventArgs e)
	{
		FormSelectRunway formSelectRunway = new FormSelectRunway(OurAirportsManager.GetRunways(txtDepartureIcao.Text), txtDepartureIcao.Text);
		formSelectRunway.ShowDialog();
		if (formSelectRunway.selectedRunway != null)
		{
			labDepartureRunway.Text = formSelectRunway.selectedRunway.id + "  (" + formSelectRunway.selectedRunway.runwayThreshold.Altitude.ToString("F0") + "ft)";
			FlightPlan.TakeOffRunway = formSelectRunway.selectedRunway;
		}
	}

	private void btnCreateFlightPlan_Click(object sender, EventArgs e)
	{
		if (FlightPlan.LandingRunway == null)
		{
			MessageBox.Show("Must specify a landing runway");
			return;
		}
		if (FlightPlan.TakeOffRunway == null)
		{
			MessageBox.Show("Must specify a takeoff runway");
			return;
		}
		FlightPlan.Title = txtDepartureIcao.Text + " - " + txtDestinationIcao.Text;
		FlightPlan.FPType = "AUTOMATIC";
		FlightPlan.CruisingAlt = (int)nudPlannedCruiseAltitude.Value;
		FlightPlan.DepartureID = txtDepartureIcao.Text;
		FlightPlan.DestinationID = txtDestinationIcao.Text;
		FlightPlan.DepartureLLA = "???";
		FlightPlan.DestinationLLA = "???";
		FlightPlan.Descr = "autogenerated";
		FlightPlan.DepartureName = labDepartureDescription.Text;
		FlightPlan.DestinationName = labDestinationDescription.Text;
		FlightPlan.DepartureTime = DateTime.Now;
		FlightPlan.ATCWaypoints.Clear();
		WayPoint departure = FlightPlan.Departure;
		WayPoint destination = FlightPlan.Destination;
		RunWay landingRunway = FlightPlan.LandingRunway;
		WayPoint runwayThreshold = landingRunway.runwayThreshold;
		double num = (double)nudPlannedApproachAltitude.Value;
		double num2 = (double)nudPlannedApproachDistance.Value;
		double degreeBearingNorth = Utility.NormalizeAngleDegree(landingRunway.BearingDegreeTrue() + 180.0);
		WayPoint wayPoint = runwayThreshold.Clone();
		wayPoint.Translate(degreeBearingNorth, num2 * 1852.0);
		ATCWaypoint aTCWaypoint = new ATCWaypoint(departure.latitude, departure.longitude, departure.Id, "AIRPORT", departure.Altitude);
		ATCWaypoint aTCWaypoint2 = new ATCWaypoint(runwayThreshold.latitude, runwayThreshold.longitude, runwayThreshold.Id, "RUNWAY", runwayThreshold.Altitude);
		ATCWaypoint aTCWaypoint3 = new ATCWaypoint(wayPoint.latitude, wayPoint.longitude, "FIX", "user", (double)runwayThreshold.Altitude + num + (double)nudRunwayEntAGL.Value);
		ATCWaypoint aTCWaypoint4 = new ATCWaypoint(destination.latitude, destination.longitude, destination.Id, "AIRPORT", destination.Altitude);
		FlightPlan.ATCWaypoints.Add(aTCWaypoint);
		FlightPlan.ATCWaypoints.Add(aTCWaypoint3);
		FlightPlan.ATCWaypoints.Add(aTCWaypoint2);
		FlightPlan.ATCWaypoints.Add(aTCWaypoint4);
		for (int i = 0; i < FlightPlan.ATCWaypoints.Count - 1; i++)
		{
			FlightPlan.ATCWaypoints[i].following = FlightPlan.ATCWaypoints[i + 1];
			FlightPlan.ATCWaypoints[i + 1].preceeding = FlightPlan.ATCWaypoints[i];
		}
		FlightPlan.Departure = aTCWaypoint;
		FlightPlan.Destination = aTCWaypoint4;
		FlightPlan.TakeOffRunway = FlightPlan.TakeOffRunway;
		FlightPlan.LandingRunway = FlightPlan.LandingRunway;
		FlightPlan.DefaultAltitudeAssignments();
		if (MessageBox.Show("Do you want to have TOC/TOD automatically calculated?", "TOC/TOD", MessageBoxButtons.YesNo) == DialogResult.Yes)
		{
			FlightPlan.CalculateTocAndTod((double)nudClimbIas.Value, (double)nudDescIas.Value, (double)nudClimbFPM.Value, (double)nudDescFPM.Value, aTCWaypoint3);
		}
		FlightPlan.DefaultSpeedAssignments((double)nudClimbIas.Value, (double)nudCruiseIas.Value, (double)nudDescIas.Value);
		aTCWaypoint2.IAS = (double)nudLandingIas.Value;
		FlightPlan.DepartureTime = DateTime.MinValue;
		ShowPlan();
	}

	private void nudPlannedApproachDistance_ValueChanged(object sender, EventArgs e)
	{
		UpdatePlannedGLideSlope();
	}

	private void nudPlannedApproachAltitude_ValueChanged(object sender, EventArgs e)
	{
		UpdatePlannedGLideSlope();
	}

	private void UpdatePlannedGLideSlope()
	{
		double num = Math.Atan2((double)nudPlannedApproachAltitude.Value, 6076.12 * (double)nudPlannedApproachDistance.Value);
		labPlannedApproachSlope.Text = Utility.RadToDeg(num).ToString("F1") + "°";
		double num2 = 0.0;
		if (FlightPlan.LandingRunway != null)
		{
			num2 = FlightPlan.AverageTas((double)FlightPlan.LandingRunway.runwayThreshold.Altitude + (double)nudPlannedApproachAltitude.Value, FlightPlan.LandingRunway.runwayThreshold.Altitude, (double)nudLandingIas.Value);
		}
		labPlannedApproachDescentRate.Text = ((0.0 - Math.Sin(num) * num2) * 101.26866666666666).ToString("F0") + "fpm";
	}

	private void btnLoadFseAirportAsDeparture_Click(object sender, EventArgs e)
	{
		if (FseDataManager.fseAirports.Count == 0)
		{
			FseDataManager.ReloadFSEAirports("fse//icaodata.csv");
		}
		FSEAirport fSEAirport = FseDataManager.fseAirports.Where((FSEAirport A) => A.icao == txtDepartureIcao.Text).FirstOrDefault();
		if (fSEAirport == null)
		{
			MessageBox.Show("Airport not found");
		}
		else
		{
			txtDepartureIcao.Text = fSEAirport.icao;
			labDepartureDescription.Text = fSEAirport.name;
			DepartureGoogle.Tag = fSEAirport.wayPoint.googleMapLink;
			labDepartureElevation.Text = fSEAirport.wayPoint.Altitude.ToString("F0");
			txtDepartureSessagesimal.Text = fSEAirport.wayPoint.GetSessagesimalCoordinates();
			if (SimulatorInformationProcessing.currentAircraft != null && SimulatorInformationProcessing.currentAircraft.position != null)
			{
				double num = fSEAirport.wayPoint.DistanceFromMiles(SimulatorInformationProcessing.currentAircraft.position);
				MessageBox.Show(fSEAirport.wayPoint.Id + " is " + num.ToString("F1") + " miles from current position");
			}
			FlightPlan.Departure = new ATCWaypoint(fSEAirport.lat, fSEAirport.lon, fSEAirport.icao, "AIRPORT", 0.0);
			labDepartureRunway.Text = "----";
			lblTakeOffRunway.Text = "----";
			FlightPlan.TakeOffRunway = null;
		}
		UpdateDepartureDestinationInfo();
	}

	private void DepartureGoogle_Click(object sender, EventArgs e)
	{
		if (FlightPlan.Departure != null)
		{
			Process.Start(FlightPlan.Departure.googleMapLink);
		}
	}

	private void ArrivalGoogle_Click(object sender, EventArgs e)
	{
		if (FlightPlan.Destination != null)
		{
			Process.Start(FlightPlan.Destination.googleMapLink);
		}
	}

	private void btnSearchTakeOffRunway_Click(object sender, EventArgs e)
	{
		if (FlightPlan.ATCWaypoints.Count == 0)
		{
			MessageBox.Show("You need to load a FlightPlan with a valid ICAO departure first");
			return;
		}
		FormSelectRunway formSelectRunway = new FormSelectRunway(OurAirportsManager.GetRunways(FlightPlan.ATCWaypoints[0].Id), FlightPlan.ATCWaypoints[0].Id);
		formSelectRunway.ShowDialog();
		labDepartureRunway.Text = formSelectRunway.selectedRunway.id + "  (" + formSelectRunway.selectedRunway.runwayThreshold.Altitude.ToString("F0") + "ft)";
		lblTakeOffRunway.Text = formSelectRunway.selectedRunway.id + "  (" + formSelectRunway.selectedRunway.runwayThreshold.Altitude.ToString("F0") + "ft)";
		FlightPlan.TakeOffRunway = formSelectRunway.selectedRunway;
		ShowPlan();
	}

	private void btnSearchLandingRunway_Click(object sender, EventArgs e)
	{
		if (FlightPlan.ATCWaypoints.Count == 0)
		{
			MessageBox.Show("You need to load a FlightPlan with a valid ICAO arrival first");
			return;
		}
		FormSelectRunway formSelectRunway = new FormSelectRunway(OurAirportsManager.GetRunways(FlightPlan.ATCWaypoints[FlightPlan.ATCWaypoints.Count - 1].Id), FlightPlan.ATCWaypoints[FlightPlan.ATCWaypoints.Count - 1].Id);
		formSelectRunway.ShowDialog();
		labArrivalRunway.Text = formSelectRunway.selectedRunway.id + "  (" + formSelectRunway.selectedRunway.runwayThreshold.Altitude.ToString("F0") + "ft)";
		lblLandingRunway.Text = formSelectRunway.selectedRunway.id + "  (" + formSelectRunway.selectedRunway.runwayThreshold.Altitude.ToString("F0") + "ft)";
		FlightPlan.LandingRunway = formSelectRunway.selectedRunway;
		UpdatePlannedGLideSlope();
		ShowPlan();
	}

	public void btnLoadOurAirportAsDeparture_Click(object sender, EventArgs e)
	{
		OurAirport airport = OurAirportsManager.GetAirport(txtDepartureIcao.Text);
		if (airport == null)
		{
			MessageBox.Show("Airport not found");
		}
		else
		{
			WayPoint wayPoint = airport.GetWayPoint();
			txtDepartureIcao.Text = airport.ident;
			labDepartureDescription.Text = airport.name;
			DepartureGoogle.Tag = wayPoint.googleMapLink;
			labDepartureElevation.Text = airport.elevation_ft.ToString();
			txtDepartureSessagesimal.Text = wayPoint.GetSessagesimalCoordinates();
			if (SimulatorInformationProcessing.currentAircraft != null && SimulatorInformationProcessing.currentAircraft.position != null)
			{
				double num = wayPoint.DistanceFromMiles(SimulatorInformationProcessing.currentAircraft.position);
				MessageBox.Show(wayPoint.Id + " is " + num.ToString("F1") + " miles from current position");
			}
			FlightPlan.Departure = new ATCWaypoint(wayPoint.latitude, wayPoint.longitude, airport.ident, "AIRPORT", airport.elevation_ft ?? 0.0);
			labDepartureRunway.Text = "----";
			lblTakeOffRunway.Text = "----";
			FlightPlan.TakeOffRunway = null;
		}
		UpdateDepartureDestinationInfo();
	}

	public void btnLoadOurAirportAsDestination_Click(object sender, EventArgs e)
	{
		OurAirport airport = OurAirportsManager.GetAirport(txtDestinationIcao.Text);
		if (airport == null)
		{
			MessageBox.Show("Airport not found");
		}
		else
		{
			WayPoint wayPoint = airport.GetWayPoint();
			txtDestinationIcao.Text = airport.ident;
			labDestinationDescription.Text = airport.name;
			DestinationGoogle.Tag = wayPoint.googleMapLink;
			labDestinationElevation.Text = airport.elevation_ft.ToString();
			txtDestinationSessagesimal.Text = wayPoint.GetSessagesimalCoordinates();
			FlightPlan.Destination = new ATCWaypoint(wayPoint.latitude, wayPoint.longitude, airport.ident, "AIRPORT", airport.elevation_ft ?? 0.0);
			labArrivalRunway.Text = "----";
			lblLandingRunway.Text = "----";
			FlightPlan.LandingRunway = null;
		}
		UpdateDepartureDestinationInfo();
	}

	private void youtubeChannelToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Process.Start("https://www.youtube.com/embed/videoseries?list=PL4nUhBGGMKtnSRRjzMbhNjfjI_jlaphw9");
	}

	private void discordChatToolStripMenuItem_Click(object sender, EventArgs e)
	{
		Process.Start("https://discord.gg/TRQ5KmqVEn");
	}

	private void ArrivalRunwayGoogle_Click(object sender, EventArgs e)
	{
	}

	private void DepartureRunwayGoogle_Click(object sender, EventArgs e)
	{
		if (FlightPlan.TakeOffRunway != null)
		{
			Process.Start(FlightPlan.TakeOffRunway.runwayThreshold.googleMapLink);
		}
	}

	private void btnTeleportToCustomLocation_Click(object sender, EventArgs e)
	{
		SimulatorConnectionManager.Struct3 @struct = new SimulatorConnectionManager.Struct3
		{
			altitude = (double)nudTeleportAltitude.Value,
			latitude = Utility.toDouble(txtTeleportCoordinates.Text.Split(',')[0]),
			longitude = Utility.toDouble(txtTeleportCoordinates.Text.Split(',')[1]),
			plane_heading_degree_true = Utility.DegToRad((double)nudTeleportHeading.Value)
		};
		SimulatorConnectionManager.Sim_TransmitDataToSimConnect(SimulatorConnectionManager.DEFINITIONS1.STRUCT3, @struct);
	}

	private void LandingRunwayGoogle_Click(object sender, EventArgs e)
	{
		if (FlightPlan.LandingRunway != null)
		{
			Process.Start(FlightPlan.LandingRunway.runwayThreshold.googleMapLink);
		}
	}

	private void nudLandingIas_ValueChanged(object sender, EventArgs e)
	{
		UpdatePlannedGLideSlope();
	}

	private void btnChaseLocation_Click(object sender, EventArgs e)
	{
		SimulatorInformationProcessing.customLocation = new WayPoint(Utility.toDouble(txtTeleportCoordinates.Text.Split(',')[0]), Utility.toDouble(txtTeleportCoordinates.Text.Split(',')[1]), "Custom Location", "CUSTOM", (double)nudTeleportAltitude.Value);
	}

	private void panelGraphicLog_Paint(object sender, PaintEventArgs e)
	{
		if (graphicLogImage != null)
		{
			e.Graphics.DrawImage(graphicLogImage, new Point(0, 0));
		}
	}

	public void GraphicLogProcedure(SimulatorConnectionManager.Struct2 simulatorData)
	{
		if (graphicLogImage == null || graphicLogImage.Width != panelGraphicLog.Width || graphicLogImage.Height != panelGraphicLog.Height)
		{
			graphicLogImage = new Bitmap(panelGraphicLog.Width, panelGraphicLog.Height);
		}
		graphicLogX++;
		if (graphicLogX > panelGraphicLog.Width)
		{
			graphicLogX = 0;
		}
		Graphics graphics = Graphics.FromImage(graphicLogImage);
		graphics.FillRectangle(new SolidBrush(panelGraphicLog.BackColor), graphicLogX + 2, 0, 3, panelGraphicLog.Height);
		IEnumerable<CheckBox> source = flpGraphicLogConfig.Controls.OfType<CheckBox>();
		IEnumerable<NumericUpDown> source2 = flpGraphicLogConfig.Controls.OfType<NumericUpDown>();
		FieldInfo[] fields = typeof(SimulatorConnectionManager.Struct2).GetFields();
		foreach (FieldInfo fi in fields)
		{
			if (!(fi.FieldType == typeof(double)))
			{
				continue;
			}
			CheckBox checkBox = source.Where((CheckBox C) => C.Name == "CHK" + fi.Name).FirstOrDefault();
			if (checkBox.Checked)
			{
				NumericUpDown numericUpDown = source2.Where((NumericUpDown N) => N.Name == "NUD" + fi.Name).FirstOrDefault();
				DrawLogLine(graphics, checkBox.BackColor, (double)fi.GetValue(previousLogSimulatorData), (double)fi.GetValue(simulatorData), (double)numericUpDown.Value);
			}
		}
		panelGraphicLog.Refresh();
		previousLogSimulatorData = simulatorData;
	}

	private void DrawLogLine(Graphics G, Color C, double v1, double v2, double m)
	{
		Pen pen = new Pen(C);
		double num = panelGraphicLog.Height / 2;
		float num2 = (float)(num - v1 * m);
		float num3 = (float)(num - v2 * m);
		G.DrawLine(pen, new PointF(graphicLogX, num2), new PointF(graphicLogX + 1, num3));
	}

	private void SetupLogControls()
	{
		List<Color> list = new List<Color>();
		for (int i = 0; i < 3; i++)
		{
			list.Add(Color.DarkGray);
		}
		list.Add(Color.LightYellow);
		list.Add(Color.Red);
		list.Add(Color.Orange);
		list.Add(Color.Yellow);
		list.Add(Color.Green);
		list.Add(Color.Blue);
		list.Add(Color.DarkGray);
		list.Add(Color.DarkGray);
		list.Add(Color.Cyan);
		list.Add(Color.Purple);
		list.Add(Color.DarkOrange);
		list.Add(Color.DarkGreen);
		list.Add(Color.DarkBlue);
		list.Add(Color.DarkCyan);
		list.Add(Color.MediumPurple);
		list.Add(Color.MediumPurple);
		list.Add(Color.AliceBlue);
		list.Add(Color.BlanchedAlmond);
		list.Add(Color.CadetBlue);
		list.Add(Color.Firebrick);
		list.Add(Color.Gainsboro);
		for (int j = 0; j < 100; j++)
		{
			list.Add(Color.DarkGray);
		}
		int num = 0;
		FieldInfo[] fields = typeof(SimulatorConnectionManager.Struct2).GetFields();
		foreach (FieldInfo fieldInfo in fields)
		{
			if (fieldInfo.FieldType == typeof(double))
			{
				CheckBox checkBox = new CheckBox();
				checkBox.Name = "CHK" + fieldInfo.Name;
				checkBox.Text = fieldInfo.Name;
				checkBox.Width = 160;
				checkBox.BackColor = list[num];
				checkBox.ForeColor = Color.Black;
				flpGraphicLogConfig.Controls.Add(checkBox);
				NumericUpDown numericUpDown = new NumericUpDown();
				numericUpDown.Minimum = 1m;
				numericUpDown.Maximum = 1000m;
				numericUpDown.Value = 1m;
				numericUpDown.Increment = 1m;
				numericUpDown.Name = "NUD" + fieldInfo.Name;
				numericUpDown.Width = 40;
				flpGraphicLogConfig.Controls.Add(numericUpDown);
				num++;
			}
		}
	}

	private void btnAirplaneCheck_Click(object sender, EventArgs e)
	{
		if (SimulatorConnectionManager.MySim != null)
		{
			SimulatorConnectionManager.Sim_RequestDataToSimConnect(SimulatorConnectionManager.DATA_REQUESTS1.REQUEST4, SimulatorConnectionManager.DEFINITIONS1.STRUCT10);
		}
	}

	public void displayAirplaneData(SimulatorConnectionManager.Struct10 airplaneData)
	{
		labAirplaneDescription.Text = airplaneData.title + "\r\nCruise Altitude: " + airplaneData.DESIGN_CRUISE_ALT.ToString("F0") + "\r\nCruise Speed: " + airplaneData.DESIGN_SPEED_VC.ToString("F0") + "\r\nWing Span/Area: " + airplaneData.WING_SPAN.ToString("F0") + " " + airplaneData.WING_AREA.ToString("F0") + "\r\n";
	}

	private void btnGetPlaneQuotation_click(object sender, EventArgs e)
	{
		try
		{
			BuddyWorldManager.QuotateAirplane();
			if (BuddyWorldManager.quotatedAirplane != null)
			{
				lblAirplaneQuotation.Text = BuddyWorldManager.quotatedAirplane.ToString() + "\r\nBody: " + (100.0 * BuddyWorldManager.quotatedAirplane.bodyStatus).ToString("F1") + "% Engine: " + (100.0 * BuddyWorldManager.quotatedAirplane.engineStatus).ToString("F1") + "% Price: " + BuddyWorldManager.quotatedAirplane.quotation.ToString("F0") + " Markup: " + (100.0 * BuddyWorldManager.quotatedAirplane.quotation / BuddyWorldManager.quotatedAirplane.CurrentMarketValue()).ToString("F0") + "%";
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnBuyQuotedPlane_Click(object sender, EventArgs e)
	{
		try
		{
			if (BuddyWorldManager.quotatedAirplane != null)
			{
				BuddyWorldManager.BuyQuotedPlane();
				RefreshBuddyWorldTab();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void RefreshFlightStatus()
	{
		labFlightStatus.Text = BuddyWorldManager.FlightDescription();
	}

	public void OnRefreshBuddyWorldManager(object sender, EventArgs e)
	{
		RefreshBuddyWorldTab();
	}

	public void OnRefreshBuddyWorldManagerActivities(object sender, EventArgs e)
	{
		RefreshActivityTab();
	}

	public void RefreshBuddyWorldTab()
	{
		listAirplanes.Tag = "NOEVENT";
		listAirplanes.DataSource = null;
		listAirplanes.DataSource = BuddyWorldManager.world.airplanes;
		listAirplanes.Refresh();
		Application.DoEvents();
		listAirplanes.Tag = null;
		if (BuddyWorldManager.selectedPlane != null)
		{
			Airplane selectedPlane = BuddyWorldManager.selectedPlane;
			lblSelectedAirplane.Text = selectedPlane.ToString();
			if (selectedPlane.rented)
			{
				lblSelectedAirplane.Text += " (rented)";
			}
			btnWaypointSelectedAirplane.Text = selectedPlane.position.Id;
			btnWaypointSelectedAirplane.Tag = selectedPlane.position;
			labSelectedAirplaneFuel.Text = selectedPlane.fuelgal.ToString("F0") + "/" + selectedPlane.fuel_total_capacity.ToString("F0") + " - " + (100.0 * (selectedPlane.fuelgal / selectedPlane.fuel_total_capacity)).ToString("F0") + "%";
			labAirplaneCompleteFlights.Text = selectedPlane.completeFlights.ToString();
			lblAirplaneFlightHours.Text = selectedPlane.flightHours.ToString("F1");
			labAirplaneMileage.Text = selectedPlane.flightMileage.ToString("F0");
			labAirplaneBodyStatus.Text = (100.0 * selectedPlane.bodyStatus).ToString("F1") + "%";
			labAirplaneEngineStatus.Text = (100.0 * selectedPlane.engineStatus).ToString("F1") + "%";
			labAirplaneMarketPrice.Text = selectedPlane.CurrentMarketValue().ToString("F0");
			if (selectedPlane.nextAvailableMoment > DateTime.Now)
			{
				labNextAvailableMoment.Text = selectedPlane.nextAvailableMoment.ToString();
			}
			else
			{
				labNextAvailableMoment.Text = "Available";
			}
			if (selectedPlane.rented)
			{
				btnSellAirplane.Text = "RETURN";
			}
			else
			{
				btnSellAirplane.Text = "SELL";
			}
			if (selectedPlane.bodyDamage == 0.0)
			{
				btnAirplaneBodyMaitenanceRepair.Text = "Maintenance";
				btnAirplaneBodyMaitenanceRepair.ForeColor = Color.Black;
			}
			else
			{
				btnAirplaneBodyMaitenanceRepair.Text = "Repair";
				btnAirplaneBodyMaitenanceRepair.ForeColor = Color.Red;
			}
			if (selectedPlane.engineDamage == 0.0)
			{
				btnAirplaneEngineMaitenanceRepair.Text = "Maintenance";
				btnAirplaneEngineMaitenanceRepair.ForeColor = Color.Black;
			}
			else
			{
				btnAirplaneEngineMaitenanceRepair.Text = "Repair";
				btnAirplaneEngineMaitenanceRepair.ForeColor = Color.Red;
			}
		}
		else
		{
			lblSelectedAirplane.Text = "---";
			btnWaypointSelectedAirplane.Text = "---";
			btnWaypointSelectedAirplane.Tag = null;
			labSelectedAirplaneFuel.Text = "---";
			labAirplaneCompleteFlights.Text = "---";
			lblAirplaneFlightHours.Text = "---";
			labAirplaneMileage.Text = "---";
			labAirplaneBodyStatus.Text = "---";
			labAirplaneMarketPrice.Text = "---";
			btnAirplaneBodyMaitenanceRepair.Text = "---";
			labNextAvailableMoment.Text = "---";
			btnSellAirplane.Text = "SELL";
		}
		labCash.Text = BuddyWorldManager.world.money.ToString("F0") + " $";
		labReputation.Text = BuddyWorldManager.world.reputation.ToString("F1");
		labPilotPositionDescription.Text = BuddyWorldManager.world.yourPosition.Id;
		nudPilotWeight.Value = BuddyWorldManager.world.pilotWeight;
		txtHomeBase.Text = BuddyWorldManager.world.homeBase;
		RefreshFlightStatus();
		labFlightRequiredPayload.Text = ((double)BuddyWorldManager.world.pilotWeight + BuddyWorldManager.CurrentLoadedPayload().Sum((PayLoad payload) => payload.WeightLb)).ToString("F0") + " lbs";
		panelPayload.Controls.Clear();
		int num = 0;
		List<PayLoad> list = BuddyWorldManager.CurrentLoadedPayload();
		if (list == null)
		{
			return;
		}
		foreach (PayLoad item in list)
		{
			if (item != null)
			{
				CtlPayload ctlPayload = new CtlPayload();
				panelPayload.Controls.Add(ctlPayload);
				ctlPayload.Location = new Point(0, num);
				ctlPayload.Width = panelPayload.Width - 100;
				ctlPayload.RefreshPayload(item);
				num += ctlPayload.Height;
			}
		}
	}

	private void listAirplanes_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (!((string)(listAirplanes.Tag ?? "") == "NOEVENT"))
		{
			BuddyWorldManager.TryToSelectPlane((Airplane)listAirplanes.SelectedItem);
			RefreshBuddyWorldTab();
		}
	}

	private void tabBuddyWorld_Click(object sender, EventArgs e)
	{
	}

	private void btnSellAirplane_Click(object sender, EventArgs e)
	{
		try
		{
			if (BuddyWorldManager.selectedPlane != null)
			{
				if (!BuddyWorldManager.selectedPlane.rented)
				{
					BuddyWorldManager.SellSelectedPlane();
				}
				else
				{
					BuddyWorldManager.ReturnPlane(BuddyWorldManager.selectedPlane);
				}
				RefreshBuddyWorldTab();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnRefuel_Click(object sender, EventArgs e)
	{
		try
		{
			if (BuddyWorldManager.selectedPlane != null)
			{
				BuddyWorldManager.Refuel(BuddyWorldManager.selectedPlane, (double)nupRefuel.Value);
				RefreshBuddyWorldTab();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnPilotPosition_Click(object sender, EventArgs e)
	{
		if (BuddyWorldManager.world != null && BuddyWorldManager.world.yourPosition != null)
		{
			Process.Start(BuddyWorldManager.world.yourPosition.googleMapLink);
		}
	}

	private void btnTravelTo_Click(object sender, EventArgs e)
	{
		OurAirport airport = OurAirportsManager.GetAirport(txtIcaoNewPosition.Text);
		BuddyWorldManager.TravelTo(airport.GetWayPoint());
		RefreshBuddyWorldTab();
	}

	private void btnTravelToAirplane_Click(object sender, EventArgs e)
	{
		if (BuddyWorldManager.selectedPlane != null)
		{
			BuddyWorldManager.TravelTo(BuddyWorldManager.selectedPlane.position);
		}
		RefreshBuddyWorldTab();
	}

	private void btnAirplanePositionDescription_Click(object sender, EventArgs e)
	{
		try
		{
			if (BuddyWorldManager.selectedPlane != null)
			{
				BuddyWorldManager.SetAirplanePositionDescription(BuddyWorldManager.selectedPlane);
				RefreshBuddyWorldTab();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnstartFlight_Click(object sender, EventArgs e)
	{
		try
		{
			BuddyWorldManager.StartFlight();
			RefreshBuddyWorldTab();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnEndFlight_Click(object sender, EventArgs e)
	{
		try
		{
			if (!BuddyWorldManager.EndFlight())
			{
				MessageBox.Show("Impossible to end flight");
			}
			RefreshBuddyWorldTab();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnAirplaneMaitenanceRepair_Click(object sender, EventArgs e)
	{
		try
		{
			if (BuddyWorldManager.selectedPlane != null)
			{
				BuddyWorldManager.AttemptBodyMaintenanceOrRepair(BuddyWorldManager.selectedPlane);
			}
			RefreshBuddyWorldTab();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnSearchActivities_Click(object sender, EventArgs e)
	{
		try
		{
			RefreshActivityTab();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void RefreshActivityTab()
	{
		try
		{
			btnGenerateActivitiesAtHome.Enabled = (BuddyWorldManager.world.homeBase ?? "") != "";
			btnGenerateActivitiesAtHome.Text = BuddyWorldManager.world.homeBase ?? "";
			btnGenerateActivitiesAtUserPos.Text = BuddyWorldManager.world.yourPosition.Id;
			ActivitySearchPanel.Controls.Clear();
			ActivityAssignedPanel.Controls.Clear();
			int num = 0;
			List<Activity> list = BuddyWorldManager.world.activities.Where((Activity A) => !A.Accepted).ToList();
			num = 0;
			foreach (Activity item in list)
			{
				CtlActivity ctlActivity = new CtlActivity();
				ActivitySearchPanel.Controls.Add(ctlActivity);
				ctlActivity.Width = ActivitySearchPanel.Width - 20;
				ctlActivity.Location = new Point(0, num);
				ctlActivity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
				ctlActivity.RefreshActivityControl(item, this);
				num = ctlActivity.Top + ctlActivity.Height;
			}
			List<Activity> list2 = BuddyWorldManager.world.activities.Where((Activity A) => A.Accepted).ToList();
			num = 0;
			foreach (Activity item2 in list2)
			{
				CtlActivity ctlActivity2 = new CtlActivity();
				ActivityAssignedPanel.Controls.Add(ctlActivity2);
				ctlActivity2.Width = ActivityAssignedPanel.Width - 20;
				ctlActivity2.Location = new Point(0, num);
				ctlActivity2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
				ctlActivity2.RefreshActivityControl(item2, this);
				num = ctlActivity2.Top + ctlActivity2.Height;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnAbortFlight_Click(object sender, EventArgs e)
	{
		try
		{
			BuddyWorldManager.AbortFlight();
			RefreshBuddyWorldTab();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnGenerateActivitiesAtHome_Click(object sender, EventArgs e)
	{
		if ((BuddyWorldManager.world.homeBase ?? "") != "")
		{
			BuddyWorldManager.RequestNewActivity(OurAirportsManager.GetAirport(BuddyWorldManager.world.homeBase).GetWayPoint());
		}
		RefreshActivityTab();
	}

	private void compassToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (frmCompass.IsDisposed)
		{
			frmCompass = new FormCompass();
		}
		frmCompass.Show();
		frmCompass.Location = new Point(Screen.FromControl(this).Bounds.Left, 0);
		frmCompass.TopMost = true;
		frmCompass.TopMost = false;
		frmCompass.Focus();
	}

	public void RefreshFinanceTab()
	{
		try
		{
			lstTransactions.DataSource = null;
			lstTransactions.DataSource = BuddyWorldManager.world.transactions;
			lstTransactions.Refresh();
			labFinanceCash.Text = BuddyWorldManager.world.money.ToString("F0") + " $";
			labFinanceLoan.Text = BuddyWorldManager.world.loan.ToString("F0") + " $";
			labFinanceMaxLoan.Text = BuddyWorldManager.world.maxloan().ToString("F0") + " $";
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnAskNewLoan_Click(object sender, EventArgs e)
	{
		BuddyWorldManager.AskLoan((double)nudLoan.Value);
		RefreshFinanceTab();
	}

	private void btnRepayLoan_Click(object sender, EventArgs e)
	{
		BuddyWorldManager.RepayLoan((double)nudLoan.Value);
		RefreshFinanceTab();
	}

	private void nudPilotWeight_ValueChanged(object sender, EventArgs e)
	{
		BuddyWorldManager.world.pilotWeight = (int)nudPilotWeight.Value;
		BuddyWorldManager.SaveBuddyWorld();
	}

	private void btnAirplaneEngineMaitenanceRepair_Click(object sender, EventArgs e)
	{
		try
		{
			try
			{
				if (BuddyWorldManager.selectedPlane != null)
				{
					BuddyWorldManager.AttemptEngineMaintenanceOrRepair(BuddyWorldManager.selectedPlane);
				}
				RefreshBuddyWorldTab();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
		}
	}

	private void btnWaypointButton_Click(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		FormWaypoint formWaypoint = new FormWaypoint();
		if (button.Name.ToLower().Contains("selectedairplane"))
		{
			formWaypoint.heading = BuddyWorldManager.selectedPlane.heading;
		}
		if (button.Tag != null)
		{
			formWaypoint.WP = (WayPoint)button.Tag;
		}
		formWaypoint.ShowDialog();
	}

	private void btnCheckActivityLocation_Click(object sender, EventArgs e)
	{
	}

	private void btnRentQuotedPlane_Click(object sender, EventArgs e)
	{
		try
		{
			if (BuddyWorldManager.quotatedAirplane != null)
			{
				BuddyWorldManager.RentQuotedAirplane();
				RefreshBuddyWorldTab();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void positionstackKeyToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void btnSetPositionstackKey_Click(object sender, EventArgs e)
	{
		if (BuddyWorldManager.world != null)
		{
			BuddyWorldManager.world.positionstackkey = txtPositionstackKey.Text;
		}
		BuddyWorldManager.SaveBuddyWorld();
		txtPositionstackKey.Text = "";
		MessageBox.Show("The key has been saved. For privacy reason it won't be displayed.");
	}

	private void btnInfo_Click(object sender, EventArgs e)
	{
		WayPoint wP = new WayPoint(Utility.toDouble(txtTeleportCoordinates.Text.Split(',')[0]), Utility.toDouble(txtTeleportCoordinates.Text.Split(',')[1]), "Custom Location", "CUSTOM", (double)nudTeleportAltitude.Value);
		PositionStackData positionStackData = PositionStackManager.ReverseLocation(wP);
		if (positionStackData != null && positionStackData.data != null)
		{
			PositionStackElement[] data = positionStackData.data;
			foreach (PositionStackElement positionStackElement in data)
			{
				MessageBox.Show(positionStackElement.ToString());
			}
		}
	}

	private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void btnAdvertisement_Click(object sender, EventArgs e)
	{
		BuddyWorldManager.TryToAdvertise();
	}

	private void btnHomeBase_Click(object sender, EventArgs e)
	{
		BuddyWorldManager.TryToSetHome(txtHomeBase.Text);
	}

	private void btnGenerateActivitiesAtUserPos_Click(object sender, EventArgs e)
	{
		BuddyWorldManager.RequestNewActivity(BuddyWorldManager.world.yourPosition);
		RefreshActivityTab();
	}

	private void helicopterHelpToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (frmHeliHelp.IsDisposed)
		{
			frmHeliHelp = new FormHeliHelp();
		}
		frmHeliHelp.Show();
		frmHeliHelp.Location = new Point(Screen.FromControl(this).Bounds.Left, 0);
		frmHeliHelp.TopMost = true;
		frmHeliHelp.TopMost = false;
		frmHeliHelp.Focus();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavBuddy.FormMain));
		this.lblPlanDescription = new System.Windows.Forms.Label();
		this.dataGridView1 = new System.Windows.Forms.DataGridView();
		this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.googleMapLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Altitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.IAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.avgTAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Dist_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Dist_sum_nm = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.FPM = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.HDG_deg = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.time_sum = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.arrival_time_expected = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.arrival_time_actual = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.labBuddyWorldFlightCompactStatus = new System.Windows.Forms.Label();
		this.picBoxConnSpy = new System.Windows.Forms.PictureBox();
		this.btnPositionGoogle = new System.Windows.Forms.Button();
		this.label24 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.labCurrentPOS = new System.Windows.Forms.Label();
		this.labRemainingMiles = new System.Windows.Forms.Label();
		this.labElapsedMiles = new System.Windows.Forms.Label();
		this.labArrivalTimeReal = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.labRemainigTime = new System.Windows.Forms.Label();
		this.labArrivalTime = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.labElapsedTime = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.labCurrentTime = new System.Windows.Forms.Label();
		this.tabControl1 = new System.Windows.Forms.TabControl();
		this.tabNavLog = new System.Windows.Forms.TabPage();
		this.grpRunways = new System.Windows.Forms.GroupBox();
		this.btnSearchTakeOffRunway = new System.Windows.Forms.Button();
		this.btnSearchLandingRunway = new System.Windows.Forms.Button();
		this.lblLandingRunway = new System.Windows.Forms.Label();
		this.label60 = new System.Windows.Forms.Label();
		this.lblTakeOffRunway = new System.Windows.Forms.Label();
		this.LandingRunwayGoogle = new System.Windows.Forms.Button();
		this.label58 = new System.Windows.Forms.Label();
		this.TakeOffRunwayGoogle = new System.Windows.Forms.Button();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.tabParameters = new System.Windows.Forms.TabPage();
		this.grpPerformance = new System.Windows.Forms.GroupBox();
		this.nudLandFPM = new System.Windows.Forms.NumericUpDown();
		this.nudThrottleDamper = new System.Windows.Forms.NumericUpDown();
		this.label90 = new System.Windows.Forms.Label();
		this.nudThrottleEffect = new System.Windows.Forms.NumericUpDown();
		this.label91 = new System.Windows.Forms.Label();
		this.nudFlapsLandingPerc = new System.Windows.Forms.NumericUpDown();
		this.label89 = new System.Windows.Forms.Label();
		this.label88 = new System.Windows.Forms.Label();
		this.nudFlapsLandingIas = new System.Windows.Forms.NumericUpDown();
		this.label87 = new System.Windows.Forms.Label();
		this.label86 = new System.Windows.Forms.Label();
		this.label56 = new System.Windows.Forms.Label();
		this.nudReverseThrust = new System.Windows.Forms.NumericUpDown();
		this.label53 = new System.Windows.Forms.Label();
		this.label63 = new System.Windows.Forms.Label();
		this.nudMaxBankAngle = new System.Windows.Forms.NumericUpDown();
		this.label70 = new System.Windows.Forms.Label();
		this.panel1 = new System.Windows.Forms.Panel();
		this.label85 = new System.Windows.Forms.Label();
		this.nudElevatorDamper = new System.Windows.Forms.NumericUpDown();
		this.label71 = new System.Windows.Forms.Label();
		this.nudElevatorEffect = new System.Windows.Forms.NumericUpDown();
		this.label72 = new System.Windows.Forms.Label();
		this.nudAileronDamper = new System.Windows.Forms.NumericUpDown();
		this.label66 = new System.Windows.Forms.Label();
		this.nudAileronEffect = new System.Windows.Forms.NumericUpDown();
		this.label67 = new System.Windows.Forms.Label();
		this.nudRudderEffect = new System.Windows.Forms.NumericUpDown();
		this.label62 = new System.Windows.Forms.Label();
		this.nudFlapsTakeOffIas = new System.Windows.Forms.NumericUpDown();
		this.label54 = new System.Windows.Forms.Label();
		this.nudFlapsTakeOffPerc = new System.Windows.Forms.NumericUpDown();
		this.nudLandingGearDownAGL = new System.Windows.Forms.NumericUpDown();
		this.nudLandingGearUpAGL = new System.Windows.Forms.NumericUpDown();
		this.label35 = new System.Windows.Forms.Label();
		this.nudTakeOffCompletedAGL = new System.Windows.Forms.NumericUpDown();
		this.label38 = new System.Windows.Forms.Label();
		this.nudDescFPM = new System.Windows.Forms.NumericUpDown();
		this.label44 = new System.Windows.Forms.Label();
		this.nudRunwayEntAGL = new System.Windows.Forms.NumericUpDown();
		this.label36 = new System.Windows.Forms.Label();
		this.nudClimbFPM = new System.Windows.Forms.NumericUpDown();
		this.label19 = new System.Windows.Forms.Label();
		this.nudDescIas = new System.Windows.Forms.NumericUpDown();
		this.label18 = new System.Windows.Forms.Label();
		this.nudClimbIas = new System.Windows.Forms.NumericUpDown();
		this.label17 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.nudCruiseIas = new System.Windows.Forms.NumericUpDown();
		this.label1 = new System.Windows.Forms.Label();
		this.nudSafeIas = new System.Windows.Forms.NumericUpDown();
		this.label37 = new System.Windows.Forms.Label();
		this.nudLandingIas = new System.Windows.Forms.NumericUpDown();
		this.tabPlanning = new System.Windows.Forms.TabPage();
		this.label80 = new System.Windows.Forms.Label();
		this.label79 = new System.Windows.Forms.Label();
		this.label78 = new System.Windows.Forms.Label();
		this.labPlannedApproachDescentRate = new System.Windows.Forms.Label();
		this.labPlanningDistance = new System.Windows.Forms.Label();
		this.label69 = new System.Windows.Forms.Label();
		this.label68 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.btnTeleportToRunway = new System.Windows.Forms.Button();
		this.label39 = new System.Windows.Forms.Label();
		this.nudAddFeetTeleport = new System.Windows.Forms.NumericUpDown();
		this.label65 = new System.Windows.Forms.Label();
		this.label64 = new System.Windows.Forms.Label();
		this.label50 = new System.Windows.Forms.Label();
		this.labDestinationElevation = new System.Windows.Forms.Label();
		this.labDepartureElevation = new System.Windows.Forms.Label();
		this.labDestinationDescription = new System.Windows.Forms.Label();
		this.labDepartureDescription = new System.Windows.Forms.Label();
		this.labArrivalRunway = new System.Windows.Forms.Label();
		this.labDepartureRunway = new System.Windows.Forms.Label();
		this.label59 = new System.Windows.Forms.Label();
		this.labPlannedApproachSlope = new System.Windows.Forms.Label();
		this.nudPlannedApproachAltitude = new System.Windows.Forms.NumericUpDown();
		this.label57 = new System.Windows.Forms.Label();
		this.nudPlannedApproachDistance = new System.Windows.Forms.NumericUpDown();
		this.label55 = new System.Windows.Forms.Label();
		this.nudPlannedCruiseAltitude = new System.Windows.Forms.NumericUpDown();
		this.label33 = new System.Windows.Forms.Label();
		this.btnCreateFlightPlan = new System.Windows.Forms.Button();
		this.label32 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.txtDestinationIcao = new System.Windows.Forms.TextBox();
		this.txtDestinationSessagesimal = new System.Windows.Forms.TextBox();
		this.label27 = new System.Windows.Forms.Label();
		this.txtDepartureIcao = new System.Windows.Forms.TextBox();
		this.label51 = new System.Windows.Forms.Label();
		this.txtDepartureSessagesimal = new System.Windows.Forms.TextBox();
		this.btnLoadOurAirportAsDeparture = new System.Windows.Forms.Button();
		this.btnLoadOurAirportAsDestination = new System.Windows.Forms.Button();
		this.btnLoadFseAirportAsDeparture = new System.Windows.Forms.Button();
		this.btnSearchRunwayDeparture = new System.Windows.Forms.Button();
		this.btnSearchRunwayArrival = new System.Windows.Forms.Button();
		this.btnLoadFseAirportAsDestination = new System.Windows.Forms.Button();
		this.DestinationGoogle = new System.Windows.Forms.Button();
		this.DepartureGoogle = new System.Windows.Forms.Button();
		this.tabFuelManagement = new System.Windows.Forms.TabPage();
		this.groupBox8 = new System.Windows.Forms.GroupBox();
		this.label61 = new System.Windows.Forms.Label();
		this.labEstimatedResidualFlightMiles = new System.Windows.Forms.Label();
		this.label34 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.labFuelWeight = new System.Windows.Forms.Label();
		this.labFuelQuantity = new System.Windows.Forms.Label();
		this.labFuelFlow = new System.Windows.Forms.Label();
		this.label29 = new System.Windows.Forms.Label();
		this.label30 = new System.Windows.Forms.Label();
		this.labEstimatedResidualFlightTime = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.labAirplanetitle = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.tabForceTrack = new System.Windows.Forms.TabPage();
		this.tabPathTrack = new System.Windows.Forms.TabPage();
		this.nupFligthTrackRecordDrawingSamples = new System.Windows.Forms.NumericUpDown();
		this.label42 = new System.Windows.Forms.Label();
		this.btnExportFligthTrackRecord = new System.Windows.Forms.Button();
		this.btnResetFligthTrackRecord = new System.Windows.Forms.Button();
		this.nupFligthTrackRecordInterval = new System.Windows.Forms.NumericUpDown();
		this.MapPanel = new System.Windows.Forms.Panel();
		this.btnMapZoomDec = new System.Windows.Forms.Button();
		this.btnMapZoomInc = new System.Windows.Forms.Button();
		this.label41 = new System.Windows.Forms.Label();
		this.btnFligthTrackRecord = new System.Windows.Forms.Button();
		this.tabStandardManouver = new System.Windows.Forms.TabPage();
		this.groupBox11 = new System.Windows.Forms.GroupBox();
		this.lblST_CurrentTurnRay = new System.Windows.Forms.Label();
		this.lblST_ExpectedBanking = new System.Windows.Forms.Label();
		this.lblST_CurrentGS = new System.Windows.Forms.Label();
		this.label48 = new System.Windows.Forms.Label();
		this.lblST_CurrentIas = new System.Windows.Forms.Label();
		this.label47 = new System.Windows.Forms.Label();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.groupBox10 = new System.Windows.Forms.GroupBox();
		this.labRefHeading = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.labRefHeading270 = new System.Windows.Forms.Label();
		this.labRefHeading180 = new System.Windows.Forms.Label();
		this.labRefHeading90 = new System.Windows.Forms.Label();
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.labHeadingMag = new System.Windows.Forms.Label();
		this.PanelTurnSpeed = new System.Windows.Forms.Panel();
		this.label43 = new System.Windows.Forms.Label();
		this.label45 = new System.Windows.Forms.Label();
		this.labHeadingTrue = new System.Windows.Forms.Label();
		this.labDeltaHeading = new System.Windows.Forms.Label();
		this.label46 = new System.Windows.Forms.Label();
		this.btnResetCronometer = new System.Windows.Forms.Button();
		this.btnStartStopCronometer = new System.Windows.Forms.Button();
		this.label4 = new System.Windows.Forms.Label();
		this.labCronometer = new System.Windows.Forms.Label();
		this.tabMisc = new System.Windows.Forms.TabPage();
		this.label82 = new System.Windows.Forms.Label();
		this.btnSetPositionstackKey = new System.Windows.Forms.Button();
		this.label81 = new System.Windows.Forms.Label();
		this.txtPositionstackKey = new System.Windows.Forms.TextBox();
		this.groupBox12 = new System.Windows.Forms.GroupBox();
		this.labAirplaneDescription = new System.Windows.Forms.Label();
		this.btnAirplaneCheck = new System.Windows.Forms.Button();
		this.grpTeleport = new System.Windows.Forms.GroupBox();
		this.btnInfo = new System.Windows.Forms.Button();
		this.btnChaseLocation = new System.Windows.Forms.Button();
		this.label77 = new System.Windows.Forms.Label();
		this.label76 = new System.Windows.Forms.Label();
		this.nudTeleportHeading = new System.Windows.Forms.NumericUpDown();
		this.label74 = new System.Windows.Forms.Label();
		this.label75 = new System.Windows.Forms.Label();
		this.nudTeleportAltitude = new System.Windows.Forms.NumericUpDown();
		this.btnTeleportToCustomLocation = new System.Windows.Forms.Button();
		this.label73 = new System.Windows.Forms.Label();
		this.txtTeleportCoordinates = new System.Windows.Forms.TextBox();
		this.tabGraphicLog = new System.Windows.Forms.TabPage();
		this.grpGraphicLogConfig = new System.Windows.Forms.GroupBox();
		this.flpGraphicLogConfig = new System.Windows.Forms.FlowLayoutPanel();
		this.grpGraphicLog = new System.Windows.Forms.GroupBox();
		this.tabLog = new System.Windows.Forms.TabPage();
		this.txtCommLog = new System.Windows.Forms.TextBox();
		this.tabInfo = new System.Windows.Forms.TabPage();
		this.label14 = new System.Windows.Forms.Label();
		this.tabBuddyWorld = new System.Windows.Forms.TabPage();
		this.grpPayLoad = new System.Windows.Forms.GroupBox();
		this.panelPayload = new System.Windows.Forms.Panel();
		this.groupBox15 = new System.Windows.Forms.GroupBox();
		this.button1 = new System.Windows.Forms.Button();
		this.txtHomeBase = new System.Windows.Forms.TextBox();
		this.btnHomeBase = new System.Windows.Forms.Button();
		this.btnAdvertisement = new System.Windows.Forms.Button();
		this.label84 = new System.Windows.Forms.Label();
		this.labReputation = new System.Windows.Forms.Label();
		this.label103 = new System.Windows.Forms.Label();
		this.nudPilotWeight = new System.Windows.Forms.NumericUpDown();
		this.labPilotPositionDescription = new System.Windows.Forms.Label();
		this.btnTravelToAirplane = new System.Windows.Forms.Button();
		this.label92 = new System.Windows.Forms.Label();
		this.txtIcaoNewPosition = new System.Windows.Forms.TextBox();
		this.label83 = new System.Windows.Forms.Label();
		this.btnTravelToICAO = new System.Windows.Forms.Button();
		this.btnPilotPosition = new System.Windows.Forms.Button();
		this.labCash = new System.Windows.Forms.Label();
		this.groupBox13 = new System.Windows.Forms.GroupBox();
		this.btnRentQuotedPlane = new System.Windows.Forms.Button();
		this.btnBuyQuotedPlane = new System.Windows.Forms.Button();
		this.lblAirplaneQuotation = new System.Windows.Forms.Label();
		this.btnGetPlaneQuotation = new System.Windows.Forms.Button();
		this.groupBox16 = new System.Windows.Forms.GroupBox();
		this.labFlightRequiredPayload = new System.Windows.Forms.Label();
		this.label105 = new System.Windows.Forms.Label();
		this.btnAbortFlight = new System.Windows.Forms.Button();
		this.btnEndflight = new System.Windows.Forms.Button();
		this.labFlightStatus = new System.Windows.Forms.Label();
		this.btnstartFlight = new System.Windows.Forms.Button();
		this.groupBox14 = new System.Windows.Forms.GroupBox();
		this.panelWorldAirplanes = new System.Windows.Forms.Panel();
		this.labNextAvailableMoment = new System.Windows.Forms.Label();
		this.label102 = new System.Windows.Forms.Label();
		this.btnWaypointSelectedAirplane = new System.Windows.Forms.Button();
		this.btnAirplaneEngineMaitenanceRepair = new System.Windows.Forms.Button();
		this.labAirplaneEngineStatus = new System.Windows.Forms.Label();
		this.label107 = new System.Windows.Forms.Label();
		this.labAirplaneMarketPrice = new System.Windows.Forms.Label();
		this.label96 = new System.Windows.Forms.Label();
		this.listAirplanes = new System.Windows.Forms.ListBox();
		this.btnAirplaneBodyMaitenanceRepair = new System.Windows.Forms.Button();
		this.btnSellAirplane = new System.Windows.Forms.Button();
		this.label94 = new System.Windows.Forms.Label();
		this.lblSelectedAirplane = new System.Windows.Forms.Label();
		this.labAirplaneMileage = new System.Windows.Forms.Label();
		this.labAirplaneBodyStatus = new System.Windows.Forms.Label();
		this.label98 = new System.Windows.Forms.Label();
		this.labAirplaneCompleteFlights = new System.Windows.Forms.Label();
		this.label99 = new System.Windows.Forms.Label();
		this.label97 = new System.Windows.Forms.Label();
		this.nupRefuel = new System.Windows.Forms.NumericUpDown();
		this.label95 = new System.Windows.Forms.Label();
		this.btnRefuel = new System.Windows.Forms.Button();
		this.labSelectedAirplaneFuel = new System.Windows.Forms.Label();
		this.label93 = new System.Windows.Forms.Label();
		this.label40 = new System.Windows.Forms.Label();
		this.lblAirplaneFlightHours = new System.Windows.Forms.Label();
		this.tabActivities = new System.Windows.Forms.TabPage();
		this.groupBox17 = new System.Windows.Forms.GroupBox();
		this.label104 = new System.Windows.Forms.Label();
		this.btnGenerateActivitiesAtUserPos = new System.Windows.Forms.Button();
		this.btnGenerateActivitiesAtHome = new System.Windows.Forms.Button();
		this.ActivitySearchPanel = new System.Windows.Forms.Panel();
		this.groupBoxAssignedActivities = new System.Windows.Forms.GroupBox();
		this.ActivityAssignedPanel = new System.Windows.Forms.Panel();
		this.tabFinance = new System.Windows.Forms.TabPage();
		this.groupBox19 = new System.Windows.Forms.GroupBox();
		this.label106 = new System.Windows.Forms.Label();
		this.labFinanceMaxLoan = new System.Windows.Forms.Label();
		this.btnAskNewLoan = new System.Windows.Forms.Button();
		this.nudLoan = new System.Windows.Forms.NumericUpDown();
		this.label100 = new System.Windows.Forms.Label();
		this.labFinanceLoan = new System.Windows.Forms.Label();
		this.btnRepayLoan = new System.Windows.Forms.Button();
		this.label101 = new System.Windows.Forms.Label();
		this.labFinanceCash = new System.Windows.Forms.Label();
		this.groupBox18 = new System.Windows.Forms.GroupBox();
		this.lstTransactions = new System.Windows.Forms.ListBox();
		this.tabGoodsTrade = new System.Windows.Forms.TabPage();
		this.AircraftMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.PayloadMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.loadIntoAirplaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.unloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.deliverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.PayLoadcleanupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.AssignmentMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.MenuItemTakeAssignment = new System.Windows.Forms.ToolStripMenuItem();
		this.MenuItemDismissAssignment = new System.Windows.Forms.ToolStripMenuItem();
		this.TimerSlow = new System.Windows.Forms.Timer(this.components);
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.nudVnavHoldAlt = new System.Windows.Forms.NumericUpDown();
		this.radVnavHoldAGL = new System.Windows.Forms.RadioButton();
		this.radVnavStandard = new System.Windows.Forms.RadioButton();
		this.labVnavReference = new System.Windows.Forms.Label();
		this.label52 = new System.Windows.Forms.Label();
		this.labVnavDescription = new System.Windows.Forms.Label();
		this.label49 = new System.Windows.Forms.Label();
		this.btnExpandTabControl = new System.Windows.Forms.Button();
		this.label15 = new System.Windows.Forms.Label();
		this.labSlopeGoal = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.labFPMGoal = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.labAltitudeGoal = new System.Windows.Forms.Label();
		this.labCurrentAltitude = new System.Windows.Forms.Label();
		this.labCurrentGS = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.labArrivalTimeWP = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.labRemainingMilesWP = new System.Windows.Forms.Label();
		this.labRemainigTimeWP = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.labNextWaypointDescription = new System.Windows.Forms.Label();
		this.imageList16 = new System.Windows.Forms.ImageList(this.components);
		this.TimerQuick = new System.Windows.Forms.Timer(this.components);
		this.imageList40 = new System.Windows.Forms.ImageList(this.components);
		this.imageList20 = new System.Windows.Forms.ImageList(this.components);
		this.imageList7X7 = new System.Windows.Forms.ImageList(this.components);
		this.menuStrip1 = new System.Windows.Forms.MenuStrip();
		this.flightPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.resetFlightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.exportForGoogleEarthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.completeCurrentLegToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.simulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.flightControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.compassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.helicopterHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.dataFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.performanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.savePerformanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.loadPerformanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.youtubeChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.discordChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.timerCronometer = new System.Windows.Forms.Timer(this.components);
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picBoxConnSpy).BeginInit();
		this.tabControl1.SuspendLayout();
		this.tabNavLog.SuspendLayout();
		this.grpRunways.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.tabParameters.SuspendLayout();
		this.grpPerformance.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudLandFPM).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudThrottleDamper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudThrottleEffect).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudFlapsLandingPerc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudFlapsLandingIas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudReverseThrust).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudMaxBankAngle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudElevatorDamper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudElevatorEffect).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudAileronDamper).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudAileronEffect).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudRudderEffect).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudFlapsTakeOffIas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudFlapsTakeOffPerc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudLandingGearDownAGL).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudLandingGearUpAGL).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudTakeOffCompletedAGL).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudDescFPM).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudRunwayEntAGL).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudClimbFPM).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudDescIas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudClimbIas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudCruiseIas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudSafeIas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudLandingIas).BeginInit();
		this.tabPlanning.SuspendLayout();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudAddFeetTeleport).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudPlannedApproachAltitude).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudPlannedApproachDistance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudPlannedCruiseAltitude).BeginInit();
		this.tabFuelManagement.SuspendLayout();
		this.groupBox8.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.tabPathTrack.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nupFligthTrackRecordDrawingSamples).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nupFligthTrackRecordInterval).BeginInit();
		this.MapPanel.SuspendLayout();
		this.tabStandardManouver.SuspendLayout();
		this.groupBox11.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		this.groupBox10.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.groupBox9.SuspendLayout();
		this.tabMisc.SuspendLayout();
		this.groupBox12.SuspendLayout();
		this.grpTeleport.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudTeleportHeading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.nudTeleportAltitude).BeginInit();
		this.tabGraphicLog.SuspendLayout();
		this.grpGraphicLogConfig.SuspendLayout();
		this.tabLog.SuspendLayout();
		this.tabInfo.SuspendLayout();
		this.tabBuddyWorld.SuspendLayout();
		this.grpPayLoad.SuspendLayout();
		this.groupBox15.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudPilotWeight).BeginInit();
		this.groupBox13.SuspendLayout();
		this.groupBox16.SuspendLayout();
		this.groupBox14.SuspendLayout();
		this.panelWorldAirplanes.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nupRefuel).BeginInit();
		this.tabActivities.SuspendLayout();
		this.groupBox17.SuspendLayout();
		this.groupBoxAssignedActivities.SuspendLayout();
		this.tabFinance.SuspendLayout();
		this.groupBox19.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudLoan).BeginInit();
		this.groupBox18.SuspendLayout();
		this.AircraftMenuStrip.SuspendLayout();
		this.PayloadMenuStrip.SuspendLayout();
		this.AssignmentMenuStrip.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.nudVnavHoldAlt).BeginInit();
		this.menuStrip1.SuspendLayout();
		this.groupBox7.SuspendLayout();
		base.SuspendLayout();
		this.lblPlanDescription.BackColor = System.Drawing.Color.Black;
		this.lblPlanDescription.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblPlanDescription.ForeColor = System.Drawing.Color.Lime;
		this.lblPlanDescription.Location = new System.Drawing.Point(6, 17);
		this.lblPlanDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblPlanDescription.Name = "lblPlanDescription";
		this.lblPlanDescription.Size = new System.Drawing.Size(411, 94);
		this.lblPlanDescription.TabIndex = 2;
		this.lblPlanDescription.Text = "No plan loaded";
		this.dataGridView1.AllowUserToAddRows = false;
		this.dataGridView1.AllowUserToDeleteRows = false;
		this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dataGridView1.BackgroundColor = System.Drawing.Color.Black;
		this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle.BackColor = System.Drawing.Color.FromArgb(0, 192, 0);
		dataGridViewCellStyle.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView1.Columns.AddRange(this.Id, this.googleMapLink, this.Type, this.Altitude, this.IAS, this.avgTAS, this.Dist_nm, this.Dist_sum_nm, this.FPM, this.HDG_deg, this.time, this.time_sum, this.arrival_time_expected, this.arrival_time_actual, this.notes);
		this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dataGridView1.GridColor = System.Drawing.Color.White;
		this.dataGridView1.Location = new System.Drawing.Point(6, 134);
		this.dataGridView1.MultiSelect = false;
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
		this.dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
		this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dataGridView1.Size = new System.Drawing.Size(1131, 232);
		this.dataGridView1.TabIndex = 3;
		this.Id.FillWeight = 15f;
		this.Id.HeaderText = "Waypoint";
		this.Id.Name = "Id";
		this.Id.ReadOnly = true;
		this.googleMapLink.DataPropertyName = "googleMapLink";
		this.googleMapLink.FillWeight = 4f;
		this.googleMapLink.HeaderText = "";
		this.googleMapLink.Name = "googleMapLink";
		this.Type.DataPropertyName = "Type";
		this.Type.FillWeight = 15f;
		this.Type.HeaderText = "Type";
		this.Type.Name = "Type";
		this.Type.ReadOnly = true;
		this.Altitude.DataPropertyName = "Altitude";
		this.Altitude.FillWeight = 15f;
		this.Altitude.HeaderText = "Alt (ft)";
		this.Altitude.Name = "Altitude";
		this.IAS.DataPropertyName = "IAS";
		dataGridViewCellStyle2.Format = "N0";
		dataGridViewCellStyle2.NullValue = null;
		this.IAS.DefaultCellStyle = dataGridViewCellStyle2;
		this.IAS.FillWeight = 12f;
		this.IAS.HeaderText = "IAS";
		this.IAS.Name = "IAS";
		dataGridViewCellStyle3.Format = "N0";
		this.avgTAS.DefaultCellStyle = dataGridViewCellStyle3;
		this.avgTAS.FillWeight = 12f;
		this.avgTAS.HeaderText = "avgTAS";
		this.avgTAS.Name = "avgTAS";
		this.avgTAS.ReadOnly = true;
		this.Dist_nm.DataPropertyName = "Dist_nm";
		this.Dist_nm.FillWeight = 15f;
		this.Dist_nm.HeaderText = "Distance";
		this.Dist_nm.Name = "Dist_nm";
		this.Dist_nm.ReadOnly = true;
		this.Dist_sum_nm.DataPropertyName = "Dist_sum_nm";
		this.Dist_sum_nm.FillWeight = 20f;
		this.Dist_sum_nm.HeaderText = "Overall. d.";
		this.Dist_sum_nm.Name = "Dist_sum_nm";
		this.Dist_sum_nm.ReadOnly = true;
		this.FPM.DataPropertyName = "FPM";
		this.FPM.FillWeight = 12f;
		this.FPM.HeaderText = "rate";
		this.FPM.Name = "FPM";
		this.FPM.ReadOnly = true;
		this.HDG_deg.DataPropertyName = "HDG_deg";
		this.HDG_deg.FillWeight = 12f;
		this.HDG_deg.HeaderText = "Hdg°";
		this.HDG_deg.Name = "HDG_deg";
		this.HDG_deg.ReadOnly = true;
		this.time.FillWeight = 15f;
		this.time.HeaderText = "Time";
		this.time.Name = "time";
		this.time.ReadOnly = true;
		this.time_sum.DataPropertyName = "time_sum";
		this.time_sum.FillWeight = 20f;
		this.time_sum.HeaderText = "Overall. t.";
		this.time_sum.Name = "time_sum";
		this.time_sum.ReadOnly = true;
		this.arrival_time_expected.DataPropertyName = "arrival_time_expected";
		this.arrival_time_expected.FillWeight = 15f;
		this.arrival_time_expected.HeaderText = "Expected";
		this.arrival_time_expected.Name = "arrival_time_expected";
		this.arrival_time_expected.ReadOnly = true;
		this.arrival_time_actual.DataPropertyName = "arrival_time_actual";
		this.arrival_time_actual.FillWeight = 15f;
		this.arrival_time_actual.HeaderText = "Actual";
		this.arrival_time_actual.Name = "arrival_time_actual";
		this.notes.DataPropertyName = "notes";
		this.notes.FillWeight = 25f;
		this.notes.HeaderText = "Notes";
		this.notes.Name = "notes";
		this.notes.ReadOnly = true;
		this.groupBox1.BackColor = System.Drawing.Color.Black;
		this.groupBox1.Controls.Add(this.labBuddyWorldFlightCompactStatus);
		this.groupBox1.Controls.Add(this.picBoxConnSpy);
		this.groupBox1.Controls.Add(this.btnPositionGoogle);
		this.groupBox1.Controls.Add(this.label24);
		this.groupBox1.Controls.Add(this.label22);
		this.groupBox1.Controls.Add(this.label21);
		this.groupBox1.Controls.Add(this.label20);
		this.groupBox1.Controls.Add(this.labCurrentPOS);
		this.groupBox1.Controls.Add(this.labRemainingMiles);
		this.groupBox1.Controls.Add(this.labElapsedMiles);
		this.groupBox1.Controls.Add(this.labArrivalTimeReal);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.labRemainigTime);
		this.groupBox1.Controls.Add(this.labArrivalTime);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.labElapsedTime);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.labCurrentTime);
		this.groupBox1.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.ForeColor = System.Drawing.Color.White;
		this.groupBox1.Location = new System.Drawing.Point(4, 24);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(379, 142);
		this.groupBox1.TabIndex = 16;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Flight Progress";
		this.labBuddyWorldFlightCompactStatus.Font = new System.Drawing.Font("Calibri", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.labBuddyWorldFlightCompactStatus.ForeColor = System.Drawing.Color.Lime;
		this.labBuddyWorldFlightCompactStatus.Location = new System.Drawing.Point(236, 109);
		this.labBuddyWorldFlightCompactStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labBuddyWorldFlightCompactStatus.Name = "labBuddyWorldFlightCompactStatus";
		this.labBuddyWorldFlightCompactStatus.Size = new System.Drawing.Size(140, 25);
		this.labBuddyWorldFlightCompactStatus.TabIndex = 30;
		this.labBuddyWorldFlightCompactStatus.Text = "-----";
		this.labBuddyWorldFlightCompactStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.picBoxConnSpy.Image = NavBuddy.Properties.Resources.Spy7x7off1;
		this.picBoxConnSpy.Location = new System.Drawing.Point(356, 21);
		this.picBoxConnSpy.Name = "picBoxConnSpy";
		this.picBoxConnSpy.Size = new System.Drawing.Size(7, 7);
		this.picBoxConnSpy.TabIndex = 29;
		this.picBoxConnSpy.TabStop = false;
		this.btnPositionGoogle.BackColor = System.Drawing.Color.Black;
		this.btnPositionGoogle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnPositionGoogle.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnPositionGoogle.ForeColor = System.Drawing.Color.Black;
		this.btnPositionGoogle.Image = NavBuddy.Properties.Resources.googlelink;
		this.btnPositionGoogle.Location = new System.Drawing.Point(350, 86);
		this.btnPositionGoogle.Name = "btnPositionGoogle";
		this.btnPositionGoogle.Size = new System.Drawing.Size(20, 20);
		this.btnPositionGoogle.TabIndex = 28;
		this.btnPositionGoogle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnPositionGoogle.UseVisualStyleBackColor = false;
		this.btnPositionGoogle.Click += new System.EventHandler(btnPositionGoogle_Click);
		this.label24.AutoSize = true;
		this.label24.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label24.ForeColor = System.Drawing.Color.White;
		this.label24.Location = new System.Drawing.Point(190, 114);
		this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(47, 18);
		this.label24.TabIndex = 25;
		this.label24.Text = "Flight:";
		this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label22.AutoSize = true;
		this.label22.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label22.ForeColor = System.Drawing.Color.White;
		this.label22.Location = new System.Drawing.Point(190, 63);
		this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(32, 18);
		this.label22.TabIndex = 23;
		this.label22.Text = "nm:";
		this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label21.AutoSize = true;
		this.label21.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label21.ForeColor = System.Drawing.Color.White;
		this.label21.Location = new System.Drawing.Point(190, 39);
		this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(32, 18);
		this.label21.TabIndex = 22;
		this.label21.Text = "nm:";
		this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.ForeColor = System.Drawing.Color.White;
		this.label20.Location = new System.Drawing.Point(190, 87);
		this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(34, 18);
		this.label20.TabIndex = 21;
		this.label20.Text = "pos:";
		this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labCurrentPOS.AutoSize = true;
		this.labCurrentPOS.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labCurrentPOS.ForeColor = System.Drawing.Color.Lime;
		this.labCurrentPOS.Location = new System.Drawing.Point(232, 87);
		this.labCurrentPOS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labCurrentPOS.Name = "labCurrentPOS";
		this.labCurrentPOS.Size = new System.Drawing.Size(34, 18);
		this.labCurrentPOS.TabIndex = 20;
		this.labCurrentPOS.Text = "--/--";
		this.labCurrentPOS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labRemainingMiles.AutoSize = true;
		this.labRemainingMiles.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labRemainingMiles.ForeColor = System.Drawing.Color.Lime;
		this.labRemainingMiles.Location = new System.Drawing.Point(232, 63);
		this.labRemainingMiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labRemainingMiles.Name = "labRemainingMiles";
		this.labRemainingMiles.Size = new System.Drawing.Size(33, 18);
		this.labRemainingMiles.TabIndex = 19;
		this.labRemainingMiles.Text = "-----";
		this.labRemainingMiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labElapsedMiles.AutoSize = true;
		this.labElapsedMiles.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labElapsedMiles.ForeColor = System.Drawing.Color.Lime;
		this.labElapsedMiles.Location = new System.Drawing.Point(232, 39);
		this.labElapsedMiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labElapsedMiles.Name = "labElapsedMiles";
		this.labElapsedMiles.Size = new System.Drawing.Size(33, 18);
		this.labElapsedMiles.TabIndex = 18;
		this.labElapsedMiles.Text = "-----";
		this.labElapsedMiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labArrivalTimeReal.AutoSize = true;
		this.labArrivalTimeReal.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labArrivalTimeReal.ForeColor = System.Drawing.Color.Lime;
		this.labArrivalTimeReal.Location = new System.Drawing.Point(93, 111);
		this.labArrivalTimeReal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labArrivalTimeReal.Name = "labArrivalTimeReal";
		this.labArrivalTimeReal.Size = new System.Drawing.Size(46, 18);
		this.labArrivalTimeReal.TabIndex = 17;
		this.labArrivalTimeReal.Text = "--:--:--";
		this.labArrivalTimeReal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.ForeColor = System.Drawing.Color.White;
		this.label8.Location = new System.Drawing.Point(5, 111);
		this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(73, 18);
		this.label8.TabIndex = 16;
		this.label8.Text = "(real time)";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labRemainigTime.AutoSize = true;
		this.labRemainigTime.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labRemainigTime.ForeColor = System.Drawing.Color.Lime;
		this.labRemainigTime.Location = new System.Drawing.Point(93, 63);
		this.labRemainigTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labRemainigTime.Name = "labRemainigTime";
		this.labRemainigTime.Size = new System.Drawing.Size(46, 18);
		this.labRemainigTime.TabIndex = 15;
		this.labRemainigTime.Text = "--:--:--";
		this.labRemainigTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labArrivalTime.AutoSize = true;
		this.labArrivalTime.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labArrivalTime.ForeColor = System.Drawing.Color.Lime;
		this.labArrivalTime.Location = new System.Drawing.Point(93, 87);
		this.labArrivalTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labArrivalTime.Name = "labArrivalTime";
		this.labArrivalTime.Size = new System.Drawing.Size(46, 18);
		this.labArrivalTime.TabIndex = 14;
		this.labArrivalTime.Text = "--:--:--";
		this.labArrivalTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.White;
		this.label6.Location = new System.Drawing.Point(6, 63);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(75, 18);
		this.label6.TabIndex = 13;
		this.label6.Text = "remaining:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.White;
		this.label5.Location = new System.Drawing.Point(6, 87);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(66, 18);
		this.label5.TabIndex = 12;
		this.label5.Text = "arrival at:";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.White;
		this.label3.Location = new System.Drawing.Point(6, 39);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(61, 18);
		this.label3.TabIndex = 11;
		this.label3.Text = "elapsed:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labElapsedTime.AutoSize = true;
		this.labElapsedTime.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labElapsedTime.ForeColor = System.Drawing.Color.Lime;
		this.labElapsedTime.Location = new System.Drawing.Point(93, 39);
		this.labElapsedTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labElapsedTime.Name = "labElapsedTime";
		this.labElapsedTime.Size = new System.Drawing.Size(46, 18);
		this.labElapsedTime.TabIndex = 10;
		this.labElapsedTime.Text = "--:--:--";
		this.labElapsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.White;
		this.label2.Location = new System.Drawing.Point(6, 15);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(40, 18);
		this.label2.TabIndex = 9;
		this.label2.Text = "time:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labCurrentTime.AutoSize = true;
		this.labCurrentTime.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labCurrentTime.ForeColor = System.Drawing.Color.Lime;
		this.labCurrentTime.Location = new System.Drawing.Point(93, 15);
		this.labCurrentTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labCurrentTime.Name = "labCurrentTime";
		this.labCurrentTime.Size = new System.Drawing.Size(46, 18);
		this.labCurrentTime.TabIndex = 6;
		this.labCurrentTime.Text = "--:--:--";
		this.labCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
		this.tabControl1.Controls.Add(this.tabNavLog);
		this.tabControl1.Controls.Add(this.tabParameters);
		this.tabControl1.Controls.Add(this.tabPlanning);
		this.tabControl1.Controls.Add(this.tabFuelManagement);
		this.tabControl1.Controls.Add(this.tabForceTrack);
		this.tabControl1.Controls.Add(this.tabPathTrack);
		this.tabControl1.Controls.Add(this.tabStandardManouver);
		this.tabControl1.Controls.Add(this.tabMisc);
		this.tabControl1.Controls.Add(this.tabGraphicLog);
		this.tabControl1.Controls.Add(this.tabLog);
		this.tabControl1.Controls.Add(this.tabInfo);
		this.tabControl1.Controls.Add(this.tabBuddyWorld);
		this.tabControl1.Controls.Add(this.tabActivities);
		this.tabControl1.Controls.Add(this.tabFinance);
		this.tabControl1.Controls.Add(this.tabGoodsTrade);
		this.tabControl1.Location = new System.Drawing.Point(4, 170);
		this.tabControl1.Name = "tabControl1";
		this.tabControl1.SelectedIndex = 0;
		this.tabControl1.Size = new System.Drawing.Size(1151, 421);
		this.tabControl1.TabIndex = 22;
		this.tabControl1.SelectedIndexChanged += new System.EventHandler(tabControl1_SelectedIndexChanged);
		this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(tabControl1_Selected);
		this.tabNavLog.BackColor = System.Drawing.Color.Black;
		this.tabNavLog.Controls.Add(this.grpRunways);
		this.tabNavLog.Controls.Add(this.dataGridView1);
		this.tabNavLog.Controls.Add(this.groupBox3);
		this.tabNavLog.Location = new System.Drawing.Point(4, 27);
		this.tabNavLog.Name = "tabNavLog";
		this.tabNavLog.Padding = new System.Windows.Forms.Padding(3);
		this.tabNavLog.Size = new System.Drawing.Size(1143, 390);
		this.tabNavLog.TabIndex = 0;
		this.tabNavLog.Text = "Nav Log";
		this.grpRunways.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.grpRunways.BackColor = System.Drawing.Color.Black;
		this.grpRunways.Controls.Add(this.btnSearchTakeOffRunway);
		this.grpRunways.Controls.Add(this.btnSearchLandingRunway);
		this.grpRunways.Controls.Add(this.lblLandingRunway);
		this.grpRunways.Controls.Add(this.label60);
		this.grpRunways.Controls.Add(this.lblTakeOffRunway);
		this.grpRunways.Controls.Add(this.LandingRunwayGoogle);
		this.grpRunways.Controls.Add(this.label58);
		this.grpRunways.Controls.Add(this.TakeOffRunwayGoogle);
		this.grpRunways.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpRunways.ForeColor = System.Drawing.Color.White;
		this.grpRunways.Location = new System.Drawing.Point(450, 6);
		this.grpRunways.Name = "grpRunways";
		this.grpRunways.Size = new System.Drawing.Size(687, 122);
		this.grpRunways.TabIndex = 21;
		this.grpRunways.TabStop = false;
		this.grpRunways.Text = "Runways";
		this.btnSearchTakeOffRunway.BackColor = System.Drawing.Color.Gray;
		this.btnSearchTakeOffRunway.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSearchTakeOffRunway.ForeColor = System.Drawing.Color.Black;
		this.btnSearchTakeOffRunway.Image = (System.Drawing.Image)resources.GetObject("btnSearchTakeOffRunway.Image");
		this.btnSearchTakeOffRunway.Location = new System.Drawing.Point(12, 26);
		this.btnSearchTakeOffRunway.Name = "btnSearchTakeOffRunway";
		this.btnSearchTakeOffRunway.Size = new System.Drawing.Size(30, 26);
		this.btnSearchTakeOffRunway.TabIndex = 59;
		this.btnSearchTakeOffRunway.UseVisualStyleBackColor = false;
		this.btnSearchTakeOffRunway.Click += new System.EventHandler(btnSearchTakeOffRunway_Click);
		this.btnSearchLandingRunway.BackColor = System.Drawing.Color.Gray;
		this.btnSearchLandingRunway.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSearchLandingRunway.ForeColor = System.Drawing.Color.Black;
		this.btnSearchLandingRunway.Image = (System.Drawing.Image)resources.GetObject("btnSearchLandingRunway.Image");
		this.btnSearchLandingRunway.Location = new System.Drawing.Point(12, 72);
		this.btnSearchLandingRunway.Name = "btnSearchLandingRunway";
		this.btnSearchLandingRunway.Size = new System.Drawing.Size(30, 26);
		this.btnSearchLandingRunway.TabIndex = 58;
		this.btnSearchLandingRunway.UseVisualStyleBackColor = false;
		this.btnSearchLandingRunway.Click += new System.EventHandler(btnSearchLandingRunway_Click);
		this.lblLandingRunway.AutoSize = true;
		this.lblLandingRunway.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblLandingRunway.ForeColor = System.Drawing.Color.Lime;
		this.lblLandingRunway.Location = new System.Drawing.Point(143, 76);
		this.lblLandingRunway.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblLandingRunway.Name = "lblLandingRunway";
		this.lblLandingRunway.Size = new System.Drawing.Size(28, 18);
		this.lblLandingRunway.TabIndex = 19;
		this.lblLandingRunway.Text = "----";
		this.lblLandingRunway.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label60.AutoSize = true;
		this.label60.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label60.ForeColor = System.Drawing.Color.White;
		this.label60.Location = new System.Drawing.Point(49, 75);
		this.label60.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label60.Name = "label60";
		this.label60.Size = new System.Drawing.Size(60, 18);
		this.label60.TabIndex = 18;
		this.label60.Text = "Landing:";
		this.label60.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblTakeOffRunway.AutoSize = true;
		this.lblTakeOffRunway.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTakeOffRunway.ForeColor = System.Drawing.Color.Lime;
		this.lblTakeOffRunway.Location = new System.Drawing.Point(143, 30);
		this.lblTakeOffRunway.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblTakeOffRunway.Name = "lblTakeOffRunway";
		this.lblTakeOffRunway.Size = new System.Drawing.Size(28, 18);
		this.lblTakeOffRunway.TabIndex = 17;
		this.lblTakeOffRunway.Text = "----";
		this.lblTakeOffRunway.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.LandingRunwayGoogle.BackColor = System.Drawing.Color.Black;
		this.LandingRunwayGoogle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.LandingRunwayGoogle.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.LandingRunwayGoogle.ForeColor = System.Drawing.Color.Black;
		this.LandingRunwayGoogle.Image = NavBuddy.Properties.Resources.googlelink;
		this.LandingRunwayGoogle.Location = new System.Drawing.Point(116, 75);
		this.LandingRunwayGoogle.Name = "LandingRunwayGoogle";
		this.LandingRunwayGoogle.Size = new System.Drawing.Size(20, 20);
		this.LandingRunwayGoogle.TabIndex = 57;
		this.LandingRunwayGoogle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.LandingRunwayGoogle.UseVisualStyleBackColor = false;
		this.LandingRunwayGoogle.Click += new System.EventHandler(LandingRunwayGoogle_Click);
		this.label58.AutoSize = true;
		this.label58.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label58.ForeColor = System.Drawing.Color.White;
		this.label58.Location = new System.Drawing.Point(49, 30);
		this.label58.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label58.Name = "label58";
		this.label58.Size = new System.Drawing.Size(60, 18);
		this.label58.TabIndex = 16;
		this.label58.Text = "Take off:";
		this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TakeOffRunwayGoogle.BackColor = System.Drawing.Color.Black;
		this.TakeOffRunwayGoogle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.TakeOffRunwayGoogle.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.TakeOffRunwayGoogle.ForeColor = System.Drawing.Color.Black;
		this.TakeOffRunwayGoogle.Image = NavBuddy.Properties.Resources.googlelink;
		this.TakeOffRunwayGoogle.Location = new System.Drawing.Point(116, 28);
		this.TakeOffRunwayGoogle.Name = "TakeOffRunwayGoogle";
		this.TakeOffRunwayGoogle.Size = new System.Drawing.Size(20, 20);
		this.TakeOffRunwayGoogle.TabIndex = 55;
		this.TakeOffRunwayGoogle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.TakeOffRunwayGoogle.UseVisualStyleBackColor = false;
		this.TakeOffRunwayGoogle.Click += new System.EventHandler(DepartureRunwayGoogle_Click);
		this.groupBox3.BackColor = System.Drawing.Color.Black;
		this.groupBox3.Controls.Add(this.lblPlanDescription);
		this.groupBox3.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox3.ForeColor = System.Drawing.Color.White;
		this.groupBox3.Location = new System.Drawing.Point(6, 6);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(438, 122);
		this.groupBox3.TabIndex = 20;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Flight Plan";
		this.tabParameters.BackColor = System.Drawing.Color.Black;
		this.tabParameters.Controls.Add(this.grpPerformance);
		this.tabParameters.Location = new System.Drawing.Point(4, 27);
		this.tabParameters.Name = "tabParameters";
		this.tabParameters.Size = new System.Drawing.Size(1143, 390);
		this.tabParameters.TabIndex = 12;
		this.tabParameters.Text = "Performances";
		this.grpPerformance.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.grpPerformance.BackColor = System.Drawing.Color.Black;
		this.grpPerformance.Controls.Add(this.nudLandFPM);
		this.grpPerformance.Controls.Add(this.nudThrottleDamper);
		this.grpPerformance.Controls.Add(this.label90);
		this.grpPerformance.Controls.Add(this.nudThrottleEffect);
		this.grpPerformance.Controls.Add(this.label91);
		this.grpPerformance.Controls.Add(this.nudFlapsLandingPerc);
		this.grpPerformance.Controls.Add(this.label89);
		this.grpPerformance.Controls.Add(this.label88);
		this.grpPerformance.Controls.Add(this.nudFlapsLandingIas);
		this.grpPerformance.Controls.Add(this.label87);
		this.grpPerformance.Controls.Add(this.label86);
		this.grpPerformance.Controls.Add(this.label56);
		this.grpPerformance.Controls.Add(this.nudReverseThrust);
		this.grpPerformance.Controls.Add(this.label53);
		this.grpPerformance.Controls.Add(this.label63);
		this.grpPerformance.Controls.Add(this.nudMaxBankAngle);
		this.grpPerformance.Controls.Add(this.label70);
		this.grpPerformance.Controls.Add(this.panel1);
		this.grpPerformance.Controls.Add(this.label85);
		this.grpPerformance.Controls.Add(this.nudElevatorDamper);
		this.grpPerformance.Controls.Add(this.label71);
		this.grpPerformance.Controls.Add(this.nudElevatorEffect);
		this.grpPerformance.Controls.Add(this.label72);
		this.grpPerformance.Controls.Add(this.nudAileronDamper);
		this.grpPerformance.Controls.Add(this.label66);
		this.grpPerformance.Controls.Add(this.nudAileronEffect);
		this.grpPerformance.Controls.Add(this.label67);
		this.grpPerformance.Controls.Add(this.nudRudderEffect);
		this.grpPerformance.Controls.Add(this.label62);
		this.grpPerformance.Controls.Add(this.nudFlapsTakeOffIas);
		this.grpPerformance.Controls.Add(this.label54);
		this.grpPerformance.Controls.Add(this.nudFlapsTakeOffPerc);
		this.grpPerformance.Controls.Add(this.nudLandingGearDownAGL);
		this.grpPerformance.Controls.Add(this.nudLandingGearUpAGL);
		this.grpPerformance.Controls.Add(this.label35);
		this.grpPerformance.Controls.Add(this.nudTakeOffCompletedAGL);
		this.grpPerformance.Controls.Add(this.label38);
		this.grpPerformance.Controls.Add(this.nudDescFPM);
		this.grpPerformance.Controls.Add(this.label44);
		this.grpPerformance.Controls.Add(this.nudRunwayEntAGL);
		this.grpPerformance.Controls.Add(this.label36);
		this.grpPerformance.Controls.Add(this.nudClimbFPM);
		this.grpPerformance.Controls.Add(this.label19);
		this.grpPerformance.Controls.Add(this.nudDescIas);
		this.grpPerformance.Controls.Add(this.label18);
		this.grpPerformance.Controls.Add(this.nudClimbIas);
		this.grpPerformance.Controls.Add(this.label17);
		this.grpPerformance.Controls.Add(this.label16);
		this.grpPerformance.Controls.Add(this.label23);
		this.grpPerformance.Controls.Add(this.nudCruiseIas);
		this.grpPerformance.Controls.Add(this.label1);
		this.grpPerformance.Controls.Add(this.nudSafeIas);
		this.grpPerformance.Controls.Add(this.label37);
		this.grpPerformance.Controls.Add(this.nudLandingIas);
		this.grpPerformance.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpPerformance.ForeColor = System.Drawing.Color.White;
		this.grpPerformance.Location = new System.Drawing.Point(8, -1);
		this.grpPerformance.Name = "grpPerformance";
		this.grpPerformance.Size = new System.Drawing.Size(1125, 384);
		this.grpPerformance.TabIndex = 25;
		this.grpPerformance.TabStop = false;
		this.grpPerformance.Text = "Performance";
		this.nudLandFPM.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudLandFPM.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudLandFPM.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.nudLandFPM.Location = new System.Drawing.Point(135, 168);
		this.nudLandFPM.Maximum = new decimal(new int[4]);
		this.nudLandFPM.Minimum = new decimal(new int[4] { 1000, 0, 0, -2147483648 });
		this.nudLandFPM.Name = "nudLandFPM";
		this.nudLandFPM.Size = new System.Drawing.Size(55, 23);
		this.nudLandFPM.TabIndex = 72;
		this.nudLandFPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudThrottleDamper.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudThrottleDamper.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudThrottleDamper.Increment = new decimal(new int[4] { 5, 0, 0, 0 });
		this.nudThrottleDamper.Location = new System.Drawing.Point(957, 266);
		this.nudThrottleDamper.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.nudThrottleDamper.Name = "nudThrottleDamper";
		this.nudThrottleDamper.Size = new System.Drawing.Size(47, 23);
		this.nudThrottleDamper.TabIndex = 71;
		this.nudThrottleDamper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudThrottleDamper.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.label90.AutoSize = true;
		this.label90.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label90.ForeColor = System.Drawing.Color.White;
		this.label90.Location = new System.Drawing.Point(827, 268);
		this.label90.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label90.Name = "label90";
		this.label90.Size = new System.Drawing.Size(123, 18);
		this.label90.TabIndex = 70;
		this.label90.Text = "Throttle damper %";
		this.nudThrottleEffect.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudThrottleEffect.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudThrottleEffect.Increment = new decimal(new int[4] { 5, 0, 0, 0 });
		this.nudThrottleEffect.Location = new System.Drawing.Point(957, 235);
		this.nudThrottleEffect.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.nudThrottleEffect.Name = "nudThrottleEffect";
		this.nudThrottleEffect.Size = new System.Drawing.Size(47, 23);
		this.nudThrottleEffect.TabIndex = 69;
		this.nudThrottleEffect.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudThrottleEffect.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.label91.AutoSize = true;
		this.label91.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label91.ForeColor = System.Drawing.Color.White;
		this.label91.Location = new System.Drawing.Point(827, 237);
		this.label91.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label91.Name = "label91";
		this.label91.Size = new System.Drawing.Size(111, 18);
		this.label91.TabIndex = 68;
		this.label91.Text = "Throttle effect %";
		this.nudFlapsLandingPerc.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudFlapsLandingPerc.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudFlapsLandingPerc.Increment = new decimal(new int[4] { 5, 0, 0, 0 });
		this.nudFlapsLandingPerc.Location = new System.Drawing.Point(622, 108);
		this.nudFlapsLandingPerc.Name = "nudFlapsLandingPerc";
		this.nudFlapsLandingPerc.Size = new System.Drawing.Size(47, 23);
		this.nudFlapsLandingPerc.TabIndex = 67;
		this.nudFlapsLandingPerc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudFlapsLandingPerc.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.label89.AutoSize = true;
		this.label89.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label89.ForeColor = System.Drawing.Color.White;
		this.label89.Location = new System.Drawing.Point(635, 50);
		this.label89.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label89.Name = "label89";
		this.label89.Size = new System.Drawing.Size(19, 18);
		this.label89.TabIndex = 66;
		this.label89.Text = "%";
		this.label88.AutoSize = true;
		this.label88.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label88.ForeColor = System.Drawing.Color.White;
		this.label88.Location = new System.Drawing.Point(648, 17);
		this.label88.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label88.Name = "label88";
		this.label88.Size = new System.Drawing.Size(40, 18);
		this.label88.TabIndex = 65;
		this.label88.Text = "Flaps";
		this.nudFlapsLandingIas.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudFlapsLandingIas.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudFlapsLandingIas.Location = new System.Drawing.Point(678, 108);
		this.nudFlapsLandingIas.Maximum = new decimal(new int[4] { 9999, 0, 0, 0 });
		this.nudFlapsLandingIas.Name = "nudFlapsLandingIas";
		this.nudFlapsLandingIas.Size = new System.Drawing.Size(47, 23);
		this.nudFlapsLandingIas.TabIndex = 64;
		this.nudFlapsLandingIas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudFlapsLandingIas.Value = new decimal(new int[4] { 120, 0, 0, 0 });
		this.label87.AutoSize = true;
		this.label87.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label87.ForeColor = System.Drawing.Color.White;
		this.label87.Location = new System.Drawing.Point(542, 112);
		this.label87.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label87.Name = "label87";
		this.label87.Size = new System.Drawing.Size(37, 18);
		this.label87.TabIndex = 63;
		this.label87.Text = "Land";
		this.label86.AutoSize = true;
		this.label86.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label86.ForeColor = System.Drawing.Color.White;
		this.label86.Location = new System.Drawing.Point(686, 50);
		this.label86.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label86.Name = "label86";
		this.label86.Size = new System.Drawing.Size(28, 18);
		this.label86.TabIndex = 62;
		this.label86.Text = "IAS";
		this.label56.AutoSize = true;
		this.label56.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label56.ForeColor = System.Drawing.Color.White;
		this.label56.Location = new System.Drawing.Point(528, 286);
		this.label56.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label56.Name = "label56";
		this.label56.Size = new System.Drawing.Size(19, 18);
		this.label56.TabIndex = 61;
		this.label56.Text = "%";
		this.nudReverseThrust.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudReverseThrust.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudReverseThrust.Location = new System.Drawing.Point(474, 283);
		this.nudReverseThrust.Maximum = new decimal(new int[4]);
		this.nudReverseThrust.Minimum = new decimal(new int[4] { 50, 0, 0, -2147483648 });
		this.nudReverseThrust.Name = "nudReverseThrust";
		this.nudReverseThrust.Size = new System.Drawing.Size(47, 23);
		this.nudReverseThrust.TabIndex = 60;
		this.nudReverseThrust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label53.AutoSize = true;
		this.label53.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label53.ForeColor = System.Drawing.Color.White;
		this.label53.Location = new System.Drawing.Point(313, 286);
		this.label53.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label53.Name = "label53";
		this.label53.Size = new System.Drawing.Size(146, 18);
		this.label53.TabIndex = 59;
		this.label53.Text = "Landing reverse thrust";
		this.label63.AutoSize = true;
		this.label63.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label63.ForeColor = System.Drawing.Color.White;
		this.label63.Location = new System.Drawing.Point(197, 285);
		this.label63.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label63.Name = "label63";
		this.label63.Size = new System.Drawing.Size(13, 18);
		this.label63.TabIndex = 58;
		this.label63.Text = "°";
		this.nudMaxBankAngle.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudMaxBankAngle.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudMaxBankAngle.Location = new System.Drawing.Point(135, 283);
		this.nudMaxBankAngle.Maximum = new decimal(new int[4] { 90, 0, 0, 0 });
		this.nudMaxBankAngle.Name = "nudMaxBankAngle";
		this.nudMaxBankAngle.Size = new System.Drawing.Size(55, 23);
		this.nudMaxBankAngle.TabIndex = 57;
		this.nudMaxBankAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudMaxBankAngle.Value = new decimal(new int[4] { 30, 0, 0, 0 });
		this.label70.AutoSize = true;
		this.label70.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label70.ForeColor = System.Drawing.Color.White;
		this.label70.Location = new System.Drawing.Point(14, 285);
		this.label70.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label70.Name = "label70";
		this.label70.Size = new System.Drawing.Size(104, 18);
		this.label70.TabIndex = 56;
		this.label70.Text = "Max bank angle";
		this.panel1.BackColor = System.Drawing.Color.White;
		this.panel1.Location = new System.Drawing.Point(11, 244);
		this.panel1.Margin = new System.Windows.Forms.Padding(0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(750, 1);
		this.panel1.TabIndex = 55;
		this.label85.AutoSize = true;
		this.label85.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label85.ForeColor = System.Drawing.Color.White;
		this.label85.Location = new System.Drawing.Point(161, 264);
		this.label85.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label85.Name = "label85";
		this.label85.Size = new System.Drawing.Size(29, 15);
		this.label85.TabIndex = 54;
		this.label85.Text = "DEG";
		this.nudElevatorDamper.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudElevatorDamper.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudElevatorDamper.Increment = new decimal(new int[4] { 5, 0, 0, 0 });
		this.nudElevatorDamper.Location = new System.Drawing.Point(957, 193);
		this.nudElevatorDamper.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.nudElevatorDamper.Name = "nudElevatorDamper";
		this.nudElevatorDamper.Size = new System.Drawing.Size(47, 23);
		this.nudElevatorDamper.TabIndex = 50;
		this.nudElevatorDamper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudElevatorDamper.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.label71.AutoSize = true;
		this.label71.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label71.ForeColor = System.Drawing.Color.White;
		this.label71.Location = new System.Drawing.Point(827, 195);
		this.label71.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label71.Name = "label71";
		this.label71.Size = new System.Drawing.Size(124, 18);
		this.label71.TabIndex = 49;
		this.label71.Text = "Elevator damper %";
		this.nudElevatorEffect.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudElevatorEffect.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudElevatorEffect.Increment = new decimal(new int[4] { 5, 0, 0, 0 });
		this.nudElevatorEffect.Location = new System.Drawing.Point(957, 162);
		this.nudElevatorEffect.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.nudElevatorEffect.Name = "nudElevatorEffect";
		this.nudElevatorEffect.Size = new System.Drawing.Size(47, 23);
		this.nudElevatorEffect.TabIndex = 48;
		this.nudElevatorEffect.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudElevatorEffect.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.label72.AutoSize = true;
		this.label72.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label72.ForeColor = System.Drawing.Color.White;
		this.label72.Location = new System.Drawing.Point(827, 164);
		this.label72.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label72.Name = "label72";
		this.label72.Size = new System.Drawing.Size(112, 18);
		this.label72.TabIndex = 47;
		this.label72.Text = "Elevator effect %";
		this.nudAileronDamper.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudAileronDamper.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudAileronDamper.Increment = new decimal(new int[4] { 5, 0, 0, 0 });
		this.nudAileronDamper.Location = new System.Drawing.Point(957, 118);
		this.nudAileronDamper.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.nudAileronDamper.Name = "nudAileronDamper";
		this.nudAileronDamper.Size = new System.Drawing.Size(47, 23);
		this.nudAileronDamper.TabIndex = 45;
		this.nudAileronDamper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudAileronDamper.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.label66.AutoSize = true;
		this.label66.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label66.ForeColor = System.Drawing.Color.White;
		this.label66.Location = new System.Drawing.Point(827, 120);
		this.label66.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label66.Name = "label66";
		this.label66.Size = new System.Drawing.Size(119, 18);
		this.label66.TabIndex = 44;
		this.label66.Text = "Aileron damper %";
		this.nudAileronEffect.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudAileronEffect.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudAileronEffect.Increment = new decimal(new int[4] { 5, 0, 0, 0 });
		this.nudAileronEffect.Location = new System.Drawing.Point(957, 87);
		this.nudAileronEffect.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.nudAileronEffect.Name = "nudAileronEffect";
		this.nudAileronEffect.Size = new System.Drawing.Size(47, 23);
		this.nudAileronEffect.TabIndex = 43;
		this.nudAileronEffect.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudAileronEffect.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.label67.AutoSize = true;
		this.label67.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label67.ForeColor = System.Drawing.Color.White;
		this.label67.Location = new System.Drawing.Point(827, 89);
		this.label67.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label67.Name = "label67";
		this.label67.Size = new System.Drawing.Size(107, 18);
		this.label67.TabIndex = 42;
		this.label67.Text = "Aileron effect %";
		this.nudRudderEffect.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudRudderEffect.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudRudderEffect.Increment = new decimal(new int[4] { 5, 0, 0, 0 });
		this.nudRudderEffect.Location = new System.Drawing.Point(957, 45);
		this.nudRudderEffect.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.nudRudderEffect.Name = "nudRudderEffect";
		this.nudRudderEffect.Size = new System.Drawing.Size(47, 23);
		this.nudRudderEffect.TabIndex = 39;
		this.nudRudderEffect.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudRudderEffect.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.label62.AutoSize = true;
		this.label62.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label62.ForeColor = System.Drawing.Color.White;
		this.label62.Location = new System.Drawing.Point(827, 47);
		this.label62.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label62.Name = "label62";
		this.label62.Size = new System.Drawing.Size(106, 18);
		this.label62.TabIndex = 38;
		this.label62.Text = "Rudder effect %";
		this.nudFlapsTakeOffIas.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudFlapsTakeOffIas.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudFlapsTakeOffIas.Location = new System.Drawing.Point(678, 76);
		this.nudFlapsTakeOffIas.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.nudFlapsTakeOffIas.Name = "nudFlapsTakeOffIas";
		this.nudFlapsTakeOffIas.Size = new System.Drawing.Size(47, 23);
		this.nudFlapsTakeOffIas.TabIndex = 37;
		this.nudFlapsTakeOffIas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudFlapsTakeOffIas.Value = new decimal(new int[4] { 90, 0, 0, 0 });
		this.label54.AutoSize = true;
		this.label54.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label54.ForeColor = System.Drawing.Color.White;
		this.label54.Location = new System.Drawing.Point(542, 77);
		this.label54.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label54.Name = "label54";
		this.label54.Size = new System.Drawing.Size(58, 18);
		this.label54.TabIndex = 36;
		this.label54.Text = "Take Off";
		this.nudFlapsTakeOffPerc.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudFlapsTakeOffPerc.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudFlapsTakeOffPerc.Increment = new decimal(new int[4] { 5, 0, 0, 0 });
		this.nudFlapsTakeOffPerc.Location = new System.Drawing.Point(622, 77);
		this.nudFlapsTakeOffPerc.Name = "nudFlapsTakeOffPerc";
		this.nudFlapsTakeOffPerc.Size = new System.Drawing.Size(47, 23);
		this.nudFlapsTakeOffPerc.TabIndex = 35;
		this.nudFlapsTakeOffPerc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudFlapsTakeOffPerc.Value = new decimal(new int[4] { 25, 0, 0, 0 });
		this.nudLandingGearDownAGL.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudLandingGearDownAGL.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudLandingGearDownAGL.Increment = new decimal(new int[4] { 50, 0, 0, 0 });
		this.nudLandingGearDownAGL.Location = new System.Drawing.Point(466, 43);
		this.nudLandingGearDownAGL.Maximum = new decimal(new int[4] { 9999, 0, 0, 0 });
		this.nudLandingGearDownAGL.Name = "nudLandingGearDownAGL";
		this.nudLandingGearDownAGL.Size = new System.Drawing.Size(47, 23);
		this.nudLandingGearDownAGL.TabIndex = 33;
		this.nudLandingGearDownAGL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudLandingGearDownAGL.Value = new decimal(new int[4] { 500, 0, 0, 0 });
		this.nudLandingGearUpAGL.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudLandingGearUpAGL.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudLandingGearUpAGL.Increment = new decimal(new int[4] { 50, 0, 0, 0 });
		this.nudLandingGearUpAGL.Location = new System.Drawing.Point(397, 43);
		this.nudLandingGearUpAGL.Maximum = new decimal(new int[4] { 9999, 0, 0, 0 });
		this.nudLandingGearUpAGL.Name = "nudLandingGearUpAGL";
		this.nudLandingGearUpAGL.Size = new System.Drawing.Size(47, 23);
		this.nudLandingGearUpAGL.TabIndex = 31;
		this.nudLandingGearUpAGL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudLandingGearUpAGL.Value = new decimal(new int[4] { 500, 0, 0, 0 });
		this.label35.AutoSize = true;
		this.label35.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label35.ForeColor = System.Drawing.Color.White;
		this.label35.Location = new System.Drawing.Point(242, 45);
		this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(150, 18);
		this.label35.TabIndex = 30;
		this.label35.Text = "Landing Gear Up/Down";
		this.nudTakeOffCompletedAGL.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudTakeOffCompletedAGL.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudTakeOffCompletedAGL.Increment = new decimal(new int[4] { 50, 0, 0, 0 });
		this.nudTakeOffCompletedAGL.Location = new System.Drawing.Point(397, 76);
		this.nudTakeOffCompletedAGL.Maximum = new decimal(new int[4] { 9999, 0, 0, 0 });
		this.nudTakeOffCompletedAGL.Name = "nudTakeOffCompletedAGL";
		this.nudTakeOffCompletedAGL.Size = new System.Drawing.Size(47, 23);
		this.nudTakeOffCompletedAGL.TabIndex = 24;
		this.nudTakeOffCompletedAGL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudTakeOffCompletedAGL.Value = new decimal(new int[4] { 500, 0, 0, 0 });
		this.label38.AutoSize = true;
		this.label38.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label38.ForeColor = System.Drawing.Color.White;
		this.label38.Location = new System.Drawing.Point(243, 79);
		this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label38.Name = "label38";
		this.label38.Size = new System.Drawing.Size(126, 18);
		this.label38.TabIndex = 23;
		this.label38.Text = "Take off completed";
		this.nudDescFPM.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudDescFPM.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudDescFPM.Location = new System.Drawing.Point(135, 136);
		this.nudDescFPM.Maximum = new decimal(new int[4] { 100, 0, 0, -2147483648 });
		this.nudDescFPM.Minimum = new decimal(new int[4] { 5000, 0, 0, -2147483648 });
		this.nudDescFPM.Name = "nudDescFPM";
		this.nudDescFPM.Size = new System.Drawing.Size(55, 23);
		this.nudDescFPM.TabIndex = 14;
		this.nudDescFPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudDescFPM.Value = new decimal(new int[4] { 500, 0, 0, -2147483648 });
		this.label44.AutoSize = true;
		this.label44.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label44.ForeColor = System.Drawing.Color.White;
		this.label44.Location = new System.Drawing.Point(429, 19);
		this.label44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label44.Name = "label44";
		this.label44.Size = new System.Drawing.Size(50, 15);
		this.label44.TabIndex = 22;
		this.label44.Text = "feet AGL";
		this.nudRunwayEntAGL.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudRunwayEntAGL.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudRunwayEntAGL.Location = new System.Drawing.Point(397, 111);
		this.nudRunwayEntAGL.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.nudRunwayEntAGL.Name = "nudRunwayEntAGL";
		this.nudRunwayEntAGL.Size = new System.Drawing.Size(47, 23);
		this.nudRunwayEntAGL.TabIndex = 21;
		this.nudRunwayEntAGL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudRunwayEntAGL.Value = new decimal(new int[4] { 50, 0, 0, 0 });
		this.label36.AutoSize = true;
		this.label36.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label36.ForeColor = System.Drawing.Color.White;
		this.label36.Location = new System.Drawing.Point(242, 113);
		this.label36.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(115, 18);
		this.label36.TabIndex = 20;
		this.label36.Text = "Runway entrance";
		this.nudClimbFPM.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudClimbFPM.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudClimbFPM.Location = new System.Drawing.Point(135, 74);
		this.nudClimbFPM.Maximum = new decimal(new int[4] { 5000, 0, 0, 0 });
		this.nudClimbFPM.Minimum = new decimal(new int[4] { 100, 0, 0, 0 });
		this.nudClimbFPM.Name = "nudClimbFPM";
		this.nudClimbFPM.Size = new System.Drawing.Size(55, 23);
		this.nudClimbFPM.TabIndex = 13;
		this.nudClimbFPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudClimbFPM.Value = new decimal(new int[4] { 500, 0, 0, 0 });
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.ForeColor = System.Drawing.Color.White;
		this.label19.Location = new System.Drawing.Point(146, 23);
		this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(35, 18);
		this.label19.TabIndex = 11;
		this.label19.Text = "FPM";
		this.nudDescIas.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudDescIas.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudDescIas.Location = new System.Drawing.Point(72, 136);
		this.nudDescIas.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.nudDescIas.Name = "nudDescIas";
		this.nudDescIas.Size = new System.Drawing.Size(44, 23);
		this.nudDescIas.TabIndex = 9;
		this.nudDescIas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudDescIas.Value = new decimal(new int[4] { 80, 0, 0, 0 });
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label18.ForeColor = System.Drawing.Color.White;
		this.label18.Location = new System.Drawing.Point(13, 138);
		this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(41, 18);
		this.label18.TabIndex = 10;
		this.label18.Text = "Desc.";
		this.nudClimbIas.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudClimbIas.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudClimbIas.Location = new System.Drawing.Point(72, 74);
		this.nudClimbIas.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.nudClimbIas.Name = "nudClimbIas";
		this.nudClimbIas.Size = new System.Drawing.Size(44, 23);
		this.nudClimbIas.TabIndex = 7;
		this.nudClimbIas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudClimbIas.Value = new decimal(new int[4] { 75, 0, 0, 0 });
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label17.ForeColor = System.Drawing.Color.White;
		this.label17.Location = new System.Drawing.Point(13, 107);
		this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(47, 18);
		this.label17.TabIndex = 8;
		this.label17.Text = "Cruise";
		this.label16.AutoSize = true;
		this.label16.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label16.ForeColor = System.Drawing.Color.White;
		this.label16.Location = new System.Drawing.Point(80, 22);
		this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(28, 18);
		this.label16.TabIndex = 6;
		this.label16.Text = "IAS";
		this.label23.AutoSize = true;
		this.label23.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label23.ForeColor = System.Drawing.Color.White;
		this.label23.Location = new System.Drawing.Point(13, 45);
		this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(49, 18);
		this.label23.TabIndex = 15;
		this.label23.Text = "Rotate";
		this.nudCruiseIas.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudCruiseIas.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudCruiseIas.Location = new System.Drawing.Point(72, 105);
		this.nudCruiseIas.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.nudCruiseIas.Name = "nudCruiseIas";
		this.nudCruiseIas.Size = new System.Drawing.Size(44, 23);
		this.nudCruiseIas.TabIndex = 4;
		this.nudCruiseIas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudCruiseIas.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(13, 76);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(44, 18);
		this.label1.TabIndex = 5;
		this.label1.Text = "Climb";
		this.nudSafeIas.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudSafeIas.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudSafeIas.Location = new System.Drawing.Point(73, 43);
		this.nudSafeIas.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.nudSafeIas.Name = "nudSafeIas";
		this.nudSafeIas.Size = new System.Drawing.Size(44, 23);
		this.nudSafeIas.TabIndex = 16;
		this.nudSafeIas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudSafeIas.Value = new decimal(new int[4] { 55, 0, 0, 0 });
		this.label37.AutoSize = true;
		this.label37.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label37.ForeColor = System.Drawing.Color.White;
		this.label37.Location = new System.Drawing.Point(14, 170);
		this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(56, 18);
		this.label37.TabIndex = 18;
		this.label37.Text = "Landing";
		this.nudLandingIas.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudLandingIas.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudLandingIas.Location = new System.Drawing.Point(73, 168);
		this.nudLandingIas.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.nudLandingIas.Name = "nudLandingIas";
		this.nudLandingIas.Size = new System.Drawing.Size(44, 23);
		this.nudLandingIas.TabIndex = 19;
		this.nudLandingIas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudLandingIas.Value = new decimal(new int[4] { 75, 0, 0, 0 });
		this.nudLandingIas.ValueChanged += new System.EventHandler(nudLandingIas_ValueChanged);
		this.tabPlanning.BackColor = System.Drawing.Color.Black;
		this.tabPlanning.Controls.Add(this.label80);
		this.tabPlanning.Controls.Add(this.label79);
		this.tabPlanning.Controls.Add(this.label78);
		this.tabPlanning.Controls.Add(this.labPlannedApproachDescentRate);
		this.tabPlanning.Controls.Add(this.labPlanningDistance);
		this.tabPlanning.Controls.Add(this.label69);
		this.tabPlanning.Controls.Add(this.label68);
		this.tabPlanning.Controls.Add(this.groupBox4);
		this.tabPlanning.Controls.Add(this.label65);
		this.tabPlanning.Controls.Add(this.label64);
		this.tabPlanning.Controls.Add(this.label50);
		this.tabPlanning.Controls.Add(this.labDestinationElevation);
		this.tabPlanning.Controls.Add(this.labDepartureElevation);
		this.tabPlanning.Controls.Add(this.labDestinationDescription);
		this.tabPlanning.Controls.Add(this.labDepartureDescription);
		this.tabPlanning.Controls.Add(this.labArrivalRunway);
		this.tabPlanning.Controls.Add(this.labDepartureRunway);
		this.tabPlanning.Controls.Add(this.label59);
		this.tabPlanning.Controls.Add(this.labPlannedApproachSlope);
		this.tabPlanning.Controls.Add(this.nudPlannedApproachAltitude);
		this.tabPlanning.Controls.Add(this.label57);
		this.tabPlanning.Controls.Add(this.nudPlannedApproachDistance);
		this.tabPlanning.Controls.Add(this.label55);
		this.tabPlanning.Controls.Add(this.nudPlannedCruiseAltitude);
		this.tabPlanning.Controls.Add(this.label33);
		this.tabPlanning.Controls.Add(this.btnCreateFlightPlan);
		this.tabPlanning.Controls.Add(this.label32);
		this.tabPlanning.Controls.Add(this.label31);
		this.tabPlanning.Controls.Add(this.label28);
		this.tabPlanning.Controls.Add(this.txtDestinationIcao);
		this.tabPlanning.Controls.Add(this.txtDestinationSessagesimal);
		this.tabPlanning.Controls.Add(this.label27);
		this.tabPlanning.Controls.Add(this.txtDepartureIcao);
		this.tabPlanning.Controls.Add(this.label51);
		this.tabPlanning.Controls.Add(this.txtDepartureSessagesimal);
		this.tabPlanning.Controls.Add(this.btnLoadOurAirportAsDeparture);
		this.tabPlanning.Controls.Add(this.btnLoadOurAirportAsDestination);
		this.tabPlanning.Controls.Add(this.btnLoadFseAirportAsDeparture);
		this.tabPlanning.Controls.Add(this.btnSearchRunwayDeparture);
		this.tabPlanning.Controls.Add(this.btnSearchRunwayArrival);
		this.tabPlanning.Controls.Add(this.btnLoadFseAirportAsDestination);
		this.tabPlanning.Controls.Add(this.DestinationGoogle);
		this.tabPlanning.Controls.Add(this.DepartureGoogle);
		this.tabPlanning.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.tabPlanning.ForeColor = System.Drawing.Color.Gray;
		this.tabPlanning.Location = new System.Drawing.Point(4, 27);
		this.tabPlanning.Name = "tabPlanning";
		this.tabPlanning.Size = new System.Drawing.Size(1143, 390);
		this.tabPlanning.TabIndex = 11;
		this.tabPlanning.Text = "Planning";
		this.label80.AutoSize = true;
		this.label80.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label80.ForeColor = System.Drawing.Color.White;
		this.label80.Location = new System.Drawing.Point(251, 227);
		this.label80.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label80.Name = "label80";
		this.label80.Size = new System.Drawing.Size(104, 13);
		this.label80.TabIndex = 86;
		this.label80.Text = "(above runway start)";
		this.label80.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label79.AutoSize = true;
		this.label79.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label79.ForeColor = System.Drawing.Color.White;
		this.label79.Location = new System.Drawing.Point(125, 227);
		this.label79.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label79.Name = "label79";
		this.label79.Size = new System.Drawing.Size(97, 13);
		this.label79.TabIndex = 85;
		this.label79.Text = "(from runway start)";
		this.label79.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label78.AutoSize = true;
		this.label78.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label78.ForeColor = System.Drawing.Color.White;
		this.label78.Location = new System.Drawing.Point(505, 203);
		this.label78.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label78.Name = "label78";
		this.label78.Size = new System.Drawing.Size(37, 18);
		this.label78.TabIndex = 84;
		this.label78.Text = "fpm:";
		this.label78.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labPlannedApproachDescentRate.AutoSize = true;
		this.labPlannedApproachDescentRate.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labPlannedApproachDescentRate.ForeColor = System.Drawing.Color.Lime;
		this.labPlannedApproachDescentRate.Location = new System.Drawing.Point(556, 203);
		this.labPlannedApproachDescentRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labPlannedApproachDescentRate.Name = "labPlannedApproachDescentRate";
		this.labPlannedApproachDescentRate.Size = new System.Drawing.Size(23, 18);
		this.labPlannedApproachDescentRate.TabIndex = 83;
		this.labPlannedApproachDescentRate.Text = "---";
		this.labPlannedApproachDescentRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labPlanningDistance.AutoSize = true;
		this.labPlanningDistance.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labPlanningDistance.ForeColor = System.Drawing.Color.Lime;
		this.labPlanningDistance.Location = new System.Drawing.Point(125, 120);
		this.labPlanningDistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labPlanningDistance.Name = "labPlanningDistance";
		this.labPlanningDistance.Size = new System.Drawing.Size(33, 18);
		this.labPlanningDistance.TabIndex = 82;
		this.labPlanningDistance.Text = "-----";
		this.labPlanningDistance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label69.AutoSize = true;
		this.label69.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label69.ForeColor = System.Drawing.Color.White;
		this.label69.Location = new System.Drawing.Point(199, 158);
		this.label69.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label69.Name = "label69";
		this.label69.Size = new System.Drawing.Size(34, 18);
		this.label69.TabIndex = 81;
		this.label69.Text = "feet";
		this.label69.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label68.AutoSize = true;
		this.label68.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label68.ForeColor = System.Drawing.Color.White;
		this.label68.Location = new System.Drawing.Point(16, 120);
		this.label68.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label68.Name = "label68";
		this.label68.Size = new System.Drawing.Size(65, 18);
		this.label68.TabIndex = 80;
		this.label68.Text = "Distance:";
		this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.groupBox4.Controls.Add(this.btnTeleportToRunway);
		this.groupBox4.Controls.Add(this.label39);
		this.groupBox4.Controls.Add(this.nudAddFeetTeleport);
		this.groupBox4.ForeColor = System.Drawing.Color.White;
		this.groupBox4.Location = new System.Drawing.Point(710, 248);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(317, 65);
		this.groupBox4.TabIndex = 79;
		this.groupBox4.TabStop = false;
		this.btnTeleportToRunway.BackColor = System.Drawing.Color.Lime;
		this.btnTeleportToRunway.ForeColor = System.Drawing.Color.Black;
		this.btnTeleportToRunway.Location = new System.Drawing.Point(11, 23);
		this.btnTeleportToRunway.Name = "btnTeleportToRunway";
		this.btnTeleportToRunway.Size = new System.Drawing.Size(192, 26);
		this.btnTeleportToRunway.TabIndex = 31;
		this.btnTeleportToRunway.Text = "Teleport to TakeOff";
		this.btnTeleportToRunway.UseVisualStyleBackColor = false;
		this.btnTeleportToRunway.Click += new System.EventHandler(btnTeleportToTakeoffRunway_Click);
		this.label39.AutoSize = true;
		this.label39.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label39.ForeColor = System.Drawing.Color.White;
		this.label39.Location = new System.Drawing.Point(269, 27);
		this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label39.Name = "label39";
		this.label39.Size = new System.Drawing.Size(34, 18);
		this.label39.TabIndex = 33;
		this.label39.Text = "feet";
		this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.nudAddFeetTeleport.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudAddFeetTeleport.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudAddFeetTeleport.Location = new System.Drawing.Point(213, 25);
		this.nudAddFeetTeleport.Minimum = new decimal(new int[4] { 100, 0, 0, -2147483648 });
		this.nudAddFeetTeleport.Name = "nudAddFeetTeleport";
		this.nudAddFeetTeleport.Size = new System.Drawing.Size(42, 23);
		this.nudAddFeetTeleport.TabIndex = 32;
		this.nudAddFeetTeleport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudAddFeetTeleport.Value = new decimal(new int[4] { 10, 0, 0, 0 });
		this.label65.AutoSize = true;
		this.label65.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label65.ForeColor = System.Drawing.Color.White;
		this.label65.Location = new System.Drawing.Point(393, 203);
		this.label65.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label65.Name = "label65";
		this.label65.Size = new System.Drawing.Size(46, 18);
		this.label65.TabIndex = 78;
		this.label65.Text = "slope:";
		this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label64.AutoSize = true;
		this.label64.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label64.ForeColor = System.Drawing.Color.White;
		this.label64.Location = new System.Drawing.Point(325, 203);
		this.label64.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label64.Name = "label64";
		this.label64.Size = new System.Drawing.Size(34, 18);
		this.label64.TabIndex = 77;
		this.label64.Text = "feet";
		this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label50.AutoSize = true;
		this.label50.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label50.ForeColor = System.Drawing.Color.White;
		this.label50.Location = new System.Drawing.Point(199, 203);
		this.label50.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label50.Name = "label50";
		this.label50.Size = new System.Drawing.Size(28, 18);
		this.label50.TabIndex = 76;
		this.label50.Text = "nm";
		this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labDestinationElevation.AutoSize = true;
		this.labDestinationElevation.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labDestinationElevation.ForeColor = System.Drawing.Color.Lime;
		this.labDestinationElevation.Location = new System.Drawing.Point(646, 80);
		this.labDestinationElevation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labDestinationElevation.Name = "labDestinationElevation";
		this.labDestinationElevation.Size = new System.Drawing.Size(33, 18);
		this.labDestinationElevation.TabIndex = 73;
		this.labDestinationElevation.Text = "-----";
		this.labDestinationElevation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labDepartureElevation.AutoSize = true;
		this.labDepartureElevation.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labDepartureElevation.ForeColor = System.Drawing.Color.Lime;
		this.labDepartureElevation.Location = new System.Drawing.Point(646, 48);
		this.labDepartureElevation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labDepartureElevation.Name = "labDepartureElevation";
		this.labDepartureElevation.Size = new System.Drawing.Size(33, 18);
		this.labDepartureElevation.TabIndex = 72;
		this.labDepartureElevation.Text = "-----";
		this.labDepartureElevation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labDestinationDescription.AutoSize = true;
		this.labDestinationDescription.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labDestinationDescription.ForeColor = System.Drawing.Color.Lime;
		this.labDestinationDescription.Location = new System.Drawing.Point(384, 81);
		this.labDestinationDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labDestinationDescription.Name = "labDestinationDescription";
		this.labDestinationDescription.Size = new System.Drawing.Size(33, 18);
		this.labDestinationDescription.TabIndex = 71;
		this.labDestinationDescription.Text = "-----";
		this.labDestinationDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labDepartureDescription.AutoSize = true;
		this.labDepartureDescription.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labDepartureDescription.ForeColor = System.Drawing.Color.Lime;
		this.labDepartureDescription.Location = new System.Drawing.Point(384, 48);
		this.labDepartureDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labDepartureDescription.Name = "labDepartureDescription";
		this.labDepartureDescription.Size = new System.Drawing.Size(33, 18);
		this.labDepartureDescription.TabIndex = 70;
		this.labDepartureDescription.Text = "-----";
		this.labDepartureDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labArrivalRunway.AutoSize = true;
		this.labArrivalRunway.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labArrivalRunway.ForeColor = System.Drawing.Color.Lime;
		this.labArrivalRunway.Location = new System.Drawing.Point(973, 84);
		this.labArrivalRunway.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labArrivalRunway.Name = "labArrivalRunway";
		this.labArrivalRunway.Size = new System.Drawing.Size(33, 18);
		this.labArrivalRunway.TabIndex = 69;
		this.labArrivalRunway.Text = "-----";
		this.labArrivalRunway.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labDepartureRunway.AutoSize = true;
		this.labDepartureRunway.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labDepartureRunway.ForeColor = System.Drawing.Color.Lime;
		this.labDepartureRunway.Location = new System.Drawing.Point(973, 48);
		this.labDepartureRunway.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labDepartureRunway.Name = "labDepartureRunway";
		this.labDepartureRunway.Size = new System.Drawing.Size(33, 18);
		this.labDepartureRunway.TabIndex = 68;
		this.labDepartureRunway.Text = "-----";
		this.labDepartureRunway.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label59.AutoSize = true;
		this.label59.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label59.ForeColor = System.Drawing.Color.White;
		this.label59.Location = new System.Drawing.Point(937, 19);
		this.label59.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label59.Name = "label59";
		this.label59.Size = new System.Drawing.Size(67, 18);
		this.label59.TabIndex = 67;
		this.label59.Text = "Runways:";
		this.label59.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labPlannedApproachSlope.AutoSize = true;
		this.labPlannedApproachSlope.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labPlannedApproachSlope.ForeColor = System.Drawing.Color.Lime;
		this.labPlannedApproachSlope.Location = new System.Drawing.Point(444, 203);
		this.labPlannedApproachSlope.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labPlannedApproachSlope.Name = "labPlannedApproachSlope";
		this.labPlannedApproachSlope.Size = new System.Drawing.Size(23, 18);
		this.labPlannedApproachSlope.TabIndex = 65;
		this.labPlannedApproachSlope.Text = "---";
		this.labPlannedApproachSlope.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.nudPlannedApproachAltitude.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudPlannedApproachAltitude.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudPlannedApproachAltitude.Increment = new decimal(new int[4] { 100, 0, 0, 0 });
		this.nudPlannedApproachAltitude.Location = new System.Drawing.Point(254, 201);
		this.nudPlannedApproachAltitude.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.nudPlannedApproachAltitude.Minimum = new decimal(new int[4] { 100, 0, 0, 0 });
		this.nudPlannedApproachAltitude.Name = "nudPlannedApproachAltitude";
		this.nudPlannedApproachAltitude.Size = new System.Drawing.Size(65, 23);
		this.nudPlannedApproachAltitude.TabIndex = 64;
		this.nudPlannedApproachAltitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudPlannedApproachAltitude.Value = new decimal(new int[4] { 2500, 0, 0, 0 });
		this.nudPlannedApproachAltitude.ValueChanged += new System.EventHandler(nudPlannedApproachAltitude_ValueChanged);
		this.label57.AutoSize = true;
		this.label57.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label57.ForeColor = System.Drawing.Color.White;
		this.label57.Location = new System.Drawing.Point(16, 203);
		this.label57.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label57.Name = "label57";
		this.label57.Size = new System.Drawing.Size(71, 18);
		this.label57.TabIndex = 63;
		this.label57.Text = "Approach:";
		this.label57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.nudPlannedApproachDistance.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudPlannedApproachDistance.DecimalPlaces = 1;
		this.nudPlannedApproachDistance.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudPlannedApproachDistance.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.nudPlannedApproachDistance.Location = new System.Drawing.Point(128, 201);
		this.nudPlannedApproachDistance.Minimum = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.nudPlannedApproachDistance.Name = "nudPlannedApproachDistance";
		this.nudPlannedApproachDistance.Size = new System.Drawing.Size(65, 23);
		this.nudPlannedApproachDistance.TabIndex = 62;
		this.nudPlannedApproachDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudPlannedApproachDistance.Value = new decimal(new int[4] { 7, 0, 0, 0 });
		this.nudPlannedApproachDistance.ValueChanged += new System.EventHandler(nudPlannedApproachDistance_ValueChanged);
		this.label55.AutoSize = true;
		this.label55.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label55.ForeColor = System.Drawing.Color.White;
		this.label55.Location = new System.Drawing.Point(16, 158);
		this.label55.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label55.Name = "label55";
		this.label55.Size = new System.Drawing.Size(102, 18);
		this.label55.TabIndex = 61;
		this.label55.Text = "Cruise altitude:";
		this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.nudPlannedCruiseAltitude.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudPlannedCruiseAltitude.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudPlannedCruiseAltitude.Increment = new decimal(new int[4] { 500, 0, 0, 0 });
		this.nudPlannedCruiseAltitude.Location = new System.Drawing.Point(128, 156);
		this.nudPlannedCruiseAltitude.Maximum = new decimal(new int[4] { 50000, 0, 0, 0 });
		this.nudPlannedCruiseAltitude.Minimum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.nudPlannedCruiseAltitude.Name = "nudPlannedCruiseAltitude";
		this.nudPlannedCruiseAltitude.Size = new System.Drawing.Size(65, 23);
		this.nudPlannedCruiseAltitude.TabIndex = 59;
		this.nudPlannedCruiseAltitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudPlannedCruiseAltitude.Value = new decimal(new int[4] { 5000, 0, 0, 0 });
		this.label33.AutoSize = true;
		this.label33.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label33.ForeColor = System.Drawing.Color.White;
		this.label33.Location = new System.Drawing.Point(641, 19);
		this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(38, 18);
		this.label33.TabIndex = 54;
		this.label33.Text = "Elev:";
		this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCreateFlightPlan.BackColor = System.Drawing.SystemColors.ButtonFace;
		this.btnCreateFlightPlan.ForeColor = System.Drawing.Color.Black;
		this.btnCreateFlightPlan.Location = new System.Drawing.Point(127, 260);
		this.btnCreateFlightPlan.Name = "btnCreateFlightPlan";
		this.btnCreateFlightPlan.Size = new System.Drawing.Size(192, 26);
		this.btnCreateFlightPlan.TabIndex = 51;
		this.btnCreateFlightPlan.Text = "CREATE FLIGHT PLAN";
		this.btnCreateFlightPlan.UseVisualStyleBackColor = false;
		this.btnCreateFlightPlan.Click += new System.EventHandler(btnCreateFlightPlan_Click);
		this.label32.AutoSize = true;
		this.label32.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label32.ForeColor = System.Drawing.Color.White;
		this.label32.Location = new System.Drawing.Point(133, 20);
		this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(39, 18);
		this.label32.TabIndex = 45;
		this.label32.Text = "ICAO";
		this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label31.AutoSize = true;
		this.label31.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label31.ForeColor = System.Drawing.Color.White;
		this.label31.Location = new System.Drawing.Point(384, 20);
		this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(82, 18);
		this.label31.TabIndex = 44;
		this.label31.Text = "Description:";
		this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label28.AutoSize = true;
		this.label28.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label28.ForeColor = System.Drawing.Color.White;
		this.label28.Location = new System.Drawing.Point(16, 82);
		this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(53, 18);
		this.label28.TabIndex = 42;
		this.label28.Text = "Arrival:";
		this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtDestinationIcao.BackColor = System.Drawing.Color.White;
		this.txtDestinationIcao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtDestinationIcao.ForeColor = System.Drawing.Color.Black;
		this.txtDestinationIcao.Location = new System.Drawing.Point(128, 80);
		this.txtDestinationIcao.Name = "txtDestinationIcao";
		this.txtDestinationIcao.Size = new System.Drawing.Size(65, 26);
		this.txtDestinationIcao.TabIndex = 41;
		this.txtDestinationSessagesimal.Location = new System.Drawing.Point(710, 80);
		this.txtDestinationSessagesimal.Name = "txtDestinationSessagesimal";
		this.txtDestinationSessagesimal.ReadOnly = true;
		this.txtDestinationSessagesimal.Size = new System.Drawing.Size(180, 26);
		this.txtDestinationSessagesimal.TabIndex = 38;
		this.label27.AutoSize = true;
		this.label27.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label27.ForeColor = System.Drawing.Color.White;
		this.label27.Location = new System.Drawing.Point(16, 46);
		this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(75, 18);
		this.label27.TabIndex = 36;
		this.label27.Text = "Departure:";
		this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtDepartureIcao.BackColor = System.Drawing.Color.White;
		this.txtDepartureIcao.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDepartureIcao.ForeColor = System.Drawing.Color.Black;
		this.txtDepartureIcao.Location = new System.Drawing.Point(128, 44);
		this.txtDepartureIcao.Margin = new System.Windows.Forms.Padding(0);
		this.txtDepartureIcao.Name = "txtDepartureIcao";
		this.txtDepartureIcao.Size = new System.Drawing.Size(65, 26);
		this.txtDepartureIcao.TabIndex = 35;
		this.label51.AutoSize = true;
		this.label51.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label51.ForeColor = System.Drawing.Color.White;
		this.label51.Location = new System.Drawing.Point(707, 20);
		this.label51.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label51.Name = "label51";
		this.label51.Size = new System.Drawing.Size(140, 18);
		this.label51.TabIndex = 19;
		this.label51.Text = "Sessagesimal Coords:";
		this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtDepartureSessagesimal.Location = new System.Drawing.Point(710, 44);
		this.txtDepartureSessagesimal.Name = "txtDepartureSessagesimal";
		this.txtDepartureSessagesimal.ReadOnly = true;
		this.txtDepartureSessagesimal.Size = new System.Drawing.Size(180, 26);
		this.txtDepartureSessagesimal.TabIndex = 18;
		this.btnLoadOurAirportAsDeparture.BackColor = System.Drawing.Color.Gray;
		this.btnLoadOurAirportAsDeparture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnLoadOurAirportAsDeparture.ForeColor = System.Drawing.Color.Black;
		this.btnLoadOurAirportAsDeparture.Image = (System.Drawing.Image)resources.GetObject("btnLoadOurAirportAsDeparture.Image");
		this.btnLoadOurAirportAsDeparture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnLoadOurAirportAsDeparture.Location = new System.Drawing.Point(277, 44);
		this.btnLoadOurAirportAsDeparture.Name = "btnLoadOurAirportAsDeparture";
		this.btnLoadOurAirportAsDeparture.Size = new System.Drawing.Size(42, 26);
		this.btnLoadOurAirportAsDeparture.TabIndex = 75;
		this.btnLoadOurAirportAsDeparture.Text = "?";
		this.btnLoadOurAirportAsDeparture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnLoadOurAirportAsDeparture.UseVisualStyleBackColor = false;
		this.btnLoadOurAirportAsDeparture.Click += new System.EventHandler(btnLoadOurAirportAsDeparture_Click);
		this.btnLoadOurAirportAsDestination.BackColor = System.Drawing.Color.Gray;
		this.btnLoadOurAirportAsDestination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnLoadOurAirportAsDestination.ForeColor = System.Drawing.Color.Black;
		this.btnLoadOurAirportAsDestination.Image = (System.Drawing.Image)resources.GetObject("btnLoadOurAirportAsDestination.Image");
		this.btnLoadOurAirportAsDestination.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnLoadOurAirportAsDestination.Location = new System.Drawing.Point(277, 80);
		this.btnLoadOurAirportAsDestination.Name = "btnLoadOurAirportAsDestination";
		this.btnLoadOurAirportAsDestination.Size = new System.Drawing.Size(42, 26);
		this.btnLoadOurAirportAsDestination.TabIndex = 74;
		this.btnLoadOurAirportAsDestination.Text = "?";
		this.btnLoadOurAirportAsDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnLoadOurAirportAsDestination.UseVisualStyleBackColor = false;
		this.btnLoadOurAirportAsDestination.Click += new System.EventHandler(btnLoadOurAirportAsDestination_Click);
		this.btnLoadFseAirportAsDeparture.BackColor = System.Drawing.Color.Gray;
		this.btnLoadFseAirportAsDeparture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnLoadFseAirportAsDeparture.ForeColor = System.Drawing.Color.Black;
		this.btnLoadFseAirportAsDeparture.Image = (System.Drawing.Image)resources.GetObject("btnLoadFseAirportAsDeparture.Image");
		this.btnLoadFseAirportAsDeparture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnLoadFseAirportAsDeparture.Location = new System.Drawing.Point(204, 44);
		this.btnLoadFseAirportAsDeparture.Name = "btnLoadFseAirportAsDeparture";
		this.btnLoadFseAirportAsDeparture.Size = new System.Drawing.Size(67, 26);
		this.btnLoadFseAirportAsDeparture.TabIndex = 66;
		this.btnLoadFseAirportAsDeparture.Text = "FSE?";
		this.btnLoadFseAirportAsDeparture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnLoadFseAirportAsDeparture.UseVisualStyleBackColor = false;
		this.btnLoadFseAirportAsDeparture.Click += new System.EventHandler(btnLoadFseAirportAsDeparture_Click);
		this.btnSearchRunwayDeparture.BackColor = System.Drawing.Color.Gray;
		this.btnSearchRunwayDeparture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSearchRunwayDeparture.ForeColor = System.Drawing.Color.Black;
		this.btnSearchRunwayDeparture.Image = (System.Drawing.Image)resources.GetObject("btnSearchRunwayDeparture.Image");
		this.btnSearchRunwayDeparture.Location = new System.Drawing.Point(936, 44);
		this.btnSearchRunwayDeparture.Name = "btnSearchRunwayDeparture";
		this.btnSearchRunwayDeparture.Size = new System.Drawing.Size(30, 26);
		this.btnSearchRunwayDeparture.TabIndex = 50;
		this.btnSearchRunwayDeparture.UseVisualStyleBackColor = false;
		this.btnSearchRunwayDeparture.Click += new System.EventHandler(btnSearchRunwayDeparture_Click);
		this.btnSearchRunwayArrival.BackColor = System.Drawing.Color.Gray;
		this.btnSearchRunwayArrival.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSearchRunwayArrival.ForeColor = System.Drawing.Color.Black;
		this.btnSearchRunwayArrival.Image = (System.Drawing.Image)resources.GetObject("btnSearchRunwayArrival.Image");
		this.btnSearchRunwayArrival.Location = new System.Drawing.Point(936, 80);
		this.btnSearchRunwayArrival.Name = "btnSearchRunwayArrival";
		this.btnSearchRunwayArrival.Size = new System.Drawing.Size(30, 26);
		this.btnSearchRunwayArrival.TabIndex = 48;
		this.btnSearchRunwayArrival.UseVisualStyleBackColor = false;
		this.btnSearchRunwayArrival.Click += new System.EventHandler(btnSearchRunwayArrival_Click);
		this.btnLoadFseAirportAsDestination.BackColor = System.Drawing.Color.Gray;
		this.btnLoadFseAirportAsDestination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnLoadFseAirportAsDestination.ForeColor = System.Drawing.Color.Black;
		this.btnLoadFseAirportAsDestination.Image = (System.Drawing.Image)resources.GetObject("btnLoadFseAirportAsDestination.Image");
		this.btnLoadFseAirportAsDestination.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnLoadFseAirportAsDestination.Location = new System.Drawing.Point(204, 80);
		this.btnLoadFseAirportAsDestination.Name = "btnLoadFseAirportAsDestination";
		this.btnLoadFseAirportAsDestination.Size = new System.Drawing.Size(67, 26);
		this.btnLoadFseAirportAsDestination.TabIndex = 46;
		this.btnLoadFseAirportAsDestination.Text = "FSE?";
		this.btnLoadFseAirportAsDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnLoadFseAirportAsDestination.UseVisualStyleBackColor = false;
		this.btnLoadFseAirportAsDestination.Click += new System.EventHandler(btnLoadFseAirportAsDestination_Click);
		this.DestinationGoogle.BackColor = System.Drawing.Color.Black;
		this.DestinationGoogle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.DestinationGoogle.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DestinationGoogle.ForeColor = System.Drawing.Color.Black;
		this.DestinationGoogle.Image = NavBuddy.Properties.Resources.googlelink;
		this.DestinationGoogle.Location = new System.Drawing.Point(354, 81);
		this.DestinationGoogle.Name = "DestinationGoogle";
		this.DestinationGoogle.Size = new System.Drawing.Size(20, 20);
		this.DestinationGoogle.TabIndex = 40;
		this.DestinationGoogle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.DestinationGoogle.UseVisualStyleBackColor = false;
		this.DestinationGoogle.Click += new System.EventHandler(ArrivalGoogle_Click);
		this.DepartureGoogle.BackColor = System.Drawing.Color.Black;
		this.DepartureGoogle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.DepartureGoogle.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DepartureGoogle.ForeColor = System.Drawing.Color.Black;
		this.DepartureGoogle.Image = NavBuddy.Properties.Resources.googlelink;
		this.DepartureGoogle.Location = new System.Drawing.Point(354, 45);
		this.DepartureGoogle.Name = "DepartureGoogle";
		this.DepartureGoogle.Size = new System.Drawing.Size(20, 20);
		this.DepartureGoogle.TabIndex = 34;
		this.DepartureGoogle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.DepartureGoogle.UseVisualStyleBackColor = false;
		this.DepartureGoogle.Click += new System.EventHandler(DepartureGoogle_Click);
		this.tabFuelManagement.BackColor = System.Drawing.Color.Black;
		this.tabFuelManagement.Controls.Add(this.groupBox8);
		this.tabFuelManagement.Controls.Add(this.groupBox5);
		this.tabFuelManagement.Location = new System.Drawing.Point(4, 27);
		this.tabFuelManagement.Name = "tabFuelManagement";
		this.tabFuelManagement.Padding = new System.Windows.Forms.Padding(3);
		this.tabFuelManagement.Size = new System.Drawing.Size(1143, 390);
		this.tabFuelManagement.TabIndex = 1;
		this.tabFuelManagement.Text = "Fuel management";
		this.groupBox8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox8.BackColor = System.Drawing.Color.Black;
		this.groupBox8.Controls.Add(this.label61);
		this.groupBox8.Controls.Add(this.labEstimatedResidualFlightMiles);
		this.groupBox8.Controls.Add(this.label34);
		this.groupBox8.Controls.Add(this.label25);
		this.groupBox8.Controls.Add(this.labFuelWeight);
		this.groupBox8.Controls.Add(this.labFuelQuantity);
		this.groupBox8.Controls.Add(this.labFuelFlow);
		this.groupBox8.Controls.Add(this.label29);
		this.groupBox8.Controls.Add(this.label30);
		this.groupBox8.Controls.Add(this.labEstimatedResidualFlightTime);
		this.groupBox8.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox8.ForeColor = System.Drawing.Color.White;
		this.groupBox8.Location = new System.Drawing.Point(6, 64);
		this.groupBox8.Name = "groupBox8";
		this.groupBox8.Size = new System.Drawing.Size(1127, 319);
		this.groupBox8.TabIndex = 48;
		this.groupBox8.TabStop = false;
		this.groupBox8.Text = "Fuel";
		this.label61.AutoSize = true;
		this.label61.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label61.ForeColor = System.Drawing.Color.White;
		this.label61.Location = new System.Drawing.Point(581, 72);
		this.label61.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label61.Name = "label61";
		this.label61.Size = new System.Drawing.Size(164, 18);
		this.label61.TabIndex = 48;
		this.label61.Text = "estimated residual miles:";
		this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labEstimatedResidualFlightMiles.AutoSize = true;
		this.labEstimatedResidualFlightMiles.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labEstimatedResidualFlightMiles.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.labEstimatedResidualFlightMiles.Location = new System.Drawing.Point(785, 72);
		this.labEstimatedResidualFlightMiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labEstimatedResidualFlightMiles.Name = "labEstimatedResidualFlightMiles";
		this.labEstimatedResidualFlightMiles.Size = new System.Drawing.Size(23, 18);
		this.labEstimatedResidualFlightMiles.TabIndex = 49;
		this.labEstimatedResidualFlightMiles.Text = "---";
		this.labEstimatedResidualFlightMiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label34.AutoSize = true;
		this.label34.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label34.ForeColor = System.Drawing.Color.White;
		this.label34.Location = new System.Drawing.Point(280, 28);
		this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(68, 18);
		this.label34.TabIndex = 47;
		this.label34.Text = "fuel (lbs):";
		this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label25.AutoSize = true;
		this.label25.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label25.ForeColor = System.Drawing.Color.White;
		this.label25.Location = new System.Drawing.Point(17, 28);
		this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(70, 18);
		this.label25.TabIndex = 22;
		this.label25.Text = "fuel (Gal):";
		this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labFuelWeight.AutoSize = true;
		this.labFuelWeight.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labFuelWeight.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.labFuelWeight.Location = new System.Drawing.Point(358, 28);
		this.labFuelWeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labFuelWeight.Name = "labFuelWeight";
		this.labFuelWeight.Size = new System.Drawing.Size(23, 18);
		this.labFuelWeight.TabIndex = 46;
		this.labFuelWeight.Text = "---";
		this.labFuelWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labFuelQuantity.AutoSize = true;
		this.labFuelQuantity.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labFuelQuantity.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.labFuelQuantity.Location = new System.Drawing.Point(133, 28);
		this.labFuelQuantity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labFuelQuantity.Name = "labFuelQuantity";
		this.labFuelQuantity.Size = new System.Drawing.Size(23, 18);
		this.labFuelQuantity.TabIndex = 23;
		this.labFuelQuantity.Text = "---";
		this.labFuelQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labFuelFlow.AutoSize = true;
		this.labFuelFlow.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labFuelFlow.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.labFuelFlow.Location = new System.Drawing.Point(133, 72);
		this.labFuelFlow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labFuelFlow.Name = "labFuelFlow";
		this.labFuelFlow.Size = new System.Drawing.Size(23, 18);
		this.labFuelFlow.TabIndex = 35;
		this.labFuelFlow.Text = "---";
		this.labFuelFlow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label29.AutoSize = true;
		this.label29.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label29.ForeColor = System.Drawing.Color.White;
		this.label29.Location = new System.Drawing.Point(280, 72);
		this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(193, 18);
		this.label29.TabIndex = 28;
		this.label29.Text = "estimated residual flight time:";
		this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label30.AutoSize = true;
		this.label30.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label30.ForeColor = System.Drawing.Color.White;
		this.label30.Location = new System.Drawing.Point(17, 72);
		this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(105, 18);
		this.label30.TabIndex = 34;
		this.label30.Text = "fuel flow (Gph):";
		this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labEstimatedResidualFlightTime.AutoSize = true;
		this.labEstimatedResidualFlightTime.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labEstimatedResidualFlightTime.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.labEstimatedResidualFlightTime.Location = new System.Drawing.Point(484, 72);
		this.labEstimatedResidualFlightTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labEstimatedResidualFlightTime.Name = "labEstimatedResidualFlightTime";
		this.labEstimatedResidualFlightTime.Size = new System.Drawing.Size(23, 18);
		this.labEstimatedResidualFlightTime.TabIndex = 29;
		this.labEstimatedResidualFlightTime.Text = "---";
		this.labEstimatedResidualFlightTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox5.BackColor = System.Drawing.Color.Black;
		this.groupBox5.Controls.Add(this.labAirplanetitle);
		this.groupBox5.Controls.Add(this.label26);
		this.groupBox5.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox5.ForeColor = System.Drawing.Color.White;
		this.groupBox5.Location = new System.Drawing.Point(6, 6);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(1127, 58);
		this.groupBox5.TabIndex = 25;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "Aircraft";
		this.labAirplanetitle.AutoSize = true;
		this.labAirplanetitle.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAirplanetitle.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.labAirplanetitle.Location = new System.Drawing.Point(133, 24);
		this.labAirplanetitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAirplanetitle.Name = "labAirplanetitle";
		this.labAirplanetitle.Size = new System.Drawing.Size(23, 18);
		this.labAirplanetitle.TabIndex = 25;
		this.labAirplanetitle.Text = "---";
		this.labAirplanetitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label26.AutoSize = true;
		this.label26.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label26.ForeColor = System.Drawing.Color.White;
		this.label26.Location = new System.Drawing.Point(21, 24);
		this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(52, 18);
		this.label26.TabIndex = 24;
		this.label26.Text = "model:";
		this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tabForceTrack.BackColor = System.Drawing.Color.Black;
		this.tabForceTrack.Location = new System.Drawing.Point(4, 27);
		this.tabForceTrack.Name = "tabForceTrack";
		this.tabForceTrack.Size = new System.Drawing.Size(1143, 390);
		this.tabForceTrack.TabIndex = 5;
		this.tabForceTrack.Text = "G-graph";
		this.tabPathTrack.BackColor = System.Drawing.Color.Black;
		this.tabPathTrack.Controls.Add(this.nupFligthTrackRecordDrawingSamples);
		this.tabPathTrack.Controls.Add(this.label42);
		this.tabPathTrack.Controls.Add(this.btnExportFligthTrackRecord);
		this.tabPathTrack.Controls.Add(this.btnResetFligthTrackRecord);
		this.tabPathTrack.Controls.Add(this.nupFligthTrackRecordInterval);
		this.tabPathTrack.Controls.Add(this.MapPanel);
		this.tabPathTrack.Controls.Add(this.label41);
		this.tabPathTrack.Controls.Add(this.btnFligthTrackRecord);
		this.tabPathTrack.Location = new System.Drawing.Point(4, 27);
		this.tabPathTrack.Name = "tabPathTrack";
		this.tabPathTrack.Size = new System.Drawing.Size(1143, 390);
		this.tabPathTrack.TabIndex = 9;
		this.tabPathTrack.Text = "Flight track";
		this.nupFligthTrackRecordDrawingSamples.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nupFligthTrackRecordDrawingSamples.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nupFligthTrackRecordDrawingSamples.Increment = new decimal(new int[4] { 5, 0, 0, 0 });
		this.nupFligthTrackRecordDrawingSamples.Location = new System.Drawing.Point(1024, 65);
		this.nupFligthTrackRecordDrawingSamples.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.nupFligthTrackRecordDrawingSamples.Minimum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.nupFligthTrackRecordDrawingSamples.Name = "nupFligthTrackRecordDrawingSamples";
		this.nupFligthTrackRecordDrawingSamples.Size = new System.Drawing.Size(44, 23);
		this.nupFligthTrackRecordDrawingSamples.TabIndex = 47;
		this.nupFligthTrackRecordDrawingSamples.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nupFligthTrackRecordDrawingSamples.Value = new decimal(new int[4] { 60, 0, 0, 0 });
		this.label42.AutoSize = true;
		this.label42.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label42.ForeColor = System.Drawing.Color.White;
		this.label42.Location = new System.Drawing.Point(913, 67);
		this.label42.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label42.Name = "label42";
		this.label42.Size = new System.Drawing.Size(91, 15);
		this.label42.TabIndex = 46;
		this.label42.Text = "Samples drawn";
		this.btnExportFligthTrackRecord.BackColor = System.Drawing.Color.Gray;
		this.btnExportFligthTrackRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnExportFligthTrackRecord.ForeColor = System.Drawing.Color.Black;
		this.btnExportFligthTrackRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnExportFligthTrackRecord.Location = new System.Drawing.Point(715, 149);
		this.btnExportFligthTrackRecord.Name = "btnExportFligthTrackRecord";
		this.btnExportFligthTrackRecord.Size = new System.Drawing.Size(353, 31);
		this.btnExportFligthTrackRecord.TabIndex = 45;
		this.btnExportFligthTrackRecord.Text = "EXPORT";
		this.btnExportFligthTrackRecord.UseVisualStyleBackColor = false;
		this.btnExportFligthTrackRecord.Click += new System.EventHandler(btnExportFligthTrackRecord_Click);
		this.btnResetFligthTrackRecord.BackColor = System.Drawing.Color.Gray;
		this.btnResetFligthTrackRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnResetFligthTrackRecord.ForeColor = System.Drawing.Color.Black;
		this.btnResetFligthTrackRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnResetFligthTrackRecord.Location = new System.Drawing.Point(715, 112);
		this.btnResetFligthTrackRecord.Name = "btnResetFligthTrackRecord";
		this.btnResetFligthTrackRecord.Size = new System.Drawing.Size(353, 31);
		this.btnResetFligthTrackRecord.TabIndex = 44;
		this.btnResetFligthTrackRecord.Text = "RESET";
		this.btnResetFligthTrackRecord.UseVisualStyleBackColor = false;
		this.btnResetFligthTrackRecord.Click += new System.EventHandler(btnResetFligthTrackRecord_Click);
		this.nupFligthTrackRecordInterval.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nupFligthTrackRecordInterval.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nupFligthTrackRecordInterval.Location = new System.Drawing.Point(1024, 24);
		this.nupFligthTrackRecordInterval.Maximum = new decimal(new int[4] { 999, 0, 0, 0 });
		this.nupFligthTrackRecordInterval.Minimum = new decimal(new int[4] { 2, 0, 0, 0 });
		this.nupFligthTrackRecordInterval.Name = "nupFligthTrackRecordInterval";
		this.nupFligthTrackRecordInterval.Size = new System.Drawing.Size(44, 23);
		this.nupFligthTrackRecordInterval.TabIndex = 31;
		this.nupFligthTrackRecordInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nupFligthTrackRecordInterval.Value = new decimal(new int[4] { 10, 0, 0, 0 });
		this.MapPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.MapPanel.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		this.MapPanel.Controls.Add(this.btnMapZoomDec);
		this.MapPanel.Controls.Add(this.btnMapZoomInc);
		this.MapPanel.Location = new System.Drawing.Point(5, 6);
		this.MapPanel.Name = "MapPanel";
		this.MapPanel.Size = new System.Drawing.Size(692, 381);
		this.MapPanel.TabIndex = 0;
		this.MapPanel.Paint += new System.Windows.Forms.PaintEventHandler(MapPanel_Paint);
		this.btnMapZoomDec.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnMapZoomDec.BackColor = System.Drawing.Color.Gray;
		this.btnMapZoomDec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnMapZoomDec.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnMapZoomDec.ForeColor = System.Drawing.Color.Black;
		this.btnMapZoomDec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnMapZoomDec.Location = new System.Drawing.Point(663, 35);
		this.btnMapZoomDec.Name = "btnMapZoomDec";
		this.btnMapZoomDec.Size = new System.Drawing.Size(26, 26);
		this.btnMapZoomDec.TabIndex = 29;
		this.btnMapZoomDec.Text = "-";
		this.btnMapZoomDec.UseVisualStyleBackColor = false;
		this.btnMapZoomDec.Click += new System.EventHandler(btnMapZoomDec_Click);
		this.btnMapZoomInc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnMapZoomInc.BackColor = System.Drawing.Color.Gray;
		this.btnMapZoomInc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnMapZoomInc.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnMapZoomInc.ForeColor = System.Drawing.Color.Black;
		this.btnMapZoomInc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnMapZoomInc.Location = new System.Drawing.Point(663, 3);
		this.btnMapZoomInc.Name = "btnMapZoomInc";
		this.btnMapZoomInc.Size = new System.Drawing.Size(26, 26);
		this.btnMapZoomInc.TabIndex = 28;
		this.btnMapZoomInc.Text = "+";
		this.btnMapZoomInc.UseVisualStyleBackColor = false;
		this.btnMapZoomInc.Click += new System.EventHandler(btnMapZoomInc_Click);
		this.label41.AutoSize = true;
		this.label41.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label41.ForeColor = System.Drawing.Color.White;
		this.label41.Location = new System.Drawing.Point(913, 26);
		this.label41.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label41.Name = "label41";
		this.label41.Size = new System.Drawing.Size(104, 15);
		this.label41.TabIndex = 30;
		this.label41.Text = "Sampling interval";
		this.btnFligthTrackRecord.BackColor = System.Drawing.Color.Gray;
		this.btnFligthTrackRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnFligthTrackRecord.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnFligthTrackRecord.ForeColor = System.Drawing.Color.Black;
		this.btnFligthTrackRecord.Image = (System.Drawing.Image)resources.GetObject("btnFligthTrackRecord.Image");
		this.btnFligthTrackRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnFligthTrackRecord.Location = new System.Drawing.Point(715, 9);
		this.btnFligthTrackRecord.Name = "btnFligthTrackRecord";
		this.btnFligthTrackRecord.Size = new System.Drawing.Size(162, 48);
		this.btnFligthTrackRecord.TabIndex = 29;
		this.btnFligthTrackRecord.Text = "FLIGHT TRACK RECORD";
		this.btnFligthTrackRecord.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnFligthTrackRecord.UseVisualStyleBackColor = false;
		this.btnFligthTrackRecord.Click += new System.EventHandler(btnFligthTrackRecord_Click);
		this.tabStandardManouver.BackColor = System.Drawing.Color.Black;
		this.tabStandardManouver.Controls.Add(this.groupBox11);
		this.tabStandardManouver.Controls.Add(this.groupBox10);
		this.tabStandardManouver.Controls.Add(this.groupBox9);
		this.tabStandardManouver.Controls.Add(this.btnResetCronometer);
		this.tabStandardManouver.Controls.Add(this.btnStartStopCronometer);
		this.tabStandardManouver.Controls.Add(this.label4);
		this.tabStandardManouver.Controls.Add(this.labCronometer);
		this.tabStandardManouver.Location = new System.Drawing.Point(4, 27);
		this.tabStandardManouver.Name = "tabStandardManouver";
		this.tabStandardManouver.Padding = new System.Windows.Forms.Padding(3);
		this.tabStandardManouver.Size = new System.Drawing.Size(1143, 390);
		this.tabStandardManouver.TabIndex = 10;
		this.tabStandardManouver.Text = "Standard Maneuvers";
		this.groupBox11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox11.BackColor = System.Drawing.Color.Black;
		this.groupBox11.Controls.Add(this.lblST_CurrentTurnRay);
		this.groupBox11.Controls.Add(this.lblST_ExpectedBanking);
		this.groupBox11.Controls.Add(this.lblST_CurrentGS);
		this.groupBox11.Controls.Add(this.label48);
		this.groupBox11.Controls.Add(this.lblST_CurrentIas);
		this.groupBox11.Controls.Add(this.label47);
		this.groupBox11.Controls.Add(this.pictureBox2);
		this.groupBox11.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox11.ForeColor = System.Drawing.Color.White;
		this.groupBox11.Location = new System.Drawing.Point(379, 19);
		this.groupBox11.Name = "groupBox11";
		this.groupBox11.Size = new System.Drawing.Size(318, 265);
		this.groupBox11.TabIndex = 70;
		this.groupBox11.TabStop = false;
		this.groupBox11.Text = "Standard turn";
		this.lblST_CurrentTurnRay.AutoSize = true;
		this.lblST_CurrentTurnRay.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblST_CurrentTurnRay.ForeColor = System.Drawing.Color.White;
		this.lblST_CurrentTurnRay.Location = new System.Drawing.Point(256, 226);
		this.lblST_CurrentTurnRay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblST_CurrentTurnRay.Name = "lblST_CurrentTurnRay";
		this.lblST_CurrentTurnRay.Size = new System.Drawing.Size(29, 18);
		this.lblST_CurrentTurnRay.TabIndex = 65;
		this.lblST_CurrentTurnRay.Text = "000";
		this.lblST_CurrentTurnRay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblST_ExpectedBanking.AutoSize = true;
		this.lblST_ExpectedBanking.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblST_ExpectedBanking.ForeColor = System.Drawing.Color.White;
		this.lblST_ExpectedBanking.Location = new System.Drawing.Point(256, 31);
		this.lblST_ExpectedBanking.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblST_ExpectedBanking.Name = "lblST_ExpectedBanking";
		this.lblST_ExpectedBanking.Size = new System.Drawing.Size(29, 18);
		this.lblST_ExpectedBanking.TabIndex = 62;
		this.lblST_ExpectedBanking.Text = "000";
		this.lblST_ExpectedBanking.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblST_CurrentGS.AutoSize = true;
		this.lblST_CurrentGS.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblST_CurrentGS.ForeColor = System.Drawing.Color.White;
		this.lblST_CurrentGS.Location = new System.Drawing.Point(188, 224);
		this.lblST_CurrentGS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblST_CurrentGS.Name = "lblST_CurrentGS";
		this.lblST_CurrentGS.Size = new System.Drawing.Size(29, 18);
		this.lblST_CurrentGS.TabIndex = 61;
		this.lblST_CurrentGS.Text = "000";
		this.lblST_CurrentGS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label48.AutoSize = true;
		this.label48.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label48.ForeColor = System.Drawing.Color.White;
		this.label48.Location = new System.Drawing.Point(20, 224);
		this.label48.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label48.Name = "label48";
		this.label48.Size = new System.Drawing.Size(134, 18);
		this.label48.TabIndex = 60;
		this.label48.Text = "Current GS / turn ray";
		this.label48.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblST_CurrentIas.AutoSize = true;
		this.lblST_CurrentIas.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblST_CurrentIas.ForeColor = System.Drawing.Color.White;
		this.lblST_CurrentIas.Location = new System.Drawing.Point(188, 31);
		this.lblST_CurrentIas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblST_CurrentIas.Name = "lblST_CurrentIas";
		this.lblST_CurrentIas.Size = new System.Drawing.Size(29, 18);
		this.lblST_CurrentIas.TabIndex = 59;
		this.lblST_CurrentIas.Text = "000";
		this.lblST_CurrentIas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label47.AutoSize = true;
		this.label47.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label47.ForeColor = System.Drawing.Color.White;
		this.label47.Location = new System.Drawing.Point(16, 31);
		this.label47.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label47.Name = "label47";
		this.label47.Size = new System.Drawing.Size(143, 18);
		this.label47.TabIndex = 58;
		this.label47.Text = "Current IAS / Banking:";
		this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
		this.pictureBox2.Location = new System.Drawing.Point(47, 73);
		this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(220, 140);
		this.pictureBox2.TabIndex = 66;
		this.pictureBox2.TabStop = false;
		this.groupBox10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox10.BackColor = System.Drawing.Color.Black;
		this.groupBox10.Controls.Add(this.labRefHeading);
		this.groupBox10.Controls.Add(this.pictureBox1);
		this.groupBox10.Controls.Add(this.labRefHeading270);
		this.groupBox10.Controls.Add(this.labRefHeading180);
		this.groupBox10.Controls.Add(this.labRefHeading90);
		this.groupBox10.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox10.ForeColor = System.Drawing.Color.White;
		this.groupBox10.Location = new System.Drawing.Point(745, 19);
		this.groupBox10.Name = "groupBox10";
		this.groupBox10.Size = new System.Drawing.Size(318, 265);
		this.groupBox10.TabIndex = 69;
		this.groupBox10.TabStop = false;
		this.groupBox10.Text = "Reference";
		this.labRefHeading.BackColor = System.Drawing.Color.Black;
		this.labRefHeading.Font = new System.Drawing.Font("Calibri", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labRefHeading.ForeColor = System.Drawing.Color.White;
		this.labRefHeading.Location = new System.Drawing.Point(116, 43);
		this.labRefHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labRefHeading.Name = "labRefHeading";
		this.labRefHeading.Size = new System.Drawing.Size(96, 37);
		this.labRefHeading.TabIndex = 62;
		this.labRefHeading.Text = "000";
		this.labRefHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.Location = new System.Drawing.Point(113, 86);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(96, 100);
		this.pictureBox1.TabIndex = 63;
		this.pictureBox1.TabStop = false;
		this.labRefHeading270.BackColor = System.Drawing.Color.Black;
		this.labRefHeading270.Font = new System.Drawing.Font("Calibri", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labRefHeading270.ForeColor = System.Drawing.Color.White;
		this.labRefHeading270.Location = new System.Drawing.Point(22, 118);
		this.labRefHeading270.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labRefHeading270.Name = "labRefHeading270";
		this.labRefHeading270.Size = new System.Drawing.Size(84, 37);
		this.labRefHeading270.TabIndex = 66;
		this.labRefHeading270.Text = "000";
		this.labRefHeading270.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labRefHeading180.BackColor = System.Drawing.Color.Black;
		this.labRefHeading180.Font = new System.Drawing.Font("Calibri", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labRefHeading180.ForeColor = System.Drawing.Color.White;
		this.labRefHeading180.Location = new System.Drawing.Point(116, 192);
		this.labRefHeading180.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labRefHeading180.Name = "labRefHeading180";
		this.labRefHeading180.Size = new System.Drawing.Size(96, 37);
		this.labRefHeading180.TabIndex = 64;
		this.labRefHeading180.Text = "000";
		this.labRefHeading180.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labRefHeading90.BackColor = System.Drawing.Color.Black;
		this.labRefHeading90.Font = new System.Drawing.Font("Calibri", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labRefHeading90.ForeColor = System.Drawing.Color.White;
		this.labRefHeading90.Location = new System.Drawing.Point(219, 118);
		this.labRefHeading90.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labRefHeading90.Name = "labRefHeading90";
		this.labRefHeading90.Size = new System.Drawing.Size(84, 37);
		this.labRefHeading90.TabIndex = 65;
		this.labRefHeading90.Text = "000";
		this.labRefHeading90.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.groupBox9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox9.BackColor = System.Drawing.Color.Black;
		this.groupBox9.Controls.Add(this.labHeadingMag);
		this.groupBox9.Controls.Add(this.PanelTurnSpeed);
		this.groupBox9.Controls.Add(this.label43);
		this.groupBox9.Controls.Add(this.label45);
		this.groupBox9.Controls.Add(this.labHeadingTrue);
		this.groupBox9.Controls.Add(this.labDeltaHeading);
		this.groupBox9.Controls.Add(this.label46);
		this.groupBox9.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox9.ForeColor = System.Drawing.Color.White;
		this.groupBox9.Location = new System.Drawing.Point(13, 19);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(318, 265);
		this.groupBox9.TabIndex = 68;
		this.groupBox9.TabStop = false;
		this.groupBox9.Text = "Heading";
		this.labHeadingMag.BackColor = System.Drawing.Color.Black;
		this.labHeadingMag.Font = new System.Drawing.Font("Calibri", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labHeadingMag.ForeColor = System.Drawing.Color.White;
		this.labHeadingMag.Location = new System.Drawing.Point(177, 18);
		this.labHeadingMag.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labHeadingMag.Name = "labHeadingMag";
		this.labHeadingMag.Size = new System.Drawing.Size(84, 45);
		this.labHeadingMag.TabIndex = 57;
		this.labHeadingMag.Text = "0000";
		this.labHeadingMag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.PanelTurnSpeed.Location = new System.Drawing.Point(6, 142);
		this.PanelTurnSpeed.Name = "PanelTurnSpeed";
		this.PanelTurnSpeed.Size = new System.Drawing.Size(305, 64);
		this.PanelTurnSpeed.TabIndex = 67;
		this.PanelTurnSpeed.Paint += new System.Windows.Forms.PaintEventHandler(PanelTurnSpeed_Paint);
		this.label43.AutoSize = true;
		this.label43.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label43.ForeColor = System.Drawing.Color.White;
		this.label43.Location = new System.Drawing.Point(50, 31);
		this.label43.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label43.Name = "label43";
		this.label43.Size = new System.Drawing.Size(68, 18);
		this.label43.TabIndex = 56;
		this.label43.Text = "Magnetic:";
		this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label45.AutoSize = true;
		this.label45.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label45.ForeColor = System.Drawing.Color.White;
		this.label45.Location = new System.Drawing.Point(52, 76);
		this.label45.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label45.Name = "label45";
		this.label45.Size = new System.Drawing.Size(39, 18);
		this.label45.TabIndex = 58;
		this.label45.Text = "True:";
		this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labHeadingTrue.BackColor = System.Drawing.Color.Black;
		this.labHeadingTrue.Font = new System.Drawing.Font("Calibri", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labHeadingTrue.ForeColor = System.Drawing.Color.White;
		this.labHeadingTrue.Location = new System.Drawing.Point(177, 63);
		this.labHeadingTrue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labHeadingTrue.Name = "labHeadingTrue";
		this.labHeadingTrue.Size = new System.Drawing.Size(84, 45);
		this.labHeadingTrue.TabIndex = 59;
		this.labHeadingTrue.Text = "0000";
		this.labHeadingTrue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labDeltaHeading.BackColor = System.Drawing.Color.Black;
		this.labDeltaHeading.Font = new System.Drawing.Font("Calibri", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labDeltaHeading.ForeColor = System.Drawing.Color.White;
		this.labDeltaHeading.Location = new System.Drawing.Point(111, 210);
		this.labDeltaHeading.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labDeltaHeading.Name = "labDeltaHeading";
		this.labDeltaHeading.Size = new System.Drawing.Size(84, 45);
		this.labDeltaHeading.TabIndex = 61;
		this.labDeltaHeading.Text = "0000";
		this.labDeltaHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.label46.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label46.ForeColor = System.Drawing.Color.White;
		this.label46.Location = new System.Drawing.Point(6, 114);
		this.label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label46.Name = "label46";
		this.label46.Size = new System.Drawing.Size(305, 18);
		this.label46.TabIndex = 60;
		this.label46.Text = "Direction variation speed:";
		this.label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnResetCronometer.BackColor = System.Drawing.Color.Gray;
		this.btnResetCronometer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnResetCronometer.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnResetCronometer.ForeColor = System.Drawing.Color.Black;
		this.btnResetCronometer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnResetCronometer.Location = new System.Drawing.Point(238, 343);
		this.btnResetCronometer.Name = "btnResetCronometer";
		this.btnResetCronometer.Size = new System.Drawing.Size(85, 25);
		this.btnResetCronometer.TabIndex = 54;
		this.btnResetCronometer.Text = "RESET";
		this.btnResetCronometer.UseVisualStyleBackColor = false;
		this.btnResetCronometer.Click += new System.EventHandler(btnResetCronometer_Click);
		this.btnStartStopCronometer.BackColor = System.Drawing.Color.Gray;
		this.btnStartStopCronometer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnStartStopCronometer.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnStartStopCronometer.ForeColor = System.Drawing.Color.Black;
		this.btnStartStopCronometer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnStartStopCronometer.Location = new System.Drawing.Point(238, 300);
		this.btnStartStopCronometer.Name = "btnStartStopCronometer";
		this.btnStartStopCronometer.Size = new System.Drawing.Size(85, 25);
		this.btnStartStopCronometer.TabIndex = 53;
		this.btnStartStopCronometer.Text = "START/STOP";
		this.btnStartStopCronometer.UseVisualStyleBackColor = false;
		this.btnStartStopCronometer.Click += new System.EventHandler(btnStartStopCronometer_Click);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.White;
		this.label4.Location = new System.Drawing.Point(9, 325);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(87, 18);
		this.label4.TabIndex = 44;
		this.label4.Text = "Cronometer:";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labCronometer.BackColor = System.Drawing.Color.Black;
		this.labCronometer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.labCronometer.Font = new System.Drawing.Font("Calibri", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labCronometer.ForeColor = System.Drawing.Color.White;
		this.labCronometer.Location = new System.Drawing.Point(104, 300);
		this.labCronometer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labCronometer.Name = "labCronometer";
		this.labCronometer.Size = new System.Drawing.Size(110, 68);
		this.labCronometer.TabIndex = 43;
		this.labCronometer.Text = "000";
		this.labCronometer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.tabMisc.BackColor = System.Drawing.Color.Black;
		this.tabMisc.Controls.Add(this.label82);
		this.tabMisc.Controls.Add(this.btnSetPositionstackKey);
		this.tabMisc.Controls.Add(this.label81);
		this.tabMisc.Controls.Add(this.txtPositionstackKey);
		this.tabMisc.Controls.Add(this.groupBox12);
		this.tabMisc.Controls.Add(this.grpTeleport);
		this.tabMisc.Location = new System.Drawing.Point(4, 27);
		this.tabMisc.Name = "tabMisc";
		this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
		this.tabMisc.Size = new System.Drawing.Size(1143, 390);
		this.tabMisc.TabIndex = 13;
		this.tabMisc.Text = "Miscellaneous";
		this.label82.AutoSize = true;
		this.label82.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label82.ForeColor = System.Drawing.Color.White;
		this.label82.Location = new System.Drawing.Point(438, 65);
		this.label82.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label82.Name = "label82";
		this.label82.Size = new System.Drawing.Size(173, 18);
		this.label82.TabIndex = 90;
		this.label82.Text = "https://positionstack.com/";
		this.label82.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSetPositionstackKey.BackColor = System.Drawing.Color.Gray;
		this.btnSetPositionstackKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSetPositionstackKey.ForeColor = System.Drawing.Color.Black;
		this.btnSetPositionstackKey.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSetPositionstackKey.Location = new System.Drawing.Point(870, 30);
		this.btnSetPositionstackKey.Name = "btnSetPositionstackKey";
		this.btnSetPositionstackKey.Size = new System.Drawing.Size(86, 26);
		this.btnSetPositionstackKey.TabIndex = 89;
		this.btnSetPositionstackKey.Text = "SET KEY";
		this.btnSetPositionstackKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSetPositionstackKey.UseVisualStyleBackColor = false;
		this.btnSetPositionstackKey.Click += new System.EventHandler(btnSetPositionstackKey_Click);
		this.label81.AutoSize = true;
		this.label81.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label81.ForeColor = System.Drawing.Color.White;
		this.label81.Location = new System.Drawing.Point(438, 33);
		this.label81.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label81.Name = "label81";
		this.label81.Size = new System.Drawing.Size(117, 18);
		this.label81.TabIndex = 40;
		this.label81.Text = "Positionstack KEY:";
		this.label81.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtPositionstackKey.BackColor = System.Drawing.Color.White;
		this.txtPositionstackKey.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPositionstackKey.ForeColor = System.Drawing.Color.Black;
		this.txtPositionstackKey.Location = new System.Drawing.Point(576, 30);
		this.txtPositionstackKey.Margin = new System.Windows.Forms.Padding(0);
		this.txtPositionstackKey.Name = "txtPositionstackKey";
		this.txtPositionstackKey.Size = new System.Drawing.Size(267, 26);
		this.txtPositionstackKey.TabIndex = 39;
		this.groupBox12.BackColor = System.Drawing.Color.Black;
		this.groupBox12.Controls.Add(this.labAirplaneDescription);
		this.groupBox12.Controls.Add(this.btnAirplaneCheck);
		this.groupBox12.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox12.ForeColor = System.Drawing.Color.White;
		this.groupBox12.Location = new System.Drawing.Point(9, 198);
		this.groupBox12.Name = "groupBox12";
		this.groupBox12.Size = new System.Drawing.Size(404, 186);
		this.groupBox12.TabIndex = 28;
		this.groupBox12.TabStop = false;
		this.groupBox12.Text = "Airplane:";
		this.labAirplaneDescription.AutoSize = true;
		this.labAirplaneDescription.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAirplaneDescription.ForeColor = System.Drawing.Color.White;
		this.labAirplaneDescription.Location = new System.Drawing.Point(18, 29);
		this.labAirplaneDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAirplaneDescription.Name = "labAirplaneDescription";
		this.labAirplaneDescription.Size = new System.Drawing.Size(26, 18);
		this.labAirplaneDescription.TabIndex = 89;
		this.labAirplaneDescription.Text = "///";
		this.labAirplaneDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAirplaneCheck.BackColor = System.Drawing.Color.Gray;
		this.btnAirplaneCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAirplaneCheck.ForeColor = System.Drawing.Color.Black;
		this.btnAirplaneCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAirplaneCheck.Location = new System.Drawing.Point(302, 138);
		this.btnAirplaneCheck.Name = "btnAirplaneCheck";
		this.btnAirplaneCheck.Size = new System.Drawing.Size(86, 26);
		this.btnAirplaneCheck.TabIndex = 88;
		this.btnAirplaneCheck.Text = "Check";
		this.btnAirplaneCheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAirplaneCheck.UseVisualStyleBackColor = false;
		this.btnAirplaneCheck.Click += new System.EventHandler(btnAirplaneCheck_Click);
		this.grpTeleport.BackColor = System.Drawing.Color.Black;
		this.grpTeleport.Controls.Add(this.btnInfo);
		this.grpTeleport.Controls.Add(this.btnChaseLocation);
		this.grpTeleport.Controls.Add(this.label77);
		this.grpTeleport.Controls.Add(this.label76);
		this.grpTeleport.Controls.Add(this.nudTeleportHeading);
		this.grpTeleport.Controls.Add(this.label74);
		this.grpTeleport.Controls.Add(this.label75);
		this.grpTeleport.Controls.Add(this.nudTeleportAltitude);
		this.grpTeleport.Controls.Add(this.btnTeleportToCustomLocation);
		this.grpTeleport.Controls.Add(this.label73);
		this.grpTeleport.Controls.Add(this.txtTeleportCoordinates);
		this.grpTeleport.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpTeleport.ForeColor = System.Drawing.Color.White;
		this.grpTeleport.Location = new System.Drawing.Point(9, 4);
		this.grpTeleport.Name = "grpTeleport";
		this.grpTeleport.Size = new System.Drawing.Size(404, 189);
		this.grpTeleport.TabIndex = 26;
		this.grpTeleport.TabStop = false;
		this.grpTeleport.Text = "Custom location";
		this.btnInfo.BackColor = System.Drawing.Color.Gray;
		this.btnInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnInfo.ForeColor = System.Drawing.Color.Black;
		this.btnInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnInfo.Location = new System.Drawing.Point(264, 65);
		this.btnInfo.Name = "btnInfo";
		this.btnInfo.Size = new System.Drawing.Size(86, 26);
		this.btnInfo.TabIndex = 91;
		this.btnInfo.Text = "INFO";
		this.btnInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnInfo.UseVisualStyleBackColor = false;
		this.btnInfo.Click += new System.EventHandler(btnInfo_Click);
		this.btnChaseLocation.BackColor = System.Drawing.Color.Gray;
		this.btnChaseLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnChaseLocation.ForeColor = System.Drawing.Color.Black;
		this.btnChaseLocation.Image = NavBuddy.Properties.Resources.follow;
		this.btnChaseLocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnChaseLocation.Location = new System.Drawing.Point(264, 101);
		this.btnChaseLocation.Name = "btnChaseLocation";
		this.btnChaseLocation.Size = new System.Drawing.Size(86, 26);
		this.btnChaseLocation.TabIndex = 88;
		this.btnChaseLocation.Text = "TRACK";
		this.btnChaseLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnChaseLocation.UseVisualStyleBackColor = false;
		this.btnChaseLocation.Click += new System.EventHandler(btnChaseLocation_Click);
		this.label77.AutoSize = true;
		this.label77.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label77.ForeColor = System.Drawing.Color.White;
		this.label77.Location = new System.Drawing.Point(19, 103);
		this.label77.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label77.Name = "label77";
		this.label77.Size = new System.Drawing.Size(63, 18);
		this.label77.TabIndex = 87;
		this.label77.Text = "Heading:";
		this.label77.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label76.AutoSize = true;
		this.label76.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label76.ForeColor = System.Drawing.Color.White;
		this.label76.Location = new System.Drawing.Point(203, 105);
		this.label76.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label76.Name = "label76";
		this.label76.Size = new System.Drawing.Size(13, 18);
		this.label76.TabIndex = 86;
		this.label76.Text = "°";
		this.label76.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.nudTeleportHeading.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudTeleportHeading.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudTeleportHeading.Location = new System.Drawing.Point(121, 103);
		this.nudTeleportHeading.Maximum = new decimal(new int[4] { 360, 0, 0, 0 });
		this.nudTeleportHeading.Name = "nudTeleportHeading";
		this.nudTeleportHeading.Size = new System.Drawing.Size(71, 23);
		this.nudTeleportHeading.TabIndex = 85;
		this.nudTeleportHeading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudTeleportHeading.Value = new decimal(new int[4] { 360, 0, 0, 0 });
		this.label74.AutoSize = true;
		this.label74.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label74.ForeColor = System.Drawing.Color.White;
		this.label74.Location = new System.Drawing.Point(199, 74);
		this.label74.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label74.Name = "label74";
		this.label74.Size = new System.Drawing.Size(34, 18);
		this.label74.TabIndex = 84;
		this.label74.Text = "feet";
		this.label74.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label75.AutoSize = true;
		this.label75.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label75.ForeColor = System.Drawing.Color.White;
		this.label75.Location = new System.Drawing.Point(19, 71);
		this.label75.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label75.Name = "label75";
		this.label75.Size = new System.Drawing.Size(62, 18);
		this.label75.TabIndex = 83;
		this.label75.Text = "Altitude:";
		this.label75.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.nudTeleportAltitude.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudTeleportAltitude.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudTeleportAltitude.Increment = new decimal(new int[4] { 100, 0, 0, 0 });
		this.nudTeleportAltitude.Location = new System.Drawing.Point(121, 69);
		this.nudTeleportAltitude.Maximum = new decimal(new int[4] { 50000, 0, 0, 0 });
		this.nudTeleportAltitude.Name = "nudTeleportAltitude";
		this.nudTeleportAltitude.Size = new System.Drawing.Size(71, 23);
		this.nudTeleportAltitude.TabIndex = 82;
		this.nudTeleportAltitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudTeleportAltitude.Value = new decimal(new int[4] { 5000, 0, 0, 0 });
		this.btnTeleportToCustomLocation.BackColor = System.Drawing.Color.Gray;
		this.btnTeleportToCustomLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnTeleportToCustomLocation.ForeColor = System.Drawing.Color.Black;
		this.btnTeleportToCustomLocation.Image = NavBuddy.Properties.Resources.freccina;
		this.btnTeleportToCustomLocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnTeleportToCustomLocation.Location = new System.Drawing.Point(264, 144);
		this.btnTeleportToCustomLocation.Name = "btnTeleportToCustomLocation";
		this.btnTeleportToCustomLocation.Size = new System.Drawing.Size(86, 26);
		this.btnTeleportToCustomLocation.TabIndex = 67;
		this.btnTeleportToCustomLocation.Text = "TELEPORT";
		this.btnTeleportToCustomLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnTeleportToCustomLocation.UseVisualStyleBackColor = false;
		this.btnTeleportToCustomLocation.Click += new System.EventHandler(btnTeleportToCustomLocation_Click);
		this.label73.AutoSize = true;
		this.label73.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label73.ForeColor = System.Drawing.Color.White;
		this.label73.Location = new System.Drawing.Point(19, 29);
		this.label73.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label73.Name = "label73";
		this.label73.Size = new System.Drawing.Size(87, 18);
		this.label73.TabIndex = 38;
		this.label73.Text = "Coordinates:";
		this.label73.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.txtTeleportCoordinates.BackColor = System.Drawing.Color.White;
		this.txtTeleportCoordinates.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtTeleportCoordinates.ForeColor = System.Drawing.Color.Black;
		this.txtTeleportCoordinates.Location = new System.Drawing.Point(121, 26);
		this.txtTeleportCoordinates.Margin = new System.Windows.Forms.Padding(0);
		this.txtTeleportCoordinates.Name = "txtTeleportCoordinates";
		this.txtTeleportCoordinates.Size = new System.Drawing.Size(267, 26);
		this.txtTeleportCoordinates.TabIndex = 37;
		this.txtTeleportCoordinates.Text = "45.434533935256106, 9.27852168027531";
		this.tabGraphicLog.BackColor = System.Drawing.Color.Black;
		this.tabGraphicLog.Controls.Add(this.grpGraphicLogConfig);
		this.tabGraphicLog.Controls.Add(this.grpGraphicLog);
		this.tabGraphicLog.Location = new System.Drawing.Point(4, 27);
		this.tabGraphicLog.Name = "tabGraphicLog";
		this.tabGraphicLog.Padding = new System.Windows.Forms.Padding(3);
		this.tabGraphicLog.Size = new System.Drawing.Size(1143, 390);
		this.tabGraphicLog.TabIndex = 14;
		this.tabGraphicLog.Text = "Graphic Log";
		this.grpGraphicLogConfig.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.grpGraphicLogConfig.BackColor = System.Drawing.Color.Lime;
		this.grpGraphicLogConfig.Controls.Add(this.flpGraphicLogConfig);
		this.grpGraphicLogConfig.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpGraphicLogConfig.ForeColor = System.Drawing.Color.White;
		this.grpGraphicLogConfig.Location = new System.Drawing.Point(877, 6);
		this.grpGraphicLogConfig.Name = "grpGraphicLogConfig";
		this.grpGraphicLogConfig.Size = new System.Drawing.Size(260, 379);
		this.grpGraphicLogConfig.TabIndex = 29;
		this.grpGraphicLogConfig.TabStop = false;
		this.flpGraphicLogConfig.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.flpGraphicLogConfig.AutoScroll = true;
		this.flpGraphicLogConfig.Location = new System.Drawing.Point(6, 11);
		this.flpGraphicLogConfig.Name = "flpGraphicLogConfig";
		this.flpGraphicLogConfig.Size = new System.Drawing.Size(248, 362);
		this.flpGraphicLogConfig.TabIndex = 0;
		this.grpGraphicLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.grpGraphicLog.BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		this.grpGraphicLog.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpGraphicLog.ForeColor = System.Drawing.Color.White;
		this.grpGraphicLog.Location = new System.Drawing.Point(5, 6);
		this.grpGraphicLog.Margin = new System.Windows.Forms.Padding(10);
		this.grpGraphicLog.Name = "grpGraphicLog";
		this.grpGraphicLog.Padding = new System.Windows.Forms.Padding(10);
		this.grpGraphicLog.Size = new System.Drawing.Size(859, 379);
		this.grpGraphicLog.TabIndex = 28;
		this.grpGraphicLog.TabStop = false;
		this.tabLog.BackColor = System.Drawing.Color.Black;
		this.tabLog.Controls.Add(this.txtCommLog);
		this.tabLog.Location = new System.Drawing.Point(4, 27);
		this.tabLog.Name = "tabLog";
		this.tabLog.Padding = new System.Windows.Forms.Padding(3);
		this.tabLog.Size = new System.Drawing.Size(1143, 390);
		this.tabLog.TabIndex = 2;
		this.tabLog.Text = "Log";
		this.txtCommLog.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtCommLog.Location = new System.Drawing.Point(3, 3);
		this.txtCommLog.Multiline = true;
		this.txtCommLog.Name = "txtCommLog";
		this.txtCommLog.ReadOnly = true;
		this.txtCommLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
		this.txtCommLog.Size = new System.Drawing.Size(1088, 381);
		this.txtCommLog.TabIndex = 0;
		this.tabInfo.Controls.Add(this.label14);
		this.tabInfo.Location = new System.Drawing.Point(4, 27);
		this.tabInfo.Name = "tabInfo";
		this.tabInfo.Size = new System.Drawing.Size(1143, 390);
		this.tabInfo.TabIndex = 3;
		this.tabInfo.Text = "Info";
		this.tabInfo.UseVisualStyleBackColor = true;
		this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.label14.BackColor = System.Drawing.Color.Black;
		this.label14.Font = new System.Drawing.Font("Calibri", 14.25f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
		this.label14.ForeColor = System.Drawing.Color.Lime;
		this.label14.Location = new System.Drawing.Point(0, 0);
		this.label14.Margin = new System.Windows.Forms.Padding(20);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(1083, 394);
		this.label14.TabIndex = 5;
		this.label14.Text = resources.GetString("label14.Text");
		this.tabBuddyWorld.BackColor = System.Drawing.Color.Black;
		this.tabBuddyWorld.Controls.Add(this.grpPayLoad);
		this.tabBuddyWorld.Controls.Add(this.groupBox15);
		this.tabBuddyWorld.Controls.Add(this.groupBox13);
		this.tabBuddyWorld.Controls.Add(this.groupBox16);
		this.tabBuddyWorld.Controls.Add(this.groupBox14);
		this.tabBuddyWorld.Location = new System.Drawing.Point(4, 27);
		this.tabBuddyWorld.Name = "tabBuddyWorld";
		this.tabBuddyWorld.Padding = new System.Windows.Forms.Padding(3);
		this.tabBuddyWorld.Size = new System.Drawing.Size(1143, 390);
		this.tabBuddyWorld.TabIndex = 15;
		this.tabBuddyWorld.Text = "Buddy World";
		this.tabBuddyWorld.Click += new System.EventHandler(tabBuddyWorld_Click);
		this.grpPayLoad.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.grpPayLoad.BackColor = System.Drawing.Color.Black;
		this.grpPayLoad.Controls.Add(this.panelPayload);
		this.grpPayLoad.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpPayLoad.ForeColor = System.Drawing.Color.White;
		this.grpPayLoad.Location = new System.Drawing.Point(8, 288);
		this.grpPayLoad.Name = "grpPayLoad";
		this.grpPayLoad.Size = new System.Drawing.Size(1125, 100);
		this.grpPayLoad.TabIndex = 95;
		this.grpPayLoad.TabStop = false;
		this.grpPayLoad.Text = "Payload";
		this.panelPayload.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panelPayload.AutoScroll = true;
		this.panelPayload.BackColor = System.Drawing.Color.Black;
		this.panelPayload.Location = new System.Drawing.Point(8, 13);
		this.panelPayload.Name = "panelPayload";
		this.panelPayload.Size = new System.Drawing.Size(1109, 81);
		this.panelPayload.TabIndex = 19;
		this.groupBox15.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox15.BackColor = System.Drawing.Color.Black;
		this.groupBox15.Controls.Add(this.button1);
		this.groupBox15.Controls.Add(this.txtHomeBase);
		this.groupBox15.Controls.Add(this.btnHomeBase);
		this.groupBox15.Controls.Add(this.btnAdvertisement);
		this.groupBox15.Controls.Add(this.label84);
		this.groupBox15.Controls.Add(this.labReputation);
		this.groupBox15.Controls.Add(this.label103);
		this.groupBox15.Controls.Add(this.nudPilotWeight);
		this.groupBox15.Controls.Add(this.labPilotPositionDescription);
		this.groupBox15.Controls.Add(this.btnTravelToAirplane);
		this.groupBox15.Controls.Add(this.label92);
		this.groupBox15.Controls.Add(this.txtIcaoNewPosition);
		this.groupBox15.Controls.Add(this.label83);
		this.groupBox15.Controls.Add(this.btnTravelToICAO);
		this.groupBox15.Controls.Add(this.btnPilotPosition);
		this.groupBox15.Controls.Add(this.labCash);
		this.groupBox15.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox15.ForeColor = System.Drawing.Color.White;
		this.groupBox15.Location = new System.Drawing.Point(647, 0);
		this.groupBox15.Name = "groupBox15";
		this.groupBox15.Size = new System.Drawing.Size(486, 133);
		this.groupBox15.TabIndex = 93;
		this.groupBox15.TabStop = false;
		this.groupBox15.Text = "Pilot";
		this.button1.BackColor = System.Drawing.Color.Black;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.button1.ForeColor = System.Drawing.Color.Black;
		this.button1.Image = NavBuddy.Properties.Resources.user;
		this.button1.Location = new System.Drawing.Point(10, 18);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(20, 20);
		this.button1.TabIndex = 110;
		this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.button1.UseVisualStyleBackColor = false;
		this.txtHomeBase.BackColor = System.Drawing.Color.White;
		this.txtHomeBase.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtHomeBase.ForeColor = System.Drawing.Color.Black;
		this.txtHomeBase.Location = new System.Drawing.Point(405, 94);
		this.txtHomeBase.Margin = new System.Windows.Forms.Padding(0);
		this.txtHomeBase.Name = "txtHomeBase";
		this.txtHomeBase.Size = new System.Drawing.Size(65, 26);
		this.txtHomeBase.TabIndex = 109;
		this.btnHomeBase.BackColor = System.Drawing.Color.Gray;
		this.btnHomeBase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnHomeBase.ForeColor = System.Drawing.Color.Black;
		this.btnHomeBase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnHomeBase.Location = new System.Drawing.Point(325, 94);
		this.btnHomeBase.Name = "btnHomeBase";
		this.btnHomeBase.Size = new System.Drawing.Size(72, 26);
		this.btnHomeBase.TabIndex = 108;
		this.btnHomeBase.Text = "HOME";
		this.btnHomeBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnHomeBase.UseVisualStyleBackColor = false;
		this.btnHomeBase.Click += new System.EventHandler(btnHomeBase_Click);
		this.btnAdvertisement.BackColor = System.Drawing.Color.Gray;
		this.btnAdvertisement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAdvertisement.ForeColor = System.Drawing.Color.Black;
		this.btnAdvertisement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAdvertisement.Location = new System.Drawing.Point(352, 16);
		this.btnAdvertisement.Name = "btnAdvertisement";
		this.btnAdvertisement.Size = new System.Drawing.Size(45, 26);
		this.btnAdvertisement.TabIndex = 107;
		this.btnAdvertisement.Text = "ADV";
		this.btnAdvertisement.UseVisualStyleBackColor = false;
		this.btnAdvertisement.Click += new System.EventHandler(btnAdvertisement_Click);
		this.label84.AutoSize = true;
		this.label84.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label84.ForeColor = System.Drawing.Color.White;
		this.label84.Location = new System.Drawing.Point(174, 21);
		this.label84.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label84.Name = "label84";
		this.label84.Size = new System.Drawing.Size(80, 18);
		this.label84.TabIndex = 106;
		this.label84.Text = "Reputation:";
		this.label84.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labReputation.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labReputation.ForeColor = System.Drawing.Color.Lime;
		this.labReputation.Location = new System.Drawing.Point(261, 22);
		this.labReputation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labReputation.Name = "labReputation";
		this.labReputation.Size = new System.Drawing.Size(40, 18);
		this.labReputation.TabIndex = 105;
		this.labReputation.Text = "---";
		this.labReputation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label103.AutoSize = true;
		this.label103.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label103.ForeColor = System.Drawing.Color.White;
		this.label103.Location = new System.Drawing.Point(174, 58);
		this.label103.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label103.Name = "label103";
		this.label103.Size = new System.Drawing.Size(59, 18);
		this.label103.TabIndex = 103;
		this.label103.Text = "Weight: ";
		this.label103.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.nudPilotWeight.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudPilotWeight.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudPilotWeight.Location = new System.Drawing.Point(246, 56);
		this.nudPilotWeight.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.nudPilotWeight.Name = "nudPilotWeight";
		this.nudPilotWeight.Size = new System.Drawing.Size(55, 23);
		this.nudPilotWeight.TabIndex = 102;
		this.nudPilotWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudPilotWeight.Value = new decimal(new int[4] { 170, 0, 0, 0 });
		this.nudPilotWeight.ValueChanged += new System.EventHandler(nudPilotWeight_ValueChanged);
		this.labPilotPositionDescription.AutoSize = true;
		this.labPilotPositionDescription.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labPilotPositionDescription.ForeColor = System.Drawing.Color.Lime;
		this.labPilotPositionDescription.Location = new System.Drawing.Point(130, 59);
		this.labPilotPositionDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labPilotPositionDescription.Name = "labPilotPositionDescription";
		this.labPilotPositionDescription.Size = new System.Drawing.Size(23, 18);
		this.labPilotPositionDescription.TabIndex = 101;
		this.labPilotPositionDescription.Text = "---";
		this.labPilotPositionDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnTravelToAirplane.BackColor = System.Drawing.Color.Gray;
		this.btnTravelToAirplane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnTravelToAirplane.ForeColor = System.Drawing.Color.Black;
		this.btnTravelToAirplane.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnTravelToAirplane.Location = new System.Drawing.Point(172, 94);
		this.btnTravelToAirplane.Name = "btnTravelToAirplane";
		this.btnTravelToAirplane.Size = new System.Drawing.Size(131, 26);
		this.btnTravelToAirplane.TabIndex = 100;
		this.btnTravelToAirplane.Text = "TRAVEL TO AIRPLANE";
		this.btnTravelToAirplane.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnTravelToAirplane.UseVisualStyleBackColor = false;
		this.btnTravelToAirplane.Click += new System.EventHandler(btnTravelToAirplane_Click);
		this.label92.AutoSize = true;
		this.label92.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label92.ForeColor = System.Drawing.Color.White;
		this.label92.Location = new System.Drawing.Point(42, 22);
		this.label92.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label92.Name = "label92";
		this.label92.Size = new System.Drawing.Size(41, 18);
		this.label92.TabIndex = 99;
		this.label92.Text = "Cash:";
		this.label92.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtIcaoNewPosition.BackColor = System.Drawing.Color.White;
		this.txtIcaoNewPosition.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtIcaoNewPosition.ForeColor = System.Drawing.Color.Black;
		this.txtIcaoNewPosition.Location = new System.Drawing.Point(90, 94);
		this.txtIcaoNewPosition.Margin = new System.Windows.Forms.Padding(0);
		this.txtIcaoNewPosition.Name = "txtIcaoNewPosition";
		this.txtIcaoNewPosition.Size = new System.Drawing.Size(65, 26);
		this.txtIcaoNewPosition.TabIndex = 98;
		this.label83.AutoSize = true;
		this.label83.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label83.ForeColor = System.Drawing.Color.White;
		this.label83.Location = new System.Drawing.Point(30, 58);
		this.label83.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label83.Name = "label83";
		this.label83.Size = new System.Drawing.Size(62, 18);
		this.label83.TabIndex = 97;
		this.label83.Text = "Position:";
		this.label83.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnTravelToICAO.BackColor = System.Drawing.Color.Gray;
		this.btnTravelToICAO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnTravelToICAO.ForeColor = System.Drawing.Color.Black;
		this.btnTravelToICAO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnTravelToICAO.Location = new System.Drawing.Point(10, 94);
		this.btnTravelToICAO.Name = "btnTravelToICAO";
		this.btnTravelToICAO.Size = new System.Drawing.Size(72, 26);
		this.btnTravelToICAO.TabIndex = 96;
		this.btnTravelToICAO.Text = "TRAVEL TO";
		this.btnTravelToICAO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnTravelToICAO.UseVisualStyleBackColor = false;
		this.btnTravelToICAO.Click += new System.EventHandler(btnTravelTo_Click);
		this.btnPilotPosition.BackColor = System.Drawing.Color.Black;
		this.btnPilotPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnPilotPosition.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnPilotPosition.ForeColor = System.Drawing.Color.Black;
		this.btnPilotPosition.Image = NavBuddy.Properties.Resources.googlelink;
		this.btnPilotPosition.Location = new System.Drawing.Point(7, 57);
		this.btnPilotPosition.Name = "btnPilotPosition";
		this.btnPilotPosition.Size = new System.Drawing.Size(20, 20);
		this.btnPilotPosition.TabIndex = 95;
		this.btnPilotPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnPilotPosition.UseVisualStyleBackColor = false;
		this.btnPilotPosition.Click += new System.EventHandler(btnPilotPosition_Click);
		this.labCash.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labCash.ForeColor = System.Drawing.Color.Lime;
		this.labCash.Location = new System.Drawing.Point(92, 22);
		this.labCash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labCash.Name = "labCash";
		this.labCash.Size = new System.Drawing.Size(62, 18);
		this.labCash.TabIndex = 89;
		this.labCash.Text = "---";
		this.labCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.groupBox13.BackColor = System.Drawing.Color.Black;
		this.groupBox13.Controls.Add(this.btnRentQuotedPlane);
		this.groupBox13.Controls.Add(this.btnBuyQuotedPlane);
		this.groupBox13.Controls.Add(this.lblAirplaneQuotation);
		this.groupBox13.Controls.Add(this.btnGetPlaneQuotation);
		this.groupBox13.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox13.ForeColor = System.Drawing.Color.White;
		this.groupBox13.Location = new System.Drawing.Point(8, 0);
		this.groupBox13.Name = "groupBox13";
		this.groupBox13.Size = new System.Drawing.Size(633, 53);
		this.groupBox13.TabIndex = 28;
		this.groupBox13.TabStop = false;
		this.groupBox13.Text = "Purchase new Airplane";
		this.btnRentQuotedPlane.BackColor = System.Drawing.Color.Gray;
		this.btnRentQuotedPlane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnRentQuotedPlane.ForeColor = System.Drawing.Color.Black;
		this.btnRentQuotedPlane.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRentQuotedPlane.Location = new System.Drawing.Point(144, 18);
		this.btnRentQuotedPlane.Name = "btnRentQuotedPlane";
		this.btnRentQuotedPlane.Size = new System.Drawing.Size(56, 26);
		this.btnRentQuotedPlane.TabIndex = 91;
		this.btnRentQuotedPlane.Text = "RENT";
		this.btnRentQuotedPlane.UseVisualStyleBackColor = false;
		this.btnRentQuotedPlane.Click += new System.EventHandler(btnRentQuotedPlane_Click);
		this.btnBuyQuotedPlane.BackColor = System.Drawing.Color.Gray;
		this.btnBuyQuotedPlane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnBuyQuotedPlane.ForeColor = System.Drawing.Color.Black;
		this.btnBuyQuotedPlane.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBuyQuotedPlane.Location = new System.Drawing.Point(82, 18);
		this.btnBuyQuotedPlane.Name = "btnBuyQuotedPlane";
		this.btnBuyQuotedPlane.Size = new System.Drawing.Size(56, 26);
		this.btnBuyQuotedPlane.TabIndex = 90;
		this.btnBuyQuotedPlane.Text = "BUY";
		this.btnBuyQuotedPlane.UseVisualStyleBackColor = false;
		this.btnBuyQuotedPlane.Click += new System.EventHandler(btnBuyQuotedPlane_Click);
		this.lblAirplaneQuotation.AutoSize = true;
		this.lblAirplaneQuotation.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblAirplaneQuotation.ForeColor = System.Drawing.Color.Lime;
		this.lblAirplaneQuotation.Location = new System.Drawing.Point(207, 12);
		this.lblAirplaneQuotation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblAirplaneQuotation.Name = "lblAirplaneQuotation";
		this.lblAirplaneQuotation.Size = new System.Drawing.Size(23, 36);
		this.lblAirplaneQuotation.TabIndex = 89;
		this.lblAirplaneQuotation.Text = "---\r\n---";
		this.lblAirplaneQuotation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGetPlaneQuotation.BackColor = System.Drawing.Color.Gray;
		this.btnGetPlaneQuotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGetPlaneQuotation.ForeColor = System.Drawing.Color.Black;
		this.btnGetPlaneQuotation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGetPlaneQuotation.Location = new System.Drawing.Point(9, 18);
		this.btnGetPlaneQuotation.Name = "btnGetPlaneQuotation";
		this.btnGetPlaneQuotation.Size = new System.Drawing.Size(67, 26);
		this.btnGetPlaneQuotation.TabIndex = 67;
		this.btnGetPlaneQuotation.Text = "QUOTATE";
		this.btnGetPlaneQuotation.UseVisualStyleBackColor = false;
		this.btnGetPlaneQuotation.Click += new System.EventHandler(btnGetPlaneQuotation_click);
		this.groupBox16.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox16.BackColor = System.Drawing.Color.Black;
		this.groupBox16.Controls.Add(this.labFlightRequiredPayload);
		this.groupBox16.Controls.Add(this.label105);
		this.groupBox16.Controls.Add(this.btnAbortFlight);
		this.groupBox16.Controls.Add(this.btnEndflight);
		this.groupBox16.Controls.Add(this.labFlightStatus);
		this.groupBox16.Controls.Add(this.btnstartFlight);
		this.groupBox16.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox16.ForeColor = System.Drawing.Color.White;
		this.groupBox16.Location = new System.Drawing.Point(646, 134);
		this.groupBox16.Name = "groupBox16";
		this.groupBox16.Size = new System.Drawing.Size(486, 155);
		this.groupBox16.TabIndex = 94;
		this.groupBox16.TabStop = false;
		this.groupBox16.Text = "Flight";
		this.labFlightRequiredPayload.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labFlightRequiredPayload.ForeColor = System.Drawing.Color.Lime;
		this.labFlightRequiredPayload.Location = new System.Drawing.Point(429, 32);
		this.labFlightRequiredPayload.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labFlightRequiredPayload.Name = "labFlightRequiredPayload";
		this.labFlightRequiredPayload.Size = new System.Drawing.Size(42, 18);
		this.labFlightRequiredPayload.TabIndex = 102;
		this.labFlightRequiredPayload.Text = "---";
		this.labFlightRequiredPayload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label105.AutoSize = true;
		this.label105.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label105.ForeColor = System.Drawing.Color.White;
		this.label105.Location = new System.Drawing.Point(300, 32);
		this.label105.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label105.Name = "label105";
		this.label105.Size = new System.Drawing.Size(121, 18);
		this.label105.TabIndex = 101;
		this.label105.Text = "Required payload:";
		this.label105.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAbortFlight.BackColor = System.Drawing.Color.Gray;
		this.btnAbortFlight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAbortFlight.ForeColor = System.Drawing.Color.Black;
		this.btnAbortFlight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAbortFlight.Location = new System.Drawing.Point(202, 28);
		this.btnAbortFlight.Name = "btnAbortFlight";
		this.btnAbortFlight.Size = new System.Drawing.Size(93, 26);
		this.btnAbortFlight.TabIndex = 100;
		this.btnAbortFlight.Text = "ABORT FLIGHT";
		this.btnAbortFlight.UseVisualStyleBackColor = false;
		this.btnAbortFlight.Click += new System.EventHandler(btnAbortFlight_Click);
		this.btnEndflight.BackColor = System.Drawing.Color.Gray;
		this.btnEndflight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnEndflight.ForeColor = System.Drawing.Color.Black;
		this.btnEndflight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnEndflight.Location = new System.Drawing.Point(107, 28);
		this.btnEndflight.Name = "btnEndflight";
		this.btnEndflight.Size = new System.Drawing.Size(93, 26);
		this.btnEndflight.TabIndex = 99;
		this.btnEndflight.Text = "END FLIGHT";
		this.btnEndflight.UseVisualStyleBackColor = false;
		this.btnEndflight.Click += new System.EventHandler(btnEndFlight_Click);
		this.labFlightStatus.AutoSize = true;
		this.labFlightStatus.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labFlightStatus.ForeColor = System.Drawing.Color.White;
		this.labFlightStatus.Location = new System.Drawing.Point(13, 73);
		this.labFlightStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labFlightStatus.Name = "labFlightStatus";
		this.labFlightStatus.Size = new System.Drawing.Size(132, 18);
		this.labFlightStatus.TabIndex = 98;
		this.labFlightStatus.Text = "No flight in progress";
		this.labFlightStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnstartFlight.BackColor = System.Drawing.Color.Gray;
		this.btnstartFlight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnstartFlight.ForeColor = System.Drawing.Color.Black;
		this.btnstartFlight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnstartFlight.Location = new System.Drawing.Point(12, 28);
		this.btnstartFlight.Name = "btnstartFlight";
		this.btnstartFlight.Size = new System.Drawing.Size(93, 26);
		this.btnstartFlight.TabIndex = 93;
		this.btnstartFlight.Text = "START FLIGHT";
		this.btnstartFlight.UseVisualStyleBackColor = false;
		this.btnstartFlight.Click += new System.EventHandler(btnstartFlight_Click);
		this.groupBox14.BackColor = System.Drawing.Color.Black;
		this.groupBox14.Controls.Add(this.panelWorldAirplanes);
		this.groupBox14.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox14.ForeColor = System.Drawing.Color.White;
		this.groupBox14.Location = new System.Drawing.Point(8, 50);
		this.groupBox14.Name = "groupBox14";
		this.groupBox14.Size = new System.Drawing.Size(632, 239);
		this.groupBox14.TabIndex = 92;
		this.groupBox14.TabStop = false;
		this.groupBox14.Text = "Airplanes";
		this.panelWorldAirplanes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panelWorldAirplanes.AutoScroll = true;
		this.panelWorldAirplanes.Controls.Add(this.labNextAvailableMoment);
		this.panelWorldAirplanes.Controls.Add(this.label102);
		this.panelWorldAirplanes.Controls.Add(this.btnWaypointSelectedAirplane);
		this.panelWorldAirplanes.Controls.Add(this.btnAirplaneEngineMaitenanceRepair);
		this.panelWorldAirplanes.Controls.Add(this.labAirplaneEngineStatus);
		this.panelWorldAirplanes.Controls.Add(this.label107);
		this.panelWorldAirplanes.Controls.Add(this.labAirplaneMarketPrice);
		this.panelWorldAirplanes.Controls.Add(this.label96);
		this.panelWorldAirplanes.Controls.Add(this.listAirplanes);
		this.panelWorldAirplanes.Controls.Add(this.btnAirplaneBodyMaitenanceRepair);
		this.panelWorldAirplanes.Controls.Add(this.btnSellAirplane);
		this.panelWorldAirplanes.Controls.Add(this.label94);
		this.panelWorldAirplanes.Controls.Add(this.lblSelectedAirplane);
		this.panelWorldAirplanes.Controls.Add(this.labAirplaneMileage);
		this.panelWorldAirplanes.Controls.Add(this.labAirplaneBodyStatus);
		this.panelWorldAirplanes.Controls.Add(this.label98);
		this.panelWorldAirplanes.Controls.Add(this.labAirplaneCompleteFlights);
		this.panelWorldAirplanes.Controls.Add(this.label99);
		this.panelWorldAirplanes.Controls.Add(this.label97);
		this.panelWorldAirplanes.Controls.Add(this.nupRefuel);
		this.panelWorldAirplanes.Controls.Add(this.label95);
		this.panelWorldAirplanes.Controls.Add(this.btnRefuel);
		this.panelWorldAirplanes.Controls.Add(this.labSelectedAirplaneFuel);
		this.panelWorldAirplanes.Controls.Add(this.label93);
		this.panelWorldAirplanes.Controls.Add(this.label40);
		this.panelWorldAirplanes.Controls.Add(this.lblAirplaneFlightHours);
		this.panelWorldAirplanes.Location = new System.Drawing.Point(1, 15);
		this.panelWorldAirplanes.Margin = new System.Windows.Forms.Padding(0);
		this.panelWorldAirplanes.Name = "panelWorldAirplanes";
		this.panelWorldAirplanes.Size = new System.Drawing.Size(625, 220);
		this.panelWorldAirplanes.TabIndex = 114;
		this.labNextAvailableMoment.AutoSize = true;
		this.labNextAvailableMoment.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labNextAvailableMoment.ForeColor = System.Drawing.Color.Lime;
		this.labNextAvailableMoment.Location = new System.Drawing.Point(496, 192);
		this.labNextAvailableMoment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labNextAvailableMoment.Name = "labNextAvailableMoment";
		this.labNextAvailableMoment.Size = new System.Drawing.Size(23, 18);
		this.labNextAvailableMoment.TabIndex = 122;
		this.labNextAvailableMoment.Text = "---";
		this.labNextAvailableMoment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label102.AutoSize = true;
		this.label102.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label102.ForeColor = System.Drawing.Color.White;
		this.label102.Location = new System.Drawing.Point(387, 192);
		this.label102.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label102.Name = "label102";
		this.label102.Size = new System.Drawing.Size(82, 18);
		this.label102.TabIndex = 121;
		this.label102.Text = "Availability:";
		this.label102.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnWaypointSelectedAirplane.BackColor = System.Drawing.Color.Black;
		this.btnWaypointSelectedAirplane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnWaypointSelectedAirplane.ForeColor = System.Drawing.Color.Lime;
		this.btnWaypointSelectedAirplane.Image = NavBuddy.Properties.Resources.googlelink;
		this.btnWaypointSelectedAirplane.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnWaypointSelectedAirplane.Location = new System.Drawing.Point(182, 80);
		this.btnWaypointSelectedAirplane.Margin = new System.Windows.Forms.Padding(0);
		this.btnWaypointSelectedAirplane.Name = "btnWaypointSelectedAirplane";
		this.btnWaypointSelectedAirplane.Size = new System.Drawing.Size(108, 26);
		this.btnWaypointSelectedAirplane.TabIndex = 120;
		this.btnWaypointSelectedAirplane.Text = "----";
		this.btnWaypointSelectedAirplane.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnWaypointSelectedAirplane.UseVisualStyleBackColor = false;
		this.btnWaypointSelectedAirplane.Click += new System.EventHandler(btnWaypointButton_Click);
		this.btnAirplaneEngineMaitenanceRepair.BackColor = System.Drawing.Color.Gray;
		this.btnAirplaneEngineMaitenanceRepair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAirplaneEngineMaitenanceRepair.ForeColor = System.Drawing.Color.Black;
		this.btnAirplaneEngineMaitenanceRepair.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAirplaneEngineMaitenanceRepair.Location = new System.Drawing.Point(182, 188);
		this.btnAirplaneEngineMaitenanceRepair.Margin = new System.Windows.Forms.Padding(0);
		this.btnAirplaneEngineMaitenanceRepair.Name = "btnAirplaneEngineMaitenanceRepair";
		this.btnAirplaneEngineMaitenanceRepair.Size = new System.Drawing.Size(108, 26);
		this.btnAirplaneEngineMaitenanceRepair.TabIndex = 119;
		this.btnAirplaneEngineMaitenanceRepair.Text = "MAINTENANCE";
		this.btnAirplaneEngineMaitenanceRepair.UseVisualStyleBackColor = false;
		this.btnAirplaneEngineMaitenanceRepair.Click += new System.EventHandler(btnAirplaneEngineMaitenanceRepair_Click);
		this.labAirplaneEngineStatus.AutoSize = true;
		this.labAirplaneEngineStatus.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAirplaneEngineStatus.ForeColor = System.Drawing.Color.Lime;
		this.labAirplaneEngineStatus.Location = new System.Drawing.Point(132, 192);
		this.labAirplaneEngineStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAirplaneEngineStatus.Name = "labAirplaneEngineStatus";
		this.labAirplaneEngineStatus.Size = new System.Drawing.Size(23, 18);
		this.labAirplaneEngineStatus.TabIndex = 118;
		this.labAirplaneEngineStatus.Text = "---";
		this.labAirplaneEngineStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label107.AutoSize = true;
		this.label107.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label107.ForeColor = System.Drawing.Color.White;
		this.label107.Location = new System.Drawing.Point(2, 192);
		this.label107.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label107.Name = "label107";
		this.label107.Size = new System.Drawing.Size(94, 18);
		this.label107.TabIndex = 117;
		this.label107.Text = "Engine status:";
		this.label107.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labAirplaneMarketPrice.AutoSize = true;
		this.labAirplaneMarketPrice.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAirplaneMarketPrice.ForeColor = System.Drawing.Color.Lime;
		this.labAirplaneMarketPrice.Location = new System.Drawing.Point(496, 165);
		this.labAirplaneMarketPrice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAirplaneMarketPrice.Name = "labAirplaneMarketPrice";
		this.labAirplaneMarketPrice.Size = new System.Drawing.Size(23, 18);
		this.labAirplaneMarketPrice.TabIndex = 115;
		this.labAirplaneMarketPrice.Text = "---";
		this.labAirplaneMarketPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label96.AutoSize = true;
		this.label96.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label96.ForeColor = System.Drawing.Color.White;
		this.label96.Location = new System.Drawing.Point(385, 165);
		this.label96.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label96.Name = "label96";
		this.label96.Size = new System.Drawing.Size(90, 18);
		this.label96.TabIndex = 114;
		this.label96.Text = "Market price:";
		this.label96.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.listAirplanes.FormattingEnabled = true;
		this.listAirplanes.ItemHeight = 15;
		this.listAirplanes.Location = new System.Drawing.Point(5, 1);
		this.listAirplanes.Name = "listAirplanes";
		this.listAirplanes.Size = new System.Drawing.Size(615, 49);
		this.listAirplanes.TabIndex = 91;
		this.listAirplanes.SelectedIndexChanged += new System.EventHandler(listAirplanes_SelectedIndexChanged);
		this.btnAirplaneBodyMaitenanceRepair.BackColor = System.Drawing.Color.Gray;
		this.btnAirplaneBodyMaitenanceRepair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAirplaneBodyMaitenanceRepair.ForeColor = System.Drawing.Color.Black;
		this.btnAirplaneBodyMaitenanceRepair.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAirplaneBodyMaitenanceRepair.Location = new System.Drawing.Point(182, 161);
		this.btnAirplaneBodyMaitenanceRepair.Margin = new System.Windows.Forms.Padding(0);
		this.btnAirplaneBodyMaitenanceRepair.Name = "btnAirplaneBodyMaitenanceRepair";
		this.btnAirplaneBodyMaitenanceRepair.Size = new System.Drawing.Size(108, 26);
		this.btnAirplaneBodyMaitenanceRepair.TabIndex = 112;
		this.btnAirplaneBodyMaitenanceRepair.Text = "MAINTENANCE";
		this.btnAirplaneBodyMaitenanceRepair.UseVisualStyleBackColor = false;
		this.btnAirplaneBodyMaitenanceRepair.Click += new System.EventHandler(btnAirplaneMaitenanceRepair_Click);
		this.btnSellAirplane.BackColor = System.Drawing.Color.Gray;
		this.btnSellAirplane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSellAirplane.ForeColor = System.Drawing.Color.Black;
		this.btnSellAirplane.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSellAirplane.Location = new System.Drawing.Point(307, 161);
		this.btnSellAirplane.Name = "btnSellAirplane";
		this.btnSellAirplane.Size = new System.Drawing.Size(57, 26);
		this.btnSellAirplane.TabIndex = 93;
		this.btnSellAirplane.Text = "SELL";
		this.btnSellAirplane.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSellAirplane.UseVisualStyleBackColor = false;
		this.btnSellAirplane.Click += new System.EventHandler(btnSellAirplane_Click);
		this.label94.AutoSize = true;
		this.label94.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label94.ForeColor = System.Drawing.Color.White;
		this.label94.Location = new System.Drawing.Point(2, 57);
		this.label94.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label94.Name = "label94";
		this.label94.Size = new System.Drawing.Size(122, 18);
		this.label94.TabIndex = 100;
		this.label94.Text = "Selected Airplane:";
		this.label94.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblSelectedAirplane.AutoSize = true;
		this.lblSelectedAirplane.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblSelectedAirplane.ForeColor = System.Drawing.Color.Lime;
		this.lblSelectedAirplane.Location = new System.Drawing.Point(132, 57);
		this.lblSelectedAirplane.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblSelectedAirplane.Name = "lblSelectedAirplane";
		this.lblSelectedAirplane.Size = new System.Drawing.Size(23, 18);
		this.lblSelectedAirplane.TabIndex = 92;
		this.lblSelectedAirplane.Text = "---";
		this.lblSelectedAirplane.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labAirplaneMileage.AutoSize = true;
		this.labAirplaneMileage.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAirplaneMileage.ForeColor = System.Drawing.Color.Lime;
		this.labAirplaneMileage.Location = new System.Drawing.Point(496, 138);
		this.labAirplaneMileage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAirplaneMileage.Name = "labAirplaneMileage";
		this.labAirplaneMileage.Size = new System.Drawing.Size(23, 18);
		this.labAirplaneMileage.TabIndex = 109;
		this.labAirplaneMileage.Text = "---";
		this.labAirplaneMileage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labAirplaneBodyStatus.AutoSize = true;
		this.labAirplaneBodyStatus.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAirplaneBodyStatus.ForeColor = System.Drawing.Color.Lime;
		this.labAirplaneBodyStatus.Location = new System.Drawing.Point(132, 165);
		this.labAirplaneBodyStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAirplaneBodyStatus.Name = "labAirplaneBodyStatus";
		this.labAirplaneBodyStatus.Size = new System.Drawing.Size(23, 18);
		this.labAirplaneBodyStatus.TabIndex = 111;
		this.labAirplaneBodyStatus.Text = "---";
		this.labAirplaneBodyStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label98.AutoSize = true;
		this.label98.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label98.ForeColor = System.Drawing.Color.White;
		this.label98.Location = new System.Drawing.Point(389, 138);
		this.label98.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label98.Name = "label98";
		this.label98.Size = new System.Drawing.Size(62, 18);
		this.label98.TabIndex = 108;
		this.label98.Text = "Mileage:";
		this.label98.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labAirplaneCompleteFlights.AutoSize = true;
		this.labAirplaneCompleteFlights.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAirplaneCompleteFlights.ForeColor = System.Drawing.Color.Lime;
		this.labAirplaneCompleteFlights.Location = new System.Drawing.Point(263, 138);
		this.labAirplaneCompleteFlights.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAirplaneCompleteFlights.Name = "labAirplaneCompleteFlights";
		this.labAirplaneCompleteFlights.Size = new System.Drawing.Size(23, 18);
		this.labAirplaneCompleteFlights.TabIndex = 107;
		this.labAirplaneCompleteFlights.Text = "---";
		this.labAirplaneCompleteFlights.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label99.AutoSize = true;
		this.label99.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label99.ForeColor = System.Drawing.Color.White;
		this.label99.Location = new System.Drawing.Point(2, 165);
		this.label99.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label99.Name = "label99";
		this.label99.Size = new System.Drawing.Size(83, 18);
		this.label99.TabIndex = 110;
		this.label99.Text = "Body status:";
		this.label99.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label97.AutoSize = true;
		this.label97.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label97.ForeColor = System.Drawing.Color.White;
		this.label97.Location = new System.Drawing.Point(180, 138);
		this.label97.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label97.Name = "label97";
		this.label97.Size = new System.Drawing.Size(53, 18);
		this.label97.TabIndex = 106;
		this.label97.Text = "Flights:";
		this.label97.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.nupRefuel.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nupRefuel.DecimalPlaces = 1;
		this.nupRefuel.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nupRefuel.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.nupRefuel.Location = new System.Drawing.Point(394, 109);
		this.nupRefuel.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.nupRefuel.Name = "nupRefuel";
		this.nupRefuel.Size = new System.Drawing.Size(55, 23);
		this.nupRefuel.TabIndex = 96;
		this.nupRefuel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nupRefuel.Value = new decimal(new int[4] { 10, 0, 0, 0 });
		this.label95.AutoSize = true;
		this.label95.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label95.ForeColor = System.Drawing.Color.White;
		this.label95.Location = new System.Drawing.Point(2, 84);
		this.label95.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label95.Name = "label95";
		this.label95.Size = new System.Drawing.Size(62, 18);
		this.label95.TabIndex = 101;
		this.label95.Text = "Position:";
		this.label95.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRefuel.BackColor = System.Drawing.Color.Gray;
		this.btnRefuel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnRefuel.ForeColor = System.Drawing.Color.Black;
		this.btnRefuel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRefuel.Location = new System.Drawing.Point(308, 107);
		this.btnRefuel.Name = "btnRefuel";
		this.btnRefuel.Size = new System.Drawing.Size(57, 26);
		this.btnRefuel.TabIndex = 95;
		this.btnRefuel.Text = "REFUEL";
		this.btnRefuel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRefuel.UseVisualStyleBackColor = false;
		this.btnRefuel.Click += new System.EventHandler(btnRefuel_Click);
		this.labSelectedAirplaneFuel.AutoSize = true;
		this.labSelectedAirplaneFuel.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labSelectedAirplaneFuel.ForeColor = System.Drawing.Color.Lime;
		this.labSelectedAirplaneFuel.Location = new System.Drawing.Point(132, 111);
		this.labSelectedAirplaneFuel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labSelectedAirplaneFuel.Name = "labSelectedAirplaneFuel";
		this.labSelectedAirplaneFuel.Size = new System.Drawing.Size(23, 18);
		this.labSelectedAirplaneFuel.TabIndex = 102;
		this.labSelectedAirplaneFuel.Text = "---";
		this.labSelectedAirplaneFuel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label93.AutoSize = true;
		this.label93.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label93.ForeColor = System.Drawing.Color.White;
		this.label93.Location = new System.Drawing.Point(2, 111);
		this.label93.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label93.Name = "label93";
		this.label93.Size = new System.Drawing.Size(39, 18);
		this.label93.TabIndex = 103;
		this.label93.Text = "Fuel:";
		this.label93.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label40.AutoSize = true;
		this.label40.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label40.ForeColor = System.Drawing.Color.White;
		this.label40.Location = new System.Drawing.Point(2, 138);
		this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label40.Name = "label40";
		this.label40.Size = new System.Drawing.Size(85, 18);
		this.label40.TabIndex = 104;
		this.label40.Text = "Flight hours:";
		this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblAirplaneFlightHours.AutoSize = true;
		this.lblAirplaneFlightHours.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblAirplaneFlightHours.ForeColor = System.Drawing.Color.Lime;
		this.lblAirplaneFlightHours.Location = new System.Drawing.Point(132, 138);
		this.lblAirplaneFlightHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.lblAirplaneFlightHours.Name = "lblAirplaneFlightHours";
		this.lblAirplaneFlightHours.Size = new System.Drawing.Size(23, 18);
		this.lblAirplaneFlightHours.TabIndex = 105;
		this.lblAirplaneFlightHours.Text = "---";
		this.lblAirplaneFlightHours.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tabActivities.BackColor = System.Drawing.Color.Black;
		this.tabActivities.Controls.Add(this.groupBox17);
		this.tabActivities.Controls.Add(this.groupBoxAssignedActivities);
		this.tabActivities.Location = new System.Drawing.Point(4, 27);
		this.tabActivities.Name = "tabActivities";
		this.tabActivities.Size = new System.Drawing.Size(1143, 390);
		this.tabActivities.TabIndex = 16;
		this.tabActivities.Text = "Activities";
		this.groupBox17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox17.BackColor = System.Drawing.Color.Black;
		this.groupBox17.Controls.Add(this.label104);
		this.groupBox17.Controls.Add(this.btnGenerateActivitiesAtUserPos);
		this.groupBox17.Controls.Add(this.btnGenerateActivitiesAtHome);
		this.groupBox17.Controls.Add(this.ActivitySearchPanel);
		this.groupBox17.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox17.ForeColor = System.Drawing.Color.White;
		this.groupBox17.Location = new System.Drawing.Point(5, 181);
		this.groupBox17.Name = "groupBox17";
		this.groupBox17.Size = new System.Drawing.Size(1128, 206);
		this.groupBox17.TabIndex = 94;
		this.groupBox17.TabStop = false;
		this.groupBox17.Text = "Search activity";
		this.label104.AutoSize = true;
		this.label104.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label104.ForeColor = System.Drawing.Color.White;
		this.label104.Location = new System.Drawing.Point(7, 19);
		this.label104.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label104.Name = "label104";
		this.label104.Size = new System.Drawing.Size(70, 18);
		this.label104.TabIndex = 100;
		this.label104.Text = "Generate:";
		this.label104.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.btnGenerateActivitiesAtUserPos.BackColor = System.Drawing.Color.Gray;
		this.btnGenerateActivitiesAtUserPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGenerateActivitiesAtUserPos.ForeColor = System.Drawing.Color.Black;
		this.btnGenerateActivitiesAtUserPos.Image = NavBuddy.Properties.Resources.user;
		this.btnGenerateActivitiesAtUserPos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerateActivitiesAtUserPos.Location = new System.Drawing.Point(175, 16);
		this.btnGenerateActivitiesAtUserPos.Name = "btnGenerateActivitiesAtUserPos";
		this.btnGenerateActivitiesAtUserPos.Size = new System.Drawing.Size(75, 25);
		this.btnGenerateActivitiesAtUserPos.TabIndex = 70;
		this.btnGenerateActivitiesAtUserPos.Text = "EFGH";
		this.btnGenerateActivitiesAtUserPos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGenerateActivitiesAtUserPos.UseVisualStyleBackColor = false;
		this.btnGenerateActivitiesAtUserPos.Click += new System.EventHandler(btnGenerateActivitiesAtUserPos_Click);
		this.btnGenerateActivitiesAtHome.BackColor = System.Drawing.Color.Gray;
		this.btnGenerateActivitiesAtHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGenerateActivitiesAtHome.ForeColor = System.Drawing.Color.Black;
		this.btnGenerateActivitiesAtHome.Image = NavBuddy.Properties.Resources.home;
		this.btnGenerateActivitiesAtHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGenerateActivitiesAtHome.Location = new System.Drawing.Point(87, 16);
		this.btnGenerateActivitiesAtHome.Name = "btnGenerateActivitiesAtHome";
		this.btnGenerateActivitiesAtHome.Size = new System.Drawing.Size(75, 25);
		this.btnGenerateActivitiesAtHome.TabIndex = 69;
		this.btnGenerateActivitiesAtHome.Text = "ABCD";
		this.btnGenerateActivitiesAtHome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGenerateActivitiesAtHome.UseVisualStyleBackColor = false;
		this.btnGenerateActivitiesAtHome.Click += new System.EventHandler(btnGenerateActivitiesAtHome_Click);
		this.ActivitySearchPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ActivitySearchPanel.AutoScroll = true;
		this.ActivitySearchPanel.BackColor = System.Drawing.Color.Black;
		this.ActivitySearchPanel.Location = new System.Drawing.Point(9, 47);
		this.ActivitySearchPanel.Margin = new System.Windows.Forms.Padding(0);
		this.ActivitySearchPanel.Name = "ActivitySearchPanel";
		this.ActivitySearchPanel.Size = new System.Drawing.Size(1111, 152);
		this.ActivitySearchPanel.TabIndex = 68;
		this.groupBoxAssignedActivities.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBoxAssignedActivities.BackColor = System.Drawing.Color.Black;
		this.groupBoxAssignedActivities.Controls.Add(this.ActivityAssignedPanel);
		this.groupBoxAssignedActivities.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBoxAssignedActivities.ForeColor = System.Drawing.Color.White;
		this.groupBoxAssignedActivities.Location = new System.Drawing.Point(5, 0);
		this.groupBoxAssignedActivities.Name = "groupBoxAssignedActivities";
		this.groupBoxAssignedActivities.Size = new System.Drawing.Size(1127, 180);
		this.groupBoxAssignedActivities.TabIndex = 95;
		this.groupBoxAssignedActivities.TabStop = false;
		this.groupBoxAssignedActivities.Text = "Accepted";
		this.ActivityAssignedPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.ActivityAssignedPanel.AutoScroll = true;
		this.ActivityAssignedPanel.BackColor = System.Drawing.Color.Black;
		this.ActivityAssignedPanel.Location = new System.Drawing.Point(9, 16);
		this.ActivityAssignedPanel.Margin = new System.Windows.Forms.Padding(0);
		this.ActivityAssignedPanel.Name = "ActivityAssignedPanel";
		this.ActivityAssignedPanel.Size = new System.Drawing.Size(1110, 155);
		this.ActivityAssignedPanel.TabIndex = 68;
		this.tabFinance.BackColor = System.Drawing.Color.Black;
		this.tabFinance.Controls.Add(this.groupBox19);
		this.tabFinance.Controls.Add(this.groupBox18);
		this.tabFinance.Location = new System.Drawing.Point(4, 27);
		this.tabFinance.Name = "tabFinance";
		this.tabFinance.Padding = new System.Windows.Forms.Padding(3);
		this.tabFinance.Size = new System.Drawing.Size(1143, 390);
		this.tabFinance.TabIndex = 17;
		this.tabFinance.Text = "Finance";
		this.groupBox19.BackColor = System.Drawing.Color.Black;
		this.groupBox19.Controls.Add(this.label106);
		this.groupBox19.Controls.Add(this.labFinanceMaxLoan);
		this.groupBox19.Controls.Add(this.btnAskNewLoan);
		this.groupBox19.Controls.Add(this.nudLoan);
		this.groupBox19.Controls.Add(this.label100);
		this.groupBox19.Controls.Add(this.labFinanceLoan);
		this.groupBox19.Controls.Add(this.btnRepayLoan);
		this.groupBox19.Controls.Add(this.label101);
		this.groupBox19.Controls.Add(this.labFinanceCash);
		this.groupBox19.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox19.ForeColor = System.Drawing.Color.White;
		this.groupBox19.Location = new System.Drawing.Point(728, 6);
		this.groupBox19.Name = "groupBox19";
		this.groupBox19.Size = new System.Drawing.Size(406, 378);
		this.groupBox19.TabIndex = 96;
		this.groupBox19.TabStop = false;
		this.groupBox19.Text = "Bank account";
		this.label106.AutoSize = true;
		this.label106.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label106.ForeColor = System.Drawing.Color.White;
		this.label106.Location = new System.Drawing.Point(195, 56);
		this.label106.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label106.Name = "label106";
		this.label106.Size = new System.Drawing.Size(70, 18);
		this.label106.TabIndex = 108;
		this.label106.Text = "Max Loan:";
		this.label106.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labFinanceMaxLoan.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labFinanceMaxLoan.ForeColor = System.Drawing.Color.Lime;
		this.labFinanceMaxLoan.Location = new System.Drawing.Point(274, 56);
		this.labFinanceMaxLoan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labFinanceMaxLoan.Name = "labFinanceMaxLoan";
		this.labFinanceMaxLoan.Size = new System.Drawing.Size(100, 18);
		this.labFinanceMaxLoan.TabIndex = 107;
		this.labFinanceMaxLoan.Text = "---";
		this.labFinanceMaxLoan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAskNewLoan.BackColor = System.Drawing.Color.Gray;
		this.btnAskNewLoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAskNewLoan.ForeColor = System.Drawing.Color.Black;
		this.btnAskNewLoan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAskNewLoan.Location = new System.Drawing.Point(193, 86);
		this.btnAskNewLoan.Name = "btnAskNewLoan";
		this.btnAskNewLoan.Size = new System.Drawing.Size(79, 26);
		this.btnAskNewLoan.TabIndex = 106;
		this.btnAskNewLoan.Text = "Ask loan";
		this.btnAskNewLoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAskNewLoan.UseVisualStyleBackColor = false;
		this.btnAskNewLoan.Click += new System.EventHandler(btnAskNewLoan_Click);
		this.nudLoan.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudLoan.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudLoan.Increment = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.nudLoan.Location = new System.Drawing.Point(24, 88);
		this.nudLoan.Maximum = new decimal(new int[4] { 100000000, 0, 0, 0 });
		this.nudLoan.Name = "nudLoan";
		this.nudLoan.Size = new System.Drawing.Size(144, 23);
		this.nudLoan.TabIndex = 105;
		this.nudLoan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label100.AutoSize = true;
		this.label100.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label100.ForeColor = System.Drawing.Color.White;
		this.label100.Location = new System.Drawing.Point(19, 56);
		this.label100.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label100.Name = "label100";
		this.label100.Size = new System.Drawing.Size(41, 18);
		this.label100.TabIndex = 102;
		this.label100.Text = "Loan:";
		this.label100.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labFinanceLoan.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labFinanceLoan.ForeColor = System.Drawing.Color.Lime;
		this.labFinanceLoan.Location = new System.Drawing.Point(68, 56);
		this.labFinanceLoan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labFinanceLoan.Name = "labFinanceLoan";
		this.labFinanceLoan.Size = new System.Drawing.Size(100, 18);
		this.labFinanceLoan.TabIndex = 101;
		this.labFinanceLoan.Text = "---";
		this.labFinanceLoan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnRepayLoan.BackColor = System.Drawing.Color.Gray;
		this.btnRepayLoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnRepayLoan.ForeColor = System.Drawing.Color.Black;
		this.btnRepayLoan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRepayLoan.Location = new System.Drawing.Point(295, 86);
		this.btnRepayLoan.Name = "btnRepayLoan";
		this.btnRepayLoan.Size = new System.Drawing.Size(79, 26);
		this.btnRepayLoan.TabIndex = 100;
		this.btnRepayLoan.Text = "Pay loan";
		this.btnRepayLoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnRepayLoan.UseVisualStyleBackColor = false;
		this.btnRepayLoan.Click += new System.EventHandler(btnRepayLoan_Click);
		this.label101.AutoSize = true;
		this.label101.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label101.ForeColor = System.Drawing.Color.White;
		this.label101.Location = new System.Drawing.Point(19, 27);
		this.label101.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label101.Name = "label101";
		this.label101.Size = new System.Drawing.Size(41, 18);
		this.label101.TabIndex = 99;
		this.label101.Text = "Cash:";
		this.label101.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.labFinanceCash.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labFinanceCash.ForeColor = System.Drawing.Color.Lime;
		this.labFinanceCash.Location = new System.Drawing.Point(68, 27);
		this.labFinanceCash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labFinanceCash.Name = "labFinanceCash";
		this.labFinanceCash.Size = new System.Drawing.Size(100, 18);
		this.labFinanceCash.TabIndex = 89;
		this.labFinanceCash.Text = "---";
		this.labFinanceCash.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.groupBox18.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox18.BackColor = System.Drawing.Color.Black;
		this.groupBox18.Controls.Add(this.lstTransactions);
		this.groupBox18.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox18.ForeColor = System.Drawing.Color.White;
		this.groupBox18.Location = new System.Drawing.Point(6, 6);
		this.groupBox18.Name = "groupBox18";
		this.groupBox18.Size = new System.Drawing.Size(716, 378);
		this.groupBox18.TabIndex = 95;
		this.groupBox18.TabStop = false;
		this.groupBox18.Text = "Transaction log";
		this.lstTransactions.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lstTransactions.FormattingEnabled = true;
		this.lstTransactions.ItemHeight = 15;
		this.lstTransactions.Location = new System.Drawing.Point(6, 21);
		this.lstTransactions.Name = "lstTransactions";
		this.lstTransactions.Size = new System.Drawing.Size(700, 349);
		this.lstTransactions.TabIndex = 92;
		this.tabGoodsTrade.Location = new System.Drawing.Point(4, 27);
		this.tabGoodsTrade.Name = "tabGoodsTrade";
		this.tabGoodsTrade.Padding = new System.Windows.Forms.Padding(3);
		this.tabGoodsTrade.Size = new System.Drawing.Size(1143, 390);
		this.tabGoodsTrade.TabIndex = 18;
		this.tabGoodsTrade.Text = "Goods & Trade";
		this.tabGoodsTrade.UseVisualStyleBackColor = true;
		this.AircraftMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.deleteToolStripMenuItem });
		this.AircraftMenuStrip.Name = "AssignmentMenuStrip";
		this.AircraftMenuStrip.Size = new System.Drawing.Size(108, 26);
		this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
		this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
		this.deleteToolStripMenuItem.Text = "Delete";
		this.deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
		this.PayloadMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.loadIntoAirplaneToolStripMenuItem, this.unloadToolStripMenuItem, this.deliverToolStripMenuItem, this.PayLoadcleanupToolStripMenuItem });
		this.PayloadMenuStrip.Name = "AssignmentMenuStrip";
		this.PayloadMenuStrip.Size = new System.Drawing.Size(119, 92);
		this.loadIntoAirplaneToolStripMenuItem.Name = "loadIntoAirplaneToolStripMenuItem";
		this.loadIntoAirplaneToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
		this.loadIntoAirplaneToolStripMenuItem.Text = "Load";
		this.unloadToolStripMenuItem.Name = "unloadToolStripMenuItem";
		this.unloadToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
		this.unloadToolStripMenuItem.Text = "Unload";
		this.deliverToolStripMenuItem.Name = "deliverToolStripMenuItem";
		this.deliverToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
		this.deliverToolStripMenuItem.Text = "Deliver";
		this.PayLoadcleanupToolStripMenuItem.Name = "PayLoadcleanupToolStripMenuItem";
		this.PayLoadcleanupToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
		this.PayLoadcleanupToolStripMenuItem.Text = "Cleanup";
		this.AssignmentMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.MenuItemTakeAssignment, this.MenuItemDismissAssignment });
		this.AssignmentMenuStrip.Name = "AssignmentMenuStrip";
		this.AssignmentMenuStrip.Size = new System.Drawing.Size(144, 48);
		this.MenuItemTakeAssignment.Name = "MenuItemTakeAssignment";
		this.MenuItemTakeAssignment.Size = new System.Drawing.Size(143, 22);
		this.MenuItemTakeAssignment.Text = "Assign to me";
		this.MenuItemDismissAssignment.Name = "MenuItemDismissAssignment";
		this.MenuItemDismissAssignment.Size = new System.Drawing.Size(143, 22);
		this.MenuItemDismissAssignment.Text = "Dismiss";
		this.TimerSlow.Enabled = true;
		this.TimerSlow.Interval = 2000;
		this.TimerSlow.Tick += new System.EventHandler(TimerSlow_Tick);
		this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox2.BackColor = System.Drawing.Color.Black;
		this.groupBox2.Controls.Add(this.nudVnavHoldAlt);
		this.groupBox2.Controls.Add(this.radVnavHoldAGL);
		this.groupBox2.Controls.Add(this.radVnavStandard);
		this.groupBox2.Controls.Add(this.labVnavReference);
		this.groupBox2.Controls.Add(this.label52);
		this.groupBox2.Controls.Add(this.labVnavDescription);
		this.groupBox2.Controls.Add(this.label49);
		this.groupBox2.Controls.Add(this.btnExpandTabControl);
		this.groupBox2.Controls.Add(this.label15);
		this.groupBox2.Controls.Add(this.labSlopeGoal);
		this.groupBox2.Controls.Add(this.label13);
		this.groupBox2.Controls.Add(this.labFPMGoal);
		this.groupBox2.Controls.Add(this.label11);
		this.groupBox2.Controls.Add(this.labAltitudeGoal);
		this.groupBox2.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox2.ForeColor = System.Drawing.Color.White;
		this.groupBox2.Location = new System.Drawing.Point(765, 24);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(390, 142);
		this.groupBox2.TabIndex = 24;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "VNAV";
		this.nudVnavHoldAlt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.nudVnavHoldAlt.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.nudVnavHoldAlt.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.nudVnavHoldAlt.Increment = new decimal(new int[4] { 100, 0, 0, 0 });
		this.nudVnavHoldAlt.Location = new System.Drawing.Point(326, 43);
		this.nudVnavHoldAlt.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.nudVnavHoldAlt.Name = "nudVnavHoldAlt";
		this.nudVnavHoldAlt.Size = new System.Drawing.Size(55, 23);
		this.nudVnavHoldAlt.TabIndex = 46;
		this.nudVnavHoldAlt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.nudVnavHoldAlt.Value = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.radVnavHoldAGL.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.radVnavHoldAGL.AutoSize = true;
		this.radVnavHoldAGL.Location = new System.Drawing.Point(248, 44);
		this.radVnavHoldAGL.Name = "radVnavHoldAGL";
		this.radVnavHoldAGL.Size = new System.Drawing.Size(76, 19);
		this.radVnavHoldAGL.TabIndex = 45;
		this.radVnavHoldAGL.Text = "hold AGL:";
		this.radVnavHoldAGL.UseVisualStyleBackColor = true;
		this.radVnavStandard.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.radVnavStandard.AutoSize = true;
		this.radVnavStandard.Checked = true;
		this.radVnavStandard.Location = new System.Drawing.Point(248, 20);
		this.radVnavStandard.Name = "radVnavStandard";
		this.radVnavStandard.Size = new System.Drawing.Size(128, 19);
		this.radVnavStandard.TabIndex = 44;
		this.radVnavStandard.TabStop = true;
		this.radVnavStandard.Text = "Standard TOC/TOD";
		this.radVnavStandard.UseVisualStyleBackColor = true;
		this.labVnavReference.AutoSize = true;
		this.labVnavReference.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labVnavReference.ForeColor = System.Drawing.Color.White;
		this.labVnavReference.Location = new System.Drawing.Point(98, 51);
		this.labVnavReference.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labVnavReference.Name = "labVnavReference";
		this.labVnavReference.Size = new System.Drawing.Size(23, 18);
		this.labVnavReference.TabIndex = 43;
		this.labVnavReference.Text = "---";
		this.labVnavReference.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label52.AutoSize = true;
		this.label52.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label52.ForeColor = System.Drawing.Color.White;
		this.label52.Location = new System.Drawing.Point(13, 51);
		this.label52.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label52.Name = "label52";
		this.label52.Size = new System.Drawing.Size(73, 18);
		this.label52.TabIndex = 42;
		this.label52.Text = "reference:";
		this.label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labVnavDescription.AutoSize = true;
		this.labVnavDescription.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labVnavDescription.ForeColor = System.Drawing.Color.White;
		this.labVnavDescription.Location = new System.Drawing.Point(98, 21);
		this.labVnavDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labVnavDescription.Name = "labVnavDescription";
		this.labVnavDescription.Size = new System.Drawing.Size(23, 18);
		this.labVnavDescription.TabIndex = 41;
		this.labVnavDescription.Text = "---";
		this.labVnavDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label49.AutoSize = true;
		this.label49.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label49.ForeColor = System.Drawing.Color.White;
		this.label49.Location = new System.Drawing.Point(13, 21);
		this.label49.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label49.Name = "label49";
		this.label49.Size = new System.Drawing.Size(49, 18);
		this.label49.TabIndex = 40;
		this.label49.Text = "phase:";
		this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnExpandTabControl.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnExpandTabControl.Image = (System.Drawing.Image)resources.GetObject("btnExpandTabControl.Image");
		this.btnExpandTabControl.Location = new System.Drawing.Point(363, 123);
		this.btnExpandTabControl.Name = "btnExpandTabControl";
		this.btnExpandTabControl.Size = new System.Drawing.Size(21, 13);
		this.btnExpandTabControl.TabIndex = 28;
		this.btnExpandTabControl.UseVisualStyleBackColor = true;
		this.btnExpandTabControl.Click += new System.EventHandler(btnExpandTabControl_Click);
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.ForeColor = System.Drawing.Color.White;
		this.label15.Location = new System.Drawing.Point(176, 111);
		this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(46, 18);
		this.label15.TabIndex = 35;
		this.label15.Text = "slope:";
		this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labSlopeGoal.AutoSize = true;
		this.labSlopeGoal.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labSlopeGoal.ForeColor = System.Drawing.Color.Fuchsia;
		this.labSlopeGoal.Location = new System.Drawing.Point(248, 111);
		this.labSlopeGoal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labSlopeGoal.Name = "labSlopeGoal";
		this.labSlopeGoal.Size = new System.Drawing.Size(33, 18);
		this.labSlopeGoal.TabIndex = 34;
		this.labSlopeGoal.Text = "-----";
		this.labSlopeGoal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.ForeColor = System.Drawing.Color.White;
		this.label13.Location = new System.Drawing.Point(13, 111);
		this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(37, 18);
		this.label13.TabIndex = 33;
		this.label13.Text = "rate:";
		this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labFPMGoal.AutoSize = true;
		this.labFPMGoal.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labFPMGoal.ForeColor = System.Drawing.Color.Fuchsia;
		this.labFPMGoal.Location = new System.Drawing.Point(98, 111);
		this.labFPMGoal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labFPMGoal.Name = "labFPMGoal";
		this.labFPMGoal.Size = new System.Drawing.Size(33, 18);
		this.labFPMGoal.TabIndex = 32;
		this.labFPMGoal.Text = "-----";
		this.labFPMGoal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.ForeColor = System.Drawing.Color.White;
		this.label11.Location = new System.Drawing.Point(13, 81);
		this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(46, 18);
		this.label11.TabIndex = 31;
		this.label11.Text = "reach:";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labAltitudeGoal.AutoSize = true;
		this.labAltitudeGoal.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labAltitudeGoal.ForeColor = System.Drawing.Color.Fuchsia;
		this.labAltitudeGoal.Location = new System.Drawing.Point(98, 81);
		this.labAltitudeGoal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labAltitudeGoal.Name = "labAltitudeGoal";
		this.labAltitudeGoal.Size = new System.Drawing.Size(33, 18);
		this.labAltitudeGoal.TabIndex = 30;
		this.labAltitudeGoal.Text = "-----";
		this.labAltitudeGoal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labCurrentAltitude.AutoSize = true;
		this.labCurrentAltitude.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labCurrentAltitude.ForeColor = System.Drawing.Color.Lime;
		this.labCurrentAltitude.Location = new System.Drawing.Point(310, 31);
		this.labCurrentAltitude.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labCurrentAltitude.Name = "labCurrentAltitude";
		this.labCurrentAltitude.Size = new System.Drawing.Size(33, 18);
		this.labCurrentAltitude.TabIndex = 28;
		this.labCurrentAltitude.Text = "-----";
		this.labCurrentAltitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labCurrentGS.AutoSize = true;
		this.labCurrentGS.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labCurrentGS.ForeColor = System.Drawing.Color.Lime;
		this.labCurrentGS.Location = new System.Drawing.Point(310, 64);
		this.labCurrentGS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labCurrentGS.Name = "labCurrentGS";
		this.labCurrentGS.Size = new System.Drawing.Size(33, 18);
		this.labCurrentGS.TabIndex = 27;
		this.labCurrentGS.Text = "-----";
		this.labCurrentGS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.Color.White;
		this.label7.Location = new System.Drawing.Point(235, 64);
		this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(28, 18);
		this.label7.TabIndex = 26;
		this.label7.Text = "GS:";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.ForeColor = System.Drawing.Color.White;
		this.label12.Location = new System.Drawing.Point(235, 31);
		this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(60, 18);
		this.label12.TabIndex = 25;
		this.label12.Text = "altitude:";
		this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labArrivalTimeWP.AutoSize = true;
		this.labArrivalTimeWP.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labArrivalTimeWP.ForeColor = System.Drawing.Color.Lime;
		this.labArrivalTimeWP.Location = new System.Drawing.Point(90, 86);
		this.labArrivalTimeWP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labArrivalTimeWP.Name = "labArrivalTimeWP";
		this.labArrivalTimeWP.Size = new System.Drawing.Size(46, 18);
		this.labArrivalTimeWP.TabIndex = 24;
		this.labArrivalTimeWP.Text = "--:--:--";
		this.labArrivalTimeWP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.ForeColor = System.Drawing.Color.White;
		this.label9.Location = new System.Drawing.Point(11, 86);
		this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(66, 18);
		this.label9.TabIndex = 23;
		this.label9.Text = "arrival at:";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labRemainingMilesWP.AutoSize = true;
		this.labRemainingMilesWP.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labRemainingMilesWP.ForeColor = System.Drawing.Color.Lime;
		this.labRemainingMilesWP.Location = new System.Drawing.Point(177, 64);
		this.labRemainingMilesWP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labRemainingMilesWP.Name = "labRemainingMilesWP";
		this.labRemainingMilesWP.Size = new System.Drawing.Size(33, 18);
		this.labRemainingMilesWP.TabIndex = 22;
		this.labRemainingMilesWP.Text = "-----";
		this.labRemainingMilesWP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.labRemainigTimeWP.AutoSize = true;
		this.labRemainigTimeWP.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labRemainigTimeWP.ForeColor = System.Drawing.Color.Lime;
		this.labRemainigTimeWP.Location = new System.Drawing.Point(90, 64);
		this.labRemainigTimeWP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labRemainigTimeWP.Name = "labRemainigTimeWP";
		this.labRemainigTimeWP.Size = new System.Drawing.Size(46, 18);
		this.labRemainigTimeWP.TabIndex = 21;
		this.labRemainigTimeWP.Text = "--:--:--";
		this.labRemainigTimeWP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.ForeColor = System.Drawing.Color.White;
		this.label10.Location = new System.Drawing.Point(11, 64);
		this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(75, 18);
		this.label10.TabIndex = 20;
		this.label10.Text = "remaining:";
		this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labNextWaypointDescription.AutoSize = true;
		this.labNextWaypointDescription.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labNextWaypointDescription.ForeColor = System.Drawing.Color.Lime;
		this.labNextWaypointDescription.Location = new System.Drawing.Point(16, 27);
		this.labNextWaypointDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labNextWaypointDescription.Name = "labNextWaypointDescription";
		this.labNextWaypointDescription.Size = new System.Drawing.Size(28, 18);
		this.labNextWaypointDescription.TabIndex = 9;
		this.labNextWaypointDescription.Text = "----";
		this.labNextWaypointDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.imageList16.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList16.ImageStream");
		this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList16.Images.SetKeyName(0, "spyOff");
		this.imageList16.Images.SetKeyName(1, "spyOn");
		this.imageList16.Images.SetKeyName(2, "googlelink");
		this.imageList16.Images.SetKeyName(3, "lente16");
		this.imageList16.Images.SetKeyName(4, "cogwheel16x16.png");
		this.TimerQuick.Enabled = true;
		this.TimerQuick.Tick += new System.EventHandler(TimerQuick_Tick);
		this.imageList40.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList40.ImageStream");
		this.imageList40.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList40.Images.SetKeyName(0, "manopola_big.png");
		this.imageList20.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList20.ImageStream");
		this.imageList20.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList20.Images.SetKeyName(0, "spyOff");
		this.imageList20.Images.SetKeyName(1, "spyOn");
		this.imageList7X7.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList7X7.ImageStream");
		this.imageList7X7.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList7X7.Images.SetKeyName(0, "minidown");
		this.imageList7X7.Images.SetKeyName(1, "miniup");
		this.imageList7X7.Images.SetKeyName(2, "Spy7x7on");
		this.imageList7X7.Images.SetKeyName(3, "Spy7x7off");
		this.menuStrip1.Font = new System.Drawing.Font("Calibri", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.flightPlanToolStripMenuItem, this.simulatorToolStripMenuItem, this.dataToolStripMenuItem, this.performanceToolStripMenuItem, this.infoToolStripMenuItem });
		this.menuStrip1.Location = new System.Drawing.Point(0, 0);
		this.menuStrip1.Name = "menuStrip1";
		this.menuStrip1.Size = new System.Drawing.Size(1164, 24);
		this.menuStrip1.TabIndex = 28;
		this.menuStrip1.Text = "menuStrip1";
		this.flightPlanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.loadToolStripMenuItem, this.reloadToolStripMenuItem, this.resetFlightToolStripMenuItem, this.exportForGoogleEarthToolStripMenuItem, this.completeCurrentLegToolStripMenuItem });
		this.flightPlanToolStripMenuItem.Font = new System.Drawing.Font("Calibri", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.flightPlanToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.flightPlanToolStripMenuItem.Name = "flightPlanToolStripMenuItem";
		this.flightPlanToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
		this.flightPlanToolStripMenuItem.Text = "Flight plan";
		this.loadToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.loadToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
		this.loadToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
		this.loadToolStripMenuItem.Text = "Load";
		this.loadToolStripMenuItem.Click += new System.EventHandler(loadToolStripMenuItem_Click);
		this.reloadToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.reloadToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
		this.reloadToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
		this.reloadToolStripMenuItem.Text = "Reload";
		this.reloadToolStripMenuItem.Click += new System.EventHandler(reloadToolStripMenuItem_Click);
		this.resetFlightToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.resetFlightToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
		this.resetFlightToolStripMenuItem.Name = "resetFlightToolStripMenuItem";
		this.resetFlightToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
		this.resetFlightToolStripMenuItem.Text = "Reset flight";
		this.resetFlightToolStripMenuItem.Click += new System.EventHandler(resetFlightToolStripMenuItem_Click);
		this.exportForGoogleEarthToolStripMenuItem.Name = "exportForGoogleEarthToolStripMenuItem";
		this.exportForGoogleEarthToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
		this.exportForGoogleEarthToolStripMenuItem.Text = "Export for Google Earth";
		this.exportForGoogleEarthToolStripMenuItem.Click += new System.EventHandler(exportForGoogleEarthToolStripMenuItem_Click);
		this.completeCurrentLegToolStripMenuItem.Name = "completeCurrentLegToolStripMenuItem";
		this.completeCurrentLegToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
		this.completeCurrentLegToolStripMenuItem.Text = "Complete Current Leg";
		this.completeCurrentLegToolStripMenuItem.Click += new System.EventHandler(completeCurrentLegToolStripMenuItem_Click);
		this.simulatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.connectToolStripMenuItem, this.showLogToolStripMenuItem, this.flightControlsToolStripMenuItem, this.compassToolStripMenuItem, this.helicopterHelpToolStripMenuItem });
		this.simulatorToolStripMenuItem.Name = "simulatorToolStripMenuItem";
		this.simulatorToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
		this.simulatorToolStripMenuItem.Text = "Simulator";
		this.simulatorToolStripMenuItem.Click += new System.EventHandler(simulatorToolStripMenuItem_Click);
		this.connectToolStripMenuItem.CheckOnClick = true;
		this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
		this.connectToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
		this.connectToolStripMenuItem.Text = "Connect";
		this.connectToolStripMenuItem.CheckStateChanged += new System.EventHandler(connectToolStripMenuItem_CheckStateChanged);
		this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
		this.showLogToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
		this.showLogToolStripMenuItem.Text = "Show communication log";
		this.showLogToolStripMenuItem.Click += new System.EventHandler(showLogToolStripMenuItem_Click);
		this.flightControlsToolStripMenuItem.Name = "flightControlsToolStripMenuItem";
		this.flightControlsToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
		this.flightControlsToolStripMenuItem.Text = "Flight panel";
		this.flightControlsToolStripMenuItem.Click += new System.EventHandler(flightControlsToolStripMenuItem_Click);
		this.compassToolStripMenuItem.Name = "compassToolStripMenuItem";
		this.compassToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
		this.compassToolStripMenuItem.Text = "Compass";
		this.compassToolStripMenuItem.Click += new System.EventHandler(compassToolStripMenuItem_Click);
		this.helicopterHelpToolStripMenuItem.Name = "helicopterHelpToolStripMenuItem";
		this.helicopterHelpToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
		this.helicopterHelpToolStripMenuItem.Text = "Helicopter Help";
		this.helicopterHelpToolStripMenuItem.Click += new System.EventHandler(helicopterHelpToolStripMenuItem_Click);
		this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.dataFolderToolStripMenuItem });
		this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
		this.dataToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
		this.dataToolStripMenuItem.Text = "Data";
		this.dataFolderToolStripMenuItem.Name = "dataFolderToolStripMenuItem";
		this.dataFolderToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
		this.dataFolderToolStripMenuItem.Text = "Data folder";
		this.dataFolderToolStripMenuItem.Click += new System.EventHandler(dataFolderToolStripMenuItem_Click);
		this.performanceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.savePerformanceToolStripMenuItem, this.loadPerformanceToolStripMenuItem });
		this.performanceToolStripMenuItem.Name = "performanceToolStripMenuItem";
		this.performanceToolStripMenuItem.Size = new System.Drawing.Size(129, 20);
		this.performanceToolStripMenuItem.Text = "Aircraft performance";
		this.savePerformanceToolStripMenuItem.Name = "savePerformanceToolStripMenuItem";
		this.savePerformanceToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
		this.savePerformanceToolStripMenuItem.Text = "Save performance";
		this.savePerformanceToolStripMenuItem.Click += new System.EventHandler(savePerformanceToolStripMenuItem_Click);
		this.loadPerformanceToolStripMenuItem.Name = "loadPerformanceToolStripMenuItem";
		this.loadPerformanceToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
		this.loadPerformanceToolStripMenuItem.Text = "Load performance";
		this.loadPerformanceToolStripMenuItem.Click += new System.EventHandler(loadPerformanceToolStripMenuItem_Click);
		this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.youtubeChannelToolStripMenuItem, this.discordChatToolStripMenuItem });
		this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
		this.infoToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
		this.infoToolStripMenuItem.Text = "Info";
		this.infoToolStripMenuItem.Click += new System.EventHandler(infoToolStripMenuItem_Click_1);
		this.youtubeChannelToolStripMenuItem.Name = "youtubeChannelToolStripMenuItem";
		this.youtubeChannelToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
		this.youtubeChannelToolStripMenuItem.Text = "Youtube channel";
		this.youtubeChannelToolStripMenuItem.Click += new System.EventHandler(youtubeChannelToolStripMenuItem_Click);
		this.discordChatToolStripMenuItem.Name = "discordChatToolStripMenuItem";
		this.discordChatToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
		this.discordChatToolStripMenuItem.Text = "Discord chat";
		this.discordChatToolStripMenuItem.Click += new System.EventHandler(discordChatToolStripMenuItem_Click);
		this.timerCronometer.Tick += new System.EventHandler(timerCronometer_Tick);
		this.groupBox7.BackColor = System.Drawing.Color.Black;
		this.groupBox7.Controls.Add(this.labNextWaypointDescription);
		this.groupBox7.Controls.Add(this.label10);
		this.groupBox7.Controls.Add(this.labRemainigTimeWP);
		this.groupBox7.Controls.Add(this.labRemainingMilesWP);
		this.groupBox7.Controls.Add(this.label9);
		this.groupBox7.Controls.Add(this.labArrivalTimeWP);
		this.groupBox7.Controls.Add(this.label12);
		this.groupBox7.Controls.Add(this.label7);
		this.groupBox7.Controls.Add(this.labCurrentGS);
		this.groupBox7.Controls.Add(this.labCurrentAltitude);
		this.groupBox7.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox7.ForeColor = System.Drawing.Color.White;
		this.groupBox7.Location = new System.Drawing.Point(387, 24);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Size = new System.Drawing.Size(372, 142);
		this.groupBox7.TabIndex = 40;
		this.groupBox7.TabStop = false;
		this.groupBox7.Text = "Next Way Point";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(1164, 598);
		base.Controls.Add(this.groupBox7);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.tabControl1);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.menuStrip1);
		this.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.ForeColor = System.Drawing.Color.White;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MainMenuStrip = this.menuStrip1;
		base.Margin = new System.Windows.Forms.Padding(4);
		base.Name = "FormMain";
		this.Text = "Nav Buddy";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Form1_FormClosing);
		base.Load += new System.EventHandler(Form1_Load);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.picBoxConnSpy).EndInit();
		this.tabControl1.ResumeLayout(false);
		this.tabNavLog.ResumeLayout(false);
		this.grpRunways.ResumeLayout(false);
		this.grpRunways.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.tabParameters.ResumeLayout(false);
		this.grpPerformance.ResumeLayout(false);
		this.grpPerformance.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudLandFPM).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudThrottleDamper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudThrottleEffect).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudFlapsLandingPerc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudFlapsLandingIas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudReverseThrust).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudMaxBankAngle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudElevatorDamper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudElevatorEffect).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudAileronDamper).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudAileronEffect).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudRudderEffect).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudFlapsTakeOffIas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudFlapsTakeOffPerc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudLandingGearDownAGL).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudLandingGearUpAGL).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudTakeOffCompletedAGL).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudDescFPM).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudRunwayEntAGL).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudClimbFPM).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudDescIas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudClimbIas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudCruiseIas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudSafeIas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudLandingIas).EndInit();
		this.tabPlanning.ResumeLayout(false);
		this.tabPlanning.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudAddFeetTeleport).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudPlannedApproachAltitude).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudPlannedApproachDistance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudPlannedCruiseAltitude).EndInit();
		this.tabFuelManagement.ResumeLayout(false);
		this.groupBox8.ResumeLayout(false);
		this.groupBox8.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.tabPathTrack.ResumeLayout(false);
		this.tabPathTrack.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nupFligthTrackRecordDrawingSamples).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nupFligthTrackRecordInterval).EndInit();
		this.MapPanel.ResumeLayout(false);
		this.tabStandardManouver.ResumeLayout(false);
		this.tabStandardManouver.PerformLayout();
		this.groupBox11.ResumeLayout(false);
		this.groupBox11.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		this.groupBox10.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.groupBox9.ResumeLayout(false);
		this.groupBox9.PerformLayout();
		this.tabMisc.ResumeLayout(false);
		this.tabMisc.PerformLayout();
		this.groupBox12.ResumeLayout(false);
		this.groupBox12.PerformLayout();
		this.grpTeleport.ResumeLayout(false);
		this.grpTeleport.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudTeleportHeading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.nudTeleportAltitude).EndInit();
		this.tabGraphicLog.ResumeLayout(false);
		this.grpGraphicLogConfig.ResumeLayout(false);
		this.tabLog.ResumeLayout(false);
		this.tabLog.PerformLayout();
		this.tabInfo.ResumeLayout(false);
		this.tabBuddyWorld.ResumeLayout(false);
		this.grpPayLoad.ResumeLayout(false);
		this.groupBox15.ResumeLayout(false);
		this.groupBox15.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudPilotWeight).EndInit();
		this.groupBox13.ResumeLayout(false);
		this.groupBox13.PerformLayout();
		this.groupBox16.ResumeLayout(false);
		this.groupBox16.PerformLayout();
		this.groupBox14.ResumeLayout(false);
		this.panelWorldAirplanes.ResumeLayout(false);
		this.panelWorldAirplanes.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nupRefuel).EndInit();
		this.tabActivities.ResumeLayout(false);
		this.groupBox17.ResumeLayout(false);
		this.groupBox17.PerformLayout();
		this.groupBoxAssignedActivities.ResumeLayout(false);
		this.tabFinance.ResumeLayout(false);
		this.groupBox19.ResumeLayout(false);
		this.groupBox19.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudLoan).EndInit();
		this.groupBox18.ResumeLayout(false);
		this.AircraftMenuStrip.ResumeLayout(false);
		this.PayloadMenuStrip.ResumeLayout(false);
		this.AssignmentMenuStrip.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.nudVnavHoldAlt).EndInit();
		this.menuStrip1.ResumeLayout(false);
		this.menuStrip1.PerformLayout();
		this.groupBox7.ResumeLayout(false);
		this.groupBox7.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void SelectAndLoadFlightPlan()
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "Pln files (*.pln)|*.pln|All files (*.*)|*.*";
		XmlDocument xmlDocument = new XmlDocument();
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			LoadFlightPlan(openFileDialog.FileName);
			reloadToolStripMenuItem.Text = "Reload: " + FlightPlan.Title;
			reloadToolStripMenuItem.Tag = openFileDialog.FileName;
		}
	}

	private void LoadFlightPlan(string strFlightPlanFile)
	{
		try
		{
			FlightPlan.LoadPlanFromPLNFile(strFlightPlanFile);
			FlightPlan.DefaultAltitudeAssignments();
			if (MessageBox.Show("Do you want to have TOC/TOD automatically calculated?", "TOC/TOD", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				FlightPlan.CalculateTocAndTod((double)nudClimbIas.Value, (double)nudDescIas.Value, (double)nudClimbFPM.Value, (double)nudDescFPM.Value);
			}
			FlightPlan.DefaultSpeedAssignments((double)nudClimbIas.Value, (double)nudCruiseIas.Value, (double)nudDescIas.Value);
			FlightPlan.DepartureTime = DateTime.MinValue;
			ShowPlan();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message + "\n\n");
		}
	}

	private void Reset_flight()
	{
		foreach (ATCWaypoint aTCWaypoint in FlightPlan.ATCWaypoints)
		{
			aTCWaypoint.arrival_time_actual = DateTime.MinValue;
			aTCWaypoint.calculated_arrival_time_expected_valud = false;
		}
		SimulatorInformationProcessing.elapsedMiles = 0.0;
		SimulatorConnectionManager.CleanLog();
		dataGridView1.Refresh();
	}

	private void ShowPlan()
	{
		lblPlanDescription.Text = FlightPlan.Descr + Environment.NewLine + "From: " + FlightPlan.DepartureID + " / " + FlightPlan.DepartureName + Environment.NewLine + "To: " + FlightPlan.DestinationID + " / " + FlightPlan.DestinationName + Environment.NewLine + "Cruising Altitude: " + FlightPlan.CruisingAlt + "ft  distance:" + FlightPlan.Destination.DistanceFromMiles(FlightPlan.Departure).ToString("F0") + "nm" + Environment.NewLine + "Type: " + FlightPlan.FPType;
		dataGridView1.DataSource = null;
		dataGridView1.DataSource = FlightPlan.ATCWaypoints;
		showRunways();
	}

	private void showRunways()
	{
		if (FlightPlan.TakeOffRunway != null)
		{
			lblTakeOffRunway.Text = FlightPlan.TakeOffRunway.id + " - " + FlightPlan.TakeOffRunway.runwayThreshold.ToString();
		}
		if (FlightPlan.LandingRunway != null)
		{
			lblLandingRunway.Text = FlightPlan.LandingRunway.id + " - " + FlightPlan.LandingRunway.runwayThreshold.ToString();
		}
	}

	private void btnTransmitClientEventToSim_Click(object sender, EventArgs e)
	{
		try
		{
			int num = (int)Enum.Parse(typeof(SimulatorConnectionManager.EVENTS), ((Button)sender).Text.Trim());
			SimulatorConnectionManager.MySim.TransmitClientEvent(SimConnect.SIMCONNECT_OBJECT_ID_USER, (SimulatorConnectionManager.EVENTS)num, 0u, SimulatorConnectionManager.GROUP.ID_PRIORITY_STANDARD, SIMCONNECT_EVENT_FLAG.GROUPID_IS_PRIORITY);
			object[] obj = new object[5] { "Event ", num, " ", null, null };
			SimulatorConnectionManager.EVENTS eVENTS = (SimulatorConnectionManager.EVENTS)num;
			obj[3] = eVENTS.ToString();
			obj[4] = " sent";
			SimulatorConnectionManager.WriteLog(string.Concat(obj));
		}
		catch (Exception ex)
		{
			SimulatorConnectionManager.WriteLogNL(ex.Message);
		}
	}

	public void Sim_ReceiveSimConnectMessage()
	{
		try
		{
			if (SimulatorConnectionManager.MySim != null)
			{
				SimulatorConnectionManager.MySim?.ReceiveMessage();
			}
		}
		catch (Exception ex)
		{
			SimulatorConnectionManager.WriteLogNL("Exception in Sim_ReceiveSimConnectMessage " + ex.Message);
		}
	}

	private IntPtr WndProc(IntPtr hWnd, int iMsg, IntPtr hWParam, IntPtr hLParam, ref bool bHandled)
	{
		try
		{
			if (iMsg == 1026)
			{
				Sim_ReceiveSimConnectMessage();
			}
		}
		catch
		{
			SimulatorConnectionManager.Sim_DisconnectFromSimulator();
		}
		return IntPtr.Zero;
	}

	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	protected override void WndProc(ref Message m)
	{
		try
		{
			int msg = m.Msg;
			if (msg == 1026)
			{
				Sim_ReceiveSimConnectMessage();
				Invalidate();
			}
			base.WndProc(ref m);
		}
		catch (Exception ex)
		{
			SimulatorConnectionManager.WriteLogNL("Exception occuredd in WndProc - " + ex.Message);
		}
	}
}
