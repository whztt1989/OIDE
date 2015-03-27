using Microsoft.Practices.Unity;
using Module.Properties.Interface;
using Module.Properties.Interface.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wide.Core.Services;
using Wide.Interfaces.Services;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace OIDE.Gorilla.Model
{
    public struct Kerning
    {
        public int RightGlyphID { get; set; }
        public int KerningValue { get; set; }
    }

    public class Glyph
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class FontData : PItem
    {
        private GorillaModel m_Gorilla;

        public int Index { get; set; }

        [ExpandableObject]
        public Glyph Glyph { get; set; }
        public ObservableCollection<Kerning> Kerning { get; set; }
        public int VerticalOffset { get; set; }

        public FontData(GorillaModel gorilla)
        {
            m_Gorilla = gorilla;
            Kerning = new ObservableCollection<Kerning>();
            UnityContainer = gorilla.UnityContainer;
        }

        [Browsable(false)]
        public IUnityContainer UnityContainer { get; set; }
        [Browsable(false)]
        public CollectionOfIItem Items { get; set; }
        public String ContentID { get; private set; }
        [Browsable(false)]
        public bool HasChildren { get; set; }
        [Browsable(false)]
        public bool IsExpanded { get; set; }

        private Boolean m_IsSelected;
        [Browsable(false)]
        public bool IsSelected
        {
            get { return m_IsSelected; }
            set
            {
                m_IsSelected = value;
                var propService = UnityContainer.Resolve<IPropertiesService>();
                propService.CurrentItem = this;


                m_Gorilla.SelectedRectangle.Stroke = System.Windows.Media.Brushes.Red;
                m_Gorilla.SelectedRectangle.StrokeThickness = 2;
                m_Gorilla.SelectedRectangle.Width = Glyph.width;
                m_Gorilla.SelectedRectangle.Height = Glyph.height;

                Canvas.SetLeft(m_Gorilla.SelectedRectangle, Glyph.X);
                Canvas.SetTop(m_Gorilla.SelectedRectangle, Glyph.Y);
                //    var gorillaService = m_container.Resolve<IGorillaService>();
                //    gorillaService.SelectedGorilla = this;
            }
        }
        [Browsable(false)]
        public List<System.Windows.Controls.MenuItem> MenuOptions { get; set; }
        public string Name { get; set; }
        [Browsable(false)]
        public IItem Parent { get; set; }

        public bool Create(IUnityContainer unityContainer) { return true; }
        public bool Delete() { return true; }
        public void Drop(IItem item) { }
        public void Finish() { }
        public bool Open(IUnityContainer unityContainer, object paramID) { return true; }
        public void Refresh() { }
        public bool Save(object param = null) { return true; }
    }
}
