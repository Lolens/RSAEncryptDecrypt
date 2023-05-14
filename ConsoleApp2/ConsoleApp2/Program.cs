// RSA Encryption
using System.Numerics;

class Program
{
    static void Main(string[] args)
    {
        //Использование TextReader для считывания текста из файла
        TextReader tr = File.OpenText("RSA.txt");
        String line = tr.ReadLine();
        int wh = 0;
        //Считываем весь файл и берем определенные строки, записываем в переменные
        string[] arr = new string[8];
        while (line != null)
        {
            line = tr.ReadLine();
            arr[wh] = line;
            wh++;
        }
        //Парсим элементы массива в BigInteger 
        BigInteger p = BigInteger.Parse(arr[0]);
        BigInteger q = BigInteger.Parse(arr[2]);
        BigInteger e = BigInteger.Parse(arr[4]);
        //Вывод вводимых делаем "красиво" и читабельно
        Console.WriteLine("===================================");
        Console.WriteLine("Случайное простое число 1: " + p);
        Console.WriteLine("Случайное простое число 2: " + q);
        Console.WriteLine("Открытая экспонента: " + e);
        Console.WriteLine("Сообщение: " + arr[6]);
        Console.WriteLine("===================================");

        //Значение функции Эйлера
        BigInteger phi = (p - 1) * (q - 1);

        //Модуль 
        BigInteger n = p * q;

        String message = arr[6];

        Console.WriteLine("Зашифрованное сообщение: ");

        //Массив размером в длину сообщения
        BigInteger[] encrypted = new BigInteger[message.Length];

        //Шифровка сообщения
        for (int i = 0; i < message.Length; i++)
        {
            encrypted[i] = BigInteger.ModPow(message[i], e, n);
            Console.Write(encrypted[i] + " ");
        }


        String decrypted = "";
        BigInteger d = calculateD(e, phi);

        for (int i = 0; i < encrypted.Length; i++)
        {
            //Дешифровка и преобразование в символы
            decrypted += (char)BigInteger.ModPow(encrypted[i], d, n);
        }

        Console.WriteLine("\nРасшифрованное сообщение: " + decrypted);
        Console.WriteLine("===================================");
        //Ожидание
        Console.Read();
    }

    //Вычисление секретного ключа
    static BigInteger calculateD(BigInteger e, BigInteger phi)
    {
        BigInteger d = 0;
        while (true)
        {
            if ((d * e) % phi == 1)
            {
                return d;
            }
            d++;
        }
    }
}