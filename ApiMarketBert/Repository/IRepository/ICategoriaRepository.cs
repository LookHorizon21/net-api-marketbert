using ApiMarketBert.Models;

namespace ApiMarketBert.Repository.IRepository
{
    public interface ICategoriaRepository
    {
        ICollection<CategoriaMd> GetCategorias();
        CategoriaMd GetCategoria(int CategoriaId);
        bool ExisteCategoria(int id);
        bool ExisteCategoria(string nombre);

        bool CrearCategoria(CategoriaMd categoria);
        bool ActualizarCategoria(CategoriaMd categoria);
        bool BorrarCategoria(CategoriaMd categoria);
        bool Guardar();
    }
}
