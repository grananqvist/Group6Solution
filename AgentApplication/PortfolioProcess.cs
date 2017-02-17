using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using AgentLibrary.Memories;
using AgentLibrary.BrainProcesses;
using AgentLibrary;
using System.Web.Script.Serialization;
using AgentApplication.MarketObjects;
using System.Windows.Forms;


namespace AgentApplication
{
    [DataContract]
    public class PortfolioProcess : BrainProcess
    {
        private List<Trade> stockList = null;

        private DateTime timeOfLastInput;
        private System.Windows.Forms.TableLayoutPanel portfolioTable;
        private double availableFunds = 0;

        private string stockInFocus = null;
        private double stockInFocusLastPrice = 0;
        private int tradeInFocus = 0;
        private int nextPortfolioTradeID = 0;

        private DateTime timeOfLastResponseSearch;
        private DateTime timeOfLastResponsePortfolio;

        private string processActivatedOnFailure = null; // The process that will be activated IF the current process fails (see HandleWorkingMemoryChanged)

        public PortfolioProcess(System.Windows.Forms.TableLayoutPanel portfolioTable, double startingCapital)
        {
            this.portfolioTable = portfolioTable;
            this.availableFunds = startingCapital;
            stockList = new List<Trade>();
            timeOfLastResponseSearch = DateTime.Now;
            timeOfLastResponsePortfolio = DateTime.Now;
        }

        //if activate brain process and first DialogueItem doesnt require input, execute it
        public override void Activate()
        {
            base.Activate();
            timeOfLastInput = DateTime.Now; // Required here, to avoid triggering nonMatchingInput

            
        }

        // Run during startup. Sets pointers
        public override void SetOwnerAgent(Agent agent)
        {
            base.SetOwnerAgent(agent);

            timeOfLastInput = DateTime.MinValue;
        }

        public bool IsTradeSuccessful(int quantity, int direction)
        {
            bool sufficientFunds = quantity * direction * stockInFocusLastPrice <= availableFunds;

            if (sufficientFunds)
            {
                //Current time not a good estimate for fill time, but its a simulation anyway
                DateTime fillTime = DateTime.Now;

                //Create a new trade to save in portfolio
                tradeInFocus = nextPortfolioTradeID;
                Trade newTrade = new Trade(nextPortfolioTradeID++,stockInFocus, quantity * direction, fillTime, stockInFocusLastPrice);
                stockList.Add(newTrade);
                

                //subtract trade cost (no comissions, spread, slippage. Very simplified)
                availableFunds -= quantity * direction * stockInFocusLastPrice;

                //Update portfolio data query by including the new position
                refreshPortfolioDataQuery();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsSetLimitSuccessful(double limit, int limitOrderType, int direction)
        {
            if (limitOrderType * direction < 0)
            {
                //Trying to set a stop loss on a buy position, or a target profit on a sell position,
                //limit needs to be below current price
                if (limit < stockInFocusLastPrice)
                {
                    //Find the trade by searching by id, then add the limit to it
                    Trade tempTrade = stockList.Find(x => x.Id == tradeInFocus);
                    if (limitOrderType == SLTPOrderItem.SET_STOPP_LOSS) tempTrade.StopLoss = limit;
                    if (limitOrderType == SLTPOrderItem.SET_TARGET_PROFIT) tempTrade.TargetProfit = limit;

                    //Repaint form containing info about positions
                    if (portfolioTable.InvokeRequired)
                    {
                        portfolioTable.BeginInvoke(new MethodInvoker(() => RepaintPortfolioTable()));
                    }
                    else { RepaintPortfolioTable(); }

                    return true;
                }
            }
            else if (limitOrderType * direction > 0)
            {
                //Trying to set a stop loss on a sell position, or a target profit on a buy position,
                //limit needs to be above current price
                if (limit > stockInFocusLastPrice)
                {
                    //Find the trade by searching by id, then add the limit to it
                    Trade tempTrade = stockList.Find(x => x.Id == tradeInFocus);
                    if (limitOrderType == SLTPOrderItem.SET_STOPP_LOSS) tempTrade.StopLoss = limit;
                    if (limitOrderType == SLTPOrderItem.SET_TARGET_PROFIT) tempTrade.TargetProfit = limit;

                    //Repaint form containing info about positions
                    if (portfolioTable.InvokeRequired)
                    {
                        portfolioTable.BeginInvoke(new MethodInvoker(() => RepaintPortfolioTable()));
                    }
                    else { RepaintPortfolioTable(); }

                    return true;
                }
            }
            return false;
        }

        public bool TryExitPosition(int id)
        {
            Trade tempTrade;
            //if there is a posistion with the given id, sell it
            if ((tempTrade = stockList.Find(x => x.Id == id)) != null)
            {
                availableFunds += tempTrade.Quantity * tempTrade.LastPrice;
                stockList.Remove(tempTrade);

                //Repaint form containing info about positions
                if (portfolioTable.InvokeRequired)
                {
                    portfolioTable.BeginInvoke(new MethodInvoker(() => RepaintPortfolioTable()));
                }
                else { RepaintPortfolioTable(); }


                return true;
            }
            return false;
        }

        private void refreshPortfolioDataQuery()
        {

            MemoryItem outputItem = new MemoryItem();
            outputItem.CreationDateTime = DateTime.Now;
            outputItem.Tag = MemoryItemTags.InternetDataAcquisitionProcess;
            outputItem.Content = "requestPortfolio*" + TickerListString;
            ownerAgent.WorkingMemory.InsertItem(outputItem);
        }

        private void RepaintPortfolioTable()
        {

            portfolioTable.Controls.Clear();
            portfolioTable.RowCount = stockList.Count+1;
            portfolioTable.Size = new System.Drawing.Size(723, 30*(stockList.Count+1));
            portfolioTable.Location = new System.Drawing.Point(0, 30);
            for (int i = 0; i < stockList.Count+1; i++)
            {
                portfolioTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            }

            double totalPAndL = 0;
            for (int row = 0; row < stockList.Count; row++)
            {
                Trade tempTrade = stockList[row];
                portfolioTable.Controls.Add(GenerateLabel(tempTrade.Id.ToString()), 0, row);
                portfolioTable.Controls.Add(GenerateLabel(tempTrade.FillTime.ToString()), 1, row);
                portfolioTable.Controls.Add(GenerateLabel(tempTrade.StockName), 2, row);
                portfolioTable.Controls.Add(GenerateLabel(tempTrade.Quantity.ToString()), 3, row);
                portfolioTable.Controls.Add(GenerateLabel(tempTrade.FillPrice.ToString()), 4, row);
                
                if (tempTrade.StopLoss != 0)
                {
                    portfolioTable.Controls.Add(GenerateLabel(tempTrade.StopLoss.ToString()), 5, row);
                }

                if (tempTrade.TargetProfit != 0)
                {
                    portfolioTable.Controls.Add(GenerateLabel(tempTrade.TargetProfit.ToString()), 6, row);
                }

                portfolioTable.Controls.Add(GenerateLabel(tempTrade.LastPrice.ToString()), 7, row);
                portfolioTable.Controls.Add(GenerateLabel(tempTrade.ProfitAndLoss.ToString("F2")), 8, row);
                totalPAndL += tempTrade.ProfitAndLoss;
            }

            //Label for total P&L
            Label totalPAndLLabel = new Label();
            totalPAndLLabel.Text = totalPAndL.ToString();
            totalPAndLLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            totalPAndLLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            portfolioTable.Controls.Add(totalPAndLLabel, 8,portfolioTable.RowCount-1);

            //Label saying "Total: "
            Label totalTextLabel = new Label();
            totalTextLabel.Text = "Total";
            totalTextLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            totalTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            portfolioTable.Controls.Add(totalTextLabel, 7, portfolioTable.RowCount-1);
        }

        
        

        private Label GenerateLabel(string text)
        {
            Label label = new Label();
            label.Text = text;
            label.Anchor = System.Windows.Forms.AnchorStyles.None;

            //TODO: style & format

            return label;
        }

        /*
         * Method executed when short term memory changed
         */
        protected override void HandleWorkingMemoryChanged(object sender, EventArgs e)
        {

            //Set the most recent search ticker in focus
            MemoryItem idaItem = ownerAgent.WorkingMemory.GetLastItemByTag(MemoryItemTags.InternetDataAcquisitionProcess);
            if (idaItem != null)
            {
                //Only go further if idaItem is a response (both IDA requests and responses share the same MemoryItemTag)
                string idaItemType = idaItem.Content.Split('*')[0];
                string idaItemMessage = idaItem.Content.Split('*')[1];


                if (idaItemType.Equals("responseSearch") && timeOfLastResponseSearch < idaItem.CreationDateTime)
                {
                    JavaScriptSerializer parser = new JavaScriptSerializer();

                    //Put ':' back into string to make JSON compatible
                    string idaItemMessageJson = idaItemMessage.Replace(';', ':');

                    List<StockInfo> stockInfoList = (List<StockInfo>)parser.Deserialize(idaItemMessageJson, typeof(List<StockInfo>));
                    StockInfo stockInfo = stockInfoList.First();

                    stockInFocus = stockInfo.t;
                    stockInFocusLastPrice = stockInfo.l_fix;

                    timeOfLastResponseSearch = idaItem.CreationDateTime;
                }
                if (idaItemType.Equals("responsePortfolio") && timeOfLastResponsePortfolio < idaItem.CreationDateTime)
                {
                    JavaScriptSerializer parser = new JavaScriptSerializer();

                    //Put ':' back into string to make JSON compatible
                    string idaItemMessageJson = idaItemMessage.Replace(';', ':');

                    List<StockInfo> stockInfoList = (List<StockInfo>)parser.Deserialize(idaItemMessageJson, typeof(List<StockInfo>));

                    //Iterate through new data recieved about stocks and update the last known price of each stock in portfolio
                    foreach(StockInfo stockInfo in stockInfoList)
                    {
                        
                        List<Trade> tempTradeList = stockList.FindAll(x => x.StockName == stockInfo.t);
                        foreach( Trade tempTrade in tempTradeList)
                        {
                            tempTrade.LastPrice = stockInfo.l_fix;
                        }
                        
                    }
                    timeOfLastResponsePortfolio = idaItem.CreationDateTime;

                    //Repaint form containing info about positions
                    if (portfolioTable.InvokeRequired)
                    {
                        portfolioTable.BeginInvoke(new MethodInvoker(() => RepaintPortfolioTable()));
                    }
                    else { RepaintPortfolioTable(); }
                }
            }

            //Check if there is any request to modify the portfolio
            MemoryItem portfolioItem = ownerAgent.WorkingMemory.GetLastItemByTag(MyMemoryItemTags.PortfolioUpdate);
            if (portfolioItem != null)
            {
                if (timeOfLastInput < portfolioItem.CreationDateTime)
                {

                    string content = portfolioItem.Content;



                    timeOfLastInput = portfolioItem.CreationDateTime;
                }
            }
        }


        [DataMember]
        public int NextPortfolioTradeID
        {
            get { return nextPortfolioTradeID; }
            set { nextPortfolioTradeID = value; }
        }

        [DataMember]
        public List<Trade> StockList
        {
            get { return stockList; }
            set { stockList = value; }
        }

        [DataMember]
        public string ProcessActivatedOnFailure
        {
            get { return processActivatedOnFailure; }
            set { processActivatedOnFailure = value; }
        }

        [DataMember]
        public double AvailableFunds
        {
            get { return availableFunds; }
            set { availableFunds = value; }
        }
        
        public string StockInFocus
        {
            get { return stockInFocus; }
            set { stockInFocus = value; }
        }

        public double StockInFocusLastPrice
        {
            get { return stockInFocusLastPrice; }
            set { stockInFocusLastPrice = value; }
        }

        /*
         * Returns a string of tickers currently in portfolio
         */
        public string TickerListString
        {
            get
            {
                List<string> tickerList = new List<string>();
                string output = "";

                //make sure items of tickerList are unique
                foreach(Trade trade in stockList)
                {
                    if (!tickerList.Exists(x => x == trade.StockName))
                    {
                        tickerList.Add(trade.StockName);
                    }
                }

                foreach(string ticker in tickerList)
                {
                    //Ment to be ':', but it is reserved
                    output += "STO;" + ticker + ',';
                }
                //Remove last ','
                output = output.Substring(0, output.Length-1);

                return output;
            }
        }
    }
}
