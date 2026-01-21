namespace pdp11_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded BRANCH(ushort ir)
    {
        Decoded decoded = new()
        {
            Operation =  Operation.BRANCH,
            CycleLatch = (ushort)(ir & 0xFF), 
            // 0x80..0x87 or 0x1..0x7 
            Condition = (Condition)((ir >> 8) > 7 ? (ir >> 8) - 120 : ir >> 8),
        };
        
        decoded.MicroCycles.Add(MicroCycle.BRANCH_COMMIT);
        return decoded;
    }

    protected Decoded SOB(ushort ir)
    {
        Decoded decoded = new()
        {
            Registers = [(Register)((ir >> 6) & 0x7)],

            Operation = Operation.SUB,
            CycleLatch = (ushort)((ir & 0x3F) << 1),

            Condition = Condition.SOB, // != 0
        };
        
        decoded.MicroCycles.Add(MicroCycle.BRANCH_DEC);
        decoded.MicroCycles.Add(MicroCycle.BRANCH_COMMIT);
        return decoded;
    }
}