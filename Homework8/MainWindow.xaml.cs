using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Reflection;

namespace Homework8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        #region Поля
        public delegate void test();
        // Коллекция департаментов
        List<string> DeptList = new List<string>()
        {
            "Инфраструктуры IT",
            "транспорта",
            "Связи",
            "Оптовых продаж",
            "Логистики",
            "Контроля качества"
        };

        #endregion

        #region Главное окно
        public static MainWindow Self { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            // Указание доступности элементов управления главной формы
            Self = this;

            // Заполнение списка департаментов
            cmbDept.ItemsSource = DeptList;

            //tableDeptWorker.ItemsSource = Methods.workers;
        }
        #endregion
            
        #region События 

        /// <summary>
        /// Событие нажатия кнопки "Добавить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            // Вызов метода добавления работников в таблицу
            Methods.AddWorkers();

            // Запись добавленных строк в файл департамента
            tableDeptWorker.ItemsSource = Methods.workers;
        }
      
        /// <summary>
        /// Событие нажатия кнопки "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Вызов метода сохранения файла
            Methods.SaveFile();
        }

        /// <summary>
        /// Событие нажатия кнопки "Открыть"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            // Вызов метода открытия xml файла департамента
            Methods.Conditions();
        }
       
        /// <summary>
        /// Событие нажатия кнопки "Сортировка"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSort_Click(object sender, RoutedEventArgs e)
        {
            // Вызов метода сортировки
            //Methods.SortTable();

            //tableDeptWorker.ItemsSource = Methods.workers;
        }

        /// <summary>
        /// Событие нажатия кнопки "Загрузить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            // Вызов метода загрузки файла
            Methods.LoadFile();
        }
        
        /// <summary>
        /// Условие переопределения коллекции wirker в соответствии с сортировкой tableDeptWorker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TableDeptWorker_Sorting(object sender, DataGridSortingEventArgs e)
        {
            // Переменная получает направление сортировки
            var test = e.Column.SortDirection;
            
            // Цикл сортировки по имени столбца
            switch (e.Column.Header.ToString())
            {
                #region Условия переопределения коллекции
                case "Имя":
                    Methods.SortTable(nameof(CompanyWorker.Name), test);
                    break;
                case "Фамилия":
                    Methods.SortTable(nameof(CompanyWorker.LastName), test);
                    break;
                case "Возраст":
                    Methods.SortTable(nameof(CompanyWorker.Age), test);
                    break;
                case "Должность":
                    Methods.SortTable(nameof(CompanyWorker.Position), test);
                    break;
                case "Зарплата":
                    Methods.SortTable(nameof(CompanyWorker.Salary), test);
                    break;
                #endregion
            }                       
        }
        #endregion

        #region неспользуемое событие
        /// <summary>
        /// Присваивание имени столбцам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void TableDeptWorker_AutoGeneratingColumns(object sender, EventArgs e)
        //{
        //    tableDeptWorker.Columns[0].Header = Methods.ColumnName(nameof(CompanyWorker.Name));
        //}
        #endregion
    }

}
