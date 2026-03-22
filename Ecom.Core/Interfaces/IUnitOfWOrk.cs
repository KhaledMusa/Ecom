using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWOrk
    {
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get;  }
        public IPhotoRepository PhotoRepository { get;  }
        IGenericRepositry<TEntity> Repositry<TEntity>() where TEntity : class;
        Task<int> Complete();
    }
}
