
using System.Runtime.InteropServices;
using static GHelper.Gpu.AMD.Adl2.NativeMethods;

namespace GHelper.Gpu.AMD;

// Reference: https://github.com/GPUOpen-LibrariesAndSDKs/display-library/blob/master/Sample-Managed/Program.cs
public class AmdGpuControl
{
    private bool _isReady;
    private nint _adlContextHandle;

    private readonly ADLAdapterInfo _internalDiscreteAdapter;
    private readonly ADLAdapterInfo? _iGPU;


    public string FullName => _internalDiscreteAdapter!.AdapterName;

    private ADLAdapterInfo? FindByType(ADLAsicFamilyType type = ADLAsicFamilyType.Discrete)
    {
        ADL2_Adapter_NumberOfAdapters_Get(_adlContextHandle, out int numberOfAdapters);
        if (numberOfAdapters <= 0)
            return null;

        ADLAdapterInfoArray osAdapterInfoData = new();
        int osAdapterInfoDataSize = Marshal.SizeOf(osAdapterInfoData);
        nint AdapterBuffer = Marshal.AllocCoTaskMem(osAdapterInfoDataSize);
        Marshal.StructureToPtr(osAdapterInfoData, AdapterBuffer, false);
        if (ADL2_Adapter_AdapterInfo_Get(_adlContextHandle, AdapterBuffer, osAdapterInfoDataSize) != Adl2.ADL_SUCCESS)
            return null;

        osAdapterInfoData = (ADLAdapterInfoArray)Marshal.PtrToStructure(AdapterBuffer, osAdapterInfoData.GetType())!;

        const int amdVendorId = 1002;   //this is always the same for AMD devices

        // Determine which GPU is internal discrete AMD GPU
        ADLAdapterInfo internalDiscreteAdapter =
            osAdapterInfoData.ADLAdapterInfo
                .FirstOrDefault(adapter =>
                {
                    if (adapter.Exist == 0 || adapter.Present == 0)
                        return false;

                    if (adapter.VendorID != amdVendorId)
                        return false;

                    if (ADL2_Adapter_ASICFamilyType_Get(_adlContextHandle, adapter.AdapterIndex, out ADLAsicFamilyType asicFamilyType, out int asicFamilyTypeValids) != Adl2.ADL_SUCCESS)
                        return false;

                    asicFamilyType = (ADLAsicFamilyType)((int)asicFamilyType & asicFamilyTypeValids);

                    return (asicFamilyType & type) != 0;
                });

        if (internalDiscreteAdapter.Exist == 0)
            return null;

        return internalDiscreteAdapter;

    }

    public AmdGpuControl()
    {
       

        try
        {
            if (Adl2.ADL2_Main_Control_Create(1, out _adlContextHandle) != Adl2.ADL_SUCCESS) return;
        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        ADLAdapterInfo? internalDiscreteAdapter = FindByType(ADLAsicFamilyType.Discrete);

        if (internalDiscreteAdapter is not null)
        {
            _internalDiscreteAdapter = (ADLAdapterInfo)internalDiscreteAdapter;
            _isReady = true;
        }

        _iGPU = FindByType(ADLAsicFamilyType.Integrated);

    }

    public bool IsValid => _isReady && _adlContextHandle != nint.Zero;




    public bool IsReady => _isReady;
    


  
    // Returns -1 if no iGPU is detected (ie: Ryzen CPU without inertal Radeon graphics)
    
    // Returns 0 if only iGPU is active
    // Returns 1 if the dGPU is active
    public int GetAMD_GPU_Status()
    {



        if (_iGPU == null  ) // Checks if only a
        {
            return -1; //iGPU was never assigned a handle, thus, doesn't exist.
        }



        //TODO could use a try catch


        if (GetGPUActiveAdapter() == 0) //if this outputs 0, the main dGPU isn't active
        {
            return 0;   //the dGPU isn't active
        }

        return 1;   //the dGPU is active
    }



    public int? GetiGPUActiveAdapter()  //unused, but could be useful. Outputs 0 if iGPU inactive, else, active. Outputs -1 for error
    {
        
        if (_adlContextHandle == nint.Zero || _iGPU == null) return -1;
        if (ADL2_Adapter_Active_Get(_adlContextHandle, ((ADLAdapterInfo)_iGPU).AdapterIndex, out int isActive) != Adl2.ADL_SUCCESS) return -1;

  
        return isActive;

    }


    public int? GetGPUActiveAdapter() //Outputs 0 if dGPU is inactive, else, active. Outputs -1 for error
    {

        if (_adlContextHandle == nint.Zero) return -1;
        if (ADL2_Adapter_Active_Get(_adlContextHandle, _internalDiscreteAdapter.AdapterIndex, out int issActive) != Adl2.ADL_SUCCESS) return -1;



       return issActive;

    }







    private void ReleaseUnmanagedResources()
    {
        if (_adlContextHandle != nint.Zero)
        {
            ADL2_Main_Control_Destroy(_adlContextHandle);
            _adlContextHandle = nint.Zero;
            _isReady = false;
        }
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~AmdGpuControl()
    {
        ReleaseUnmanagedResources();
    }
}
