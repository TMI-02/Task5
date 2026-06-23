using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TextCorrector
{
    class Program
    {
        static Dictionary<string, string> errorWords = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("    КОРРЕКТОР ТЕКСТОВЫХ ФАЙЛОВ");
            Console.WriteLine("========================================");
            Console.WriteLine();

            // Заполняем словарь ошибочных слов
            FillErrorDictionary();

            Console.WriteLine("Словарь ошибочных слов загружен.");
            Console.WriteLine("Всего слов в словаре: " + errorWords.Count);
            Console.WriteLine();

            // Меню работы со словарем
            bool dictionaryMenuExit = false;
            while (dictionaryMenuExit == false)
            {
                Console.WriteLine("МЕНЮ РАБОТЫ СО СЛОВАРЕМ:");
                Console.WriteLine("1. Показать все слова в словаре");
                Console.WriteLine("2. Добавить новое ошибочное слово");
                Console.WriteLine("3. Удалить слово из словаря");
                Console.WriteLine("4. Редактировать слово в словаре");
                Console.WriteLine("5. Очистить словарь");
                Console.WriteLine("6. Начать обработку файлов");
                Console.WriteLine("7. Выход");
                Console.Write("\nВыберите действие: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "1")
                {
                    ShowDictionary();
                }
                else if (choice == "2")
                {
                    AddWordToDictionary();
                }
                else if (choice == "3")
                {
                    RemoveWordFromDictionary();
                }
                else if (choice == "4")
                {
                    EditWordInDictionary();
                }
                else if (choice == "5")
                {
                    ClearDictionary();
                }
                else if (choice == "6")
                {
                    dictionaryMenuExit = true;
                    ProcessFiles();
                }
                else if (choice == "7")
                {
                    Console.WriteLine("До свидания!");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                }

                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void FillErrorDictionary()
        {
            // Добавляем ошибочные слова и их правильные варианты
            errorWords.Add("првиет", "привет");
            errorWords.Add("пирвет", "привет");
            errorWords.Add("превед", "привет");
            errorWords.Add("привт", "привет");

            errorWords.Add("спосибо", "спасибо");
            errorWords.Add("спасиба", "спасибо");
            errorWords.Add("спс", "спасибо");

            errorWords.Add("пожалйста", "пожалуйста");
            errorWords.Add("пжалуйста", "пожалуйста");
            errorWords.Add("пожалста", "пожалуйста");

            errorWords.Add("здраствуйте", "здравствуйте");
            errorWords.Add("здравствуй", "здравствуйте");
            errorWords.Add("здрасте", "здравствуйте");

            errorWords.Add("дасвидания", "до свидания");

            errorWords.Add("извените", "извините");
            errorWords.Add("извени", "извините");

            errorWords.Add("пажалуйста", "пожалуйста");
            errorWords.Add("пожалуста", "пожалуйста");

            errorWords.Add("какдила", "как дела");

            errorWords.Add("хороше", "хорошо");
            errorWords.Add("хороша", "хорошо");

            errorWords.Add("плоха", "плохо");

            errorWords.Add("севодня", "сегодня");
            errorWords.Add("седня", "сегодня");

            errorWords.Add("завтро", "завтра");

            errorWords.Add("вчерась", "вчера");

            errorWords.Add("понятна", "понятно");

            errorWords.Add("конешно", "конечно");
            errorWords.Add("канечно", "конечно");

            errorWords.Add("щас", "сейчас");
            errorWords.Add("ща", "сейчас");

            errorWords.Add("чиловек", "человек");
            errorWords.Add("чел", "человек");

            errorWords.Add("инет", "интернет");
            errorWords.Add("инетрнет", "интернет");

            errorWords.Add("комп", "компьютер");
            errorWords.Add("компутер", "компьютер");

            errorWords.Add("тилефон", "телефон");
            errorWords.Add("телехон", "телефон");

            errorWords.Add("магаз", "магазин");
            errorWords.Add("могазин", "магазин");

            errorWords.Add("робота", "работа");

            errorWords.Add("улеца", "улица");
            errorWords.Add("улитса", "улица");

            errorWords.Add("машына", "машина");

            errorWords.Add("денги", "деньги");
            errorWords.Add("денег", "деньги");

            errorWords.Add("времья", "время");
            errorWords.Add("времени", "время");

            errorWords.Add("грод", "город");
            errorWords.Add("горот", "город");
        }

        static void ShowDictionary()
        {
            if (errorWords.Count == 0)
            {
                Console.WriteLine("Словарь пуст.");
                return;
            }

            Console.WriteLine("Словарь ошибочных слов:");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Ошибочное слово -> Правильное слово");
            Console.WriteLine("----------------------------------------");

            int count = 0;
            foreach (KeyValuePair<string, string> pair in errorWords)
            {
                count++;
                Console.WriteLine(count + ". " + pair.Key + " -> " + pair.Value);
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Всего слов в словаре: " + errorWords.Count);
        }

        static void AddWordToDictionary()
        {
            Console.Write("Введите ошибочное слово: ");
            string errorWord = Console.ReadLine();

            if (string.IsNullOrEmpty(errorWord))
            {
                Console.WriteLine("Слово не может быть пустым.");
                return;
            }

            // Приводим к нижнему регистру
            errorWord = errorWord.ToLower();

            if (errorWords.ContainsKey(errorWord) == true)
            {
                Console.WriteLine("Такое слово уже есть в словаре.");
                Console.WriteLine("Правильный вариант: " + errorWords[errorWord]);
                return;
            }

            Console.Write("Введите правильный вариант слова: ");
            string correctWord = Console.ReadLine();

            if (string.IsNullOrEmpty(correctWord))
            {
                Console.WriteLine("Правильное слово не может быть пустым.");
                return;
            }

            errorWords.Add(errorWord, correctWord.ToLower());
            Console.WriteLine("Слово успешно добавлено в словарь!");
            Console.WriteLine("Добавлено: " + errorWord + " -> " + correctWord);
        }

        static void RemoveWordFromDictionary()
        {
            if (errorWords.Count == 0)
            {
                Console.WriteLine("Словарь пуст. Нечего удалять.");
                return;
            }

            ShowDictionary();
            Console.WriteLine();

            Console.Write("Введите ошибочное слово для удаления: ");
            string errorWord = Console.ReadLine();

            if (string.IsNullOrEmpty(errorWord))
            {
                Console.WriteLine("Слово не может быть пустым.");
                return;
            }

            errorWord = errorWord.ToLower();

            if (errorWords.ContainsKey(errorWord) == true)
            {
                string correctWord = errorWords[errorWord];
                errorWords.Remove(errorWord);
                Console.WriteLine("Слово удалено из словаря!");
                Console.WriteLine("Удалено: " + errorWord + " -> " + correctWord);
            }
            else
            {
                Console.WriteLine("Слово не найдено в словаре.");
            }
        }

        static void EditWordInDictionary()
        {
            if (errorWords.Count == 0)
            {
                Console.WriteLine("Словарь пуст. Нечего редактировать.");
                return;
            }

            ShowDictionary();
            Console.WriteLine();

            Console.Write("Введите ошибочное слово для редактирования: ");
            string errorWord = Console.ReadLine();

            if (string.IsNullOrEmpty(errorWord))
            {
                Console.WriteLine("Слово не может быть пустым.");
                return;
            }

            errorWord = errorWord.ToLower();

            if (errorWords.ContainsKey(errorWord) == true)
            {
                Console.WriteLine("Текущий правильный вариант: " + errorWords[errorWord]);
                Console.Write("Введите новый правильный вариант: ");
                string newCorrectWord = Console.ReadLine();

                if (string.IsNullOrEmpty(newCorrectWord))
                {
                    Console.WriteLine("Правильное слово не может быть пустым.");
                    return;
                }

                errorWords[errorWord] = newCorrectWord.ToLower();
                Console.WriteLine("Слово успешно обновлено!");
                Console.WriteLine("Обновлено: " + errorWord + " -> " + newCorrectWord);
            }
            else
            {
                Console.WriteLine("Слово не найдено в словаре.");
            }
        }

        static void ClearDictionary()
        {
            if (errorWords.Count == 0)
            {
                Console.WriteLine("Словарь уже пуст.");
                return;
            }

            Console.Write("Вы уверены, что хотите очистить словарь? (да/нет): ");
            string answer = Console.ReadLine();

            if (answer.ToLower() == "да")
            {
                errorWords.Clear();
                Console.WriteLine("Словарь очищен!");
            }
            else
            {
                Console.WriteLine("Очистка отменена.");
            }
        }

        static void ProcessFiles()
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("    НАЧАЛО ОБРАБОТКИ ФАЙЛОВ");
            Console.WriteLine("========================================");
            Console.WriteLine();

            Console.WriteLine("Текущий словарь содержит " + errorWords.Count + " слов.");
            Console.WriteLine();

            Console.Write("Введите путь к директории с текстовыми файлами: ");
            string directory = Console.ReadLine();

            if (Directory.Exists(directory) == false)
            {
                Console.WriteLine("Директория не существует.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Выполняется обработка файлов...");
            Console.WriteLine();

            // Получаем все текстовые файлы
            string[] files = Directory.GetFiles(directory, "*.txt");

            if (files.Length == 0)
            {
                Console.WriteLine("В указанной директории нет текстовых файлов.");
                return;
            }

            Console.WriteLine("Найдено файлов: " + files.Length);
            Console.WriteLine();

            int processedFiles = 0;
            int fixedWords = 0;
            int fixedPhones = 0;

            // Обрабатываем каждый файл
            foreach (string filePath in files)
            {
                Console.WriteLine("Обработка файла: " + Path.GetFileName(filePath));

                try
                {
                    // Читаем содержимое файла
                    string content = File.ReadAllText(filePath, Encoding.UTF8);

                    // Исправляем ошибочные слова
                    string correctedContent = FixErrorWords(content);
                    int wordsFixed = CountDifferences(content, correctedContent);
                    fixedWords = fixedWords + wordsFixed;

                    // Исправляем номера телефонов
                    string phoneCorrectedContent = FixPhoneNumbers(correctedContent);
                    int phonesFixed = CountPhoneDifferences(correctedContent, phoneCorrectedContent);
                    fixedPhones = fixedPhones + phonesFixed;

                    // Сохраняем исправленный текст
                    File.WriteAllText(filePath, phoneCorrectedContent, Encoding.UTF8);

                    processedFiles++;
                    Console.WriteLine("  Исправлено слов: " + wordsFixed + ", телефонов: " + phonesFixed);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("  Ошибка при обработке файла: " + ex.Message);
                }

                Console.WriteLine();
            }

            Console.WriteLine("========================================");
            Console.WriteLine("ОБРАБОТКА ЗАВЕРШЕНА!");
            Console.WriteLine("========================================");
            Console.WriteLine("Обработано файлов: " + processedFiles);
            Console.WriteLine("Всего исправлено слов: " + fixedWords);
            Console.WriteLine("Всего исправлено номеров телефонов: " + fixedPhones);
            Console.WriteLine();
        }

        static string FixErrorWords(string text)
        {
            string result = text;

            // Разбиваем текст на слова
            string[] words = text.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                // Убираем знаки препинания для проверки
                string cleanWord = word;
                string punctuation = "";

                // Проверяем, есть ли знаки препинания в конце слова
                if (word.Length > 0)
                {
                    char lastChar = word[word.Length - 1];
                    if (lastChar == '.' || lastChar == ',' || lastChar == '!' || lastChar == '?' || lastChar == ';' || lastChar == ':')
                    {
                        cleanWord = word.Substring(0, word.Length - 1);
                        punctuation = lastChar.ToString();
                    }
                }

                // Проверяем слово в словаре
                if (errorWords.ContainsKey(cleanWord.ToLower()))
                {
                    string correctWord = errorWords[cleanWord.ToLower()];

                    // Сохраняем регистр
                    if (char.IsUpper(cleanWord[0]))
                    {
                        correctWord = char.ToUpper(correctWord[0]) + correctWord.Substring(1);
                    }

                    words[i] = correctWord + punctuation;
                }
            }

            // Собираем текст обратно
            result = string.Join(" ", words);

            return result;
        }

        static string FixPhoneNumbers(string text)
        {
            // Регулярное выражение для поиска номеров в формате (012) 345-67-89
            string pattern = @"\((\d{3})\)\s*(\d{3})-(\d{2})-(\d{2})";
            string replacement = "+380 $1 $2 $3 $4";

            string result = Regex.Replace(text, pattern, replacement);

            return result;
        }

        static int CountDifferences(string oldText, string newText)
        {
            int count = 0;

            string[] oldWords = oldText.Split(' ');
            string[] newWords = newText.Split(' ');

            int minLength = oldWords.Length;
            if (newWords.Length < minLength)
            {
                minLength = newWords.Length;
            }

            for (int i = 0; i < minLength; i++)
            {
                if (oldWords[i] != newWords[i])
                {
                    count++;
                }
            }

            return count;
        }

        static int CountPhoneDifferences(string oldText, string newText)
        {
            int count = 0;

            // Находим все номера в старом тексте
            string pattern = @"\((\d{3})\)\s*(\d{3})-(\d{2})-(\d{2})";
            MatchCollection matches = Regex.Matches(oldText, pattern);

            count = matches.Count;

            return count;
        }
    }
}