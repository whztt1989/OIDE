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
using OIDE.Scene.Interface.Services;
using System.Xml.Serialization;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using OIDE.Scene.Interface;
using Wide.Core.Services;
using DAL;

namespace OIDE.Scene
{
    public class SceneCategoryModel : DBTableModel, ISceneItem
    {
        private ICommand m_cmdCreateScene;


        [Browsable(false)]
        [XmlIgnore]
        public CollectionOfISceneItem SceneItems { get; private set; }

        public void Drop(IItem item) { }

        [Browsable(false)]
        [XmlIgnore]
        public override List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem mib1a = new MenuItem() { Header = "Create Scene", Command = m_cmdCreateScene, CommandParameter = this };
                list.Add(mib1a);
                return list;
            }
        }

        public Boolean Visible { get; set; }

        public override Boolean Create(IUnityContainer unityContainer) { return true; }
        public override Boolean Open(IUnityContainer unityContainer, object id) { return true; }
        public override Boolean Save(object param) { return true; }
        public override void Refresh() { }
        public override Boolean Delete() { return true; }

        public SceneCategoryModel()
        {
            SceneItems = new CollectionOfISceneItem();
            m_cmdCreateScene = new CmdCreateScene(this);

        }
    }



    public class CmdCreateScene : ICommand
    {
        private SceneCategoryModel m_model;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            SceneCategoryModel sceneCategoryModel = parameter as SceneCategoryModel;

            sceneCategoryModel.Items.Add(new SceneCategoryModel() { Parent = sceneCategoryModel, UnityContainer = sceneCategoryModel.UnityContainer, Name = "Scene 1", ContentID = "SceneID:##:" }); //CreateScene();

            //IDAL dbI = new IDAL();

            //// To serialize the hashtable and its key/value pairs,  
            //// you must first open a stream for writing. 
            //// In this case, use a file stream.
            //using (MemoryStream inputStream = new MemoryStream())
            //{
            //    // write to a file
            //    ProtoBuf.Serializer.Serialize(inputStream, mpm.Data);

            //    if (mpm.ID > -1)
            //        dbI.updatePhysics(mpm.ID, inputStream.ToArray());
            //    else
            //        dbI.insertPhysics(mpm.ID, inputStream.ToArray());
            //}

            //DLL_Singleton.Instance.updateObject(0, (int)ObjType.Physic);
        }

        public CmdCreateScene(SceneCategoryModel model)
        {
            m_model = model;
        }
    }


}