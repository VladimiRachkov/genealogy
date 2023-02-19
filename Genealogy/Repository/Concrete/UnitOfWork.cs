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

        public UserRepository UserRepository => userRepository ?? new UserRepository(_genealogyContext);
        private UserRepository userRepository;

        public RoleRepository RoleRepository => roleRepository ?? new RoleRepository(_genealogyContext);
        private RoleRepository roleRepository;

        public PersonGroupRepository PersonGroupRepository => personGroupRepository ?? new PersonGroupRepository(_genealogyContext);
        private PersonGroupRepository personGroupRepository;

        public BusinessObjectRepository BusinessObjectRepository => businessObjectRepository ?? new BusinessObjectRepository(_genealogyContext);
        private BusinessObjectRepository businessObjectRepository;

        public MetatypeRepository MetatypeRepository => metatypeRepository ?? new MetatypeRepository(_genealogyContext);
        private MetatypeRepository metatypeRepository;

        public CountyRepository CountyRepository => countyRepository ?? new CountyRepository(_genealogyContext);
        private CountyRepository countyRepository;

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