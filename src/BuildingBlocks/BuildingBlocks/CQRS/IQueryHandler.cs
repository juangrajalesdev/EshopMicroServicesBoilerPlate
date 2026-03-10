using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler <in Tquery,TResponse>
        :IRequestHandler<Tquery,TResponse>
        where Tquery : IQuery<TResponse>
        where TResponse : notnull 
    {
    }
}
