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
using Wide.Interfaces.Services;

namespace OIDE.Scene.Model.Objects
{
    /// <summary>
    /// class represents a physics object on view
    /// </summary>
    [Serializable]
    public class PhysicObject : ViewModelBase
    {
        private IUnityContainer UnityContainer;
        private FB_Physics m_Physics_FBData;
        private ILoggerService m_Logger;

        public enum PhysicsType : sbyte
        {
            PT_NO_COLLISION = 0,
            PT_STATIC = 1,
            PT_DYNAMIC = 2,
            PT_RIGID = 3,
            PT_SOFT = 4,
            PT_SENSOR = 5,
            PT_NAVMESH = 6,
            PT_CHARACTER = 7,
        }

        public enum ShapeType : sbyte
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

        public OIDE.Scene.Interface.Services.Vector3 Offset { get { return m_Physics_FBData.Offset; }
            set {
                 m_Physics_FBData.Offset = value;   
                RaisePropertyChanged("AnimationInfo");
            }
        }

        public String AttachToBone { get { return m_Physics_FBData.AttachToBone; } set { m_Physics_FBData.AttachToBone = value; } }

        public short colMask { get { return m_Physics_FBData.colMask; } set { m_Physics_FBData.colMask = value; } }
        public PhysicsType type { get { return m_Physics_FBData.type; } set { m_Physics_FBData.type = value; } }
        public uint mode { get { return m_Physics_FBData.mode; } set { m_Physics_FBData.mode = value; } }
        public ShapeType shape { get { return (ShapeType)m_Physics_FBData.shape; } set { m_Physics_FBData.shape = value; } }
        public float mass { get { return m_Physics_FBData.mass; } set { m_Physics_FBData.mass = value; } }
        public float margin { get { return m_Physics_FBData.margin; } set { m_Physics_FBData.margin = value; } }
        public float radius { get { return m_Physics_FBData.radius; } set { m_Physics_FBData.radius = value; } }
        public float angularDamp { get { return m_Physics_FBData.angularDamp; } set { m_Physics_FBData.angularDamp = value; } }
        public float linearDamp { get { return m_Physics_FBData.linearDamp; } set { m_Physics_FBData.linearDamp = value; } }
        public float formFactor { get { return m_Physics_FBData.formFactor; } set { m_Physics_FBData.formFactor = value; } }
        public float minVel { get { return m_Physics_FBData.minVel; } set { m_Physics_FBData.minVel = value; } }
        public float maxVel { get { return m_Physics_FBData.maxVel; } set { m_Physics_FBData.maxVel = value; } }
        public float restitution { get { return m_Physics_FBData.restitution; } set { m_Physics_FBData.restitution = value; } }
        public float friction { get { return m_Physics_FBData.friction; } set { m_Physics_FBData.friction = value; } }
        public uint colGroupMask { get { return m_Physics_FBData.colGroupMask; } set { m_Physics_FBData.colGroupMask = value; } }
        public float charStepHeight { get { return m_Physics_FBData.charStepHeight; } set { m_Physics_FBData.charStepHeight = value; } }
        public float charJumpSpeed { get { return m_Physics_FBData.charJumpSpeed; } set { m_Physics_FBData.charJumpSpeed = value; } }
        public float charFallSpeed { get { return m_Physics_FBData.charFallSpeed; } set { m_Physics_FBData.charFallSpeed = value; } }



        [Category("Entity basic")]
        [Description("animationinfo - triggers")]
   
        public int Create(FlatBufferBuilder fbb)
        {
            return m_Physics_FBData.Create(fbb);
        }

        public PhysicObject(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
            m_Physics_FBData = new FB_Physics();
            m_Logger = UnityContainer.Resolve<ILoggerService>();
        }
      
        public PhysicObject()
        {
            m_Physics_FBData = new FB_Physics();
        }
    }
}
