﻿#region License

//The MIT License (MIT)

//Copyright (c) 2014 Konrad Huber

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

#endregion

using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using Module.Properties.Interface;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.Xml.Serialization;
using Microsoft.Practices.Unity;
using Wide.Core.Services;

namespace OIDE.IDAL
{

    public class DBTableModel : PItem
    {
        private UInt32 m_AutoIncID;

        /// <summary>
        /// override for serializable
        /// </summary>
        [Browsable(false)]
        public override CollectionOfIItem Items { get { return base.Items; } set { base.Items = value; } }


        /// <summary>
        /// current max incremented id for this table
        /// </summary>
        [Description("current max incremented id for this table")]
        public UInt32 IncID { get { return m_AutoIncID; } set { m_AutoIncID = value; RaisePropertyChanged("IncID"); } }

        /// <summary>
        /// increment autoincrement id +1 
        /// </summary>
        public UInt32 AutoIncrement()
        {
            m_AutoIncID = m_AutoIncID + 1;

            RaisePropertyChanged("IncID");

            return m_AutoIncID;
        }
    }
}