using DAL;
using DAL.MDB;
using Module.DB.Interface.Services;
using Module.PFExplorer.Service;
using OIDE.Scene.Interface;
using OIDE.Scene.Interface.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OIDE.Scene.Service
{
    public class SceneItem : ProjectItemModel, ISceneItem
    {
        [Browsable(false)]
        [XmlIgnore]
        public virtual CollectionOfISceneItem SceneItems { get; protected set; }

        public SceneItem()
        {
            SceneItems = new CollectionOfISceneItem();
         
        }

        [XmlIgnore]
        [Browsable(false)]
        public ISceneItem SelectedItem { get; set; }

        protected IDatabaseService m_DBService;
        private IDAL_DCTX m_DataContext;

        [Browsable(false)]
        [XmlIgnore]
        public IDAL_DCTX DataContext
        {
            get
            {
                if (m_DataContext == null)
                    m_DataContext = ((IDAL)m_DBService.CurrentDB).GetDataContextOpt(false) as IDAL_DCTX;

                return m_DataContext;
            }
            set
            {
                m_DataContext = value;
            }
        }

    }
}
