// //FiremanApi->FiremanApi->ResultResponse.cs
// //andreygolubkow Андрей Голубков
namespace FiremanApi2.Adapters
{
    public struct ResultResponse
    {
        public ResultResponse(int status)
        {
            Status = status;
            Message = "";
        }

        public ResultResponse(int status,string message)
        {
            Status = status;
            Message = message;
        }

        public int Status;

        public string Message;
    }
}
