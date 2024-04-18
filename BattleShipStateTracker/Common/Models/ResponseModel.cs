namespace BattleShipStateTracker.Common.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        { }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public ResponseModel(string message = null)
        {
            Succeeded = true;
            StatusCode = 200;
            Message = message;
        }
    }
}
