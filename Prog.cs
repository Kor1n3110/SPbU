using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

class Program
{
    static void Main()
    {
        string html = File.ReadAllText("index.html");  // читаем файл
        string text = Regex.Replace(html, @"<[^>]*>", "\n"); // удаляем теги
        text = WebUtility.HtmlDecode(text); // преобразуем обычные символы
        string[] lines = text.Split('\n'); // разделяем на строки ("\n")
        using (StreamWriter writer = new StreamWriter("TextOnly.txt"))
        {
            foreach (string line in lines) // удаляем лишнее
            {
                string cleanLine = line.Trim();
                if (cleanLine.Length > 0) // проверка что строка не путсая
                {
                    writer.WriteLine(cleanLine); // если в строке есть текст записываем в файл
                }
            }
        }
        Console.WriteLine("Текст сохранён в TextOnly.txt"); // сохраняем результат в файл
    }
}
