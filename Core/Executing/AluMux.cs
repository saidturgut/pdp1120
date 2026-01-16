namespace pdp11_emulator.Core.Executing;
using Components;

public partial class DataPath
{
    public void AluAction(TriStateBus cpuBus, TriStateBus aluBus)
    {
        if(signals.AluAction is null)
            return;
        
        
    }
}