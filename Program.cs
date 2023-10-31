using System;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Runtime;

namespace Practice_1_Files
{
  [Serializable]
  class Book
  {
    public string Name;
    public string Author;
    public int YearOfRelease;
    public Book()
    {
      this.Name = "name";
      this.Author = "Author";
      this.YearOfRelease = 1000;
    }
    public Book(string name, string author, int year)
    {
      this.Name = name;
      this.Author = author;
      this.YearOfRelease = year;
    }
  }
  class MyFile
  {
    private string filePath;
    public MyFile()
    {
      filePath = "";
    }
    public void SetPath(string path)
    { this.filePath = path; }

    public void CreateAndWriteInFile()
    {
      Console.WriteLine("Введите строку для записи в файл: ");
      string fileContent = Console.ReadLine();
      try
      {
        using (StreamWriter sw = new StreamWriter(this.filePath, false, System.Text.Encoding.Default))
        {
          sw.WriteLineAsync(fileContent);
        }

        Console.WriteLine("Запись выполнена");
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }

    public void ReadFromFile()
    {
      FileInfo fileInf = new FileInfo(filePath);
      if (fileInf.Exists)
      {
        try
        {
          using (StreamReader sr = new StreamReader(filePath))
          {
            Console.WriteLine("Данные из файла: " + sr.ReadToEnd());
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }
      else
      {
        Console.WriteLine("Файла с таким названием не существует.");
      }
      
    }
    public void DeleteFile()
    {
      FileInfo fileInf = new FileInfo(filePath);
      if (fileInf.Exists)
      {
        fileInf.Delete();
        Console.WriteLine("Файл удален.");
      }
      else
      {
        Console.WriteLine("Файла с таким названием не существует.");
      }
    }
  }

  class MyJSONFile
  {
    private string filePath;
    public MyJSONFile()
    {
      filePath = "";
    }
    public void SetPath(string path)
    { this.filePath = path; }

    public async Task  WriteInFile()
    {
      Book book1 = new Book()
      {
        Name = "Omniscient Reader's Viewpoint",
        Author = "SingSong",
        YearOfRelease = 2019
      };
      string json = JsonSerializer.Serialize(book1);

      using (StreamWriter sw = new StreamWriter(this.filePath, false))
      {
        sw.WriteLine(json);
      }

      Console.WriteLine("Данные записаны.");

      //Book book2 = new Book();
      //Console.Write("Введите название книги: ");
      //book2.Name = Console.ReadLine();
      //Console.Write("Введите автора: ");
      //book2.Author = Console.ReadLine();
      //Console.Write("Введите дату издания: ");
      //book2.YearOfRelease = Int16.Parse(Console.ReadLine());

      //JsonSerializer.Serialize<Book>(fs, book2);


    }

    public void ReadFromFile()
    {
      using (FileStream fs = new FileStream(this.filePath, FileMode.Open))
      {
        //all null ???
        Book restoredBook = JsonSerializer.Deserialize<Book>(fs);
        Console.WriteLine($"Name: {restoredBook.Name}  Author: {restoredBook.Author} Year: {restoredBook.YearOfRelease}");
      }

    }
    public void DeleteFile()
    {
      FileInfo fileInf = new FileInfo(filePath);
      if (fileInf.Exists)
      {
        fileInf.Delete();
        Console.WriteLine("Файл удален.");
      }
      else
      {
        Console.WriteLine("Файла с таким названием не существует.");
      }
    }
  }

  internal class Program
  {
    static void Main(string[] args)
    {
      //Temp directory for practice
      string dirName = @"D:\university\Курс_4\Разработка безопасного ПО\Practice_1_Directory";
      DirectoryInfo dirInfo = new DirectoryInfo(dirName);
      if (!dirInfo.Exists)
      {
        dirInfo.Create();
        Console.WriteLine("Каталог создан");
      }

      string menu = "Меню: \n" +
        "Введите \"i\", чтобы увидеть информацию о дисках\n" +
        "Введите \"f\" для работы с файлами\n" +
        "Введите \"j\" для работы с форматом JSON\n" +
        //"Введите \"м\", чтобы увидеть меню снова\n" + 
        "Введите \"0\", чтобы выйти из программы\n";
      //"Type \"x\" to work with XML format\n" +
      //"Type \"z\" to work with ZIP format\n" +
      //"Type \"0\" to exit the program.";

      Console.Clear();
      Console.WriteLine(menu);
      Console.Write("\tВведите команду: ");
      string format = Console.ReadLine();
      while (format != "0")
      {
        //if (format == "м") { Console.WriteLine(menu); }
        if (format == "i")
        {
          //Вывести информацию в консоль о логических дисках, именах, метке тома, размере и типе файловой системы.
          DriveInfo[] drives = DriveInfo.GetDrives();

          foreach (DriveInfo drive in drives)
          {
            Console.WriteLine($"Название: {drive.Name}");

            if (drive.IsReady)
            {
              Console.WriteLine($"Объем диска: {drive.TotalSize}");
              Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
              Console.WriteLine($"Метка: {drive.VolumeLabel}");
              Console.WriteLine($"Тип файловой системы: {drive.DriveType}");
            }
            Console.WriteLine();
          }

        }
        else if(format =="f")
        {
          string commandList = "Список комманд: \n" +
          "Введите \"1\", чтобы записать данные в файл\n" +
          "Введите \"2\", чтобы прочитать данные из файла\n" +
          "Введите \"3\", чтобы удалить файл\n" +
          "Введите \"0\", чтобы закончить работу с файлами\n";
          Console.WriteLine(commandList);
          Console.Write("\t\tВведите команду: ");
          string command = Console.ReadLine();
          MyFile file = new MyFile();

          while (command !=  "0")
          {
            if (command == "1")
            {
              Console.Write("Введите название файла: ");
              string fileName = Console.ReadLine();
              file.SetPath(dirInfo.FullName + $@"\{fileName}.txt");
              file.CreateAndWriteInFile();
            }
            else if (command == "2")
            {
              Console.Write("Введите название файла: ");
              string fileName = Console.ReadLine();
              file.SetPath(dirInfo.FullName + $@"\{fileName}.txt");
              file.ReadFromFile();
            }
            else if (command == "3")
            {
              Console.Write("Введите название файла: ");
              string fileName = Console.ReadLine();
              file.SetPath(dirInfo.FullName + $@"\{fileName}.txt");
              file.DeleteFile();
            }
            Console.Write("\t\tВведите следующую команду: ");
            command = Console.ReadLine();
          }

        }
        else if(format == "j")
        {
          string commandList = "Список комманд: \n" +
          "Введите \"1\", чтобы записать данные в JSON-файл\n" +
          "Введите \"2\", чтобы прочитать данные из JSON-файла\n" +
          "Введите \"3\", чтобы удалить JSON-файл\n" +
          "Введите \"0\", чтобы закончить работу с JSON-файлами\n";
          Console.WriteLine(commandList);
          Console.Write("\t\tВведите команду: ");
          string command = Console.ReadLine();
          MyJSONFile file = new MyJSONFile();

          while (command != "0")
          {
            if (command == "1")
            {
              Console.Write("Введите название файла: ");
              string fileName = Console.ReadLine();
              file.SetPath(dirInfo.FullName + $@"\{fileName}.json");
              file.WriteInFile();
            }
            else if (command == "2")
            {
              Console.Write("Введите название файла: ");
              string fileName = Console.ReadLine();
              file.SetPath(dirInfo.FullName + $@"\{fileName}.json");
              file.ReadFromFile();

            }
            else if (command == "3")
            {
              Console.Write("Введите название файла: ");
              string fileName = Console.ReadLine();
              file.SetPath(dirInfo.FullName + $@"\{fileName}.json");
              file.DeleteFile();
            }
            Console.Write("\t\tВведите следующую команду: ");
            command = Console.ReadLine();
          }
      

        }

        Console.Clear();
        Console.WriteLine(menu);
        Console.Write("\tВведите следующую команду: ");
        format = Console.ReadLine();

      }

      

      //string sourceFile = "D://test/book.pdf"; // исходный файл
      //string compressedFile = "D://test/book.gz"; // сжатый файл
      //string targetFile = "D://test/book_new.pdf"; // восстановленный файл

      //// создание сжатого файла
      //Compress(sourceFile, compressedFile);
      //// чтение из сжатого файла
      //Decompress(compressedFile, targetFile);

      Console.ReadKey();
    

      try
      {
        dirInfo.Delete(true);                                                                         
        Console.WriteLine("Каталог удален");
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }

    }

    public static void Compress(string sourceFile, string compressedFile)
    {
      // поток для чтения исходного файла
      using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
      {
        // поток для записи сжатого файла
        using (FileStream targetStream = File.Create(compressedFile))
        {
          // поток архивации
          using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
          {
            sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
            Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
          }
        }
      }
    }

    public static void Decompress(string compressedFile, string targetFile)
    {
      // поток для чтения из сжатого файла
      using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
      {
        // поток для записи восстановленного файла
        using (FileStream targetStream = File.Create(targetFile))
        {
          // поток разархивации
          using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
          {
            decompressionStream.CopyTo(targetStream);
            Console.WriteLine("Восстановлен файл: {0}", targetFile);
          }
        }
      }
    }

  }
}