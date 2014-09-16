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
using OIDE.DAL;
using OIDE.Scene;
using OIDE.Scene.Model;
using OIDE.DAL.MDB;
using Module.Protob.Utilities;

namespace OIDE.Core
{
    public class GameDBFileModel : DBFileModel, IItem
    {
        private IDAL m_DBI;

        public String Name { get; set; }
        public CollectionOfIItem Items { get; set; }

        public String ContentID { get; set; }


        [Browsable(false)]
        [XmlIgnore]
        public List<MenuItem> MenuOptions { get; protected set; }

        private Boolean mIsExpanded;

        public Boolean IsExpanded { get { return mIsExpanded; } set { mIsExpanded = value; if (!mOpened) Open(); } }
        public Boolean IsSelected { get; set; }
        public Boolean Enabled { get; set; }
        public Boolean Visible { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Boolean HasChildren { get { return Items != null && Items.Count > 0 ? true : false; } }

        [Browsable(false)]
        [XmlIgnore]
        public IItem Parent { get; private set; }


        private Boolean mOpened;

        public Boolean Open()
        {


            ScenesListModel scenesProto = new ScenesListModel(this, UnityContainer) { Name = "Scenes" };
            scenesProto.IsExpanded = true;
            this.Items.Add(scenesProto);

            DBCategoryModel dbRuntime = new DBCategoryModel(this, UnityContainer) { Name = "Runtime Data (not needed now)" };
            //DBCategoryModel players = new DBCategoryModel(dbRuntime, unityContainer) { Name = "Players" };
            //DBCategoryModel player1 = new DBCategoryModel(players, unityContainer) { Name = "Player1" };
            //DBCategoryModel charsPlayer = new DBCategoryModel(player1, unityContainer) { Name = "Characters" };
            //DBCategoryModel char1Player = new DBCategoryModel(charsPlayer, unityContainer) { Name = "Character 1" };
            //charsPlayer.Items.Add(char1Player);
            //player1.Items.Add(charsPlayer);
            //players.Items.Add(player1);
            //dbRuntime.Items.Add(players);
            //this.Items.Add(dbRuntime);

            DBCategoryModel scriptMats = new DBCategoryModel(this, UnityContainer) { Name = "Materials (Scripts)" };
            //DBCategoryModel mat1 = new DBCategoryModel(scriptMats, UnityContainer) { Name = "MaterialsScript1" };
            //scriptMats.Items.Add(mat1);
            this.Items.Add(scriptMats);

            DBCategoryModel objects = new DBCategoryModel(this, UnityContainer) { Name = "GameEntites" };
            objects.IsExpanded = true;

            StaticObjectCategoyModel staticObjects = new StaticObjectCategoyModel(objects, UnityContainer) { Name = "Statics" };

            //StaticObjectModel object1 = new StaticObjectModel(staticObjects, unityContainer) { Name = "Floor" };
            //staticObjects.Items.Add(object1);



            CharacterCategoryModel characterObjects = new CharacterCategoryModel(objects, UnityContainer) { Name = "Characters" };
            CreatureCategoryModel creatureObjects = new CreatureCategoryModel(objects, UnityContainer) { Name = "Creatures" };
            //RaceModel race = new RaceModel(chars, unityContainer) { Name = "Human" };
            //GenderModel male = new GenderModel(race, unityContainer) { Name = "Male" };
            //race.Items.Add(male);
            //chars.Items.Add(race);

            //   PhysicCategoryModel allPhysics = new PhysicCategoryModel(objects, UnityContainer) { Name = "Physics" };
            //PhysicsObjectModel po1 = new PhysicsObjectModel(allPhysics, unityContainer) { Name = "pomChar1" };
            //allPhysics.Items.Add(po1);

            SpawnPointCategoryModel allSpawns = new SpawnPointCategoryModel(objects, UnityContainer) { Name = "SpawnPoints" };
            
            
            SpawnPointCategoryModel allTrigger = new SpawnPointCategoryModel(objects, UnityContainer) { Name = "Triggers" };
            SpawnPointCategoryModel allLights = new SpawnPointCategoryModel(objects, UnityContainer) { Name = "Lights" };
            SpawnPointCategoryModel allSkies = new SpawnPointCategoryModel(objects, UnityContainer) { Name = "Skies" };
            SpawnPointCategoryModel allTerrains = new SpawnPointCategoryModel(objects, UnityContainer) { Name = "Terrains" };
            SpawnPointCategoryModel allSounds = new SpawnPointCategoryModel(objects, UnityContainer) { Name = "Sounds" };


            StaticObjectCategoyModel DynamicObjects = new StaticObjectCategoyModel(objects, UnityContainer) { Name = "Dynamics" };

            //PhysicCategoryModel allOgreObjects = new PhysicCategoryModel(objects, UnityContainer) { Name = "Ogre Objects" };
            //allOgreObjects.Items.Add(new StaticObjectModel(allOgreObjects, UnityContainer) { Name = "Plane" });
            //allOgreObjects.Items.Add(new StaticObjectModel(allOgreObjects, UnityContainer) { Name = "Cube" });

            try
            {


                IEnumerable<GameEntity> result = m_DBI.selectAllGameEntities();

                if (result != null)
                {
                    //select all Nodes
                    foreach (var gameEntity in result)
                    {
                        // ProtoType.Node nodeDeserialized = ProtoSerialize.Deserialize<ProtoType.Node>(node.Node.Data);

                        if (gameEntity.EntType == null)
                            continue;

                        switch ((ProtoType.EntityTypes)gameEntity.EntType)
                        {
                            case ProtoType.EntityTypes.NT_SpawnPoint:

                                SpawnPointModel tmpSpawnPoint = new SpawnPointModel(allSpawns, UnityContainer, m_DBI)
                                {
                                    ContentID = "SpawnPointID:##:" + gameEntity.EntID,
                                    Name = gameEntity.Name ?? ("Noname SpawnPoint " + (int)gameEntity.EntID),
                                    DBData = gameEntity
                                };// Data = gameEntityDataDeserialized });

                                allSpawns.Items.Add(tmpSpawnPoint);
                                break;

                            case ProtoType.EntityTypes.NT_Static:

                                StaticObjectModel tmp = new StaticObjectModel(staticObjects, UnityContainer, m_DBI)
                                {
                                    ContentID = "StaticID:##:" + gameEntity.EntID,
                                    Name = gameEntity.Name ?? ("Noname Static " + (int)gameEntity.EntID),
                                    DBData = gameEntity
                                };// Data = gameEntityDataDeserialized });

                                staticObjects.Items.Add(tmp);
                                break;
                            case ProtoType.EntityTypes.NT_Character:

                                CharacterCustomizeModel tmpChar = new CharacterCustomizeModel(characterObjects, UnityContainer, m_DBI)
                                {
                                    ContentID = "CharacterObjID:##:" + gameEntity.EntID,
                                    Name = gameEntity.Name ?? ("Noname CharObj " + (int)gameEntity.EntID),
                                    DBData = gameEntity
                                };// Data = gameEntityDataDeserialized });

                                characterObjects.Items.Add(tmpChar);
                                break;
                            //case NodeTypes.Physic:

                            //    ProtoType.PhysicsObject dataPhysObj = new ProtoType.PhysicsObject();
                            //    if (gameEntity.Data != null)
                            //        dataPhysObj = ProtoSerialize.Deserialize<ProtoType.PhysicsObject>(gameEntity.Data);

                            //    allPhysics.Items.Add(new PhysicsObjectModel(allPhysics, UnityContainer, dataPhysObj, m_DBI) { ContentID = "PhysicID:##:" + gameEntity.EntID, Name = gameEntity.Name ?? ("Noname" + (int)gameEntity.EntID) });// Data = gameEntityDataDeserialized });

                            //    break;
                            case ProtoType.EntityTypes.NT_Camera:
                                //todo contentid for camera

                                //   SceneNodes = new SceneNodes() { NodeID = sNode.NodeID, EntID = sNode.Node.EntityID, SceneID = ID, Data = ProtoSerialize.Serialize(sNode.Node) };


                                //var itemCam = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == ""); // Search for Camera category
                                //if (itemCam.Any())
                                //    itemCam.First().SceneItems.Add(new CameraModel(itemCam.First(), UnityContainer) { Node = nodeDeserialized });

                                break;
                            case ProtoType.EntityTypes.NT_Light:

                                //var itemLight = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == "");
                                //if (itemLight.Any())
                                //    itemLight.First().SceneItems.Add(new LightModel(itemLight.First(), UnityContainer) { Node = nodeDeserialized });
                                break;
                        }

                    }
                }

                objects.Items.Add(staticObjects);
                objects.Items.Add(characterObjects);
                objects.Items.Add(creatureObjects);
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
            }

            //    scenes.Items.Add(scene);
            this.Items.Add(objects);

            RaceCategoryModel races = new RaceCategoryModel(this, UnityContainer) { Name = "Races" };

            try
            {
                IEnumerable<Race> result = m_DBI.selectAllRace();
                if (result != null)
                {
                    //select all Races
                    foreach (var race in result)
                    {
                        RaceModel newRace = new RaceModel() { DBData = race };

                        races.Items.Add(newRace);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            this.Items.Add(races);
           
            return mOpened = true;

        }
        public Boolean Save() { return true; }
        public Boolean Delete() { return true; }

        public String Location { get; set; }

        public GameDBFileModel()
        {
            m_DBI = new IDAL();

        }

        [Browsable(false)]
        [XmlIgnore]
        public IUnityContainer UnityContainer { get; private set; }

        public GameDBFileModel(IItem parent, IUnityContainer unityContainer)
        {
            Name = "SQLiteDB";
            UnityContainer = unityContainer;
            Parent = parent;
            Items = new CollectionOfIItem();
            MenuOptions = new List<MenuItem>();

            MenuItem mib1a = new MenuItem();
            mib1a.Header = "Text.xaml";
            MenuOptions.Add(mib1a);

            m_DBI = new IDAL();






        }
    }
}