using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ApiMapped.Data.Repositories;
using ApiMapped.Domain.Entities;
using ApiMapped.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiMapped.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase, IDisposable
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;

        private Stopwatch execWatch;

        public ProductsController(ILogger<ProductsController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

            execWatch = new Stopwatch();
            execWatch.Start();
        }

        [HttpGet("simple")]
        public IActionResult GetSimple()
        {
            var repository = new ProductRepository();
            var modelData = repository.GetAll();

            var entities = modelData.Select(mdl => new Product(
                id: mdl.Id,
                productName: mdl.ProductName,
                category: mdl.Category,
                prices: mdl.Prices,
                codeBar: mdl.Ean13
            ));

            var resultViewModels = entities.Select(entity => new ProductViewModel(
                id: entity.Id,
                productName: entity.ProductName,
                category: entity.Category.CategoryName,
                price: entity.Prices.Last().Price,
                codeBar: entity.CodeBar
            )).ToList();

            return Ok(new
            {
                Result = GetResult(resultViewModels.Count()),
                // Data = resultViewModels,
            });
        }

        [HttpGet("implicit-operator")]
        public IActionResult GetImplicitOperator()
        {
            var repository = new ProductRepository();
            var modelData = repository.GetAll();

            var entities = modelData.Select(mdl => (Product)mdl);

            var resultViewModels = entities.Select(entity => (ProductViewModel)entity).ToList();

            return Ok(new
            {
                Result = GetResult(resultViewModels.Count()),
                // Data = resultViewModels,
            });
        }

        [HttpGet("automapper-1")]
        public IActionResult GetAutoMapper1()
        {
            var repository = new ProductRepository();
            var modelData = repository.GetAll();

            var entities = _mapper.Map<IEnumerable<Product>>(modelData);

            var resultViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(entities);

            return Ok(new
            {
                Result = GetResult(resultViewModels.Count()),
                // Data = resultViewModels,
            });
        }

        public object GetResult(int lines)
        {
            execWatch.Stop();

            return new
            {
                Lines = lines,
                ExecutionTime = $"{execWatch.ElapsedMilliseconds} ms",
                Memory = $"{Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024} mb"
            };
        }

        public void Dispose()
        {
            GC.Collect(0);
            GC.Collect(1);
            GC.Collect(2);
        }
    }
}
