using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AgentLibrary;
using AgentLibrary.BrainProcesses;
using AgentLibrary.BrainProcesses.DialogueActions;
using AgentLibrary.BrainProcesses.DialogueItems;
using AgentLibrary.Memories;
using AuxiliaryLibrary;
using CommunicationLibrary;
using CustomUserControlsLibrary;
using ObjectSerializerLibrary;
using System.Web.Script.Serialization;
using AgentApplication.MarketObjects;


namespace AgentApplication
{
    public partial class AgentMainForm : Form
    {
        #region Constants
        private const string DATETIME_FORMAT = "yyyyMMdd HH:mm:ss";
        #endregion

        #region Fields
        private Agent agent;
        private DateTime timeOfLastidaResponseSearchItem;
        private DateTime timeOfLastidaResponsePortfolioItem;
        private static object communicationLogLockObject = new object();
        //   private static object dialogueLockObject = new object();
        #endregion

        #region Constructors
        public AgentMainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Protected methods
        // To prevent flickering when resizing
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        #endregion

        #region Agent definitions
        private void GenerateAgent()
        {
            SetUpAgent();
            

            PortfolioProcess portfolioProcess = new PortfolioProcess(portfolioLayoutPanel, TotalEquityLabel, 100000);
            portfolioProcess.Name = "Portfolio";
            agent.BrainProcessList.Add(portfolioProcess);
            portfolioProcess.ActiveOnStartup = true;



            MyDialogueProcess mainDialogue = new MyDialogueProcess();
            mainDialogue.Name = "MainDialogue";
            agent.BrainProcessList.Add(mainDialogue);
            mainDialogue.ActiveOnStartup = true;
            startButton.Enabled = true;

            //Main Item 1
            InteractionItem mainDialogueItem1 = new InteractionItem();
            mainDialogueItem1.Name = "MainDialogueItem1";

            ResponseAction mainItem1ToItem2 = new ResponseAction(); //Search for stock path
            mainItem1ToItem2.InputList.Add("Search");
            mainItem1ToItem2.OutputList.Add("Which stock would you like to search for?");
            mainItem1ToItem2.TargetDialogueItemName = "MainDialogueItem2";
            mainDialogueItem1.ActionList.Add(mainItem1ToItem2);
            
            ResponseAction mainItem1ToPortfolioDialogue = new ResponseAction(); //Modify portfolio path
            mainItem1ToPortfolioDialogue.InputList.Add("Portfolio");
            mainItem1ToPortfolioDialogue.TargetDialogueItemName = "ModifyPortfolioDialogueItem1";
            mainItem1ToPortfolioDialogue.BrainProcessToDeactivate = "MainDialogue";
            mainItem1ToPortfolioDialogue.BrainProcessToActivate = "ModifyPortfolioDialogue";
            mainDialogueItem1.ActionList.Add(mainItem1ToPortfolioDialogue);
            mainDialogue.ItemList.Add(mainDialogueItem1);

            //TODO: change name readAction

            InteractionItem mainDialogueItem2 = new InteractionItem();
            mainDialogueItem2.Name = "MainDialogueItem2";
            QueryIDAAction readAction2 = new QueryIDAAction();
            readAction2.InputList.AddRange(DialogueInputStrings.getStockNameList());
            readAction2.InputList.AddRange(DialogueInputStrings.getStockTickerInputs());
            readAction2.TargetDialogueItemName = "MainDialogueItem3";
            mainDialogueItem2.ActionList.Add(readAction2);
            mainDialogue.ItemList.Add(mainDialogueItem2);

            //Main Item 3 : Buy/sell or search for another stock
            InteractionItem mainDialogueItem3 = new InteractionItem();
            mainDialogueItem3.Name = "MainDialogueItem3";

            ResponseAction mainItem3ToItem2 = new ResponseAction(); //search another stock path
            mainItem3ToItem2.InputList.Add("Search");
            mainItem3ToItem2.OutputList.Add("Which stock would you like to search for?");
            mainItem3ToItem2.TargetDialogueItemName = "MainDialogueItem2";
            mainDialogueItem3.ActionList.Add(mainItem3ToItem2);

            ResponseAction mainItem3ToGoLongDialogueAction = new ResponseAction(); //Buy stock path
            mainItem3ToGoLongDialogueAction.InputList.Add("Buy");
            mainItem3ToGoLongDialogueAction.BrainProcessToDeactivate = "MainDialogue";
            mainItem3ToGoLongDialogueAction.BrainProcessToActivate = "GoLongDialogue";
            mainDialogueItem3.ActionList.Add(mainItem3ToGoLongDialogueAction);

            ResponseAction mainItem3TogoShortDialogueAction = new ResponseAction(); //Skiing path
            mainItem3TogoShortDialogueAction.InputList.Add("Sell");
            mainItem3TogoShortDialogueAction.BrainProcessToDeactivate = "MainDialogue";
            mainItem3TogoShortDialogueAction.BrainProcessToActivate = "GoShortDialogue";
            mainDialogueItem3.ActionList.Add(mainItem3TogoShortDialogueAction);
            mainDialogue.ItemList.Add(mainDialogueItem3);

            /*
             * Main Dialogue Process end
             */

            /*
             * Modify Portfolio Dialogue Process start
             */

            MyDialogueProcess modifyPortfolioDialogue = new MyDialogueProcess();
            modifyPortfolioDialogue.Name = "ModifyPortfolioDialogue";
            agent.BrainProcessList.Add(modifyPortfolioDialogue);
            modifyPortfolioDialogue.ActiveOnStartup = false;

            //Modify Portfolio Item1 : 
            CheckEmptyPortfolioItem modifyPortfolioDialogueItem1 = new CheckEmptyPortfolioItem();
            modifyPortfolioDialogueItem1.Name = "ModifyPortfolioDialogueItem1";

            OutputAction modifyPortfolioItem1ToMainDialogue = new OutputAction(); //There is no portfolio path
            modifyPortfolioItem1ToMainDialogue.InputList.Add("failure");
            modifyPortfolioItem1ToMainDialogue.OutputList.Add("Sorry, but your portfolio is empty, you have nothing to modify");
            modifyPortfolioItem1ToMainDialogue.TargetDialogueItemName = "MainDialogueItem1";
            modifyPortfolioItem1ToMainDialogue.BrainProcessToDeactivate = "ModifyPortfolioDialogue";
            modifyPortfolioItem1ToMainDialogue.BrainProcessToActivate = "MainDialogue";
            modifyPortfolioDialogueItem1.ActionList.Add(modifyPortfolioItem1ToMainDialogue);

            OutputAction modifyPortfolioItem1ToItem2 = new OutputAction(); //modify portfolio path
            modifyPortfolioItem1ToItem2.InputList.Add("success");
            modifyPortfolioItem1ToItem2.TargetDialogueItemName = "ModifyPortfolioDialogueItem2";
            modifyPortfolioDialogueItem1.ActionList.Add(modifyPortfolioItem1ToItem2);
            modifyPortfolioDialogue.ItemList.Add(modifyPortfolioDialogueItem1);

            //Modify Portfolio Item2 : Which id to exit question
            InteractionItem modifyPortfolioDialogueItem2 = new InteractionItem();
            modifyPortfolioDialogueItem2.Name = "ModifyPortfolioDialogueItem2";
            modifyPortfolioDialogueItem2.MillisecondDelay = 500;
            OutputAction modifyPortfolioItem2ToItem3 = new OutputAction();
            modifyPortfolioItem2ToItem3.OutputList.Add("Enter the ID of the position you want to exit");
            modifyPortfolioItem2ToItem3.TargetDialogueItemName = "ModifyPortfolioDialogueItem3";
            modifyPortfolioDialogueItem2.ActionList.Add(modifyPortfolioItem2ToItem3);
            modifyPortfolioDialogue.ItemList.Add(modifyPortfolioDialogueItem2);

            //Modify Portfolio Item3 : which id to exit answer
            ExitOrderItem modifyPortfolioDialogueItem3 = new ExitOrderItem();
            modifyPortfolioDialogueItem3.Name = "ModifyPortfolioDialogueItem3";
            modifyPortfolioDialogueItem3.MillisecondDelay = 1000;

            ResponseAction modifyPortfolioItem3ToItem4 = new ResponseAction(); //Exited position path
            modifyPortfolioItem3ToItem4.InputList.Add("success");
            modifyPortfolioItem3ToItem4.OutputList.Add("Position exited successfully, do you wish to exit another position?");
            modifyPortfolioItem3ToItem4.TargetDialogueItemName = "ModifyPortfolioDialogueItem4";
            modifyPortfolioDialogueItem3.ActionList.Add(modifyPortfolioItem3ToItem4);

            ResponseAction modifyPortfolioItem3ToItem2 = new ResponseAction(); //No ID path
            modifyPortfolioItem3ToItem2.InputList.Add("failure");
            modifyPortfolioItem3ToItem2.OutputList.Add("The ID doesn't exist, try again.");
            modifyPortfolioItem3ToItem2.TargetDialogueItemName = "ModifyPortfolioDialogueItem2";
            modifyPortfolioDialogueItem3.ActionList.Add(modifyPortfolioItem3ToItem2);
            modifyPortfolioDialogue.ItemList.Add(modifyPortfolioDialogueItem3);

            //Modify Portfolio Item4 : Exit another one answer
            InteractionItem modifyPortfolioDialogueItem4 = new InteractionItem();
            modifyPortfolioDialogueItem4.Name = "ModifyPortfolioDialogueItem4";
            modifyPortfolioDialogueItem4.MillisecondDelay = 500;

            ResponseAction modifyPortfolioItem4ToMainDialogue = new ResponseAction(); //dont set a stop path
            modifyPortfolioItem4ToMainDialogue.InputList.Add("no"); //TODO Change to negative answer list
            modifyPortfolioItem4ToMainDialogue.OutputList.Add("Okay, have a nice day!");
            modifyPortfolioItem4ToMainDialogue.TargetDialogueItemName = "MainDialogueItem1";
            modifyPortfolioItem4ToMainDialogue.BrainProcessToDeactivate = "ModifyPortfolioDialogue";
            modifyPortfolioItem4ToMainDialogue.BrainProcessToActivate = "MainDialogue";
            modifyPortfolioDialogueItem4.ActionList.Add(modifyPortfolioItem4ToMainDialogue);

            ResponseAction modifyPortfolioItem4ToItem2 = new ResponseAction(); // set a stop path
            modifyPortfolioItem4ToItem2.InputList.Add("yes");
            modifyPortfolioItem4ToItem2.TargetDialogueItemName = "ModifyPortfolioDialogueItem1";
            modifyPortfolioDialogueItem4.ActionList.Add(modifyPortfolioItem4ToItem2);
            modifyPortfolioDialogue.ItemList.Add(modifyPortfolioDialogueItem4);

            /*
             * Modify Portfolio Dialogue Process end
             */

            /*
             * GoLong Dialogue Process start
             */

            MyDialogueProcess goLongDialogue = new MyDialogueProcess();
            goLongDialogue.Name = "GoLongDialogue";
            agent.BrainProcessList.Add(goLongDialogue);
            goLongDialogue.ActiveOnStartup = false;

            InteractionItem goLongDialogueItem1 = new InteractionItem();
            goLongDialogueItem1.Name = "GoLongDialogueItem1";
            goLongDialogueItem1.MillisecondDelay = 500;
            OutputAction goLongItem1ToItem2 = new OutputAction();
            goLongItem1ToItem2.OutputList.Add("How many stocks would you like to buy?");
            goLongItem1ToItem2.TargetDialogueItemName = "GoLongDialogueItem2";
            goLongDialogueItem1.ActionList.Add(goLongItem1ToItem2);
            goLongDialogue.ItemList.Add(goLongDialogueItem1);

            MarketOrderItem goLongDialogueItem2 = new MarketOrderItem(MarketOrderItem.DIRECTION_LONG);
            goLongDialogueItem2.Name = "GoLongDialogueItem2";
            goLongDialogueItem2.MillisecondDelay = 500;
            MarketExecutionAction goLongItem2ToItem3 = new MarketExecutionAction();
            goLongItem2ToItem3.InputList.Add("success");
            goLongItem2ToItem3.TargetDialogueItemName = "GoLongDialogueItem3";
            goLongDialogueItem2.ActionList.Add(goLongItem2ToItem3);
            ResponseAction goLongItem2ToItem4 = new ResponseAction();
            goLongItem2ToItem4.InputList.Add("failure");
            goLongItem2ToItem4.OutputList.Add("You have insufficient funds, try again with a smaller size"); //TODO: Change action name
            goLongItem2ToItem4.TargetDialogueItemName = "GoLongDialogueItem1";
            goLongDialogueItem2.ActionList.Add(goLongItem2ToItem4);
            goLongDialogue.ItemList.Add(goLongDialogueItem2);

            //Item3 : set SL or TP question
            InteractionItem goLongDialogueItem3 = new InteractionItem();
            goLongDialogueItem3.Name = "GoLongDialogueItem3";
            goLongDialogueItem3.MillisecondDelay = 2000;
            OutputAction goLongItem3ToItem5 = new OutputAction();
            goLongItem3ToItem5.OutputList.Add("Would you like to set a Stop Loss?");
            goLongItem3ToItem5.TargetDialogueItemName = "GoLongDialogueItem5";
            goLongDialogueItem3.ActionList.Add(goLongItem3ToItem5);
            goLongDialogue.ItemList.Add(goLongDialogueItem3);

            //Item5 : set SL or TP answer
            InteractionItem goLongDialogueItem5 = new InteractionItem();
            goLongDialogueItem5.Name = "GoLongDialogueItem5";
            goLongDialogueItem5.MillisecondDelay = 500;

            ResponseAction goLongItem5ToItem9 = new ResponseAction(); //dont set a stop path
            goLongItem5ToItem9.InputList.Add("no"); //TODO Change to negative answer list
            goLongItem5ToItem9.OutputList.Add("Okay, how about a target profit?");
            goLongItem5ToItem9.TargetDialogueItemName = "GoLongDialogueItem9";
            goLongDialogueItem5.ActionList.Add(goLongItem5ToItem9);

            ResponseAction goLongItem5ToItem6Action = new ResponseAction(); // set a stop path
            goLongItem5ToItem6Action.InputList.Add("yes");
            goLongItem5ToItem6Action.TargetDialogueItemName = "GoLongDialogueItem6";
            goLongDialogueItem5.ActionList.Add(goLongItem5ToItem6Action);
            goLongDialogue.ItemList.Add(goLongDialogueItem5);
            
            //Item6: where to set stop question
            InteractionItem goLongDialogueItem6 = new InteractionItem();
            goLongDialogueItem6.Name = "GoLongDialogueItem6";
            goLongDialogueItem6.MillisecondDelay = 500;
            OutputAction goLongItem6ToItem7 = new OutputAction();
            goLongItem6ToItem7.OutputList.Add("At what level would you like to set your stop loss?");
            goLongItem6ToItem7.TargetDialogueItemName = "GoLongDialogueItem7";
            goLongDialogueItem6.ActionList.Add(goLongItem6ToItem7);
            goLongDialogue.ItemList.Add(goLongDialogueItem6);

            //Item7 : where to set stop answer
            SLTPOrderItem goLongDialogueItem7 = new SLTPOrderItem(SLTPOrderItem.SET_STOPP_LOSS,MarketOrderItem.DIRECTION_LONG);
            goLongDialogueItem7.Name = "GoLongDialogueItem7";
            goLongDialogueItem7.MillisecondDelay = 500;
            LimitExecutionAction goLongItem7ToItem8 = new LimitExecutionAction();
            goLongItem7ToItem8.InputList.Add("success");
            goLongItem7ToItem8.TargetDialogueItemName = "GoLongDialogueItem8";
            goLongDialogueItem7.ActionList.Add(goLongItem7ToItem8);
            ResponseAction goLongItem7ToItem6 = new ResponseAction();
            goLongItem7ToItem6.InputList.Add("failure");
            goLongItem7ToItem6.OutputList.Add("Your limit was on the wrong side of the market, try again");
            goLongItem7ToItem6.TargetDialogueItemName = "GoLongDialogueItem6";
            goLongDialogueItem7.ActionList.Add(goLongItem7ToItem6);
            goLongDialogue.ItemList.Add(goLongDialogueItem7);

            //Item8 : set TP question
            InteractionItem goLongDialogueItem8 = new InteractionItem();
            goLongDialogueItem8.Name = "GoLongDialogueItem8";
            goLongDialogueItem8.MillisecondDelay = 1500;
            OutputAction goLongItem8ToItem9 = new OutputAction();
            goLongItem8ToItem9.OutputList.Add("Would you also like to set a Target Profit?");
            goLongItem8ToItem9.TargetDialogueItemName = "GoLongDialogueItem9";
            goLongDialogueItem8.ActionList.Add(goLongItem8ToItem9);
            goLongDialogue.ItemList.Add(goLongDialogueItem8);

            //Item9 : set TP answer
            InteractionItem goLongDialogueItem9 = new InteractionItem();
            goLongDialogueItem9.Name = "GoLongDialogueItem9";
            goLongDialogueItem9.MillisecondDelay = 500;

            ResponseAction goLongItem9ToMainDialogueAction = new ResponseAction(); //dont set a target path
            goLongItem9ToMainDialogueAction.InputList.Add("no"); //TODO Change to negative answer list
            goLongItem9ToMainDialogueAction.OutputList.Add("Okay, have a nice day");
            goLongItem9ToMainDialogueAction.TargetDialogueItemName = "MainDialogueItem1";
            goLongItem9ToMainDialogueAction.BrainProcessToDeactivate = "GoLongDialogue";
            goLongItem9ToMainDialogueAction.BrainProcessToActivate = "MainDialogue";
            goLongDialogueItem9.ActionList.Add(goLongItem9ToMainDialogueAction);

            ResponseAction goLongItem9ToItem10Action = new ResponseAction(); // set a target path
            goLongItem9ToItem10Action.InputList.Add("yes");
            goLongItem9ToItem10Action.TargetDialogueItemName = "GoLongDialogueItem10";
            goLongDialogueItem9.ActionList.Add(goLongItem9ToItem10Action);
            goLongDialogue.ItemList.Add(goLongDialogueItem9);

            //Item10: where to set target question
            InteractionItem goLongDialogueItem10 = new InteractionItem();
            goLongDialogueItem10.Name = "GoLongDialogueItem10";
            goLongDialogueItem10.MillisecondDelay = 500;
            OutputAction goLongItem10ToItem11 = new OutputAction();
            goLongItem10ToItem11.OutputList.Add("At what level would you like to set your target profit?");
            goLongItem10ToItem11.TargetDialogueItemName = "GoLongDialogueItem11";
            goLongDialogueItem10.ActionList.Add(goLongItem10ToItem11);
            goLongDialogue.ItemList.Add(goLongDialogueItem10);


            //Item11 : where to set target answer
            SLTPOrderItem goLongDialogueItem11 = new SLTPOrderItem(SLTPOrderItem.SET_TARGET_PROFIT, MarketOrderItem.DIRECTION_LONG);
            goLongDialogueItem11.Name = "GoLongDialogueItem11";
            goLongDialogueItem11.MillisecondDelay = 500;
            LimitExecutionAction goLongItem11ToItem12 = new LimitExecutionAction();
            goLongItem11ToItem12.InputList.Add("success");
            goLongItem11ToItem12.TargetDialogueItemName = "GoLongDialogueItem12";
            goLongDialogueItem11.ActionList.Add(goLongItem11ToItem12);
            ResponseAction goLongItem11ToItem10 = new ResponseAction();
            goLongItem11ToItem10.InputList.Add("failure");
            goLongItem11ToItem10.OutputList.Add("Your limit was on the wrong side of the market, try again");
            goLongItem11ToItem10.TargetDialogueItemName = "GoLongDialogueItem10";
            goLongDialogueItem11.ActionList.Add(goLongItem11ToItem10);
            goLongDialogue.ItemList.Add(goLongDialogueItem11);


            //Item12 : Everything done, return to main dialogue
            InteractionItem goLongDialogueItem12 = new InteractionItem();
            goLongDialogueItem12.Name = "GoLongDialogueItem12";
            goLongDialogueItem12.MillisecondDelay = 1500;
            OutputAction goLongItem12ToMainDialogue = new OutputAction();
            goLongItem12ToMainDialogue.OutputList.Add("All is set, good luck and have a nice day!");
            goLongItem12ToMainDialogue.TargetDialogueItemName = "MainDialogueItem1";
            goLongItem12ToMainDialogue.BrainProcessToDeactivate = "GoLongDialogue";
            goLongItem12ToMainDialogue.BrainProcessToActivate = "MainDialogue";
            goLongDialogueItem12.ActionList.Add(goLongItem12ToMainDialogue);
            goLongDialogue.ItemList.Add(goLongDialogueItem12);

            /*
             * GoLong Dialogue Process end
             */


            /*
             * goShort Dialogue Process start
             */

            MyDialogueProcess goShortDialogue = new MyDialogueProcess();
            goShortDialogue.Name = "GoShortDialogue";
            agent.BrainProcessList.Add(goShortDialogue);
            goShortDialogue.ActiveOnStartup = false;

            InteractionItem goShortDialogueItem1 = new InteractionItem();
            goShortDialogueItem1.Name = "GoShortDialogueItem1";
            goShortDialogueItem1.MillisecondDelay = 500;
            OutputAction goShortItem1ToItem2 = new OutputAction();
            goShortItem1ToItem2.OutputList.Add("How many stocks would you like to sell?");
            goShortItem1ToItem2.TargetDialogueItemName = "GoShortDialogueItem2";
            goShortDialogueItem1.ActionList.Add(goShortItem1ToItem2);
            goShortDialogue.ItemList.Add(goShortDialogueItem1);

            MarketOrderItem goShortDialogueItem2 = new MarketOrderItem(MarketOrderItem.DIRECTION_SHORT);
            goShortDialogueItem2.Name = "GoShortDialogueItem2";
            goShortDialogueItem2.MillisecondDelay = 500;
            MarketExecutionAction goShortItem2ToItem3 = new MarketExecutionAction();
            goShortItem2ToItem3.InputList.Add("success");
            goShortItem2ToItem3.TargetDialogueItemName = "GoShortDialogueItem3";
            goShortDialogueItem2.ActionList.Add(goShortItem2ToItem3);
            goShortDialogue.ItemList.Add(goShortDialogueItem2);

            InteractionItem goShortDialogueItem3 = new InteractionItem();
            goShortDialogueItem3.Name = "GoShortDialogueItem3";
            goShortDialogueItem3.MillisecondDelay = 2000;
            OutputAction goShortItem3ToItem5 = new OutputAction();
            goShortItem3ToItem5.OutputList.Add("Would you like to set a Stop Loss?");
            goShortItem3ToItem5.TargetDialogueItemName = "GoShortDialogueItem5";
            goShortDialogueItem3.ActionList.Add(goShortItem3ToItem5);
            goShortDialogue.ItemList.Add(goShortDialogueItem3);
            

            //Item5 : set SL or TP answer
            InteractionItem goShortDialogueItem5 = new InteractionItem();
            goShortDialogueItem5.Name = "GoShortDialogueItem5";
            goShortDialogueItem5.MillisecondDelay = 500;

            ResponseAction goShortItem5ToItem9 = new ResponseAction(); //dont set a stop path
            goShortItem5ToItem9.InputList.Add("no"); //TODO Change to negative answer list
            goShortItem5ToItem9.OutputList.Add("Okay, how about a target profit?");
            goShortItem5ToItem9.TargetDialogueItemName = "GoShortDialogueItem9";
            goShortDialogueItem5.ActionList.Add(goShortItem5ToItem9);

            ResponseAction goShortItem5ToItem6Action = new ResponseAction(); // set a stop path
            goShortItem5ToItem6Action.InputList.Add("yes");
            goShortItem5ToItem6Action.TargetDialogueItemName = "GoShortDialogueItem6";
            goShortDialogueItem5.ActionList.Add(goShortItem5ToItem6Action);
            goShortDialogue.ItemList.Add(goShortDialogueItem5);

            //Item6: where to set stop question
            InteractionItem goShortDialogueItem6 = new InteractionItem();
            goShortDialogueItem6.Name = "GoShortDialogueItem6";
            goShortDialogueItem6.MillisecondDelay = 500;
            OutputAction goShortItem6ToItem7 = new OutputAction();
            goShortItem6ToItem7.OutputList.Add("At what level would you like to set your stop loss?");
            goShortItem6ToItem7.TargetDialogueItemName = "GoShortDialogueItem7";
            goShortDialogueItem6.ActionList.Add(goShortItem6ToItem7);
            goShortDialogue.ItemList.Add(goShortDialogueItem6);

            //Item7 : where to set stop answer
            SLTPOrderItem goShortDialogueItem7 = new SLTPOrderItem(SLTPOrderItem.SET_STOPP_LOSS, MarketOrderItem.DIRECTION_SHORT);
            goShortDialogueItem7.Name = "GoShortDialogueItem7";
            goShortDialogueItem7.MillisecondDelay = 500;
            LimitExecutionAction goShortItem7ToItem8 = new LimitExecutionAction();
            goShortItem7ToItem8.InputList.Add("success");
            goShortItem7ToItem8.TargetDialogueItemName = "GoShortDialogueItem8";
            goShortDialogueItem7.ActionList.Add(goShortItem7ToItem8);
            ResponseAction goShortItem7ToItem6 = new ResponseAction();
            goShortItem7ToItem6.InputList.Add("failure");
            goShortItem7ToItem6.OutputList.Add("Your limit was on the wrong side of the market, try again");
            goShortItem7ToItem6.TargetDialogueItemName = "GoShortDialogueItem6";
            goShortDialogueItem7.ActionList.Add(goShortItem7ToItem6);
            goShortDialogue.ItemList.Add(goShortDialogueItem7);

            //Item8 : set TP question
            InteractionItem goShortDialogueItem8 = new InteractionItem();
            goShortDialogueItem8.Name = "GoShortDialogueItem8";
            goShortDialogueItem8.MillisecondDelay = 1500;
            OutputAction goShortItem8ToItem9 = new OutputAction();
            goShortItem8ToItem9.OutputList.Add("Would you also like to set a Target Profit?");
            goShortItem8ToItem9.TargetDialogueItemName = "GoShortDialogueItem9";
            goShortDialogueItem8.ActionList.Add(goShortItem8ToItem9);
            goShortDialogue.ItemList.Add(goShortDialogueItem8);

            //Item9 : set TP answer
            InteractionItem goShortDialogueItem9 = new InteractionItem();
            goShortDialogueItem9.Name = "GoShortDialogueItem9";
            goShortDialogueItem9.MillisecondDelay = 500;

            ResponseAction goShortItem9ToMainDialogueAction = new ResponseAction(); //dont set a target path
            goShortItem9ToMainDialogueAction.InputList.Add("no"); //TODO Change to negative answer list
            goShortItem9ToMainDialogueAction.OutputList.Add("Okay, have a nice day");
            goShortItem9ToMainDialogueAction.TargetDialogueItemName = "MainDialogueItem1";
            goShortItem9ToMainDialogueAction.BrainProcessToDeactivate = "GoShortDialogue";
            goShortItem9ToMainDialogueAction.BrainProcessToActivate = "MainDialogue";
            goShortDialogueItem9.ActionList.Add(goShortItem9ToMainDialogueAction);

            ResponseAction goShortItem9ToItem10Action = new ResponseAction(); // set a target path
            goShortItem9ToItem10Action.InputList.Add("yes");
            goShortItem9ToItem10Action.TargetDialogueItemName = "GoShortDialogueItem10";
            goShortDialogueItem9.ActionList.Add(goShortItem9ToItem10Action);
            goShortDialogue.ItemList.Add(goShortDialogueItem9);

            //Item10: where to set target question
            InteractionItem goShortDialogueItem10 = new InteractionItem();
            goShortDialogueItem10.Name = "GoShortDialogueItem10";
            goShortDialogueItem10.MillisecondDelay = 500;
            OutputAction goShortItem10ToItem11 = new OutputAction();
            goShortItem10ToItem11.OutputList.Add("At what level would you like to set your target profit?");
            goShortItem10ToItem11.TargetDialogueItemName = "GoShortDialogueItem11";
            goShortDialogueItem10.ActionList.Add(goShortItem10ToItem11);
            goShortDialogue.ItemList.Add(goShortDialogueItem10);


            //Item11 : where to set target answer
            SLTPOrderItem goShortDialogueItem11 = new SLTPOrderItem(SLTPOrderItem.SET_TARGET_PROFIT, MarketOrderItem.DIRECTION_SHORT);
            goShortDialogueItem11.Name = "GoShortDialogueItem11";
            goShortDialogueItem11.MillisecondDelay = 500;
            LimitExecutionAction goShortItem11ToItem12 = new LimitExecutionAction();
            goShortItem11ToItem12.InputList.Add("success");
            goShortItem11ToItem12.TargetDialogueItemName = "GoShortDialogueItem12";
            goShortDialogueItem11.ActionList.Add(goShortItem11ToItem12);
            ResponseAction goShortItem11ToItem10 = new ResponseAction();
            goShortItem11ToItem10.InputList.Add("failure");
            goShortItem11ToItem10.OutputList.Add("Your limit was on the wrong side of the market, try again");
            goShortItem11ToItem10.TargetDialogueItemName = "GoShortDialogueItem10";
            goShortDialogueItem11.ActionList.Add(goShortItem11ToItem10);
            goShortDialogue.ItemList.Add(goShortDialogueItem11);


            //Item12 : Everything done, return to main dialogue
            InteractionItem goShortDialogueItem12 = new InteractionItem();
            goShortDialogueItem12.Name = "GoShortDialogueItem12";
            goShortDialogueItem12.MillisecondDelay = 1500;
            OutputAction goShortItem12ToMainDialogue = new OutputAction();
            goShortItem12ToMainDialogue.OutputList.Add("All is set, good luck and have a nice day!");
            goShortItem12ToMainDialogue.TargetDialogueItemName = "MainDialogueItem1";
            goShortItem12ToMainDialogue.BrainProcessToDeactivate = "GoShortDialogue";
            goShortItem12ToMainDialogue.BrainProcessToActivate = "MainDialogue";
            goShortDialogueItem12.ActionList.Add(goShortItem12ToMainDialogue);
            goShortDialogue.ItemList.Add(goShortDialogueItem12);


            /*
             * goShort Dialogue Process end
             */

            FinalizeSetup();
        }

        
        #endregion

        #region GUI action methods
        

        private void AgentMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (agent != null)
            {
                agent.Stop();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            TimerResolution.TimeBeginPeriod(1);  // Sets the timer resolution to 1 ms (default in Windows 7: 15.6 ms)
            startButton.Enabled = false;
            stopButton.Enabled = true;
            agentTabControl.SelectTab(workingMemoryTabPage);
            agent.Start();
            agent.SetWindowPositions();
            agent.ShowVisualizerOnly();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = false;
            if (agent != null)
            {
                agent.Stop();
            }
            TimerResolution.TimeEndPeriod(1); // See also the startButton_Click method.
            memoryViewer.Clear();
            communicationLogColorListBox.Items.Clear();
        }
        #endregion

        #region Private methods
        // Sets up and agent, defining everything EXCEPT the processes (e.g. its long-term memory)
        private void SetUpAgent()
        {
            agent = new StockMarketAgent();
            agent.Name = "TestAgent";

            // Define communication server
           // agent.Server.Name = "Agent";
            agent.Server.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleAgentServerProgress);

            agent.WorkingMemory.MemoryChanged += new EventHandler(HandleWorkingMemoryChanged);
            //    agent.Server.Received += new EventHandler<DataPacketEventArgs>(HandleAgentServerReceived);  // Not used
            agent.IPAddress = "127.0.0.1";  // (Not really needed (default value))
            agent.CommunicationPort = 7;    // (Not really needed (default value))

            // Define client processes:
            agent.ClientProcessRelativeFilePathList = new List<string>();
            agent.ClientProcessRelativeFilePathList.Add("..\\..\\..\\ListenerApplication\\bin\\Debug\\ListenerApplication.exe");
            agent.ClientProcessRelativeFilePathList.Add("..\\..\\..\\SpeechApplication\\bin\\Debug\\SpeechApplication.exe");
            agent.ClientProcessRelativeFilePathList.Add("..\\..\\..\\InternetDataAcquisitionApplication\\bin\\Debug\\InternetDataAcquisitionApplication.exe");

            timeOfLastidaResponseSearchItem = DateTime.MinValue;
            timeOfLastidaResponsePortfolioItem = DateTime.MinValue;
        }

        private void FinalizeSetup()
        {
            startButton.Enabled = true;
            memoryViewer.SetMemory(agent.WorkingMemory);
            memoryViewer.InvocationListVisible = true;
        }

        //insert message e into log
        private void InsertLogMessage(CommunicationProgressEventArgs e)
        {
            Monitor.Enter(communicationLogLockObject);
            Color itemBackColor = communicationLogColorListBox.BackColor;
            Color ItemForeColor = communicationLogColorListBox.ForeColor;
            ColorListBoxItem item = new ColorListBoxItem(e.DateTime.ToString(DATETIME_FORMAT) + ": " + e.Message,
                itemBackColor, ItemForeColor);
            communicationLogColorListBox.Items.Insert(0, item);
            Monitor.Exit(communicationLogLockObject);
        }

        //method subscribed on agent's progress eventhandler
        private void HandleAgentServerProgress(object sender, CommunicationProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => InsertLogMessage(e)));
            }
            else { InsertLogMessage(e); }
        }

        private void HandleWorkingMemoryChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() => InsertSearchResult()));
            }
            else { InsertSearchResult(); }

        }

        private void InsertSearchResult()
        { 
            MemoryItem idaItem = agent.WorkingMemory.GetLastItemByTag(MemoryItemTags.InternetDataAcquisitionProcess);

            if (idaItem != null)
            {
                //Only go further if idaItem is a response (both IDA requests and responses share the same MemoryItemTag)
                string idaItemType = idaItem.Content.Split('*')[0];
                string idaItemMessage = idaItem.Content.Split('*')[1];
                

                if (idaItemType.Equals("responseSearch"))
                {
                    JavaScriptSerializer parser = new JavaScriptSerializer();

                    //Put ':' back into string to make JSON compatible
                    string idaItemMessageJson = idaItemMessage.Replace(';', ':');

                    List<StockInfo> stockInfoList = (List<StockInfo>)parser.Deserialize(idaItemMessageJson, typeof(List<StockInfo>));
                    StockInfo stockInfo = stockInfoList.First();

                    stockInfoTextBox.Text = stockInfo.t + "\n\r" + stockInfo.l_fix;
                 
                }
                if (idaItemType.Equals("responsePortfolio"))
                {

                }
            }
        }

        //save agent object with serialize 
        private void SaveAccountMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "XML files (*.xml)|*.xml";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ObjectXmlSerializer.SerializeObject(saveFileDialog.FileName, agent);
                }
            }
        }

        //load agent object from file with serializer
        private void LoadAccountMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML files (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    agent = (Agent)ObjectXmlSerializer.ObtainSerializedObject(openFileDialog.FileName, typeof(Agent));
                    agent.Initialize();
                    agent.Server.Name = "Agent";
                    agent.Server.Progress += new EventHandler<CommunicationProgressEventArgs>(HandleAgentServerProgress);
                    FinalizeSetup();
                }
            }
        }

        /*  private void InsertDialogueMessage(string dialogueMessage)
          {
              Monitor.Enter(dialogueLockObject);
              Color itemBackColor = communicationLogColorListBox.BackColor;
              Color ItemForeColor = communicationLogColorListBox.ForeColor;
              ColorListBoxItem item = new ColorListBoxItem(dialogueMessage, itemBackColor, Color.Lime);
              dialogueColorListBox.Items.Insert(0, item);
              Monitor.Exit(dialogueLockObject);
          }  */
        #endregion

        private void NewAccountMenuItem_Click(object sender, EventArgs e)
        {
            //GenerateTestAgent4();
            GenerateAgent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
