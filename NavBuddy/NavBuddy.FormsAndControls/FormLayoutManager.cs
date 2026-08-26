using System.Drawing;
using System.Windows.Forms;

namespace NavBuddy.FormsAndControls;

public static class FormLayoutManager
{
	public static void ManageLayout(Control C)
	{
		if (C.GetType().Equals(typeof(Label)))
		{
			Label label = (Label)C;
			Font font = label.Font;
			label.Font = new Font("Calibri", font.Size);
		}
		else if (C.GetType().Equals(typeof(TextBox)))
		{
			TextBox textBox = (TextBox)C;
			textBox.BorderStyle = BorderStyle.None;
			textBox.BackColor = Color.Black;
			textBox.ForeColor = Color.Lime;
			Font font2 = textBox.Font;
			textBox.Font = new Font("Calibri", font2.Size);
			Panel panel = new Panel();
			panel.Size = textBox.Size + new Size(2, 2);
			if (textBox.Multiline)
			{
				panel.Size = textBox.Size + new Size(2, 2);
			}
			panel.BorderStyle = BorderStyle.None;
			panel.Location = textBox.Location + new Size(-1, 3);
			panel.BackColor = Color.Lime;
			panel.Parent = C.Parent;
			textBox.Location += new Size(0, 4);
		}
		else if (C.GetType().Equals(typeof(NumericUpDown)))
		{
			NumericUpDown numericUpDown = (NumericUpDown)C;
			numericUpDown.BorderStyle = BorderStyle.None;
			numericUpDown.BackColor = Color.Black;
			numericUpDown.ForeColor = Color.Lime;
			Font font3 = numericUpDown.Font;
			numericUpDown.Font = new Font("Calibri", font3.Size);
			Panel panel2 = new Panel();
			panel2.Size = numericUpDown.Size + new Size(2, 2);
			panel2.BorderStyle = BorderStyle.None;
			panel2.Location = numericUpDown.Location + new Size(-1, 3);
			panel2.BackColor = Color.Lime;
			panel2.Parent = C.Parent;
			numericUpDown.Location += new Size(0, 4);
		}
		else if (C.GetType().Equals(typeof(Button)))
		{
			Button button = (Button)C;
			if (button.Name.Contains("Waypoint"))
			{
				button.FlatStyle = FlatStyle.Flat;
				button.BackColor = Color.Black;
				button.ForeColor = Color.Lime;
				Font font4 = button.Font;
				button.Font = new Font("Calibri", font4.Size);
			}
			else
			{
				button.FlatStyle = FlatStyle.Flat;
				if (button.BackColor != Color.Black)
				{
					button.BackColor = Color.Gray;
				}
				button.ForeColor = Color.Black;
				Font font5 = button.Font;
				button.Font = new Font("Calibri", font5.Size);
			}
		}
		else if (C.GetType().Equals(typeof(GroupBox)))
		{
			GroupBox groupBox = (GroupBox)C;
			groupBox.BackColor = Color.Black;
			groupBox.ForeColor = Color.White;
			Font font6 = groupBox.Font;
			groupBox.Font = new Font("Calibri", font6.Size);
		}
		else if (C.GetType().Equals(typeof(ListBox)))
		{
			ListBox listBox = (ListBox)C;
			listBox.BackColor = Color.Black;
			listBox.ForeColor = Color.Lime;
			listBox.DrawMode = DrawMode.OwnerDrawFixed;
			listBox.DrawItem += listBox1_DrawItem;
			listBox.BorderStyle = BorderStyle.FixedSingle;
			Font font7 = listBox.Font;
			listBox.Font = new Font("Calibri", font7.Size);
		}
		else if (C.GetType().Equals(typeof(Form)))
		{
			Form form = (Form)C;
			form.BackColor = Color.Black;
			form.ForeColor = Color.White;
		}
		if (!C.HasChildren)
		{
			return;
		}
		foreach (Control control in C.Controls)
		{
			ManageLayout(control);
		}
	}

	private static void listBox1_DrawItem(object sender, DrawItemEventArgs e)
	{
		ListBox listBox = (ListBox)sender;
		e.DrawBackground();
		bool flag = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
		int index = e.Index;
		if (index >= 0 && index < listBox.Items.Count)
		{
			Graphics graphics = e.Graphics;
			SolidBrush solidBrush = new SolidBrush(flag ? Color.Lime : Color.Black);
			graphics.FillRectangle(solidBrush, e.Bounds);
			object obj = listBox.Items[index];
			string s = obj.ToString();
			SolidBrush solidBrush2 = (flag ? new SolidBrush(Color.Black) : new SolidBrush(Color.Lime));
			graphics.DrawString(s, e.Font, solidBrush2, listBox.GetItemRectangle(index).Location);
			solidBrush.Dispose();
			solidBrush2.Dispose();
		}
		e.DrawFocusRectangle();
	}
}
