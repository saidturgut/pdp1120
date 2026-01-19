namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// WRITE BACK CYCLES
public partial class ControlUnitRom
{
    private static SignalSet WRITE_BACK() => new()
    {
        CpuBusDriver = Register.TMP,
        CpuBusLatcher = decoded.Drivers[0],
    };
    
    private static SignalSet WRITE_BACK_REG() => new()
    {
        CpuBusDriver = Register.TMP,
        CpuBusLatcher = decoded.Drivers[1],
    };

    private static SignalSet WRITE_BACK_RAM() => new()
    {
        UniBusDriving = UniBusDriving.WRITE_WORD,
    };
}