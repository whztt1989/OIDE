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

using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using Module.Properties.Interface;
using Wide.Core.TextDocument;
using Wide.Interfaces;
using Wide.Interfaces.Services;
using OIDE.Scene.Interface.Services;
using System.Xml.Serialization;
using Microsoft.Practices.Unity;
using System.Windows.Input;
using DAL;
using System.IO;
using OIDE.Scene.Model;
using OIDE.Scene.Interface;
using Wide.Core.Services;

namespace OIDE.Scene
{
    public class CharacterCategoryModel : PItem, ISceneItem
    {
        public Int32 NodeID { get; set; }
      
        public void Drop(IItem item) { }

        [Browsable(false)]
        [XmlIgnore]
        public CollectionOfISceneItem SceneItems { get; private set; }
   
        [Browsable(false)]
        [XmlIgnore]
        public override List<MenuItem> MenuOptions 
        {
              get
            {
                List<MenuItem>  menuOptions = new List<MenuItem>();
                MenuItem miAdd = new MenuItem() { Command = new CmdAddCharacterObj(UnityContainer), CommandParameter = this, Header = "Create character object" };
                menuOptions.Add(miAdd);

          //      MenuItem miAdd2 = new MenuItem() { Command = new CmdAddCharacterCustomizeObj(UnityContainer), CommandParameter = this, Header = "Create character cust object" };
          //      menuOptions.Add(miAdd2);

                return menuOptions;
            }
        }
 
        public Boolean Visible { get; set; }

        public override Boolean Create(IUnityContainer unityContainer) { return true; }
        public override Boolean Open(IUnityContainer unityContainer, object id) { return true; }
        public override Boolean Save(object param) { return true; }
        public override void Refresh() { }

        public override Boolean Delete() { return true; }

        public CharacterCategoryModel()
        {
            SceneItems = new CollectionOfISceneItem();
        }
    }

    public class CmdAddCharacterObj : ICommand
    {
        private IUnityContainer mContainer;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            CharacterCategoryModel parent = parameter as CharacterCategoryModel;
            UInt32 id = 0;

            if(parent != null)
            {
                var tableModel = parent.Parent as DBTableModel;
                if(tableModel != null)
                {
                    id = tableModel.AutoIncrement();
                }
            }

            if (id > 0)
            {
                CharacterEntity pom = new CharacterEntity() { Parent = parent, Name = "Character Obj NEW", ContentID = "CharacterEntID:##" + id };

                pom.Create(parent.UnityContainer);
                parent.Items.Add(pom);

                ISceneService sceneService = parent.UnityContainer.Resolve<ISceneService>();
            }else
            {
                parent.UnityContainer.Resolve<ILoggerService>().Log("Error: CmdAddCharacterObj id =  (" + id.ToString() + ")", LogCategory.Error, LogPriority.High); 
            }
        }

        public CmdAddCharacterObj(IUnityContainer container)
        {
            mContainer = container;
        }
    }
}