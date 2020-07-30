using System;
using Genealogy.Models;
using Genealogy.Repository.Abstract;

namespace Genealogy.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GenealogyContext _genealogyContext;
        public PersonRepository PersonRepository => personRepository ?? new PersonRepository(_genealogyContext);
        private PersonRepository personRepository;
        public CemeteryRepository CemeteryRepository => cemeteryRepository ?? new CemeteryRepository(_genealogyContext);
        private CemeteryRepository cemeteryRepository;
        public PageRepository PageRepository => pageRepository ?? new PageRepository(_genealogyContext);
        private PageRepository pageRepository;
        public LinkRepository LinkRepository => linkRepository ?? new LinkRepository(_genealogyContext);
        private LinkRepository linkRepository;

        public UnitOfWork(GenealogyContext genealogyContext)
        {
            _genealogyContext = genealogyContext;
        }

        public void Save()
        {
            _genealogyContext.SaveChanges();
        }

        #region Disposed
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _genealogyContext.Dispose();
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