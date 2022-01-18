using AutoMapper;
using Onnorokom.ShoppingCart.Membership.BusinessObjects;
using Onnorokom.ShoppingCart.Membership.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Onnorokom.ShoppingCart.Membership.Services
{
    public class StockService : IStockService
    {
        private readonly IShoppingCartUnitOfWork _shoppingCartUnitOfWork;
        private readonly IMapper _mapper;

        public StockService(IShoppingCartUnitOfWork shoppingCartUnitOfWork, IMapper mapper)
        {
            _shoppingCartUnitOfWork = shoppingCartUnitOfWork;
            _mapper = mapper;
        }

        public void Create(Stock stock)
        {
            if (stock == null)
                throw new InvalidOperationException("Stock is not provided");

            _shoppingCartUnitOfWork.Stocks.Add(
                _mapper.Map<Entities.Stock>(stock));

            _shoppingCartUnitOfWork.Save();

        }

        public Stock GetStock(int id)
        {
            var stockentity = _shoppingCartUnitOfWork.Stocks.GetById(id);

            var productentity = _shoppingCartUnitOfWork.Products.GetById(stockentity.ProductId);

            var product = _mapper.Map<Stock>(stockentity);

            product.ProductName = productentity.Name;

            return product;
        }

        public (IList<Stock> records, int total, int totalDisplay) Stocks(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var stockData = _shoppingCartUnitOfWork.Stocks.GetAll();

            var stocks = (from stock in stockData
                          select _mapper.Map<Stock>(stock)).ToList();

            foreach (var stock in stocks)
            {
                var product = _shoppingCartUnitOfWork.Products.GetById(stock.ProductId);
                stock.ProductName = product.Name;
            }

            if(string.IsNullOrWhiteSpace(searchText) == false)
                  stocks = (from stock in stocks where
                          stock.ProductName == searchText
                          select stock).ToList();

            var data =new List<Stock>();

            if (sortText == null)
                data = stocks.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            else
                data = stocks.AsQueryable().OrderBy(sortText).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return (data, stocks.Count, stockData.Count);
        }

        public void Update(Stock stock)
        {
            if (stock == null)
                throw new InvalidOperationException("Stock is not provided");

            var product = _shoppingCartUnitOfWork.Products.Get(x => x.Name == stock.ProductName, string.Empty);

            var stockentity = _shoppingCartUnitOfWork.Stocks.GetById(stock.Id);

            stock.ProductId = product[0].Id;
            if (stockentity != null)
            {
                stockentity.ProductId = stock.ProductId;
                stockentity.Quantity = stock.Quantity;

                _shoppingCartUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Stock is not found");
        }
    }
}
