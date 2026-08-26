using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NavBuddy.FormsAndControls;

public class FormCompass : Form
{
	private DoubleBufferedPanel panelCompass = new DoubleBufferedPanel();

	private IContainer components = null;

	public Button btnBuddyPilot;

	public Button btnTrueMag;

	public Button btnCustom;

	public FormCompass()
	{
		InitializeComponent();
		panelCompass.BackColor = Color.Black;
		panelCompass.Location = new Point(2, 2);
		panelCompass.Size = base.ClientSize - new Size(4, 4);
		panelCompass.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		panelCompass.Name = "pnlHeading";
		panelCompass.TabIndex = 0;
		panelCompass.Paint += PanelCompassPainter.Compass_Paint;
		base.Controls.Add(panelCompass);
	}

	private void FormCompass_FormClosed(object sender, FormClosedEventArgs e)
	{
	}

	private void FormCompass_Load(object sender, EventArgs e)
	{
	}

	public void ProcessSmallInfroFromSimulator(SimulatorConnectionManager.Struct2 simulatorData)
	{
		panelCompass.Tag = simulatorData;
		panelCompass.Refresh();
	}

	private void btnBuddyPilot_Click(object sender, EventArgs e)
	{
		PanelCompassPainter.drawBuddyPilotReference = !PanelCompassPainter.drawBuddyPilotReference;
		if (PanelCompassPainter.drawBuddyPilotReference)
		{
			((Button)sender).ForeColor = Color.Lime;
		}
		else
		{
			((Button)sender).ForeColor = Color.Black;
		}
	}

	private void btnTrueMag_Click(object sender, EventArgs e)
	{
		if (PanelCompassPainter.trueOrMagnetic == "TRUE")
		{
			PanelCompassPainter.trueOrMagnetic = "MAG";
		}
		else
		{
			PanelCompassPainter.trueOrMagnetic = "TRUE";
		}
	}

	private void btnCustom_Click(object sender, EventArgs e)
	{
		PanelCompassPainter.drawCustomReference = !PanelCompassPainter.drawCustomReference;
		if (PanelCompassPainter.drawCustomReference)
		{
			((Button)sender).ForeColor = Color.Lime;
		}
		else
		{
			((Button)sender).ForeColor = Color.Black;
		}
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
		this.btnBuddyPilot = new System.Windows.Forms.Button();
		this.btnTrueMag = new System.Windows.Forms.Button();
		this.btnCustom = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.btnBuddyPilot.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnBuddyPilot.BackColor = System.Drawing.Color.Gray;
		this.btnBuddyPilot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnBuddyPilot.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnBuddyPilot.ForeColor = System.Drawing.Color.Black;
		this.btnBuddyPilot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnBuddyPilot.Location = new System.Drawing.Point(80, 259);
		this.btnBuddyPilot.Margin = new System.Windows.Forms.Padding(0);
		this.btnBuddyPilot.Name = "btnBuddyPilot";
		this.btnBuddyPilot.Size = new System.Drawing.Size(44, 22);
		this.btnBuddyPilot.TabIndex = 30;
		this.btnBuddyPilot.Text = "B.P.";
		this.btnBuddyPilot.UseVisualStyleBackColor = false;
		this.btnBuddyPilot.Click += new System.EventHandler(btnBuddyPilot_Click);
		this.btnTrueMag.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnTrueMag.BackColor = System.Drawing.Color.Gray;
		this.btnTrueMag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnTrueMag.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnTrueMag.ForeColor = System.Drawing.Color.Black;
		this.btnTrueMag.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnTrueMag.Location = new System.Drawing.Point(6, 259);
		this.btnTrueMag.Margin = new System.Windows.Forms.Padding(0);
		this.btnTrueMag.Name = "btnTrueMag";
		this.btnTrueMag.Size = new System.Drawing.Size(62, 22);
		this.btnTrueMag.TabIndex = 31;
		this.btnTrueMag.Text = "GEO/MAG";
		this.btnTrueMag.UseVisualStyleBackColor = false;
		this.btnTrueMag.Click += new System.EventHandler(btnTrueMag_Click);
		this.btnCustom.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.btnCustom.BackColor = System.Drawing.Color.Gray;
		this.btnCustom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnCustom.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCustom.ForeColor = System.Drawing.Color.Black;
		this.btnCustom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnCustom.Location = new System.Drawing.Point(258, 259);
		this.btnCustom.Margin = new System.Windows.Forms.Padding(0);
		this.btnCustom.Name = "btnCustom";
		this.btnCustom.Size = new System.Drawing.Size(58, 22);
		this.btnCustom.TabIndex = 32;
		this.btnCustom.Text = "CUSTOM";
		this.btnCustom.UseVisualStyleBackColor = false;
		this.btnCustom.Click += new System.EventHandler(btnCustom_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(325, 290);
		base.Controls.Add(this.btnCustom);
		base.Controls.Add(this.btnTrueMag);
		base.Controls.Add(this.btnBuddyPilot);
		base.Name = "FormCompass";
		this.Text = "Compass";
		base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(FormCompass_FormClosed);
		base.Load += new System.EventHandler(FormCompass_Load);
		base.ResumeLayout(false);
	}
}
