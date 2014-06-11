﻿#region License

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
using System.Windows.Controls;
using OIDE.DAL.Interface;
using TModul.Properties.Interface;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using OIDE.DAL.Interface.Services;

namespace OIDE.DAL.Model
{
    internal class CharacterModel : TextModel , ISceneItem
    {
        public String Name { get; set; }

        [Browsable(false)]
        public ObservableCollection<IItem> Items { get; private set; }
        public Guid Guid { get; private set; }
        public List<MenuItem> MenuOptions { get; private set; }
        public Boolean IsExpanded { get; set; }
        public Boolean IsSelected { get; set; }
        public Boolean HasChildren { get { return Items.Count > 0 ? true : false; } }

        public CharacterModel(ICommandManager commandManager, IMenuService menuService)
            : base(commandManager, menuService)
        {
            Items = new ObservableCollection<IItem>();
            Guid = new Guid();
        }
    }
}