using System.ComponentModel;
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

namespace Calculator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		private string _output = "0";

		// Part of INTERFACE INotifyPropertyChanged, Event (think signal in GDScript) for 
		// telling when a Binding Property has changed/updated.
		public event PropertyChangedEventHandler? PropertyChanged;

		public string Output
		{
			set {
				_output = value;
				// On a PropertyChanged Event, update the GUI that Binds this Property.
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Output"));
			}
			get
			{
				return _output;
			}
		}
		public int FirstNumber
		{
			set; get;
		}
		public int SecondNumber
		{
			set; get;
		}

		public MainWindow()
		{
			InitializeComponent();
			
			// Set the Data Binding source to THIS.
			this.DataContext = this;
		}

		// On Button Pressed.
		private void NumBtn_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button) sender;
			//MessageBox.Show($"Button Value: {button.Content}");

			if (int.TryParse((string) button.Content, out int number))
			{
				Output += button.Content;
				Output = Convert.ToString(Convert.ToInt32(Output));
				//OutputTextBlock.Text = Output;
			}
			else
			{
			}
		}

		private void ClearBtn_Click(object sender, RoutedEventArgs e)
		{
			Output = string.Empty;
		}
	}
}