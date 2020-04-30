using MusZil_Core.Accounts;
using MusZil_Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using static MusZil_Core.Address;

namespace MusZil_Core.API
{
    public class ResponseHandler 
    {
        public static MusResult GetResult(ref APIResponse response, Unit unit = Unit.ZIL)
        {
            var msg = "";
            var resStr = "";
            if (!CheckError(ref response, out msg))
            {
                resStr = (string)((JObject)response.Result)["result"];
            }
            var result = new MusResult(resStr, msg);
            return result;
        }
        #region Accounts

        /// <summary>
        /// Gets Balance from result object 
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="unit">Default: Unit.ZIL. See Unit enum</param>
        /// <returns></returns>
        public static MusResult GetBalanceFromResult(ref APIResponse resp, Unit unit = Unit.ZIL)
        {
            float balance = resp.Error != null ? -1 : (float)((JObject)resp.Result)["balance"];
            var bal = new Balance(balance);
            bal.SwitchUnit(unit);
            var msg = "";
            CheckError(ref resp, out msg);
            return new MusResult(bal.GetBalance(),msg);
        }

        #endregion

        #region Contracts 

        public static MusResult GetContractCode(ref APIResponse resp)
        {
            var msg = "";
            var code = "";
            if (!CheckError(ref resp, out msg))
            {
                code = (string)((JObject)resp.Result)["code"];
            }
            var result = new MusResult(code, "GetContractCode Success");
            return result;
        }

        public static MusResult GetContractBalance(ref APIResponse resp)
        {
            var msg = "";
            decimal balance = -1;
            if (!CheckError(ref resp, out msg))
            {
                balance = (decimal)((JObject)resp.Result)["_balance"];
            }
            var result = new MusResult(balance, "GetContractCode Success");
            return result;
        }

        #endregion

        #region Helpers

        private static bool CheckError(ref APIResponse resp,out string message)
        {
            var isError = resp.Error != null;
            message =  isError ? ((JObject)resp.Error)["message"].ToString() : "API Call Success";
            return isError;
        }

        #endregion

    }
}
