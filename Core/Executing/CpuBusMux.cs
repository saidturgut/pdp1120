namespace pdp11_emulator.Core.Executing;
using Signaling;
using Components;

public partial class DataPath
{
    public void CpuBusDrive(TriStateBus cpuBus)
    {
        if(signals.CpuBusDriver is RegisterAction.NONE)
            return;
        
        cpuBus.Set(Access(signals.CpuBusDriver).Get());
    }
    
    public void CpuBusLatch(TriStateBus cpuBus, TriStateBus aluBus)
    {
        if(signals.CpuBusLatcher is RegisterAction.NONE)
            return;
        
        if (signals.AluAction is not null)
            cpuBus = aluBus;

        Access(signals.CpuBusLatcher).Set(cpuBus.Get());
    }
}