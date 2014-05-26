using Devart.Data.Linq.Mapping;
using PSComIDContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TModul.DB.Interface;
using TModul.DB.Interface.Services;
using TModul.DB.Settings;

namespace PSComIDContext
{
    //override TEst
     public partial class PSComIDDataContext
     {
         //partial void InsertPS_USER(PS_USER instance)
         //{

         //}
     }
}

namespace ComID.DBI
{
    public class CDBI
    {

    }
    //public class CDBI : IDB
    //{
    //    private IDatabaseService _databaseService;
    //    private PSComIDDataContext crmDataContext;

    //    public DBOptions DBOptions { get; set; }
    //    public Guid Guid { get; set; }

    //    public CDBI(IDatabaseService databaseService)
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
    //          //  Stream contextStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(String.Format("ComID.DBI.MDB.CrmDataMapping{0}.lqml", providerPrefix));
    //         //   Devart.Data.Linq.Mapping.MappingSource mappingSource = Devart.Data.Linq.Mapping.XmlMappingSource.FromStream(contextStream);
    //            crmDataContext = new PSComIDDataContext(connectionString);//, mappingSource);

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
