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

namespace GerenciarArquivo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Items { get; set; }
        public List<string> CaminhosDestino { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Criando dados de teste
            Items = new List<string>
            {
                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                "Quarto e último item de teste"
            };

            CaminhosDestino = new List<string>
            {
                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                                "Primeiro item da lista",
                "Segundo item com texto mais longo",
                "Terceiro item",
                "Quarto e último item de teste"
            };

            DataContext = this; // Define o DataContext para a própria janela
        }
    }
}