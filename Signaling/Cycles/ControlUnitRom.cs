namespace pdp11_emulator.Signaling.Cycles;
using Decoding;

public partial class ControlUnitRom
{    
    protected static Decoded decoded = new();
    protected static byte registersIndex;

    protected static readonly Func<SignalSet>[] MicroCycles =
    [
        EMPTY,
        FETCH_MAR, PC_INC, FETCH_MDR,
        DECODE, HALT,
    
        EA_REG_DATA_LATCH, EA_REG_ADDR_LATCH,
        EA_INC, EA_DEC,
        EA_INDEX_MAR, EA_INDEX_MDR,
        EA_UNI_ADDR_LATCH, EA_UNI_DATA_LATCH,
        EA_TOGGLE,
        
        EXE_WRITE_BACK, EXE_FLAGS,
        WRITE_BACK, WRITE_BACK_REG, WRITE_BACK_RAM,
    ];
}

public enum MicroCycle
{ 
    EMPTY,
    FETCH_MAR, PC_INC, FETCH_MDR,
    DECODE, HALT,
    
    EA_REG_DATA_LATCH, EA_REG_ADDR_LATCH,
    EA_INC, EA_DEC,
    EA_INDEX_MAR, EA_INDEX_MDR,
    EA_UNI_ADDR_LATCH, EA_UNI_DATA_LATCH,
    EA_TOGGLE,
        
    EXE_WRITE_BACK, EXE_FLAGS,
    WRITE_BACK, WRITE_BACK_REG, WRITE_BACK_RAM,
}
