using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWOrk
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManageService _imageManageService;   
        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository PhotoRepository { get; }
        public UnitOfWork(AppDbContext context, IImageManageService imageManageService, IMapper mapper)
        {
            _context = context;
            _imageManageService = imageManageService;
            _mapper = mapper;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, _mapper, _imageManageService);
            PhotoRepository = new PhotoRepository(_context);
            
        }
    }
}
