namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// EXECUTE CYCLES
public partial class ControlUnitRom
{
    private static SignalSet HALT() => new()
    {
    };
    
    private static SignalSet EXE_WRITE_BACK() => new()
    {
        CpuBusDriver = Register.TMP,
        AluAction = new AluAction(decoded.AluOperation, 
            Register.DST, decoded.FlagMask),
        CpuBusLatcher = Register.TMP,
    };

    private static SignalSet EXE_FLAGS() => new()
    {
        CpuBusDriver = Register.TMP,
        AluAction = new AluAction(decoded.AluOperation,
            Register.DST, decoded.FlagMask),
    };
}