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
            dynamic ModelToEntity;
            dynamic EntityToViewModel;
            IEnumerable<ProductViewModel> resultViewModels;

            using (var repository = new ProductRepository())
            {
                var modelData = repository.GetByAleatoryFilter();

                execWatch = new Stopwatch();
                execWatch.Start();
                var entities = modelData.Select(mdl => new Product(
                    id: mdl.Id,
                    productName: mdl.ProductName,
                    category: new Category(mdl.Category.Id, mdl.Category.CategoryName),
                    prices: mdl.Prices,
                    codeBar: mdl.Ean13
                )).ToList();
                ModelToEntity = GetResult(modelData.Count());

                execWatch = new Stopwatch();
                execWatch.Start();
                resultViewModels = entities.Select(entity => new ProductViewModel(
                   id: entity.Id,
                   productName: entity.ProductName,
                   category: entity.Category.CategoryName,
                   price: entity.Prices.Last().Price,
                   codeBar: entity.CodeBar,
                   upperPrice: entity.UpperPrice.Price,
                   lowerPrice: entity.LowerPrice.Price
                )).ToList();
                EntityToViewModel = GetResult(resultViewModels.Count());

                entities = null;
            }

            return Ok(new
            {
                From = "Simple",
                ModelToEntity,
                EntityToViewModel,
                Data = resultViewModels.Take(5),
            });
        }

        [HttpGet("implicit-operator")]
        public IActionResult GetImplicitOperator()
        {
            dynamic ModelToEntity;
            dynamic EntityToViewModel;
            IEnumerable<ProductViewModel> resultViewModels;

            using (var repository = new ProductRepository())
            {
                var modelData = repository.GetByAleatoryFilter();

                execWatch = new Stopwatch();
                execWatch.Start();
                var entities = modelData.Select(mdl => (Product)mdl).ToList();
                ModelToEntity = GetResult(entities.Count());

                execWatch = new Stopwatch();
                execWatch.Start();
                resultViewModels = entities.Select(entity => (ProductViewModel)entity).ToList();
                EntityToViewModel = GetResult(resultViewModels.Count());

                entities = null;
            }

            return Ok(new
            {
                From = "Implicit operator",
                ModelToEntity,
                EntityToViewModel,
                Data = resultViewModels.Take(5),
            });
        }

        [HttpGet("automapper-1")]
        public IActionResult GetAutoMapper1()
        {
            dynamic ModelToEntity;
            dynamic EntityToViewModel;
            IEnumerable<ProductViewModel> resultViewModels;

            using (var repository = new ProductRepository())
            {
                var modelData = repository.GetByAleatoryFilter();

                execWatch = new Stopwatch();
                execWatch.Start();
                var entities = _mapper.Map<IEnumerable<Product>>(modelData);
                ModelToEntity = GetResult(entities.Count());


                execWatch = new Stopwatch();
                execWatch.Start();
                resultViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(entities);
                EntityToViewModel = GetResult(resultViewModels.Count());

                entities = null;
            }

            return Ok(new
            {
                From = "AutoMapper 1",
                ModelToEntity,
                EntityToViewModel,
                Data = resultViewModels.Take(5),
            });
        }

        [HttpGet("automapper-2")]
        public IActionResult GetAutoMapper2()
        {
            dynamic ModelToEntity;
            dynamic EntityToViewModel;
            IEnumerable<ProductViewModel> resultViewModels;

            try
            {
                using (var repository = new ProductRepository())
                {
                    var modelData = repository.GetByAleatoryFilterToMapper();

                    execWatch = new Stopwatch();
                    execWatch.Start();
                    var entities = modelData.ProjectTo<Product>(_mapper.ConfigurationProvider).ToList();
                    ModelToEntity = GetResult(entities.Count());

                    execWatch = new Stopwatch();
                    execWatch.Start();
                    resultViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(entities);
                    EntityToViewModel = GetResult(resultViewModels.Count());

                    entities = null;
                }

                return Ok(new
                {
                    From = "AutoMapper 2",
                    ModelToEntity,
                    EntityToViewModel,
                    Data = resultViewModels.Take(5),
                });
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
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
