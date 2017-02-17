using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using AgentLibrary.Memories;
using AgentLibrary.BrainProcesses.DialogueItems;
using AgentLibrary.BrainProcesses;

namespace AgentApplication
{
    /*
     * New derived dialogue process with support for MarketOrderItems
     */
    [DataContract]
    public class MyDialogueProcess : DialogueProcess
    {
        private DateTime timeOfLastInput;
        private bool timeOfLastInputUpToDate;
       
        public MyDialogueProcess() : base()
        {
            timeOfLastInputUpToDate = false;
            timeOfLastInput = DateTime.Now;
        }
        
        public override void Activate()
        {
            base.Activate();

            //If current item is CheckEmptyPortfolioItem, call the event to handle
            //because the item requires no input
            if (ItemList[ItemIndex] is CheckEmptyPortfolioItem)
            {
                HandleWorkingMemoryChanged(ownerAgent.WorkingMemory, EventArgs.Empty);
            }
        }


        //triggered every time the working memory is updated
        protected override void HandleWorkingMemoryChanged(object sender, EventArgs e)
        {
            base.HandleWorkingMemoryChanged(sender, e);

            

            if ((ItemIndex >= 0) && (ItemIndex < ItemList.Count))
            {
                string targetItemName;
                string input = "";
                DialogueItem item = ItemList[ItemIndex];

                if (item is MarketOrderItem)
                {
                    if (!timeOfLastInputUpToDate)
                    {
                        //Ugly trick used to ignore most recent item from Listener (which is "buy"/"sell")
                        //Because timeOfLastInput in superclass is private
                        List<MemoryItem> tempList = ownerAgent.WorkingMemory.GetItemsByTag(timeOfLastInput, MemoryItemTags.ListenerProcess);
                        if (tempList.Count > 0)
                        {
                            timeOfLastInput = tempList[0].CreationDateTime;
                            timeOfLastInputUpToDate = true;
                        }
                    }
                    MarketOrderItem marketOrderItem = (MarketOrderItem)item;

                    //get latest inputs (MemoryItems) from ListenerProcess
                    List<MemoryItem> inputItemList = ownerAgent.WorkingMemory.GetItemsByTag(timeOfLastInput, MemoryItemTags.ListenerProcess);

                    if (inputItemList.Count > 0)
                    {
                        //timeOfLastInput will not record InteractionItems, so there will most likely be multiple items in inputItemList
                        //Get content of the latest memoryItem only. 
                        MemoryItem inputMemoryItem = inputItemList[0];
                        timeOfLastInput = inputMemoryItem.CreationDateTime;
                        input = inputMemoryItem.Content.ToLower();  // Make the response case-insensitive

                        List<MemoryItem> memoryItemList = marketOrderItem.TryExecuteTrade(input, out targetItemName);


                        if (memoryItemList != null)
                        {
                            ItemIndex = ItemList.FindIndex(i => i.Name == targetItemName);

                            //Ugly trick continued. set timeOfLastInputUpToDate to false again when the trade is successful,
                            //because the next item will not be a MarketOrderItem, and therefore handled by MyDialogueProcess' superclass
                            if (marketOrderItem.LastOrderSuccessful)
                            {
                                timeOfLastInputUpToDate = false;
                            }

                            ownerAgent.WorkingMemory.InsertItems(memoryItemList);

                        }
                    }
                }






                if (item is SLTPOrderItem)
                {
                    if (!timeOfLastInputUpToDate)
                    {
                        //Ugly trick used to ignore most recent item from Listener 
                        //Because timeOfLastInput in superclass is private
                        List<MemoryItem> tempList = ownerAgent.WorkingMemory.GetItemsByTag(timeOfLastInput, MemoryItemTags.ListenerProcess);
                        if (tempList.Count > 0)
                        {
                            timeOfLastInput = tempList[0].CreationDateTime;
                            timeOfLastInputUpToDate = true;
                        }
                    }
                    SLTPOrderItem sltpOrderItem = (SLTPOrderItem)item;

                    //get latest inputs (MemoryItems) from ListenerProcess
                    List<MemoryItem> inputItemList = ownerAgent.WorkingMemory.GetItemsByTag(timeOfLastInput, MemoryItemTags.ListenerProcess);

                    if (inputItemList.Count > 0)
                    {
                        //timeOfLastInput will not record InteractionItems, so there will most likely be multiple items in inputItemList
                        //Get content of the latest memoryItem only. 
                        MemoryItem inputMemoryItem = inputItemList[0];
                        timeOfLastInput = inputMemoryItem.CreationDateTime;
                        input = inputMemoryItem.Content.ToLower();  // Make the response case-insensitive

                        List<MemoryItem> memoryItemList = sltpOrderItem.TrySetLimit(input, out targetItemName);


                        if (memoryItemList != null)
                        {
                            ItemIndex = ItemList.FindIndex(i => i.Name == targetItemName);

                            //Ugly trick continued. set timeOfLastInputUpToDate to false again when the limit is set successful,
                            //because the next item will not be a SLTPOrderItem, and therefore handled by MyDialogueProcess' superclass
                            if (sltpOrderItem.LastOrderSuccessful)
                            {
                                timeOfLastInputUpToDate = false;
                            }

                            ownerAgent.WorkingMemory.InsertItems(memoryItemList);

                        }
                    }

                }



                if (item is CheckEmptyPortfolioItem)
                {

                    CheckEmptyPortfolioItem checkEmptyPortfolioItem = (CheckEmptyPortfolioItem)item;

                    List<MemoryItem> memoryItemList = checkEmptyPortfolioItem.CheckPortfolioNotEmpty(out targetItemName);


                    if (memoryItemList != null)
                    {
                        ItemIndex = ItemList.FindIndex(i => i.Name == targetItemName);
                        ownerAgent.WorkingMemory.InsertItems(memoryItemList);

                    }
                }




                if (item is ExitOrderItem)
                {
                    if (!timeOfLastInputUpToDate)
                    {
                        //Ugly trick used to ignore most recent item from Listener 
                        //Because timeOfLastInput in superclass is private
                        List<MemoryItem> tempList = ownerAgent.WorkingMemory.GetItemsByTag(timeOfLastInput, MemoryItemTags.ListenerProcess);
                        if (tempList.Count > 0)
                        {
                            timeOfLastInput = tempList[0].CreationDateTime;
                            timeOfLastInputUpToDate = true;
                        }
                    }
                    ExitOrderItem exitOrderItem = (ExitOrderItem)item;

                    //get latest inputs (MemoryItems) from ListenerProcess
                    List<MemoryItem> inputItemList = ownerAgent.WorkingMemory.GetItemsByTag(timeOfLastInput, MemoryItemTags.ListenerProcess);

                    if (inputItemList.Count > 0)
                    {
                        //timeOfLastInput will not record InteractionItems, so there will most likely be multiple items in inputItemList
                        //Get content of the latest memoryItem only. 
                        MemoryItem inputMemoryItem = inputItemList[0];
                        timeOfLastInput = inputMemoryItem.CreationDateTime;
                        input = inputMemoryItem.Content.ToLower();  // Make the response case-insensitive

                        List<MemoryItem> memoryItemList = exitOrderItem.TryExitPosition(input, out targetItemName);


                        if (memoryItemList != null)
                        {
                            ItemIndex = ItemList.FindIndex(i => i.Name == targetItemName);

                            //Ugly trick continued. set timeOfLastInputUpToDate to false again when the position exited successful,
                            //because the next item will not be a ExitOrderItem, and therefore handled by MyDialogueProcess' superclass
                            if (exitOrderItem.LastOrderSuccessful)
                            {
                                timeOfLastInputUpToDate = false;
                            }

                            ownerAgent.WorkingMemory.InsertItems(memoryItemList);

                        }
                    }

                }

            }
        }


   

    }
}
