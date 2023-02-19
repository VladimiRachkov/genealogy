using System.Threading.Tasks;
using Genealogy.Service.Astract;
using MimeKit;
using MailKit.Net.Smtp;
using Genealogy.Models;
using Genealogy.Data;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Logging;
using DocumentFormat.OpenXml.Packaging;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Genealogy.Service.Concrete
{

    class DateItem
    {
        public int Index { get; set; }
        public string Date { get; set; }
    }

    class PersonItem
    {
        public DateItem StartDate { get; set; }
        public DateItem FinishDate { get; set; }
    }
    public partial class GenealogyService : IGenealogyService
    {
        private string readAsList(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.Append(reader.ReadLine());
            }
            return result.ToString();
        }

        public Response AddFile(IFormFile uploadedFile)
        {
            int count = 0;
            Regex dateReg = new Regex(@"^\d{0,2}\?{0,2}.\d{0,2}\?{0,2}.\d{0,4}\?{0,4}\d{0,3}-\d{0,2}\?{0,2}.\d{0,2}\?{0,2}.\d{0,4}\?{0,4}\d{0,3}$", RegexOptions.IgnorePatternWhitespace);
            Regex nameReg = new Regex(@"[^0-9]$", RegexOptions.IgnorePatternWhitespace);

            try
            {
                if (uploadedFile != null)
                {
                    using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(uploadedFile.OpenReadStream(), false))
                    {
                        DocumentFormat.OpenXml.Wordprocessing.Body body = wordDoc.MainDocumentPart.Document.Body;

                        var rows = body.ChildElements.Where(item => !String.IsNullOrEmpty(item.InnerText.Trim())).Select(item => item.InnerText.Trim()).ToList();

                        // Получение названия уезда и обработка
                        var countyName = rows.FirstOrDefault();
                        //if (countyName == null) {

                        //};

                        if (!String.IsNullOrEmpty(countyName))
                        {
                            rows.RemoveAt(0);
                        }

                        // Ищем существующий уезд
                        var county = _unitOfWork.CountyRepository.Get(x => (x.Name == countyName)).FirstOrDefault();
                        if (county == null) {
                            // Если нет, то добавляем новый
                            county = addCounty(countyName);
                        }

                        // Получение название кладбища и обработка
                        var cemeteryName = rows.FirstOrDefault();

                        var cemetery = new Cemetery()
                        {
                            Id = Guid.NewGuid(),
                            Name = cemeteryName,
                            CountyId = county.Id,
                            isRemoved = false
                        };

                        _unitOfWork.CemeteryRepository.Add(cemetery);

                        if (!String.IsNullOrEmpty(cemeteryName))
                        {
                            rows.RemoveAt(0);
                        }

                        var rowCount = 0;
                        var persons = new List<Person>();

                        foreach (var item in rows)
                        {
                            rowCount++;
                            PersonGroup personGroup = null;
                            var personData = item.Split(' ').Select(x => x.Trim()).Where(x => x.Count() > 1).ToList();

                            if (personData.Count > 4)
                            {
                                personGroup = CreatePersonGroup();
                            }

                            while (personData.Any())
                            {
                                var arr = new List<string>();

                                personData.ForEach(x => arr.Add(new String(x)));
                                var names = new List<string>();
                                var dates = new List<string>();

                                /** Поиск ФИО */
                                foreach (var x in arr)
                                {
                                    if (nameReg.Match(x).Success && names.Count() < 3)
                                    {
                                        var name = personData.ElementAtOrDefault(0);
                                        if (!String.IsNullOrEmpty(name))
                                        {
                                            names.Add(x);
                                            personData.RemoveAt(0);
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                arr.RemoveAll(x => true);
                                personData.ForEach(x => arr.Add(new String(x)));

                                /** Поиск дат */
                                foreach (var element in arr)
                                {
                                    if (dateReg.Match(element).Success && dates.Count() < 2)
                                    {
                                        var dateArr = element.Split('-');
                                        dates.Add(dateArr[0]);
                                        dates.Add(dateArr[1]);

                                        personData.RemoveAt(0);
                                    }
                                }

                                var person = new Person()
                                {
                                    Lastname = names.ElementAtOrDefault(0),
                                    Firstname = names.ElementAtOrDefault(1),
                                    Patronymic = names.ElementAtOrDefault(2),
                                    StartDate = dates.ElementAtOrDefault(0),
                                    FinishDate = dates.ElementAtOrDefault(1),
                                    Cemetery = cemetery,
                                    PersonGroup = personGroup
                                };

                                if (person.Lastname == null)
                                {
                                    return new Response() { Message = $"Ошибка в {rowCount} строке.", Result = "Danger" };
                                }

                                persons.Add(person);
                                _unitOfWork.PersonRepository.Add(person);
                            }
                        }

                        _unitOfWork.Save();
                        count = persons.Count();
                    }
                }
            }

            catch (ApplicationException e)
            {
                _logger.LogError(e.ToString());
                throw e;
            }

            return new Response() { Message = $"Добавлено {count} записей.", Result = "Success" };
        }

        private string formatDate(string src)
        {
            return src.Split('.').Select(item => Int16.Parse(item)).Select(item => item.ToString("D2")).Aggregate((current, next) => current + "." + next);
        }
    }
}