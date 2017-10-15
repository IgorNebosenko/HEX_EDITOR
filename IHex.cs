using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEX_EDITOR
{
    interface IHex
    {
        void SelectFiles();                                                                         //Select files
        void CloseFiles();                                                                          //Close files
        void Show();                                                                                //Show what file has
    }
}
