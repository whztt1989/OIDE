﻿using System;
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
        [NewItemTypes(new Type[] { typeof(MeshModel), typeof(PlaneModel), typeof(CubeModel) })]
        public List<MeshModel> Meshes { get { return m_BaseObj_FBData.Meshes; } set { m_BaseObj_FBData.Meshes = value; } }

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


        [Category("Entity basic")]
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

        [Category("Entity basic")]
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

        [Category("Entity basic")]
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

        [Category("Entity basic")]
        public EntityTypes EntType
        {
            get { return m_BaseObj_FBData.EntType; }
            set
            {
                int res = m_BaseObj_FBData.SetEntType(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer EntityBaseModel.EntType.SetEntType invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                }

                RaisePropertyChanged("EntType");
            }
        }

        [Category("Entity basic")]
        public uint Mode
        {
            get { return m_BaseObj_FBData.Mode; }
            set
            {
                int res = m_BaseObj_FBData.SetMode(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer EntityBaseModel.Mode.SetMode invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                }

                RaisePropertyChanged("Mode");
            }
        }


        [Category("Entity basic")]
        public Boolean ShowDebug
        {
            get { return m_BaseObj_FBData.ShowDebug; }
            set
            {
                int res = m_BaseObj_FBData.SetShowDebug(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer EntityBaseModel.ShowDebug.SetShowDebug invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                }

                RaisePropertyChanged("ShowDebug");
            }
        }

        [Category("Entity basic")]
        public Boolean ShowAABB
        {
            get { return m_BaseObj_FBData.ShowAABB; }
            set
            {
                int res = m_BaseObj_FBData.SetShowAABB(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer EntityBaseModel.ShowDebug.SetShowAABB invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                }

                RaisePropertyChanged("ShowAABB");
            }
        }


        [Category("Entity basic")]
        public uint Renderqueue
        {
            get { return m_BaseObj_FBData.Renderqueue; }
            set
            {
                int res = m_BaseObj_FBData.SetRenderqueue(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer EntityBaseModel.Renderqueue.SetRenderqueue invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                }

                RaisePropertyChanged("Renderqueue");
            }
        }


        [Category("Entity basic")]
        public uint Group
        {
            get { return m_BaseObj_FBData.Group; }
            set
            {
                int res = m_BaseObj_FBData.SetGroup(value);
                if (res != 0) //fehler beim senden
                {
                    var logger = UnityContainer.Resolve<ILoggerService>();
                    logger.Log("Flatbuffer EntityBaseModel.Group.SetGroup invalid (" + value.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
                }

                RaisePropertyChanged("Group");
            }
        }

        #endregion
    }
}
