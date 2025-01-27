using GHelper.Gpu.AMD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PowerModeWinUI.GPU
{
    internal class Amd
    {

        public bool GetAMD()
        {
            AmdGpuControl _gpuControl = new AmdGpuControl();

            if (_gpuControl.IsValid)    //either there's no AMD dGPU, or otherwise, unable to utilize the detection system 
            {

                Debug.WriteLine(_gpuControl.FullName);
                return true;
            }
            else
            {
                _gpuControl.Dispose();
                return false;
            }

        }

        public bool AmdDGPUIsActive()
        {
            var amdI = new GHelper.Gpu.AMD.AmdGpuControl();


            try
            {
                int status = amdI.GetAMD_GPU_Status();
                if (status == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return false;
            }


        }

    }
}
