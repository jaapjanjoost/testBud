using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using NavBuddy.BuddyWorld;

namespace NavBuddy.FormsAndControls;

public class CtlActivity : UserControl
{
	public Activity activity;

	private FormMain F;

	private DoubleBufferedPanel pnlActivityHeadingDoubleBuffered = new DoubleBufferedPanel();

	private IContainer components = null;

	public Label label1;

	public Label labLocation;

	public GroupBox grpActivity;

	public Label label4;

	public Label labDescription;

	private Panel panelPayload;

	private Button btnDismissActivity;

	public Label label3;

	public Label labReward;

	public Label label2;

	public Label labActivityFullPayload;

	private Button btnAcceptActivity;

	public Label label5;

	private Panel pnlActivityHeading;

	public Label labActivityExpiringDate;

	public CtlActivity()
	{
		InitializeComponent();
		pnlActivityHeadingDoubleBuffered.AutoScroll = false;
		pnlActivityHeadingDoubleBuffered.AutoSize = false;
		pnlActivityHeadingDoubleBuffered.BackColor = Color.Red;
		pnlActivityHeadingDoubleBuffered.BorderStyle = BorderStyle.None;
		pnlActivityHeadingDoubleBuffered.Location = pnlActivityHeading.Location;
		pnlActivityHeadingDoubleBuffered.Name = "pnlActivityHeadingDoubleBuffered";
		pnlActivityHeadingDoubleBuffered.Size = new Size(50, 50);
		pnlActivityHeadingDoubleBuffered.TabIndex = 25;
		pnlActivityHeadingDoubleBuffered.Paint += pnlActivityHeading_Paint;
		grpActivity.Controls.Add(pnlActivityHeadingDoubleBuffered);
		grpActivity.Controls.Remove(pnlActivityHeading);
	}

	private void groupBox5_Enter(object sender, EventArgs e)
	{
	}

	public void RefreshActivityControl(Activity activity, FormMain F)
	{
		if (activity.Failed)
		{
			btnAcceptActivity.BackColor = Color.Red;
			btnAcceptActivity.ForeColor = Color.White;
			btnAcceptActivity.Text = "FAILED";
		}
		else if (activity.Completed)
		{
			btnAcceptActivity.BackColor = Color.Lime;
			btnAcceptActivity.ForeColor = Color.Black;
			btnAcceptActivity.Text = "COMPLETED";
		}
		else if (activity.Expired())
		{
			btnAcceptActivity.BackColor = Color.Red;
			btnAcceptActivity.ForeColor = Color.White;
			btnAcceptActivity.Text = "EXPIRED";
		}
		else if (activity.Accepted)
		{
			btnAcceptActivity.BackColor = Color.Lime;
			btnAcceptActivity.ForeColor = Color.Black;
			btnAcceptActivity.Text = "ACCEPTED";
		}
		else
		{
			btnAcceptActivity.BackColor = Color.Black;
			btnAcceptActivity.ForeColor = Color.Lime;
			btnAcceptActivity.Text = "ACCEPT";
		}
		this.F = F;
		this.activity = activity;
		grpActivity.Text = Enum.GetName(typeof(ActivityType), activity.activityType);
		labLocation.Text = activity.ReferenceAirportIdent;
		labDescription.Text = activity.Description;
		labActivityFullPayload.Text = activity.payloads.Sum((PayLoad p) => p.WeightLb).ToString("F0") + " lbs";
		labReward.Text = activity.Reward.ToString("F0") + " $";
		labActivityExpiringDate.Text = activity.ExpiringDate.ToShortDateString() + " " + activity.ExpiringDate.ToShortTimeString();
		PayLoad payLoad = activity.payloads.Where((PayLoad A) => !A.Delivered).FirstOrDefault();
		panelPayload.Controls.Clear();
		int num = 0;
		if (activity.payloads != null)
		{
			foreach (PayLoad payload in activity.payloads)
			{
				if (payload != null)
				{
					CtlPayload ctlPayload = new CtlPayload();
					panelPayload.Controls.Add(ctlPayload);
					ctlPayload.RefreshPayload(payload);
					ctlPayload.Width = panelPayload.Width - 50;
					ctlPayload.Location = new Point(0, num);
					num += ctlPayload.Height;
				}
			}
		}
		panelPayload.Height = 5 + num + new CtlPayload().Height;
		pnlActivityHeading.Refresh();
		base.Height = panelPayload.Height + panelPayload.Top + 5;
	}

	private void btnDismissActivity_Click(object sender, EventArgs e)
	{
		BuddyWorldManager.world.activities.Remove(activity);
		BuddyWorldManager.SaveBuddyWorld();
		F.RefreshActivityTab();
	}

	private void btnAcceptActivity_Click(object sender, EventArgs e)
	{
		if (activity.Completed)
		{
			MessageBox.Show("Impossible to accept completed activiy");
		}
		if (!activity.Expired())
		{
			activity.Accepted = true;
			BuddyWorldManager.SaveBuddyWorld();
			F.RefreshActivityTab();
		}
		else
		{
			MessageBox.Show("Impossible to accept expired activiy");
		}
	}

	private void pnlActivityHeading_Paint(object sender, PaintEventArgs e)
	{
		Graphics graphics = e.Graphics;
		graphics.SmoothingMode = SmoothingMode.AntiAlias;
		graphics.Clear(Color.Black);
		PayLoad payLoad = activity.payloads.Where((PayLoad A) => !A.Delivered).FirstOrDefault();
		Rectangle clientRectangle = ((Panel)sender).ClientRectangle;
		if (payLoad != null)
		{
			double num = 0.0;
			double num2 = 0.0;
			if (payLoad != null)
			{
				num = payLoad.PathHeading();
				num2 = payLoad.PathMiles();
			}
			if (num >= 0.0)
			{
				graphics.ResetTransform();
				Pen pen = new Pen(new SolidBrush(Color.Lime), 2f);
				Brush brush = new SolidBrush(Color.Lime);
				graphics.DrawRectangle(pen, new Rectangle(clientRectangle.Location, new Size(clientRectangle.Size.Width - 1, clientRectangle.Size.Height - 1)));
				string s = num.ToString("F0") + "°";
				SizeF sizeF = graphics.MeasureString(s, pnlActivityHeading.Font);
				graphics.DrawString(s, pnlActivityHeading.Font, brush, new Point((int)((float)(clientRectangle.Width / 2) - sizeF.Width / 2f), (int)(8f - sizeF.Height / 2f)));
				string s2 = num2.ToString("F0") + "nm";
				sizeF = graphics.MeasureString(s2, pnlActivityHeading.Font);
				graphics.DrawString(s2, pnlActivityHeading.Font, brush, new Point((int)((float)(clientRectangle.Width / 2) - sizeF.Width / 2f), (int)((float)(clientRectangle.Height - 7) - sizeF.Height / 2f)));
				float num3 = 0.6f * (float)Math.Min(clientRectangle.Width / 2, clientRectangle.Height / 2);
				graphics.TranslateTransform(clientRectangle.Width / 2, clientRectangle.Height / 2);
				graphics.RotateTransform((float)num);
				e.Graphics.DrawLine(pen, new PointF(0f, num3), new PointF(0f, 0f - num3));
				e.Graphics.DrawLine(pen, new PointF(-3f, 0f - num3 + 5f), new PointF(0f, 0f - num3));
				e.Graphics.DrawLine(pen, new PointF(3f, 0f - num3 + 5f), new PointF(0f, 0f - num3));
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavBuddy.FormsAndControls.CtlActivity));
		this.label1 = new System.Windows.Forms.Label();
		this.labLocation = new System.Windows.Forms.Label();
		this.grpActivity = new System.Windows.Forms.GroupBox();
		this.labActivityExpiringDate = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.pnlActivityHeading = new System.Windows.Forms.Panel();
		this.btnAcceptActivity = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.labActivityFullPayload = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.labReward = new System.Windows.Forms.Label();
		this.btnDismissActivity = new System.Windows.Forms.Button();
		this.panelPayload = new System.Windows.Forms.Panel();
		this.label4 = new System.Windows.Forms.Label();
		this.labDescription = new System.Windows.Forms.Label();
		this.grpActivity.SuspendLayout();
		base.SuspendLayout();
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(99, 17);
		this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(63, 18);
		this.label1.TabIndex = 13;
		this.label1.Text = "Location:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labLocation.AutoSize = true;
		this.labLocation.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labLocation.ForeColor = System.Drawing.Color.Lime;
		this.labLocation.Location = new System.Drawing.Point(161, 17);
		this.labLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labLocation.Name = "labLocation";
		this.labLocation.Size = new System.Drawing.Size(23, 18);
		this.labLocation.TabIndex = 12;
		this.labLocation.Text = "---";
		this.labLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.grpActivity.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.grpActivity.BackColor = System.Drawing.Color.Black;
		this.grpActivity.Controls.Add(this.labActivityExpiringDate);
		this.grpActivity.Controls.Add(this.label5);
		this.grpActivity.Controls.Add(this.pnlActivityHeading);
		this.grpActivity.Controls.Add(this.btnAcceptActivity);
		this.grpActivity.Controls.Add(this.label2);
		this.grpActivity.Controls.Add(this.labActivityFullPayload);
		this.grpActivity.Controls.Add(this.label3);
		this.grpActivity.Controls.Add(this.labReward);
		this.grpActivity.Controls.Add(this.btnDismissActivity);
		this.grpActivity.Controls.Add(this.panelPayload);
		this.grpActivity.Controls.Add(this.label4);
		this.grpActivity.Controls.Add(this.labDescription);
		this.grpActivity.Controls.Add(this.label1);
		this.grpActivity.Controls.Add(this.labLocation);
		this.grpActivity.Font = new System.Drawing.Font("Calibri", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.grpActivity.ForeColor = System.Drawing.Color.White;
		this.grpActivity.Location = new System.Drawing.Point(4, -2);
		this.grpActivity.Name = "grpActivity";
		this.grpActivity.Size = new System.Drawing.Size(1114, 138);
		this.grpActivity.TabIndex = 26;
		this.grpActivity.TabStop = false;
		this.grpActivity.Enter += new System.EventHandler(groupBox5_Enter);
		this.labActivityExpiringDate.AutoSize = true;
		this.labActivityExpiringDate.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labActivityExpiringDate.ForeColor = System.Drawing.Color.Lime;
		this.labActivityExpiringDate.Location = new System.Drawing.Point(275, 17);
		this.labActivityExpiringDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labActivityExpiringDate.Name = "labActivityExpiringDate";
		this.labActivityExpiringDate.Size = new System.Drawing.Size(23, 18);
		this.labActivityExpiringDate.TabIndex = 27;
		this.labActivityExpiringDate.Text = "---";
		this.labActivityExpiringDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.ForeColor = System.Drawing.Color.White;
		this.label5.Location = new System.Drawing.Point(218, 17);
		this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(57, 18);
		this.label5.TabIndex = 26;
		this.label5.Text = "Expires:";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.pnlActivityHeading.AutoScroll = true;
		this.pnlActivityHeading.AutoSize = true;
		this.pnlActivityHeading.BackColor = System.Drawing.Color.Red;
		this.pnlActivityHeading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlActivityHeading.Location = new System.Drawing.Point(392, 15);
		this.pnlActivityHeading.Name = "pnlActivityHeading";
		this.pnlActivityHeading.Size = new System.Drawing.Size(50, 50);
		this.pnlActivityHeading.TabIndex = 25;
		this.pnlActivityHeading.Paint += new System.Windows.Forms.PaintEventHandler(pnlActivityHeading_Paint);
		this.btnAcceptActivity.BackColor = System.Drawing.SystemColors.GrayText;
		this.btnAcceptActivity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAcceptActivity.ForeColor = System.Drawing.Color.Black;
		this.btnAcceptActivity.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAcceptActivity.Location = new System.Drawing.Point(10, 14);
		this.btnAcceptActivity.Name = "btnAcceptActivity";
		this.btnAcceptActivity.Size = new System.Drawing.Size(81, 25);
		this.btnAcceptActivity.TabIndex = 24;
		this.btnAcceptActivity.Text = "ACCEPT";
		this.btnAcceptActivity.UseVisualStyleBackColor = false;
		this.btnAcceptActivity.Click += new System.EventHandler(btnAcceptActivity_Click);
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.ForeColor = System.Drawing.Color.White;
		this.label2.Location = new System.Drawing.Point(216, 46);
		this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(61, 18);
		this.label2.TabIndex = 23;
		this.label2.Text = "Payload:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labActivityFullPayload.AutoSize = true;
		this.labActivityFullPayload.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labActivityFullPayload.ForeColor = System.Drawing.Color.Lime;
		this.labActivityFullPayload.Location = new System.Drawing.Point(275, 46);
		this.labActivityFullPayload.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labActivityFullPayload.Name = "labActivityFullPayload";
		this.labActivityFullPayload.Size = new System.Drawing.Size(23, 18);
		this.labActivityFullPayload.TabIndex = 22;
		this.labActivityFullPayload.Text = "---";
		this.labActivityFullPayload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.ForeColor = System.Drawing.Color.White;
		this.label3.Location = new System.Drawing.Point(99, 46);
		this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(59, 18);
		this.label3.TabIndex = 21;
		this.label3.Text = "Reward:";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labReward.AutoSize = true;
		this.labReward.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labReward.ForeColor = System.Drawing.Color.Lime;
		this.labReward.Location = new System.Drawing.Point(160, 46);
		this.labReward.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labReward.Name = "labReward";
		this.labReward.Size = new System.Drawing.Size(23, 18);
		this.labReward.TabIndex = 20;
		this.labReward.Text = "---";
		this.labReward.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnDismissActivity.BackColor = System.Drawing.Color.Gray;
		this.btnDismissActivity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnDismissActivity.ForeColor = System.Drawing.Color.Black;
		this.btnDismissActivity.Image = (System.Drawing.Image)resources.GetObject("btnDismissActivity.Image");
		this.btnDismissActivity.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnDismissActivity.Location = new System.Drawing.Point(10, 43);
		this.btnDismissActivity.Name = "btnDismissActivity";
		this.btnDismissActivity.Size = new System.Drawing.Size(81, 25);
		this.btnDismissActivity.TabIndex = 19;
		this.btnDismissActivity.Text = "DISMISS";
		this.btnDismissActivity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnDismissActivity.UseVisualStyleBackColor = false;
		this.btnDismissActivity.Click += new System.EventHandler(btnDismissActivity_Click);
		this.panelPayload.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panelPayload.AutoScroll = true;
		this.panelPayload.BackColor = System.Drawing.Color.Black;
		this.panelPayload.Location = new System.Drawing.Point(10, 75);
		this.panelPayload.Name = "panelPayload";
		this.panelPayload.Size = new System.Drawing.Size(1096, 56);
		this.panelPayload.TabIndex = 18;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.White;
		this.label4.Location = new System.Drawing.Point(449, 17);
		this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(82, 18);
		this.label4.TabIndex = 17;
		this.label4.Text = "Description:";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.labDescription.AutoSize = true;
		this.labDescription.Font = new System.Drawing.Font("Calibri", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.labDescription.ForeColor = System.Drawing.Color.Lime;
		this.labDescription.Location = new System.Drawing.Point(535, 17);
		this.labDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
		this.labDescription.Name = "labDescription";
		this.labDescription.Size = new System.Drawing.Size(23, 18);
		this.labDescription.TabIndex = 16;
		this.labDescription.Text = "---";
		this.labDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.Controls.Add(this.grpActivity);
		base.Margin = new System.Windows.Forms.Padding(0);
		base.Name = "CtlActivity";
		base.Size = new System.Drawing.Size(1122, 140);
		this.grpActivity.ResumeLayout(false);
		this.grpActivity.PerformLayout();
		base.ResumeLayout(false);
	}
}
