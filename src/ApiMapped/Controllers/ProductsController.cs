using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ApiMapped.Data.Repositories;
using ApiMapped.Domain.Entities;
using ApiMapped.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            var modelData = repository.GetByAleatoryFilter();

            execWatch = new Stopwatch();
            execWatch.Start();
            var entities = modelData.Select(mdl => new Product(
                id: mdl.Id,
                productName: mdl.ProductName,
                category: new Category(mdl.Category.Id, mdl.Category.CategoryName),
                prices: mdl.Prices,
                codeBar: mdl.Ean13
            ));
            var ModelToEntity = GetResult(modelData.Count());

            execWatch = new Stopwatch();
            execWatch.Start();
            var resultViewModels = entities.Select(entity => new ProductViewModel(
                id: entity.Id,
                productName: entity.ProductName,
                category: entity.Category.CategoryName,
                price: entity.Prices.Last().Price,
                codeBar: entity.CodeBar,
                upperPrice: entity.UpperPrice.Price,
                lowerPrice: entity.LowerPrice.Price
            )).ToList();
            var EntityToViewModel = GetResult(resultViewModels.Count());

            return Ok(new
            {
                From = "Simple",
                ModelToEntity,
                EntityToViewModel,
                Data = resultViewModels,
            });
        }

        [HttpGet("implicit-operator")]
        public IActionResult GetImplicitOperator()
        {
            var repository = new ProductRepository();
            var modelData = repository.GetByAleatoryFilter();

            execWatch = new Stopwatch();
            execWatch.Start();
            var entities = modelData.Select(mdl => (Product)mdl);
            var ModelToEntity = GetResult(entities.Count());

            execWatch = new Stopwatch();
            execWatch.Start();
            var resultViewModels = entities.Select(entity => (ProductViewModel)entity).ToList();
            var EntityToViewModel = GetResult(entities.Count());

            return Ok(new
            {
                From = "Implicit operator",
                ModelToEntity,
                EntityToViewModel,
                Data = resultViewModels,
            });
        }

        [HttpGet("automapper-1")]
        public IActionResult GetAutoMapper1()
        {
            var repository = new ProductRepository();
            var modelData = repository.GetByAleatoryFilter();

            execWatch = new Stopwatch();
            execWatch.Start();
            var entities = _mapper.Map<IEnumerable<Product>>(modelData);
            var ModelToEntity = GetResult(entities.Count());

            execWatch = new Stopwatch();
            execWatch.Start();
            var resultViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(entities);
            var EntityToViewModel = GetResult(entities.Count());

            return Ok(new
            {
                From = "AutoMapper 1",
                ModelToEntity,
                EntityToViewModel,
                Data = resultViewModels,
            });
        }

        [HttpGet("automapper-2")]
        public IActionResult GetAutoMapper2()
        {
            var repository = new ProductRepository();
            var modelData = repository.GetByAleatoryFilterToMapper();

            execWatch = new Stopwatch();
            execWatch.Start();
            var entities = modelData.ProjectTo<Product>(_mapper.ConfigurationProvider).ToList();
            var ModelToEntity = GetResult(entities.Count());

            execWatch = new Stopwatch();
            execWatch.Start();
            var resultViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(entities);
            var EntityToViewModel = GetResult(entities.Count());

            return Ok(new
            {
                From = "AutoMapper 2",
                ModelToEntity,
                EntityToViewModel,
                Data = resultViewModels,
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
