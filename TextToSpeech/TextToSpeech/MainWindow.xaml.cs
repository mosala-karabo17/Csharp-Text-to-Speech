using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;
using Microsoft.Win32;
using System.IO;

namespace TextToSpeech
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpeechSynthesizer voice = new SpeechSynthesizer();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnSpeak_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rbMale.IsChecked == true)
                {
                    voice.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult ) ;
                    
                }
                else
                {
                    voice.SelectVoiceByHints(VoiceGender.Female);
                }

                voice.SpeakAsync(txtBoxText.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            voice.Pause();
        }

        private void btnResume_Click(object sender, RoutedEventArgs e)
        {
            voice.Resume();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog sdlg = new SaveFileDialog();
                sdlg.Filter = "wav file (*.wav) | *.wav";
                sdlg.Title = "Save to a wav file";
                sdlg.FilterIndex = 1;

                Nullable<bool> result = sdlg.ShowDialog();
                if (result == true)
                {
                    FileStream fs = new FileStream(sdlg.FileName, FileMode.Create, FileAccess.Write);
                    voice.SetOutputToWaveStream(fs);
                    voice.Speak(txtBoxText.Text);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
    }
}
