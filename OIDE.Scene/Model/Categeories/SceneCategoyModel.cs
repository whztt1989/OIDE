#region License

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
using OIDE.Scene.Model;
using OIDE.IDAL;

namespace OIDE.Scene
{
    public class SceneCategoryModel : DBTableModel, ISceneItem, IDBFileItem
    {
        private ICommand m_cmdCreateScene;

        [XmlIgnore]
        [Browsable(false)]
        public ISceneItem SelectedItem { get; set; }

        /// <summary>
        /// override for serializable
        /// </summary>
        [Browsable(false)]
        public override CollectionOfIItem Items { get { return base.Items; } set { base.Items = value; } }

        [Browsable(false)]
        [XmlIgnore]
        public CollectionOfISceneItem SceneItems { get; private set; }

        public Boolean SaveToDB()
        {
            foreach (var item in Items)
            {
                var dbFileItem = item as IDBFileItem;
                if (dbFileItem != null)
                {
                    dbFileItem.SaveToDB();
                }
            }

            return true;
        }

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

        public Boolean Create(IUnityContainer unityContainer) { return true; }
        public Boolean Open(IUnityContainer unityContainer, object id) { return true; }
        public Boolean Save(object param) { return true; }
        public void Refresh() { }
        public Boolean Delete() { return true; }

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
            SceneCategoryModel parent = parameter as SceneCategoryModel;

          //  sceneCategoryModel.Items.Add(new SceneCategoryModel() { Parent = sceneCategoryModel, UnityContainer = sceneCategoryModel.UnityContainer, Name = "Scene 1", ContentID = "SceneID:##:" }); //CreateScene();

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

            UInt32 id = 0;

            if (parent != null)
            {
                var tableModel = parent as DBTableModel;
                if (tableModel != null)
                {
                    id = tableModel.AutoIncrement();
                }
            }
            else
                return;

            parent.IsDirty = true;

            if (id > 0)
            {
                SceneDataModel pom = new SceneDataModel() { Parent = parent, UnityContainer = parent.UnityContainer, Name = "Scene Obj NEW", ContentID = "SceneID:##:" + id, SceneID = id };

                pom.Create(parent.UnityContainer);
                parent.Items.Add(pom);

                //  ISceneService sceneService = parent.UnityContainer.Resolve<ISceneService>();
            }
            else
            {
                parent.UnityContainer.Resolve<ILoggerService>().Log("Error: CmdCreateScene id =  (" + id.ToString() + ")", LogCategory.Error, LogPriority.High);
            }
        }

        public CmdCreateScene(SceneCategoryModel model)
        {
            m_model = model;
        }
    }


}