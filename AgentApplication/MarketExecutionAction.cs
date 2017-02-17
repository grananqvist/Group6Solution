using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentLibrary.BrainProcesses.DialogueActions;
using AgentLibrary.Memories;
using AgentLibrary.BrainProcesses;

namespace AgentApplication
{
    public class MarketExecutionAction : DialogueAction
    {

        private string stock = "";
        private int quantity = 0;
        private double fillPrice = 0;

        public override List<MemoryItem> GetMemoryItems()
        {
            List<MemoryItem> memoryItemList = new List<MemoryItem>();

            string direction = quantity < 0 ? "short" : "long";
            string output = "You are now " + direction + " " + Math.Abs(quantity).ToString() + " stocks of " + stock + " at " + fillPrice;

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
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public double FillPrice
        {
            get { return fillPrice; }
            set { fillPrice = value; }
        }
    }
}
