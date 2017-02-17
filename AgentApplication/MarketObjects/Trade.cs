using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentApplication.MarketObjects
{
    public class Trade
    {
        private int id;
        private string stockName;
        private int quantity;
        private DateTime fillTime;
        private double fillPrice;
        private double stopLoss;
        private double targetProfit;

        private double lastPrice = 0;

        public Trade(int id, string stockName, int quantity, DateTime fillTime, double fillPrice)
        {
            this.id = id;
            this.stockName = stockName;
            this.quantity = quantity;
            this.fillTime = fillTime;
            this.fillPrice = fillPrice;
        }

        /*
         * Calculates P&L of a position
         */
        public double ProfitAndLoss
        {
            get { return (lastPrice - fillPrice) * quantity; }
        }

        public double StopLoss
        {
            get { return stopLoss; }
            set { stopLoss = value; }
        }

        public double TargetProfit
        {
            get { return targetProfit; }
            set { targetProfit = value; }
        }

        public int Id
        {
            get { return id; }
        }

        public string StockName
        {
            get { return stockName; }
        }

        public int Quantity
        {
            get { return quantity; }
        }

        public DateTime FillTime
        {
            get { return fillTime; }
        }

        public double FillPrice
        {
            get { return fillPrice; }
        }

        public double LastPrice
        {
            get { return lastPrice; }
            set { lastPrice = value; }
        }
    }
}
