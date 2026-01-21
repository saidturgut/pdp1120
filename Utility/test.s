        .text
        .org    00000

        mov     #1, r0
        br loop
        
start: halt

loop:
        sob     r0, start
        halt