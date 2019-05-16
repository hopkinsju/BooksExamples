using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DisplayQueryResult
{
    public partial class TitleQueries : Form
    {
        public TitleQueries()
        {
            InitializeComponent();
        }

        private BooksExamples.BooksEntities dbcontext =
            new BooksExamples.BooksEntities();

        private void TitleQueries_Load(object sender, EventArgs e)
        {
            dbcontext.Titles.Load();
            queriesComboBox.SelectedIndex = 0;
        }

        private void QueriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (queriesComboBox.SelectedIndex)
            {
                case 0:
                    titleBindingSource.DataSource =
                        dbcontext.Titles.Local
                        .OrderBy(book => book.Title1);
                    break;
                case 1:
                    titleBindingSource.DataSource =
                        dbcontext.Titles.Local
                        .Where(book => book.Copyright == "2016")
                        .OrderBy(book => book.Title1);
                    break;
                case 2:
                    titleBindingSource.DataSource =
                        dbcontext.Titles.Local
                        .Where(book => book.Title1.EndsWith("How to Program"))
                        .OrderBy(book => book.Title1);
                    break;
                default:
                    break;
            }

            titleBindingSource.MoveFirst();
        }
    }
}
