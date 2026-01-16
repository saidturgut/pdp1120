namespace pdp11_emulator.Core.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux
{
    protected Decoded SINGLE_OPERAND(ushort opcode)
    {
        Decoded decoded = new()
        {
            Drivers = [(RegisterAction)(opcode & 0x7)],
        };
        decoded.MicroCycles.AddRange(EaEngine[(opcode >> 3) & 0x7]);
        
        return decoded;
    }
}