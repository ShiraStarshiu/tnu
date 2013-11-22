using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playfair
{
    class Program
    {

        static bool Contains(Char[,] matrix, Char value)
        {
            for (int i=0; i<5; i++)
                for (int j=0; j<5; j++)
                {
                    if (matrix[i, j] == value) return true;
                }
            return false;
        }

        static Char[,] getMatrix(String input)
        {
            input = input.Replace('j', 'i');
            input = new string(input.Distinct().ToArray());
            String alphabet = "";
            for (int i = 97; i <= 122; i++) if (i!=106) alphabet += Convert.ToChar(i);
            Char[,] matrix = new Char[5, 5];
            int k = 0, f = 0;
            for (int i=0; i<5; i++)
                for (int j=0; j<5; j++)
                {
                    if (k<input.Length)
                        matrix[i, j] = input[k];
                    else
                    {
                        while (Contains(matrix,alphabet[f])) f++;
                        matrix[i, j] = alphabet[f];
                    }
                    k++;
                }
            return matrix;
        }

        static void showMatrix(char [,] matrix)
        {
            Console.WriteLine("Key matrix:");
            for (int i=0; i<5; i++)
            {
                for (int j = 0; j < 5; j++)
                    Console.Write(matrix[i, j] + " ");
                Console.Write("\n");
            }
        }
      
        static int getX(Char a,Char[,] matrix)
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (matrix[i, j] == a) return i;
            return 100500;
        }

        static int getY(Char a, Char[,] matrix)
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (matrix[i, j] == a) return j;
            return 100500;
        }


        static String Crypt(String input, Char[,] matrix)
        {
            input = input.Replace('j', 'i');
            String output = "";
            int x1, y1 , x2 , y2;
            if (input.Length % 2 == 1) input += 'x';
            for (int i = 0; i < input.Length - 1; i++)
            {
                x1 = getX(input[i], matrix);
                y1 = getY(input[i], matrix);
                x2 = getX(input[++i], matrix);
                y2 = getY(input[i], matrix);

                if (x1 == x2 && y1 == y2)
                {
                    x2 = getX('x', matrix);
                    y2 = getY('x', matrix);
                }

                if (x1 == x2)
                {
                    output += matrix[x1, (y1 + 1) % 5];
                    output += matrix[x2, (y2 + 1) % 5];
                }
                else
                if (y1 == y2)
                {
                    output += matrix[(x1 + 1) % 5, y1];
                    output += matrix[(x2 + 1) % 5, y2];
                }
                else
                {
                    output += matrix[x1, y2];
                    output += matrix[x2, y1];
                }

            }

            return output;  
        }

        static String Decrypt(String input, Char[,] matrix)
        {
            String output = "";
            int x1, y1, x2, y2;
         
            for (int i = 0; i < input.Length - 1; i++)
            {
                char a, b;
                x1 = getX(input[i], matrix);
                y1 = getY(input[i], matrix);
                x2 = getX(input[++i], matrix);
                y2 = getY(input[i], matrix);

 

                if (x1 == x2)
                {   
                 
                    a= matrix[x1,((y1-1) == -1? 4 : y1-1)];
                    b = matrix[x2, ((y2 - 1) == -1 ? 4 : y2 - 1)];
                }
                else
                    if (y1 == y2)
                    {
                        a = matrix[((x1 - 1) == -1 ? 4 : x1 - 1), y1];
                        b = matrix[((x2 - 1) == -1 ? 4 : x2 - 1), y2];
                    }
                    else
                    {
                       a= matrix[x1, y2];
                       b= matrix[x2, y1];
                    }
                if (b == 'x' && i != input.Length - 1) b = a;
                output += a;
                output += b;
            }
            if (output[output.Length - 1] == 'x') output = output.Remove(output.Length - 1);
            return output;
        }
        static void Main(string[] args)
        {
            String input = Console.ReadLine();
            String key = Console.ReadLine();
      
            var matrix = getMatrix(key);
            showMatrix(matrix);

            Console.WriteLine("Crypt:   "+Crypt(input, matrix));
            Console.WriteLine("Decrypt: " + Decrypt(Crypt(input, matrix), matrix));




            Console.ReadKey();
            
        }
    }
}
