using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ToDoListApp.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;


        private const string SortingByAlphabetKey = "sorting_by_alphabet";
        private static readonly bool SortingByAlphabetDefault = true;

        #endregion


        public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}

        public static bool SortingByAlphabet
        {
            get
            {
                return AppSettings.GetValueOrDefault(SortingByAlphabetKey, SortingByAlphabetDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SortingByAlphabetKey, value);
            }
        }
    }
}