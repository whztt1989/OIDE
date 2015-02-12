#region License

    //The MIT License (MIT)

    //Copyright (c) 2014 Konrad Huber

    //Permission is hereby granted, free of charge, to any person obtaining a copy
    //of this software and associated documentation files (the "Software"), to deal
    //in the Software without restriction, including without limitation the rights
    //to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    //copies of the Software, and to permit persons to whom the Software is
    //furnished to do so, subject to the following conditions:

    //The above copyright notice and this permission notice shall be included in all
    //copies or substantial portions of the Software.

    //THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    //IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    //FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    //AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    //LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    //OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    //SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using FlatBuffers;
using Microsoft.Practices.Unity;
using Wide.Interfaces;

namespace OIDE.Scene.Model.Objects
{
    [Serializable]
    public class PhysicObject 
    {
        private IUnityContainer UnityContainer;
        private FB_PhysicsModel m_Physics_FBData;

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

        //public uint colMask { get { return ProtoData.colMask; } set { ProtoData.colMask = value; } }
        //public ProtoType.PhysicsType type { get { return ProtoData.type; } set { ProtoData.type = value; } }
        //public int mode { get { return ProtoData.mode; } set { ProtoData.mode = value; } }
        //public ShapeType shape { get { return (ShapeType)ProtoData.shape; } set { ProtoData.shape = (int)value; } }
        //public float mass { get { return ProtoData.mass; } set { ProtoData.mass = value; } }
        //public float margin { get { return ProtoData.margin; } set { ProtoData.margin = value; } }
        //public float radius { get { return ProtoData.radius; } set { ProtoData.radius = value; } }
        //public float angularDamp { get { return ProtoData.angularDamp; } set { ProtoData.angularDamp = value; } }
        //public float linearDamp { get { return ProtoData.linearDamp; } set { ProtoData.linearDamp = value; } }
        //public float formFactor { get { return ProtoData.formFactor; } set { ProtoData.formFactor = value; } }
        //public float minVel { get { return ProtoData.minVel; } set { ProtoData.minVel = value; } }
        //public float maxVel { get { return ProtoData.maxVel; } set { ProtoData.maxVel = value; } }
        //public float restitution { get { return ProtoData.restitution; } set { ProtoData.restitution = value; } }
        //public float friction { get { return ProtoData.friction; } set { ProtoData.friction = value; } }
        //public uint colGroupMask { get { return ProtoData.colGroupMask; } set { ProtoData.colGroupMask = value; } }
        //public float charStepHeight { get { return ProtoData.charStepHeight; } set { ProtoData.charStepHeight = value; } }
        //public float charJumpSpeed { get { return ProtoData.charJumpSpeed; } set { ProtoData.charJumpSpeed = value; } }
        //public float charFallSpeed { get { return ProtoData.charFallSpeed; } set { ProtoData.charFallSpeed = value; } }

        //[XmlIgnore]
        //[Browsable(false)]
        //public ProtoType.PhysicsObject ProtoData { get; set; }

   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fbb"></param>
        /// <returns>flatbuffers offset</returns>
        public int Create(FlatBufferBuilder fbb)
        {
            XFBType.PhysicsObject.StartPhysicsObject(fbb);
           
            return XFBType.PhysicsObject.EndPhysicsObject(fbb);
        }

        public PhysicObject(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer; 
        }


      
        public PhysicObject()
        {
          //  ProtoData = new ProtoType.PhysicsObject();
        }
    }
}
