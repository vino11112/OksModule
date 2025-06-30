using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using OksModule.ViewModels;

namespace OksModule.Views
{
    /// <summary>
    /// Логика взаимодействия для DocumentsListView.xaml
    /// </summary>
    public partial class DocumentsListView : Window
    {
        public DocumentsListView()
        {
            InitializeComponent();
            var viewModel = new DocumentsListViewModel();
            this.DataContext = viewModel;
        }
    }
}
