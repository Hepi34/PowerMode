using NvAPIWrapper.GPU;
using NvAPIWrapper;
using System.Diagnostics;

namespace PowerModeWinUI.GPU
{
    internal class Nvidia
    {
        public bool NVIDIApresent()
        {
            try
            {
                NVIDIA.Initialize();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool GpuType()
        {
            NVIDIA.Initialize();

            PhysicalGPU gpu = PhysicalGPU.GetPhysicalGPUs()[0];

            try
            {
                //Dumb way, but when the GPU is off, it throws an exception. I did this so it wouldn't wake the GPU from sleep every time. I save power this way.
                Debug.WriteLine(gpu.PerformanceStatesInfo);
                return true;
            }
            catch
            {

                return false;
            }

        }
    }
}
