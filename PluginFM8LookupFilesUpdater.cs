using SimHub.Plugins;
using System.Net;

namespace DaZD.FH.IDsToCarModels
{
    [PluginDescription("This plugin pull the Car ids and Track ids lookup files for Forza Motorsport 2023 from the up to date Forzurda's github repositories")]
    [PluginAuthor("DaZD")]
    [PluginName("Forza Motorsport lookup files updater")]
    public class PluginFM8LookupFilesUpdater: SimHub.Plugins.IPlugin
    {
        const string carNamesGitFileURL = "https://raw.githubusercontent.com/Forzurda/ForzaMotorsport2023CarIDs/main/FM8.CarNames.csv";
        const string trackNamesGitFileURL = "https://raw.githubusercontent.com/Forzurda/ForzaMotorsport2023TrackIDs/main/FM2023TrackIDs.csv";

        const string carNamesFile = "FM8.CarNames.csv";
        const string trackNamesFile = "FM8.TrackNames.csv";

        /// <summary>
        /// Instance of the current plugin manager
        /// </summary>
        public PluginManager PluginManager { get; set; }
      

        /// <summary>
        /// Called at plugin manager stop, close/dispose anything needed here !
        /// Plugins are rebuilt at game change
        /// </summary>
        /// <param name="pluginManager"></param>
        public void End(PluginManager pluginManager)
        {
        }

        /// <summary>
        /// Returns the settings control, return null if no settings control is required
        /// </summary>
        /// <param name="pluginManager"></param>
        /// <returns></returns>
        public System.Windows.Controls.Control GetWPFSettingsControl(PluginManager pluginManager)
        {
            return null;
        }

        /// <summary>
        /// Called once after plugins startup
        /// Plugins are rebuilt at game change
        /// </summary>
        /// <param name="pluginManager"></param>
        public void Init(PluginManager pluginManager)
        {
            SimHub.Logging.Current.Info("Updating FM8 lookup files...");
            DownloadUpdate(carNamesGitFileURL, carNamesFile);
            DownloadUpdate(trackNamesGitFileURL, trackNamesFile);
            SimHub.Logging.Current.Info("FM8 lookup files updated.");
        }

        /// <summary>
        /// Download the latest version of the  specified lookup file from Forzurda's git repository.
        /// </summary>
        /// <param name="gitURL"></param>
        /// <param name="fileName"></param>
        public void DownloadUpdate(string gitURL, string fileName)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(gitURL, "Update");
            string filePath = ".\\LookupTables\\" + fileName ;
            if (System.IO.File.Exists(filePath)) {
                System.IO.File.Delete(filePath);
            }
            System.IO.File.Move("Update", filePath);
        }
    }
}