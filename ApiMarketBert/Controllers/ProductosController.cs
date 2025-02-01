using ApiMarketBert.Models;
using ApiMarketBert.Models.Dtos;
using ApiMarketBert.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiMarketBert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ICategoriaRepository _catRepo;
        private readonly IProductoRepository _prodRepo;        
        private readonly IMapper _mapper;

        public ProductosController(ICategoriaRepository catRepo, IProductoRepository prodRepo, IMapper mapper)
        {
            _catRepo = catRepo;
            _prodRepo = prodRepo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProductos()
        {
            var listaProductos = _prodRepo.GetProductos();

            var listaProductosDto = new List<ProductoDto>();

            foreach (var lista in listaProductos)
            {
                listaProductosDto.Add(_mapper.Map<ProductoDto>(lista));
            }
            return Ok(listaProductosDto);
        }

        [AllowAnonymous]
        [HttpGet("{productoId:int}", Name = "GetProducto")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProducto(int productoId)
        {
            var itemProducto = _prodRepo.GetProducto(productoId);

            if (itemProducto == null)
            {
                return NotFound();
            }

            var itemProductoDto = _mapper.Map<ProductoDto>(itemProducto);

            return Ok(itemProductoDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProductoDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearProducto([FromBody] CrearProductoDto crearProductoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (crearProductoDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_prodRepo.ExisteProducto(crearProductoDto.Nombre))
            {
                ModelState.AddModelError("", "El producto ya existe");
                return StatusCode(404, ModelState);
            }

            var itemCategoria = _catRepo.GetCategoria(crearProductoDto.categoriaId);
            if (itemCategoria == null)
            {
                ModelState.AddModelError("", "La categoria no existe");
                return StatusCode(404, ModelState);
            }

            var producto = _mapper.Map<ProductoMd>(crearProductoDto);

            if (!_prodRepo.CrearProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro{producto.Nombre}");
                return StatusCode(404, ModelState);
            }

            return CreatedAtRoute("GetProducto", new { ProductoId = producto.Id }, producto);
        }

        [Authorize(Roles = "admin")]
        [HttpPatch("{productoId:int}", Name = "ActualizarPatchProducto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult ActualizarPatchProducto(int productoId, [FromBody] ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productoDto == null || productoId != productoDto.Id)
            {
                return BadRequest(ModelState);
            }

            var productoExistente = _prodRepo.GetProducto(productoId);
            if (productoExistente == null)
            {
                return NotFound($"No se encontro el producto con ID {productoId}");
            }

            var producto = _mapper.Map<ProductoMd>(productoDto);

            if (!_prodRepo.ActualizarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salio mal actualizando el registro{producto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{productoId:int}", Name = "BorrarProducto")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarProducto(int productoId)
        {

            if (!_prodRepo.ExisteProducto(productoId))
            {
                return NotFound();
            }

            var producto = _prodRepo.GetProducto(productoId);

            if (!_prodRepo.BorrarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salio mal borrando el registro{producto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("GetProductosEnCategoria/{categoriaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProductosEnCategoria(int categoriaId)
        {
            var listaProductos = _prodRepo.GetProductosEnCategoria(categoriaId);

            if (listaProductos == null)
            {
                return NotFound();
            }

            var itemProducto = new List<ProductoDto>();
            foreach (var producto in listaProductos)
            {
                itemProducto.Add(_mapper.Map<ProductoDto>(producto));
            }

            return Ok(itemProducto);
        }

        [AllowAnonymous]
        [HttpGet("Buscar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Buscar(string nombre)
        {
            try
            {
                var resultado = _prodRepo.BuscarProducto(nombre);
                if (resultado.Any())
                {
                    return Ok(resultado);
                }

                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos de la aplicación");
            }
        }
    }
}
