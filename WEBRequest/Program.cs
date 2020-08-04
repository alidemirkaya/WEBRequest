using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WEBRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            Console.WriteLine("Lütfen tutarı giriniz :");
            var amountInput = Console.ReadLine();
            if (!checkAmount(amountInput))
            {
                Console.WriteLine("Geçerli bir tutar girmediniz.");
                Console.ReadKey();
                return;
            }
            
            var amount = float.Parse(amountInput);

            Console.WriteLine("Para birimi giriniz (USD / TRY): ");
            string from = Console.ReadLine();
            Console.WriteLine("Para birimi giriniz (USD / TRY): ");
            string to = Console.ReadLine();

            if ((from.ToUpper().Equals("TRY")|| from.ToUpper().Equals("USD")) && (to.ToUpper().Equals("USD") || to.ToUpper().Equals("TRY")))
            {
                if (from != to)
                {
                    string url = "https://api.exchangerate.host/convert?from={0}&to={1}&amount={2}";
                    string formattedUrl = string.Format(url, from, to, amount);

                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(formattedUrl);
                    webRequest.Method = "GET";
                    HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
                    string jsonString;
                    if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stream = httpWebResponse.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                            jsonString = reader.ReadToEnd();
                        }

                        try
                        {
                            ExchangeRate json = JsonConvert.DeserializeObject<ExchangeRate>(jsonString);
                            Console.WriteLine("Girdiğiniz tutar : " + amount + " " + from.ToUpper());

                            Console.WriteLine(json.Result + " " + to.ToUpper() + " değerindedir.");
                            Console.WriteLine("Döviz Kuru : " + json.Info.Rate);
                            Console.ReadKey();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        //Console.WriteLine(jsonString);
                    }
                    else
                    {
                        Console.WriteLine(httpWebResponse.StatusDescription);
                    }
                    httpWebResponse.Dispose();// ramden temizler
                }
                else
                {
                    Console.WriteLine("Para birimlerini eşit girdiniz. Sonuç: " + amount.ToString());
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Yanlış para birimi girdiniz.");
                Console.ReadKey();
            }

        }
        public static bool checkAmount(string amount)
        {
            float formattedAmount;
            return float.TryParse(amount, out formattedAmount);
        }

    }
}
