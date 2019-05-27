/* Josh Degazio.
 * Program that writes every possible placement of numbers, from a given string of numbers.
 * Started: Saturday 25th, May 2019.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace _182685_u5_Lexicographic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Globals
        private string input;
        private string output;
        private string[] permutations = new string[0];
        private int[] values = new int[0];

        //Boot up the program
        public MainWindow()
        {
            InitializeComponent();
        }

        //Private method that generates permutations
        //Based on variables passed to the method
        private static IEnumerable<IEnumerable<T>>
        GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        //Button click events
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Read_Input();
            Generate_Outcomes();
            Generate_Output();
        }

        //Reads input and sets variables
        private void Read_Input()
        {
            input = tB_Input.Text;

            for (int i = 0; i < input.Length - 1; i++)
            {
                Array.Resize(ref values, values.Length + 1);
                int.TryParse(input.Split(',')[i], out values[i]);
            }
        }

        //Generates permutations
        private void Generate_Outcomes()
        {
            permutations = new string[0];
            IEnumerable<IEnumerable<int>> result =
            GetPermutations(Enumerable.Range(values[0], values[1] + 1), values[1] + 1);

            foreach(IEnumerable<int> res in result)
            {
                Array.Resize(ref permutations, permutations.Length + 1);
                foreach (int i in res)
                {

                    permutations[permutations.Length - 1] += i.ToString();

                }
            }
        }

        //Generates the output to display to the user
        private void Generate_Output()
        {
            output = " ";

            for (int i = 0; i < permutations.Length; i++)
            {
                if (output != " ")
                {
                    output += ", " + permutations[i];
                }
                else output = "Permutations: " + permutations[i];
            }

            tB_Output.Text = output;
        }
    }
}
