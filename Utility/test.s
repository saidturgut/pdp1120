        .text
        .org    000000

START:
    jsr r5, A
    halt

A:
    jsr r5, B
    rts r5

B:
    rts r5