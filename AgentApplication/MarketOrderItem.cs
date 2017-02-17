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
    class MarketOrderItem : DialogueItem
    {
        public const int DIRECTION_LONG = 1;
        public const int DIRECTION_SHORT = -1;

        private int direction = 1;
        private bool lastOrderSuccessful = true;

        public MarketOrderItem(int direction) : base()
        {
            this.direction = Math.Sign(direction);
        }

        public List<MemoryItem> TryExecuteTrade(string inputString, out string targetDialogueItemName )
        {
            targetDialogueItemName = "";  
            List<MemoryItem> memoryItemList = null;

            bool tradeIsSuccessful = false;
            bool validInput = true;
            int size = int.Parse(inputString);
            string tradeIsSuccessfulString = "failure";

            validInput = size > 0;

            /*
             * Maybe should've followed the event pipeline structure below instead of searching up the portfolio process
             * but this is much simpler and short of time.
             */

            //Get the portfolio brain process
            BrainProcess portfolioProcess = ownerAgent.BrainProcessList.Find(x => x.Name == "Portfolio");

            //Only try to execute trade if a valid input size
            if (validInput)
            {
                //Let the portfolio brain process try to execute the trade
                lastOrderSuccessful = tradeIsSuccessful = ((PortfolioProcess)portfolioProcess).IsTradeSuccessful(size, direction);
                tradeIsSuccessfulString = tradeIsSuccessful ? "success" : "failure";
            }

            

            foreach (DialogueAction action in actionList)
            {
                if (action is MarketExecutionAction)
                {
                    MarketExecutionAction marketExecutionAction = (MarketExecutionAction)action;
                    //Get the Dialogue action representing trade execution success/failure
                    Boolean matching = marketExecutionAction.CheckMatch(tradeIsSuccessfulString);
                    if (matching)
                    {
                        marketExecutionAction.Stock = ((PortfolioProcess)portfolioProcess).StockInFocus;
                        marketExecutionAction.Quantity = size * direction;
                        marketExecutionAction.FillPrice = ((PortfolioProcess)portfolioProcess).StockInFocusLastPrice;
                        memoryItemList = marketExecutionAction.GetMemoryItems();
                        targetDialogueItemName = marketExecutionAction.TargetDialogueItemName;
                    }
                }
                else //Most likely ResponseAction
                {
                    Boolean matching = action.CheckMatch(tradeIsSuccessfulString);
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
