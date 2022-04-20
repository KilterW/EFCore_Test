using EFCore_CodeFirst;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Test.ViewModels
{
    public class CodeFirstViewModel: BindableBase
    {
        private CodeFContext codeFContext { get; set; }
        public DelegateCommand ReadCommand { get; set; }
        public DelegateCommand InsertCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }

        private List<Book> _books;

        public List<Book> Books
        {
            get => _books; 
            set => SetProperty  (ref _books, value);
        }

        public CodeFirstViewModel()
        {
            codeFContext = new CodeFContext();
            ReadCommand = new DelegateCommand(QueryData);
            InsertCommand = new DelegateCommand(InsertData);
            DeleteCommand = new DelegateCommand(DeleteData);
            UpdateCommand = new DelegateCommand(UpdateData);
        }

        private void QueryData()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            //查询Book表所有数据
            Books = codeFContext.Books.ToList();
            //条件查询,linq操作
            //Books = codeFContext.Books.Where(x => x.Id > 3).ToList();
            stopwatch.Stop();
            Trace.WriteLine($"查询时间：{stopwatch.ElapsedMilliseconds} ms");
        }

        private async void InsertData()
        {
            var books=new List<Book>() { 
                new Book() { Title ="追风筝的人"},
                new Book() { Title ="霍乱时期的爱情"}
            };
            //修改DbSet
            //codeFContext.Books.AddRange(books);
            ////再save更改
            //await codeFContext.SaveChangesAsync();

            await codeFContext.BulkInsertAsync(books);
        }

        private async void DeleteData()
        {
            //先Linq查询
            //var books= codeFContext.Books.Where(x => x.Id>5);
            //从实体中删除数据
            //codeFContext.Books.RemoveRange(books);
            //再save更改
            //await codeFContext.SaveChangesAsync();

            //批量
            //await codeFContext.DeleteRangeAsync<Book>(x=>x.Id>5);

            //take仅删除前3个
            await codeFContext.Books.Where(x=>x.Id>5).Take(3).DeleteRangeAsync(codeFContext);
        }

        private async void UpdateData()
        {
            //需要先查询
            //var books = codeFContext.Books.Where(x => x.Title == "追风筝的人");
            ////再对查询到的数据进行修改
            //foreach (var item in books)
            //{
            //    item.Title = "放学后";
            //}
            ////再save更改
            //await codeFContext.SaveChangesAsync();

            //批量更新，条件，设置更新的列和值，执行
            //await codeFContext.BatchUpdate<Book>()
            //    .Where(x => x.Title == "放学后")
            //    .Set(x=>x.Title,x=>x.Title+"Test")
            //    .Set(x=>x.Price,x=>20)
            //    .ExecuteAsync();

            //skip跳过前3个
            await codeFContext.BatchUpdate<Book>()
                .Where(x => x.Title == "放学后")
                .Set(x => x.Title, x => x.Title + "Test")
                .Set(x => x.Price, x => 20)
                .Skip(3)
                .ExecuteAsync();

        }
    }
}
