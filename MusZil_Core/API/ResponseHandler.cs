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
        public static (decimal,string) GetBalanceFromResult(ref MusResponse resp,Unit unit = Unit.ZIL)
        {

            decimal balance = resp.Error != null ? -1 : (decimal)((JObject)resp.Result)["balance"];
            var msg = "";
            if (!IsError(ref resp, out msg))
            {
                switch (unit)
                {
                    case Unit.ZIL:
                        balance /= 1000000000000;
                        break;
                    case Unit.LI:
                        balance /= 1000000;
                        break;
                }
            }
            else
            {
                balance = -1;
            }
            
            return (balance, msg);
        }

        #endregion

        #region Contracts 

        public static string GetContractCode(ref MusResponse resp)
        {
            return (string)((JObject)resp.Result)["code"];
        }

        #endregion

        #region Helpers
        
        private static bool IsError(ref MusResponse resp,out string message)
        {
            var isError = resp.Error != null;
            message =  isError ? ((JObject)resp.Error)["message"].ToString() : "Success";
            return isError;
        }

        #endregion

    }
}
