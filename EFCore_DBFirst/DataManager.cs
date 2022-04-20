using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using EFCore.BulkExtensions.SQLAdapters.PostgreSql;

namespace EFCore_DBFirst
{
    public class DataManager
    {
        public static DataManager Instance = new();
        public DBFContext dbFContext { get; set; }
        private DataManager()
        {
            dbFContext = new DBFContext();
        }

        public async Task<List<Student>> QueryData()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<Student> students =new List<Student>();
            await dbFContext.BulkReadAsync(students);
            stopwatch.Stop();
            Trace.WriteLine($"查询时间：{stopwatch.ElapsedMilliseconds} ms");
            return students;
        }

        public async void InsertData()
        {
            var students = new List<Student>(){
                new Student() {Id=1, Name = "AA" ,StuNum="2010"},
                new Student() {Id=2, Name = "BB" ,StuNum="2011"},
                new Student() {Id=3, Name = "CC",StuNum="2012" }};
            await dbFContext.BulkInsertAsync(students);
        }

        public async void DeleteData()
        {
            var stus = dbFContext.Students;
            await dbFContext.BulkDeleteAsync(stus.ToList());
            await dbFContext.Students.Where(x=>x.Name=="BB").BatchDeleteAsync();
        }

        public async void UpdateData()
        {
            var stus = dbFContext.Students.ToList();
            foreach (var item in stus)
            {
                item.Name += "QQQ";
            }
            await dbFContext.BulkUpdateAsync(stus);
            await dbFContext.Students.Where(x => x.StuNum == "2222").BatchUpdateAsync(new Student() {StuNum="2233" });
            await dbFContext.Students.Where(x => x.StuNum == "2233").BatchUpdateAsync(x=>new Student() { StuNum =x.StuNum+ "444" });
        }

        //Bulk相关(一条操作一个事务，均是传入实体)
        //直接使用这些操作时，每个操作都是独立的事务，并且会自动提交。
        //如果我们需要在单个过程中执行多个操作，则应使用显式事务
        public async void TransactionTest()
        {
            using (var transaction= dbFContext.Database.BeginTransaction())
            {
                try
                {
                    var students = new List<Student>(){
                     new Student() {Id=6, Name = "DD" ,StuNum="2044"},
                     new Student() {Id=7, Name = "EE" ,StuNum="2055"}};
                    await dbFContext.BulkInsertAsync(students);
                    await dbFContext.Students.Where(x => x.StuNum == "2044").BatchUpdateAsync(new Student() { Name = "DDEEF" });
                    transaction.Commit();
                }
                catch (Exception ex )
                {
                    //using包裹不需要手写rollback,报错会自动回滚
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
    }
}
