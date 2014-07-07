using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using WpfTreeViewBinding.Model;

namespace OIDE.VFS
{
    //public class ItemProvider
    //{
    //    public List<Item> GetItems(string path)
    //    {
    //        var items = new List<Item>();

    //        var dirInfo = new DirectoryInfo(path);

    //        foreach (var directory in dirInfo.GetDirectories())
    //        {
    //            var item = new DirectoryItem
    //            {
    //                Name = directory.Name,
    //                Path = directory.FullName,
    //                Items = GetItems(directory.FullName)
    //            };

    //            items.Add(item);
    //        }

    //        foreach (var file in dirInfo.GetFiles())
    //        {
    //            var item = new FileItem
    //            {
    //                Name = file.Name,
    //                Path = file.FullName
    //            };

    //            items.Add(item);
    //        }

    //        return items;
    //    }
    //}
}
namespace WpfTreeViewBinding.Model
{
    public class Item : IItem
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public string ContentID { get; set; }
        public bool HasChildren { get; set; }
        public bool IsExpanded { get; set; }
        public bool IsSelected { get; set; }
        public CollectionOfIItem Items { get; set; }
        public List<System.Windows.Controls.MenuItem> MenuOptions { get; set; }
        public IItem Parent { get; set; }
        public IUnityContainer UnityContainer { get; set; }

        public bool Delete() { return true; }
        public bool Open() { return true; }
        public bool Save() { return true; }

    }
}

namespace WpfTreeViewBinding.Model
{
    public class FileItem : Item
    {

    }
}

namespace WpfTreeViewBinding.Model
{
    public class DirectoryItem : Item
    {
        public List<Item> Items { get; set; }

        public DirectoryItem()
        {
            Items = new List<Item>();
        }
    }
}
