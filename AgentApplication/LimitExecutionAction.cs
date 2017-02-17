using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentLibrary.BrainProcesses.DialogueActions;
using AgentLibrary.Memories;
using AgentLibrary.BrainProcesses;

namespace AgentApplication
{
    public class LimitExecutionAction : DialogueAction
    {

        private string stock = "";
        private int limitType = 1;
        private double limitLevel = 0;

        public override List<MemoryItem> GetMemoryItems()
        {
            List<MemoryItem> memoryItemList = new List<MemoryItem>();

            string limitTypeString = limitType < 0 ? "stop loss" : "target profit";
            string output = "You have set a " + limitTypeString + " at " + limitLevel.ToString() + " on your position of " + stock;

            MemoryItem outputItem = new MemoryItem();
            outputItem.CreationDateTime = DateTime.Now;
            outputItem.Tag = MemoryItemTags.SpeechProcess;
            outputItem.Content = output;
            memoryItemList.Add(outputItem);

            memoryItemList.Reverse(); // Doesn't matter really, but looks more elegant in the display:
                                      // Displayed as action first, then deactivation, then activation (on top).
            return memoryItemList;
        }

        public string Stock
        {
            get { return stock; }
            set { stock = value; }
        }
        public int LimitType
        {
            get { return limitType; }
            set { limitType = value; }
        }
        public double LimitLevel
        {
            get { return limitLevel; }
            set { limitLevel = value; }
        }
    }
}
