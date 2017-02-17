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
using InternetDataAcquisitionLibrary;


namespace ListenerApplication
{
    public partial class InternetDataAcquisitionMainForm : Form
    {
        private const string CLIENT_NAME = "InternetDataAcquisition";
        private const string DEFAULT_IP_ADDRESS = "127.0.0.1";
        private const int DEFAULT_PORT = 7;
        private const string DATETIME_FORMAT = "yyyyMMdd HH:mm:ss";
        private const int DEFAULT_SAMPLING_FREQUENCY = 16000;
        private const int DEFAULT_BITS_PER_SAMPLE = 16;
        private const int DEFAULT_NUMBER_OF_CHANNELS = 1;
        private const string SEARCH_URL = "http://www.google.com/finance/info?client=ig&q=STO:";
        private const string PORTFOLIO_URL = "http://www.google.com/finance/info?client=ig&q=";

        private string ipAddress = null;
        private int port = -1;
        private Client client = null;

        private HTMLDownloader searchDownloader = null;
        private HTMLDownloader portfolioDownloader = null;

     //   private WAVRecorder wavRecorder;
     //   private Thread visualizationThread = null;
     //   private Thread recognitionThread = null;
     //   private Boolean running = false;

        //private IsolatedWordRecognizer recognizer = null;

        //private List<Tuple<string, DateTime, DateTime>> recognizedWordList = null;

        public InternetDataAcquisitionMainForm()
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

            //init HTML downloaders
            searchDownloader = new HTMLDownloader();
            searchDownloader.RunRepeatedly = false;
            portfolioDownloader = new HTMLDownloader();
            portfolioDownloader.RunRepeatedly = true;
            portfolioDownloader.DownloadInterval = 20000000;
            

            //init event handlers for fetching data
            searchDownloader.NewDataAvailable += new EventHandler(HandleSearchResponseReceived);
            searchDownloader.Error += new EventHandler<ErrorEventArgs>(HandleErrorSearchResponse);
            portfolioDownloader.NewDataAvailable += new EventHandler(HandlePortfolioResponseReceived);

        }

        private void Connect()
        {
            client = new Client();
            client.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleClientProgress);
            client.Received += new EventHandler<DataPacketEventArgs>(HandleClientReceived);
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

        private void HandleClientReceived(object sender, DataPacketEventArgs e)
        {
            if (InvokeRequired) { BeginInvoke(new MethodInvoker(() => FetchData(e.DataPacket))); }
            else { FetchData(e.DataPacket); }
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
                Console.WriteLine("KEYDOWN");

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

        private void FetchData(DataPacket dataPacket)
        {
            string data = dataPacket.Message;
            

            string idaItemType = data.Split('*')[0];
            string idaItem = data.Split('*')[1];
            /*
            //   speechSynthesizer.SpeakAsync(sentence);
            ColorListBoxItem item = new ColorListBoxItem(dataPacket.TimeStamp.ToString("yyyyMMdd HH:mm:ss") + ": " + SEARCH_URL + idaItem, inputMessageColorListBox.BackColor,
                inputMessageColorListBox.ForeColor);
            inputMessageColorListBox.Items.Insert(0, item);
            */
            if (idaItemType.Equals("requestSearch")) // Filter on search requests
            {
                ColorListBoxItem item = new ColorListBoxItem("search query request: " + idaItem, inputMessageColorListBox.BackColor,
                inputMessageColorListBox.ForeColor);
                inputMessageColorListBox.Items.Insert(0, item);

                searchDownloader.Start(SEARCH_URL + idaItem);
               
            }
            else if (idaItemType.Equals("requestPortfolio")) // Filter on portfolio requests
            {
                ColorListBoxItem item = new ColorListBoxItem("portfolio query request: " + idaItem, inputMessageColorListBox.BackColor,
                inputMessageColorListBox.ForeColor);
                inputMessageColorListBox.Items.Insert(0, item);

                portfolioDownloader.DownloadInterval = 20000000;
                portfolioDownloader.Start(PORTFOLIO_URL + idaItem.Replace(';',':'));
            }



            /*Thread.Sleep(5000);
            string responseMessage = searchDownloader.GetLastString();

            //   speechSynthesizer.SpeakAsync(sentence);
            item = new ColorListBoxItem(responseMessage, inputMessageColorListBox.BackColor,
                inputMessageColorListBox.ForeColor);
            inputMessageColorListBox.Items.Insert(0, item);

            client.Send("searchResponse" + responseMessage);*/
        }

        /*
         * Function triggered when a packet containing information about ticker searched for has been downloaded
         */
        private void HandleSearchResponseReceived(object sender, EventArgs e)
        {
            //remove the first 3 characters ('// ') and remove newlines
            //The ':' char is reserved by the agent library as a separator...
            string responseMessage = searchDownloader.GetLastString().Substring(3).Trim().Replace(':',';').Replace('\n',' ');
            
            client.Send("responseSearch*" + responseMessage);


            //   speechSynthesizer.SpeakAsync(sentence);
            ColorListBoxItem item = new ColorListBoxItem("query result:" + responseMessage, inputMessageColorListBox.BackColor,
                inputMessageColorListBox.ForeColor);
            inputMessageColorListBox.Items.Insert(0, item);
        }

        /*
         * Function triggered when a packet containing information about the tickers 
         * of the entire portfolio has been downloaded
         */
        private void HandlePortfolioResponseReceived(object sender, EventArgs e)
        {
            //remove the first 3 characters ('// ') and remove newlines
            //The ':' char is reserved by the agent library as a separator...
            string responseMessage = portfolioDownloader.GetLastString().Substring(3).Trim().Replace(':', ';').Replace('\n', ' ');

            client.Send("responsePortfolio*" + responseMessage);


            //   speechSynthesizer.SpeakAsync(sentence);
            ColorListBoxItem item = new ColorListBoxItem("portfolio query result:" + responseMessage, inputMessageColorListBox.BackColor,
                inputMessageColorListBox.ForeColor);
            inputMessageColorListBox.Items.Insert(0, item);
        }

        private void HandleErrorSearchResponse<ErrorEventArgs>(object sender, ErrorEventArgs e)
        {
            //   speechSynthesizer.SpeakAsync(sentence);
            ColorListBoxItem item = new ColorListBoxItem("ERROR: ", inputMessageColorListBox.BackColor,
                inputMessageColorListBox.ForeColor);
            inputMessageColorListBox.Items.Insert(0, item);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
