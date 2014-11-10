using System;
using System.Windows;
using mCubed.Core;

namespace mCubed
{
	public partial class App : Application, ISingleInstanceApp
	{
		#region Data Store

		private const string UNIQUE = "mCubed_Application_Mutex_Version_1.0";

		#endregion

		#region Main

		[STAThread]
		public static void Main()
		{
			if (SingleInstance<App>.InitializeAsFirstInstance(UNIQUE))
			{
				try
				{
					new App().Run();
				}
				finally
				{
					SingleInstance<App>.Cleanup();
				}
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Creates the new application by initializing the component
		/// </summary>
		public App()
		{
			InitializeComponent();
			Startup += new StartupEventHandler(OnStartup);
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Event that handles when the application has started up
		/// </summary>
		/// <param name="sender">The sender object</param>
		/// <param name="e">The event arguments</param>
		private void OnStartup(object sender, StartupEventArgs e)
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);
			HandleCommandLineArgs(e.Args, true);
		}

		/// <summary>
		/// Called when an unhandled exception has been encountered.
		/// </summary>
		/// <param name="sender">The sender object</param>
		/// <param name="e">The event arguments</param>
		private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var exception = e.ExceptionObject as Exception;
			while (exception != null)
			{
				Logger.Log(LogLevel.Exception, LogType.Application, exception);
				exception = exception.InnerException;
			}
		}

		/// <summary>
		/// Event that handles when another instance of the application is opened with the given command line arguments
		/// </summary>
		/// <param name="args">The command line arguments that were used in opening the other instance</param>
		public void SignalExternalCommandLineArgs(string[] args)
		{
			HandleCommandLineArgs(args, false);
		}

		/// <summary>
		/// Handles the command line arguments, whether it's the first instance or if it got sent from another instance
		/// </summary>
		/// <param name="args">The command line arguments that should be handled</param>
		/// <param name="isFirstInstance">True if this is the first instance, or false if this is from another instance</param>
		private void HandleCommandLineArgs(string[] args, bool isFirstInstance)
		{
			Library.GenerateMediaFromCommandLine(args);
		}

		#endregion
	}
}