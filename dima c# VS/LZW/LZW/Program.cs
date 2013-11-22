using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZW
{
    class Program
    {

        static int getValue(char a, ref String alphabet)
        {
            int i = 0;
            while (alphabet[i] != a) i++;
            alphabet = alphabet.Remove(i, 1);
            alphabet = alphabet.Insert(0, a.ToString());

            return i;
        }

        static char getDecrypt(String i,ref  String alphabet)
        {
            Char answer = alphabet[int.Parse(i)];
            
            alphabet = alphabet.Remove(int.Parse(i), 1);
            alphabet = alphabet.Insert(0, answer.ToString());

            
            return answer;
        }


        static void Main(string[] args)
        {
            String alphabet = "", output = "";
            
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            for (int i = a; i <= b; i++)
                alphabet += Convert.ToChar(i);
            
            String input = Console.ReadLine();
            for (int i = 0; i < input.Length; i++ )
            {
                output += getValue(input[i],ref alphabet);
                 if (i!=input.Length-1)
                     output += ",";
            }


            Console.WriteLine(output);

            var dima = output.Split(',');
            alphabet = "";
            for (int i = a; i <= b; i++)
                alphabet += Convert.ToChar(i);
            output = "";
            foreach (var i in dima)
            {
                output += getDecrypt(i,ref alphabet);
            }
            Console.WriteLine(output);
            Console.ReadKey();
            
        }
    }
}
