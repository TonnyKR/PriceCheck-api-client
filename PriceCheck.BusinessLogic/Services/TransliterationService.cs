using PriceCheck.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCheck.BusinessLogic.Services
{
    public class TransliterationService : ITransliterationService
    {
        private Dictionary<string, string> translitSymbols;
        private string transliterationName;
        public TransliterationService() 
        {
            translitSymbols = new Dictionary<string, string>()
            {
                {"а", "a" },
                {"б", "b" },
                {"в", "v" },
                {"г", "h" },
                {"ґ", "g" },
                {"д", "d" },
                {"е", "e" },
                {"є", "ye" },
                {"ж", "zh" },
                {"з", "z" },
                {"и", "y" },
                {"і", "i" },
                {"ї", "yi" },
                {"й", "y" },
                {"к", "k" },
                {"л", "l" },
                {"м", "m" },
                {"н", "n" },
                {"о", "o" },
                {"п", "p" },
                {"р", "r" },
                {"с", "s" },
                {"т", "t" },
                {"у", "u" },
                {"ф", "f" },
                {"х", "kh" },
                {"ц", "ts" },
                {"ч", "ch" },
                {"ш", "sh" },
                {"щ", "shch" },
                {"ь", "jh" },
                {"ю", "yu" },
                {"я", "ya" }
            };
        }
        public string Transliterate(string name)
        {
            foreach (var letter in name)
            {
                if (translitSymbols.ContainsKey(letter.ToString()))
                {
                    transliterationName += translitSymbols[letter.ToString()];
                }                
            }
            Console.WriteLine(transliterationName);
            return transliterationName;
        }
    }
}
