using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Utilities.View;
using Microsoft.Practices.Unity;
using Wide.Interfaces.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace OIDE.Scene.Model.Objects
{
    public class PlaneModel : MeshModel
    {
        public PlaneModel(IUnityContainer unityContainer)
             :base(unityContainer)
        {
        }

    }

    public class CubeModel : MeshModel
    {
        public CubeModel(IUnityContainer unityContainer)
             :base(unityContainer)
        {
        }
    }

     public class MeshModel  : ViewModelBase
    {
         public MeshModel(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer; 
        }

        private FB_MeshModel m_Mesh_FBData;

        #region MeshData


        #endregion

        #region Properties

        [Browsable(false)]
        private IUnityContainer UnityContainer { get; set; }
         

        [Category("Scene Data")]
        [Description("scene ambient color")]
        public String AnimationInfo
        {
            get { return m_Mesh_FBData.FileName; }
            set
            {
                int res = 0;// = m_Mesh_FBData.FileName = value;
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer EntityBaseModel.AnimationInfo.SetAnimationInfo invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                }

                RaisePropertyChanged("AnimationInfo");
            }
        }

        #endregion
    }
}
