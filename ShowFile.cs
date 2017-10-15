using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HEX_EDITOR
{
    class ShowFile : IHex
    {
        const int ISizeBlock = 4096;                                                                //Size of block for read
        const int ILengthString = 8;                                                                //Number of bytes in string

        string sPath = null;                                                                        //Path to file
        long iCurrPos = 0;                                                                          //Current position seek in file

        FileStream fs = null;


        public void SelectFiles()
        {
            Console.WriteLine("Write part to file, or drag'n drop to this window");
            this.sPath = Console.ReadLine();

            //If file drag'n drop to this window - path may has "", so need delete them
            while (this.sPath.Contains('\"'))
                this.sPath = this.sPath.Replace("\"", "");

            //Now tests is exist file on this path
            if (!File.Exists(this.sPath))
                throw new FileNotFoundException();

            //So if file exists - try to open for read this file
            this.fs = new FileStream(this.sPath, FileMode.Open, FileAccess.Read);
        }

        public void CloseFiles()
        {
            if (this.fs != null)
            {
                this.fs.Close();
                this.fs = null;
            }
        }

        public void Show()
        {
            try
            {
                byte[] cBuf = new byte[ISizeBlock];
                while (this.iCurrPos < this.fs.Length)
                {
                    long iNumRead = this.fs.Read(cBuf, 0, ISizeBlock);

                    for (var i = 0; i < iNumRead; i += 8)
                    {
                        Console.Write((i + this.iCurrPos).ToString("X").PadLeft(16, '0') + "  ");   //Adress

                        for (var j = 0; j < ILengthString && j + i < iNumRead; ++j)                 //Shows values in hex
                        {
                            Console.Write(cBuf[i + j].ToString("X").PadLeft(2, '0') + " ");
                        }

                        Console.Write("\t");


                        for (var j = 0; j < ILengthString && i + j < iNumRead; ++j)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            if (cBuf[i + j] == '\a')
                                Console.Write('A');
                            else if (cBuf[i + j] == '\b')
                                Console.Write('B');
                            else if (cBuf[i + j] == '\f')
                                Console.Write('F');
                            else if (cBuf[i + j] == '\n')
                                Console.Write('N');
                            else if (cBuf[i + j] == '\r')
                                Console.Write('R');
                            else if (cBuf[i + j] == '\t')
                                Console.Write('T');
                            else if (cBuf[i + j] == '\v')
                                Console.Write('V');
                            else if (cBuf[i + j] == '\0')
                                Console.Write('0');
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write((char)cBuf[i + j]);
                            }
                        }
                        Console.Write("\n");

                        Console.ForegroundColor = ConsoleColor.White;

                        //stop if show 1 kB
                        if (i % (ISizeBlock / 4) == 0 && i + this.iCurrPos != 0)
                        {
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                        }
                    }

                    this.iCurrPos += iNumRead;
                } 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseFiles();
            }
        }
    }
}
