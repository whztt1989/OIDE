using Microsoft.Practices.Unity;
using Module.Protob.Interface;
using OIDE.InteropEditor.DLL;
using OIDE.Scene.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wide.Interfaces.Services;

namespace OIDE.Scene.Model.Objects.FBufferObject
{
    public static class FB_Helper
    {
        //update selected object on c++ side
        public static T UpdateSelectedObject<T>(IFBObject objectdata, T oldValue, T newValue)
        {
            int res = 0;

            String objectName = objectdata.GetType().Name; //object name e.g. FB_Physics

            //todo   send only if changed from GUI!
            var sceneData = objectdata.Parent as IScene;
            if (sceneData != null)
            {
                var logger = (sceneData.UnityContainer as UnityContainer).Resolve<ILoggerService>();
                logger.Log("Flatbuffer SceneDataModel.ColourAmbient.SetColourAmbient ungültig (" + newValue.ToString() + "): " + res, LogCategory.Error, LogPriority.High);
             

                //send to c++ DLL
                Byte[] tmp = objectdata.CreateByteBuffer(null);

                if (DLL_Singleton.Instance != null)
                {
                    //  todo  res = DLL_Singleton.Instance.command("cmd update 0", tmp, tmp.Length);
                }
            }

            if (res == 0) // OK = 0
            {
                oldValue = newValue;
            }

            return oldValue;
        }
    }
}
