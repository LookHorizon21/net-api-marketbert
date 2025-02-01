using ApiMarketBert.Models;

namespace ApiMarketBert.Repository.IRepository
{
    public interface IProductoRepository
    {
        ICollection<ProductoMd> GetProductos();
        ICollection<ProductoMd> GetProductosEnCategoria(int catId);
        IEnumerable<ProductoMd> BuscarProducto(string nombre);
        ProductoMd GetProducto(int ProductoId);
        bool ExisteProducto(int id);
        bool ExisteProducto(string nombre);

        bool CrearProducto(ProductoMd Producto);
        bool ActualizarProducto(ProductoMd Producto);
        bool BorrarProducto(ProductoMd Producto);
        bool Guardar();
    }
}
