namespace pdp11_emulator.Signaling.Cycles;
using Decoding;

public partial class ControlUnitRom
{    
    protected static Decoded decoded = new();
    protected static byte registersIndex;

    private static byte GetStepSize()
        => (byte)(decoded.CycleMode != CycleMode.BYTE_MODE || 
                  (decoded.Registers[registersIndex] is Register.PC or Register.SP) ? 2 : 1);

    private static UniBusDriving GetReadMode()
        => decoded.CycleMode != CycleMode.BYTE_MODE
            ? UniBusDriving.READ_WORD : UniBusDriving.READ_BYTE;
    
    private static UniBusDriving GetWriteMode()
        => decoded.CycleMode != CycleMode.BYTE_MODE
            ? UniBusDriving.WRITE_WORD : UniBusDriving.WRITE_BYTE;

    protected static readonly Func<SignalSet>[] MicroCycles =
    [
        EMPTY,
        //FETCH ENGINE
        FETCH_READ, PC_INC, FETCH_LATCH,
        DECODE, HALT,
    
        // ADDRESS ENGINE
        REG_TO_TEMP, 
        REG_TO_MAR_MOD, REG_TO_MAR_WORD,
        PC_TO_MAR, 
        REG_INC, REG_DEC,
        MDR_TO_TEMP, MDR_TO_MAR, 
        MDR_INDEX_MAR_MOD, MDR_INDEX_MAR_WORD,
        INDEX_TOGGLE,
    
        // CONTROL ENGINE
        REG_ALU, REG_TO_MAR,
        PC_TO_REG, REG_TO_PC, DST_TO_PC,
        MDR_TO_REG,
        
        // EXECUTE AND COMMIT ENGINE
        EXECUTE_EA, EXECUTE_FLAGS, EXECUTE_PSW,
        BRANCH_DEC, BRANCH_COMMIT,
        TMP_TO_REG, TMP_TO_RAM,
    ];
}

public enum MicroCycle
{ 
    EMPTY,
    //FETCH ENGINE
    FETCH_READ, PC_INC, FETCH_LATCH,
    DECODE, HALT,
    
    // ADDRESS ENGINE
    REG_TO_TEMP, 
    REG_TO_MAR_MOD, REG_TO_MAR_WORD,
    PC_TO_MAR, 
    REG_INC, REG_DEC,
    MDR_TO_TEMP, MDR_TO_MAR, 
    MDR_INDEX_MAR_MOD, MDR_INDEX_MAR_WORD,
    INDEX_TOGGLE,
    
    // CONTROL ENGINE
    REG_ALU, REG_TO_MAR,
    PC_TO_REG, REG_TO_PC, DST_TO_PC,
    MDR_TO_REG,
        
    // EXECUTE AND COMMIT ENGINE
    EXECUTE_EA, EXECUTE_FLAGS, EXECUTE_PSW,
    BRANCH_DEC, BRANCH_COMMIT,
    TMP_TO_REG, TMP_TO_RAM,
}
