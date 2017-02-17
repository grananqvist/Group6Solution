using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CommunicationLibrary;
using AgentLibrary.BrainProcesses;
using AgentLibrary.Memories;
using ObjectSerializerLibrary;
using AgentLibrary;

namespace AgentApplication
{
    class StockMarketAgent : Agent
    {
        private DateTime timeOfLastIDASearchOutput;
        private DateTime timeOfLastIDAPortfolioOutput;

        public StockMarketAgent() : base() {

            WorkingMemory.MemoryChanged += new EventHandler(HandleWorkingMemoryChanged2);

        }

        /*
         * Method executed when event in MemoryChanged
         * Checks if there is any message needed to be sent to the InternetDataAcquisitionApplication
         */
        private void HandleWorkingMemoryChanged2(object sender, EventArgs e) {


            MemoryItem idaItem = WorkingMemory.GetLastItemByTag(MemoryItemTags.InternetDataAcquisitionProcess);
            if (idaItem != null)
            {

                //Only go further if idaItem is a response (both IDA requests and responses share the same MemoryItemTag)
                string idaItemType = idaItem.Content.Split('*')[0];

                //Keep timeOfLast... search requests and portfolio requests separate
                if (idaItemType.Equals("requestSearch")) // Filter on search requests
                {
                    if (idaItem.CreationDateTime > timeOfLastIDASearchOutput)  // To avoid incorrectly repeating a previous output
                    {
                        
                        //get the IDA process ID
                        string clientID = Server.GetFirstClientID(ProcessTagValues.InternetDataAcquisitionProcess);
                        if (clientID != null)
                        {
                            //Send the content of idaItem to IDAProcess
                            Server.Send(clientID, idaItem.Content);
                        }
                        timeOfLastIDASearchOutput = idaItem.CreationDateTime;
                    }
                }

                if (idaItemType.Equals("requestPortfolio")) // Filter on search requests
                {
                    if (idaItem.CreationDateTime > timeOfLastIDAPortfolioOutput)  // To avoid incorrectly repeating a previous output
                    {

                        //get the IDA process ID
                        string clientID = Server.GetFirstClientID(ProcessTagValues.InternetDataAcquisitionProcess);
                        if (clientID != null)
                        {
                            //Send the content of idaItem to IDAProcess
                            Server.Send(clientID, idaItem.Content);
                        }
                        timeOfLastIDAPortfolioOutput = idaItem.CreationDateTime;
                    }
                }



            }
        }
    }
}
