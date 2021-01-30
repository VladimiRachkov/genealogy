using System;
using System.Collections.Generic;
using Genealogy.Models;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        List<PersonDto> GetPerson(PersonFilter filter);
        List<PersonDto> GetAllPersons(PersonFilter filter);
        PersonDto AddPerson(PersonDto newPerson);
        PersonDto ChangePerson(PersonDto personDto);
        CountOutDto GetPersonsCount();
    }
}