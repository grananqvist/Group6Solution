using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentLibrary.BrainProcesses.DialogueActions;
using AgentLibrary.Memories;
using AgentLibrary.BrainProcesses;

namespace AgentApplication
{
    public class MarketOrderAction : DialogueAction
    {
        

        public override List<MemoryItem> GetMemoryItems()
        {
            List<MemoryItem> memoryItemList = new List<MemoryItem>();
            
            //get latest input (MemoryItem) from ListenerProcess, which should be the quantity given
            MemoryItem latestListenerItem = ownerAgent.WorkingMemory.GetLastItemByTag(MemoryItemTags.ListenerProcess);
            string quantityString = latestListenerItem.Content.ToLower();

            int quantity = int.Parse(quantityString);

            //Get the portfolio brain process to check if sufficient funds available
            BrainProcess portfolioProcess = ownerAgent.BrainProcessList.Find(x => x.Name == "Portfolio");

            if (((PortfolioProcess)portfolioProcess).IsTradeSuccessful(quantity,1))
            {

            }
            
            /*
            MemoryItem outputItem = new MemoryItem();
            outputItem.CreationDateTime = DateTime.Now;
            outputItem.Tag = MemoryItemTags.InternetDataAcquisitionProcess;
            outputItem.Content = "requestSearch*" + tickerToQuery;
            memoryItemList.Add(outputItem);*/

            memoryItemList.Reverse(); // Doesn't matter really, but looks more elegant in the display:
                                      // Displayed as action first, then deactivation, then activation (on top).
            return memoryItemList;
        }
    }
}
