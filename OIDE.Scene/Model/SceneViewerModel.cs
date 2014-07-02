using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OIDE.Scene.Interface.Services;
using Module.Properties.Interface;
using Wide.Core.TextDocument;
using Wide.Interfaces.Services;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Commands;
using OIDE.DAL;
using System.IO;
using System.Text.RegularExpressions;

namespace OIDE.Scene.Model
{



    /// <summary>
    /// Complete Scene description
    /// </summary>
    public class SceneViewerModel : TextModel
    {
         private SceneViewerModel m_model;

        public event EventHandler CanExecuteChanged;

        public Guid Guid { get; private set; }
        [Browsable(false)]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                //MenuItem miSave = new MenuItem() { Command = m_cmdCreateFile, Header = "Add File" };
                //list.Add(miSave);
                //MenuItem miDelete = new MenuItem() { Command = m_cmdDelete, Header = "Delete" };
                //list.Add(miDelete);
                return list;
            }
        }

        private string result;
  
        public ICommand RaiseConfirmation { get; private set; }
        public ICommand RaiseSelectAEF { get; private set; }

        //public InteractionRequest<PSelectAEFViewModel> SelectAEFRequest { get; private set; }
        public InteractionRequest<Confirmation> ConfirmationRequest { get; private set; }

        private void OnRaiseConfirmation()
        {
            this.ConfirmationRequest.Raise( new Confirmation { Content = "Confirmation Message", Title = "WPF Confirmation" },
                (cb) => { Result = cb.Confirmed ? "The user confirmed" : "The user cancelled"; });
        }


        //private void RaiseConfirmation()
        //{
        //    this.RaiseConfirmation.Raise(
        //        new PSelectAEFViewModel { Title = "Items" },
        //        (vm) =>
        //        {
        //            if (vm.SelectedItem != null)
        //            {
        //                Result = "The user selected: " + vm.SelectedItem;
        //            }
        //            else
        //            {
        //                Result = "The user didn't select an item.";
        //            }
        //        });
        //}

        public string Result
        {
            get
            {
                return this.result;
            }

            set
            {
                this.result = value;
                RaisePropertyChanged("Result");
            }
        }

        public void SetCurrentScene(IItem root)
        {

        }

        ISceneService m_SceneService;
        public SceneViewerModel(ICommandManager commandManager, IMenuService menuService, ISceneService sceneService)
            : base(commandManager, menuService)
        {
            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);

            m_SceneService = sceneService;

            IDAL dbI = new IDAL();
            int sceneID = 0;
              string[] split = Regex.Split(m_SceneService.SelectedScene.ContentID, ":##:");
              if (split.Count() == 2)
              {
                  string identifier = split[0];
                  string path = split[1];
                  if (identifier == "SceneID")
                  {
                      int.TryParse(path, out sceneID);
                  }
              }

              IEnumerable<OIDE.DAL.IDAL.SceneNodeContainer> result = dbI.selectSceneNodes(sceneID);

            //  Console.WriteLine(BitConverter.ToString(res));
            try
            {
                //select all Nodes
                foreach (var node in result)
                {
                    using (MemoryStream stream = new MemoryStream(node.Node.Data))
                    {
               //todo!!         m_SceneService.SelectedScene.SceneItems = ProtoBuf.Serializer.Deserialize<ProtoType.SceneNode>(stream);
                    }
                }
            }
            catch
            {
                m_SceneService.SelectedScene.SceneItems.Clear();
            }

            //---------------------------------------------
            //Scene Graph Tree
            //---------------------------------------------
            //SceneCategoryModel root = new SceneCategoryModel(null, commandManager, menuService) { Name = "RootNode" };
          
         //   Scene.SceneCategoryModel rootScene = new Scene.SceneCategoryModel(null, commandManager, menuService) { Name = "RootNode" };
            SceneDataModel scene = new SceneDataModel(null, commandManager, menuService) { Name = "Scene 1" };

            SceneCategoryModel controllers = new SceneCategoryModel(scene, commandManager, menuService) { Name = "SpawnPoints" };
            SceneCategoryModel controller1 = new SceneCategoryModel(scene, commandManager, menuService) { Name = "SpawnPoint 1" };
            controllers.Items.Add(controller1);
            scene.SceneItems.Add(controllers);

            //  CategoryModel dynamics = new CategoryModel(scene, commandManager, menuService) { Name = "Physics" };

            SceneCategoryModel triggers = new SceneCategoryModel(scene, commandManager, menuService) { Name = "Triggers" };
            SceneCategoryModel trigger1 = new SceneCategoryModel(scene, commandManager, menuService) { Name = "Trigger 1" };
            triggers.Items.Add(trigger1);
            scene.SceneItems.Add(triggers);


            StaticObjectCategoyModel statics = new StaticObjectCategoyModel(scene, commandManager, menuService) { Name = "Statics" };
            SceneCategoryModel obj1 = new SceneCategoryModel(scene, commandManager, menuService) { Name = "Object1" };
            SceneCategoryModel physics = new SceneCategoryModel(statics, commandManager, menuService) { Name = "Physics" };
            PhysicsObjectModel po1 = new PhysicsObjectModel(physics, commandManager, menuService, 0) { Name = "pomChar1" };
            physics.Items.Add(po1);
            obj1.Items.Add(physics);
            SceneCategoryModel obj2 = new SceneCategoryModel(scene, commandManager, menuService) { Name = "Floor (Obj)" };
            statics.Items.Add(obj2);
            statics.Items.Add(obj1);
            scene.SceneItems.Add(statics);

            SceneCategoryModel terrain = new SceneCategoryModel(scene, commandManager, menuService) { Name = "Terrain" };
            scene.SceneItems.Add(terrain);

            scene.RootItem = scene;
        //    scene.Items.Add(scene);
            sceneService.Scenes.Add(scene);

        }

        internal void SetLocation(object location)
        {
            this.Location = location;
            RaisePropertyChanged("Location");
        }

        internal void SetDirty(bool value)
        {
            this.IsDirty = value;
        }
    }
}
