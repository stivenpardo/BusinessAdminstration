using System.Net;

namespace BusinessAdministration.Aplication.Dto.Base
{
    public class DataTransferObject
    {
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
}
