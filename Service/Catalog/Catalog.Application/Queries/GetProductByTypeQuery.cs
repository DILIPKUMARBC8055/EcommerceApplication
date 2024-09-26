using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductByTypeQuery : IRequest<IList<ProductResponse>>
    {
        public GetProductByTypeQuery(string type)
        {
            Type = type;
        }

        public string Type { get; }
    }
}
