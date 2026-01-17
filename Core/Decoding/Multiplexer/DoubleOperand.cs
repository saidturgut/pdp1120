namespace pdp11_emulator.Core.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded DOUBLE_OPERAND(ushort opcode)
    {
        byte operation = (byte)((opcode >> 12) & 0xF);
        bool byteMode =  ((opcode >> 11) & 1) != 0;
        
        DoubleOperandType type = (DoubleOperandType)operation;
        
        Decoded decoded = new()
        {
            Drivers = [(RegisterAction)((opcode >> 6)  & 0x7), 
                (RegisterAction)(opcode & 0x7)],
            AluOperation = DoubleOperandTable[(ushort)type],
            FlagMask = FlagMasks[type == DoubleOperandType.MOV ? 1 : 0], 
            StepSize = 2,
        };
        
        Console.WriteLine(DoubleOperandTable[(ushort)type]);
        
        Console.WriteLine(decoded.Drivers[0]);
        Console.WriteLine(decoded.Drivers[1]);
        
        // EFFECTIVE ADDRESS ENGINE
        if (type is not (DoubleOperandType.ADD or DoubleOperandType.SUB))
            decoded.StepSize = (byte)(byteMode ? 1 : 2);
        
        decoded.MicroCycles.AddRange(EaEngine[(opcode >> 9) & 0x7]);        
        decoded.MicroCycles.AddRange(EaEngine[(opcode >> 3)  & 0x7]);

        // EXECUTE ENGINE
        if (type is not DoubleOperandType.MOV)
            decoded.MicroCycles.Add(MicroCycle.ALU_EXECUTE);
        else
            
        
        // WRITE BACK ENGINE
        if (type is not (DoubleOperandType.CMP or DoubleOperandType.BIT))
        {
            decoded.MicroCycles.Add(((opcode >> 3) & 0x38) == 0 ? 
                MicroCycle.WRITE_BACK_REG : MicroCycle.WRITE_BACK_RAM);
        }
        
        return decoded;
    }

    private enum DoubleOperandType
    {
        MOV = 0x1, CMP = 0x2, BIT = 0x3, BIC = 0x4, BIS = 0x5, 
        ADD = 0x6, SUB = 0xE
    }

    public AluOperation[] DoubleOperandTable =
    [
        AluOperation.NONE,
        AluOperation.NONE, AluOperation.SUB, 
        AluOperation.AND, AluOperation.AND, 
        AluOperation.OR, AluOperation.ADD,
        AluOperation.NONE, AluOperation.NONE, 
        AluOperation.NONE, AluOperation.NONE, 
        AluOperation.NONE, AluOperation.NONE, 
        AluOperation.NONE, AluOperation.SUB,
    ];
}