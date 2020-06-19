using System;
using System.Collections.Generic;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        List<PersonDto> GetPerson(PersonFilter filter);
        PersonDto AddPerson(PersonDto newPerson);
        PersonDto MarkAsRemovedPerson(Guid id);
        PersonDto ChangePerson(PersonDto personDto);
    }
}