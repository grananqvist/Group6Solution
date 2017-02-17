using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommunicationLibrary;
using CustomUserControlsLibrary;

namespace SpeechApplication
{
    public partial class SpeechMainForm : Form
    {
        private const string CLIENT_NAME = "Speech";
        private const string DEFAULT_IP_ADDRESS = "127.0.0.1";
        private const int DEFAULT_PORT = 7;

        private string ipAddress = null;
        private int port = -1;
        private Client client = null;

        public SpeechMainForm()
        {
            InitializeComponent();
            Initialize();
            Connect();
            Start();
        }

        private void Initialize()
        {
            ipAddress = DEFAULT_IP_ADDRESS;
            port = DEFAULT_PORT;
            // Default location for the window
            Size screenSize = Screen.GetBounds(this).Size;
            this.Location = new Point(screenSize.Width - this.Width, screenSize.Height - this.Height);
            mainTabControl.SelectedTab = speechTabPage;
        }

        private void Connect()
        {
            client = new Client();
            client.Received += new EventHandler<DataPacketEventArgs>(HandleClientReceived);
            client.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleClientProgress);
          //  client.Disconnected += new EventHandler(HandleClientDisconnected);
            client.Name = CLIENT_NAME;
            client.Connect(ipAddress, port);
        }

        private void Start()
        {
        }

        // To do (student task): Add actual speech synthesis!
        private void Speak(DataPacket dataPacket)
        {
            string sentence = dataPacket.Message;
         //   speechSynthesizer.SpeakAsync(sentence);
            ColorListBoxItem item = new ColorListBoxItem(dataPacket.TimeStamp.ToString("yyyyMMdd HH:mm:ss") + ": " + dataPacket.Message, speechColorListBox.BackColor,
                speechColorListBox.ForeColor);
            speechColorListBox.Items.Insert(0, item);
        }

        private void HandleClientReceived(object sender, DataPacketEventArgs e)
        {
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => Speak(e.DataPacket))); }
            else { Speak(e.DataPacket); }
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
            item = new ColorListBoxItem(e.Message, communicationLogListBox.BackColor, communicationLogListBox.ForeColor);
            communicationLogListBox.Items.Insert(0, item);
        }
    }
}
