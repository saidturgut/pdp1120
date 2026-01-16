namespace pdp11_emulator.Core.Decoding.Multiplexer;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux : DecoderRom
{
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