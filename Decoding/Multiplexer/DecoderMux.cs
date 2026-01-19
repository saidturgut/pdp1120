namespace pdp11_emulator.Decoding.Multiplexer;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux : DecoderRom
{
    private byte fzzz;
    private byte zfzz;
    private byte zzfz;
    private byte zzzf;
    
    protected void SetNibbles(ushort ir)
    {
        fzzz = (byte)(ir >> 12);
        zfzz = (byte)((ir & 0xF00) >> 8);
        zzfz = (byte)((ir & 0xF0) >> 4);
        zzzf = (byte)(ir & 0xF);
    }

    protected Decoded ZERO_OPERAND(MicroCycle microCycle)
    {
        Decoded decoded = new();
        decoded.MicroCycles.Add(microCycle);
        return decoded;
    }
    
    protected Decoded BRANCH(ushort opcode)
    {
        Decoded decoded = new();
        return decoded;
    }
    
    protected Decoded JSR(ushort opcode)
    {
        Decoded decoded = new();
        return decoded;
    }
    
    protected Decoded RTS(ushort opcode)
    {
        Decoded decoded = new();
        return decoded;
    }
}