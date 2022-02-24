namespace AwesomeMusic.Data.DTOs
{
    using MediatR;

    public class Response<T> : IRequest<T>
    {
        public object Error { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
    }
}
