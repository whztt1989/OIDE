using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace OIDE.Scene.ViewModel
{
    public class RaceGenderViewModel
    {
        private ProtoType.RaceGender mProtoData;

        public RaceGenderViewModel(ProtoType.RaceGender data)
        {
            mProtoData = data;

            if (mProtoData.bodyPhys == null) mProtoData.bodyPhys = new ProtoType.PhysicsObject();
            if (mProtoData.footLPhys == null) mProtoData.footLPhys = new ProtoType.PhysicsObject();
            if (mProtoData.footRPhys == null) mProtoData.footRPhys = new ProtoType.PhysicsObject();
            if (mProtoData.HandLPhys == null) mProtoData.HandLPhys = new ProtoType.PhysicsObject();
            if (mProtoData.HandRPhys == null) mProtoData.HandRPhys = new ProtoType.PhysicsObject();

            //mProtoData = new ProtoType.RaceGender();
            //mProtoData.race = new ProtoType.Race();
            //if (idx == 0)
            //    mProtoData.race.name = "Human NEU";
            //else if (idx == 1)
            //    mProtoData.race.name = "Human WEU";
        }

        public String Name { get { return mProtoData.race.name; } set { mProtoData.race.name = value; } }
        public String Description { get { return mProtoData.race.description; } set { mProtoData.race.description = value; } }
       // public ProtoType.Race Race { get { return mProtoData.race; } set { mProtoData.race = value; } }
        [Category("Physic")]
        [ExpandableObject]
        public ProtoType.PhysicsObject BodyPhys { get { return mProtoData.bodyPhys; } set { mProtoData.bodyPhys = value; } }
        [Category("Physic")]
        [ExpandableObject]
        public ProtoType.PhysicsObject FootLPhys { get { return mProtoData.footLPhys; } set { mProtoData.footLPhys = value; } }
        [Category("Physic")]
        [ExpandableObject]
        public ProtoType.PhysicsObject FootRPhys { get { return mProtoData.footRPhys; } set { mProtoData.footRPhys = value; } }
        [Category("Physic")]
        [ExpandableObject]
        public ProtoType.PhysicsObject HandLPhys { get { return mProtoData.HandLPhys; } set { mProtoData.HandLPhys = value; } }
        [Category("Physic")]
        [ExpandableObject]
        public ProtoType.PhysicsObject HanfRPhys { get { return mProtoData.HandRPhys; } set { mProtoData.HandRPhys = value; } }

        public float TurnSpeed { get { return mProtoData.turnSpeed; } set { mProtoData.turnSpeed = value; } }
        public ProtoType.Gender Gender { get { return mProtoData.gender; } set { mProtoData.gender = value; } }
        public List<ProtoType.Sound> Sound { get { return mProtoData.sounds; } set { } }
        public List<ProtoType.Face> Face { get { return mProtoData.faces; } set {  } }
        public List<ProtoType.Skin> Skin { get { return mProtoData.skins; } set {  } }
        public List<ProtoType.HairColor> HairColor { get { return mProtoData.hairColors; } set { } }
        public List<ProtoType.HairStyle> HairStyle { get { return mProtoData.hairStyles; } set {  } }
        public List<ProtoType.FacialFeature> FacialFeature { get { return mProtoData.facials; } set {  } }
        public List<ProtoType.FacialColor> FacialColor { get { return mProtoData.facialcolors; } set {  } }
        public List<ProtoType.Shoulder> Shoulder { get { return mProtoData.shoulder; } set { ; } }
        public List<ProtoType.Boots> Boots { get { return mProtoData.boots; } set {  } }
        public List<ProtoType.SpellEntity> SpellEntity { get { return mProtoData.spells; } set {  } }
    }

    public enum RaceGender
    {
        HumanNEU,
        HumaWEU
    }
}
