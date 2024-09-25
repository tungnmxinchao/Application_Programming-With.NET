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
using DemoXMLAndJson.Models;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.IO;
using System.Xml.Serialization;

namespace DemoXMLAndJson
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json|XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog.Title = "Select a JSON or XML File";

            if (openFileDialog.ShowDialog() == true)
            {
                string extension = System.IO.Path.GetExtension(openFileDialog.FileName).ToLower();

                if (extension == ".json")
                {
                    LoadJsonData(openFileDialog.FileName);
                }
                else if (extension == ".xml")
                {
                    LoadXmlData(openFileDialog.FileName);
                }
                else
                {
                    MessageBox.Show("Unsupported file type.");
                }
            }
        }

        private void LoadJsonData(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                var quizData = JsonConvert.DeserializeObject<Rootobject>(json);

                // Gán dữ liệu cho DataGrid
                DataGrid.ItemsSource = new List<QuizItem>
                {
                    new QuizItem { Question = quizData.quiz.sport.q1.question, Answer = quizData.quiz.sport.q1.answer },
                    new QuizItem { Question = quizData.quiz.maths.q1.question, Answer = quizData.quiz.maths.q1.answer },
                    new QuizItem { Question = quizData.quiz.maths.q2.question, Answer = quizData.quiz.maths.q2.answer }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading JSON: " + ex.Message);
            }
        }

        private void LoadXmlData(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(catalog));
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    catalog catalogData = (catalog)serializer.Deserialize(fileStream);
               
                    var bookItems = new List<BookItem>();
                    foreach (var book in catalogData.book)
                    {
                        bookItems.Add(new BookItem
                        {
                            Author = book.author,
                            Title = book.title,
                            Genre = book.genre,
                            Price = book.price,
                            PublishDate = book.publish_date.ToShortDateString(),
                            Description = book.description
                        });
                    }

                    DataGridXml.ItemsSource = bookItems;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading XML: " + ex.Message);
            }
        }

        public class QuizItem
        {
            public string Question { get; set; }
            public string Answer { get; set; }
        }

        public class BookItem
        {
            public string Author { get; set; }
            public string Title { get; set; }
            public string Genre { get; set; }
            public decimal Price { get; set; }
            public string PublishDate { get; set; }
            public string Description { get; set; }
        }
    }
}