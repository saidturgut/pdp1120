namespace pdp11_emulator.Core.Signaling.Cycles;

// EFFECTIVE ADDRESS MICRO CYCLES
public partial class MicroUnitROM
{
    // ** 000000000000000000000000 ** //
    private static SignalSet EA_REG() => new()
    {
        CPUBusDriver = decoded.driver,
        CPUBusLatcher = RegisterAction.TMP,
    };// TO EXECUTE
    
    // ** 222222222222222222222222 ** //
    private static SignalSet EA_AUTOINC_ADDR() => new()
    {
        CPUBusDriver = decoded.driver,
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    };// TO EA_AUTOINC_INC
    private static SignalSet EA_AUTOINC_INC() => new()
    {
        CPUBusDriver = decoded.driver,
        ALUAction = new ALUAction(ALUOperation.ADD, 
            RegisterAction.NONE,2),
        CPUBusLatcher = decoded.driver,
    }; // TO EA_MEM_READ
    
    // ** 444444444444444444444444 ** //
    private static SignalSet EA_AUTODEC_DEC() => new()
    {
        CPUBusDriver = decoded.driver,
        ALUAction = new ALUAction(ALUOperation.SUB, 
            RegisterAction.NONE,2),
        CPUBusLatcher = decoded.driver,
    }; // TO EA_AUTODEC_ADDR
    private static SignalSet EA_AUTODEC_ADDR() => new()
    {
        CPUBusDriver = decoded.driver,
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    }; // TO EA_MEM_READ
    
    // ** 666666666666666666666666 ** //
    private static SignalSet EA_INDEXED_DISP_FETCH() => new()
    {
        CPUBusDriver = RegisterAction.R7,
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    }; // TO EA_INDEXED_DISP_LATCH
    private static SignalSet EA_INDEXED_DISP_LATCH() => new()
    {
        UNIBUSLatch = UNIBUSAction.READ,
        CPUBusDriver = RegisterAction.MAR,
        CPUBusLatcher = RegisterAction.TMP,
    }; // TO EA_INDEXED_ADDR
    private static SignalSet EA_INDEXED_ADDR() => new()
    {
        CPUBusDriver = decoded.driver,
        ALUAction = new ALUAction(ALUOperation.ADD, 
            RegisterAction.TMP, 0),
        CPUBusLatcher = RegisterAction.MAR,
        UNIBUSDrive = UNIBUSAction.READ,
    }; // TO EA_MEM_READ

    // ** COMPLETION ** //
    private static SignalSet EA_MEM_READ() => new()
    {
        UNIBUSLatch = UNIBUSAction.READ,
        CPUBusDriver = RegisterAction.MDR,
        CPUBusLatcher = RegisterAction.TMP,
    };// EXECUTE

}