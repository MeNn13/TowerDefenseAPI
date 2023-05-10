using System.Net;

namespace TowerDefenseAPI.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; } = default!;
        public T Data { get; set; } = default!;
    }

    public interface IBaseResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        T Data { get; set; }
    }
}
