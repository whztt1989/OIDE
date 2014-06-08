#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using test;
using TModul.PFExplorer.Interface;
using TModul.Properties.Interface;
using TModul.Properties.Types;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using XIDE.DBI;

namespace XIDE.Scene.Model
{
    //[Category("Conections")]
    //[Description("This property is a complex property and has no default editor.")]
    //[ExpandableObject]
    //public class TestObj 
    //{
    //    private object mData;

    //    public String name { get; set; }

    //    [ExpandableObject]
    //    public object Data2
    //    {
    //        get
    //        {
    //            using (StreamReader outputStream = new StreamReader("DataFile.dat"))
    //            {
    //                // read from a file
    //                TestObj test = new TestObj();
    //                mData = ProtoBuf.Serializer.Deserialize<Person>(outputStream.BaseStream);
    //                return mData;
    //            }
    //            return mData;
    //        }
    //        set { mData = value; }
    //    }
    //    public ObjectUserControlEditor Options { get;set;}
    //}

    internal class CmdSave : ICommand
    {
        private PhysicsObjectModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            CDBI dbI = new CDBI();

            // To serialize the hashtable and its key/value pairs,  
            // you must first open a stream for writing. 
            // In this case, use a file stream.
            using (MemoryStream inputStream = new MemoryStream())
            {
                // write to a file
                ProtoBuf.Serializer.Serialize(inputStream, mpm.Data);

                if (mpm.Data.id > 0)
                    dbI.updateEntityChar((uint)mpm.Data.id, inputStream.ToArray());
                else
                    dbI.insertEntityChar((uint)mpm.Data.id, inputStream.ToArray());
            }
        }

        public CmdSave(PhysicsObjectModel pm)
        {
            mpm = pm;
        }
    }

    internal class PhysicsObjectModel : TextModel , IItem
    {
        ICommand CmdSave;

        public String Name { get; set; }
        [Browsable(false)]
        public ObservableCollection<IItem> Items { get; private set; }
        public Guid Guid { get; private set; }
        [Browsable(false)]
        public List<MenuItem> MenuOptions { get {
            List<MenuItem> list = new List<MenuItem>();
            MenuItem miSave = new MenuItem() { Command = CmdSave, Header = "Save"};
            list.Add(miSave);
            return list;
        }}

        [Browsable(false)]
        public Boolean IsExpanded { get; set; }
        [Browsable(false)]
        public Boolean IsSelected { get; set; }
        public Boolean HasChildren { get { return Items.Count > 0 ? true : false; } }
 
        private Person mData;

        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public Person Data
        {
            get
            {

                return mData;
            }
            set { mData = value; }
        }
        //private object mtest;

        //[Editor(typeof(ByteArrayUserControlEditor), typeof(ByteArrayUserControlEditor))]
        //public object test
        //{
        //    get { return mtest; }
        //    set
        //    {
                
        //            //Console.WriteLine("setted:" + BitConverter.ToString(value));
        //            mtest = value;
                
        //    }
        //}

        public PhysicsObjectModel(UInt32 id,ICommandManager commandManager, IMenuService menuService)
            : base(commandManager, menuService)
        {
            CDBI dbI = new CDBI();
            Byte[] res = dbI.selectEntityChar(id);
            using (MemoryStream stream = new MemoryStream(res))
            {
                mData = ProtoBuf.Serializer.Deserialize<Person>(stream);
            }

            //using (StreamReader outputStream = new StreamReader("DataFile.dat"))
            //{
            //    // read from a file
            //    mData = ProtoBuf.Serializer.Deserialize<Person>(outputStream.BaseStream);
            //}

            CmdSave = new CmdSave(this);
           //  mtest = new Byte[10];
            Items = new ObservableCollection<IItem>();
            Guid = new Guid();
        }
    }
}