using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace NavBuddy.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				ResourceManager resourceManager = new ResourceManager("NavBuddy.Properties.Resources", typeof(Resources).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static Bitmap cessnaPROJ
	{
		get
		{
			object obj = ResourceManager.GetObject("cessnaPROJ", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap follow
	{
		get
		{
			object obj = ResourceManager.GetObject("follow", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap freccina
	{
		get
		{
			object obj = ResourceManager.GetObject("freccina", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap googlelink
	{
		get
		{
			object obj = ResourceManager.GetObject("googlelink", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap home
	{
		get
		{
			object obj = ResourceManager.GetObject("home", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap lente16
	{
		get
		{
			object obj = ResourceManager.GetObject("lente16", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Spy7x7off
	{
		get
		{
			object obj = ResourceManager.GetObject("Spy7x7off", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap Spy7x7off1
	{
		get
		{
			object obj = ResourceManager.GetObject("Spy7x7off1", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap spyOff
	{
		get
		{
			object obj = ResourceManager.GetObject("spyOff", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap spyOn
	{
		get
		{
			object obj = ResourceManager.GetObject("spyOn", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal static Bitmap user
	{
		get
		{
			object obj = ResourceManager.GetObject("user", resourceCulture);
			return (Bitmap)obj;
		}
	}

	internal Resources()
	{
	}
}
