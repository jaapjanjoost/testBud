using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace NavBuddy;

public static class DataManager
{
	private const string NAVBUDDY_DATA_DIR = "NavBuddy";

	public static string DataFolder()
	{
		return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NavBuddy";
	}

	public static void CheckDataFolder()
	{
		if (!Directory.Exists(DataFolder()))
		{
			Directory.CreateDirectory(DataFolder());
		}
	}

	private static string SerializeObject(object obj)
	{
		return SerializeObject(obj, 0);
	}

	private static string SerializeObject(object obj, int tabLevel)
	{
		int num = 0;
		StringBuilder stringBuilder = new StringBuilder();
		string text = new string('\t', tabLevel);
		stringBuilder.Append(text + "{" + Environment.NewLine);
		if (obj != null)
		{
			PropertyInfo[] properties = obj.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.Name == "Count")
				{
					num = (int)propertyInfo.GetValue(obj);
				}
				try
				{
					ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
					if (indexParameters.Length == 0)
					{
						stringBuilder.Append(SerializeValue(propertyInfo, propertyInfo.GetValue(obj), text, tabLevel));
						continue;
					}
					object[] array = new object[1];
					for (int j = 0; j < num; j++)
					{
						array[0] = j;
						stringBuilder.Append(SerializeValue(propertyInfo, propertyInfo.GetValue(obj, array), text, tabLevel));
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
		stringBuilder.Append(text + "}" + Environment.NewLine);
		return stringBuilder.ToString();
	}

	private static string SerializeValue(PropertyInfo Pi, object value, string rientro, int tabLevel)
	{
		CultureInfo invariantCulture = CultureInfo.InvariantCulture;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(rientro + Pi.Name);
		stringBuilder.Append("=");
		if (value != null)
		{
			Type propertyType = Pi.PropertyType;
			switch (Type.GetTypeCode(propertyType))
			{
			case TypeCode.Boolean:
				stringBuilder.Append(value.ToString());
				break;
			case TypeCode.String:
				stringBuilder.Append(value.ToString());
				break;
			case TypeCode.Double:
				stringBuilder.Append(((double)value).ToString("G", invariantCulture));
				break;
			case TypeCode.DateTime:
				stringBuilder.Append(((DateTime)value).ToString("G", invariantCulture));
				break;
			case TypeCode.Int16:
			case TypeCode.Int32:
				stringBuilder.Append(((int)value).ToString("G", invariantCulture));
				break;
			case TypeCode.Int64:
				stringBuilder.Append(((long)value).ToString("G", invariantCulture));
				break;
			default:
				if (Pi.PropertyType.Name == typeof(TimeSpan).Name)
				{
					stringBuilder.Append((int)((TimeSpan)value).TotalSeconds);
				}
				else
				{
					stringBuilder.Append(Environment.NewLine + SerializeObject(value, tabLevel + 1));
				}
				break;
			}
		}
		stringBuilder.Append(Environment.NewLine);
		return stringBuilder.ToString();
	}

	private static object DeserializeObject(string text, Type T)
	{
		object obj = null;
		try
		{
			obj = Activator.CreateInstance(T);
			string[] array = text.Split('\r', '\n');
			for (int i = 1; array[i].Trim() != "}"; i++)
			{
				string text2 = array[i].Trim();
				if (!(text2 != "") || !(text2 != "{"))
				{
					continue;
				}
				string[] array2 = text2.Split('=');
				if (array2.Length == 2)
				{
					PropertyInfo property = T.GetProperty(array2[0]);
					if (property != null)
					{
						string text3 = array2[1];
						Type propertyType = property.PropertyType;
						switch (Type.GetTypeCode(propertyType))
						{
						case TypeCode.Boolean:
							property.SetValue(obj, bool.Parse(text3));
							continue;
						case TypeCode.String:
							property.SetValue(obj, text3);
							continue;
						case TypeCode.Double:
							property.SetValue(obj, double.Parse(text3, CultureInfo.InvariantCulture));
							continue;
						case TypeCode.DateTime:
						{
							if (DateTime.TryParseExact(text3, "G", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
							{
								property.SetValue(obj, result);
							}
							continue;
						}
						case TypeCode.Int16:
						case TypeCode.Int32:
						case TypeCode.Int64:
							if (!(property.GetSetMethod() == null))
							{
								property.SetValue(obj, int.Parse(text3));
							}
							continue;
						}
						if (property.PropertyType.Name == typeof(TimeSpan).Name)
						{
							TimeSpan timeSpan = new TimeSpan(0, 0, int.Parse(text3));
							property.SetValue(obj, timeSpan);
							continue;
						}
						int num = 0;
						int num2 = i + 1;
						while (num < 1)
						{
							if (array[num2].Trim() == "{")
							{
								num++;
							}
							if (array[num2].Trim() == "}")
							{
								num--;
							}
							num2++;
						}
						int num3 = num2 - 1;
						while (num >= 1)
						{
							if (array[num2].Trim() == "{")
							{
								num++;
							}
							if (array[num2].Trim() == "}")
							{
								num--;
							}
							num2++;
						}
						int num4 = num2;
						StringBuilder stringBuilder = new StringBuilder();
						for (int j = num3; j < num4; j++)
						{
							stringBuilder.Append(array[j] + Environment.NewLine);
						}
						if (array2[0] == "Item")
						{
							object[] parameters = new object[1] { DeserializeObject(stringBuilder.ToString(), property.PropertyType) };
							obj.GetType().GetMethod("Add").Invoke(obj, parameters);
						}
						else
						{
							property.SetValue(obj, DeserializeObject(stringBuilder.ToString(), property.PropertyType));
						}
						i = num4 + 1;
					}
					else
					{
						MessageBox.Show("Object " + T.Name + " doesn't contain a property called " + array2[0] + ". This can occur due to a file belonging to older version of NavBuddy or to a misformatted file");
					}
				}
				else if (array2.Length != 3)
				{
				}
			}
			return obj;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return null;
		}
	}

	public static void AppendObjectToFile(string file, object obj)
	{
	}

	public static void SaveObjectIntoFile(ISalvableDataObject obj, string specificFilename = null, string extension = ".dat")
	{
		CheckDataFolder();
		string path = DataFolder() + "\\" + (specificFilename ?? obj.FileIdentifier()) + extension;
		File.WriteAllText(path, SerializeObject(obj));
		SimulatorConnectionManager.WriteLogNL("Saved " + obj.FileIdentifier());
	}

	public static object LoadObjectFromFile(string filename, Type T, string extension = ".dat")
	{
		CheckDataFolder();
		if (File.Exists(DataFolder() + "\\" + filename + extension))
		{
			string text = File.ReadAllText(DataFolder() + "\\" + filename + extension);
			text = text.Replace("\t", "");
			SimulatorConnectionManager.WriteLogNL("Loaded " + filename);
			return DeserializeObject(text, T);
		}
		SimulatorConnectionManager.WriteLogNL(filename + " doesn't exist");
		return null;
	}

	public static void DeleteCorrespondingFile(ISalvableDataObject obj)
	{
		CheckDataFolder();
		string path = DataFolder() + "\\" + obj.FileIdentifier() + ".dat";
		File.Delete(path);
		SimulatorConnectionManager.WriteLogNL("Delete " + obj.FileIdentifier());
	}
}
