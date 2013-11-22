using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Criptology
{
    class Program
    {

        static bool isNumber(char a)
        {
            return ((a > 47) && (a < 58)) || a=='-' ;
        }


        static String getText(String input)
        {
            String number = "",output = "";
            int i = 0,numb;
            while (isNumber(input[i])) { number += input[i].ToString(); i++; }
            numb = Convert.ToInt32(number);
            if (numb>0)
            {
                for (int j = 0; j < numb; j++) output += input[i];
            }
            else
            {
                for (int j = 0; j < Math.Abs(numb); j++) output += input[i+j];
            }
            return output;
        }

        static int getValue(String input, int n)
        {
            int i = n, count=0;
            while (i!=input.Length-1 && input[i]==input[i+1])
            {
                count++;
                i++;
            }
            if (count > 0) return ++count;

            while (i!=input.Length-1 && input[i]!=input[i+1])
            {
                count++;
                i++;
            }
            if (i == input.Length - 1) count++;
            if (count == 1) return count;
            return -count;

        }



        static void Compress(String input)
        {
            int i=0,value,k;
            String output = "";
            while (i < input.Length)
            {
                value = getValue(input, i);
                if (value>0)
                {
                    output += value.ToString();
                    output += input[i];
                    i += value;
                }
                else
                {
                    output += value.ToString();
                    k = i + Math.Abs(value);
                    while (i<k)
                    {
                        output += input[i];
                        i++;
                    }
                }
            }
            Console.WriteLine(output);
        }

        static void Decompress(String input)
        {
            int i = 0,k = 0;
            String output = "";
            while (i<input.Length)
            {
                k = i;
                while (isNumber(input[i])) i++;
                while (i < input.Length && !isNumber(input[i])) i++;
                String temp="";
                for (int j=k; j<i; j++) temp+=input[j].ToString();
                           
                output += getText(temp);
            }
            Console.WriteLine(output);
        }

        static void Main(string[] args)
        {
          
            String mod = Console.ReadLine();
            String input = Console.ReadLine();
            switch (mod)
            {
                case "1":
                    Compress(input);
                    break;
                case "2":
                    Decompress(input);
                    break;
                default:
                    Console.WriteLine("Invalid value");
                    break;
             }

            Console.ReadKey();
         
        }
    }
}
