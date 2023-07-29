﻿using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PCParser.DTOs;

namespace PCParser
{
    public class ClassParsRAM : BaseParseClass
    {
        public List<RAMparse> _rams = new();
        public async Task StartParseRAM()
        {

            List<string> listref = GetListRef();
            Console.WriteLine("Начало парсинга RAM");
            var manufacturerSelector = "td#tdsa2943";
            var modelSelector = "td#tdsa2944";                
            var MemorySelector = "td#tdsa3360";
            var DDRSelector = "td#tdsa2510";
            var priceSelector = "a.add_to_cart.btn.btn-t-0.btn-c-6.CanBeSold.pc-component";

            foreach (string link in listref)
            {
                RAMparse _ram = new();
                using var doc = GetPage(link);

                _ram.Manufacturer =  doc.QuerySelector(manufacturerSelector)?.TextContent ?? "n/a";

                _ram.Model = doc.QuerySelector(modelSelector).FirstChild?.TextContent ?? "n/a";

                _ram.DDR = doc.QuerySelector(DDRSelector).FirstChild?.TextContent ?? "n/a";

                try {_ram.Memory = ushort.Parse(Regex.Replace(doc.QuerySelector(MemorySelector).FirstChild.TextContent, @"\D+", "")); }
                catch (Exception ex) { _ram.Memory = 0; }

                try { _ram.Price = decimal.Parse(Regex.Replace(doc.QuerySelector(priceSelector)?.TextContent, @"\D+", "")); }
                catch (Exception ex) { _ram.Price = 0; }

                Console.WriteLine(_ram.Manufacturer);
                Console.WriteLine(_ram.Model);
                Console.WriteLine(_ram.DDR);
                Console.WriteLine(_ram.Memory);
                Console.WriteLine(_ram.Price);
                Console.WriteLine(new string('.', 80));

                _rams.Add(_ram);
            }
            Console.WriteLine("Конец работы");
            //foreach (RAMparse ram in _rams)
            //{
            //    Console.WriteLine(ram.Manufacturer);
            //    Console.WriteLine(ram.Model);
            //    Console.WriteLine(ram.DDR);
            //    Console.WriteLine(ram.Memory);
            //    Console.WriteLine(ram.Price);
            //    Console.WriteLine(new string('.', 80));
            //}
            //for (int i = 0; i < RAMs.Count; i++)
            //{
            //    Console.WriteLine($"Производитель : {RAMs[i].Manufacturer}");
            //    Console.WriteLine($"Модель : {RAMs[i].Model}");
            //    Console.WriteLine($"Объем памяти : {RAMs[i].Memory}");
            //    Console.WriteLine($"Тип памяти : {RAMs[i].DDR}");
            //    Console.WriteLine($"Цена : {RAMs[i].Price}");
            //    Console.WriteLine("================================================================");
            //}

        }



    }
}
