using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NavBuddy.FormsAndControls;

public class FormHeliHelp : Form
{
	private DoubleBufferedPanel panelHeliHelp = new DoubleBufferedPanel();

	private IContainer components = null;

	public Button btnReset0Agl;

	public FormHeliHelp()
	{
		InitializeComponent();
		panelHeliHelp.BackColor = Color.Black;
		panelHeliHelp.Location = new Point(2, 2);
		panelHeliHelp.Size = base.ClientSize - new Size(4, 4);
		panelHeliHelp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		panelHeliHelp.Name = "pnlHeading";
		panelHeliHelp.TabIndex = 0;
		panelHeliHelp.Paint += PanelHeliHelpPainter.HeliHelper_Paint;
		base.Controls.Add(panelHeliHelp);
	}

	public void ProcessSmallInfroFromSimulator(SimulatorConnectionManager.Struct2 simulatorData)
	{
		panelHeliHelp.Tag = simulatorData;
		panelHeliHelp.Refresh();
	}

	private void btnReset0Agl_Click(object sender, EventArgs e)
	{
		if (PanelHeliHelpPainter.zeroLevel != 0.0)
		{
			PanelHeliHelpPainter.zeroLevel = 0.0;
		}
		else
		{
			PanelHeliHelpPainter.zeroLevel = PanelHeliHelpPainter.lastSimulatorData.PLANE_ALT_ABOVE_GROUND;
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
		this.btnReset0Agl = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.btnReset0Agl.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnReset0Agl.BackColor = System.Drawing.Color.Gray;
		this.btnReset0Agl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnReset0Agl.Font = new System.Drawing.Font("Calibri", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnReset0Agl.ForeColor = System.Drawing.Color.Black;
		this.btnReset0Agl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnReset0Agl.Location = new System.Drawing.Point(9, 350);
		this.btnReset0Agl.Margin = new System.Windows.Forms.Padding(0);
		this.btnReset0Agl.Name = "btnReset0Agl";
		this.btnReset0Agl.Size = new System.Drawing.Size(88, 22);
		this.btnReset0Agl.TabIndex = 32;
		this.btnReset0Agl.Text = "RESET 0 AGL";
		this.btnReset0Agl.UseVisualStyleBackColor = false;
		this.btnReset0Agl.Click += new System.EventHandler(btnReset0Agl_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(404, 381);
		base.Controls.Add(this.btnReset0Agl);
		base.Name = "FormHeliHelp";
		this.Text = "FromHeliHelp";
		base.ResumeLayout(false);
	}
}
