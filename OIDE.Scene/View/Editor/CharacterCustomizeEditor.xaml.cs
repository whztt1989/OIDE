﻿using OIDE.Scene.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using GongSolutions.Wpf.DragDrop;

namespace OIDE.Scene.View.Editor
{
    /// <summary>
    /// Interaktionslogik für CharacterCustomizeEditor.xaml
    /// </summary>
    public partial class CharacterCustomizeEditor : UserControl, ITypeEditor
    {
        public CharacterCustomizeEditor()
        {
            InitializeComponent();
            

        }


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(CharacterCustomizeEditor),
                                                                                             new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        //[NotNullValidator(MessageTemplate = "Customer must have valid no")]
        //[StringLengthValidator(5, RangeBoundaryType.Inclusive, 5, RangeBoundaryType.Inclusive, MessageTemplate = "Customer no must have {3} characters.")]
        //[RegexValidator("[A-Z]{2}[0-9]{3}", MessageTemplate = "Customer no must be 2 capital letters and 3 numbers.")]
        //[Required(ErrorMessage = "Customer no can not be empty")]
        //[StringLength(5, ErrorMessage = "Customer no must be 5 characters.")]
        //[RegularExpression("[A-Z]{2}[0-9]{3}", ErrorMessage = "Customer no must be 2 capital letters and 3 numbers.")]
        public object Value
        {
            get { return GetValue(ValueProperty); }
            private set
            {
                SetValue(ValueProperty, value);
            }
        }

        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            CharacterCustomizeModel ccm = (CharacterCustomizeModel)propertyItem.Instance;
            cbRaceGender.ItemsSource =  ccm.RaceGenderList;

            Binding binding = new Binding("Value");
            binding.Source = propertyItem;
            binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(this, CharacterCustomizeEditor.ValueProperty, binding);
            return this;
        }

    }
}
