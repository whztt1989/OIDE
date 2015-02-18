using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces.Services;

using Microsoft.Practices.Unity;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using OIDE.Scene.Model.Objects.ChildObject;
using Wide.Interfaces;


namespace OIDE.Scene.Model.Objects.ObjectData
{
    public class EntityBaseModel : ViewModelBase
    {
        public EntityBaseModel(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
            m_BaseObj_FBData = new FB_EntityBaseModel();
        }

        protected FB_EntityBaseModel m_BaseObj_FBData;

        public void SetFBData(FB_EntityBaseModel FBBaseModel)
        {
            if (FBBaseModel != null)
                m_BaseObj_FBData = FBBaseModel;
        }

        #region Properties

        [Browsable(false)]
        private IUnityContainer UnityContainer { get; set; }


        [Category("Entity basic")]
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        [NewItemTypes(new Type[] { typeof(MeshObject), typeof(PlaneObject), typeof(CubeObject) })]
        public List<MeshObject> Meshes { get { return m_BaseObj_FBData.Meshes; } set { m_BaseObj_FBData.Meshes = value; } }

        [Category("Entity basic")]
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        public List<MaterialObject> Materials { get { return m_BaseObj_FBData.Materials; } set { m_BaseObj_FBData.Materials = value; } }

        [Category("Entity basic")]
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        public List<PhysicObject> Physics { get { return m_BaseObj_FBData.Physics; } set { m_BaseObj_FBData.Physics = value; } }

        [Category("Entity basic")]
        [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.CollectionEditor))]
        public List<SoundObject> Sounds { get { return m_BaseObj_FBData.Sounds; } set { m_BaseObj_FBData.Sounds = value; } }


        [Category("Entity basic")]
        [Description("animationinfo - triggers")]
        public String AnimationInfo
        {
            get { return m_BaseObj_FBData.AnimationInfo; }
            set { m_BaseObj_FBData.AnimationInfo = value; RaisePropertyChanged("AnimationInfo"); }
        }

        [Category("Entity basic")]
        public String AnimationTree
        {
            get { return m_BaseObj_FBData.AnimationTree; }
            set { m_BaseObj_FBData.AnimationTree = value; RaisePropertyChanged("AnimationTree"); }
        }

        [Category("Entity basic")]
        public String Boneparent
        {
            get { return m_BaseObj_FBData.Boneparent; }
            set { m_BaseObj_FBData.Boneparent = value; RaisePropertyChanged("Boneparent"); }
        }

        [Category("Entity basic")]
        public Boolean CastShadows
        {
            get { return m_BaseObj_FBData.CastShadows; }
            set { m_BaseObj_FBData.CastShadows = value; RaisePropertyChanged("CastShadows"); }
        }

        [Category("Entity basic")]
        public EntityTypes EntType
        {
            get { return m_BaseObj_FBData.EntType; }
            set { m_BaseObj_FBData.EntType = value; RaisePropertyChanged("EntType"); }
        }

        [Category("Entity basic")]
        public uint Mode
        {
            get { return m_BaseObj_FBData.Mode; }
            set { m_BaseObj_FBData.Mode = value; RaisePropertyChanged("Mode"); }
        }


        [Category("Entity basic")]
        public Boolean ShowDebug
        {
            get { return m_BaseObj_FBData.ShowDebug; }
            set
            {
                m_BaseObj_FBData.ShowDebug = value;
                RaisePropertyChanged("ShowDebug");
            }
        }

        [Category("Entity basic")]
        public Boolean ShowAABB
        {
            get { return m_BaseObj_FBData.ShowAABB; }
            set { m_BaseObj_FBData.ShowAABB = value; RaisePropertyChanged("ShowAABB"); }
        }


        [Category("Entity basic")]
        public uint Renderqueue
        {
            get { return m_BaseObj_FBData.Renderqueue; }
            set { m_BaseObj_FBData.Renderqueue = value; RaisePropertyChanged("Renderqueue"); }
        }


        [Category("Entity basic")]
        public uint Group
        {
            get { return m_BaseObj_FBData.Group; }
            set { m_BaseObj_FBData.Group = value; RaisePropertyChanged("Group"); }
        }

        #endregion
    }
}
