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

namespace OIDE.Scene.Model.Objects
{
    [Serializable]
    public class FB_StaticObjectModel : IFBObject
    {
        #region private members
       
        private XFBType.StaticEntity m_FBData = new XFBType.StaticEntity();
        private int m_Group;
   
        private FB_EntityBaseModel m_EntityBaseModel;

        #endregion
        
        #region Properties

       [XmlIgnore]
       public String AbsPathToXML { get; set; }

       public String RelPathToXML { get; set; }

       public int Group { get { return m_Group; } set { m_Group = value; } }
      
        /// <summary>
        /// only for serialization
        /// </summary>
      public FB_EntityBaseModel EntityBaseModel { get { return m_EntityBaseModel; } set { m_EntityBaseModel = value; } }


        #endregion

       #region methods

       public void Read(Byte[] fbData)
        {
            try { 
            ByteBuffer byteBuffer = new ByteBuffer(fbData);
            m_FBData = XFBType.StaticEntity.GetRootAsStaticEntity(byteBuffer); // read 

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
            var meshes = m_FBData.Entitybase().Meshes(0);
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

       public Byte[] CreateByteBuffer(IFBObject child)
        {
            try
            {
                //--------------------------------------
                //create flatbuffer data
                //--------------------------------------
                FlatBufferBuilder fbb = new FlatBufferBuilder(1);
                int soOffset = XFBType.StaticEntity.CreateStaticEntity(fbb, child.Create(fbb));
                fbb.Finish(soOffset); //!!!!! important ..

                return fbb.SizedByteArray();  //bytebuffer
                //--------------------------------------
            }
            catch(Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }

           return new Byte[0];
        }

       #endregion
    }
}
