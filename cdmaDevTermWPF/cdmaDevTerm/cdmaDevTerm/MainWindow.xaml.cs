//Copyright 2012 Dillon Graham
//GPL v3 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using cdmaDevLib;

namespace cdmaDevTerm
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Elysium.Theme.Controls.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region theme
                dynamic accent;
                switch (Properties.Settings.Default.Accent.ToLower())
                {
                    case "lime":
                        accent = Elysium.Theme.AccentColors.Lime;
                        break;
                    case "blue":
                        accent = Elysium.Theme.AccentColors.Blue;
                        break;
                    case "brown":
                        accent = Elysium.Theme.AccentColors.Brown;
                        break;
                    case "green":
                        accent = Elysium.Theme.AccentColors.Green;
                        break;
                    case "magenta":
                        accent = Elysium.Theme.AccentColors.Magenta;
                        break;
                    case "orange":
                        accent = Elysium.Theme.AccentColors.Orange;
                        break;
                    case "pink":
                        accent = Elysium.Theme.AccentColors.Pink;
                        break;
                    case "purple":
                        accent = Elysium.Theme.AccentColors.Purple;
                        break;
                    case "red":
                        accent = Elysium.Theme.AccentColors.Red;
                        break;
                    case "viridian":
                        accent = Elysium.Theme.AccentColors.Viridian;
                        break;
                    default:
                        accent = Elysium.Theme.AccentColors.Lime;
                        break;
                }

                switch (Properties.Settings.Default.ThemeType)
                {
                    case Elysium.Theme.ThemeType.Dark:
                        Elysium.Theme.ThemeManager.Instance.Dark(accent);
                        break;
                    case Elysium.Theme.ThemeType.Light:
                        Elysium.Theme.ThemeManager.Instance.Light(accent);
                        break;
                    default:
                        Elysium.Theme.ThemeManager.Instance.Dark(accent);
                        break;
                }
            #endregion 

            this.DataContext = cdmaTerm.thePhone;
            cdmaTerm.initSixteenDigitCodes(AppDomain.CurrentDomain.BaseDirectory + "16digitpass.txt");
            cdmaTerm.GetComs();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            Boolean result = true;
            cdmaTerm.connectSub(cdmaTerm.thePhone.ComPortName);

            if (Properties.Settings.Default.AutoModeOffline)
            {
                cdmaTerm.dispatchQ.clearCommandQ();
                var v = new Command(cdmaTerm.modeOfflineD, "Offline");
                cdmaTerm.dispatchQ.add(ref v);
                result = cdmaTerm.dispatchQ.executeCommandQ();
            }
            if (result)
            {
                cdmaTerm.AddAllEvdo();
                cdmaTerm.AddNv(NvItems.NVItems.NV_MEID_I);
                cdmaTerm.AddQc(Qcdm.Cmd.DIAG_ESN_F);
                cdmaTerm.AddNv(NvItems.NVItems.NV_DIR_NUMBER_I);
                cdmaTerm.AddNv(NvItems.NVItems.NV_SEC_CODE_I);
                cdmaTerm.AddNv(NvItems.NVItems.NV_LOCK_CODE_I);
                cdmaTerm.AddNv(NvItems.NVItems.NV_HOME_SID_NID_I);
                cdmaTerm.AddNv(NvItems.NVItems.NV_NAM_LOCK_I);
                result = result && cdmaTerm.dispatchQ.executeCommandQ();
                if(result)
                cdmaTerm.ReadMIN1();
            }
        }

        private void tabControl1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (Wiki.IsSelected)
            {
                wikiWebBrowser.Navigate(new Uri("http://code.google.com/p/cdmaworkshoptool/wiki/cdmaWorkshopTool?tm=6"));
            }
        }

        private void Scan_Click(object sender, RoutedEventArgs e)
        {
            cdmaTerm.GetComs();
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            cdmaTerm.disconnectPort();
        }

        private void readSpc_Click(object sender, RoutedEventArgs e)
        {
            cdmaTerm.readSpcFromPhone(cdmaTerm.thePhone.SpcReadType);
            cdmaTerm.dispatchQ.executeCommandQ(); 
        }

        #region keyPress
            private void key1_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.One);
            }

            private void key2_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.Two);
            }

            private void key3_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.Three);
            }

            private void key4_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.Four);
            }

            private void key5_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.Five);
            }

            private void key6_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.Six);
            }

            private void key7_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.Seven);
            }

            private void key8_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.Eight);
            }

            private void key9_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.Nine);
            }

            private void keyStar_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.Star);
            }

            private void key0_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.Zero);
            }

            private void keyPound_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.Pound);
            }
            private void keySend_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.SendKey);
            }

            private void keyEnd_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.KeyPress(cdmaTerm.phoneKeys.EndKey);
            }
        #endregion

            private void sendSpc_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.dispatchQ.clearCommandQ();
                cdmaTerm.sendAnySPC(cdmaTerm.thePhone.Spc);
                cdmaTerm.dispatchQ.executeCommandQ();
            }

            private void SendSP_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.SendA16digitCode(cdmaTerm.thePhone.SixteenDigitSP);
            }

            private void writeSpc_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.dispatchQ.clearCommandQ();
                cdmaTerm.writeAnySpc(cdmaTerm.thePhone.Spc);
                cdmaTerm.dispatchQ.executeCommandQ();
            }

            private void SendTerm_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.SendTerminalCommand(cdmaTerm.thePhone.TermCommand);
            }

            private void writeEvdo_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.dispatchQ.clearCommandQ();
                cdmaTerm.sendAllEVDO(cdmaTerm.thePhone.Username, cdmaTerm.thePhone.Password);
                cdmaTerm.dispatchQ.executeCommandQ();
            }

            private void sendModeSwitch_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.dispatchQ.clearCommandQ();
                cdmaTerm.modeSwitch(cdmaTerm.thePhone.ModeChangeType);
                cdmaTerm.dispatchQ.executeCommandQ();
            }

            private void sendPrl_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.dispatchQ.clearCommandQ();
                cdmaTerm.SendPrlFile(cdmaTerm.thePhone.PrlFilename);
                cdmaTerm.dispatchQ.executeCommandQ();
            }

            private void ChoosePrl_Click(object sender, RoutedEventArgs e)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
                {
                    Title = "Select PRL",
                    DefaultExt = ".prl",
                    Filter = "PRL-file (.prl)|*.prl",
                    CheckFileExists = true
                };
                if((bool)dlg.ShowDialog())
                cdmaTerm.thePhone.PrlFilename = dlg.FileName;
            }

            private void writeNam_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.dispatchQ.clearCommandQ();
                cdmaTerm.updatePhoneFromViewModel();
            }

            private void readPrl_Click(object sender, RoutedEventArgs e)
            {
                var p = new cdmaDevLib.Prl();
                p.DownloadPrl("prl.prl");
                cdmaTerm.dispatchQ.executeCommandQ();
            }

            private void readPhone_Click(object sender, RoutedEventArgs e)
            {
                Boolean result = true;
                cdmaTerm.AddAllEvdo();
                cdmaTerm.AddNv(NvItems.NVItems.NV_MEID_I);
                cdmaTerm.AddQc(Qcdm.Cmd.DIAG_ESN_F);
                cdmaTerm.AddNv(NvItems.NVItems.NV_DIR_NUMBER_I);
                cdmaTerm.AddNv(NvItems.NVItems.NV_SEC_CODE_I);
                cdmaTerm.AddNv(NvItems.NVItems.NV_LOCK_CODE_I);
                cdmaTerm.AddNv(NvItems.NVItems.NV_HOME_SID_NID_I);
                cdmaTerm.AddNv(NvItems.NVItems.NV_NAM_LOCK_I);
                result = result && cdmaTerm.dispatchQ.executeCommandQ();
                if (result)
                    cdmaTerm.ReadMIN1();
            }

    }
}
