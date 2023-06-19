using Dapper;
using ManyToManyDapper.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

namespace ManyToManyDapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
            using (var connection=new SqlConnection(conn))
            {
                //var sql = @" SELECT S.StudentId,S.Firstname,S.Age,S.GroupId,
                //                G.GroupId,G.Title
                //                FROM Students AS S
                //                INNER JOIN Groups AS G
                //                ON  S.GroupId=G.GroupId";
                //var students = connection.Query<Student, Group, Student>(sql,
                //    (student, group) =>
                //    {
                //        student.Group = group;
                //        return student;
                //    }, splitOn: "GroupId").ToList();

                //myDataGrid.ItemsSource = students;

                ///////////////////////////////////////////////////////////////
                //var sql = @" SELECT G.GroupId,G.Title,
                //                    S.StudentId,S.Firstname,S.Age,S.GroupId
                //                    FROM Students AS S
                //                    INNER JOIN Groups AS G
                //                    ON  S.GroupId=G.GroupId";

                //var groups = connection.Query<Group, Student, Group>(sql,
                //    (group, student) =>
                //    {
                //        group.Students.Add(student);
                //        student.Group = group;
                //        return group;
                //    },splitOn:"GroupId");


                //myDataGrid.ItemsSource = groups.Select(g =>
                //{
                //    return new GroupModel
                //    {
                //        Id = g.GroupId,
                //        Title = g.Title
                //    };
                //}).ToList();



//                /*
//                 Book  AuthorId,CategoryId

//var books=connection.Query<Book,Author,Category,Book>(sql,
//(book,author,category)=>{
//   book.Author=author;
//   book.Category=category;
//                return book;
//},splitOn:"AuthorId,CategoryId");
//                 */


                var sql = @" SELECT * FROM Groups";

                var groups = connection.Query<Group>(sql);
                myDataGrid.ItemsSource = groups;


            }
        }

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item =myDataGrid.SelectedItem as Group;
            var conn = ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString;
            using (var connection = new SqlConnection(conn))
            {
                var sql = @" SELECT * FROM Students WHERE GroupId=@id";

                var students = connection.Query<Student>(sql,new {id=item.GroupId});
                myDataGrid2.ItemsSource = students;
            }
        }
    }
}
