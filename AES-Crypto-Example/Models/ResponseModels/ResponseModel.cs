namespace AES_Crypto_Example.ResponseModels
{
    public class ResponseModel
    {        
        public string Message;
        public bool Status;

        public ResponseModel(string message, bool status)
        {
            Message = message;
            Status = status;
        }
    }
}
