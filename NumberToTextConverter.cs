using System;
using System.Collections.Generic;
using System.Globalization;

namespace EbiRom.N2T
{
    public static class NumberToTextConverter
    {
        public enum Language
        {
            Persian,
            Arabic,
            English,
            Russian,
            French,
            German
        }

        private static readonly Dictionary<Language, CultureInfo> CultureMap = new Dictionary<Language, CultureInfo>
        {
            { Language.Persian, new CultureInfo("fa-IR") },
            { Language.Arabic, new CultureInfo("ar-SA") },
            { Language.English, new CultureInfo("en-US") },
            { Language.Russian, new CultureInfo("ru-RU") },
            { Language.French, new CultureInfo("fr-FR") },
            { Language.German, new CultureInfo("de-DE") }
        };

        private static readonly Dictionary<Language, string[]> ScaleWords = new Dictionary<Language, string[]>
        {
            {
                Language.Persian,
                new[] { "", "هزار", "میلیون", "میلیارد", "تریلیون" }
            },
            {
                Language.Arabic,
                new[] { "", "ألف", "مليون", "مليار", "تريليون" }
            },
            {
                Language.English,
                new[] { "", "thousand", "million", "billion", "trillion" }
            },
            {
                Language.Russian,
                new[] { "", "тысяча", "миллион", "миллиард", "триллион" }
            },
            {
                Language.French,
                new[] { "", "mille", "million", "milliard", "billion" }
            },
            {
                Language.German,
                new[] { "", "tausend", "Million", "Milliarde", "Billion" }
            }
        };

        private static readonly Dictionary<Language, string[]> DigitWords = new Dictionary<Language, string[]>
        {
            {
                Language.Persian,
                new[] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه", "ده", 
                       "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" }
            },
            {
                Language.Arabic,
                new[] { "صفر", "واحد", "اثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة",
                       "أحد عشر", "اثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر" }
            },
            {
                Language.English,
                new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
                       "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" }
            },
            {
                Language.Russian,
                new[] { "ноль", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять", "десять",
                       "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" }
            },
            {
                Language.French,
                new[] { "zéro", "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf", "dix",
                       "onze", "douze", "treize", "quatorze", "quinze", "seize", "dix-sept", "dix-huit", "dix-neuf" }
            },
            {
                Language.German,
                new[] { "null", "eins", "zwei", "drei", "vier", "fünf", "sechs", "sieben", "acht", "neun", "zehn",
                       "elf", "zwölf", "dreizehn", "vierzehn", "fünfzehn", "sechzehn", "siebzehn", "achtzehn", "neunzehn" }
            }
        };

        private static readonly Dictionary<Language, string[]> TensWords = new Dictionary<Language, string[]>
        {
            {
                Language.Persian,
                new[] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" }
            },
            {
                Language.Arabic,
                new[] { "", "", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" }
            },
            {
                Language.English,
                new[] { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" }
            },
            {
                Language.Russian,
                new[] { "", "", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" }
            },
            {
                Language.French,
                new[] { "", "", "vingt", "trente", "quarante", "cinquante", "soixante", "soixante-dix", "quatre-vingt", "quatre-vingt-dix" }
            },
            {
                Language.German,
                new[] { "", "", "zwanzig", "dreißig", "vierzig", "fünfzig", "sechzig", "siebzig", "achtzig", "neunzig" }
            }
        };

        private static readonly Dictionary<Language, string> HundredWord = new Dictionary<Language, string>
        {
            { Language.Persian, "صد" },
            { Language.Arabic, "مائة" },
            { Language.English, "hundred" },
            { Language.Russian, "сто" },
            { Language.French, "cent" },
            { Language.German, "hundert" }
        };

        public static string ConvertToText(long number, Language language, string suffix = "")
        {
            if (number == 0)
                return DigitWords[language][0] + GetSuffix(suffix, language);

            if (number < 0)
                return GetNegativeWord(language) + " " + ConvertToText(-number, language, suffix);

            var parts = new List<string>();
            var scaleIndex = 0;

            while (number > 0)
            {
                var chunk = (int)(number % 1000);
                if (chunk != 0)
                {
                    var chunkText = ConvertChunk(chunk, language);
                    var scaleWord = ScaleWords[language][scaleIndex];
                    if (!string.IsNullOrEmpty(scaleWord))
                    {
                        parts.Insert(0, chunkText + " " + scaleWord);
                    }
                    else
                    {
                        parts.Insert(0, chunkText);
                    }
                }
                number /= 1000;
                scaleIndex++;
            }

            var result = string.Join(" ", parts);
            return result.Trim() + GetSuffix(suffix, language);
        }

        public static string ConvertToText(decimal number, Language language, string suffix = "")
        {
            var integerPart = (long)Math.Truncate(number);
            var decimalPart = (int)((number - integerPart) * 100);

            var integerText = ConvertToText(integerPart, language, "");

            if (decimalPart == 0)
                return integerText + GetSuffix(suffix, language);

            var decimalText = ConvertToText(decimalPart, language, "");
            return $"{integerText} {GetDecimalWord(language)} {decimalText}{GetSuffix(suffix, language)}";
        }

        private static string ConvertChunk(int number, Language language)
        {
            if (number == 0)
                return "";

            var parts = new List<string>();

            // صدها
            var hundreds = number / 100;
            if (hundreds > 0)
            {
                if (hundreds == 1)
                {
                    parts.Add(HundredWord[language]);
                }
                else
                {
                    parts.Add(DigitWords[language][hundreds] + " " + HundredWord[language]);
                }
            }

            // دهگان و یکان
            var tensAndOnes = number % 100;
            if (tensAndOnes > 0)
            {
                if (tensAndOnes < 20)
                {
                    parts.Add(DigitWords[language][tensAndOnes]);
                }
                else
                {
                    var tens = tensAndOnes / 10;
                    var ones = tensAndOnes % 10;

                    if (ones == 0)
                    {
                        parts.Add(TensWords[language][tens]);
                    }
                    else
                    {
                        parts.Add(GetTensWithOnes(tens, ones, language));
                    }
                }
            }

            return string.Join(" ", parts);
        }

        private static string GetTensWithOnes(int tens, int ones, Language language)
        {
            return language switch
            {
                Language.Persian => $"{DigitWords[language][ones]} و {TensWords[language][tens]}",
                Language.Arabic => $"{DigitWords[language][ones]} و {TensWords[language][tens]}",
                Language.English => $"{TensWords[language][tens]}-{DigitWords[language][ones]}",
                Language.Russian => $"{TensWords[language][tens]} {DigitWords[language][ones]}",
                Language.French => GetFrenchTensWithOnes(tens, ones),
                Language.German => $"{DigitWords[language][ones]}und{TensWords[language][tens]}",
                _ => $"{TensWords[language][tens]} {DigitWords[language][ones]}"
            };
        }

        private static string GetFrenchTensWithOnes(int tens, int ones)
        {
            if (tens == 7 || tens == 9) // برای 70 و 90 در فرانسوی
            {
                var baseNumber = tens == 7 ? 60 : 80;
                var remaining = ones + (tens == 7 ? 10 : 20);
                if (remaining == 10) return "soixante-dix";
                if (remaining == 11) return "soixante et onze";
                return $"soixante-{DigitWords[Language.French][remaining - 60]}";
            }

            if (ones == 1 && tens != 8)
                return $"{TensWords[Language.French][tens]} et un";

            return ones == 0 ? TensWords[Language.French][tens] : $"{TensWords[Language.French][tens]}-{DigitWords[Language.French][ones]}";
        }

        private static string GetNegativeWord(Language language)
        {
            return language switch
            {
                Language.Persian => "منفی",
                Language.Arabic => "سالب",
                Language.English => "negative",
                Language.Russian => "минус",
                Language.French => "moins",
                Language.German => "minus",
                _ => "negative"
            };
        }

        private static string GetDecimalWord(Language language)
        {
            return language switch
            {
                Language.Persian => "ممیز",
                Language.Arabic => "فاصلة",
                Language.English => "point",
                Language.Russian => "запятая",
                Language.French => "virgule",
                Language.German => "Komma",
                _ => "point"
            };
        }

        private static string GetSuffix(string suffix, Language language)
        {
            if (string.IsNullOrEmpty(suffix))
                return "";

            return language switch
            {
                Language.Persian or Language.Arabic => $" {suffix}",
                _ => $" {suffix}"
            };
        }
    }
}