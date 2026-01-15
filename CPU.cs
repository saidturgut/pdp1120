namespace pdp11_emulator;
using Core.Executing.Components;
using Core.Executing;
using Core.Signaling;

public class CPU
{
    private readonly TriStateBus CPUBUS = new ();
    private readonly TriStateBus ALUBUS = new ();
    
    private readonly DataPath DataPath = new();
    private readonly MicroUnit MicroUnit = new();
    
    public void Init()
    {
        DataPath.Init();
    }
    
    public void Tick(TriStateBus uniBus)
    {
        // UNIBUS_LATCH
        // CPUBUS_DRIVE
        // ALU_COMPUTE
        // CPUBUS_LATCH
        // UNIBUS_DRIVE
    }
}