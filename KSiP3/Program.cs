using System;
using System.Collections.Generic;

namespace KSiP3
{
    class Program
    {
        static string Alphabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯабвгґдеєжзиіїйклмнопрстуфхцчшщьюя1234567890.,!?–- _()"; //your alphabet


        static void Main(string[] args)
        {
            AlphabetCreate(Text.RawText); // перші символи 0-9

            double p = 2203;
            double q = 3217;

            Console.WriteLine($"p = {p}");
            Console.WriteLine($"q = {q}");

            double n = p * q;
            double phi = (p - 1) * (q - 1);
            Console.WriteLine($"n = {n}");
            Console.WriteLine($"phi = {phi}");

            double d = FindD(phi); //223
            double e = FindE(d, phi);//7

            Console.WriteLine($"d = {d}");
            Console.WriteLine($"e = {e}");

            List<double> encrypted = Encryption(e, n, Text.RawText);

            foreach (var i in encrypted)
            {
                Console.Write(i);
                Console.Write(" ");
            }

            Console.WriteLine(Decryption(d, n, encrypted));
        }

        #region find
        static double FindD(double phi) // випадкове взаємнопросте, просте, менше за ф
        {
            var rand = new Random();
            var randNum = rand.Next((int)Math.Sqrt(phi), (int)phi);

            double d = 0;
            bool prime;

            for (d = randNum; d > 2; d--)
            {
                prime = true;
                for (int j = 2; j < d; j++)
                {
                    if (d % j == 0)
                    {
                        prime = false;
                    }
                }
                if (prime)
                {
                    break;
                }
            }

            return d;
        }

        static double FindE(double d, double phi)
        {
            double e = 0;

            for (e = 0; ; e++)
            {
                if (((e * d) % phi) == 1)
                {
                    break;
                }
            }

            return e;
        }
        #endregion

        static List<double> Encryption(double e, double n, string rawText)
        {
            var encryptedMessage = new List<double>();
            double symbolNumber = 0;

            foreach (var symbol in rawText)
            {
                symbolNumber = (int)Alphabet.IndexOf(symbol);
                var C = powerMod(symbolNumber, e, n);
 
                encryptedMessage.Add(C);
            }
            return encryptedMessage;
        }

        static string Decryption(double d, double n, List<double> encryptedMessage)
        {
            var decryptedMessage = "";

            foreach (double symbol in encryptedMessage)
            {
                var C = powerMod(symbol, d, n);
                decryptedMessage += Alphabet[Convert.ToInt32(C)];
            }
            return decryptedMessage;
        }

        static double powerMod(double num, double exponent, double modulus)
        {
            if (modulus == 1) return 0;
            double result = 1;
            num = num % modulus;
            while (exponent > 0)
            {
                if (exponent % 2 == 1)  //odd number
                    result = (result * num) % modulus;
                exponent = exponent / 2; //divide by 2
                num = (num * num) % modulus;
            }
            return result;
        }

        static void AlphabetCreate (string text)
        {
            Alphabet = "";
            foreach (var i in text)
            {
                if (Alphabet.IndexOf(i) == -1)
                {
                    Alphabet += i;
                }
            }
        }
    }
}
