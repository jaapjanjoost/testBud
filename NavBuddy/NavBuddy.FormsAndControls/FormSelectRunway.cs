using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NavBuddy.FormsAndControls;

public class FormSelectRunway : Form
{
	public RunWay selectedRunway;

	public List<RunWay> runwayDatasource;

	public string icao;

	private IContainer components = null;

	private ListBox lstBxRunwaySelection;

	private Button button1;

	private Button button2;

	private TextBox txtLatLonStart;

	private Label label1;

	private Label label3;

	private TextBox txtLatLonEnd;

	private Label label2;

	private TextBox txtWidth;

	private TextBox txtElevationStart;

	private Label label4;

	private Label label5;

	private Label label6;

	private TextBox txtSurface;

	private TextBox txtElevationEnd;

	private TextBox txtIDEnd;

	private TextBox txtIDStart;

	private Label label7;

	private Label label8;

	private GroupBox groupBox1;

	public FormSelectRunway(List<RunWay> runwayDatasource, string ICAO)
	{
		InitializeComponent();
		FormLayoutManager.ManageLayout(this);
		this.runwayDatasource = runwayDatasource;
		icao = ICAO;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		selectedRunway = (RunWay)lstBxRunwaySelection.SelectedItem;
		Close();
	}

	private void FormSelectRunway_Shown(object sender, EventArgs e)
	{
		lstBxRunwaySelection.DisplayMember = "id";
		lstBxRunwaySelection.DataSource = runwayDatasource;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		OurAirportRunway ourAirportRunway = new OurAirportRunway();
		ourAirportRunway.id = "999999";
		ourAirportRunway.airport_ref = "9999";
		ourAirportRunway.airport_ident = icao;
		ourAirportRunway.length_ft = 1.0;
		ourAirportRunway.width_ft = Utility.toDouble(txtWidth.Text);
		ourAirportRunway.surface = txtSurface.Text;
		ourAirportRunway.lighted = "0";
		ourAirportRunway.closed = "0";
		ourAirportRunway.le_ident = txtIDStart.Text;
		ourAirportRunway.le_latitude_deg = Utility.toDouble(txtLatLonStart.Text.Split(',')[0]);
		ourAirportRunway.le_longitude_deg = Utility.toDouble(txtLatLonStart.Text.Split(',')[1]);
		ourAirportRunway.le_elevation_ft = Utility.toDouble(txtElevationStart.Text);
		ourAirportRunway.le_heading_degT = 0.0;
		ourAirportRunway.le_displaced_threshold_ft = 0.0;
		ourAirportRunway.he_ident = txtIDEnd.Text;
		ourAirportRunway.he_latitude_deg = Utility.toDouble(txtLatLonEnd.Text.Split(',')[0]);
		ourAirportRunway.he_longitude_deg = Utility.toDouble(txtLatLonEnd.Text.Split(',')[1]);
		ourAirportRunway.he_elevation_ft = Utility.toDouble(txtElevationEnd.Text);
		ourAirportRunway.he_heading_degT = 0.0;
		ourAirportRunway.he_displaced_threshold_ft = 0.0;
		OurAirportRunway ourAirportRunway2 = ourAirportRunway;
		OurAirportsManager.AddNewCustomRunway(ourAirportRunway2);
		List<RunWay> list = (List<RunWay>)lstBxRunwaySelection.DataSource;
		list.Add(ourAirportRunway2.GetHeRunway());
		list.Add(ourAirportRunway2.GetLeRunway());
		lstBxRunwaySelection.DataSource = null;
		lstBxRunwaySelection.DisplayMember = "id";
		lstBxRunwaySelection.DataSource = list;
	}

	private void FormSelectRunway_Load(object sender, EventArgs e)
	{
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
		this.lstBxRunwaySelection = new System.Windows.Forms.ListBox();
		this.button1 = new System.Windows.Forms.Button();
		this.button2 = new System.Windows.Forms.Button();
		this.txtLatLonStart = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.txtLatLonEnd = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtWidth = new System.Windows.Forms.TextBox();
		this.txtElevationStart = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.txtSurface = new System.Windows.Forms.TextBox();
		this.txtElevationEnd = new System.Windows.Forms.TextBox();
		this.txtIDEnd = new System.Windows.Forms.TextBox();
		this.txtIDStart = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.lstBxRunwaySelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lstBxRunwaySelection.FormattingEnabled = true;
		this.lstBxRunwaySelection.ItemHeight = 18;
		this.lstBxRunwaySelection.Location = new System.Drawing.Point(8, 9);
		this.lstBxRunwaySelection.Name = "lstBxRunwaySelection";
		this.lstBxRunwaySelection.ScrollAlwaysVisible = true;
		this.lstBxRunwaySelection.Size = new System.Drawing.Size(498, 130);
		this.lstBxRunwaySelection.TabIndex = 0;
		this.button1.BackColor = System.Drawing.Color.Gray;
		this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.button1.ForeColor = System.Drawing.Color.Black;
		this.button1.Location = new System.Drawing.Point(438, 151);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(45, 27);
		this.button1.TabIndex = 1;
		this.button1.Text = "OK";
		this.button1.UseVisualStyleBackColor = false;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.button2.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
		this.button2.ForeColor = System.Drawing.Color.Yellow;
		this.button2.Location = new System.Drawing.Point(162, 102);
		this.button2.Name = "button2";
		this.button2.Size = new System.Drawing.Size(75, 27);
		this.button2.TabIndex = 2;
		this.button2.Text = "CREATE";
		this.button2.UseVisualStyleBackColor = false;
		this.button2.Click += new System.EventHandler(button2_Click);
		this.txtLatLonStart.Location = new System.Drawing.Point(85, 18);
		this.txtLatLonStart.Name = "txtLatLonStart";
		this.txtLatLonStart.Size = new System.Drawing.Size(130, 20);
		this.txtLatLonStart.TabIndex = 3;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(11, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(68, 13);
		this.label1.TabIndex = 4;
		this.label1.Text = "Lat/Lon start";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(11, 47);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(66, 13);
		this.label3.TabIndex = 7;
		this.label3.Text = "Lat/Lon end";
		this.txtLatLonEnd.Location = new System.Drawing.Point(85, 44);
		this.txtLatLonEnd.Name = "txtLatLonEnd";
		this.txtLatLonEnd.Size = new System.Drawing.Size(130, 20);
		this.txtLatLonEnd.TabIndex = 6;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(11, 73);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(35, 13);
		this.label2.TabIndex = 9;
		this.label2.Text = "Width";
		this.txtWidth.Location = new System.Drawing.Point(85, 70);
		this.txtWidth.Name = "txtWidth";
		this.txtWidth.Size = new System.Drawing.Size(67, 20);
		this.txtWidth.TabIndex = 8;
		this.txtElevationStart.Location = new System.Drawing.Point(278, 18);
		this.txtElevationStart.Name = "txtElevationStart";
		this.txtElevationStart.Size = new System.Drawing.Size(43, 20);
		this.txtElevationStart.TabIndex = 10;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(221, 21);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(51, 13);
		this.label4.TabIndex = 11;
		this.label4.Text = "Elevation";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(221, 47);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(51, 13);
		this.label5.TabIndex = 12;
		this.label5.Text = "Elevation";
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(180, 73);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(57, 13);
		this.label6.TabIndex = 14;
		this.label6.Text = "SURFACE";
		this.txtSurface.Location = new System.Drawing.Point(254, 70);
		this.txtSurface.Name = "txtSurface";
		this.txtSurface.Size = new System.Drawing.Size(67, 20);
		this.txtSurface.TabIndex = 13;
		this.txtElevationEnd.Location = new System.Drawing.Point(278, 44);
		this.txtElevationEnd.Name = "txtElevationEnd";
		this.txtElevationEnd.Size = new System.Drawing.Size(43, 20);
		this.txtElevationEnd.TabIndex = 15;
		this.txtIDEnd.Location = new System.Drawing.Point(352, 42);
		this.txtIDEnd.Name = "txtIDEnd";
		this.txtIDEnd.Size = new System.Drawing.Size(43, 20);
		this.txtIDEnd.TabIndex = 17;
		this.txtIDStart.Location = new System.Drawing.Point(352, 16);
		this.txtIDStart.Name = "txtIDStart";
		this.txtIDStart.Size = new System.Drawing.Size(43, 20);
		this.txtIDStart.TabIndex = 16;
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(328, 21);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(18, 13);
		this.label7.TabIndex = 18;
		this.label7.Text = "ID";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(328, 44);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(18, 13);
		this.label8.TabIndex = 19;
		this.label8.Text = "ID";
		this.groupBox1.BackColor = System.Drawing.Color.Maroon;
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.button2);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtLatLonStart);
		this.groupBox1.Controls.Add(this.txtIDEnd);
		this.groupBox1.Controls.Add(this.txtLatLonEnd);
		this.groupBox1.Controls.Add(this.txtIDStart);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtElevationEnd);
		this.groupBox1.Controls.Add(this.txtWidth);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtSurface);
		this.groupBox1.Controls.Add(this.txtElevationStart);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Location = new System.Drawing.Point(512, 3);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(403, 137);
		this.groupBox1.TabIndex = 20;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Create new runway";
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(921, 190);
		base.ControlBox = false;
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.lstBxRunwaySelection);
		base.Name = "FormSelectRunway";
		this.Text = "Select runway";
		base.Load += new System.EventHandler(FormSelectRunway_Load);
		base.Shown += new System.EventHandler(FormSelectRunway_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
