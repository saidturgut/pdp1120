namespace pdp11_emulator.Executing;
using Signaling;
using Components;

public partial class DataPath
{
    public void CpuBusDrive(TriStateBus cpuBus)
    {
        if(signals.CpuBusDriver is Register.NONE)
            return;
        
        cpuBus.Set((ushort)(Access(signals.CpuBusDriver).Get() & 0xFFFF));
    }
    
    public void CpuBusLatch(TriStateBus cpuBus, TriStateBus aluBus)
    {
        if(signals.CpuBusLatcher is Register.NONE)
            return;
        
        if (signals.AluAction is not null)
            cpuBus = aluBus;

        Access(signals.CpuBusLatcher).Set(cpuBus.Get());
    }
}