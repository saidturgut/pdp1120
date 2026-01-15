namespace pdp11_emulator.Core.Decoding;

public class Decoder : DecoderMUX
{
    public Decoded Decode(ushort IR)
    {
        if ((IR & 0xF000) >= 0x1000 && (IR & 0xF000) <= 0x6000)
            return DOUBLE_OPERAND(IR);
        if ((IR & 0xFFC0) >= 0x0A00 && (IR & 0xFFC0) <= 0x0DC0)
            return SINGLE_OPERAND(IR);
        if ((IR & 0xFF00) >= 0x0100 && (IR & 0xFF00) <= 0x1F00)
            return BRANCH(IR);
        if ((IR & 0xFFF8) == 0x0800)
            return JSR(IR);
        if ((IR & 0xFFF8) == 0x0080)
            return RTS(IR);

        throw new Exception("ILLEGAL OPCODE!!");
    }
}