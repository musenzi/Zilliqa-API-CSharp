using MusZil_Core.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusZil_Core.API
{
    public class ResponseHandler 
    {
        #region Acoounts

        /// <summary>
        /// Gets Balance from result object 
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="unit">Default: Unit.ZIL. See Unit enum</param>
        /// <returns></returns>
        public static decimal GetBalanceFromResult(ref MusResponse resp,Unit unit = Unit.ZIL)
        {
            decimal balance = (decimal)((JObject)resp.Result)["balance"];
            switch (unit)
            {
                case Unit.ZIL:
                    balance /= 1000000000000;
                    break;
                case Unit.LI:
                    balance /= 1000000;
                    break;
            }
            return balance;
        }

        #endregion

        #region Contracts 

        public static string GetContractCode(ref MusResponse resp)
        {
            return (string)((JObject)resp.Result)["code"];
        }

        #endregion

    }
}
