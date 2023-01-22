using System;
using Genealogy.Repository.Concrete;

namespace Genealogy.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        PersonRepository PersonRepository { get; }
        CemeteryRepository CemeteryRepository { get; }
        PageRepository PageRepository { get; }
        LinkRepository LinkRepository { get; }
        UserRepository UserRepository { get; }
        RoleRepository RoleRepository { get; }
        PersonGroupRepository PersonGroupRepository { get; }
        BusinessObjectRepository BusinessObjectRepository { get; }
        MetatypeRepository MetatypeRepository { get; }
        CountyRepository CountyRepository { get; }
        void Save();
    }
}