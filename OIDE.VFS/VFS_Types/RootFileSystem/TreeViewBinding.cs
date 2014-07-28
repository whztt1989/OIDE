using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Path = directory.FullName,
                    Items = GetItems(directory.FullName)
                };

                items.Add(item);
            }

            foreach (var file in dirInfo.GetFiles())
            {
                var item = new FileItem
                {
                    Name = file.Name,
                    Path = file.FullName
                };

                items.Add(item);
            }

            return items;
        }
    }

    public class Item : IItem
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public void Drop(IItem item) { }

        public string ContentID { get; set; }
        public bool HasChildren { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsSelected { get; set; }
        public CollectionOfIItem Items { get; set; }
        public List<System.Windows.Controls.MenuItem> MenuOptions { get; set; }
        public IItem Parent { get; set; }
        public IUnityContainer UnityContainer { get; set; }

        public Boolean Closing() { return true; }
        public bool Create() { return true; }
        public bool Delete() { return true; }
        public bool Open(object id) { return true; }
        public bool Save() { return true; }

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
