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
    class CheckEmptyPortfolioItem : DialogueItem
    {

        private bool lastOrderSuccessful = true;

        public CheckEmptyPortfolioItem() : base()
        {
        }

        public List<MemoryItem> CheckPortfolioNotEmpty(out string targetDialogueItemName )
        {
            targetDialogueItemName = "";  
            List<MemoryItem> memoryItemList = null;

            bool setLimitIsSuccessful = false;
            string setLimitIsSuccessfulString = "failure";
            
            /*
             * Maybe should've followed the event pipeline structure below instead of searching up the portfolio process
             * but this is much simpler and short of time.
             */

            //Get the portfolio brain process
            BrainProcess portfolioProcess = ownerAgent.BrainProcessList.Find(x => x.Name == "Portfolio");

           
            //success if portfolio not empty, else failure. A portfolio can only be modified if non-empty
            setLimitIsSuccessful = ((PortfolioProcess)portfolioProcess).StockList.Count > 0;
            setLimitIsSuccessfulString = setLimitIsSuccessful ? "success" : "failure";
            

            foreach (DialogueAction action in actionList)
            {
               
                    Boolean matching = action.CheckMatch(setLimitIsSuccessfulString);
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
