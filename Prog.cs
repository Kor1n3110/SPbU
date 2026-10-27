using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

class Program
{
    static void Main()
    {
        string html = File.ReadAllText("index.html"); // читаем файл

        // удаляем комментарии HTML
        html = Regex.Replace(html, @"<!--.*?-->", "", RegexOptions.Singleline);

        // удаляем JavaScript вместе с тегами script
        html = Regex.Replace(html, @"<script\b[^>]*>.*?</script\s*>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        // удаляем CSS вместе с тегами style
        html = Regex.Replace(html, @"<style\b[^>]*>.*?</style\s*>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        // разделяем ссылки, которые находятся на разных строках
        html = Regex.Replace(html, @"</a>[ \t]*\r?\n[ \t]*(?=<a\b)", "</a>\n", RegexOptions.IgnoreCase);
        // заменяем теги br на перенос строки
        html = Regex.Replace(html, @"<br\b[^>]*>", "\n", RegexOptions.IgnoreCase);
        // добавляем переносы после блочных элементов
        html = Regex.Replace(html, @"</(?:title|p|div|h[1-6]|li|tr|section|article|header|footer|blockquote)\s*>", "\n", RegexOptions.IgnoreCase);
        
        // удаляем оставшиеся теги с учетом кавычек в атрибутах
        string text = Regex.Replace(html, "<(?:\"[^\"]*\"|'[^']*'|[^'\">])*?>", " ");
        
        text = WebUtility.HtmlDecode(text); // преобразуем в обычные символы
        string[] lines = text.Split('\n'); // разделяем на строки
        using (StreamWriter writer = new StreamWriter("TextOnly.txt"))
        {
            foreach (string line in lines) // перебираем строки
            {
                // удаляем лишние пробелы
                string cleanLine = Regex.Replace(line, @"\s+", " ").Trim();

                if (cleanLine.Length > 0) // проверка что строка не пустая
                {
                    writer.WriteLine(cleanLine); // записываем текст в файл
                }
            }
        }

        Console.WriteLine("Текст сохранён в TextOnly.txt"); // выводим сообщение в консоль
    }
}
