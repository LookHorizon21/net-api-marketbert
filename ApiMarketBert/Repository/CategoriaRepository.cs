using ApiMarketBert.Data;
using ApiMarketBert.Models;
using ApiMarketBert.Repository.IRepository;

namespace ApiMarketBert.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _bd;

        public CategoriaRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarCategoria(CategoriaMd categoria)
        {
            //PATCH
            var categoriaExistente = _bd.Categorias.Find(categoria.Id);
            if (categoriaExistente != null)
            {
                _bd.Entry(categoriaExistente).CurrentValues.SetValues(categoria);
            }
            else
            {
                _bd.Categorias.Update(categoria);
            }

            return Guardar();
        }

        public bool BorrarCategoria(CategoriaMd categoria)
        {
            _bd.Categorias.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(CategoriaMd categoria)
        {
            _bd.Categorias.Add(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(int id)
        {
            return _bd.Categorias.Any(c => c.Id == id);
        }

        public bool ExisteCategoria(string nombre)
        {
            bool valor = _bd.Categorias.Any(c => c.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return valor;
        }

        public CategoriaMd GetCategoria(int CategoriaId)
        {
            return _bd.Categorias.FirstOrDefault(c => c.Id == CategoriaId);
        }

        public ICollection<CategoriaMd> GetCategorias()
        {
            return _bd.Categorias.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
