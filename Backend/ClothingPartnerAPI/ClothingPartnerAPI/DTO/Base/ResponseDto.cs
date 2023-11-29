namespace ClothingPartnerAPI.DTO.Base
{
    public class ResponseDTO<T> where T : class
    {
        public Error Error { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ResponseDTO()
        {
            Error = new Error();
        }
    }
}
