using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using mCubed.Core;

namespace mCubed.Controls
{
	public static class TaskbarItemInfoExtensions
	{
		#region Attached Dependency Property: MediaFile

		public static readonly DependencyProperty MediaFileProperty =
			DependencyProperty.RegisterAttached("MediaFile", typeof(MediaFile), typeof(TaskbarItemInfoExtensions), new PropertyMetadata(null, new PropertyChangedCallback(OnMediaFileChanged)));

		/// <summary>
		/// Get the current media file for use by the taskbar information.
		/// </summary>
		/// <param name="item">The item to get its media file for.</param>
		/// <returns>The media file for the specified taskbar information.</returns>
		public static MediaFile GetMediaFile(DependencyObject item)
		{
			return (MediaFile)item.GetValue(MediaFileProperty);
		}

		/// <summary>
		/// Set the current media file for use by the taskbar information.
		/// </summary>
		/// <param name="item">The item to set its media file for.</param>
		/// <param name="mediaFile">The media file for the specified taskbar information.</param>
		public static void SetMediaFile(DependencyObject item, MediaFile mediaFile)
		{
			item.SetValue(MediaFileProperty, mediaFile);
		}

		/// <summary>
		/// Called when the media file has changed for the taskbar information.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="args">The event arguments.</param>
		private static void OnMediaFileChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			var taskbar = sender as TaskbarItemInfo;
			if (taskbar != null)
			{
				UpdateTaskbarOverlay(taskbar);
			}
		}

		#endregion

		#region Attached Dependency Property: MediaState

		public static readonly DependencyProperty MediaStateProperty =
			DependencyProperty.RegisterAttached("MediaState", typeof(MediaState), typeof(TaskbarItemInfoExtensions), new PropertyMetadata(MediaState.Stop, new PropertyChangedCallback(OnMediaStateChanged)));

		/// <summary>
		/// Get the current media state for use by the taskbar information.
		/// </summary>
		/// <param name="item">The item to get its media state for.</param>
		/// <returns>The media state for the specified taskbar information.</returns>
		public static MediaState GetMediaState(DependencyObject item)
		{
			return (MediaState)item.GetValue(MediaStateProperty);
		}

		/// <summary>
		/// Set the current media state for use by the taskbar information.
		/// </summary>
		/// <param name="item">The item to set its media state for.</param>
		/// <param name="mediaState">The media state for the specified taskbar information.</param>
		public static void SetMediaState(DependencyObject item, MediaState mediaState)
		{
			item.SetValue(MediaStateProperty, mediaState);
		}

		/// <summary>
		/// Called when the media state has changed for the taskbar information.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="args">The event arguments.</param>
		private static void OnMediaStateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			var taskbar = sender as TaskbarItemInfo;
			if (taskbar != null)
			{
				UpdateTaskbarOverlay(taskbar);
			}
		}

		#endregion

		#region TaskbarItemInfo Static Methods

		/// <summary>
		/// Updates the overlay icon in the taskbar for the specified taskbar information.
		/// The icon is based on the current media state for the specified taskbar information.
		/// </summary>
		/// <param name="taskbar">The taskbar to update the overlay icon for.</param>
		private static void UpdateTaskbarOverlay(TaskbarItemInfo taskbar)
		{
			var state = GetMediaState(taskbar);
			var file = GetMediaFile(taskbar);
			if (file == null)
			{
				taskbar.ClearValue(TaskbarItemInfo.OverlayProperty);
			}
			else
			{
				taskbar.Overlay = new BitmapImage(new Uri(string.Format("pack://application:,,,/Icons/overlay_{0}.png", state.ToString().ToLower())));
			}
		}

		#endregion
	}
}
