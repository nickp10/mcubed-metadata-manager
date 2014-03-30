using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace mCubed.Core
{
	public class ScreenUtilities
	{
		#region Static Properties

		public static IEnumerable<ScreenUtilities> AllScreens
		{
			get { return Screen.AllScreens.Select(screen => new ScreenUtilities(screen)); }
		}

		public static ScreenUtilities PrimaryScreen
		{
			get { return new ScreenUtilities(Screen.PrimaryScreen); }
		}

		#endregion

		#region Static Methods

		public static ScreenUtilities GetScreenFrom(Window window)
		{
			var windowInteropHelper = new WindowInteropHelper(window);
			var screen = Screen.FromHandle(windowInteropHelper.Handle);
			return new ScreenUtilities(screen);
		}

		#endregion

		#region Data Store

		private readonly Screen _screen;

		#endregion

		#region Constructors

		internal ScreenUtilities(Screen screen)
		{
			_screen = screen;
		}

		#endregion

		#region Properties

		public Rect DeviceBounds
		{
			get { return GetRect(_screen.Bounds); }
		}

		public string DeviceName
		{
			get { return _screen.DeviceName; }
		}

		public bool IsPrimary
		{
			get { return _screen.Primary; }
		}

		public Rect WorkingArea
		{
			get { return GetRect(_screen.WorkingArea); }
		}

		#endregion

		#region Methods

		private Rect GetRect(Rectangle value)
		{
			return new Rect
			{
				X = value.X,
				Y = value.Y,
				Width = value.Width,
				Height = value.Height
			};
		}

		#endregion
	}
}
