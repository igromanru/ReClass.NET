namespace ReClassNET
{
	public class Constants
	{
		public const string ApplicationName = "SuoerClass";

		public const string ApplicationExecutableName = ApplicationName + ".exe";

		public const string ApplicationVersion = "1.2";

		public const string LauncherExecutableName = ApplicationName + "_Launcher.exe";

		public const string Author = "Igro";

		public const string HomepageUrl = "";

		public const string HelpUrl = "";

		public const string PluginUrl = "";

#if RECLASSNET64
		public const string Platform = "x64";

		public const string AddressHexFormat = "X016";
#else
		public const string Platform = "x86";

		public const string AddressHexFormat = "X08";
#endif

		public const string SettingsFile = "settings.xml";

		public const string PluginsFolder = "Plugins";

		public static class CommandLineOptions
		{
			public const string AttachTo = "attachto";

			public const string FileExtRegister = "registerfileext";
			public const string FileExtUnregister = "unregisterfileext";
		}
	}
}
