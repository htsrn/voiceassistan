using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace voiceassistan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SpeechRecognitionEngine rengine = new SpeechRecognitionEngine();
        SpeechSynthesizer synth =new SpeechSynthesizer();

        private void m1_Click(object sender, EventArgs e)
        {
            m1.Visible = false;
            rengine.RecognizeAsync();
        }

        private void m2_Click(object sender, EventArgs e)
        {
            m1.Visible = true;
        }

        void repeat()
        {
            string[] commands = { "Hi", "How are you", "Open visual studio", "Open chrome","Show commands" };
            Choices options = new Choices(commands);
            Grammar grammar = new Grammar(new GrammarBuilder(options));
            rengine.LoadGrammar(grammar);
            rengine.SetInputToDefaultAudioDevice();
            rengine.SpeechRecognized += recognize;
            foreach( var x in commands )
            {
                listBox1.Items.Add( x );
            }
        }

        private void recognize(object sender, SpeechRecognizedEventArgs e)
        {
            m1.Visible = true;
            if (e.Result.Text == "Hi")
            {
                synth.SpeakAsync("Hello what's up");
            }
            if (e.Result.Text== "Open visual studio")
            {
                synth.SpeakAsync("Okey I open visual studio");
                System.Diagnostics.Process.Start("\"C:\\Program Files\\Microsoft Visual Studio\\2022\\Community\\Common7\\IDE\\devenv.exe\"");
            }
            if (e.Result.Text == "Open chrome")
            {
                synth.SpeakAsync("Okey I open chrome");
                System.Diagnostics.Process.Start("\"C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe\"");
            }
            if (e.Result.Text == "Show commands")
            {
                timer1.Start();
                synth.SpeakAsync("Okey");
                listBox1.Visible=true;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            repeat();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m1.Visible = false;
            m2.Visible = false;
            label1.Visible = true;
        }

        private void speakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m1.Visible = true;
            m2.Visible = true;
            label1.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            listBox1.Visible= false;
            synth.SpeakAsync("Closed");
            timer1.Stop();
        }
    }
}
