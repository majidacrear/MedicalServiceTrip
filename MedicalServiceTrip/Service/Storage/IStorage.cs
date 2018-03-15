using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Storage
{
    public interface IStorage
    {
        bool StoreFile(string fileName,string pathToStore, Stream data);
    }
}
