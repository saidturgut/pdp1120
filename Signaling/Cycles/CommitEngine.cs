namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// COMMIT CYCLES
public partial class ControlUnitRom
{
    // GENERAL
    private static SignalSet TMP_TO_REG() => new()
    {
        CpuBusDriver = Register.TMP,
        CpuBusLatcher = decoded.Registers[registersIndex],
        CycleMode = decoded.CycleMode,
    };
    private static SignalSet TMP_TO_RAM() => new()
        { UniBusDriving = GetWriteMode(), };
    
    // BRANCHING
    private static SignalSet BRANCH_DEC() => new()
    {
        CpuBusDriver = decoded.Registers[0],
        AluAction = new AluAction(Operation.DEC, Register.NONE, 0),
        CpuBusLatcher = decoded.Registers[0],
    };
    private static SignalSet BRANCH_COMMIT() => new()
    {
        CpuBusDriver = Register.PC,
        
        AluAction = new AluAction(decoded.Operation, Register.NONE, decoded.CycleLatch),
        Condition = decoded.Condition,
        
        CpuBusLatcher = Register.PC,
    };
}
