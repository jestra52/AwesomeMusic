namespace AwesomeMusic.Data.Commands
{
    using MediatR;

    public abstract class CommandBase<T> : IRequest<T>
    {}
}
