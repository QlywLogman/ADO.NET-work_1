using ADO.NET_work_1.Command;
using ADO.NET_work_1.Models;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADO.NET_work_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection sqlConnection = null;
        SqlDataReader dataReader = null;
        public ICommand AddCommand { get; }
        public ICommand FillCommand { get; }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ObservableCollection<Author> Authors { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            sqlConnection = new(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            Authors = [];
            AutorsListView.ItemsSource = Authors;
            AddCommand = new RelayCommand(AddAuthor);
            FillCommand = new RelayCommand(FillAuthors);
            DataContext = this;
        }

        private void FillAuthors()
        {


        }

        private void AddAuthor()
        {
            try
            {
                sqlConnection.Open();
                var query = "Insert INTO Authors(Id,Firstname,Lastname) Values (@id,@firstname,@lastname)";
                var cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("id", Id);
                cmd.Parameters.AddWithValue("firstname", FirstName);
                cmd.Parameters.AddWithValue("lastname", LastName);


                cmd.ExecuteNonQuery();
            }
            finally
            {

                sqlConnection.Close();
                dataReader.Close();
            }
        }
    }
}