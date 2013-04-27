using System.Windows.Controls;
using System.Windows.Input;

namespace mCubed.Controls
{
	public class NoMouseWheelScrollViewer : ScrollViewer
	{
		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			// Ignore the mouse wheel event.
		}
	}
}
