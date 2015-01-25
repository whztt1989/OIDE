using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Utilities.View;
using Microsoft.Practices.Unity;
using OIDE.Scene.Interface.Services;
using OIDE.Scene.Model.Objects.ObjectData;
using Wide.Interfaces.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace OIDE.Scene.Model.Objects
{
    public class PlaneModel : MeshModel
    {
        private FB_PlaneModel m_PlaneModel_FBData;

        public PlaneModel(IUnityContainer unityContainer)
             :base(unityContainer)
        {
        }

        #region Properties

       public  Vector3  normal { get;set;}
       public float constant { get; set; }
       public float width { get; set; }
       public float height { get; set; }
       public uint xsegments { get; set; }
       public uint ysegments { get; set; }
       public bool normals { get; set; }
       public uint numTexCoordSets { get; set; }
       public float xTile { get; set; }
       public float yTile { get; set; }
       public Vector3 upVector { get; set; }


        #endregion

        /// <summary>
        /// needed for propertygrid collection
        /// </summary>
        public PlaneModel()
        {

        }
    }

    public class CubeModel : MeshModel
    {
        /// <summary>
        /// needed for propertygrid collection
        /// </summary>
        public CubeModel()
        {

        }
        public CubeModel(IUnityContainer unityContainer)
             :base(unityContainer)
        {
        }

        #region properties

        public float width { get;set; }
        
        #endregion
    }

     public class MeshModel  : ViewModelBase
    {
         /// <summary>
        /// needed for propertygrid collection
        /// </summary>
         public MeshModel()
        {
          //  ProtoData = new ProtoType.PhysicsObject();
        }

         public MeshModel(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer; 
        }

        private FB_MeshModel m_Mesh_FBData;

        #region MeshData


        #endregion

        #region private properties

        [Browsable(false)]
        private IUnityContainer UnityContainer { get; set; }

        #endregion

        #region Properties

         //todo
        public String RessGrp { get; set; }
        public String Name { get; set; }

        //[Category("Scene Data")]
        //[Description("scene ambient color")]
        //public String AnimationInfo
        //{
        //    get { return m_Mesh_FBData.FileName; }
        //    set
        //    {
        //        int res = 0;// = m_Mesh_FBData.FileName = value;
        //        if (res != 0) //fehler beim senden
        //        {
        //            var logger = UnityContainer.Resolve<ILoggerService>();
        //            logger.Log("Flatbuffer EntityBaseModel.AnimationInfo.SetAnimationInfo invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
        //        }

        //        RaisePropertyChanged("AnimationInfo");
        //    }
        //}

        #endregion
    }
}
