namespace ClothingPartnerAPI.DTO.Base
{
    public class ResponseDto<T> where T : class
    {
        public Error Error { get; set; }
        public T Data { get; set; }
        public string ResultOkMessage { get; set; }

        public ResponseDto()
        {
            Error = new Error();
        }
    }
}
