using EFCore_DBFirst;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EFCore_Test.ViewModels
{
    public class DBFirstViewModel : BindableBase
    {
        private DBFContext dbfContext { get; set; }
        public DelegateCommand ReadCommand { get; set; }
        public DelegateCommand InsertCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand TransCommand { get; set; }

        private List<Student> _students;
        public List<Student> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }


        public DBFirstViewModel()
        {
            dbfContext = new DBFContext();
            ReadCommand = new DelegateCommand(QueryData);
            InsertCommand = new DelegateCommand(InsertData);
            DeleteCommand = new DelegateCommand(DeleteData);
            UpdateCommand = new DelegateCommand(UpdateData);
            TransCommand = new DelegateCommand(TransactionTest);

        }

        private void QueryData()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Students = DataManager.Instance.QueryData().Result;
            stopwatch.Stop();
            Trace.WriteLine($"查询时间：{stopwatch.ElapsedMilliseconds} ms");
        }

        private void InsertData()
        {
            //dbfContext.Add(new Student()
            //{
            //    Name = "小绿",
            //    Gender = "女",
            //    StuNum = "2004"
            //});
            //await dbfContext.SaveChangesAsync();
            DataManager.Instance.InsertData();
        }

        private void DeleteData()
        {
            //var stu = dbfContext.Students.FirstOrDefault(x => x.StuNum == "2003");
            //dbfContext.Remove(stu);
            //await dbfContext.SaveChangesAsync();

            DataManager.Instance.DeleteData();
        }

        private void UpdateData()
        {
            //var stu = dbfContext.Students.Single(x => x.StuNum == "2002");
            //stu.Name = "更新";
            //await dbfContext.SaveChangesAsync();
            DataManager.Instance.UpdateData();
        }

        private void TransactionTest()
        {
            DataManager.Instance.TransactionTest();
        }

    }
}
