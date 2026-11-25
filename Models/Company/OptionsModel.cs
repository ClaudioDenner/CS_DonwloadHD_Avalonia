using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using SharedClientSide.Helpers;
using System;
using System.IO;

namespace DownloadHDAvalonia.Models.Company
{
    public partial class OptionsModel : ObservableObject
    {
        [ObservableProperty]
        private string _defaultPathToDownloadProfessionalTaskFiles;

        [ObservableProperty]
        private string _appTheme;

        public void Save()
        {
            AppSettingsLoader.MergeAndWriteSettings(this);
        }

        public static OptionsModel Load()
        {
            return AppSettingsLoader.LoadWithRepair(
                CreateDefault,
                RepairDefaults,
                model => model.Save());
        }

        private static bool RepairDefaults(OptionsModel model)
        {
            bool repaired = false;

            if (string.IsNullOrWhiteSpace(model.AppTheme))
            {
                model.AppTheme = AppSettingsLoader.DefaultAppTheme;
                repaired = true;
            }

            return repaired;
        }

        private static OptionsModel CreateDefault()
        {
            var om = new OptionsModel
            {
                AppTheme = AppSettingsLoader.DefaultAppTheme
            };
            om.Save();
            return om;
        }

        public OptionsModel()
        {
        }
    }
}
