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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Module.DB.Interface;
using Module.DB.Interface.Services;
using Module.DB.Settings;
using System.Data;
using DAL.MDB;
using System.Windows;


namespace DAL
{
    public class IDAL: IDisposable
    {
        dbDataEntities mCtx;

        public IDAL()
        {
            mCtx = new dbDataEntities();
        //    mCtx.Database.Connection.StateChange += new StateChangeEventHandler(StateChange);
        }

        public void Dispose()
        {
            if (mCtx != null)
            {
                //  Transaction.Dispose();
                // Transaction = null;
                mCtx.Dispose();
                mCtx = null;
            }
        }

        void StateChange(object sender, StateChangeEventArgs e)
        {
           if(mCtx.Database.Connection.State == ConnectionState.Broken
               || mCtx.Database.Connection.State == ConnectionState.Closed)
           {
               //reconnect
           }
        }

        #region EntityChar

        //public bool insertEntityChar(uint id, byte[] data)
        //{
        //    mCtx.EntityChar.Add(new EntityChar() { Data = data });
        //    mCtx.SaveChanges();
        //    return true;
        //}

        //public bool updateEntityChar(uint id, byte[] data)
        //{
        //    var result = mCtx.EntityChar.Where(x => x.EC_ID == id);
        //    if (result.Any())
        //    {
        //        result.First().Data = data;
        //        mCtx.SaveChanges();
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        //public byte[] selectEntityChar(uint id)
        //{
        //    var result = mCtx.EntityChar.Where(x => x.EC_ID == id);
        //    if (result.Any())
        //    {
        //        return result.First().Data;
        //    }
        //    else 
        //        return new byte[0];
        //}

        #endregion

        #region Physics

        public bool insertEntity(Entity po)
        {
            mCtx.Entity.Add(po);
            mCtx.SaveChanges();
            return true;
        }

        public bool insertEntityData(EntityData entityData)
        {
            mCtx.EntityData.Add(entityData);
            mCtx.SaveChanges();
            return true;
        }
      
        public bool deleteEntity(Entity po)
        {
            var result = mCtx.Entity.Where(x => x.EntID == po.EntID);
            if (result.Any())
            {
                mCtx.Entity.Remove(result.First());
                mCtx.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool updateEntity(Entity po)
        {
            var result = mCtx.Entity.Where(x => x.EntID == po.EntID);
            if (result.Any())
            {
                result.First().Data = po.Data;
                mCtx.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool updateEntityData(EntityData entityData)
        {
            var result = mCtx.EntityData.Where(x => x.EntDID == entityData.EntDID);
            if (result.Any())
            {
                result.First().Data = entityData.Data;
                mCtx.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public IEnumerable<EntityData> selectAllEntityData()
        {
            try
            {
                var result = mCtx.EntityData;
                if (result.Any())
                    return result;
                else
                    return null;
            }
             catch(Exception ex)
            {
           //     MessageBox.Show("dreck_" + id + "_!!!!");
            }

            return null;
        }

        public class EntityContainer
        {
            public EntityData EntityData { get; set; }
            public Entity Entity { get; set; }
        }

        public IEnumerable<EntityContainer> selectAllEntities()
        {
            try
            {

                var result = from n in mCtx.Entity

                             join oj in mCtx.EntityData on n.EntDID equals oj.EntDID into gjo
                             from Ent in gjo.DefaultIfEmpty()

                             //where n.SceneID == sceneID
                             //select

                          //   where n.SceneID == sceneID
                             select new EntityContainer { Entity = n, EntityData = Ent };

                if (result.Any())
                    return result;
                else
                    return null;
            }
             catch(Exception ex)
            {
           //     MessageBox.Show("dreck_" + id + "_!!!!");
            }

            return null;
        }

        public Entity selectEntity(int id)
        {
            try
            {
                var result = mCtx.Entity.Where(x => x.EntID == id);
                if (result.Any())
                    return result.First();
                else
                    return new Entity();
            }
             catch(Exception ex)
            {
           //     MessageBox.Show("dreck_" + id + "_!!!!");
            }

            return new Entity();
        }

        public EntityData selectEntityData(int id)
        {
            try
            {
                var result = mCtx.EntityData.Where(x => x.EntDID == id);
                if (result.Any())
                    return result.First();
                else
                    return new EntityData();
            }
            catch (Exception ex)
            {
                //     MessageBox.Show("dreck_" + id + "_!!!!");
            }

            return new EntityData();
        }

        #endregion


        #region Scene

        public bool insertScene(Scene scene)
        {
           // Scene tmp = new Scene() { Data = data };
            mCtx.Scene.Add(scene);
            mCtx.SaveChanges();
           // id = (int)tmp.SceneID;
            return true;
        }

        public bool DeleteScene(Int32 id)
        {
           // Scene tmp = new Scene() { Data = data };
            mCtx.Scene.Remove(mCtx.Scene.Where(x => x.SceneID == id).First());
            mCtx.SaveChanges();
           // id = (int)tmp.SceneID;
            return true;
        }

        public bool updateScene(Scene scene)
        {
            var result = mCtx.Scene.Where(x => x.SceneID == scene.SceneID);
            if (result.Any())
            {
                Scene sceneTmp = result.First();
                sceneTmp.Data = scene.Data;
                sceneTmp.FogID = scene.FogID;
                sceneTmp.SkyID = scene.SkyID;
                sceneTmp.TerrID = scene.TerrID;
                mCtx.SaveChanges();
                return true;
            }
            else
                return false;
        }


        //public class SceneContainer
        //{
        //    public Scene Scene { get; set; }
        //    public SceneNode Nodes { get; set; }
        //    public Entity Entity { get; set; }
        //}

        public class SceneNodeContainer
        {
            public SceneNode Node { get; set; }
            public Entity Entity { get; set; }
        }

        public IEnumerable<SceneNodeContainer> selectSceneNodes(int sceneID)
        {
            try
            {
                var result = from n in mCtx.SceneNode

                             join oj in mCtx.Entity on n.EntID equals oj.EntID into gjo
                             from Ent in gjo.DefaultIfEmpty()

                             //where n.SceneID == sceneID
                             //select

                             where n.SceneID == sceneID
                             select new SceneNodeContainer { Node = n, Entity = Ent };

                //  var result = mCtx.Scene.Where(x => x.SceneID == id);
                if (result.Any())
                {
                    return result;//.Data;
                }
            }
            catch (Exception ex)
            {
                //     MessageBox.Show("dreck_" + id + "_!!!!");
            }

            return null;
        }


        //public bool insertSceneNode(SceneNode sceneNode)
        //{
        //    mCtx.SceneNode.Add(sceneNode);
        //    mCtx.SaveChanges();
        //    return true;
        //}

        public bool DeleteSceneNode(Int32 id)
        {
            try
            {
                // Scene tmp = new Scene() { Data = data };
                mCtx.SceneNode.Remove(mCtx.SceneNode.Where(x => x.NodeID == id).First());
                mCtx.SaveChanges();
                // id = (int)tmp.SceneID;
                return true;
            }catch(Exception ex)
            {
                MessageBox.Show("error in DeleteSceneNode '" + id + "' " + ex.Message);
            }
            return false;
        }

        public bool updateSceneNode(SceneNode sceneNode)
        {
            var result = mCtx.SceneNode.Where(x => x.NodeID == sceneNode.NodeID);
            if (result.Any())
            {
                var scenNode = result.First();
                scenNode.Data = sceneNode.Data;
                scenNode.SceneID = sceneNode.SceneID;
                scenNode.EntID = sceneNode.EntID;
                scenNode.Name = sceneNode.Name;
                mCtx.SaveChanges();
                return true;
            }
            else
            {
                mCtx.SceneNode.Add(sceneNode);
                mCtx.SaveChanges();
                return true;
            }
                
        }

        //public IEnumerable<SceneContainer> selectCompleteScene(int id)
        //{
        //    try
        //    {
        //        var result = from n in mCtx.Scene

        //                     join nj in mCtx.SceneNodes on n.SceneID equals nj.SceneID into gj
        //                     from node in gj.DefaultIfEmpty()

        //                     join oj in mCtx.Entity on node.EntID equals oj.EntID into gjo
        //                     from Ent in gjo.DefaultIfEmpty()

        //                     where n.SceneID == id
        //                     select new SceneContainer { Scene = n, Nodes = node, Entity = Ent };

        //        //  var result = mCtx.Scene.Where(x => x.SceneID == id);
        //        if (result.Any())
        //        {
        //            return result;//.Data;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //     MessageBox.Show("dreck_" + id + "_!!!!");
        //    }

        //    return null;
        //}

        public IEnumerable<Scene> selectAllScenesDataOnly()
        {
            try
            {
                var result = from n in mCtx.Scene
                             select n;

                //  var result = mCtx.Scene.Where(x => x.SceneID == id);
                if (result.Any())
                    return result;//.Data;
                else
                    return null;
            }
            catch (Exception ex)
            {
                //     MessageBox.Show("dreck_" + id + "_!!!!");
            }

            return null;
        }

        public Scene selectSceneDataOnly(int id)
        {
            try
            {
                var result = from n in mCtx.Scene
                             where n.SceneID == id
                             select n;

                //  var result = mCtx.Scene.Where(x => x.SceneID == id);
                if (result.Any())
                    return result.First();//.Data;
                else
                    return new Scene();
            }
            catch (Exception ex)
            {
                //     MessageBox.Show("dreck_" + id + "_!!!!");
            }

            return null;
        }

        #endregion
        //public void DownloadBlob(SQLiteConnection sqConnection)
        //{
        //    SQLiteCommand sqCommand = new SQLiteCommand("SELECT * FROM Pictures", sqConnection);
        //    sqConnection.Open();
        //    SQLiteDataReader myReader = sqCommand.ExecuteReader(System.Data.CommandBehavior.Default);
        //    try
        //    {
        //        while (myReader.Read())
        //        {
        //            SQLiteBlob myBlob = myReader.GetSQLiteBlob(myReader.GetOrdinal("Picture"));
        //            if (!myBlob.IsNull)
        //            {
        //                string FN = myReader.GetString(myReader.GetOrdinal("PicName"));
        //                FileStream fs = new FileStream("D:\\Tmp\\" + FN + ".bmp", FileMode.Create);
        //                BinaryWriter w = new BinaryWriter(fs);
        //                w.Write(myBlob.Value);
        //                w.Close();
        //                fs.Close();
        //                Console.WriteLine(FN + " downloaded.");
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        myReader.Close();
        //        sqConnection.Close();
        //    }
        //}

        //public void UploadBlob(SQLiteConnection sqConnection)
        //{
        //    FileStream fs = new FileStream("D:\\Tmp\\_Water.bmp", FileMode.Open, FileAccess.Read);
        //    BinaryReader r = new BinaryReader(fs);
        //    SQLiteBlob myBlob = new SQLiteBlob(r.ReadBytes((int)fs.Length));
        //    SQLiteCommand sqCommand = new SQLiteCommand("INSERT INTO Pictures (ID, PicName, Picture) VALUES(1,'Water',:Pictures)", sqConnection);
        //    sqCommand.Parameters.Add("Pictures", myBlob);
        //    sqConnection.Open();
        //    try
        //    {
        //        Console.WriteLine(sqCommand.ExecuteNonQuery() + " rows affected.");
        //    }
        //    finally
        //    {
        //        sqConnection.Close();
        //        r.Close();
        //    }
        //} 
    }
    //public class CDAL : IDB
    //{
    //    private IDatabaseService _databaseService;
    //    private PSXIDEDataContext crmDataContext;

    //    public DBOptions DBOptions { get; set; }
    //    public Guid Guid { get; set; }

    //    public CDAL(IDatabaseService databaseService)
    //    {
    //        _databaseService = databaseService;
    //    }

    //    public void InsertPS_AUFTRAEGE(PS_AUFTRAEGE instance)
    //    {
    //        if (crmDataContext.Connection.State == System.Data.ConnectionState.Open)
    //        {
    //            var sessionFound = from d in crmDataContext.PS_AUFTRAEGEs
    //                               where d.AUFTRAGID == 0
    //                               //orderby d.Datum descending
    //                               select d;

    //            PS_AUFTRAEGE tmp = new PS_AUFTRAEGE();
    //            crmDataContext.PS_AUFTRAEGEs.InsertOnSubmit(tmp);
    //            // Associate the new product with the new category

    //            //## crmDataContext.products.DeleteOnSubmit(newProduct);

    //            // Send the changes to the database.
    //            // Until you do it, the changes are cached on the client side.
    //            crmDataContext.SubmitChanges();
    //        }
    //    }

    //    public void UpdatePS_AUFTRAEGE(PS_AUFTRAEGE instance)
    //    {
    //        //braucht nur submit changes!!
    //    }

    //    public void GetConnection()
    //    {
    //     //  DEM_PS.
    //    }

    //    public void Save()
    //    {
    //        try
    //        {
    //            crmDataContext.SubmitChanges();
    //            //    foreach (ListViewItem lvItem in lvFrames.Items)
    //            //        ((BaseControl)lvItem.Tag).OpenClick();
    //        }
    //        catch (Exception ex)
    //        {
    //            //  stripLabel.Text = ex.Message;
    //            return;
    //        }
           
    //    }

    //    public void OpenContext()
    //    {
    //        DBOptions options = _databaseService.CurrentDB.DBOptions;

    //        if (options.DBType == DBType.Oracle)
    //        {
    //            string connectionString = "User Id="+options.User+";Password="+options.Password+";Server="+options.Host+";Direct=True;Sid="+options.ServiceName+";Persist Security Info=True";
    //            string providerPrefix = "";
    //            //if (!ConnectionDialog.Show(out connectionString, out providerPrefix))
    //            //{
    //            //    stripLabel.Text = "Incorrect connection string";
    //            //    return;
    //            //}

    //            //Frame activating
    //          //  Stream contextStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(String.Format("DAL.MDB.CrmDataMapping{0}.lqml", providerPrefix));
    //         //   Devart.Data.Linq.Mapping.MappingSource mappingSource = Devart.Data.Linq.Mapping.XmlMappingSource.FromStream(contextStream);
    //            crmDataContext = new PSXIDEDataContext(connectionString);//, mappingSource);

    //            crmDataContext.Connection.StateChange += new System.Data.StateChangeEventHandler(Connection_StateChange);

    //            try
    //            {
    //                crmDataContext.Connection.Open();
    //                //    foreach (ListViewItem lvItem in lvFrames.Items)
    //                //        ((BaseControl)lvItem.Tag).OpenClick();
    //            }
    //            catch (Exception ex)
    //            {
    //                //  stripLabel.Text = ex.Message;
    //                return;
    //            }
    //        }

    //     //   ManageControlsIfOpen(true);
    //    }

    //    private void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
    //    {
            
    //   //     currentControl.ControlsEnabled(e.CurrentState == System.Data.ConnectionState.Open);
    //    }

    //    public void CloseContext()
    //    {

    //        if (crmDataContext != null)
    //        {
    //            crmDataContext.Connection.Close();
    //            //foreach (ListViewItem lvItem in lvFrames.Items)
    //            //    ((BaseControl)lvItem.Tag).CloseClick();
    //            crmDataContext = null;
    //        }

    //      //  ManageControlsIfOpen(false);
    //    }


    //    public Boolean GetApplications(out object apps)
    //    {
    //        apps = new object();

    //        if (crmDataContext.Connection.State == System.Data.ConnectionState.Open)
    //        {
    //            var found = from d in crmDataContext.PS_SEGMENTEs
    //                        //orderby d.Datum descending
    //                        select d;
    //            if (found.Any())
    //            {
    //                apps = found;
    //                return true;
    //            }
    //        }

    //        return false;

    //    }

    //    public Boolean GetAuftrag(UInt32 aid, out PS_AUFTRAEGE auftrag)
    //    {
    //        auftrag = new PS_AUFTRAEGE();

    //        if (crmDataContext.Connection.State == System.Data.ConnectionState.Open)
    //        {
    //            var found = from d in crmDataContext.PS_AUFTRAEGEs
    //                        where d.AUFTRAGID == aid
    //                               //orderby d.Datum descending
    //                               select d;
    //            if(found.Any())
    //            {
    //                auftrag = found.First();
    //                return true;
    //            }
    //        }

    //        return false;
      
    //    }

    //    public PS_ANLAGE GetAnlage(UInt32 aid)
    //    {
    //        PS_ANLAGE tmp = new PS_ANLAGE();
            
    //        return tmp;
    //    }

    //    public void GetObjektContainer(UInt32 ocid)
    //    {

    //    }

    //    public void GetVariante(UInt32 vid)
    //    {

    //    }

    //    public void GetMedien()
    //    {

    //    }

    //    public void UpdateAuftrag()
    //    {

    //    }

    //    public void UpdateAnlage()
    //    {

    //    }

    //    public void InsertAuftrag()
    //    {

    //    }

    //    public void InsertAnlage()
    //    {

    //    }
   // }

}
