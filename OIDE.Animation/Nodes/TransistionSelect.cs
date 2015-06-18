using DLL.NodeEditor.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace OIDE.Animation.Nodes
{
  
    public class TransistionSelect : DynamicNode
    {
        //protected override Effect GetEffect()
        //{
        //    return new MultiplyEffect
        //    {
        //        Input1 = new ImageBrush(InputConnectors[0].Value),
        //        Input2 = new ImageBrush(InputConnectors[1].Value)
        //    };
        //}

        protected override void PrepareDrawingVisual(DrawingVisual drawingVisual)
        {
          //  drawingVisual.Effect = GetEffect();
        }

        protected override void Draw(DrawingContext drawingContext, Rect bounds)
        {
            drawingContext.DrawRectangle( new SolidColorBrush(Colors.Blue), null,  bounds);
        }


        public TransistionSelect()
        {
            AddInputConnector("Select", Colors.DarkSeaGreen);
            AddInputConnector("Time In", Colors.DarkBlue);
            AddInputConnector("Time Out", Colors.DarkBlue);
            AddInputConnector("Anim 1", Colors.DarkBlue);
            AddInputConnector("Anim 2", Colors.DarkBlue);
        }
    }
}
