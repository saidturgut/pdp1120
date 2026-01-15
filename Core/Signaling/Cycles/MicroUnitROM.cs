namespace pdp11_emulator.Core.Signaling.Cycles;
using Decoding;

public partial class MicroUnitROM
{
    protected static Decoded decoded = new();

    protected static readonly Dictionary<MicroCycle, Func<SignalSet>> MicroCycles = new()
    {
        { MicroCycle.EMPTY, EMPTY },

        // FETCH
        { MicroCycle.FETCH_MAR, FETCH_MAR },
        { MicroCycle.FETCH_INC, FETCH_INC },
        { MicroCycle.FETCH_MDR, FETCH_MDR },
        
        { MicroCycle.DECODE, DECODE },

        // EFFECTIVE ADDRESS REGULAR
        { MicroCycle.EA_REG, EA_REG },
        
        { MicroCycle.EA_AUTOINC_ADDR, EA_AUTOINC_ADDR },
        { MicroCycle.EA_AUTOINC_INC, EA_AUTOINC_INC },
        
        { MicroCycle.EA_AUTODEC_DEC, EA_AUTODEC_DEC },
        { MicroCycle.EA_AUTODEC_ADDR, EA_AUTODEC_ADDR },
        
        { MicroCycle.EA_INDEXED_DISP_FETCH, EA_INDEXED_DISP_FETCH },
        { MicroCycle.EA_INDEXED_DISP_LATCH, EA_INDEXED_DISP_LATCH },
        { MicroCycle.EA_INDEXED_ADDR, EA_INDEXED_ADDR },
        
        { MicroCycle.EA_MEM_READ, EA_MEM_READ },

        // EFFECTIVE ADDRESS DEFERRED
        { MicroCycle.EA_REG_DEFERRED, EA_REG_DEFERRED },
        
        { MicroCycle.EA_AUTOINC_DEFERRED_ADDR, EA_AUTOINC_DEFERRED_ADDR },
        { MicroCycle.EA_AUTOINC_DEFERRED_INC, EA_AUTOINC_DEFERRED_INC },
        
        { MicroCycle.EA_AUTODEC_DEFERRED_DEC, EA_AUTODEC_DEFERRED_DEC },
        { MicroCycle.EA_AUTODEC_DEFERRED_ADDR, EA_AUTODEC_DEFERRED_ADDR },
        
        { MicroCycle.EA_INDEXED_DEFERRED_ADDR, EA_INDEXED_DEFERRED_ADDR },
        
        { MicroCycle.EA_DEFERRED_READ, EA_DEFERRED_READ },
    };
}

public enum MicroCycle
{
    EMPTY,
    FETCH_MAR, FETCH_INC, FETCH_MDR,
    DECODE,
    
    // EA REGULAR
    /*1*/ EA_REG,
    /*2*/ EA_AUTOINC_ADDR, EA_AUTOINC_INC,
    /*4*/ EA_AUTODEC_DEC, EA_AUTODEC_ADDR,
    /*6*/ EA_INDEXED_DISP_FETCH, EA_INDEXED_DISP_LATCH, EA_INDEXED_ADDR,
    /*C*/ EA_MEM_READ,
    
    // EA DEFFERRED
    /*1*/ EA_REG_DEFERRED,
    /*3*/ EA_AUTOINC_DEFERRED_ADDR, EA_AUTOINC_DEFERRED_INC,
    /*5*/ EA_AUTODEC_DEFERRED_DEC, EA_AUTODEC_DEFERRED_ADDR,
    /*7*/ EA_INDEXED_DEFERRED_ADDR,
    /*C*/ EA_DEFERRED_READ,
}


public struct SignalSet()
{
    public UNIBUSAction UNIBUSLatch = UNIBUSAction.NONE; // MDR
    public RegisterAction CPUBusDriver = RegisterAction.NONE;
    public ALUAction? ALUAction = null;
    public RegisterAction CPUBusLatcher = RegisterAction.NONE;
    public UNIBUSAction UNIBUSDrive = UNIBUSAction.NONE; // MAR
    
    public MicroCycle 
}

public struct ALUAction(ALUOperation operation, 
    RegisterAction registerOperand , ushort constOperand)
{
    public ALUOperation ALUOperation = operation;
    public RegisterAction RegisterOperand = registerOperand;
    public ushort ConstOperand = constOperand;
}

public enum UNIBUSAction
{
    NONE, READ, WRITE,
}

public enum ALUOperation
{
    NONE, ADD, SUB
}

public enum RegisterAction
{
    NONE,
    R0, R1, R2, R3, R4, R5, R6, R7,
    IR, MDR, MAR, TMP,
}

// ** FETCH_MAR
// PC -> CPUBUS
// MAR <- CPUBUS
// MAR -> UNIBUS (READ)
    
// ** FETCH_INC
// PC -> CPUBUS
// ALU (ADD, $2) -> ALUBUS
// PC <- ALUBUS
    
// ** FETCH_MDR
// MDR <- UNIBUS (READ)
// MDR -> CPUBUS
// IR <- CPUBUS
    
// ** DECODE
// DECODE
