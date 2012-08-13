using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using mCubed.Core;

namespace mCubed.Controls
{
	/// <summary>
	/// Used to display the time of which the progress slider will seek to
	/// in a human readable format [Multi-Binding, OneWay]
	/// </summary>
	public class ProgressSliderToolTipConverter : IMultiValueConverter
	{
		#region IMultiValueConverter Members

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var value = values.OfType<double>().ToArray();
			if (value.Length == 2)
			{
				return TimeSpan.FromMilliseconds(value[0] * value[1]).Format();
			}
			return null;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to determine if the given object is an instance of a
	/// group item or not [Binding, One-Way]
	/// </summary>
	public class IsGroupItemConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return GroupListHelper.IsGroupList(value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to specify the indentation level margin for the group item
	/// based off the depth of the group item [Binding, One-Way]
	/// </summary>
	public class GroupItemIndentConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int depth = 0;
			if (value is int)
				depth = (int)value - 1;
			return new Thickness(depth * 25, 0, 0, 0);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to specify the margin of a group item and also forces the group item to remain
	/// stationary when its containing ScrollViewer is scrolled horizontally [Binding, One-Way]
	/// </summary>
	public class GroupItemMarginConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return new Thickness((double)value + 5, 5, 5, 5);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to specify the width of a group item, which should remain to be equivalent to the viewport
	/// width of its contain ScrollViewer while taking into account its margin [Binding, One-Way]
	/// </summary>
	public class GroupItemWidthConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (double)value - 12;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to specify the array of directories that will be used in which one of the following conditions will be met:
	/// 0 items returns null, 1 item returns an empty array, 2 or more items returns an array of those items [Binding, One-Way]
	/// </summary>
	public class DirectoryArrayConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Convert the value to directories
			var directories = value as IEnumerable<string>;
			if (directories == null)
			{
				return null;
			}

			// 0 items returns null
			var items = directories.ToArray();
			if (items.Length == 0)
			{
				return null;
			}

			// 1 item returns an empty array
			if (items.Length == 1)
			{
				return new string[0];
			}

			// 2 or more items returns the array
			return items;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to specify whether a library should be enabled or not in which 0 directories in the
	/// library returns false and 1 or more directories returns true [Binding, One-Way]
	/// </summary>
	public class DirectoryCountConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			// Convert the value to directories
			var directories = value as IEnumerable<string>;
			if (directories == null)
			{
				return false;
			}

			// 1 or more items returns true, otherwise false
			return directories.Any();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to specify whether the "Execute Command..." context menu option is enabled or disabled.
	/// Enabled means there is at least 1 command, or disabled means there are 0 commands [Binding, One-Way]
	/// </summary>
	public class CommandCountConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var count = value as int?;
			return count.HasValue && count.Value > 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to specify the collection of columns to be able to select from. It should filter out the
	/// already selected columns if the selected columns should not be shown. [Multi-Binding, One-Way]
	/// </summary>
	public class ColumnListConverter : IMultiValueConverter
	{
		#region IMultiValueConverter Members

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			/*
			 * 0 == AllColumns (IEnumerable<ColumnDetail>)
			 * 1 == SelectedColumns (IEnumerable<ColumnVector>)
			 * 2 == ShowSelectedColumns (bool)
			 */

			// Make sure the parameters came back properly
			if (values.Length != 3)
				return null;
			var all = values[0] as IEnumerable<ColumnDetail>;
			var sel = values[1] as IEnumerable<ColumnVector>;
			var showSel = values[2] is bool && (bool)values[2];
			if (all == null || sel == null)
				return null;

			// Return all of them if we're including the selected
			if (showSel)
				return all.WrapEnumerable();

			// We have to filter otherwise
			return all.Where(detail => !sel.Any(vector => vector.ColumnDetail == detail)).ToArray();
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to determine if a given column is within the selected columns or not. It takes the collection
	/// of selected columns and the column to determine if it's within it. [Multi-Binding, One-Way]
	/// </summary>
	public class IsColumnSelectedConverter : IMultiValueConverter
	{
		#region IMultiValueConverter Members

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			/*
			 * 0 == SelectedColumns (IEnumerable<ColumnVector>)
			 * 1 == ColumnDetail (ColumnDetail)
			 */

			// Make sure the parameters came back properly
			if (values.Length != 2)
				return null;
			var sel = values[0] as IEnumerable<ColumnVector>;
			var det = values[1] as ColumnDetail;
			if (sel == null || det == null)
				return false;

			// Check if it's selected
			return sel.Any(vector => vector.ColumnDetail == det);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to display the list of selected columns as a readable string for a reference
	/// to the user as to what the organization currently is. [Binding, One-Way]
	/// </summary>
	public class ColumnOrganizerConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var columns = value as IEnumerable<ColumnVector> ?? Enumerable.Empty<ColumnVector>();
			return columns.Select(v => v.ColumnDetail.Display).AggregateIfAny((s1, s2) => s1 += ", " + s2, "N/A");
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to determine if a popup should be open or not based on whether the mouse is 
	/// over some invoking element or the popup element itself [Multi-Binding, One-Way]
	/// </summary>
	public class PopupOpenConverter : IMultiValueConverter
	{
		#region IMultiValueConverter Members

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			// 0 == InvokingElement.IsMouseOver, 1 == PopupElement.IsMouseOver
			bool[] bools = values.OfType<bool>().ToArray();
			return bools.Length == 2 && (bools[0] || bools[1]);
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to convert the given media state into a description of what the play/pause
	/// button will perform when it is clicked again. [Binding, One-Way]
	/// </summary>
	public class MediaStateToDescriptionConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is MediaState)
			{
				if ((MediaState)value == MediaState.Play)
				{
					return "Pause playback of the current media";
				}
				else if ((MediaState)value == MediaState.Pause)
				{
					return "Resume playback of the current media";
				}
			}
			return "Play the current loaded media";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to convert the given media state into an image source that can be used
	/// for the play/pause button. The pause image is returned if the media state is
	/// Play. Otherwise, the play image is returned. [Binding, One-Way]
	/// </summary>
	public class MediaStateToImageConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var isPlay = value is MediaState && (MediaState)value == MediaState.Play;
			return new BitmapImage(new Uri(string.Format("pack://application:,,,/Icons/taskbar_{0}.png", isPlay ? "pause" : "play")));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}

	/// <summary>
	/// Used to convert a bool value into a visibility. There are two properties that are
	/// used to determine the visiblity if the bool is true or false. [Binding, Two-Way]
	/// </summary>
	public class BoolToVisibilityConverter : IValueConverter
	{
		#region Constructors

		/// <summary>
		/// Constructs a new converter used to convert a bool into a visibility.
		/// </summary>
		public BoolToVisibilityConverter()
		{
			FalseVisibility = Visibility.Collapsed;
			TrueVisibility = Visibility.Visible;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Get/set the visibility to use when the value is false. The default is collapsed.
		/// </summary>
		public Visibility FalseVisibility { get; set; }

		/// <summary>
		/// Get/set the visibility to use when the value is true. The default is visible.
		/// </summary>
		public Visibility TrueVisibility { get; set; }

		#endregion

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is bool && (bool)value ? TrueVisibility : FalseVisibility;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is Visibility && (Visibility)value == TrueVisibility;
		}

		#endregion
	}

	/// <summary>
	/// Used to determine if a given object is null or not. If the object is null, then false
	/// is returned. If the object is not null, then true is returned. [Binding, One-Way]
	/// </summary>
	public class ObjectToBoolConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value != null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}

		#endregion
	}
}