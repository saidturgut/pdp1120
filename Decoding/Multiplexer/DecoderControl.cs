namespace pdp11_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded JMP(ushort ir)
    {
        Decoded decoded = new()
        {
            Registers = [(Register)(ir & 0x7), Register.PC],
        };

        decoded.MicroCycles.AddRange(AddressEngine[(ir >> 3) & 0x7]);
        decoded.MicroCycles.Add(MicroCycle.INDEX_TOGGLE);
        decoded.MicroCycles.Add(MicroCycle.TMP_TO_REG);
        return decoded;
    }

    protected Decoded JSR(ushort ir)
    {
        Decoded decoded = new()
        {
            Registers = [(Register)((ir >> 6)  & 0x7), (Register)(ir & 0x7)],
            Operation = Operation.SUB,
            UniBusMode = UniBusDriving.WRITE_WORD,
        };
        
        decoded.MicroCycles.Add(MicroCycle.REG_TO_TEMP);
        decoded.MicroCycles.Add(MicroCycle.REG_ALU);
        decoded.MicroCycles.Add(MicroCycle.REG_TO_MAR);
        decoded.MicroCycles.Add(MicroCycle.PC_TO_REG);
        
        decoded.MicroCycles.Add(MicroCycle.INDEX_TOGGLE);
        
        decoded.MicroCycles.AddRange(AddressEngine[(ir >> 3) & 0x7]);
        
        decoded.MicroCycles.Add(MicroCycle.DST_TO_PC);

        return decoded;
    }

    protected Decoded RTS(ushort ir)
    {
        Decoded decoded = new()
        {
            Registers = [(Register)(ir & 0x7)],
            Operation = Operation.ADD,
            UniBusMode = UniBusDriving.READ_WORD,
        };
        
        decoded.MicroCycles.Add(MicroCycle.REG_TO_PC);
        decoded.MicroCycles.Add(MicroCycle.REG_TO_MAR);
        decoded.MicroCycles.Add(MicroCycle.MDR_TO_REG);
        decoded.MicroCycles.Add(MicroCycle.REG_ALU);
        return decoded;
    }
}