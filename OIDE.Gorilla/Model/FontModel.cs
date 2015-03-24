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

namespace OIDE.Gorilla.Model
{

    public class FontModel : IItem
    {
        private GorillaModel m_Gorilla;
        private CollectionOfIItem mFonts;
        public CollectionOfIItem Items { get { return mFonts; } }

        public int size { get; set; }
        public int lineheight { get; set; }
        public int spacelength { get; set; }
        public int baseline { get; set; }
        public float kerning { get; set; }
        public int letterspacing { get; set; }
        public int monowidth { get; set; }
        public int rangeFrom { get; set; }
        public int rangeTo { get; set; }

        public FontModel(GorillaModel gorilla)
        {
            m_Gorilla = gorilla;
            UnityContainer = gorilla.UnityContainer;
            mFonts = new CollectionOfIItem();
        }

        public void SetGlyph(int index, Glyph glyph)
        {
            var font = mFonts.Where(x => (x as FontData).Index == index);
            if (font.Any())
                (font.First() as FontData).Glyph = glyph;
            else
                mFonts.Add(new FontData(m_Gorilla) { Name = index.ToString(), Index = index, Glyph = glyph });
        }

        public void SetVerticalOffset(int index, int offset)
        {
            var font = mFonts.Where(x => (x as FontData).Index == index);
            if (font.Any())
                (font.First() as FontData).VerticalOffset = offset;
            else
                mFonts.Add(new FontData(m_Gorilla) { Name = index.ToString(), Index = index, VerticalOffset = offset });
        }

        public void SetKerning(int index, Kerning kerning)
        {
            var font = mFonts.Where(x => (x as FontData).Index == index);
            if (font.Any())
                (font.First() as FontData).Kerning.Add(kerning);
            else
                mFonts.Add(new FontData(m_Gorilla) { Name = index.ToString(), Index = index, Kerning = new ObservableCollection<Kerning>() { kerning } });
        }

        [Browsable(false)]
        public IUnityContainer UnityContainer { get; set; }
        [Browsable(false)]
        //public CollectionOfIItem Items { get; set; }
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
