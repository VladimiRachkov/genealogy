using System;
using System.Collections.Generic;
using System.Linq;
using Genealogy.Models;
using Genealogy.Service.Astract;
using Genealogy.Service.Helpers;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public List<PersonDto> GetPerson(PersonFilter filter)
        {
            var persons = _unitOfWork.PersonRepository.Get(x =>
             (filter.Id != Guid.Empty ? x.Id == filter.Id : true) &&
             (filter.Lastname != null ? x.Lastname == filter.Lastname : true), null, "Cemetery");

            if (filter.Fio != null)
            {
                var names = filter.Fio.Split(' ').ToList();

                int[] scores = null;
                switch (names.Count())
                {
                    case 1:
                        scores = new int[7] { 1, 2, 3, 4, 5, 6, 7 };
                        break;
                    case 2:
                        scores = new int[5] { 3, 5, 6, 7, 8 };
                        break;
                    case 3:
                        scores = new int[1] { 7 };
                        break;
                }

                persons = persons.Select(person =>
                {
                    bool hasFirstname = false, hasLastname = false, hasPatronymic = false;
                    var score = names.Select(item => item.ToLower()).Select(str =>
                    {
                        if (person.Firstname.ToLower().Contains(str) && !hasFirstname)
                        {
                            hasFirstname = true;
                            return 2;
                        }
                        if (person.Lastname.ToLower().Contains(str) && !hasLastname)
                        {
                            hasLastname = true;
                            return 4;
                        }
                        if (person.Patronymic.ToLower().Contains(str) && !hasPatronymic)
                        {
                            hasPatronymic = true;
                            return 1;
                        }
                        return 0;
                    }).Sum();
                    return Tuple.Create(person, score);
                })
                .Where(t => t.Item2 > 0 && Array.IndexOf(scores, t.Item2) > (-1))
                .OrderByDescending(x => x.Item2)
                .Select(x => x.Item1);
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

        public PersonDto MarkAsRemovedPerson(Guid id)
        {
            if (id != Guid.Empty)
            {
                var person = _unitOfWork.PersonRepository.GetByID(id);
                if (person != null)
                {
                    person.Removed = true;
                    var updatedPerson = UpdatePerson(person);
                    return _mapper.Map<PersonDto>(updatedPerson);
                }
                return null;
            }
            return null;
        }

        public PersonDto ChangePerson(PersonDto personDto)
        {
            if (personDto != null && personDto.Id != null)
            {
                var person = _mapper.Map<Person>(personDto);
                person.Cemetery = _unitOfWork.CemeteryRepository.GetByID(personDto.CemeteryId);

                var result = UpdatePerson(person);
                return _mapper.Map<PersonDto>(result);
            }
            return null;
        }

        private Person UpdatePerson(Person person)
        {
            _unitOfWork.PersonRepository.Update(person);
            _unitOfWork.Save();
            return _unitOfWork.PersonRepository.GetByID(person.Id);
        }
    }
}