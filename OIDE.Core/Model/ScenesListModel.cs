#region License

// Copyright (c) 2013 Chandramouleswaran Ravichandran
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Module.PFExplorer.Interface;
using Module.Properties.Interface;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Module.PFExplorer;
using System.Windows.Input;
using System.Xml.Serialization;
using OIDE.Scene.Model;
using OIDE.Scene;
using Microsoft.Practices.Unity;
using OIDE.Scene.Interface.Services;


namespace OIDE.Core
{
    public class CmdCreateScene : ICommand
    {
        private ScenesListModel m_model;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ScenesListModel sceneCategoryModel = parameter as ScenesListModel;

            sceneCategoryModel.Items.Add(new SceneDataModel(sceneCategoryModel, sceneCategoryModel.UnityContainer) { Name = "Scene 1", ContentID = "SceneID:##:" }); //CreateScene();

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

        public CmdCreateScene(ScenesListModel model)
        {
            m_model = model;
        }
    }


    [System.Xml.Serialization.XmlInclude(typeof(ScenesListModel))]
    [System.Xml.Serialization.XmlInclude(typeof(FileCategoryModel))]
    [System.Xml.Serialization.XmlInclude(typeof(SceneDataModel))]
    [System.Xml.Serialization.XmlInclude(typeof(PhysicsObjectModel))]
    public class ScenesListModel : FileCategoryModel
    {
        //ICommandManager m_CommandManager;
        //IMenuService m_MenuService;
        private ICommand m_cmdCreateScene;

        [XmlIgnore]
        public IUnityContainer UnityContainer { get; private set; }

        public ScenesListModel()
            : base(null, null)
        {

        }

        public ScenesListModel(IItem parent, IUnityContainer container) :
            base(parent, container)
        {
            UnityContainer = container;
            //m_CommandManager = commandManager;
            //m_MenuService = menuService;
            ISceneService sceneService = container.Resolve<ISceneService>();
            OIDE.DAL.IDAL iDAL = new OIDE.DAL.IDAL();
            
            var allScenes =  iDAL.selectAllScenesDataOnly();

            if (allScenes != null)
            {
                foreach (var scene in allScenes)
                {
                    SceneDataModel sceneProto2 = new SceneDataModel(this, container)
                    { 
                        Name = "Scene_" + scene.SceneID,
                        ContentID = "SceneID:##:" + scene.SceneID, 
                        SceneID = scene.SceneID ,
                        SceneData = scene
                    };

                    sceneService.AddScene(sceneProto2);
                    this.Items.Add(sceneProto2);
                }
            }

            MenuOptions = new List<MenuItem>();
            m_cmdCreateScene = new CmdCreateScene(this);
            MenuItem mib1a = new MenuItem() { Header = "Create Scene", Command = m_cmdCreateScene, CommandParameter = this };
            MenuOptions.Add(mib1a);

            //SceneDataModel sceneProto1 = new SceneDataModel(this, container) { Name = "Scene1.proto", ContentID = "SceneID:##:0" };
            //sceneService.AddScene(sceneProto1);
     
            //this.Items.Add(sceneProto1);
           
        }
    }
}