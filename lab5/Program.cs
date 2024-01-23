using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    internal class Program
    {
        class TvNote 
        {
            public TvNote(string tvManufacturer, string tvModel, long qty) 
            {
                this.tvManufacturer = tvManufacturer;
                this.tvModel = tvModel;
                this.qty = qty;
            }
            private string tvManufacturer;
            private string tvModel;
            private long qty;

            public string TvManufacturer { get => tvManufacturer; set => tvManufacturer = value; }
            public string TvModel { get => tvModel; set => tvModel = value; }
            public long Qty { get => qty; set => qty = value; }
        }
        static void Main(string[] args)
        {
            string data;
            FileStream fstr = null;
            StreamReader reader = null;
            StreamWriter writer = null;
            string filePath = "C:\\Users\\dzmikhalchuk\\source\\repos\\lab5\\files\\file.txt";

        Console.WriteLine("Vyberite punkt:" +
                "\n 1 - Add new record" +
                "\n 2 - Show all sold tv's" +
                "\n 3 - Show all sold tv's by manufacturer" +
                "\n 4 - Delete record" +
                "\n 5 - Exit"); 

            String choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter tv manufacturer:");
                    String manufacturer = Console.ReadLine();
                    Console.WriteLine("Enter tv model:");
                    String model = Console.ReadLine();
                    Console.WriteLine("Enter quantity:");
                    long qty = System.Int64.Parse(Console.ReadLine());

                    TvNote tvNote = new TvNote(manufacturer, model, qty);

                    fstr = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fstr.Seek(0, SeekOrigin.End);

                    try
                    {
                        writer = new StreamWriter(fstr);
                        writer.WriteLine(tvNote.TvManufacturer + " " + tvNote.TvModel + " " + tvNote.Qty.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        writer.Close();
                        fstr.Close();
                    }

                    break;
                case "2":
                    try
                    {
                        reader = new StreamReader(filePath);
                        data = reader.ReadLine();

                        while (data != null)
                        {
                            Console.WriteLine(data);
                            data = reader.ReadLine();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        reader.Close();
                    }
                    break;
                case "3":
                    Console.WriteLine("Show only manufacturer:");
                    String manShow = Console.ReadLine();
                    try
                    {
                        reader = new StreamReader(filePath);
                        data = reader.ReadLine();

                        while (data != null)
                        {
                            if (data.Contains(manShow))
                            {
                                Console.WriteLine(data);
                            }
                            data = reader.ReadLine();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        reader.Close();
                    }
                    break;
                case "4":
                    Console.WriteLine("Delete model:");
                    String delete = Console.ReadLine();

                    var tempFile = Path.GetTempFileName();
                    var linesToKeep = File.ReadLines(filePath).Where(l => !l.Contains(delete));

                    File.WriteAllLines(tempFile, linesToKeep);

                    File.Delete(filePath);
                    File.Move(tempFile, filePath);

                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine("Please choose correct option (1 - 5)");
                    break;
            }
        }
    }
}
