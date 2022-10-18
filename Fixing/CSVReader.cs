using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace Fixing
{

    public class FixingRow
    {
        [Index(0)]
        public string Name { get; set; }

        [Index(1)]  
        public string ISIN { get; set; }

        [Index(2)]
        public string Bid { get; set; }

        [Index(3)]
        public string Ask { get; set; }
    }

    class CSVReader
    {
        public SecItemsData ReadCSV(string strFileName, string strBrokerCode, SecItemsData secItemsData)
        {
            try
            {
                using (var reader = new StreamReader(strFileName))
                {
                    CsvConfiguration conf = new CsvConfiguration(CultureInfo.InvariantCulture);
                    conf.AllowComments = false;
                    conf.HasHeaderRecord = false;
                    //conf.Delimiter = ";";
                    conf.DetectDelimiter = true;
                    conf.MissingFieldFound = null;

                    using (var csv = new CsvReader(reader, conf))
                    {
                        IEnumerable<FixingRow> records = csv.GetRecords<FixingRow>();
                        bool bFirstRow = true;
                        foreach (FixingRow row in records)
                        {


                            string strName;
                            string strISIN;
                            decimal dBid;
                            decimal dAsk;

                            strName = row.Name.Trim();
                            strISIN = row.ISIN.Trim();
                            string sValue;

                            sValue = row.Bid.Trim();
                            if (sValue.Contains(".")) sValue = sValue.Replace(".", ",");
                            try
                            {
                                dBid = System.Convert.ToDecimal(sValue);
                            }
                            catch (Exception ex)
                            {
                                dBid = 0;
                                if (bFirstRow)
                                {
                                    bFirstRow = false;
                                    continue;
                                }
                                else dBid = 0;
                            }

                            sValue = row.Ask.Trim();
                            if (sValue.Contains(".")) sValue = sValue.Replace(".", ",");
                            try
                            {
                                dAsk = System.Convert.ToDecimal(sValue);
                            }
                            catch (Exception ex)
                            {
                                dAsk = 0;
                                if (bFirstRow)
                                {
                                    bFirstRow = false;
                                    continue;
                                }
                                else dAsk = 0;
                            }

                            bFirstRow = false;
                            if (strISIN.Length > 0 && dBid > 0 && dAsk > 0)
                            {
                                secItemsData.AddSecItemInfo(strBrokerCode, strName, strISIN, dBid, dAsk);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "CSV read error");
            }
            return secItemsData;
        }
    }
}
