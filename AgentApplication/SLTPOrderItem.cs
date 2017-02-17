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
    class SLTPOrderItem : DialogueItem
    {
        public const int SET_STOPP_LOSS = -1;
        public const int SET_TARGET_PROFIT = 1;

        private int limitOrderType = 1;
        private int direction = 1;
        private bool lastOrderSuccessful = true;

        public SLTPOrderItem(int limitOrderType, int direction) : base()
        {
            this.limitOrderType = Math.Sign(limitOrderType);
            this.direction = Math.Sign(direction);
        }

        public List<MemoryItem> TrySetLimit(string inputString, out string targetDialogueItemName )
        {
            targetDialogueItemName = "";  
            List<MemoryItem> memoryItemList = null;

            bool setLimitIsSuccessful = false;
            bool validInput = true;
            double limit = double.Parse(inputString);
            string setLimitIsSuccessfulString = "failure";
            
            /*
             * Maybe should've followed the event pipeline structure below instead of searching up the portfolio process
             * but this is much simpler and short of time.
             */

            //Get the portfolio brain process
            BrainProcess portfolioProcess = ownerAgent.BrainProcessList.Find(x => x.Name == "Portfolio");

            //Only try to execute trade if a valid input 
            if (validInput)
            {
                //Let the portfolio brain process try to execute the trade
                lastOrderSuccessful = setLimitIsSuccessful = ((PortfolioProcess)portfolioProcess).IsSetLimitSuccessful(limit, limitOrderType, direction);
                setLimitIsSuccessfulString = setLimitIsSuccessful ? "success" : "failure";
            }

            

            foreach (DialogueAction action in actionList)
            {
                if (action is LimitExecutionAction)
                {
                    LimitExecutionAction limitExecutionAction = (LimitExecutionAction)action;
                    //Get the Dialogue action representing trade execution success/failure
                    Boolean matching = limitExecutionAction.CheckMatch(setLimitIsSuccessfulString);
                    if (matching)
                    {
                        limitExecutionAction.Stock = ((PortfolioProcess)portfolioProcess).StockInFocus;
                        limitExecutionAction.LimitLevel = limit;
                        limitExecutionAction.LimitType = limitOrderType;
                        memoryItemList = limitExecutionAction.GetMemoryItems();
                        targetDialogueItemName = limitExecutionAction.TargetDialogueItemName;
                    }
                }
                else //Most likely ResponseAction
                {
                    Boolean matching = action.CheckMatch(setLimitIsSuccessfulString);
                    if (matching)
                    {
                        memoryItemList = action.GetMemoryItems();
                        targetDialogueItemName = action.TargetDialogueItemName;
                    }
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
