using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Core.Services;
using Wide.Interfaces.Services;

namespace OIDE.VFS.VFS_Types.RootFileSystem
{
    public class ItemProvider
    {
        public CollectionOfIItem GetItems(string path)
        {
            var items = new CollectionOfIItem();

            var dirInfo = new DirectoryInfo(path);

            foreach (var directory in dirInfo.GetDirectories())
            {
                var item = new DirectoryItem
                {
                    Name = directory.Name,
                    ContentID = directory.FullName,
                    Items = GetItems(directory.FullName)
                };

                items.Add(item);
            }

            foreach (var file in dirInfo.GetFiles())
            {
                var item = new FileItem
                {
                    Name = file.Name,
                    ContentID = file.FullName
                };

                items.Add(item);
            }

            return items;
        }
    }

    public class Item : PItem
    {
     
    
    }

    public class FileItem : Item
    {

    }

    public class DirectoryItem : Item
    {
        public CollectionOfIItem Items { get; set; }

        public DirectoryItem()
        {
            Items = new CollectionOfIItem();
        }
    }
}
