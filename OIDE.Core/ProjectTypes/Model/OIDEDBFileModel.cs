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
using System.Xml.Serialization;
using Microsoft.Practices.Unity;
using DAL;
using OIDE.Scene;
using OIDE.Scene.Model;
using DAL.MDB;
using Module.Protob.Utilities;
using System.Windows;
using Helper.Utilities.Event;
using Module.History.Service;
using System.Windows.Input;
using Wide.Core.Services;

namespace OIDE.Core
{
    public class OIDEDBFileModel : PItem, IDBFileModel
    {
        private IDAL m_DBI;
        private ICommand CmdDeleteDBFile;
     
   
        [Browsable(false)]
        [XmlIgnore]
        public override List<MenuItem> MenuOptions
        {
            get
            {
                List<MenuItem> list = new List<MenuItem>();

                MenuItem miAddItem = new MenuItem() { Header = "Add Item" };

                foreach (var type in CanAddThisItems)
                {
                    miAddItem.Items.Add(new MenuItem() { Header = type.Name, Command = new CmdAddExistingItemToDBFile(this), CommandParameter = type });
                }

                list.Add(miAddItem);
                list.Add(new MenuItem() { Header = "Save" });
                list.Add(new MenuItem() { Header = "Rename" });

                MenuItem miDelete = new MenuItem() { Command = CmdDeleteDBFile, Header = "Delete scene" };
                list.Add(miDelete);

                return list;
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public List<System.Type> CanAddThisItems { get; private set; }

        public Boolean Visible { get; set; }

       private Boolean mOpened;

        public override Boolean Open(IUnityContainer unityContainer, object id)
        {
            //GameStateListModel gameStates = new GameStateListModel(this, UnityContainer) { Name = "GameStates" };
            //gameStates.IsExpanded = true;
            //this.Items.Add(gameStates);

            m_DBI = new IDAL(unityContainer);


            ScenesListModel scenesProto = new ScenesListModel() { Parent = this, UnityContainer = UnityContainer, Name = "Scenes" };
            scenesProto.IsExpanded = true;
            this.Items.Add(scenesProto);

            DBCategoryModel dbRuntime = new DBCategoryModel() { Parent = this, UnityContainer = UnityContainer, Name = "Runtime Data (not needed now)" };
            //DBCategoryModel players = new DBCategoryModel(dbRuntime, unityContainer) { Name = "Players" };
            //DBCategoryModel player1 = new DBCategoryModel(players, unityContainer) { Name = "Player1" };
            //DBCategoryModel charsPlayer = new DBCategoryModel(player1, unityContainer) { Name = "Characters" };
            //DBCategoryModel char1Player = new DBCategoryModel(charsPlayer, unityContainer) { Name = "Character 1" };
            //charsPlayer.Items.Add(char1Player);
            //player1.Items.Add(charsPlayer);
            //players.Items.Add(player1);
            //dbRuntime.Items.Add(players);
            //this.Items.Add(dbRuntime);

            DBCategoryModel scriptMats = new DBCategoryModel() { Parent = this, UnityContainer = UnityContainer, Name = "Materials (Scripts)" };
            //DBCategoryModel mat1 = new DBCategoryModel(scriptMats, UnityContainer) { Name = "MaterialsScript1" };
            //scriptMats.Items.Add(mat1);
            this.Items.Add(scriptMats);

            DBCategoryModel objects = new DBCategoryModel() { Parent = this, UnityContainer = UnityContainer, Name = "GameEntites" };
            objects.IsExpanded = true;

            StaticObjectCategoyModel staticObjects = new StaticObjectCategoyModel() { Parent = objects, UnityContainer = UnityContainer, Name = "Statics" };

            //StaticObjectModel object1 = new StaticObjectModel(staticObjects, unityContainer) { Name = "Floor" };
            //staticObjects.Items.Add(object1);



            CharacterCategoryModel characterObjects = new CharacterCategoryModel() { Parent = objects, UnityContainer = UnityContainer, Name = "Characters" };
      //      CreatureCategoryModel creatureObjects = new CreatureCategoryModel(objects, UnityContainer) { Name = "Creatures" };
            //RaceModel race = new RaceModel(chars, unityContainer) { Name = "Human" };
            //GenderModel male = new GenderModel(race, unityContainer) { Name = "Male" };
            //race.Items.Add(male);
            //chars.Items.Add(race);

            //   PhysicCategoryModel allPhysics = new PhysicCategoryModel(objects, UnityContainer) { Name = "Physics" };
            //PhysicsObjectModel po1 = new PhysicsObjectModel(allPhysics, unityContainer) { Name = "pomChar1" };
            //allPhysics.Items.Add(po1);

            SpawnPointCategoryModel allSpawns = new SpawnPointCategoryModel() { Parent = objects, UnityContainer = UnityContainer, Name = "SpawnPoints" };


            SpawnPointCategoryModel allTrigger = new SpawnPointCategoryModel() { Parent = objects, UnityContainer = UnityContainer, Name = "Triggers" };
            SpawnPointCategoryModel allLights = new SpawnPointCategoryModel() { Parent = objects, UnityContainer = UnityContainer, Name = "Lights" };
            SpawnPointCategoryModel allSkies = new SpawnPointCategoryModel() { Parent = objects, UnityContainer = UnityContainer, Name = "Skies" };
            SpawnPointCategoryModel allTerrains = new SpawnPointCategoryModel() { Parent = objects, UnityContainer = UnityContainer, Name = "Terrains" };
            SpawnPointCategoryModel allSounds = new SpawnPointCategoryModel() { Parent = objects, UnityContainer = UnityContainer, Name = "Sounds" };


            StaticObjectCategoyModel DynamicObjects = new StaticObjectCategoyModel() { Parent = objects, UnityContainer = UnityContainer, Name = "Dynamics" };

            //PhysicCategoryModel allOgreObjects = new PhysicCategoryModel(objects, UnityContainer) { Name = "Ogre Objects" };
            //allOgreObjects.Items.Add(new StaticObjectModel(allOgreObjects, UnityContainer) { Name = "Plane" });
            //allOgreObjects.Items.Add(new StaticObjectModel(allOgreObjects, UnityContainer) { Name = "Cube" });

            try
            {
                IEnumerable<DAL.IDAL.EntityContainer> result = m_DBI.selectAllEntities();

                if (result != null)
                {
                    //select all Nodes
                    foreach (var gameEntity in result)
                    {
                        // ProtoType.Node nodeDeserialized = ProtoSerialize.Deserialize<ProtoType.Node>(node.Node.Data);

                        if (gameEntity.Entity.EntType == null)
                            continue;

                        switch ((EntityTypes)gameEntity.Entity.EntType)
                        {
                            case EntityTypes.NT_SpawnPoint:

                                SpawnPointModel tmpSpawnPoint = new SpawnPointModel()
                                {
                                    Parent = allSpawns,
                                    UnityContainer = UnityContainer,
                                    ContentID = "SpawnPointID:##:" + gameEntity.Entity.EntID,
                                    Name = gameEntity.Entity.Name ?? ("Noname SpawnPoint " + (int)gameEntity.Entity.EntID),
                                    DBData = gameEntity
                                };// Data = gameEntityDataDeserialized });

                                allSpawns.Items.Add(tmpSpawnPoint);
                                break;

                            case EntityTypes.NT_Static:

                                StaticObjectModel tmp = new StaticObjectModel()
                                {
                                    Parent = staticObjects,
                                    UnityContainer = UnityContainer,
                                    ContentID = "StaticID:##:" + gameEntity.Entity.EntID,
                                    Name = gameEntity.Entity.Name ?? ("Noname Static " + (int)gameEntity.Entity.EntID),
                                    DB_Entity = gameEntity
                                };// Data = gameEntityDataDeserialized });

                                staticObjects.Items.Add(tmp);
                                break;
                            case EntityTypes.NT_Character:

                                CharacterEntity tmpChar = new CharacterEntity()
                                {
                                    Parent = characterObjects,
                                    UnityContainer = UnityContainer,
                                    ContentID = "CharacterObjID:##:" + gameEntity.Entity.EntID,
                                    Name = gameEntity.Entity.Name ?? ("Noname CharObj " + (int)gameEntity.Entity.EntID),
                                    DB_Entity = gameEntity
                                };// Data = gameEntityDataDeserialized });

                                characterObjects.Items.Add(tmpChar);
                                break;
                            //case NodeTypes.Physic:

                            //    ProtoType.PhysicsObject dataPhysObj = new ProtoType.PhysicsObject();
                            //    if (gameEntity.Data != null)
                            //        dataPhysObj = ProtoSerialize.Deserialize<ProtoType.PhysicsObject>(gameEntity.Data);

                            //    allPhysics.Items.Add(new PhysicsObjectModel(allPhysics, UnityContainer, dataPhysObj, m_DBI) { ContentID = "PhysicID:##:" + gameEntity.EntID, Name = gameEntity.Name ?? ("Noname" + (int)gameEntity.EntID) });// Data = gameEntityDataDeserialized });

                            //    break;
                            case EntityTypes.NT_Camera:
                                //todo contentid for camera

                                //   SceneNodes = new SceneNodes() { NodeID = sNode.NodeID, EntID = sNode.Node.EntityID, SceneID = ID, Data = ProtoSerialize.Serialize(sNode.Node) };


                                //var itemCam = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == ""); // Search for Camera category
                                //if (itemCam.Any())
                                //    itemCam.First().SceneItems.Add(new CameraModel(itemCam.First(), UnityContainer) { Node = nodeDeserialized });

                                break;
                            case EntityTypes.NT_Light:

                                //var itemLight = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == "");
                                //if (itemLight.Any())
                                //    itemLight.First().SceneItems.Add(new LightModel(itemLight.First(), UnityContainer) { Node = nodeDeserialized });
                                break;
                        }

                    }
                }

                objects.Items.Add(staticObjects);
                objects.Items.Add(characterObjects);
            //    objects.Items.Add(creatureObjects);
               // objects.Items.Add(allPhysics);

                objects.Items.Add(allTrigger);
                objects.Items.Add(allSpawns);
                objects.Items.Add(allLights);
                objects.Items.Add(allSkies);
                objects.Items.Add(allTerrains);
                objects.Items.Add(allSounds);
                objects.Items.Add(DynamicObjects);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }

            //    scenes.Items.Add(scene);
            this.Items.Add(objects);

         //   RaceCategoryModel races = new RaceCategoryModel(this, UnityContainer) { Name = "Races" };

            //try
            //{
            //    IEnumerable<Race> result = m_DBI.selectAllRace();
            //    if (result != null)
            //    {
            //        //select all Races
            //        foreach (var race in result)
            //        {
            //            RaceModel newRace = new RaceModel() { DBData = race };

            //            races.Items.Add(newRace);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            //this.Items.Add(races);
           
            return mOpened = true;

        }
     
        public override  Boolean Delete() 
        {
            return true; 
        }

        public String Location { get; set; }

        public OIDEDBFileModel()
        {
            Name = "SQLiteDB";
         //   Parent = parent;
            Items = new CollectionOfIItem();
            this.CanAddThisItems = new List<Type>();
    
            MenuItem mib1a = new MenuItem();
            mib1a.Header = "Text.xaml";
            MenuOptions.Add(mib1a);

           // m_DBI = new IDAL();

      
            CanAddThisItems.Add(typeof(SceneDataModel));


            CmdDeleteDBFile = new CmdDeleteDBFile(this);

        }


        public override  Boolean Create(IUnityContainer unityContainer)
        {
            m_DBI = new IDAL(unityContainer);
            return true;
        }
        public override Boolean Save(object param) { return true; }
        public override Boolean Closing() { return true; }
        public override void Refresh() { }
        public override void Finish() { }
        public override void Drop(IItem item) { }
    }



    public class CmdAddExistingItemToDBFile : IHistoryCommand
    {
        private OIDEDBFileModel mpm;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Type t = parameter as Type;

            //to create the objects i need the parameter data!!!!
            //         mpm.Save();
            if (t.Name == "SceneDataModel")
            {
                mpm.Items.Add(new SceneDataModel() { Parent = mpm, UnityContainer = mpm.UnityContainer, Name = "new scene", ContentID = "SceneID:##:" });
                // Type instance = (Type)Activator.CreateInstance(t);
                // object obj = t.GetConstructor(new Type[] { }).Invoke(new object[] { });
                //   mpm.Items.Add(obj as IItem);
            }
        }

        public CmdAddExistingItemToDBFile(OIDEDBFileModel pm)
        {
            mpm = pm;
        }

        public bool CanRedo() { return true; }
        public bool CanUndo() { return true; }
        public void Redo() { }
        public string ShortMessage() { return "add item"; }
        public void Undo() { }

    }

    public class CmdDeleteDBFile : ICommand
    {
        private OIDEDBFileModel m_model;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_model.Items.Clear();
            m_model.Parent.Items.Remove(m_model);
            //IDAL dbI = new IDAL();

            m_model.Delete();
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

        public CmdDeleteDBFile(OIDEDBFileModel model)
        {
            m_model = model;
        }
    }
}