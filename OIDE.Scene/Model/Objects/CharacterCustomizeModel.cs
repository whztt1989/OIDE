#region License

// Copyright (c) 2014 Konrad Huber

#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using OIDE.Scene.Interface;
using Module.Properties.Interface;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using OIDE.Scene.Interface.Services;
using Microsoft.Practices.Unity;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Xml.Serialization;
using OIDE.VFS.VFS_Types.RootFileSystem;
using OIDE.DAL.MDB;
using Module.Protob.Utilities;
using OIDE.Scene.Model.Objects;
using System.Windows.Input;
using OIDE.InteropEditor.DLL;
using OIDE.DAL;
using Module.Properties.Helpers;
using Module.Properties.Types;
using OIDE.Scene.View.Editor;
using GongSolutions.Wpf.DragDrop;

namespace OIDE.Scene.Model
{
    //itamsource combobox
    //http://wpftoolkit.codeplex.com/discussions/351513

    //public class RaceGenderItemsSource : IItemsSource
    //{
    //    public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
    //    {
    //        Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection raceGenders = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();
          
    //        raceGenders.Add(0, "Human NEU");
    //        raceGenders.Add(1, "Human WEU");
    //        return raceGenders;
    //    }
    //}

    public class CharacterCustomizeModel : CharacterObjModel, ISceneItem, ISceneNode, IDropTarget
    {
        #region protodata

        //private RaceGenderViewModel mRaceGenderVM;
        //private List<RaceGenderViewModel> mRaceGenderVMList;

     //   private RaceGender mSelRGenderVM;

       //    [Editor(typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.ComboBoxEditor), typeof(Xceed.Wpf.Toolkit.PropertyGrid.Editors.ComboBoxEditor))]
        //    [NewItemTypes(new Type[] { typeof(RaceGenderViewModel) })]

        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            IItem sourceItem = dropInfo.Data as IItem;
            IItem targetItem = dropInfo.TargetItem as IItem;

            if (sourceItem != null && targetItem != null)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = System.Windows.DragDropEffects.Copy;
            }
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            IItem sourceItem = dropInfo.Data as IItem;
            IItem targetItem = dropInfo.TargetItem as IItem;

            targetItem.Drop(sourceItem);
            //targetItem.Children.Add(sourceItem);
        }

        /// <summary>
        /// used in combobox editor as itemsource
        /// </summary>
      //  [XmlIgnore]
      //  [Browsable(false)] 
      //  public List<RaceGenderViewModel> RaceGenderList { get { return mRaceGenderVMList; } }

      //  [Category("Customization")]
      ////  [ItemsSource(typeof(RaceGenderItemsSource))]
      //  [Editor(typeof(CharacterCustomizeEditor), typeof(CharacterCustomizeEditor))]
      //  public RaceGenderViewModel RaceGender { get { return mRaceGenderVM; } set { mRaceGenderVM = value; } }

        [Category("Customization")]
        public int Face { get { return mData.face; } set { mData.face = value; } }
        [Category("Customization")]
        public int Skin { get { return mData.skin; } set { mData.skin = value; } }
        [Category("Customization")]
        public int HairColor { get { return mData.hairColor; } set { mData.hairColor = value; } }
        [Category("Customization")]
        public int HairStyle { get { return mData.hairStyle; } set { mData.hairStyle = value; } }
        [Category("Customization")]
        public int FacialFeature { get { return mData.facialfeature; } set { mData.facialfeature = value; } }
        [Category("Customization")]
        public int FacialColor { get { return mData.facialcolor; } set { mData.facialcolor = value; } }
          
        [Category("Equipment")]
        public ProtoType.Boots Boots { get { return mData.boots; } }
        [Category("Equipment")]
        public ProtoType.Shoulder Shoulder { get { return mData.shoulder; } }

        [Category("Equipment")]
        public ProtoType.OneHandWeapon OneHandLWeapon { get { return mData.oneHLWeapon; } }
        [Category("Equipment")]
        public ProtoType.OneHandWeapon OneHandRWeapon { get { return mData.oneHRWeapon; } }
        [Category("Equipment")]
        public ProtoType.TwoHandWeapon TwoHandWeapon { get { return mData.twoHWeapon; } }

        public ProtoType.AI AI { get { return mData.ai; } }

        #endregion


        //[XmlIgnore]
        //[Browsable(false)]
        //public object DBData
        //{
        //    get { return mDBData; }
        //    set
        //    {
        //        mDBData = value as GameEntity;

        //        GameEntity dbData = value as GameEntity;
        //        ProtoType.CharEntity dataStaticObj = new ProtoType.CharEntity();

        //        if (dbData.Data != null)
        //        {
        //            mData = ProtoSerialize.Deserialize<ProtoType.CharEntity>(dbData.Data);

        //            if (mData.gameEntity == null)
        //                mData.gameEntity = new ProtoType.GameEntity();

        //            //foreach (var item in mData.gameEntity.physics)
        //            //    m_Physics.Add(new PhysicObject() { ProtoData = item });


        //            mRaceGenderVMList = new List<RaceGenderViewModel>();

        //            mRaceGenderVMList.Add(new RaceGenderViewModel(0));
        //            mRaceGenderVMList.Add(new RaceGenderViewModel(1));
        //        }
        //    }
        //}
    
        public CharacterCustomizeModel()
        {

        }

        public CharacterCustomizeModel(IItem parent, IUnityContainer unityContainer, IDAL dbI = null, Int32 id = 0)
            : base(parent, unityContainer, dbI , id)
        {
        //    mRaceGenderVMList = new List<RaceGenderViewModel>();
        }
    }
}