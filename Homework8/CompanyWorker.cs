using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;

namespace Homework8
{
    public class CompanyWorker
    {
        #region свойства
        [DisplayName("Имя")]
        public string Name { get; set; }
        [DisplayName("Фамилия")]
        public string LastName { get; set; }
        [DisplayName("Возраст")]
        public string Age { get; set; }
        [DisplayName("Долность")]
        public string Position { get; set; }
        [DisplayName("Зарплата")]
        public string Salary { get; set; }

        #endregion

        #region поля

        private string name;

        private string lastName;

        private string age;

        private string position;

        private string salary;

        #endregion

        #region Конструктор

        public CompanyWorker(string Name, string LastName, string Age, string Position, string Salary)
        {
            this.Name = Name;
            this.LastName = LastName;
            this.Age = Age;
            this.Position = Position;
            this.Salary = Salary;
        }

        public CompanyWorker()
        {

        }

        #endregion

    }
}
