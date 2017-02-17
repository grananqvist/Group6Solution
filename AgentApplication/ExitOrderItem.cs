using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentLibrary;
using AgentLibrary.Memories;
using AgentLibrary.BrainProcesses.DialogueItems;
using AgentLibrary.BrainProcesses.DialogueActions;
using AgentLibrary.BrainProcesses;

namespace AgentApplication
{
    class ExitOrderItem : DialogueItem
    {
        
        private bool lastOrderSuccessful = true;

        public ExitOrderItem() : base()
        {
        }

        public List<MemoryItem> TryExitPosition(string inputString, out string targetDialogueItemName )
        {
            targetDialogueItemName = "";  
            List<MemoryItem> memoryItemList = null;

            bool exitPositionSuccessful = false;
            bool validInput = true;
            int id = int.Parse(inputString);
            string exitPositionSuccessfulString = "failure";
            
            /*
             * Maybe should've followed the event pipeline structure below instead of searching up the portfolio process
             * but this is much simpler and short of time.
             */

            //Get the portfolio brain process
            BrainProcess portfolioProcess = ownerAgent.BrainProcessList.Find(x => x.Name == "Portfolio");

            //Only try to exit position if a valid input 
            if (validInput)
            {
                //Let the portfolio brain process try to exit the position
                lastOrderSuccessful = exitPositionSuccessful = ((PortfolioProcess)portfolioProcess).TryExitPosition(id);
                exitPositionSuccessfulString = exitPositionSuccessful ? "success" : "failure";
            }



            foreach (DialogueAction action in actionList)
            {

                Boolean matching = action.CheckMatch(exitPositionSuccessfulString);
                if (matching)
                {
                    memoryItemList = action.GetMemoryItems();
                    targetDialogueItemName = action.TargetDialogueItemName;
                }


            }
            return memoryItemList;

        }
        
        public bool LastOrderSuccessful
        {
            get { return lastOrderSuccessful; }
        }
    }
}
