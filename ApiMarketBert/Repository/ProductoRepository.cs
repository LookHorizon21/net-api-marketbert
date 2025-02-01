using ApiMarketBert.Data;
using ApiMarketBert.Models;
using ApiMarketBert.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiMarketBert.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _bd;

        public ProductoRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarProducto(ProductoMd producto)
        {
            producto.FechaModificacion = DateTime.Now;

            //PATCH
            var productoExistente = _bd.Productos.Find(producto.Id);
            if (productoExistente != null)
            {
                _bd.Entry(productoExistente).CurrentValues.SetValues(producto);
            }
            else
            {
                _bd.Productos.Update(producto);
            }

            return Guardar();
        }


        public bool BorrarProducto(ProductoMd producto)
        {
            _bd.Productos.Remove(producto);
            return Guardar();
        }

        public IEnumerable<ProductoMd> BuscarProducto(string nombre)
        {
            IQueryable<ProductoMd> query = _bd.Productos;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Nombre.Contains(nombre) || e.Descripcion.Contains(nombre));
            }
            return query.ToList();
        }


        public bool CrearProducto(ProductoMd producto)
        {
            producto.FechaCreacion = DateTime.Now;

            _bd.Productos.Add(producto);

            return Guardar();
        }

        public bool ExisteProducto(int id)
        {
            return _bd.Productos.Any(c => c.Id == id);
        }

        public bool ExisteProducto(string nombre)
        {
            bool valor = _bd.Productos.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public ProductoMd GetProducto(int productoId)
        {
            return _bd.Productos.FirstOrDefault(c => c.Id == productoId);
        }

        public ICollection<ProductoMd> GetProductos()
        {
            return _bd.Productos.OrderBy(c => c.Nombre).ToList();
        }

        public ICollection<ProductoMd> GetProductosEnCategoria(int catId)
        {
            return _bd.Productos.Include(ca => ca.Categoria).Where(ca => ca.categoriaId == catId).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
