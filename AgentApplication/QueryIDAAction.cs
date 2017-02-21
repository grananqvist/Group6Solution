using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentLibrary.BrainProcesses.DialogueActions;
using AgentLibrary.Memories;
using AgentLibrary.BrainProcesses;

namespace AgentApplication
{
    public class QueryIDAAction : DialogueAction
    {
        

        public override List<MemoryItem> GetMemoryItems()
        {
            List<MemoryItem> memoryItemList = new List<MemoryItem>();
            
            //get latest input (MemoryItem) from ListenerProcess, which should be the stock ticker given
            MemoryItem latestListenerItem = ownerAgent.WorkingMemory.GetLastItemByTag(MemoryItemTags.ListenerProcess);
            string tickerToQuery = latestListenerItem.Content.ToLower();
            //Convert to ticker name of whole name of stock was given
            tickerToQuery = DialogueInputStrings.convertNameToTicker(tickerToQuery);

            MemoryItem outputItem = new MemoryItem();
            outputItem.CreationDateTime = DateTime.Now;
            outputItem.Tag = MemoryItemTags.InternetDataAcquisitionProcess;
            outputItem.Content = "requestSearch*" + tickerToQuery;
            memoryItemList.Add(outputItem);

            memoryItemList.Reverse(); // Doesn't matter really, but looks more elegant in the display:
                                      // Displayed as action first, then deactivation, then activation (on top).
            return memoryItemList;
        }
    }
}
