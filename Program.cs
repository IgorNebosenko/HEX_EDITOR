using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEX_EDITOR
{
    class Program
    {
        static void Main(string[] args)
        {
            const int IWindowWidth = 85;
            const int IWindowsLength = 1024 + 32;

            //Format console
            Console.Title = "C# Hex editor";
            Console.WindowWidth = IWindowWidth;
            Console.SetBufferSize(IWindowWidth, IWindowsLength);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            try
            {
                string sSw = null;                                                                  //String of select menu
                do
                {
                    IHex ih = null;                                                                 //Make interface

                    ShowMenu();
                    sSw = Console.ReadLine();                                                       //Get what user select
                    Console.Clear();

                    switch (sSw)
                    {
                        case "1":
                            ih = new ShowFile();
                            break;
                        case "2":
                            ih = new CompareFiles();
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("You\'re select incorrect variant. For exit use 0");
                            break;

                    }
                    if (sSw == "1" || sSw == "2")
                    {
                        ih.SelectFiles();
                        ih.Show();
                    }

                    //End of while block
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                while (sSw != "0");                                                                 //While user select not 0 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine("1 - Open file in hex/dec");
            Console.WriteLine("2 - Compare 2 files");
            Console.WriteLine("0 - Exit");
        }
    }
}
