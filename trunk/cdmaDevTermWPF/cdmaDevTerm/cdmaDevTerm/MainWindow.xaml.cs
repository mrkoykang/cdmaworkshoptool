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
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using IronPython.Hosting;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using System.Xml;
using System.ComponentModel;
using System.IO;
using ICSharpCode.AvalonEdit.CodeCompletion;
using System.Reflection;

namespace cdmaDevTerm
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Elysium.Theme.Controls.Window
    {
        private ScriptEngine _engine;
        private ScriptScope _scope;

        private string _scriptImports = @"#imports
import clr
from cdmaDevLib import *
from System import Array,Byte,String
";

        private string cdmaDevTermSampleScript = @"#
#cdmaDevTerm sample ironPython script
#copyright 2012 DG, chromableedstudios.com
#
#warning: 
# the cdmaDevLib api is in unstable development 
# and names may change from time to time
#
#cdmaTerm.Connect(""COM9"")
cdmaTerm.Connect(phone.ComPortName)
cdmaTerm.ReadAllNam()
cdmaTerm.ReadNv(NvItems.NvItems.NV_DS_MIP_ACTIVE_PROF_I)
cdmaTerm.ReadNv(906)
#cdmaTerm.WriteNv(NvItems.NvItems.NV_SEC_CODE_I, Array[Byte]((0x30, 0x30, 0x30, 0x30, 0x30, 0x30)))
#cdmaTerm.WriteNv(906,""PPPassword"")
cdmaTerm.SendSpc(""000000"")
cdmaTerm.ReadNvList(""900-915"",""nvOut.txt"")
q.Run()";


        public MainWindow()
        {
            InitializeComponent();

            _engine = Python.CreateEngine();
            _scope = _engine.CreateScope();

            var runtime = _engine.Runtime;
            runtime.LoadAssembly(typeof(String).Assembly);
            runtime.LoadAssembly(typeof(Array).Assembly);
            runtime.LoadAssembly(typeof(cdmaDevLib.cdmaTerm).Assembly);
            _scope.SetVariable("phone", cdmaTerm.thePhone);
            _scope.SetVariable("q", cdmaTerm.Q);

            RunScript(_scriptImports);
            CodeTextEditor.SyntaxHighlighting =
            HighlightingLoader.Load(new XmlTextReader("ICSharpCode.PythonBinding.Resources.Python.xshd"),
            HighlightingManager.Instance);
            CodeTextEditor.Text = cdmaDevTermSampleScript;

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
            cdmaTerm.thePhone.PrlFilename = Properties.Settings.Default.LastPrl;
            cdmaTerm.GetComs();
            comBox.SelectedIndex = 0;

            //code completion handlers
            CodeTextEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            CodeTextEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;

        }
      
        ~MainWindow()
        {
            Properties.Settings.Default.LastPrl = cdmaTerm.thePhone.PrlFilename;
            Properties.Settings.Default.Save();
        }

        private void runScript_Click(object sender, RoutedEventArgs e)
        {
            
            var code = CodeTextEditor.Text;
            RunScript(code);
        }
        private void RunScript(string code)
        {
            try
            {
                var source = _engine.CreateScriptSourceFromString(code, SourceCodeKind.Statements);
                source.Execute(_scope);

            }
            catch (Exception ex)
            {
                var eo = _engine.GetService<ExceptionOperations>();
                var error = eo.FormatException(ex);

                MessageBox.Show(error, "There was an Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            Boolean result = true;
            cdmaTerm.Connect(cdmaTerm.thePhone.ComPortName);

            if (Properties.Settings.Default.AutoModeOffline)
            {
                cdmaTerm.Q.Clear();
                cdmaTerm.ModeSwitch(Qcdm.Mode.MODE_RADIO_OFFLINE);
                result = cdmaTerm.Q.Run();
            }
            if (result)
            {
                cdmaTerm.AddAllEvdo();
                cdmaTerm.AddNv(NvItems.NvItems.NV_MEID_I);
                cdmaTerm.AddQc(Qcdm.Cmd.DIAG_ESN_F);
                cdmaTerm.AddNv(NvItems.NvItems.NV_DIR_NUMBER_I);
                cdmaTerm.AddNv(NvItems.NvItems.NV_SEC_CODE_I);
                cdmaTerm.AddNv(NvItems.NvItems.NV_LOCK_CODE_I);
                cdmaTerm.AddNv(NvItems.NvItems.NV_HOME_SID_NID_I);
                cdmaTerm.AddNv(NvItems.NvItems.NV_NAM_LOCK_I);
                cdmaTerm.AddNv(NvItems.NvItems.NV_DS_QCMIP_I);
                result = result && cdmaTerm.Q.Run();
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
            cdmaTerm.Disconnect();
        }

        private void readSpc_Click(object sender, RoutedEventArgs e)
        {
            cdmaTerm.readSpcFromPhone(cdmaTerm.thePhone.SpcReadType);
            cdmaTerm.Q.Run(); 
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
                cdmaTerm.Q.Clear();
                cdmaTerm.SendSpc(cdmaTerm.thePhone.Spc);
                cdmaTerm.Q.Run();
            }

            private void SendSP_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.SendA16digitCode(cdmaTerm.thePhone.SixteenDigitSP);
            }

            private void writeSpc_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.Q.Clear();
                cdmaTerm.WriteSpc(cdmaTerm.thePhone.Spc);
                cdmaTerm.Q.Run();
            }

            private void SendTerm_Click(object sender, RoutedEventArgs e)
            {
                bool appendCrcEof = TermAppendCrc.IsChecked ?? false;
                cdmaTerm.SendTerminalCommand(cdmaTerm.thePhone.TermCommand, appendCrcEof);
            }

            private void writeEvdo_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.Q.Clear();
                cdmaTerm.WriteEvdo(cdmaTerm.thePhone.Username, cdmaTerm.thePhone.Password);
                cdmaTerm.Q.Run();
            }

            private void sendModeSwitch_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.Q.Clear();
                cdmaTerm.ModeSwitch(cdmaTerm.thePhone.ModeChangeType);
                cdmaTerm.Q.Run();
            }

            private void sendPrl_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.Q.Clear();
                cdmaTerm.SendPrlFile(cdmaTerm.thePhone.PrlFilename);
                cdmaTerm.Q.Run();
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
                cdmaTerm.Q.Clear();
                cdmaTerm.updatePhoneFromViewModel();
            }

            private void readPrl_Click(object sender, RoutedEventArgs e)
            {
                var p = new cdmaDevLib.Prl();
                p.DownloadPrl("prl.prl");
                cdmaTerm.Q.Run();
            }

            private void readPhone_Click(object sender, RoutedEventArgs e)
            {
                Boolean result = true;
                cdmaTerm.AddAllEvdo();
                cdmaTerm.AddNv(NvItems.NvItems.NV_MEID_I);
                cdmaTerm.AddQc(Qcdm.Cmd.DIAG_ESN_F);
                cdmaTerm.AddNv(NvItems.NvItems.NV_DIR_NUMBER_I);
                cdmaTerm.AddNv(NvItems.NvItems.NV_SEC_CODE_I);
                cdmaTerm.AddNv(NvItems.NvItems.NV_LOCK_CODE_I);
                cdmaTerm.AddNv(NvItems.NvItems.NV_HOME_SID_NID_I);
                cdmaTerm.AddNv(NvItems.NvItems.NV_NAM_LOCK_I);
                cdmaTerm.AddNv(NvItems.NvItems.NV_DS_QCMIP_I);

                result = result && cdmaTerm.Q.Run();
                if (result)
                    cdmaTerm.ReadMIN1();
            }

            private void SendSpcZerosMenuItem_Click_1(object sender, RoutedEventArgs e)
            {
                cdmaTerm.SendSpc("000000");
                cdmaTerm.Q.Run();
            }

            private void WriteSpcZerosMenuItem_Click_1(object sender, RoutedEventArgs e)
            {
                cdmaTerm.WriteSpc("000000");
                cdmaTerm.Q.Run();
            }

            private void ReadNvItem_Click(object sender, RoutedEventArgs e)
            {  
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
                {
                    Title = "Save NV Read as...",
                    DefaultExt = ".txt",
                    Filter = "Text (.txt)|*.txt",
                };
                if ((bool)dlg.ShowDialog())
                {
                    DoNVRead(dlg.FileName, ReadNvItemTextbox.Text);
                   // cdmaTerm.ReadNvList(ReadNvItemTextbox.Text, dlg.FileName);
                }
            }
            
            private void DoNVRead(string fileName,string nvList)
            {
                BackgroundWorker bw = new BackgroundWorker();

                bw.DoWork += (sender, args) => {
                    // do your lengthy stuff here -- this will happen in a separate thread
                    cdmaTerm.ReadNvList(nvList, fileName);
                };

                bw.RunWorkerCompleted += (sender, args) => {
                if (args.Error != null)  // if an exception occurred during DoWork,
                    MessageBox.Show(args.Error.ToString());  // do your error handling here

                // do any UI stuff after the long operation here
                Logger.Add("NV Read - long operation done");
                };

                bw.RunWorkerAsync(); // start the background worker
    
            }


            private void copyEsnConverted_Click_1(object sender, RoutedEventArgs e)
            {
                if (cdmaTerm.thePhone.Esn == null)
                    return;
                Clipboard.SetText(cdmaDevLib.esnConverter.ConversionSub(cdmaTerm.thePhone.Esn));
            }

            private void copyMeidConverted_Click_1(object sender, RoutedEventArgs e)
            {
                if (cdmaTerm.thePhone.Meid == null)
                    return;
                Clipboard.SetText(cdmaDevLib.esnConverter.ConversionSub(cdmaTerm.thePhone.Meid));
            }

            private void sendMotoUnlock_Click(object sender, RoutedEventArgs e)
            {
                cdmaTerm.UnlockMotoEvdo();
            }

            private void RelockMoto_Click_1(object sender, RoutedEventArgs e)
            {
                cdmaTerm.RelockMotoEvdo();
            }

            //http://studentguru.gr/b/solidus/archive/2010/07/30/wpf-how-to-drag-amp-drop-a-file-in-your-window.aspx -->
            private void FileShowTextBox_PreviewDragEnter(object sender, DragEventArgs e)
            {
                bool isCorrect = true;

                if (e.Data.GetDataPresent(DataFormats.FileDrop, true) == true)
                {
                    string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);
                    foreach (string filename in filenames)
                    {
                        if (File.Exists(filename) == false)
                        {
                            isCorrect = false;
                            break;
                        }
                        FileInfo info = new FileInfo(filename);
                        if (info.Extension != ".txt" && info.Extension != ".py")
                        {
                            isCorrect = false;
                            break;
                        }
                    }
                }
                if (isCorrect == true)
                    e.Effects = DragDropEffects.All;
                else
                    e.Effects = DragDropEffects.None;
                e.Handled = true;
            }

            private void FileShowTextBox_PreviewDrop(object sender, DragEventArgs e)
            {
                string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);
                if (filenames != null)
                {
                    foreach (string filename in filenames)
                        CodeTextEditor.Text = File.ReadAllText(filename);
                    e.Handled = true;
                }
            }
            //<-- http://studentguru.gr/b/solidus/archive/2010/07/30/wpf-how-to-drag-amp-drop-a-file-in-your-window.aspx 

            private void ChooseScript_Click(object sender, RoutedEventArgs e)
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
                {
                    Title = "Select ironPython script",
                    DefaultExt = ".py",
                    Filter = "PRL-file (.py)|*.py|All files (*.*)|*.*",
                    CheckFileExists = true
                };
                if ((bool)dlg.ShowDialog())
                    CodeTextEditor.Text = File.ReadAllText(dlg.FileName);
            }
            private void SaveScript_Click(object sender, RoutedEventArgs e)
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
                {
                    Title = "Save ironPython script",
                    DefaultExt = ".py",
                    Filter = "PRL-file (.py)|*.py|All files (*.*)|*.*",
                };
                if ((bool)dlg.ShowDialog())
                {
                    StreamWriter sw2 = new StreamWriter(dlg.FileName,
                        false, System.Text.Encoding.UTF8);
                    sw2.Write(CodeTextEditor.Text);
                    sw2.Close();
                }
            }

            //http://www.codeproject.com/Articles/42490/Using-AvalonEdit-WPF-Text-Editor -->
            CompletionWindow completionWindow;

            void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
            {
                if (e.Text == ".")
                {
                    // Open code completion after the user has pressed dot:
                    completionWindow = new CompletionWindow(CodeTextEditor.TextArea);

                    IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
                    var offset = CodeTextEditor.CaretOffset-9;
                    if (offset > 0)
                    {
                        var s = CodeTextEditor.Text.Substring(offset, 8);
                        if (s == "cdmaTerm")
                        {

                            //http://www.java2s.com/Tutorial/CSharp/0400__Reflection/ListMethods.htm
                            cdmaTerm f = new cdmaTerm();
                            Type t = f.GetType();
                            MethodInfo[] mi = t.GetMethods(BindingFlags.Static | BindingFlags.Public);
                            foreach (MethodInfo m in mi)
                            {
                                data.Add(new CompletionData(m.Name, m.GetSignature()));

                            }
                            completionWindow.BorderThickness = new System.Windows.Thickness { Left = 0, Top = 0, Right = 0, Bottom = 0 };
                            completionWindow.Show();
                        }
                    }
                    
                    completionWindow.Closed += delegate
                    {
                        completionWindow = null;
                    };
                }
            }

            void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
            {
                if (e.Text.Length > 0 && completionWindow != null)
                {
                    if (!char.IsLetterOrDigit(e.Text[0]))
                    {
                        // Whenever a non-letter is typed while the completion window is open,
                        // insert the currently selected element.
                        completionWindow.CompletionList.RequestInsertion(e);
                    }
                }
                // Do not set e.Handled=true.
                // We still want to insert the character that was typed.
            }
        //<-- http://www.codeproject.com/Articles/42490/Using-AvalonEdit-WPF-Text-Editor 
    }
}
