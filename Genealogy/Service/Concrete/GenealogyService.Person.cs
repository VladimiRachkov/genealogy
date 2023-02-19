using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Genealogy.Helpers;
using Genealogy.Models;
using Genealogy.Service.Astract;
using Genealogy.Service.Helpers;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public List<PersonDto> GetPerson(PersonFilter filter)
        {
            if (filter.Fio == null && filter.Id == Guid.Empty)
            {
                return null;
            }
            return GetAllPersons(filter);
        }

        public List<PersonDto> GetAllPersons(PersonFilter filter)
        {
            var count = _unitOfWork.PersonRepository.Count();

            var persons = _unitOfWork.PersonRepository.Get(
            x =>
                (filter.Id != Guid.Empty ? x.Id == filter.Id : true) &&
                (filter.Lastname != null ? x.Lastname == filter.Lastname : true) &&
                (filter.CemeteryId != Guid.Empty ? x.Cemetery.Id == filter.CemeteryId : true) &&
                (filter.CountyId != Guid.Empty ? x.Cemetery.CountyId == filter.CountyId : true),
            x =>
                x.OrderBy(item => item.Lastname)
                .ThenBy(item => item.Firstname)
                .ThenBy(item => item.Patronymic), "Cemetery,PersonGroup");

            if (!String.IsNullOrEmpty(filter.Fio))
            {
                var names = filter.Fio.Split(' ').Take(2);

                int[] scores = null;
                switch (names.Count())
                {
                    case 1:
                        scores = new int[7] { 1, 2, 3, 4, 5, 6, 7 };
                        break;
                    case 2:
                        scores = new int[5] { 3, 5, 6, 7, 8 };
                        break;
                    // case 3:
                    //     scores = new int[1] { 7 };
                    //     break;
                }

                persons = persons.Select(person =>
                {
                    bool hasFirstname = false, hasLastname = false;
                    //bool hasPatronymic = false;
                    var score = names.Select(item => item.ToLower()).Select(str =>
                    {
                        if (person.Firstname != null && person.Firstname.ToLower().Contains(str) && !hasFirstname)
                        {
                            hasFirstname = true;
                            return 2;
                        }
                        if (person.Lastname != null && person.Lastname.ToLower().Contains(str) && !hasLastname)
                        {
                            hasLastname = true;
                            return 4;
                        }
                        // if (person.Patronymic != null && person.Patronymic.ToLower().Contains(str) && !hasPatronymic && names.Count() == 3)
                        // {
                        //     hasPatronymic = true;
                        //     return 1;
                        // }
                        return 0;
                    }).Sum();
                    return Tuple.Create(person, score);
                })
                .Where(t => t.Item2 > 0 && Array.IndexOf(scores, t.Item2) > (-1))
                .OrderByDescending(x => x.Item2)
                .Select(x => x.Item1);
            }

            if (filter.Step > 0)
            {
                persons = persons.Where((item, index) => index >= filter.Step * filter.Index && index < (filter.Step * filter.Index) + filter.Step);
            }

            return persons.Select(i => _mapper.Map<PersonDto>(i)).ToList();
        }

        public PersonDto AddPerson(PersonDto newPerson)
        {
            if (newPerson != null)
            {
                var person = _mapper.Map<Person>(newPerson);
                var id = Guid.NewGuid();

                person.Id = id;
                person.Cemetery = _unitOfWork.CemeteryRepository.GetByID(newPerson.CemeteryId);

                _unitOfWork.PersonRepository.Add(person);
                _unitOfWork.Save();

                var result = _unitOfWork.PersonRepository.GetByID(id);
                return _mapper.Map<PersonDto>(result);
            }
            return null;
        }

        public PersonDto ChangePerson(PersonDto personDto)
        {
            if (personDto != null && personDto.Id != null)
            {
                PersonDto result;
                //TODO: Сделать проверку всех свойств на наличие изменений
                var changedPerson = _mapper.Map<Person>(personDto);
                //changedPerson.Cemetery = _unitOfWork.CemeteryRepository.GetByID(personDto.CemeteryId);

                var person = _unitOfWork.PersonRepository.GetByID(personDto.Id);

                if (personDto.isRemoved.Value && person.isRemoved)
                {
                    result = RemovePerson(person) ? personDto : null;
                }
                else
                {
                    ObjectValues.CopyValues(person, changedPerson);
                    person = UpdatePerson(person);
                    result = _mapper.Map<PersonDto>(person);
                }

                if (result != null)
                {
                    return _mapper.Map<PersonDto>(result);
                }
            }
            return null;
        }

        public CountOutDto GetPersonsCount()
        {
            var result = new CountOutDto();
            result.Count = _unitOfWork.PersonRepository.Count();
            return result;
        }

        private Person UpdatePerson(Person person)
        {
            _unitOfWork.PersonRepository.Update(person);
            _unitOfWork.Save();

            return _unitOfWork.PersonRepository.GetByID(person.Id);
        }

        private bool RemovePerson(Person person)
        {
            _unitOfWork.PersonRepository.Delete(person);
            _unitOfWork.Save();

            return true;
        }

        private IEnumerable<Person> getPersonByCemeteryId(Guid cemeteryId)
        {
            return _unitOfWork.PersonRepository.Get(x => (cemeteryId != Guid.Empty ? x.Cemetery.Id == cemeteryId : true));
        }

        private void updatePersons(IEnumerable<Person> persons)
        {
            persons.ToList().ForEach(person => _unitOfWork.PersonRepository.Update(person));
            _unitOfWork.Save();
        }

        private void removePersonsByCemeteryId(Guid cemeteryId)
        {
            var persons = getPersonByCemeteryId(cemeteryId);
            persons.ToList().ForEach(person => _unitOfWork.PersonRepository.Delete(person));
            _unitOfWork.Save();
        }
    }
}