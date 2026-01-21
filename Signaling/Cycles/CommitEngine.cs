namespace pdp11_emulator.Signaling.Cycles;
using Executing.Computing;

// COMMIT CYCLES
public partial class ControlUnitRom
{
    // GENERAL
    private static SignalSet COMMIT_FIRST() => new()
    {
        CpuBusDriver = Register.TMP,
        CpuBusLatcher = decoded.EncodedRegisters[0],
        CycleMode = decoded.CycleMode,
    };
    private static SignalSet COMMIT_SECOND() => new()
    {
        CpuBusDriver = Register.TMP,
        CpuBusLatcher = decoded.EncodedRegisters[1],
        CycleMode = decoded.CycleMode,
    };
    private static SignalSet COMMIT_RAM() => new()
        { UniBusDriving = GetWriteMode(), };
    private static SignalSet UNIBUS_LATCH() => new()
    {
        UniBusLatching = true,
        CpuBusDriver = Register.MDR,
        CpuBusLatcher = decoded.EncodedRegisters[1],
    };
    
    // CONTROL FLOW
    private static SignalSet COMMIT_TMP() => new()
    {
        CpuBusDriver = decoded.EncodedRegisters[0],
        CpuBusLatcher = Register.TMP,
    };
    private static SignalSet COMMIT_SP() => new()
    {
        CpuBusDriver = Register.SP,
        AluAction = new AluAction(decoded.Operation, Register.NONE, 2),
        CpuBusLatcher = Register.SP,
    };
    
    private static SignalSet UNIBUS_DRIVE_SP() => new()
    {
        CpuBusDriver = Register.SP,
        CpuBusLatcher = Register.MAR,
        UniBusDriving = decoded.UniBusMode,
    };
    
    private static SignalSet COMMIT_RN() => new()
    {
        CpuBusDriver = Register.PC,
        CpuBusLatcher = decoded.EncodedRegisters[0],
    };
    // EA ENGINE COMES HERE
    private static SignalSet COMMIT_PC() => new()
    {
        CpuBusDriver = Register.TMP,
        CpuBusLatcher = Register.PC,
    };

    // BRANCHING
    private static SignalSet COMMIT_DEC() => new()
    {
        CpuBusDriver = decoded.EncodedRegisters[0],
        AluAction = new AluAction(Operation.DEC, Register.NONE, 0),
        CpuBusLatcher = decoded.EncodedRegisters[0],
    };
    private static SignalSet COMMIT_BRANCH() => new()
    {
        CpuBusDriver = Register.PC,
        
        AluAction = new AluAction(decoded.Operation, Register.NONE, decoded.CycleLatch),
        Condition = decoded.Condition,
        
        CpuBusLatcher = Register.PC,
    };
}
