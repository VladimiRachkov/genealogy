using System;
using Genealogy.Repository.Concrete;

namespace Genealogy.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        PersonRepository PersonRepository { get; }
        CemeteryRepository CemeteryRepository { get; }
        PageRepository PageRepository { get; }
        void Save();
    }
}