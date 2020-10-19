using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Homework8
{
    class Methods
    {
        #region Переменные
        // Инициализация коллекции объектов CompanyWorker
        public static ObservableCollection<CompanyWorker> workers = new ObservableCollection<CompanyWorker>();

        // Ключи словаря соответствуют содержанию ComboBox cmbDept и указывают на место сохранения таблицы с данными
        public static Dictionary<string, string> DeptFilePath = new Dictionary<string, string>()
        {
            { "Инфраструктуры IT", @"it.xml" },
            { "транспорта", @"transport.xml" },
            { "Связи", @"communication.xml" },
            { "Оптовых продаж", @"sales.xml" },
            { "Логистики", @"logistic.xml" },
            { "Контроля качества", @"quality.xml" },
        };
       
        // Пустая переменная для пути к файлу
        static string filePath = string.Empty;

        #endregion

        #region Методы

        /// <summary>
        /// Метод добавления работников в таблицу
        /// </summary>
        public static void AddWorkers()
        {
            // Заполнение конструктора с данными по работникам
            CompanyWorker addWorker = new CompanyWorker(
                    MainWindow.Self.txtNameWorker.Text,
                    MainWindow.Self.txtLastName.Text,
                    MainWindow.Self.txtAge.Text,
                    MainWindow.Self.txtPosition.Text,
                    MainWindow.Self.txtSalary.Text
                    );

            // Добавление строки с данными в таблицу
            workers.Add(addWorker);
        }

        /// <summary>
        /// Метод сохраняет файл в зависимости от выбранного расширения файла. XML либо JSON
        /// </summary>
        public static void SaveFile()
        {
            // Инициализация диалога сохранения
            SaveFileDialog sfd = new SaveFileDialog();

            // Условие сохранения в XML
            if(MainWindow.Self.rdbXML.IsChecked == true)
            {
                // Фильтр расширения xml
                sfd.Filter = "XML-Файл (*.xml)|*.xml";

                // Условие задания пути к файлу
                if (sfd.ShowDialog() == true)
                {
                    // Путь к сохраняемому файлу
                    filePath = sfd.FileName;

                    // Инициализация сериализации 
                    var xml = new XmlSerializer(typeof(ObservableCollection<CompanyWorker>));

                    // Поток записи в файл
                    var xmlStr = new FileStream(filePath, FileMode.Create, FileAccess.Write);

                    // Сериализация в файл потока xmlStr
                    xml.Serialize(xmlStr, workers);

                    // Закрытие потока
                    xmlStr.Close();
                }
            }
            else if (MainWindow.Self.rdbJson.IsChecked == true)
            {
                // Фильтр расширения json
                sfd.Filter = "json-Файл (*.json)|*.json";
                // Условие задания пути к файлу
                if (sfd.ShowDialog() == true)
                {
                    // Путь к сохраняемому файлу
                    filePath = sfd.FileName;

                    // Сериализация в файл json
                    string json = JsonConvert.SerializeObject(workers);

                    // Запись в файл по выбранному пути
                    File.WriteAllText(filePath, json);
                }
            }
        }

        /// <summary>
        /// Метод считывает XML файл в соответствии с выбором combobox cmbDept
        /// </summary>
        public static void Conditions()
        {
            if (MainWindow.Self.cmbDept.SelectedItem != null)
            {
                // Условие проверяет наличие файла
                if (File.Exists(DeptFilePath[MainWindow.Self.cmbDept.Text]))
                {
                    // Инииализация сериализации
                    var xml = new XmlSerializer(typeof(ObservableCollection<CompanyWorker>));

                    // Поток чтения из файла
                    var xmlStr = new FileStream(DeptFilePath[MainWindow.Self.cmbDept.Text], 
                        FileMode.Open, FileAccess.Read);

                    // Десериализация потока
                    var temp = xml.Deserialize(xmlStr) as 
                        ObservableCollection<CompanyWorker>;

                    // Цикл добавления объектов в коллекцию
                    foreach (var t in temp)
                    {
                        workers.Add(t);
                    }

                    // Присваивание значение таблице tableDeptWorker
                    MainWindow.Self.tableDeptWorker.ItemsSource = workers;
                }
                // Услове при отсутствии файла
                else
                {
                    MessageBox.Show("Файл департамента отсутствует. Будет создан новый файл.");

                    // Создание пустого файла xml с разметкой таблицы
                    XmlSerializer xml = new XmlSerializer(typeof(ObservableCollection<CompanyWorker>));

                    // Поток записи нового файла
                    Stream xmlStr = new FileStream(DeptFilePath[MainWindow.Self.cmbDept.Text], FileMode.Create, FileAccess.Write);

                    // Сериализация в файл
                    xml.Serialize(xmlStr, workers);

                    // Загрытие потока сериализации
                    xmlStr.Close();
                }
            }
            // Условие, если департамент не выбран
            else
            {
                MessageBox.Show("Выберите департамент");
            }
        }
       
        /// <summary>
        /// Метод переопределяет коллекцию Methods.workers и сортирует в соответствиис выбранным параметном
        /// </summary>
        public static void SortTable(string worker, ListSortDirection? test)
        {
            // Получение имени свойства
            var temp = typeof(CompanyWorker).GetProperty(worker);

            // Переопределение коллекции в соответствии с параметрами сортироки
            workers = new ObservableCollection<CompanyWorker>(workers.OrderBy(temp.GetValue));

            // Условие переопределения коллекции в соответствии с заданными параметрами сортировки в обратном порядке
            if (test == ListSortDirection.Ascending)
            {
                workers = new ObservableCollection<CompanyWorker>(workers.OrderByDescending(temp.GetValue));
            }

        }

        #region Неиспользуемый метод.
        /// <summary>
        ///  Метод переименовывет столбцы при автогенерации
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        //public static string ColumnName(string col)
        //{
        //    var temp = typeof(CompanyWorker).GetProperty(nameof(col));

        //    var t = (DisplayNameAttribute)Attribute.GetCustomAttribute(temp, typeof(DisplayNameAttribute));

        //    return t.DisplayName;
        //}
        #endregion

        /// <summary>
        /// Метод загружает файл xml или json
        /// </summary>
        public static void LoadFile()
        {
            // Инициализация класа окна выбора файла
            OpenFileDialog opn = new OpenFileDialog();

                // Фильтр окна выбора файла
                opn.Filter = "Файл XML (*.xml)|*.xml|Файл JSON (*.json)|*.json";

                // Открытие окна выбора файла
                opn.ShowDialog();

                // Присвоение переменной пути выбранного файла
                filePath = opn.FileName;

            // Условие, при котором считывается файл xml
            if (opn.FileName.Contains(".xml"))
            {
                // Инициализация сериализации
                var xml = new XmlSerializer(typeof(ObservableCollection<CompanyWorker>));

                // Поток чтения файла
                var xmlStr = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                // Десериализация файла
                var temp = xml.Deserialize(xmlStr) as ObservableCollection<CompanyWorker>;

                // Цикл добавления объекта в коллуекцию
                foreach(var t in temp)
                {
                    workers.Add(t);
                }

                // Присвоение значения таблице TablreDeptWorker
                MainWindow.Self.tableDeptWorker.ItemsSource = workers;
            }
            // Условие, при котором считывается файл json
            else if (opn.FileName.Contains(".json"))
            {
                // Инициализация считывания json файла
                string loadJson = File.ReadAllText(filePath);

                // Десериализация файла по выбранному пути
                var temp = JsonConvert.DeserializeObject<ObservableCollection<CompanyWorker>>(loadJson);

                // Цикл добавления объекта в коллекцию
                foreach (var t in temp)
                {
                    workers.Add(t);
                }

                MainWindow.Self.tableDeptWorker.ItemsSource = workers;

            }
        }
        #endregion
    }
}
