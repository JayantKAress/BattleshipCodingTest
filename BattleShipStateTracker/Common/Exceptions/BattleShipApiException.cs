using System;
using System.Globalization;

namespace BattleShipCodingTest.Shared.Exceptions
{
    public class BattleShipApiException : Exception
    {
        public BattleShipApiException(string message) : base(message) { }

        public BattleShipApiException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
        private static void ThrowException(string entityName)
        {
            throw new BattleShipApiException("Not Found", entityName);
        }
    }
}
