﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

using System.Collections.Generic;
using System.IO;
using WpfTreeViewBinding.Model;

namespace WpfTreeViewBinding
{
    public class ItemProvider
    {
        public List<Item> GetItems(string path)
        {
            var items = new List<Item>();

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
}
namespace WpfTreeViewBinding.Model
{
    public class Item
    {
        public string Name { get; set; }
        public string Path { get; set; }
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
