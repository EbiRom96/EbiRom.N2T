# EbiRom.N2T

یک کتابخانه قدرتمند و چند-platform برای تبدیل اعداد به متن به زبان‌های مختلف با پشتیبانی از تمام نسخه‌های دات نت و دات نت کور.

## 📦 ویژگی‌ها

- ✅ پشتیبانی از ۶ زبان: فارسی، عربی، انگلیسی، روسی، فرانسوی، آلمانی
- ✅ سازگاری با تمام نسخه‌های دات نت (NET Framework.) و دات نت کور (NET Core.)
- ✅ پشتیبانی از اعداد صحیح و اعشاری
- ✅ قابلیت افزودن پسوند اختیاری به انتهای متن
- ✅ پشتیبانی از اعداد منفی
- ✅ سبک و بهینه شده برای performance

## 📖 راهنمای استفاده

# تبدیل اعداد صحیح
csharp
using EbiRom.N2T;

## تبدیل به فارسی
var result = NumberToTextConverter.ConvertToText(1234567, NumberToTextConverter.Language.Persian, "ریال");
// نتیجه: "یک میلیون و دویست و سی و چهار هزار و پانصد و شصت و هفت ریال"

## تبدیل به انگلیسی
var englishResult = NumberToTextConverter.ConvertToText(1234567, NumberToTextConverter.Language.English, "Dollars");
// نتیجه: "one million two hundred thirty-four thousand five hundred sixty-seven Dollars"

## تبدیل به عربی
var arabicResult = NumberToTextConverter.ConvertToText(1234567, NumberToTextConverter.Language.Arabic, "دينار");
// نتیجه: "واحد مليون و مئتان و أربعة و ثلاثون ألف و خمسمائة و سبعة و ستون دينار"
تبدیل اعداد اعشاری
csharp
using EbiRom.N2T;

## تبدیل عدد اعشاری به فارسی
var decimalResult = NumberToTextConverter.ConvertToText(1234.56m, NumberToTextConverter.Language.Persian, "تومان");
// نتیجه: "یک هزار و دویست و سی و چهار ممیز پنجاه و شش تومان"

## تبدیل عدد اعشاری به انگلیسی
var englishDecimal = NumberToTextConverter.ConvertToText(99.99m, NumberToTextConverter.Language.English);
// نتیجه: "ninety-nine point ninety-nine"

# تبدیل اعداد منفی
csharp
using EbiRom.N2T;

## عدد منفی به فارسی
var negativeResult = NumberToTextConverter.ConvertToText(-1500, NumberToTextConverter.Language.Persian, "ریال");
// نتیجه: "منفی یک هزار و پانصد ریال"

## عدد منفی به انگلیسی
var englishNegative = NumberToTextConverter.ConvertToText(-500, NumberToTextConverter.Language.English);
// نتیجه: "negative five hundred"

# زبان‌های پشتیبانی شده
زبان	Culture	مثال
فارسی	fa-IR	یک میلیون و دویست و سی و چهار هزار
عربی	ar-SA	واحد مليون و مئتان و أربعة و ثلاثون ألف
انگلیسی	en-US	one million two hundred thirty-four thousand
روسی	ru-RU	один миллион двести тридцать четыре тысячи
فرانسوی	fr-FR	un million deux cent trente-quatre mille
آلمانی	de-DE	eine Million zweihundertvierunddreißigtausend

# پشتیبانی از فریم‌ورک‌ها
این کتابخانه از فریم‌ورک‌های زیر پشتیبانی می‌کند:

.NET Standard 2.0+
.NET Standard 2.1+
.NET Core 3.1+
.NET 5.0+
.NET 6.0+
.NET 7.0+
.NET 8.0+

# لایسنس
این پروژه تحت لایسنس MIT منتشر شده است.

### via NuGet
```bash
dotnet add package EbiRom.N2T