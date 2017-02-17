using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AudioLibrary;
using AudioLibrary.Visualization;
using CommunicationLibrary;
using CustomUserControlsLibrary;
using ObjectSerializerLibrary;
using SpeechRecognitionLibrary;

namespace ListenerApplication
{
    public partial class ListenerMainForm : Form
    {
        private const string CLIENT_NAME = "Listener";
        private const string DEFAULT_IP_ADDRESS = "127.0.0.1";
        private const int DEFAULT_PORT = 7;
        private const string DATETIME_FORMAT = "yyyyMMdd HH:mm:ss";
        private const int DEFAULT_SAMPLING_FREQUENCY = 16000;
        private const int DEFAULT_BITS_PER_SAMPLE = 16;
        private const int DEFAULT_NUMBER_OF_CHANNELS = 1;

        private string ipAddress = null;
        private int port = -1;
        private Client client = null;

     //   private WAVRecorder wavRecorder;
     //   private Thread visualizationThread = null;
     //   private Thread recognitionThread = null;
     //   private Boolean running = false;

        //private IsolatedWordRecognizer recognizer = null;

        //private List<Tuple<string, DateTime, DateTime>> recognizedWordList = null;

        public ListenerMainForm()
        {
            InitializeComponent();
            Initialize();
            Connect();
        }

        private void Initialize()
        {
            
            ipAddress = DEFAULT_IP_ADDRESS;
            port = DEFAULT_PORT;
            // Default location for the window
            Size screenSize = Screen.GetBounds(this).Size;
            this.Location = new Point(screenSize.Width - this.Width, screenSize.Height - this.Height);
            mainTabControl.SelectedTab = inputTabPage;
        }

        private void Connect()
        {
            client = new Client();
            client.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleClientProgress);
            //  client.Disconnected += new EventHandler(HandleClientDisconnected);
            client.Name = CLIENT_NAME;
            client.Connect(ipAddress, port);
        }

        private void HandleClientProgress(object sender, CommunicationProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => ShowProgress(e)));
            }
            else { ShowProgress(e); }
        }

        private void ShowProgress(CommunicationProgressEventArgs e)
        {
            ColorListBoxItem item;
            item = new ColorListBoxItem(e.Message, communicationLogColorListBox.BackColor, communicationLogColorListBox.ForeColor);
            communicationLogColorListBox.Items.Insert(0, item);
        }

        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter)
            {
                string message = inputTextBox.Text;
                inputTextBox.Text = "";
                e.Handled = true;
                e.SuppressKeyPress = true;
                client.Send(message);
                ColorListBoxItem item = new ColorListBoxItem(DateTime.Now.ToString(DATETIME_FORMAT) + ": " + message, inputMessageColorListBox.BackColor,
                    inputMessageColorListBox.ForeColor);
                inputMessageColorListBox.Items.Insert(0, item);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
