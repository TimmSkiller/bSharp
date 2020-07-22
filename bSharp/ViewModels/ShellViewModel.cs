using bSharpUILib;
using bSharpUILib.Models;
using bSharp.Properties;
using Caliburn.Micro;
using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using bSharp.ViewModels;
using System.IO;

namespace bSharpUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        IWindowManager windowManager = new WindowManager();

        public ShellViewModel()
        {
            if (!File.Exists($"{Environment.CurrentDirectory}\\db.csv"))
            {
                MessageBox.Show($"Database file not found in directory {Environment.CurrentDirectory}");
                Environment.Exit(1);
            }

            GameList = new BindableCollection<GameModel>(Tools.ReadGames($"{Environment.CurrentDirectory}\\db.csv"));
            IsDownloading = false;
            DownloadProgressBarLabel = "Progress: N/A";
        }

        public string beeShopImage { get; set; }


        private bool _isDownloading;

        private bool IsDownloading
        {
            get
            {
                return _isDownloading;
            }
            set
            {
                _isDownloading = value;
                NotifyOfPropertyChange(() => IsDownloading);
                NotifyOfPropertyChange(() => CanDisplaySettings);
                NotifyOfPropertyChange(() => CanInstallApp);
                NotifyOfPropertyChange(() => CanDownloadFile);
            }
        }

        public BindableCollection<GameModel> _gameList = new BindableCollection<GameModel>();

        public BindableCollection<GameModel> GameList
        {
            get { return _gameList; }
            set
            {
                _gameList = value;
                NotifyOfPropertyChange(() => _gameList);
            }
        }

        public GameModel _selectedGameList;

        public GameModel SelectedGameList
        {
            get
            {
                return _selectedGameList;
            }
            set
            {
                _selectedGameList = value;
                NotifyOfPropertyChange(() => SelectedGameList);
            }
        }

        private int _downloadProgress;

        public int DownloadProgress
        {
            get { return _downloadProgress; }
            set
            {
                _downloadProgress = value;
                NotifyOfPropertyChange(() => DownloadProgress);
            }
        }

        private string _downloadProgressBarLabel;

        public string DownloadProgressBarLabel
        {
            get { return _downloadProgressBarLabel; }
            set 
            { 
                _downloadProgressBarLabel = value;
                NotifyOfPropertyChange(() => DownloadProgressBarLabel);
            }
        }



        public void ShowInfo()
        {
            if (SelectedGameList == null)
            {
                MessageBox.Show("No Game was selected.");
            }
            else
            {
                MessageBox.Show($"URL: {SelectedGameList.GameUrl}");
            }
        }

        

        public bool CanDownloadFile
        {
            get
            {
                if (IsDownloading == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void DisplaySettings()
        {
            windowManager.ShowDialogAsync(new SettingsViewModel(), null, null);
        }

        public bool CanDisplaySettings
        {
            get
            {
                if (IsDownloading == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        

        public void InstallApp()
        {

        }

        public bool CanInstallApp
        {
            get
            {
                if (IsDownloading == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task DownloadFileAsync()
        {
            if (SelectedGameList == null)
            {
                MessageBox.Show("No Game was selected.");
                return;
            }

            IsDownloading = true;

            Downloader d = new Downloader(SelectedGameList.GameName, SelectedGameList.GameUrl);

            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.Stream = Resources.CTR_LOADING;
            soundPlayer.PlayLooping();

            await d.DownloadFileAsync(new Progress<int>(x => 
            { 
                DownloadProgress = x;
                DownloadProgressBarLabel = $"Progress: {x}%";
            }
            ));
            soundPlayer.Stop();

            DownloadProgress = 0;
            DownloadProgressBarLabel = "Progress: N/A";

            soundPlayer.Stream = Resources.CTR_LOADING_FINISHED;
            soundPlayer.Play();
            MessageBox.Show($"{SelectedGameList.GameName}.cia was downloaded successfully.", "beeShop");

            IsDownloading = false;
        }
    }
}