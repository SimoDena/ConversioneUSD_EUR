using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConversioneUSD_EUR
{
    class Program
    {
        static void Main(string[] args)
        {
            int scelta = 0;
            double ammontare = 0;
            do
            {
                Console.WriteLine("Premere:\n1 - Per Convertire Dollari in Euro.\n2 - Per Convertire Euro in Dollari.");
                scelta = Convert.ToInt16(Console.ReadLine());
                if (scelta != 1 && scelta != 2)
                {
                    Console.WriteLine("Inserire un numero corretto!");
                }
                else
                {
                    Console.WriteLine("\nInserire il numero da convertire:\n");
                    ammontare = Convert.ToDouble(Console.ReadLine());
                }
            } while (scelta != 1 && scelta != 2);

            switch (scelta)
            {
                case 1:
                    Conversione("USD_EUR", ammontare);
                    break;
                case 2:
                    Conversione("EUR_USD", ammontare);
                    break;
            }

            Console.ReadKey();
        }

        static async void Conversione(string valute, double ammontare)
        {
            HttpClient client = new HttpClient();
            string url = "https://free.currconv.com/api/v7/convert?q="+valute+"&compact=ultra&apiKey=c2a1d7ecdd13930690d0";
            HttpResponseMessage risposta = await client.GetAsync(url);

            if (risposta.IsSuccessStatusCode)
            {
                Conversion conversione = await risposta.Content.ReadAsAsync<Conversion>();
                switch (valute)
                {
                    case "USD_EUR":
                        double calcolo = ammontare * conversione.USD_EUR;
                        Console.WriteLine($"\n{ammontare}$ equivalgono a {calcolo} Euro");
                        break;
                    case "EUR_USD":
                        calcolo = ammontare * conversione.EUR_USD;
                        Console.WriteLine($"\n{ammontare} Euro equivalgono a {calcolo}$");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Conversione Non Riuscita!");
            }
        }
    }
}
