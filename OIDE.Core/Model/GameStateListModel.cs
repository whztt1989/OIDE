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
using System.Windows.Controls;
using Module.PFExplorer.Interface;
using Module.Properties.Interface;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using Module.PFExplorer;
using System.Windows.Input;
using System.Xml.Serialization;
using Microsoft.Practices.Unity;
using DAL;
using OIDE.Core.Model;
using Helper.Utilities.Event;


namespace OIDE.Core
{
    public class CmdCreateGameState : ICommand
    {
        private GameStateListModel m_model;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            GameStateListModel sceneCategoryModel = parameter as GameStateListModel;

            sceneCategoryModel.Items.Add(new GameStateModel(sceneCategoryModel, sceneCategoryModel.UnityContainer) { Name = "GameState 1", ContentID = "GameStateID:##:" }); //CreateGameState();

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

        public CmdCreateGameState(GameStateListModel model)
        {
            m_model = model;
        }
    }


    [System.Xml.Serialization.XmlInclude(typeof(GameStateListModel))]
    [System.Xml.Serialization.XmlInclude(typeof(FileCategoryModel))]
    [System.Xml.Serialization.XmlInclude(typeof(GameStateModel))]
  //  [System.Xml.Serialization.XmlInclude(typeof(PhysicsObjectModel))]
    public class GameStateListModel : FileCategoryModel
    {
        //ICommandManager m_CommandManager;
        //IMenuService m_MenuService;
        private ICommand m_cmdCreateGameState;

        [XmlIgnore]
        public IUnityContainer UnityContainer { get; private set; }

        public GameStateListModel()
            : base(null, null)
        {

        }

        public enum ControllerStateEvent 
        {
            INIT,
            DESTROY,
            LOADED ,
            CONTROLLER,
        }

        public class CtrlStateEvent : IObjectEvent
        {
            public ControllerStateEvent Event { get; set; }
            public String Task { get; set; }
        }

        public GameStateListModel(IItem parent, IUnityContainer container) :
            base(parent, container)
        {
            UnityContainer = container;
            //m_CommandManager = commandManager;
            //m_MenuService = menuService;
          //  IGameStateService sceneService = container.Resolve<IGameStateService>();
            IDAL iDAL = new IDAL();

            var gsctrl4 = new OIDE.Core.Model.GameStateModel() { Name = "XEditorState" };
            gsctrl4.ObjectEvents.Add(new CtrlStateEvent() { Event = ControllerStateEvent.INIT, Task = "init Scene 1" });
            gsctrl4.ObjectEvents.Add(new CtrlStateEvent() { Event = ControllerStateEvent.DESTROY, Task = "destroy Scene 1" });
            this.Items.Add(gsctrl4);

            var gsctrl = new OIDE.Core.Model.GameStateModel() { Name = "XTControllerState" };
            gsctrl.ObjectEvents.Add(new CtrlStateEvent() { Event = ControllerStateEvent.INIT, Task = "init Scene 1" });
            gsctrl.ObjectEvents.Add(new CtrlStateEvent() { Event = ControllerStateEvent.DESTROY, Task = "destroy Scene 1" });
            this.Items.Add(gsctrl);
            var gsctrl1 = new OIDE.Core.Model.GameStateModel() { Name = "XTUIState" };
            this.Items.Add(gsctrl1);
            var gsctrl2 = new OIDE.Core.Model.GameStateModel() { Name = "XTInputControllerState" };
            this.Items.Add(gsctrl2);

            //var allGameStates = null; //todo in db! iDAL.selectAllGameStates();

            //if (allGameStates != null)
            //{
            //    foreach (var scene in allGameStates)
            //    {
            //        GameStateModel sceneProto2 = new GameStateModel(this, container)
            //        { 
            //            Name = "GameState_" + scene.GameStateID,
            //            ContentID = "GameStateID:##:" + scene.GameStateID, 
            //            GameStateID = scene.GameStateID ,
            //          //todo  GameStateData = scene
            //        };

            //        sceneService.AddGameState(sceneProto2);
            //        this.Items.Add(sceneProto2);
            //    }
            //}

            MenuOptions = new List<MenuItem>();
            m_cmdCreateGameState = new CmdCreateGameState(this);
            MenuItem mib1a = new MenuItem() { Header = "Create GameState", Command = m_cmdCreateGameState, CommandParameter = this };
            MenuOptions.Add(mib1a);

            //GameStateDataModel sceneProto1 = new GameStateDataModel(this, container) { Name = "GameState1.proto", ContentID = "GameStateID:##:0" };
            //sceneService.AddGameState(sceneProto1);
     
            //this.Items.Add(sceneProto1);
           
        }
    }
}