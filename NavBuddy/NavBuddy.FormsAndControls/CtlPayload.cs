using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NavBuddy.BuddyWorld;
using NavBuddy.Properties;

namespace NavBuddy.FormsAndControls;

public class CtlPayload : UserControl
{
	private PayLoad payload = null;

	private IContainer components = null;

	public Label labType;

	public Label label1;

	public Label labWeight;

	public Label label4;

	public Label label6;

	public Label labDescription;

	private Button btnLoadUnload;

	public ImageList imageList16;

	private ContextMenuStrip ContextMenuWayPoint;

	private ToolStripMenuItem toolStripMenuItemCopyReference;

	public Label labDelivered;

	private ToolStripMenuItem infoToolStripMenuItem;

	public Label label2;

	private ToolStripMenuItem useAsFlightPlanToolStripMenuItem;

	private ToolStripMenuItem useAsCustomTargetToolStripMenuItem;

	private Button btnWaypointPosition;

	private Button btnWaypointDestination;

	public CtlPayload()
	{
		InitializeComponent();
	}

	public void RefreshPayload(PayLoad payload)
	{
		this.payload = payload;
		labType.Text = payload.Type;
		labDescription.Text = payload.Description;
		labWeight.Text = payload.WeightLb.ToString("F0");
		btnWaypointPosition.Text = ((payload.Position != null) ? payload.Position.Id : "----");
		btnWaypointDestination.Text = ((payload.Destination != null) ? payload.Destination.Id : "----");
		btnWaypointPosition.Tag = payload.Position;
		btnWaypointDestination.Tag = payload.Destination;
		labDelivered.Visible = payload.Delivered;
		btnLoadUnload.Visible = !payload.Delivered;
		if (payload.Loaded)
		{
			btnLoadUnload.Text = "Unload";
			btnLoadUnload.Image = imageList16.Images["SpyOn"];
		}
		else
		{
			btnLoadUnload.Text = "Load";
			btnLoadUnload.Image = imageList16.Images["SpyOff"];
		}
	}

	private void btnLoadUnload_Click(object sender, EventArgs e)
	{
		try
		{
			if (payload.Loaded)
			{
				BuddyWorldManager.TryToUnload(payload);
			}
			else
			{
				BuddyWorldManager.TryToLoad(payload);
			}
			RefreshPayload(payload);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void labDelivered_Click(object sender, EventArgs e)
	{
	}

	private void labPosition_Click(object sender, EventArgs e)
	{
	}

	private void labDestination_Click(object sender, EventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavBuddy.FormsAndControls.CtlPayload));
		this.labType = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.labWeight = new System.Windows.Forms.Label();
		this.ContextMenuWayPoint = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.toolStripMenuItemCopyReference = new System.Windows.Forms.ToolStripMenuItem();
		this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.useAsFlightPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.useAsCustomTargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.labDescription = new System.Windows.Forms.Label();
		this.imageList16 = new System.Windows.Forms.ImageList(this.components);
		this.labDelivered = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.btnLoadUnload = new System.Windows.Forms.Button();
		this.btnWaypointPosition = new System.Windows.Forms.Button();
		this.btnWaypointDestination = new System.Windows.Forms.Button();
		this.ContextMenuWayPoint.SuspendLayout();
		base.SuspendLayout();
		this.labType.AutoSize = true;
		this.labType.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labType.ForeColor = System.Drawing.Color.Lime;
		this.labType.Location = new System.Drawing.Point(4, 4);
		this.labType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labType.Name = "labType";
		this.labType.Size = new System.Drawing.Size(23, 18);
		this.labType.TabIndex = 25;
		this.labType.Text = "---";
		this.labType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(85, 4);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(32, 18);
		this.label1.TabIndex = 27;
		this.label1.Text = "Lbs:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labWeight.AutoSize = true;
		this.labWeight.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labWeight.ForeColor = System.Drawing.Color.Lime;
		this.labWeight.Location = new System.Drawing.Point(116, 4);
		this.labWeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labWeight.Name = "labWeight";
		this.labWeight.Size = new System.Drawing.Size(23, 18);
		this.labWeight.TabIndex = 26;
		this.labWeight.Text = "---";
		this.labWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.ContextMenuWayPoint.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.toolStripMenuItemCopyReference, this.infoToolStripMenuItem, this.useAsFlightPlanToolStripMenuItem, this.useAsCustomTargetToolStripMenuItem });
		this.ContextMenuWayPoint.Name = "ContextMenuPosition";
		this.ContextMenuWayPoint.Size = new System.Drawing.Size(185, 92);
		this.toolStripMenuItemCopyReference.Name = "toolStripMenuItemCopyReference";
		this.toolStripMenuItemCopyReference.Size = new System.Drawing.Size(184, 22);
		this.toolStripMenuItemCopyReference.Text = "Copy";
		this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
		this.infoToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
		this.infoToolStripMenuItem.Text = "Info";
		this.useAsFlightPlanToolStripMenuItem.Name = "useAsFlightPlanToolStripMenuItem";
		this.useAsFlightPlanToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
		this.useAsFlightPlanToolStripMenuItem.Text = "Use as flight plan";
		this.useAsCustomTargetToolStripMenuItem.Name = "useAsCustomTargetToolStripMenuItem";
		this.useAsCustomTargetToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
		this.useAsCustomTargetToolStripMenuItem.Text = "Use as custom target";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.White;
		this.label4.Location = new System.Drawing.Point(379, 4);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(40, 18);
		this.label4.TabIndex = 31;
		this.label4.Text = "Dest:";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.ForeColor = System.Drawing.Color.White;
		this.label6.Location = new System.Drawing.Point(510, 4);
		this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(41, 18);
		this.label6.TabIndex = 32;
		this.label6.Text = "Desc:";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labDescription.AutoSize = true;
		this.labDescription.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labDescription.ForeColor = System.Drawing.Color.Lime;
		this.labDescription.Location = new System.Drawing.Point(552, 4);
		this.labDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labDescription.Name = "labDescription";
		this.labDescription.Size = new System.Drawing.Size(28, 18);
		this.labDescription.TabIndex = 33;
		this.labDescription.Text = "----";
		this.labDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.imageList16.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList16.ImageStream");
		this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList16.Images.SetKeyName(0, "spyOff");
		this.imageList16.Images.SetKeyName(1, "spyOn");
		this.imageList16.Images.SetKeyName(2, "googlelink");
		this.imageList16.Images.SetKeyName(3, "lente16");
		this.labDelivered.AutoSize = true;
		this.labDelivered.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labDelivered.ForeColor = System.Drawing.Color.Lime;
		this.labDelivered.Location = new System.Drawing.Point(166, 5);
		this.labDelivered.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labDelivered.Name = "labDelivered";
		this.labDelivered.Size = new System.Drawing.Size(69, 18);
		this.labDelivered.TabIndex = 90;
		this.labDelivered.Text = "Delivered";
		this.labDelivered.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labDelivered.Click += new System.EventHandler(labDelivered_Click);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.White;
		this.label2.Location = new System.Drawing.Point(262, 4);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(34, 18);
		this.label2.TabIndex = 91;
		this.label2.Text = "Pos:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnLoadUnload.BackColor = System.Drawing.Color.Gray;
		this.btnLoadUnload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnLoadUnload.ForeColor = System.Drawing.Color.Black;
		this.btnLoadUnload.Image = NavBuddy.Properties.Resources.spyOff;
		this.btnLoadUnload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnLoadUnload.Location = new System.Drawing.Point(168, 2);
		this.btnLoadUnload.Name = "btnLoadUnload";
		this.btnLoadUnload.Size = new System.Drawing.Size(63, 24);
		this.btnLoadUnload.TabIndex = 89;
		this.btnLoadUnload.Text = "Load";
		this.btnLoadUnload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnLoadUnload.UseVisualStyleBackColor = false;
		this.btnLoadUnload.Click += new System.EventHandler(btnLoadUnload_Click);
		this.btnWaypointPosition.BackColor = System.Drawing.Color.Black;
		this.btnWaypointPosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnWaypointPosition.ForeColor = System.Drawing.Color.Lime;
		this.btnWaypointPosition.Image = NavBuddy.Properties.Resources.googlelink;
		this.btnWaypointPosition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnWaypointPosition.Location = new System.Drawing.Point(295, 1);
		this.btnWaypointPosition.Margin = new System.Windows.Forms.Padding(0);
		this.btnWaypointPosition.Name = "btnWaypointPosition";
		this.btnWaypointPosition.Size = new System.Drawing.Size(66, 26);
		this.btnWaypointPosition.TabIndex = 121;
		this.btnWaypointPosition.Text = "----";
		this.btnWaypointPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnWaypointPosition.UseVisualStyleBackColor = false;
		this.btnWaypointPosition.Click += new System.EventHandler(btnWaypointButton_Click);
		this.btnWaypointDestination.BackColor = System.Drawing.Color.Black;
		this.btnWaypointDestination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnWaypointDestination.ForeColor = System.Drawing.Color.Lime;
		this.btnWaypointDestination.Image = NavBuddy.Properties.Resources.googlelink;
		this.btnWaypointDestination.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnWaypointDestination.Location = new System.Drawing.Point(418, 1);
		this.btnWaypointDestination.Margin = new System.Windows.Forms.Padding(0);
		this.btnWaypointDestination.Name = "btnWaypointDestination";
		this.btnWaypointDestination.Size = new System.Drawing.Size(66, 26);
		this.btnWaypointDestination.TabIndex = 122;
		this.btnWaypointDestination.Text = "----";
		this.btnWaypointDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnWaypointDestination.UseVisualStyleBackColor = false;
		this.btnWaypointDestination.Click += new System.EventHandler(btnWaypointButton_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.Controls.Add(this.btnWaypointDestination);
		base.Controls.Add(this.btnWaypointPosition);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.labDelivered);
		base.Controls.Add(this.btnLoadUnload);
		base.Controls.Add(this.labDescription);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.labWeight);
		base.Controls.Add(this.labType);
		base.Name = "CtlPayload";
		base.Size = new System.Drawing.Size(944, 28);
		this.ContextMenuWayPoint.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
