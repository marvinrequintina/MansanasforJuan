using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MansanasForJuan
{
    /// <summary>
    /// for validation
    /// </summary>
    class Validation
    {
       public bool ValidateInput(string input)
        {
            Console.WriteLine("Validating Input.....");
            if(input =="" || input == null)
            {
                MessageBox.Show("Please enter your name correctly!", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (input.Contains(";"))
            {
                MessageBox.Show("Name must not contain ';' !", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

    }
}
