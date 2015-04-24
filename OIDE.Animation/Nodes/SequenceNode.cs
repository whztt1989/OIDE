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
  
    public class SequenceNode : DynamicNode
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


        public SequenceNode()
        {
            AddInputConnector("Left", Colors.DarkSeaGreen);
            AddInputConnector("Right", Colors.DarkSeaGreen);
        }
    }
}
