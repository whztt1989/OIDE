using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Wide.Interfaces;

namespace OIDE.Scene.Model.Objects
{
    public class PhysicObject : ViewModelBase
    {
        public enum ShapeType
        {
            SH_BOX,
            SH_CONE,
            SH_CYLINDER,
            SH_SPHERE,
            SH_CONVEX_TRIMESH,
            SH_GIMPACT_MESH,
            SH_BVH_MESH,
            SH_CAPSULE,
            SH_PLANE
        }

        //todo proto def
        public Point Offset { get; set; }
        public String AttachToBone { get; set; }

        public uint colMask { get { return ProtoData.colMask; } set { ProtoData.colMask = value; } }
        public int type { get { return ProtoData.type; } set { ProtoData.type = value; } }
        public int mode { get { return ProtoData.mode; } set { ProtoData.mode = value; } }
        public ShapeType shape { get { return (ShapeType)ProtoData.shape; } set { ProtoData.shape = (int)value; } }
        public float mass { get { return ProtoData.mass; } set { ProtoData.mass = value; } }
        public float margin { get { return ProtoData.margin; } set { ProtoData.margin = value; } }
        public float radius { get { return ProtoData.radius; } set { ProtoData.radius = value; } }
        public float angularDamp { get { return ProtoData.angularDamp; } set { ProtoData.angularDamp = value; } }
        public float linearDamp { get { return ProtoData.linearDamp; } set { ProtoData.linearDamp = value; } }
        public float formFactor { get { return ProtoData.formFactor; } set { ProtoData.formFactor = value; } }
        public float minVel { get { return ProtoData.minVel; } set { ProtoData.minVel = value; } }
        public float maxVel { get { return ProtoData.maxVel; } set { ProtoData.maxVel = value; } }
        public float restitution { get { return ProtoData.restitution; } set { ProtoData.restitution = value; } }
        public float friction { get { return ProtoData.friction; } set { ProtoData.friction = value; } }
        public uint colGroupMask { get { return ProtoData.colGroupMask; } set { ProtoData.colGroupMask = value; } }
        public float charStepHeight { get { return ProtoData.charStepHeight; } set { ProtoData.charStepHeight = value; } }
        public float charJumpSpeed { get { return ProtoData.charJumpSpeed; } set { ProtoData.charJumpSpeed = value; } }
        public float charFallSpeed { get { return ProtoData.charFallSpeed; } set { ProtoData.charFallSpeed = value; } }

        [XmlIgnore]
        [Browsable(false)]
        public ProtoType.PhysicsObject ProtoData { get; set; }

    
        public PhysicObject()
        {
            ProtoData = new ProtoType.PhysicsObject();
        }
    }
}
