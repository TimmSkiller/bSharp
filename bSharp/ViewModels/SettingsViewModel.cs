using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace bSharp.ViewModels
{
    public class SettingsViewModel : Screen
    {
        public SettingsViewModel()
        {

        }

        public void SelectDatabase()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Compatible Database Files (.csv)|*.csv";
            ofd.InitialDirectory = $"{Environment.GetEnvironmentVariable("userprofile")}\\Desktop";
            ofd.ShowDialog();

            if (File.Exists(ofd.FileName))
            {
                MessageBox.Show(ofd.FileName);
            }
        }

        public void Cancel()
        {
            TryCloseAsync();
        }

        public void Save()
        {

        }
    }
}
