using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using DAL;
using Microsoft.Practices.Unity;
using Module.Properties.Interface;

namespace OIDE.Core.ProjectTypes.Model
{
    public class GameStateModel : IItem
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

                        //switch ((EntityTypes)gameEntity.Entity.EntType)
                        //{
                        //    case EntityTypes.NT_SpawnPoint:

                        //        SpawnPointModel tmpSpawnPoint = new SpawnPointModel(allSpawns, UnityContainer, m_DBI)
                        //        {
                        //            ContentID = "SpawnPointID:##:" + gameEntity.Entity.EntID,
                        //            Name = gameEntity.Entity.Name ?? ("Noname SpawnPoint " + (int)gameEntity.Entity.EntID),
                        //            DBData = gameEntity
                        //        };// Data = gameEntityDataDeserialized });

                        //        allSpawns.Items.Add(tmpSpawnPoint);
                        //        break;

                        //    case EntityTypes.NT_Static:

                        //        StaticObjectModel tmp = new StaticObjectModel(staticObjects, UnityContainer, m_DBI)
                        //        {
                        //            ContentID = "StaticID:##:" + gameEntity.Entity.EntID,
                        //            Name = gameEntity.Entity.Name ?? ("Noname Static " + (int)gameEntity.Entity.EntID),
                        //            DB_Entity = gameEntity
                        //        };// Data = gameEntityDataDeserialized });

                        //        staticObjects.Items.Add(tmp);
                        //        break;
                        //    case EntityTypes.NT_Character:

                        //        CharacterObjModel tmpChar = new CharacterObjModel(characterObjects, UnityContainer, m_DBI)
                        //        {
                        //            ContentID = "CharacterObjID:##:" + gameEntity.Entity.EntID,
                        //            Name = gameEntity.Entity.Name ?? ("Noname CharObj " + (int)gameEntity.Entity.EntID),
                        //            DBData = gameEntity
                        //        };// Data = gameEntityDataDeserialized });

                        //        characterObjects.Items.Add(tmpChar);
                        //        break;
                        //    //case NodeTypes.Physic:

                        //    //    ProtoType.PhysicsObject dataPhysObj = new ProtoType.PhysicsObject();
                        //    //    if (gameEntity.Data != null)
                        //    //        dataPhysObj = ProtoSerialize.Deserialize<ProtoType.PhysicsObject>(gameEntity.Data);

                        //    //    allPhysics.Items.Add(new PhysicsObjectModel(allPhysics, UnityContainer, dataPhysObj, m_DBI) { ContentID = "PhysicID:##:" + gameEntity.EntID, Name = gameEntity.Name ?? ("Noname" + (int)gameEntity.EntID) });// Data = gameEntityDataDeserialized });

                        //    //    break;
                        //    case EntityTypes.NT_Camera:
                        //        //todo contentid for camera

                        //        //   SceneNodes = new SceneNodes() { NodeID = sNode.NodeID, EntID = sNode.Node.EntityID, SceneID = ID, Data = ProtoSerialize.Serialize(sNode.Node) };


                        //        //var itemCam = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == ""); // Search for Camera category
                        //        //if (itemCam.Any())
                        //        //    itemCam.First().SceneItems.Add(new CameraModel(itemCam.First(), UnityContainer) { Node = nodeDeserialized });

                        //        break;
                        //    case EntityTypes.NT_Light:

                        //        //var itemLight = m_SceneService.SelectedScene.SceneItems.Where(x => x.ContentID == "");
                        //        //if (itemLight.Any())
                        //        //    itemLight.First().SceneItems.Add(new LightModel(itemLight.First(), UnityContainer) { Node = nodeDeserialized });
                        //        break;
                        //}

                    }
                }

            //    objects.Items.Add(staticObjects);
            //    objects.Items.Add(characterObjects);
            ////    objects.Items.Add(creatureObjects);
            //   // objects.Items.Add(allPhysics);

            //    objects.Items.Add(allTrigger);
            //    objects.Items.Add(allSpawns);
            //    objects.Items.Add(allLights);
            //    objects.Items.Add(allSkies);
            //    objects.Items.Add(allTerrains);
            //    objects.Items.Add(allSounds);
            //    objects.Items.Add(DynamicObjects);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }

            //    scenes.Items.Add(scene);
            this.Items.Add(objects);
           
            return mOpened = true;

        }
        public Boolean Save() { return true; }
        public Boolean Delete() { return true; }

        public String Location { get; set; }

        public GameStateModel()
        {
            m_DBI = new IDAL();

        }

        [Browsable(false)]
        [XmlIgnore]
        public IUnityContainer UnityContainer { get; private set; }

        public GameStateModel(IItem parent, IUnityContainer unityContainer)
        {
            Name = "GameStates";
            UnityContainer = unityContainer;
            Parent = parent;
            Items = new CollectionOfIItem();
            MenuOptions = new List<MenuItem>();

            MenuItem mib1a = new MenuItem();
            mib1a.Header = "Text.xaml";
            MenuOptions.Add(mib1a);

            m_DBI = new IDAL();






        }


        public Boolean Create() { return true; }
        public Boolean Open(object id) { return true; }
        public Boolean Save(object param) { return true; }
        public Boolean Closing() { return true; }
        public void Refresh() { }
        public void Finish() { }
        public void Drop(IItem item) { }
    }
}
