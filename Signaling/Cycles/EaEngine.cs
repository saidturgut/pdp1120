namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// EFFECTIVE ADDRESS CYCLES
public partial class ControlUnitRom
{
    private static readonly Register[] EaLatchers 
        = [Register.TMP, Register.DST];
    
    private static SignalSet EA_REG_DATA_LATCH() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        CpuBusLatcher = EaLatchers[registersIndex],
    };// EXIT
    private static SignalSet EA_REG_ADDR_LATCH() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };

    private static SignalSet EA_INC() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        AluAction = new AluAction(AluOperation.ADD,
            Register.NONE, AluFlag.None),
        CpuBusLatcher = decoded.Drivers[registersIndex],
    };
    private static SignalSet EA_DEC() => new()
    {
        CpuBusDriver = decoded.Drivers[registersIndex],
        AluAction = new AluAction(AluOperation.SUB,
            Register.NONE, AluFlag.None),
        CpuBusLatcher = decoded.Drivers[registersIndex],
    };
    
    private static SignalSet EA_INDEX_MAR() => new()
    {
        CpuBusDriver = Register.R7,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    private static SignalSet EA_INDEX_MDR() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        AluAction = new AluAction(AluOperation.ADD, 
            decoded.Drivers[registersIndex], AluFlag.None),
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    
    private static SignalSet EA_UNI_ADDR_LATCH() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = UniBusDriving.READ_WORD,
    };
    
    private static SignalSet EA_UNI_DATA_LATCH() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = EaLatchers[registersIndex],
    };// EXIT

    private static SignalSet EA_TOGGLE() => new();
}
