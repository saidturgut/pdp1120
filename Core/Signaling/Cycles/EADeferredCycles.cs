namespace pdp11_emulator.Core.Signaling.Cycles;

public partial class MicroUnitROM
{
    // ** 111111111111111111111111 ** //
    private static SignalSet EA_REG_DEFERRED() => new()
    {
        CPUBusDriver = decoded.driver,
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    };// TO EA_MEM_READ
    
    // ** 333333333333333333333333 ** //
    private static SignalSet EA_AUTOINC_DEFERRED_ADDR() => new()
    {
        CPUBusDriver = decoded.driver,
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    }; // TO EA_AUTOINC_DEFERRED_INC
    private static SignalSet EA_AUTOINC_DEFERRED_INC() => new()
    {
        CPUBusDriver = decoded.driver,
        ALUAction = new ALUAction(ALUOperation.ADD, 
            RegisterAction.NONE,2),
        CPUBusLatcher = decoded.driver,
    }; // TO EA_DEFERRED_READ
    
    // ** 555555555555555555555555 ** //
    private static SignalSet EA_AUTODEC_DEFERRED_DEC() => new()
    {
        CPUBusDriver = decoded.driver,
        ALUAction = new ALUAction(ALUOperation.SUB, 
            RegisterAction.NONE, 2),
        CPUBusLatcher = decoded.driver,
    }; // TO EA_AUTODEC_DEFERRED_ADDR
    private static SignalSet EA_AUTODEC_DEFERRED_ADDR() => new()
    {
        CPUBusDriver = decoded.driver,
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    }; // TO EA_DEFERRED_READ
    
    // ** 777777777777777777777777 ** //
    private static SignalSet EA_INDEXED_DEFERRED_ADDR() => new()
    {
        CPUBusDriver = decoded.driver,
        ALUAction = new ALUAction(ALUOperation.ADD, 
            RegisterAction.TMP, 0),
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    }; // TO EA_DEFERRED_READ
    
    // ** COMPLETION ** //
    private static SignalSet EA_DEFERRED_READ() => new()
    {
        UNIBUSLatch = UNIBUSAction.READ,
        CPUBusDriver = RegisterAction.MDR,
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    };// EA_MEM_READ

}