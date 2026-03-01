using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.CQRS
{
    internal interface IQueryHandler <in Tquery,TResponse>
        :IRequestHandler<Tquery,TResponse>
        where Tquery : IQuery<TResponse>
        where TResponse : notnull 
    {
    }
}
