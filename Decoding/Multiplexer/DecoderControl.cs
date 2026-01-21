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
            EncodedRegisters = [(Register)(ir & 0x7), Register.PC],
        };

        decoded.MicroCycles.AddRange(AddressEngine[(ir >> 3) & 0x7]);
        decoded.MicroCycles.Add(MicroCycle.COMMIT_SECOND);
        return decoded;
    }

    protected Decoded JSR(ushort ir)
    {
        Decoded decoded = new()
        {
            EncodedRegisters = [(Register)((ir >> 6)  & 0x7), (Register)(ir & 0x7)],
            Operation = Operation.SUB,
            UniBusMode = UniBusDriving.WRITE_WORD,
        };
        
        // PUSH RN
        decoded.MicroCycles.Add(MicroCycle.COMMIT_TMP);
        decoded.MicroCycles.Add(MicroCycle.COMMIT_SP);
        decoded.MicroCycles.Add(MicroCycle.UNIBUS_DRIVE_SP);
        decoded.MicroCycles.Add(MicroCycle.COMMIT_RN);
        
        decoded.MicroCycles.Add(MicroCycle.EA_TOGGLE);
        
        decoded.MicroCycles.AddRange(AddressEngine[(ir >> 3) & 0x7]);
        decoded.MicroCycles.Add(MicroCycle.COMMIT_PC);

        return decoded;
    }

    protected Decoded RTS(ushort ir)
    {
        Decoded decoded = new()
        {
            EncodedRegisters = [(Register)(ir & 0x7), Register.PC],
            Operation = Operation.ADD,
            UniBusMode = UniBusDriving.READ_WORD,
        };
        
        decoded.MicroCycles.Add(MicroCycle.COMMIT_TMP);
        decoded.MicroCycles.Add(MicroCycle.COMMIT_PC);
        
        // POP RN
        decoded.MicroCycles.Add(MicroCycle.UNIBUS_DRIVE_SP);
        decoded.MicroCycles.Add(MicroCycle.UNIBUS_LATCH);
        
        return decoded;
    }
}