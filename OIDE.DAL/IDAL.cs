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
using System.Windows;
using Microsoft.Practices.Unity;
using Wide.Interfaces.Services;
using Wide.Interfaces;
using OIDE.IDAL.MDB;

namespace OIDE.IDAL.MDB
{
    public partial class dbDataEntities
    {
        private IUnityContainer m_Container;

        public UInt16 CTXID { get; set; }

        public dbDataEntities(IUnityContainer container, String xconnection)
            : base(xconnection)
        {
            m_Container = container;
        }
    }

    public class IDAL_DCTX
    {
        public object Context { get; set; }
        public IUnityContainer UnityContainer { get; set; }
    }

    public class IDAL: IDB
    {
     //   dbDataEntities mCtx;
        IUnityContainer m_Container;
        ILoggerService m_loggerService;
        public DBOptions DBOptions { get; set; }
        public Guid Guid { get; set; }


        public IDAL(IUnityContainer container)
        {
        //    mCtx = new dbDataEntities();
            m_Container = container;
            m_loggerService = container.Resolve<ILoggerService>();
        //    m(ctx.Context as dbDataEntities).Database.Connection.StateChange += new StateChangeEventHandler(StateChange);
        }

        public async void ShowLoginDialog(IUnityContainer container)
        {
        //    var workspace = container.Resolve<AbstractWorkspace>();
        //    var managerDB = container.Resolve<IDatabaseService>();
        //    var logger = container.Resolve<ILoggerService>();

        //    bool loginOK = false;
        //    String message = "zum einloggen Benutzername und Passwort angeben";

        //    //solange abfragen bis korrekter Login
        //    while (!loginOK)
        //    {
        //        //Login
        //        //MahApps.Metro.Controls.Dialogs.LoginDialogData result = await workspace.LoginDialog("Login", message);
        //        //if (result == null)
        //        //{
        //        //    //user canceled
        //        //    //##    Application.Current.Shutdown();
        //        //}
        //        //else
        //        //{
        //        //    var databaseService = container.Resolve<IDatabaseService>();
        //        //    User user = new User();

        //        //    loginOK = ((IDAL)databaseService.CurrentDB).Login(result.Username, result.Password, ref user);
        //        //    if (loginOK)
        //        //    {
        //        //        workspace.NotificationRequest.Raise(
        //        //                 new Notification { Content = String.Format("eingeloggt als: {0}", result.Username), Title = "Login erfolgreich" });

        //        //        databaseService.LoggedUser.UserID = user.UserID;
        //        //        databaseService.LoggedUser.UserGruppe = user.UserGruppe;
        //        //        databaseService.LoggedUser.LoggedIn = true;
        //        //        databaseService.LoggedUser.Data = user.Data;
        //        //        databaseService.LoggedUser.Status = LogStatus.LoggedIn;
        //        //    }
        //        //    else
        //        //    {
        //        //        message = "Logindaten ungültig!";
        //        //    }
        //        //    //else
        //        //    //{
        //        //    //    workspace.NotificationRequest.Raise(new Notification { Content = "Logindaten falsch", Title = "Login fehlgeschlagen" });
        //        //    //}
        //        //}
        //    }
        }

        public object GetDataContext()
        {
            return GetDataContextOpt();
        }

        public object GetDataContextOpt(Boolean checkUser = true)
        {
            dbDataEntities tmp = null;
            
            try
            {
                 var _databaseService = m_Container.Resolve<IDatabaseService>();

                ////if (m_DBFound)
                ////{

                ////}
                ////else
                ////    ShowDBSetting(m_Container);

                //   PS_COMID_DataContext crmDataContext = new PS_COMID_DataContext();

                DBOptions options = _databaseService.CurrentDB.DBOptions;
                if (options.DBType == DBType.MSSQL)
                {
                    //  

                    //string connectionString = "metadata=res://*/MDB.MDB_PNDS.csdl|res://*/MDB.MDB_PNDS.ssdl|res://*/MDB.MDB_PNDS.msl;Server = " + options.Host + "; Database = PNDS; User Id = " + options.User + ";Password = " + options.Password;
                    // string connectionString = "metadata=res://*/MDB.MDB_PNDS.csdl|res://*/MDB.MDB_PNDS.ssdl|res://*/MDB.MDB_PNDS.msl;provider=System.Data.SqlClient;provider connection string=\"data source=AP-HAAG\\SQLEX2008R203;initial catalog=PNDS;user id=PecuSoft;password=sesam1997oeffne;MultipleActiveResultSets=True;App=EntityFramework\"";
                    string connectionString = "metadata=res://*/MDB.MDB_PNDS.csdl|res://*/MDB.MDB_PNDS.ssdl|res://*/MDB.MDB_PNDS.msl;provider=System.Data.SqlClient;provider connection string=\"data source=" + options.Host + ";initial catalog=PNDS;user id=" + options.User + ";password=" + options.Password + ";MultipleActiveResultSets=True;App=EntityFramework\"";

                    //string connectionString = "User Id=" + options.User + ";Password=" + options.Password + ";Server=" + options.Host + ";Direct=True;Sid=" + options.ServiceName + ";Persist Security Info=True";
                    //   string providerPrefix = "";
                    //if (!ConnectionDialog.Show(out connectionString, out providerPrefix))
                    //{
                    //    stripLabel.Text = "Incorrect connection string";
                    //    return;
                    //}

                    //Frame activating
                    //  Stream contextStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(String.Format("ComID.IDAL.MDB.CrmDataMapping{0}.lqml", providerPrefix));
                    //   Devart.Data.Linq.Mapping.MappingSource mappingSource = Devart.Data.Linq.Mapping.XmlMappingSource.FromStream(contextStream);

                    //PNDSEntities crmDataContext = new PNDSEntities(m_Container, connectionString);//, mappingSource);

                    //crmDataContext.CTXID = _databaseService.GetNextCtxID();

                    ////    crmDataContext.Connection.StateChange += new System.Data.StateChangeEventHandler(Connection_StateChange);

                    //crmDataContext.Database.Connection.Open();
                    ////    foreach (ListViewItem lvItem in lvFrames.Items)
                    ////        ((BaseControl)lvItem.Tag).OpenClick();

                    //if (checkUser && _databaseService.LoggedUser.UserID < 1)
                    //{
                    //    ShowLoginDialog(m_Container);
                    //    return null;
                    //}

                    //return crmDataContext;
                }
                else if(options.DBType == DBType.SQLite)
                {
         
         //           string connectionString = "metadata=res://*/MDB.EDM_DBData.csdl|res://*/MDB.EDM_DBData.ssdl|res://*/MDB.EDM_DBData.msl;provider=System.Data.SQLite.EF6;provider connection string=\"data source=E:\\Projekte\\coop\\XEngine\\data\\Test\\dbData.s3db\"";

                    string connectionString = "metadata=res://*/MDB.EDM_DBData.csdl|res://*/MDB.EDM_DBData.ssdl|res://*/MDB.EDM_DBData.msl;provider=System.Data.SQLite.EF6;provider connection string=\"data source=" + options.Host + "\""; //D:\\Projekte\\coop\\OIDE\\Test\\dbData.s3db\"";

                  //  string connectionString = "metadata=res://*/MDB.MDB_PNDS.csdl|res://*/MDB.MDB_PNDS.ssdl|res://*/MDB.MDB_PNDS.msl;provider=System.Data.SqlClient;provider connection string=\"data source=" + options.Host + ";initial catalog=PNDS;user id=" + options.User + ";password=" + options.Password + ";MultipleActiveResultSets=True;App=EntityFramework\"";
                    dbDataEntities crmDataContext = new dbDataEntities(m_Container, connectionString);//, mappingSource);

                    crmDataContext.CTXID = _databaseService.GetNextCtxID();

                    //    crmDataContext.Connection.StateChange += new System.Data.StateChangeEventHandler(Connection_StateChange);

                    crmDataContext.Database.Connection.Open();
                    //    foreach (ListViewItem lvItem in lvFrames.Items)
                    //        ((BaseControl)lvItem.Tag).OpenClick();

                    if (checkUser && _databaseService.LoggedUser.UserID < 1)
                    {
                        ShowLoginDialog(m_Container);
                        return null;
                    }
                    tmp = crmDataContext;

                    return tmp;
                }
                else if (options.DBType == DBType.Oracle)
                {
                    //string connectionString = "User Id=" + options.User + ";Password=" + options.Password + ";Server=" + options.Host + ";Direct=True;Sid=" + options.ServiceName + ";Persist Security Info=True";
                    ////   string providerPrefix = "";
                    ////if (!ConnectionDialog.Show(out connectionString, out providerPrefix))
                    ////{
                    ////    stripLabel.Text = "Incorrect connection string";
                    ////    return;
                    ////}

                    ////Frame activating
                    ////  Stream contextStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(String.Format("ComID.IDAL.MDB.CrmDataMapping{0}.lqml", providerPrefix));
                    ////   Devart.Data.Linq.Mapping.MappingSource mappingSource = Devart.Data.Linq.Mapping.XmlMappingSource.FromStream(contextStream);

                    //PNDSEntities crmDataContext = new PNDSEntities(m_Container, connectionString);//, mappingSource);

                    //crmDataContext.CTXID = _databaseService.GetNextCtxID();

                    ////    crmDataContext.Connection.StateChange += new System.Data.StateChangeEventHandler(Connection_StateChange);

                    //crmDataContext.Database.Connection.Open();
                    ////    foreach (ListViewItem lvItem in lvFrames.Items)
                    ////        ((BaseControl)lvItem.Tag).OpenClick();

                    //if (checkUser && _databaseService.LoggedUser.UserID < 1)
                    //{
                    //    ShowLoginDialog(m_Container);
                    //    return null;
                    //}

                    //return crmDataContext;
                }
            }
            catch (Exception ex)
            {
                var workspace = m_Container.Resolve<AbstractWorkspace>();
                var loggerService = m_Container.Resolve<ILoggerService>();
                loggerService.Log("Datenbankverbindung fehlgeschlagen." + (ex.InnerException != null ? ex.InnerException.Message : ex.Message), LogCategory.Exception, LogPriority.High);
                //       
              //  workspace.NotificationRequest.Raise(new Notification { Content = "Datenbankverbindung fehlgeschlagen." + (ex.InnerException != null ? ex.InnerException.Message : ex.Message), Title = "Fehler" });

                //  stripLabel.Text = ex.Message;

            }
            return tmp;
        }

        //public void Dispose()
        //{
        //    //if (mCtx != null)
        //    //{
        //    //    //  Transaction.Dispose();
        //    //    // Transaction = null;
        //    //    m(ctx.Context as dbDataEntities).Dispose();
        //    //    mCtx = null;
        //    //}
        //}

        void StateChange(object sender, StateChangeEventArgs e)
        {
           //if(m(ctx.Context as dbDataEntities).Database.Connection.State == ConnectionState.Broken
           //    || m(ctx.Context as dbDataEntities).Database.Connection.State == ConnectionState.Closed)
           //{
           //    //reconnect
           //}
        }

        //public IEnumerable<PNDS_AUT_PARA> GetAll_PNDS_AUT_PARA(PNDSEntities dc)
        //{
        //    List<String> geraete = new List<String>()
        //                        { "K", //AKI
        //                            "T" //PATX
        //                        };

        //    var found = from d in dc.PNDS_AUT_PARA
        //                where geraete.Contains(d.Geraeteart)
        //                orderby d.Automatennr ascending
        //                select d;
        //    if (found.Any())
        //    {
        //        return found;
        //    }

        //    return null;
        //}

        #region EntityChar


        #endregion

        #region Physics

        public static bool insertEntity(IDAL_DCTX ctx, Entity po)
        {
            try
            {

                var result = (ctx.Context as dbDataEntities).Entity.Where(x => x.EntID == po.EntID);
                if (result.Any())
                {
                    result.First().Data = po.Data;
                }
                else
                {
                    (ctx.Context as dbDataEntities).Entity.Add(po);
                }

                (ctx.Context as dbDataEntities).SaveChanges();
                return true;

            }catch(Exception ex)
            {

            }

            return false;
        }

        public static bool insertEntityData(IDAL_DCTX ctx, EntityData entityData)
        {
            try
            {
                var result = (ctx.Context as dbDataEntities).EntityData.Where(x => x.EntDID == entityData.EntDID);
                if (result.Any())
                {
                    result.First().Data = entityData.Data;
                }
                else
                {
                    (ctx.Context as dbDataEntities).EntityData.Add(entityData);
                }

                (ctx.Context as dbDataEntities).SaveChanges();
                return true;

            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public static bool deleteEntity(IDAL_DCTX ctx, Entity po)
        {
            try
            {
                var result = (ctx.Context as dbDataEntities).Entity.Where(x => x.EntID == po.EntID);
                if (result.Any())
                {
                    (ctx.Context as dbDataEntities).Entity.Remove(result.First());
                    (ctx.Context as dbDataEntities).SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public static IEnumerable<EntityData> selectAllEntityData(IDAL_DCTX ctx)
        {
            try
            {
                var result = (ctx.Context as dbDataEntities).EntityData;
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

        public static IEnumerable<EntityContainer> selectAllEntities(IDAL_DCTX ctx)
        {
            try
            {

                var result = from n in (ctx.Context as dbDataEntities).Entity

                             join oj in (ctx.Context as dbDataEntities).EntityData on n.EntDID equals oj.EntDID into gjo
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
                MessageBox.Show("selectAllEntities. " + ex.Message);
            }

            return null;
        }

        public static Entity selectEntity(IDAL_DCTX ctx, int id)
        {
            try
            {
                var result = (ctx.Context as dbDataEntities).Entity.Where(x => x.EntID == id);
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

        public static EntityData selectEntityData(IDAL_DCTX ctx, int id)
        {
            try
            {
                var result = (ctx.Context as dbDataEntities).EntityData.Where(x => x.EntDID == id);
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

        public static bool insertScene(IDAL_DCTX ctx, OIDE.IDAL.MDB.Scene scene)
        {
            try
            {
                var ctxtmp = (ctx.Context as dbDataEntities);
                var result = ctxtmp.Scene.Where(x => x.SceneID == scene.SceneID);
                if (result.Any())
                {
                    var dbScene = result.First();
                    dbScene.Name = scene.Name;
                    dbScene.Data = scene.Data;
                }
                else
                {
                    (ctx.Context as dbDataEntities).Scene.Add(scene);
                }

                (ctx.Context as dbDataEntities).SaveChanges();
                return true;

            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public static bool DeleteScene(IDAL_DCTX ctx, Int32 id)
        {
            try
            {
                // Scene tmp = new Scene() { Data = data };
                (ctx.Context as dbDataEntities).Scene.Remove((ctx.Context as dbDataEntities).Scene.Where(x => x.SceneID == id).First());
                (ctx.Context as dbDataEntities).SaveChanges();
                // id = (int)tmp.SceneID;
                return true;
            }
            catch (Exception ex)
            {
              //  m_loggerService.Log("error IDAL.DeleteScene(id=" + id + ")" + (ex.InnerException != null ? ex.InnerException.Message : ex.Message), LogCategory.Exception, LogPriority.High);
                //     MessageBox.Show("dreck_" + id + "_!!!!");
            }
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

        public static IEnumerable<SceneNodeContainer> selectSceneNodes(IDAL_DCTX ctx, int sceneID)
        {
            try
            {
                var result = from n in (ctx.Context as dbDataEntities).SceneNode

                             join oj in (ctx.Context as dbDataEntities).Entity on n.EntID equals oj.EntID into gjo
                             from Ent in gjo.DefaultIfEmpty()

                             //where n.SceneID == sceneID
                             //select

                             where n.SceneID == sceneID
                             select new SceneNodeContainer { Node = n, Entity = Ent };

                //  var result = m(ctx.Context as dbDataEntities).Scene.Where(x => x.SceneID == id);
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
        //    m(ctx.Context as dbDataEntities).SceneNode.Add(sceneNode);
        //    m(ctx.Context as dbDataEntities).SaveChanges();
        //    return true;
        //}

        public static bool DeleteSceneNode(IDAL_DCTX ctx, Int32 id)
        {
            try
            {
                // Scene tmp = new Scene() { Data = data };
                (ctx.Context as dbDataEntities).SceneNode.Remove((ctx.Context as dbDataEntities).SceneNode.Where(x => x.NodeID == id).First());
                (ctx.Context as dbDataEntities).SaveChanges();
                // id = (int)tmp.SceneID;
                return true;
            }catch(Exception ex)
            {
                MessageBox.Show("error in DeleteSceneNode '" + id + "' " + ex.Message);
            }
            return false;
        }

        public static bool updateSceneNode(IDAL_DCTX ctx, SceneNode sceneNode)
        {
            var result = (ctx.Context as dbDataEntities).SceneNode.Where(x => x.NodeID == sceneNode.NodeID);
            if (result.Any())
            {
                var scenNode = result.First();
                scenNode.Data = sceneNode.Data;
                scenNode.SceneID = sceneNode.SceneID;
                scenNode.EntID = sceneNode.EntID;
                scenNode.Name = sceneNode.Name;
                (ctx.Context as dbDataEntities).SaveChanges();
                return true;
            }
            else
            {
                (ctx.Context as dbDataEntities).SceneNode.Add(sceneNode);
                (ctx.Context as dbDataEntities).SaveChanges();
                return true;
            }
                
        }

        //public IEnumerable<SceneContainer> selectCompleteScene(int id)
        //{
        //    try
        //    {
        //        var result = from n in m(ctx.Context as dbDataEntities).Scene

        //                     join nj in m(ctx.Context as dbDataEntities).SceneNodes on n.SceneID equals nj.SceneID into gj
        //                     from node in gj.DefaultIfEmpty()

        //                     join oj in m(ctx.Context as dbDataEntities).Entity on node.EntID equals oj.EntID into gjo
        //                     from Ent in gjo.DefaultIfEmpty()

        //                     where n.SceneID == id
        //                     select new SceneContainer { Scene = n, Nodes = node, Entity = Ent };

        //        //  var result = m(ctx.Context as dbDataEntities).Scene.Where(x => x.SceneID == id);
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

        public static IEnumerable<OIDE.IDAL.MDB.Scene> selectAllScenesDataOnly(IDAL_DCTX ctx)
        {
            try
            {
                var result = from n in (ctx.Context as dbDataEntities).Scene
                             select n;

                //  var result = m(ctx.Context as dbDataEntities).Scene.Where(x => x.SceneID == id);
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

        public static OIDE.IDAL.MDB.Scene selectSceneDataOnly(IDAL_DCTX ctx, int id)
        {
            try
            {
                var result = from n in (ctx.Context as dbDataEntities).Scene
                             where n.SceneID == id
                             select n;

                //  var result = m(ctx.Context as dbDataEntities).Scene.Where(x => x.SceneID == id);
                if (result.Any())
                    return result.First();//.Data;
                else
                    return new OIDE.IDAL.MDB.Scene();
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

}
