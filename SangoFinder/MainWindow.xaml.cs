using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using SangoFinder.Models;

namespace SangoFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileName;
        public MainWindow()
        {
            InitializeComponent();


            //var t1 = Encoding.GetEncoding("BIG5").GetString(new byte[] { 0xa4, 0x55, 0xcd, 0xd6 });//下邳
            //var t2 = Encoding.GetEncoding("BIG5").GetString(new byte[] { 0xa4, 0xd5, 0xa9, 0xa6 });


            //var reader = IniReader.GetCollecions(@"Settings\City.ini");

            //var ss = reader[0].GetKey(1);
            //var aq = reader[1].Get("Name");

            //dgGeneral.ItemsSource = ParseSaveData.Parse("SG001.sav").Generals;

        }

        private void LoadClicked(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "存档文件 (*.sav)|*.sav"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                fileName = openFileDialog.FileName;
                var info = GetGeneralInfo(fileName);
                MessageBox.Show(string.Format("共加载{0}名武将", info.Count()));
                dgGeneral.ItemsSource = info;
            }
        }

        private void RefreshClicked(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var info = GetGeneralInfo(fileName);
                MessageBox.Show(string.Format("共加载{0}名武将", info.Count()));
                dgGeneral.ItemsSource = info;
            }
        }

        private IEnumerable<General> GetGeneralInfo(string fileName)
        {
            return ParseSaveData.Parse(fileName).Generals.OrderByDescending(s=>s.武力).ThenByDescending(s=>s.智力);
        }

    }
}
