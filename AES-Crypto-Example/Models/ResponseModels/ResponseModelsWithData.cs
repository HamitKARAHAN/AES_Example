namespace AES_Crypto_Example.ResponseModels
{
    public class ResponseModelWithData<T>
    {
        public string Message;
        public bool Status;
        public T Data;

        public ResponseModelWithData(string message, bool status, T data)
        {
            Message = message;
            Status = status;
            Data = data;
        }
    }
}
