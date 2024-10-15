using System;
using System.ComponentModel;
using System.Data;
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
		// Events
		// Part of INTERFACE INotifyPropertyChanged, Event (think signal in GDScript) for 
		// telling when a Binding Property has changed/updated.
		public event PropertyChangedEventHandler? PropertyChanged;

		// Private VARs
		private string _output = "0";
		private List<string> _expression = new List<string>() 
		{ 
			"0",
		};

		// Public Properties
		public List<string> Expression {
			set 
			{
				_expression = value;
			}
			get
			{
				return _expression;
			}
		}
		public int ExpressionIndex { set; get; } = 0;
		public string Output
		{
			set {
				_output = value.Replace("*", "×").Replace("/", "÷");
				// On a PropertyChanged Event, update the GUI that Binds this Property.
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Output"));
			}
			get
			{
				return _output;
			}
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

			if (double.TryParse((string) button.Content, out double number))
			{
				Expression[ExpressionIndex] += number;
				Expression[ExpressionIndex] = Convert.ToString(Convert.ToDouble(Expression[ExpressionIndex]));

				Output = string.Join(" ", Expression);
				//OutputTextBlock.Text = Output;
			}
		}

		private void ClearBtn_Click(object sender, RoutedEventArgs e)
		{
			Output = string.Empty;
			Expression.Clear();
			Expression.Add("0");
			ExpressionIndex = 0;

			Output = string.Join(" ", Expression);
		}

		private void OperatorBtn_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button) sender;

			if ("=".Equals(button.Content))
			{
				try
				{
					object data_table = new DataTable().Compute(string.Join(" ", Expression), null);

					double result = Convert.ToDouble(data_table);

					if (!double.IsNaN(result))
					{
						Output = result.ToString();

						Expression.Clear();
						Expression.Add(result.ToString());
						ExpressionIndex = 0;
					}
					else
					{
						Output = result.ToString();

						Expression.Clear();
						Expression.Add("0");
						ExpressionIndex = 0;
					}
				}
				catch (SyntaxErrorException)
				{
					MessageBox.Show($"SYNTAX ERROR! PLEASE REWRITE EXPRESSION!");
				}
				catch (Exception exception)
				{
					MessageBox.Show($"ERROR! {exception.Message}");
				}
			}
			else
			{
				string operator_sign = "";
				
				// Under the Hood, STRINGs are compared by their reference, and if that is false,
				// it tries to compare by Object.Equals(); also known as Ordinal comparison, meaning each
				// value of the STRING is compared to see if the vale matches overall.
				switch (button.Content)
				{
					case "+":
						operator_sign = "+";
						break;
					case "-":
						operator_sign = "-";
						break;
					case "×":
						operator_sign = "*";
						break;
					case "÷":
						operator_sign = "/";
						break;
				}

				Expression.Add("");
				Expression[++ExpressionIndex] = operator_sign;

				Output = string.Join(" ", Expression);

				++ExpressionIndex;
				Expression.Add("");
			}
			
			

			//if (double.TryParse(Output, out double number))
			//{

			//}
		}
	}
}