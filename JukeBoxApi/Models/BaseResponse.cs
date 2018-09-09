


namespace JukeBoxApi.Models
{
    public class BaseResponse<T> where T : class
    {
        public T ResponseObject { get; set; }

        public string ResponseMessage { get; set; }

        public ResponseType ResponseType { get; set; }
    }    
}
