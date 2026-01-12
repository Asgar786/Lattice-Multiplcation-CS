using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace  LatticeMultiplicationEnginr
{
    class  LatticeMultiplication
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Hard Challenge ??\n");

            Int32 f = 123456;
            Int32 s = 789;

            int firsnumber = Convert.ToInt32(Console.ReadLine());
            int secondnumber = Convert.ToInt32(Console.ReadLine());

            Int32.TryParse(Max(firsnumber, secondnumber).ToString(), out f);

            Int32.TryParse(Min(firsnumber, secondnumber).ToString(), out s);

            Console.WriteLine($" Multiply {f} with {s}\n");
            int l = f.ToString().Length - s.ToString().Length;


            if ((l == 0))
            {
                Console.WriteLine($" {f}");
                Console.WriteLine($"{new string(' ', l)}x{s}");
                Console.WriteLine($" {new string('-', f.ToString().Length)}");
            }
            else
            {
                Console.WriteLine($"{f}");
                Console.WriteLine($"{new string(' ', l - 1)}x{s}");
                Console.WriteLine($"{new string('-', f.ToString().Length)}\n");
            }
            
            Console.WriteLine($"The result of multiplication of {f} by {s} is {f * s}.\n");
            Console.WriteLine("Normal Matrix\n\n");
            var  mtx = Matrix(f.ToString(), s.ToString());
            DisplayMatrix(mtx);
            Console.WriteLine("\n");
            Console.WriteLine(" Matrix without zeroes\n\n");
            DisplayMatrix(mtx , formatoptions.removezeroes);
            Console.WriteLine("\n");
            Int64 res = MatrixAddition(mtx);
            Console.WriteLine();
            Console.WriteLine($"The result of Multiplying {f} by {s} is {res}\n");
            
            Console.ReadLine();
        }

        //Helper Methods
        static (int, int) SplitDigits(int digits)
        {
           
            int d1 = digits / 10;
            int d2 = digits  % 10;

            return (d1, d2);
        }   
        static (int, int) SplitDigits(int x, int y)
        {
            int product = x * y;
            int d1 = product / 10;
            int d2 = product % 10;

            return (d1, d2);
        }
        static int Max(int x,int y)=>(x > y ? x : y);
        static int Min(int x,int y)=> (x < y ? x : y);
        static int[,] Matrix(string num1, string num2)
        {

            int w = num1.Length;
            int h = num2.Length;

            int[,] l = new int[w*2,w*2];
            char[] arr1 = num1.ToCharArray();
            char[] arr2 = num2.ToCharArray();

            Array.Reverse(arr2);

            for (int i = 0;  i < h; i++)
            {
                int n = 0 + i;
                int m = w - (i + 1);
                for (int j = 0; j < w; j++)
                {
                      
                    int c = Convert.ToInt32(arr1[j].ToString());
                    int d = Convert.ToInt32(arr2[i].ToString());
                    (int, int) r = SplitDigits(c, d);
                    l[n, m] = r.Item1;
                    l[n, m + 1] = r.Item2;
                    m++;
                    n++;    
                }
                
            }
            return l;
        }
       private enum  formatoptions
       {
           normal,
           removezeroes
       }
       
        static void DisplayMatrix(int[,]list , formatoptions format = formatoptions.normal)
        {
           if(format  == formatoptions.removezeroes)
           {
               for(int i = 0; i < list.GetLength(0); i++)
            {
                for(int j = 0; j < list.GetLength(1); j++)
                {
                    
           if(list[i,j] != 0)         Console.Write($"{list[i,j]} ");
           else Console.Write("  ");
                }
                Console.WriteLine();
            }
           }
           else
           {
            for(int i = 0; i < list.GetLength(0); i++)
            {
                for(int j = 0; j < list.GetLength(1); j++)
                {
                    
                    Console.Write($" {list[i,j]} ");
                }
                Console.WriteLine();
            }
            }
        }
        static Int64 MatrixAddition(int[,] list)
        {
            Int64 sum = 0;
            int carry = 0;
            int power = 0;
            for(int i = list.GetLength(0) - 1 ; i >= 0; i--)
            {
                 //123456
                 //  789    
                 int AddResult = 0;
                for(int j = list.GetLength(0) - 1; j >= 0; j--)
                {
                    int element = list[j, i]; 
                    AddResult += (element + carry);
                    carry = 0;
                    

                    
                }
            if (AddResult >= 10)
              {
                    (int, int) r = SplitDigits(AddResult);
                    carry = r.Item1;
                    sum += r.Item2 * (Int64)Math.Pow(10, power);
              }
              else
              {
                    sum += AddResult * (Int64)Math.Pow(10, power);
              }
                    power++;
            }
            return sum;
        }           
    }
}


