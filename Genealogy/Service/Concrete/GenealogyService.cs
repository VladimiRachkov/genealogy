using System;
using AutoMapper;
using Genealogy.Repository.Abstract;
using Genealogy.Service.Astract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<IGenealogyService> _logger;

        public GenealogyService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper, ILogger<IGenealogyService> logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        #region IDisposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}