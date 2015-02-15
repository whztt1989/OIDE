using FlatBuffers;
using Module.Protob.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces;
using OIDE.InteropEditor.DLL;
using System.Xml.Serialization;
using System.Windows;
using XFBType;

namespace OIDE.Scene.Model.Objects
{
    [Serializable]
    public class FB_Physics : IFBObject
    {
        private XFBType.Scene m_FBData = new XFBType.Scene();
   
        #region sceneData

        private System.Windows.Media.Color m_ColourAmbient;

        private short m_colMask;
        private OIDE.Scene.Model.Objects.PhysicObject.PhysicsType m_type;
        private uint m_mode;
        private OIDE.Scene.Model.Objects.PhysicObject.ShapeType m_shape;
        private float m_mass;
        private float m_margin;
        private float m_radius;
        private float m_angularDamp;
        private float m_linearDamp;
        private float m_formFactor;
        private float m_minVel;
        private float m_maxVel;
        private float m_restitution;
        private float m_friction;
        private uint m_colGroupMask;
        private float m_charStepHeight;
        private float m_charJumpSpeed;
        private float m_charFallSpeed;

        private OIDE.Scene.Interface.Services.Vector3 m_Offset;
        private String m_AttachToBone;


        #endregion

        #region Properties

        public String AbsPathToXML { get; set; }
        public String RelPathToXML { get; set; }
        public System.Windows.Media.Color ColourAmbient { get { return m_ColourAmbient; } set { m_ColourAmbient = value; } }

        public OIDE.Scene.Interface.Services.Vector3 Offset { get { return m_Offset; } set { m_Offset = value; } }
        public String AttachToBone { get { return m_AttachToBone; } set { m_AttachToBone = value; } }

        public short colMask { get { return m_colMask; } set { m_colMask = value; } }
        public OIDE.Scene.Model.Objects.PhysicObject.PhysicsType type { get { return m_type; } set { m_type = value; } }
        public uint mode { get { return m_mode; } set { m_mode = value; } }
        public OIDE.Scene.Model.Objects.PhysicObject.ShapeType shape { get { return m_shape; } set { m_shape = value; } }
        public float mass { get { return m_mass; } set { m_mass = value; } }
        public float margin { get { return m_margin; } set { m_margin = value; } }
        public float radius { get { return m_radius; } set { m_radius = value; } }
        public float angularDamp { get { return m_angularDamp; } set { m_angularDamp = value; } }
        public float linearDamp { get { return m_linearDamp; } set { m_linearDamp = value; } }
        public float formFactor { get { return m_formFactor; } set { m_formFactor = value; } }
        public float minVel { get { return m_minVel; } set { m_minVel = value; } }
        public float maxVel { get { return m_maxVel; } set { m_maxVel = value; } }
        public float restitution { get { return m_restitution; } set { m_restitution = value; } }
        public float friction { get { return m_friction; } set { m_friction = value; } }
        public uint colGroupMask { get { return m_colGroupMask; } set { m_colGroupMask = value; } }
        public float charStepHeight { get { return m_charStepHeight; } set { m_charStepHeight = value; } }
        public float charJumpSpeed { get { return m_charJumpSpeed; } set { m_charJumpSpeed = value; } }
        public float charFallSpeed { get { return m_charFallSpeed; } set { m_charFallSpeed = value; } }

        public int SetColourAmbient(System.Windows.Media.Color color)
        {
            int res = 0;
            m_ColourAmbient = color;

            //send to c++ DLL
            Byte[] tmp = CreateByteBuffer();

            //if (DLL_Singleton.Instance != null)
            //{
            //  todo  res = DLL_Singleton.Instance.command("cmd sceneUpdate 0", tmp, tmp.Length);
            //}
            return res;
        }

        #endregion


        #region IFBObject

        /// <summary>
        /// reads flatbuffers byte data into object
        /// </summary>
        /// <param name="fbData"></param>
        public void Read(Byte[] fbData)
        {
            ByteBuffer byteBuffer = new ByteBuffer(fbData);

            m_FBData = XFBType.Scene.GetRootAsScene(byteBuffer); // read 
            XFBType.Colour colour = m_FBData.ColourAmbient();

            m_ColourAmbient = System.Windows.Media.Color.FromScRgb(colour.A(), colour.R(), colour.G(), colour.B());

            ByteBuffer byteBuffer2 = new ByteBuffer(fbData);
            var m_FBDataNOT = XFBType.Scene.GetRootAsScene(byteBuffer2); // read      
            XFBType.Colour colourNOT = m_FBDataNOT.ColourAmbient();
            m_ColourAmbient = System.Windows.Media.Color.FromScRgb(colourNOT.A(), colourNOT.R(), colourNOT.G(), colourNOT.B());
        }

        //not implemented
        public int Create(FlatBufferBuilder fbbParent)
        {
           int boneParentOS = 0;
           if (m_AttachToBone != null)
                boneParentOS = fbbParent.CreateString(m_AttachToBone);

            XFBType.PhysicsObject.StartPhysicsObject(fbbParent);
            XFBType.PhysicsObject.AddColMask(fbbParent, m_colMask);
            XFBType.PhysicsObject.AddType(fbbParent, (sbyte)m_type);
            XFBType.PhysicsObject.AddMode(fbbParent, m_mode);
            XFBType.PhysicsObject.AddShape(fbbParent, (sbyte)m_shape);
            XFBType.PhysicsObject.AddMass(fbbParent, m_mass);
            XFBType.PhysicsObject.AddMargin(fbbParent, m_margin);
            XFBType.PhysicsObject.AddRadius(fbbParent, m_radius);
            XFBType.PhysicsObject.AddAngularDamp(fbbParent, m_angularDamp);
            XFBType.PhysicsObject.AddLinearDamp(fbbParent, m_linearDamp);
            XFBType.PhysicsObject.AddFormFactor(fbbParent, m_formFactor);
            XFBType.PhysicsObject.AddMinVel(fbbParent, m_minVel);
            XFBType.PhysicsObject.AddMaxVel(fbbParent, m_maxVel);
            XFBType.PhysicsObject.AddRestitution(fbbParent, m_restitution);
            XFBType.PhysicsObject.AddFriction(fbbParent, m_friction);
            XFBType.PhysicsObject.AddColGroupMask(fbbParent, m_colGroupMask);
            XFBType.PhysicsObject.AddCharStepHeight(fbbParent, m_charStepHeight);
            XFBType.PhysicsObject.AddCharJumpSpeed(fbbParent, m_charJumpSpeed);
            XFBType.PhysicsObject.AddCharFallSpeed(fbbParent, m_charFallSpeed);

            //Structures are always stored inline, they need to be created right where they're used
            XFBType.PhysicsObject.AddOffset(fbbParent, XFBType.Vec3f.CreateVec3f(fbbParent, m_Offset.X, m_Offset.Y, m_Offset.Z));
            
            if (m_AttachToBone != null)
                XFBType.PhysicsObject.AddBoneparent(fbbParent, boneParentOS);
          
            return fbbParent.EndVector();
        }


        /// <summary>
        /// resets the flatbufferbuilder
        /// </summary>
        /// <returns>byte data</returns>
        public Byte[] CreateByteBuffer(IFBObject child = null)
        {
            throw new Exception("CreateByteBuffer - not implemented");
        }

        #endregion
    }
}
