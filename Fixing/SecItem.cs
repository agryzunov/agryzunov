using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixing
{
    /// <summary>
    /// Информация по ценной бумаге от одного брокера
    /// </summary>
    public class BrokerItemInfo
    {
        public string BrokerCode;
        public decimal Bid;
        public decimal Ask;
        public BrokerItemInfo(string brokerCode, decimal dBid, decimal dAsk)
        {
            this.BrokerCode = brokerCode;
            this.Bid = dBid;
            this.Ask = dAsk;
        }
    }

    /// <summary>
    ///  Информация по ценной бумаге от всех брокеров
    /// </summary>
    public class SecItemInfo
    {
        public string Name;
        public string ISIN;
        public decimal MIRP;
        private bool _excludeMaxMinForMIRP = false;
        public bool excludeMaxMinForMIRP {
            get 
                {
                    return _excludeMaxMinForMIRP;
                }
            set
                {
                    _excludeMaxMinForMIRP = value;
                    RecalcMIRP(_excludeMaxMinForMIRP);
                }
        }
        public List<BrokerItemInfo> brokerItemInfo; //Список записей по всем брокерам
        public SecItemInfo(string strName, string strISIN, bool bExcludeMaxMin = false)
        {
            this.Name = strName;
            this.ISIN = strISIN;
            excludeMaxMinForMIRP = bExcludeMaxMin;
            brokerItemInfo = new List<BrokerItemInfo>();
            MIRP = 0;
        }
        /// <summary>
        /// Добавить информацию от брокера в список
        /// </summary>
        /// <param name="brokerItemData"></param>
        public void AddBrokerInfo(BrokerItemInfo brokerItemData)
        {
            //Проверяем, нет ли в списке брокера с таким кодом
            bool bExists = false;
            foreach(BrokerItemInfo bi in brokerItemInfo)
            {
                if(bi.BrokerCode.Equals(brokerItemData.BrokerCode))
                {
                    bExists = true;
                    break;
                }
            }

            if(! bExists)
            {
                brokerItemInfo.Add(brokerItemData);
                // Пересчёт MIRP
                if(brokerItemInfo.Count > 1)
                {
                    decimal mirpValue = 0;
                    decimal mirpMax=-1;
                    decimal mirpMin=decimal.MaxValue;
                    bool bInit = true;
                    foreach (BrokerItemInfo bi in brokerItemInfo)
                    {
                        decimal dVal = (bi.Ask + bi.Bid) / 2; 
                        mirpValue += dVal;
                        if (bInit)
                        {
                            mirpMax = dVal;
                            mirpMin = dVal;
                            bInit = false;
                        }
                        else
                        {
                            if (dVal > mirpMax) mirpMax = dVal;
                            if (dVal < mirpMin) mirpMin = dVal;
                        }
                    }
                    if ((!this.excludeMaxMinForMIRP) || (brokerItemInfo.Count <= 5)) mirpValue /= brokerItemInfo.Count;
                    else mirpValue = (mirpValue - mirpMax - mirpMin) / (brokerItemInfo.Count - 2);
                    this.MIRP = mirpValue;
                }
            }
            else
            {
                // Замена и пересчёт MIRP не производится
            }
        }
        public decimal RecalcMIRP(bool bExcludeMaxMin = false)
        {
            decimal mirpValue = 0;
            decimal mirpMax = -1;
            decimal mirpMin = decimal.MaxValue;
            bool bInit = true;
            if ((brokerItemInfo != null) && (brokerItemInfo.Count > 1))
            {
                foreach (BrokerItemInfo bi in brokerItemInfo)
                {
                    decimal dVal = (bi.Ask + bi.Bid) / 2;
                    mirpValue += dVal;
                    if (bInit)
                    {
                        mirpMax = dVal;
                        mirpMin = dVal;
                        bInit = false;
                    }
                    else
                    {
                        if (dVal > mirpMax) mirpMax = dVal;
                        if (dVal < mirpMin) mirpMin = dVal;
                    }
                }
                if ((!this.excludeMaxMinForMIRP) || (brokerItemInfo.Count <= 5)) mirpValue /= brokerItemInfo.Count;
                else mirpValue = (mirpValue - mirpMax - mirpMin) / (brokerItemInfo.Count - 2);
                this.MIRP = mirpValue;
            }
            else this.MIRP = 0;
            return mirpValue;
        }
    }

    public class ShortSecItemInfo
    {
        public string ISIN;
        public decimal MIRP;
        
        public ShortSecItemInfo(string strISIN, decimal decMIRP = 0)
        {
            this.ISIN = strISIN;
            MIRP = decMIRP;
        }
    }
        /// <summary>
        /// Информация по всем ценным бумагам по всем брокерам
        /// </summary>
    public class SecItemsData
    {

        public List<SecItemInfo> secItemData;
        private bool _excludeMaxMinForMIRP = false;
        public bool excludeMaxMinForMIRP
        {
            get
            {
                return _excludeMaxMinForMIRP;
            }
            set
            {
                _excludeMaxMinForMIRP = value;
                foreach (SecItemInfo si in secItemData) si.excludeMaxMinForMIRP = _excludeMaxMinForMIRP;
            }
        }
        //public decimal Bid;
        //public decimal Ask;
        public SecItemInfo FindSecItem(string strISIN)
        {
            foreach(SecItemInfo sii in secItemData)
            {
                if (sii.ISIN.Equals(strISIN)) return sii;
            }
            return null;
        }

        public int CountForOperator(string strBrokerCode)
        {
            int iCount = 0;
            foreach (SecItemInfo sii in secItemData)
            {
                foreach (BrokerItemInfo bIInfo in sii.brokerItemInfo)
                {
                    if (bIInfo.BrokerCode.Equals(strBrokerCode)) iCount++;
                }
            }
            return iCount;
        }

        public int ClearOperatorData(string strBrokerCode)
        {
            int iCount = 0;
            bool bUpdated = false;

            foreach (SecItemInfo sii in secItemData)
            {
                List<BrokerItemInfo> newBrokerInfo = new List<BrokerItemInfo>();
                foreach (BrokerItemInfo bIInfo in sii.brokerItemInfo)
                {
                    if (bIInfo.BrokerCode.Equals(strBrokerCode))
                    { iCount++;
                      bUpdated = true;
                    }
                    else newBrokerInfo.Add(bIInfo);
                }

                if (bUpdated)
                {
                    sii.brokerItemInfo = newBrokerInfo;
                }
                sii.RecalcMIRP();
            }

            List<SecItemInfo> newSecItemData = new List<SecItemInfo>();
            bUpdated = false;
            foreach (SecItemInfo sii in secItemData)
            {
                if (sii.brokerItemInfo.Count > 0)
                {
                    newSecItemData.Add(sii);
                }
                else bUpdated = true;
            }

            if (bUpdated) secItemData = newSecItemData;

            return iCount;
        }
    

        public SecItemsData()
        {
            secItemData = new List<SecItemInfo>();
        }

        public SecItemInfo AddSecItemInfo(string strBrokerCode, string strName, string strISIN, decimal dBid, decimal dAsk)
        {
            // Проверяем, есть ли "строка" для этой бумаги
            SecItemInfo sii = FindSecItem(strISIN);
            if (sii == null)
            {
                // нет строки для такой бумаги, добавляем новую строку для неё
                sii = new SecItemInfo(strName, strISIN, _excludeMaxMinForMIRP);
                //sii.excludeMaxMinForMIRP = _excludeMaxMinForMIRP;
                this.secItemData.Add(sii);
            }
            sii.AddBrokerInfo(new BrokerItemInfo(strBrokerCode, dBid, dAsk));
            return sii;
        }
    }
}
