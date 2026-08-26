using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using NavBuddy.BuddyWorld;
using NavBuddy.Properties;

namespace NavBuddy.FormsAndControls;

public class FormWaypoint : Form
{
	public WayPoint WP = null;

	public double heading = 0.0;

	public OurAirport ourAirportRef;

	private IContainer components = null;

	private Button btnCloseForm;

	public GroupBox grpHeading;

	private Button btnTeleport;

	private TextBox txtCoordinates;

	public Label label1;

	public Button btnGoogleMap;

	private GroupBox grpOpenAirport;

	private FlowLayoutPanel flowLayoutPanelOpenAirport;

	public Label labId;

	public Label label4;

	private Button btnSetAsPlanDest;

	private Button btnSetAsPlanDep;

	private Button btnChaseLocation;

	public Label labName;

	public Label labCurrentAirplaneRelative;

	public FormWaypoint()
	{
		InitializeComponent();
		SuspendLayout();
		FormLayoutManager.ManageLayout(this);
		ResumeLayout(performLayout: false);
	}

	private void FormWaypoint_Load(object sender, EventArgs e)
	{
	}

	private void btnCloseForm_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void FormWaypoint_Shown(object sender, EventArgs e)
	{
		txtCoordinates.Text = WP.latitude + "," + WP.longitude;
		labId.Text = WP.Id;
		labName.Text = WP.Type;
		labCurrentAirplaneRelative.Text = "";
		if (BuddyWorldManager.selectedPlane != null && BuddyWorldManager.selectedPlane.position != null)
		{
			double num = BuddyWorldManager.selectedPlane.position.DistanceFromMiles(WP);
			double num2 = BuddyWorldManager.selectedPlane.position.CurrentBearingDegree(WP);
			string registration = BuddyWorldManager.selectedPlane.registration;
			labCurrentAirplaneRelative.Text = "This Waipoint is " + num.ToString("F1") + " miles, " + num2.ToString("F0") + "° degree from " + registration;
		}
		DisplayOpenAirport();
		DisplayFse();
	}

	private void DisplayOpenAirport()
	{
		ourAirportRef = OurAirportsManager.GetAirport(WP.Id);
		if (ourAirportRef != null)
		{
			flowLayoutPanelOpenAirport.Controls.Clear();
			PropertyInfo[] properties = typeof(OurAirport).GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				string name = propertyInfo.Name;
				object value = propertyInfo.GetValue(ourAirportRef, null);
				Label label = new Label();
				FormLayoutManager.ManageLayout(label);
				label.AutoSize = false;
				label.Width = flowLayoutPanelOpenAirport.Width - 30;
				label.Text = name + ": " + (value ?? "null").ToString();
				label.Font = flowLayoutPanelOpenAirport.Font;
				flowLayoutPanelOpenAirport.Controls.Add(label);
			}
		}
	}

	private void DisplayFse()
	{
	}

	private void btnGoogleMap_Click(object sender, EventArgs e)
	{
		Process.Start(WP.googleMapLink);
	}

	private void btnTeleport_Click(object sender, EventArgs e)
	{
		try
		{
			if (WP != null)
			{
				if (SimulatorConnectionManager.MySim == null)
				{
					MessageBox.Show("Connect to simulator to allow teleport");
				}
				else if (MessageBox.Show("Do you want to teleport simulator airplane to BuddyWorld reference location?", "Teleport", MessageBoxButtons.YesNo) != DialogResult.No)
				{
					WayPoint wP = WP;
					SimulatorConnectionManager.Struct3 @struct = new SimulatorConnectionManager.Struct3
					{
						altitude = wP.Altitude,
						latitude = wP.latitude,
						longitude = wP.longitude,
						plane_heading_degree_true = heading
					};
					SimulatorConnectionManager.Sim_TransmitDataToSimConnect(SimulatorConnectionManager.DEFINITIONS1.STRUCT3, @struct);
				}
			}
			else
			{
				MessageBox.Show("Select an airplane first");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnSetAsPlanDep_Click(object sender, EventArgs e)
	{
		if (ourAirportRef != null)
		{
			Program.formMain.txtDepartureIcao.Text = WP.Id;
			Program.formMain.btnLoadOurAirportAsDeparture_Click(null, null);
		}
	}

	private void btnSetAsPlanDest_Click(object sender, EventArgs e)
	{
		if (ourAirportRef != null)
		{
			Program.formMain.txtDestinationIcao.Text = WP.Id;
			Program.formMain.btnLoadOurAirportAsDestination_Click(null, null);
		}
	}

	private void btnChaseLocation_Click(object sender, EventArgs e)
	{
		SimulatorInformationProcessing.customLocation = WP;
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
		this.btnCloseForm = new System.Windows.Forms.Button();
		this.grpHeading = new System.Windows.Forms.GroupBox();
		this.labName = new System.Windows.Forms.Label();
		this.btnSetAsPlanDest = new System.Windows.Forms.Button();
		this.btnSetAsPlanDep = new System.Windows.Forms.Button();
		this.btnChaseLocation = new System.Windows.Forms.Button();
		this.labId = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.grpOpenAirport = new System.Windows.Forms.GroupBox();
		this.flowLayoutPanelOpenAirport = new System.Windows.Forms.FlowLayoutPanel();
		this.btnTeleport = new System.Windows.Forms.Button();
		this.txtCoordinates = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.btnGoogleMap = new System.Windows.Forms.Button();
		this.labCurrentAirplaneRelative = new System.Windows.Forms.Label();
		this.grpHeading.SuspendLayout();
		this.grpOpenAirport.SuspendLayout();
		base.SuspendLayout();
		this.btnCloseForm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnCloseForm.BackColor = System.Drawing.Color.Blue;
		this.btnCloseForm.Location = new System.Drawing.Point(261, 382);
		this.btnCloseForm.Name = "btnCloseForm";
		this.btnCloseForm.Size = new System.Drawing.Size(77, 32);
		this.btnCloseForm.TabIndex = 0;
		this.btnCloseForm.Text = "Ok";
		this.btnCloseForm.UseVisualStyleBackColor = false;
		this.btnCloseForm.Click += new System.EventHandler(btnCloseForm_Click);
		this.grpHeading.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.grpHeading.BackColor = System.Drawing.Color.Black;
		this.grpHeading.Controls.Add(this.labCurrentAirplaneRelative);
		this.grpHeading.Controls.Add(this.labName);
		this.grpHeading.Controls.Add(this.btnSetAsPlanDest);
		this.grpHeading.Controls.Add(this.btnSetAsPlanDep);
		this.grpHeading.Controls.Add(this.btnChaseLocation);
		this.grpHeading.Controls.Add(this.labId);
		this.grpHeading.Controls.Add(this.label4);
		this.grpHeading.Controls.Add(this.grpOpenAirport);
		this.grpHeading.Controls.Add(this.btnTeleport);
		this.grpHeading.Controls.Add(this.btnCloseForm);
		this.grpHeading.Controls.Add(this.txtCoordinates);
		this.grpHeading.Controls.Add(this.label1);
		this.grpHeading.Controls.Add(this.btnGoogleMap);
		this.grpHeading.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpHeading.ForeColor = System.Drawing.Color.White;
		this.grpHeading.Location = new System.Drawing.Point(7, -2);
		this.grpHeading.Name = "grpHeading";
		this.grpHeading.Size = new System.Drawing.Size(599, 423);
		this.grpHeading.TabIndex = 120;
		this.grpHeading.TabStop = false;
		this.labName.AutoSize = true;
		this.labName.BackColor = System.Drawing.Color.Black;
		this.labName.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labName.ForeColor = System.Drawing.Color.Lime;
		this.labName.Location = new System.Drawing.Point(112, 28);
		this.labName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labName.Name = "labName";
		this.labName.Size = new System.Drawing.Size(28, 18);
		this.labName.TabIndex = 126;
		this.labName.Text = "----";
		this.labName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSetAsPlanDest.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSetAsPlanDest.BackColor = System.Drawing.Color.Gray;
		this.btnSetAsPlanDest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSetAsPlanDest.ForeColor = System.Drawing.Color.Black;
		this.btnSetAsPlanDest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSetAsPlanDest.Location = new System.Drawing.Point(501, 22);
		this.btnSetAsPlanDest.Name = "btnSetAsPlanDest";
		this.btnSetAsPlanDest.Size = new System.Drawing.Size(87, 24);
		this.btnSetAsPlanDest.TabIndex = 125;
		this.btnSetAsPlanDest.Text = "PLAN DEST.";
		this.btnSetAsPlanDest.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSetAsPlanDest.UseVisualStyleBackColor = false;
		this.btnSetAsPlanDest.Click += new System.EventHandler(btnSetAsPlanDest_Click);
		this.btnSetAsPlanDep.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnSetAsPlanDep.BackColor = System.Drawing.Color.Gray;
		this.btnSetAsPlanDep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSetAsPlanDep.ForeColor = System.Drawing.Color.Black;
		this.btnSetAsPlanDep.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnSetAsPlanDep.Location = new System.Drawing.Point(401, 23);
		this.btnSetAsPlanDep.Margin = new System.Windows.Forms.Padding(0);
		this.btnSetAsPlanDep.Name = "btnSetAsPlanDep";
		this.btnSetAsPlanDep.Size = new System.Drawing.Size(87, 24);
		this.btnSetAsPlanDep.TabIndex = 124;
		this.btnSetAsPlanDep.Text = "PLAN DEP.";
		this.btnSetAsPlanDep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSetAsPlanDep.UseVisualStyleBackColor = false;
		this.btnSetAsPlanDep.Click += new System.EventHandler(btnSetAsPlanDep_Click);
		this.btnChaseLocation.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnChaseLocation.BackColor = System.Drawing.Color.Gray;
		this.btnChaseLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnChaseLocation.ForeColor = System.Drawing.Color.Black;
		this.btnChaseLocation.Image = NavBuddy.Properties.Resources.follow;
		this.btnChaseLocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnChaseLocation.Location = new System.Drawing.Point(502, 58);
		this.btnChaseLocation.Name = "btnChaseLocation";
		this.btnChaseLocation.Size = new System.Drawing.Size(87, 24);
		this.btnChaseLocation.TabIndex = 123;
		this.btnChaseLocation.Text = "TRACK";
		this.btnChaseLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnChaseLocation.UseVisualStyleBackColor = false;
		this.btnChaseLocation.Click += new System.EventHandler(btnChaseLocation_Click);
		this.labId.AutoSize = true;
		this.labId.BackColor = System.Drawing.Color.Black;
		this.labId.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labId.ForeColor = System.Drawing.Color.Lime;
		this.labId.Location = new System.Drawing.Point(45, 29);
		this.labId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labId.Name = "labId";
		this.labId.Size = new System.Drawing.Size(28, 18);
		this.labId.TabIndex = 122;
		this.labId.Text = "----";
		this.labId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.White;
		this.label4.Location = new System.Drawing.Point(13, 29);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(24, 18);
		this.label4.TabIndex = 121;
		this.label4.Text = "Id:";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.grpOpenAirport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.grpOpenAirport.BackColor = System.Drawing.Color.Maroon;
		this.grpOpenAirport.Controls.Add(this.flowLayoutPanelOpenAirport);
		this.grpOpenAirport.Location = new System.Drawing.Point(16, 91);
		this.grpOpenAirport.Name = "grpOpenAirport";
		this.grpOpenAirport.Size = new System.Drawing.Size(572, 260);
		this.grpOpenAirport.TabIndex = 120;
		this.grpOpenAirport.TabStop = false;
		this.grpOpenAirport.Text = "Open Airport";
		this.flowLayoutPanelOpenAirport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.flowLayoutPanelOpenAirport.AutoScroll = true;
		this.flowLayoutPanelOpenAirport.Font = new System.Drawing.Font("Calibri", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.flowLayoutPanelOpenAirport.Location = new System.Drawing.Point(6, 22);
		this.flowLayoutPanelOpenAirport.Name = "flowLayoutPanelOpenAirport";
		this.flowLayoutPanelOpenAirport.Size = new System.Drawing.Size(560, 232);
		this.flowLayoutPanelOpenAirport.TabIndex = 0;
		this.btnTeleport.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.btnTeleport.BackColor = System.Drawing.Color.Gray;
		this.btnTeleport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnTeleport.ForeColor = System.Drawing.Color.Black;
		this.btnTeleport.Image = NavBuddy.Properties.Resources.freccina;
		this.btnTeleport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnTeleport.Location = new System.Drawing.Point(402, 59);
		this.btnTeleport.Margin = new System.Windows.Forms.Padding(0);
		this.btnTeleport.Name = "btnTeleport";
		this.btnTeleport.Size = new System.Drawing.Size(87, 24);
		this.btnTeleport.TabIndex = 117;
		this.btnTeleport.Text = "TELEPORT";
		this.btnTeleport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnTeleport.UseVisualStyleBackColor = false;
		this.btnTeleport.Click += new System.EventHandler(btnTeleport_Click);
		this.txtCoordinates.BackColor = System.Drawing.Color.White;
		this.txtCoordinates.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCoordinates.ForeColor = System.Drawing.Color.Black;
		this.txtCoordinates.Location = new System.Drawing.Point(115, 59);
		this.txtCoordinates.Margin = new System.Windows.Forms.Padding(0);
		this.txtCoordinates.Name = "txtCoordinates";
		this.txtCoordinates.Size = new System.Drawing.Size(184, 26);
		this.txtCoordinates.TabIndex = 99;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(13, 62);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(87, 18);
		this.label1.TabIndex = 100;
		this.label1.Text = "Coordinates:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGoogleMap.BackColor = System.Drawing.Color.Black;
		this.btnGoogleMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnGoogleMap.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGoogleMap.ForeColor = System.Drawing.Color.Black;
		this.btnGoogleMap.Image = NavBuddy.Properties.Resources.googlelink;
		this.btnGoogleMap.Location = new System.Drawing.Point(322, 60);
		this.btnGoogleMap.Name = "btnGoogleMap";
		this.btnGoogleMap.Size = new System.Drawing.Size(20, 20);
		this.btnGoogleMap.TabIndex = 101;
		this.btnGoogleMap.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGoogleMap.UseVisualStyleBackColor = false;
		this.btnGoogleMap.Click += new System.EventHandler(btnGoogleMap_Click);
		this.labCurrentAirplaneRelative.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.labCurrentAirplaneRelative.AutoSize = true;
		this.labCurrentAirplaneRelative.BackColor = System.Drawing.Color.Black;
		this.labCurrentAirplaneRelative.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labCurrentAirplaneRelative.ForeColor = System.Drawing.Color.Lime;
		this.labCurrentAirplaneRelative.Location = new System.Drawing.Point(19, 357);
		this.labCurrentAirplaneRelative.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labCurrentAirplaneRelative.Name = "labCurrentAirplaneRelative";
		this.labCurrentAirplaneRelative.Size = new System.Drawing.Size(28, 18);
		this.labCurrentAirplaneRelative.TabIndex = 127;
		this.labCurrentAirplaneRelative.Text = "----";
		this.labCurrentAirplaneRelative.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(613, 427);
		base.Controls.Add(this.grpHeading);
		base.Name = "FormWaypoint";
		this.Text = "Waypoint inspector";
		base.Load += new System.EventHandler(FormWaypoint_Load);
		base.Shown += new System.EventHandler(FormWaypoint_Shown);
		this.grpHeading.ResumeLayout(false);
		this.grpHeading.PerformLayout();
		this.grpOpenAirport.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
