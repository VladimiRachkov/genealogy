using System;
using Genealogy.Models;

namespace Genealogy.Repository.Concrete
{
    public class UnitOfWork
    {
        private readonly GenealogyContext _GenealogyContext;

        public PersonRepository PersonRepository => personRepository ?? new PersonRepository(_GenealogyContext);
        private PersonRepository personRepository;
        public CemeteryRepository CemeteryRepository => cemeteryRepository ?? new CemeteryRepository(_GenealogyContext);
        private CemeteryRepository cemeteryRepository;

        public UnitOfWork(GenealogyContext GenealogyContext)
        {
            _GenealogyContext = GenealogyContext;
        }

        public void Save()
        {
            _GenealogyContext.SaveChanges();
        }

        #region Disposed
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _GenealogyContext.Dispose();
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