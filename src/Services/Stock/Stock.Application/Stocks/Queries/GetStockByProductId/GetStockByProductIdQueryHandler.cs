using AutoMapper;
using MediatR;
using Stock.Application.Stocks.Queries.Models;
using Stock.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stock.Application.Stocks.Queries.GetStockByProductId
{
    public class GetStockByProductIdQueryHandler : IRequestHandler<GetStockByProductIdQuery, StockDto>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public GetStockByProductIdQueryHandler(IStockRepository stockRepository,
            IMapper mapper)
        {
            this._stockRepository = stockRepository;
            this._mapper = mapper;
        }

        public async Task<StockDto> Handle(GetStockByProductIdQuery request, CancellationToken cancellationToken)
        {
            var stock = await this._stockRepository.GetByProductId(request.ProductId);

            if (stock == null)
                throw new Exception("Stock not found.");

            return this._mapper.Map<StockDto>(stock);
        }
    }
}
