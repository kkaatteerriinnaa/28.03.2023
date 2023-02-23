using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp23
{
    public class bankomat
    {
        public string PIN { get; set; }

        public string Nominal { get; set; }

        public int Сount { get; set; }

        public bankomat(string pin, string nominal, int count)
        {
            PIN = pin;
            Nominal = nominal;
            Сount = count;
        }
    }
    public class Money
    {
        public string UAN { get; set; }
        public string USD { get; set; }
        public string EUR { get; set; }
        public Money(string uan, string usd, string eur)
        {
            UAN = uan;
            USD = usd;
            EUR = eur;
        }
    }
    public class Chek
    {
        public string Elect { get; set; }
        public string Paper { get; set; }
        public Chek(string elect, string paper)
        {
            Elect = elect;
            Paper = paper;
        }
    }
    class Program
    {
        static List<bankomat> read()
        {
            List<bankomat> list = new List<bankomat>() { };
            StreamReader sr = new StreamReader("bankomat.txt");
            string line;
            string[] array;
            try
            {
                while ((line = sr.ReadLine()) != null)
                {
                    array = line.Split(new char[] { ' ', '\t' });
                    bankomat bankomat1 = new bankomat();
                    bankomat1.nominal = int.Parse(array[0]);
                    bankomat1.count = int.Parse(array[1]);
                    list.Add(bankomat1);
                    line = string.Empty;
                }
                sr.Close();
            }
            catch
            {
                Console.WriteLine("Файл не найден");
            }
            return list;
        }
 
        static void calculation_and_write_in_file(List<bankomat> list, int money)
        {
            int count = 0;
            int m = 0;
            m = money;
            int j = list.Count;
            for (int p = j - 1; p >= 0; p--) 
            {
                if (list[p].count!= 0)
                {
                    count = m / list[p].nominal;
                    if (list[p].count > count)
                    {
                        m = m - count * list[p].nominal;
                        Console.WriteLine("Будет снято" + count + "номиналом" + list[p].nominal);
                    }
                    if(list[p].count<=count)
                    {
                        m = m - list[p].count * list[p].nominal;
                        Console.WriteLine("Будет снято" + list[p].count + "номиналом" + list[p].nominal);
                     }
 
                }               
            }
            Console.WriteLine("Теперь баланс банка:");
            StreamWriter sw = new StreamWriter("bankomat.txt");
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine("{0}\t{1}", list[i].nominal, list[i].count);
                    sw.WriteLine(list[i].nominal + " " + list[i].count);
                }
                sw.Close();
            }
            catch
            {
                Console.WriteLine("Данные в файл не были записаны");
            }
        }
 
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Текущий счет банка");
                List<bankomat> list = read();
                int all_money_of_bank = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine("{0}\t{1}", list[i].nominal, list[i].count);
                    all_money_of_bank = all_money_of_bank + list[i].nominal * list[i].count;
                }
                Console.WriteLine("Введите суммму:");
                int money = int.Parse(Console.ReadLine());
                if (all_money_of_bank < money)
                {
                    Console.WriteLine("В банкомате недостаточно средств");
                    Environment.Exit(0);
                }
                calculation_and_write_in_file(list, money);
            }
            catch
            {
                Console.WriteLine("Ошибка");
            }
        }
    }
}
