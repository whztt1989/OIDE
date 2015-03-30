using FlatBuffers;
using Module.Protob.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces;
using System.Xml.Serialization;
using System.Windows;
using OIDE.Scene.Model.Objects.FBufferObject;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using OIDE.Scene.Model.Objects.ObjectData;
using Microsoft.Practices.Unity;
using System.ComponentModel;

namespace OIDE.Scene.Model.Objects
{
    [Serializable]
    public class FB_StaticObjectModel : FB_EntityBaseModel, IFBObject
    {
        #region private members

        private XFBType.StaticEntity m_FBData = new XFBType.StaticEntity();
      //  private int m_Group;

     //   private FB_EntityBaseModel m_EntityBaseModel;

        #endregion

        #region Properties

        /// <summary>
        /// only for serialization
        /// </summary>
    //    [ExpandableObject]
    //    public FB_EntityBaseModel EntityBaseModel { get { return m_EntityBaseModel; } set { m_EntityBaseModel = value; } }


        //[XmlIgnore]
        //[Browsable(false)]
        //public object Parent { get; set; }

        //[XmlIgnore]
        //[Browsable(false)]
        //public IUnityContainer UnityContainer { get; set; }

        //[XmlIgnore]
        //public String AbsPathToXML { get; set; }

        //public String RelPathToXML { get; set; }

      //  public int Group { get { return m_Group; } set { m_Group = FB_Helper.UpdateSelectedObject(this, m_Group, value); } }


        #endregion

        #region methods

        public void Read(Byte[] fbData)
        {
            try
            {
                ByteBuffer byteBuffer = new ByteBuffer(fbData);
                m_FBData = XFBType.StaticEntity.GetRootAsStaticEntity(byteBuffer); // read 

                base.m_FBData = m_FBData.Entitybase();

                var entbase = m_FBData.Entitybase();
                var animinfo = entbase.AnimationInfo();

                var len = entbase.MeshesLength();
                var lenmat = entbase.MaterialsLength();

                var lensound = entbase.SoundsLength();
                var lenphys = entbase.PhysicsLength();

                var mat = entbase.Materials(0);
                var mat1 = entbase.Materials(1);

                var matname = mat.Name();
                var matname1 = mat1.Name();

                var phyiscs = entbase.Physics(0);
                var phyT1 = phyiscs.Boneparent();

                var meshes = entbase.Meshes(0);
                var name = meshes.Name();


                //     m_Group = XFBType.Group(); //per node!
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }

        public int Create(FlatBufferBuilder fbbChild)
        {
            return 0;
        }

        public Byte[] CreateByteBuffer(IFBObject child = null)
        {
            try
            {
                //--------------------------------------
                //create flatbuffer data
                //--------------------------------------
                FlatBufferBuilder fbb = new FlatBufferBuilder(1);
                int soOffset = XFBType.StaticEntity.CreateStaticEntity(fbb, base.Create(fbb));
                fbb.Finish(soOffset); //!!!!! important ..

                return fbb.SizedByteArray();  //bytebuffer
                //--------------------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }

            return new Byte[0];
        }

        #endregion
    }
}
