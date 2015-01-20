using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Utilities.View;
using Wide.Interfaces.Services;

using Microsoft.Practices.Unity;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;


namespace OIDE.Scene.Model.Objects.ObjectData
{
    public class EntityBaseModel : ViewModelBase
    {
        public EntityBaseModel(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer; 
        }

        protected FB_EntityBaseModel m_BaseObj_FBData;

        #region EntityBaseData

        

        //meshes:[Mesh];
        //sounds:[Sound];
        //materials:[Material];
        //physics:[PhysicsObject];
        //type:EntityTypes;
        //boneparent:string;
        //mode:uint;
        //castShadows:bool;
        //debug:Debug;	
        //uint:renderqueue;
        //group:uint; // e.g. static grouping

        #endregion

        #region Properties

        [Browsable(false)]
        private IUnityContainer UnityContainer { get; set; }

        private List<MeshModel> mMeshes;

        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        [NewItemTypes(new Type[] { typeof(MeshModel), typeof(PlaneModel), typeof(CubeModel) })]
        public List<MeshModel> Meshes { get { return mMeshes; } set { mMeshes = value; } }


        [Category("Scene Data")]
        [Description("scene ambient color")]
        public String AnimationInfo
        {
            get { return m_BaseObj_FBData.AnimationInfo; }
            set
            {
                int res = m_BaseObj_FBData.SetAnimationInfo(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer EntityBaseModel.AnimationInfo.SetAnimationInfo invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                }

                RaisePropertyChanged("AnimationInfo");
            }
        }


        public String AnimationTree
        {
            get { return m_BaseObj_FBData.AnimationTree; }
            set
            {
                int res = m_BaseObj_FBData.SetAnimationTree(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer EntityBaseModel.AnimationTree.SetAnimationTree invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                }

                RaisePropertyChanged("AnimationTree");
            }
        }
        public String Boneparent
        {
            get { return m_BaseObj_FBData.Boneparent; }
            set
            {
                int res = m_BaseObj_FBData.SetBoneparent(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer EntityBaseModel.Boneparent.SetBoneparent invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                }

                RaisePropertyChanged("Boneparent");
            }
        }
        public Boolean CastShadows
        {
            get { return m_BaseObj_FBData.CastShadows; }
            set
            {
                int res = m_BaseObj_FBData.SetCastShadows(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer EntityBaseModel.CastShadows.SetCastShadows invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                }

                RaisePropertyChanged("CastShadows");
            }
        }
    
        #endregion

    }
}
