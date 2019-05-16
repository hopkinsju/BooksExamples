using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoinQueries
{
    public partial class JoiningTableData : Form
    {
        public JoiningTableData()
        {
            InitializeComponent();
        }

        private void JoiningTableData_Load(object sender, EventArgs e)
        {
            var dbcontext = new BooksExamples.BooksEntities();

            var authorsAndISBNs =
                from author in dbcontext.Authors
                from book in dbcontext.Titles
                orderby author.LastName, author.FirstName
                select new { author.FirstName, author.LastName, book.ISBN };

            outputTextBox.AppendText("Authors and ISBNs:");

            foreach (var element in authorsAndISBNs)
            {
                outputTextBox.AppendText($"{Environment.NewLine}{Environment.NewLine}\t{element.FirstName,-10} " +
                    $"{element.LastName,-10} {element.ISBN,-10}");
            }

            var authorsAndTitles =
                from book in dbcontext.Titles
                from author in dbcontext.Authors
                orderby author.LastName, author.FirstName, book.Title1
                select new { author.FirstName, author.LastName, book.Title1 };

            outputTextBox.AppendText("{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}Authors and titles:");

            foreach (var element in authorsAndTitles)
            {
                outputTextBox.AppendText($"{Environment.NewLine}{Environment.NewLine}\t{element.FirstName,-10} " +
                    $"{element.LastName,-10} " +
                    $"{element.Title1,-10}");
            }

            var titlesByAuthor =
                from author in dbcontext.Authors
                orderby author.LastName, author.FirstName
                select new
                {
                    Name = author.FirstName + " " + author.LastName,
                    Titles =
                        from book in author.Titles
                        orderby book.Title1
                        select book.Title1
                };

            outputTextBox.AppendText("{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}Titles grouped by author:");

            foreach (var author in titlesByAuthor)
            {
                outputTextBox.AppendText($"{Environment.NewLine}{Environment.NewLine}\t{author.Name}:");

                foreach (var title in author.Titles)
                {
                    outputTextBox.AppendText($"{Environment.NewLine}{Environment.NewLine}\t\t{title}");
                }
            }

        }
    }
}