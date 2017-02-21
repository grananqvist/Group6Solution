using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentApplication
{
    class DialogueInputStrings
    {
        /*
         * Pairing similar inputs into lists of categories
         * 
         * This was done before being able to override checkMatch() 
         */

        private const string YES_INPUT_STRING_1 = "Yes";
        private const string YES_INPUT_STRING_2 = "Yes please";
        private const string YES_INPUT_STRING_3 = "Sure";
        private const string YES_INPUT_STRING_4 = "Of course";
        private const string YES_INPUT_STRING_5 = "Gladly";

        private const string NO_INPUT_STRING_1 = "No";
        private const string NO_INPUT_STRING_2 = "No thanks";
        private const string NO_INPUT_STRING_3 = "i prefer not";
        private const string NO_INPUT_STRING_4 = "not really";

        private const string GREETING_INPUT_STRING_1 = "Hello";
        private const string GREETING_INPUT_STRING_2 = "Hi";
        private const string GREETING_INPUT_STRING_3 = "Good morning";
        private const string GREETING_INPUT_STRING_4 = "Good afternoon";
        private const string GREETING_INPUT_STRING_5 = "Good evening";

        private const string BAD_INPUT_STRING_1 = "awful";
        private const string BAD_INPUT_STRING_2 = "not good";
        private const string BAD_INPUT_STRING_3 = "not so good";
        private const string BAD_INPUT_STRING_4 = "not very good";
        private const string BAD_INPUT_STRING_5 = "not good at all";
        private const string BAD_INPUT_STRING_6 = "bad";
        private const string BAD_INPUT_STRING_7 = "pretty bad";
        private const string BAD_INPUT_STRING_8 = "very bad";
        private const string BAD_INPUT_STRING_9 = "booring";
        private const string BAD_INPUT_STRING_10 = "pretty booring";
        private const string BAD_INPUT_STRING_11 = "very booring";
        private const string BAD_INPUT_STRING_PAST_1 = "it was awful";
        private const string BAD_INPUT_STRING_PAST_2 = "it was not good";
        private const string BAD_INPUT_STRING_PAST_3 = "it was not so good";
        private const string BAD_INPUT_STRING_PAST_4 = "it was not very good";
        private const string BAD_INPUT_STRING_PAST_5 = "it was not good at all";
        private const string BAD_INPUT_STRING_PAST_6 = "it was bad";
        private const string BAD_INPUT_STRING_PAST_7 = "it was pretty bad";
        private const string BAD_INPUT_STRING_PAST_8 = "it was very bad";
        private const string BAD_INPUT_STRING_PAST_9 = "it was booring";
        private const string BAD_INPUT_STRING_PAST_10 = "it was pretty booring";
        private const string BAD_INPUT_STRING_PAST_11 = "it was very booring";

        private const string GOOD_INPUT_STRING_1 = "fine";
        private const string GOOD_INPUT_STRING_2 = "good";
        private const string GOOD_INPUT_STRING_3 = "very good";
        private const string GOOD_INPUT_STRING_4 = "decent";
        private const string GOOD_INPUT_STRING_PAST_1 = "it was very good";
        private const string GOOD_INPUT_STRING_PAST_2 = "it was good";
        private const string GOOD_INPUT_STRING_PAST_3 = "it was decent";
        private const string GOOD_INPUT_STRING_PAST_4 = "it was fine";

        private const string GOODBYE_INPUT_STRING_1 = "goodbye";
        private const string GOODBYE_INPUT_STRING_2 = "bye";
        private const string GOODBYE_INPUT_STRING_3 = "byebye";
        private const string GOODBYE_INPUT_STRING_4 = "seeya";
        private const string GOODBYE_INPUT_STRING_5 = "cya";
        private const string GOODBYE_INPUT_STRING_6 = "i have to go";
        private const string GOODBYE_INPUT_STRING_7 = "i have to go now";
        private const string GOODBYE_INPUT_STRING_8 = "i must go";
        private const string GOODBYE_INPUT_STRING_9 = "i must go now";
        private const string GOODBYE_INPUT_STRING_10 = "see you";
        private const string GOODBYE_INPUT_STRING_11 = "see you later";

        private const string STOCK_TICKER_SWMA = "SWMA";
        private const string STOCK_TICKER_SHB = "SHB-A";
        private const string STOCK_TICKER_GETI = "GETI-B";
        private const string STOCK_TICKER_HM = "HM-B";
        private const string STOCK_TICKER_ALFA = "ALFA";
        private const string STOCK_TICKER_TEL2 = "TEL2-B";
        private const string STOCK_TICKER_AZN = "AZN";
        private const string STOCK_TICKER_SECU = "SECU-B";
        private const string STOCK_TICKER_INVE = "INVE-B";
        private const string STOCK_TICKER_ABB = "ABB";
        private const string STOCK_TICKER_ALIV = "ALIV-SDB";
        private const string STOCK_TICKER_ASSA = "ASSA-B";
        private const string STOCK_TICKER_ATCO = "ATCO-A";
        private const string STOCK_TICKER_BOL = "BOL";
        private const string STOCK_TICKER_ELUX = "ELUX-B";
        private const string STOCK_TICKER_ERIC = "ERIC-B";
        private const string STOCK_TICKER_FING = "FING-B";
        private const string STOCK_TICKER_KINV = "KINV-B";
        private const string STOCK_TICKER_LUPE = "LUPE";
        private const string STOCK_TICKER_NDA = "NDA-SEK";
        private const string STOCK_TICKER_SAND = "SAND";
        private const string STOCK_TICKER_SCA = "SCA-B";
        private const string STOCK_TICKER_SEB = "SEB-A";
        private const string STOCK_TICKER_SKA = "SKA-B";
        private const string STOCK_TICKER_SKF = "SKF-B";
        private const string STOCK_TICKER_SSAB = "SSAB-A";
        private const string STOCK_TICKER_SWED = "SWED-A";
        private const string STOCK_TICKER_TLSN = "TLSN";
        private const string STOCK_TICKER_VOLV = "VOLV-B";

        private const string STOCK_NAME_SWMA = "Swedish Match";
        private const string STOCK_NAME_SHB = "Svenska handelsbanken";
        private const string STOCK_NAME_GETI = "getinge";
        private const string STOCK_NAME_HM = "hm";
        private const string STOCK_NAME_ALFA = "alfa laval";
        private const string STOCK_NAME_TEL2 = "tele2";
        private const string STOCK_NAME_AZN = "astraZeneca";
        private const string STOCK_NAME_SECU = "Securitas";
        private const string STOCK_NAME_INVE = "Investor";
        private const string STOCK_NAME_ABB = "ABB";
        private const string STOCK_NAME_ALIV = "Autoliv";
        private const string STOCK_NAME_ASSA = "Assa abloy";
        private const string STOCK_NAME_ATCO = "Atlas copco";
        private const string STOCK_NAME_BOL = "Boliden";
        private const string STOCK_NAME_ELUX = "Electrolux";
        private const string STOCK_NAME_ERIC = "Ericsson";
        private const string STOCK_NAME_FING = "Fingerprint Cards";
        private const string STOCK_NAME_KINV = "Kinnevik";
        private const string STOCK_NAME_LUPE = "Lundin petroleum";
        private const string STOCK_NAME_NDA = "Nordea bank";
        private const string STOCK_NAME_SAND = "Sandvik";
        private const string STOCK_NAME_SCA = "SCA";
        private const string STOCK_NAME_SEB = "SEB";
        private const string STOCK_NAME_SKA = "Skanska";
        private const string STOCK_NAME_SKF = "SKF";
        private const string STOCK_NAME_SSAB = "SSAB";
        private const string STOCK_NAME_SWED = "Swedbank";
        private const string STOCK_NAME_TLSN = "Telia company";
        private const string STOCK_NAME_VOLV = "Volvo";

        public static List<string> getStockTickerInputs()
        {
            List<string> stockTickerList = new List<string>();
            stockTickerList.Add(STOCK_TICKER_SWMA);
            stockTickerList.Add(STOCK_TICKER_SHB);
            stockTickerList.Add(STOCK_TICKER_GETI);
            stockTickerList.Add(STOCK_TICKER_HM);
            stockTickerList.Add(STOCK_TICKER_ALFA);
            stockTickerList.Add(STOCK_TICKER_TEL2);
            stockTickerList.Add(STOCK_TICKER_AZN);
            stockTickerList.Add(STOCK_TICKER_SECU);
            stockTickerList.Add(STOCK_TICKER_INVE);
            stockTickerList.Add(STOCK_TICKER_ABB);
            stockTickerList.Add(STOCK_TICKER_ALIV);
            stockTickerList.Add(STOCK_TICKER_ASSA);
            stockTickerList.Add(STOCK_TICKER_ATCO);
            stockTickerList.Add(STOCK_TICKER_BOL);
            stockTickerList.Add(STOCK_TICKER_ELUX);
            stockTickerList.Add(STOCK_TICKER_ERIC);
            stockTickerList.Add(STOCK_TICKER_FING);
            stockTickerList.Add(STOCK_TICKER_KINV);
            stockTickerList.Add(STOCK_TICKER_LUPE);
            stockTickerList.Add(STOCK_TICKER_NDA);
            stockTickerList.Add(STOCK_TICKER_SAND);
            stockTickerList.Add(STOCK_TICKER_SCA);
            stockTickerList.Add(STOCK_TICKER_SEB);
            stockTickerList.Add(STOCK_TICKER_SKA);
            stockTickerList.Add(STOCK_TICKER_SKF);
            stockTickerList.Add(STOCK_TICKER_SSAB);
            stockTickerList.Add(STOCK_TICKER_SWED);
            stockTickerList.Add(STOCK_TICKER_TLSN);
            stockTickerList.Add(STOCK_TICKER_VOLV);
            return stockTickerList;
        }

        public static List<string> getStockNameList()
        {
            List<string> stockList = new List<string>();
            stockList.Add(STOCK_NAME_SWMA);
            stockList.Add(STOCK_NAME_SHB);
            stockList.Add(STOCK_NAME_GETI);
            stockList.Add(STOCK_NAME_HM);
            stockList.Add(STOCK_NAME_ALFA);
            stockList.Add(STOCK_NAME_TEL2);
            stockList.Add(STOCK_NAME_AZN);
            stockList.Add(STOCK_NAME_SECU);
            stockList.Add(STOCK_NAME_INVE);
            stockList.Add(STOCK_NAME_ABB);
            stockList.Add(STOCK_NAME_ALIV);
            stockList.Add(STOCK_NAME_ASSA);
            stockList.Add(STOCK_NAME_ATCO);
            stockList.Add(STOCK_NAME_BOL);
            stockList.Add(STOCK_NAME_ELUX);
            stockList.Add(STOCK_NAME_ERIC);
            stockList.Add(STOCK_NAME_FING);
            stockList.Add(STOCK_NAME_KINV);
            stockList.Add(STOCK_NAME_LUPE);
            stockList.Add(STOCK_NAME_NDA);
            stockList.Add(STOCK_NAME_SAND);
            stockList.Add(STOCK_NAME_SCA);
            stockList.Add(STOCK_NAME_SEB);
            stockList.Add(STOCK_NAME_SKA);
            stockList.Add(STOCK_NAME_SKF);
            stockList.Add(STOCK_NAME_SSAB);
            stockList.Add(STOCK_NAME_SWED);
            stockList.Add(STOCK_NAME_TLSN);
            stockList.Add(STOCK_NAME_VOLV);

            return stockList;
        }

        public static string convertNameToTicker(string stockName)
        {
            int tickerIndex = -1;

            if ((tickerIndex = DialogueInputStrings.getStockNameList().FindIndex(x => x.ToLower() == stockName.ToLower())) != -1)
            {
                return DialogueInputStrings.getStockTickerInputs()[tickerIndex];
            }
            return stockName;
        }

        public static List<string> getYesInputs() {
            List<string> inputList = new List<string>();
            inputList.Add(YES_INPUT_STRING_1);
            inputList.Add(YES_INPUT_STRING_2);
            inputList.Add(YES_INPUT_STRING_3);
            inputList.Add(YES_INPUT_STRING_4);
            inputList.Add(YES_INPUT_STRING_5);
            return inputList;
        }

        public static List<string> getNoInputs()
        {
            List<string> inputList = new List<string>();
            inputList.Add(NO_INPUT_STRING_1);
            inputList.Add(NO_INPUT_STRING_2);
            inputList.Add(NO_INPUT_STRING_3);
            inputList.Add(NO_INPUT_STRING_4);
            return inputList;
        }

        public static List<string> getGreetingInputs()
        {
            List<string> inputList = new List<string>();
            inputList.Add(GREETING_INPUT_STRING_1);
            inputList.Add(GREETING_INPUT_STRING_2);
            inputList.Add(GREETING_INPUT_STRING_3);
            inputList.Add(GREETING_INPUT_STRING_4);
            inputList.Add(GREETING_INPUT_STRING_5);
            return inputList;
        }

        public static List<string> getAllBadInputs()
        {
            List<string> inputList = new List<string>();
            inputList.Add(BAD_INPUT_STRING_1);
            inputList.Add(BAD_INPUT_STRING_2);
            inputList.Add(BAD_INPUT_STRING_3);
            inputList.Add(BAD_INPUT_STRING_4);
            inputList.Add(BAD_INPUT_STRING_5);
            inputList.Add(BAD_INPUT_STRING_6);
            inputList.Add(BAD_INPUT_STRING_7);
            inputList.Add(BAD_INPUT_STRING_8);
            inputList.Add(BAD_INPUT_STRING_9);
            inputList.Add(BAD_INPUT_STRING_10);
            inputList.Add(BAD_INPUT_STRING_11);
            inputList.Add(BAD_INPUT_STRING_PAST_1);
            inputList.Add(BAD_INPUT_STRING_PAST_2);
            inputList.Add(BAD_INPUT_STRING_PAST_3);
            inputList.Add(BAD_INPUT_STRING_PAST_4);
            inputList.Add(BAD_INPUT_STRING_PAST_5);
            inputList.Add(BAD_INPUT_STRING_PAST_6);
            inputList.Add(BAD_INPUT_STRING_PAST_7);
            inputList.Add(BAD_INPUT_STRING_PAST_8);
            inputList.Add(BAD_INPUT_STRING_PAST_9);
            inputList.Add(BAD_INPUT_STRING_PAST_10);
            inputList.Add(BAD_INPUT_STRING_PAST_11);
            return inputList;
        }

        public static List<string> getAllGoodInputs()
        {
            List<string> inputList = new List<string>();
            inputList.Add(GOOD_INPUT_STRING_1);
            inputList.Add(GOOD_INPUT_STRING_2);
            inputList.Add(GOOD_INPUT_STRING_3);
            inputList.Add(GOOD_INPUT_STRING_4);
            inputList.Add(GOOD_INPUT_STRING_PAST_1);
            inputList.Add(GOOD_INPUT_STRING_PAST_2);
            inputList.Add(GOOD_INPUT_STRING_PAST_3);
            inputList.Add(GOOD_INPUT_STRING_PAST_4);
            return inputList;
        }

        public static List<string> getGoodbyeInputs()
        {
            List<string> inputList = new List<string>();
            inputList.Add(GOODBYE_INPUT_STRING_1);
            inputList.Add(GOODBYE_INPUT_STRING_2);
            inputList.Add(GOODBYE_INPUT_STRING_3);
            inputList.Add(GOODBYE_INPUT_STRING_4);
            inputList.Add(GOODBYE_INPUT_STRING_5);
            inputList.Add(GOODBYE_INPUT_STRING_6);
            inputList.Add(GOODBYE_INPUT_STRING_7);
            inputList.Add(GOODBYE_INPUT_STRING_8);
            inputList.Add(GOODBYE_INPUT_STRING_9);
            inputList.Add(GOODBYE_INPUT_STRING_10);
            inputList.Add(GOODBYE_INPUT_STRING_11);
            return inputList;
        }
    }
}
