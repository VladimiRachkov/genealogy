
using System;

namespace Genealogy.Models
{
    public class Person
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        /// <value></value>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        /// <value></value>
        public string Firstname { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        /// <value></value>
        public string Lastname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        /// <value></value>
        public string Patronymic { get; set; }

        /// <summary>
        /// Место захоронения
        /// </summary>
        /// <value></value>
        public Cemetery Cemetery { get; set; }

        /// <summary>
        /// Источник информации
        /// </summary>
        /// <value></value>
        public string Source { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        /// <value></value>
        public string StartDate { get; set; }

        /// <summary>
        /// Дата смерти
        /// </summary>
        /// <value></value>
        public string FinishDate { get; set; }

        /// <summary>
        /// Флаг удаления
        /// </summary>
        /// <value></value>
        public bool isRemoved { get; set; }

        /// <summary>
        /// Группа
        /// </summary>
        /// <value></value>
        public PersonGroup PersonGroup { get; set; }

        /// <summary>
        /// Дополнительные сведения
        /// </summary>
        /// <value></value>
        public string Comment { get; set; }
    }
}