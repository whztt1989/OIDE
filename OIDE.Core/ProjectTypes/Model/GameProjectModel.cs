#region License

// Copyright (c) 2014 Huber Konrad
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using Module.Properties.Interface;
using Module.PFExplorer;
using Module.PFExplorer.Utilities;
using System.Xml.Serialization;
using Module.PFExplorer.Interface;
using System.Xml;
using System.Xml.Schema;
using OIDE.Scene.Model;
using OIDE.DAL;

namespace OIDE.Core
{
    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    [XmlInclude(typeof(ScenesModel))]
    [XmlInclude(typeof(FileCategoryModel))]
   [XmlInclude(typeof(SceneDataModel))]
   [XmlInclude(typeof(PhysicsObjectModel))]
   [Serializable]
    public class GameProjectModel : TextModel, IItem, ISerializableObj
    {
        private string result;

        [XmlAttribute]
        public Int32 ID { get; set; }
        [XmlAttribute]
        public String Name { get; set; }

        public String ContentID { get { return "GameProject"; } }
      

        private CollectionOfIItem m_Items;

        //public System.Xml.Schema.XmlSchema GetSchema() { return null; }

        //public void ReadXml(System.Xml.XmlReader reader)
        //{
        //    //reader.MoveToContent();
        //    //Name = reader.GetAttribute("Name");
        //    //Boolean isEmptyElement = reader.IsEmptyElement; // (1)
        //    //reader.ReadStartElement();
        //    //if (!isEmptyElement) // (1)
        //    //{
        //    //    Birthday = DateTime.ParseExact(reader.
        //    //        ReadElementString("Birthday"), "yyyy-MM-dd", null);
        //    //    reader.ReadEndElement();
        //    //}
        //}

        //public void WriteXml(System.Xml.XmlWriter writer)
        //{
        //    writer.WriteAttributeString("Name", Name);

        //    writer.WriteElementString("Items",
        //           Birthday.ToString("yyyy-MM-dd"));

        //    foreach (var item in m_Items)
        //    {
        //        writer.WriteElementString("Birthday",
        //            Birthday.ToString("yyyy-MM-dd"));
        //    }   
        //}


        [Browsable(false)]
        //[XmlArray("Items")]
       // [XmlArrayItem(typeof(PhysicsObjectModel)), XmlArrayItem(typeof(SceneDataModel)), XmlArrayItem(typeof(CategoryModel)), XmlArrayItem(typeof(ScenesModel))]
        //[XmlElement(typeof(ScenesModel))]
        //[XmlElement(typeof(CategoryModel))]
        //[XmlElement(typeof(SceneDataModel))]
        //[XmlElement(typeof(PhysicsObjectModel))]
        public CollectionOfIItem Items { get { return m_Items; } set { m_Items = value; } }

        [XmlIgnore]
        public Guid Guid { get; private set; }
      
        [Browsable(false)]
        [XmlIgnore]
        public List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();
                MenuItem miSave = new MenuItem() { Header = "Save" };
                list.Add(miSave);
                return list;
            }
        }

        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsExpanded { get; set; }
       
        [Browsable(false)]
        [XmlAttribute]
        public Boolean IsSelected { get; set; }

        [XmlIgnore]
        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }

         [XmlIgnore]
        public IItem Parent { get; private set; }

         [XmlIgnore]
        public ICommand RaiseConfirmation { get; private set; }
      //  public ICommand RaiseSelectAEF { get; private set; }

     //   public InteractionRequest<PSelectAEFViewModel> SelectAEFRequest { get; private set; }
         [XmlIgnore]
        public InteractionRequest<Confirmation> ConfirmationRequest { get; private set; }

        private void OnRaiseConfirmation()
        {
            this.ConfirmationRequest.Raise(
                new Confirmation { Content = "Confirmation Message", Title = "WPF Confirmation" },
                (cb) => { Result = cb.Confirmed ? "The user confirmed" : "The user cancelled"; });
        }


        //private void OnRaiseSelectAEF()
        //{
        //    this.SelectAEFRequest.Raise(
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

         [XmlIgnore]
        public string Result
        {
            get { return this.result; }
            set  {  this.result = value;  RaisePropertyChanged("Result");  }
        }

        public void SerializeObjectToXML()
        {
            ObjectSerialize.SerializeObjectToXML<GameProjectModel>(this, this.Location.ToString());
        }

        public Boolean Open() { return true; }
        public Boolean Save() { return true; }
        public Boolean Delete() { return true; }

        public GameProjectModel()
            : base(null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MDModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public GameProjectModel(ICommandManager commandManager, IMenuService menuService)
            : base(commandManager, menuService)
        {
            m_Items = new CollectionOfIItem();
            this.RaiseConfirmation = new DelegateCommand(this.OnRaiseConfirmation);
            this.ConfirmationRequest = new InteractionRequest<Confirmation>();
          //  this.SelectAEFRequest = new InteractionRequest<PSelectAEFViewModel>();
          //  this.RaiseSelectAEF = new DelegateCommand(this.OnRaiseSelectAEF);
            //ScenesModel scenes = new ScenesModel(this, commandManager, menuService) { Name = "Scenes" };
            //SceneDataModel scene = new SceneDataModel(scenes, commandManager, menuService) { Name = "Scene 1.xml" };
            //SceneDataModel sceneLogin = new SceneDataModel(scenes, commandManager, menuService) { Name = "Scene_Login.xml" };
            //SceneDataModel sceneCSelect = new SceneDataModel(scenes, commandManager, menuService) { Name = "Scene_CSelect.xml" };
            //scenes.Items.Add(sceneLogin);
            //scenes.Items.Add(sceneCSelect);

            //FileCategoryModel gameData = new FileCategoryModel(this, commandManager, menuService) { Name = "Asset Browser" };
            //FileCategoryModel objectsAB = new FileCategoryModel(gameData, commandManager, menuService) { Name = "Objects" };
            //FileCategoryModel objectAB1 = new FileCategoryModel(objectsAB, commandManager, menuService) { Name = "Floor" };
            //objectsAB.Items.Add(objectAB1);
            //gameData.Items.Add(objectsAB);
            //gameData.Items.Add(new FileCategoryModel(scene, commandManager, menuService) { Name = "Meshes" });
            //gameData.Items.Add(new FileCategoryModel(scene, commandManager, menuService) { Name = "Materials" });
            //gameData.Items.Add(new FileCategoryModel(scene, commandManager, menuService) { Name = "Sounds" });
            //gameData.Items.Add(new PhysicsObjectModel(scene, commandManager, menuService, 0) { Name = "PhysicObjects" });
            //m_Items.Add(gameData);

            FileCategoryModel fileAssets = new FileCategoryModel(this, commandManager, menuService) { Name = "AssetsArchive(ofg)" };
            m_Items.Add(fileAssets);
            fileAssets.Items.Add(new FileCategoryModel(fileAssets, commandManager, menuService) { Name = "Meshes" });
            fileAssets.Items.Add(new FileCategoryModel(fileAssets, commandManager, menuService) { Name = "Textures" });
            fileAssets.Items.Add(new FileCategoryModel(fileAssets, commandManager, menuService) { Name = "Sounds" });

            DBCategoryModel dbData = new DBCategoryModel(this, commandManager, menuService) { Name = "DBData" };

            DBCategoryModel dbRuntime = new DBCategoryModel(this, commandManager, menuService) { Name = "Runtime Data" };
            FileCategoryModel players = new FileCategoryModel(dbRuntime, commandManager, menuService) { Name = "Players" };
            FileCategoryModel player1 = new FileCategoryModel(players, commandManager, menuService) { Name = "Player1" };
            FileCategoryModel charsPlayer = new FileCategoryModel(player1, commandManager, menuService) { Name = "Characters" };
            FileCategoryModel char1Player = new FileCategoryModel(charsPlayer, commandManager, menuService) { Name = "Character 1" };
            charsPlayer.Items.Add(char1Player);
            player1.Items.Add(charsPlayer);
            players.Items.Add(player1);
            dbRuntime.Items.Add(players);
            dbData.Items.Add(dbRuntime);

            FileCategoryModel scriptMats = new FileCategoryModel(dbData, commandManager, menuService) { Name = "Materials (Scripts)" };
            FileCategoryModel mat1 = new FileCategoryModel(scriptMats, commandManager, menuService) { Name = "MaterialsScript1" };
            scriptMats.Items.Add(mat1);
             dbData.Items.Add(scriptMats);
           
            FileCategoryModel objects = new FileCategoryModel(dbData, commandManager, menuService) { Name = "Objects" };

            ScenesModel scenesProto = new ScenesModel(objects, commandManager, menuService) { Name = "Scenes" };
            SceneDataModel sceneProto1 = new SceneDataModel(scenesProto, commandManager, menuService) { Name = "Scene1.proto" };
            SceneDataModel sceneProto2 = new SceneDataModel(scenesProto, commandManager, menuService) { Name = "Scene2.proto" };
            scenesProto.Items.Add(sceneProto1);
            scenesProto.Items.Add(sceneProto2);
            objects.Items.Add(scenesProto);

            DBCategoryModel staticObjects = new DBCategoryModel(objects, commandManager, menuService) { Name = "Statics" };
            FileCategoryModel object1 = new FileCategoryModel(staticObjects, commandManager, menuService) { Name = "Floor" };
            staticObjects.Items.Add(object1);
            objects.Items.Add(staticObjects);

            FileCategoryModel chars = new FileCategoryModel(objects, commandManager, menuService) { Name = "Characters" };
            FileCategoryModel race = new FileCategoryModel(chars, commandManager, menuService) { Name = "Human" };
            FileCategoryModel male = new FileCategoryModel(race, commandManager, menuService) { Name = "Male" };
            race.Items.Add(male);
            chars.Items.Add(race);
            objects.Items.Add(chars);

            DBCategoryModel allPhysics = new DBCategoryModel(objects, commandManager, menuService) { Name = "Physics" };
             PhysicsObjectModel po1 = new PhysicsObjectModel(allPhysics, commandManager, menuService, 0) { Name = "pomChar1" };
             allPhysics.Items.Add(po1);
             objects.Items.Add(allPhysics);


          

        //    scenes.Items.Add(scene);
             dbData.Items.Add(objects);
             m_Items.Add(dbData);
         
          

            //------------- Scenes ----------------------
            //VMCategory cScenes = new VMCategory(,commandManager, menuService) { Name = "Scenes" };

            //p1.Items.Add(cScenes);

            //CVMScene sv = new CVMScene() { Name = "Scene 1" };
            //sv.Items.Add(new CVMCategory() { Name = "Cameras" });
            //sv.Items.Add(new CVMCategory() { Name = "Models" });
            //sv.Items.Add(new CVMCategory() { Name = "Sound" });
            //cScenes.Items.Add(sv);

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

         [XmlIgnore]
        public string HTMLResult { get; set; }

        public void SetHtml(string transform)
        {
            this.HTMLResult = transform;
            RaisePropertyChanged("HTMLResult");
        }
    }
}