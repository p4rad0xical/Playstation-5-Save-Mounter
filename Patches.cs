﻿using System;
using System.Collections.Generic;

namespace PS4Saves;
public class Patch
{
    public ulong Offset { get; }
    public ulong FunctionOffset { get; }
    public byte[] Bytes { get; }
    public byte[] OriginalBytes { get; set; }

    // Constructor for byte array patches
    public Patch(ulong offset, byte[] bytes)
    {
        Offset = offset;
        Bytes = bytes;
        OriginalBytes = null;
        FunctionOffset = 0;
    }

    // Constructor for single byte patches
    public Patch(ulong offset, int functionOffset)
    {
        Offset = offset;
        Bytes = null;
        OriginalBytes = null;
        FunctionOffset = (ulong)functionOffset;
    }
}

public static class Patches
{
    private static readonly Dictionary<string, List<Patch>[]> FirmwarePatches = new Dictionary<string, List<Patch>[]>
    {
        {
            "3.20", new List<Patch>[]
            {
                // Shellcore patches
                [
                    new(0x1493043, [0x00]), // 'sce_sdmemory' patch
                    new(0x148A099, [0x00]), // 'sce_sdmemory1' patch
                    new(0x148A0A7, [0x00]), // 'sce_sdmemory2' patch
                    new(0x15246EF, [0x00]), // 'sce_sdmemory3' patch
                    new(0xB5FAF0, [0x48, 0x31, 0xC0, 0xC3]), // verify keystone patch
                    new(0x0FCFB0, [0x31, 0xC0, 0xC3]), // transfer mount permission patch eg mount foreign saves with write permission
                    new(0x181600, [0x31, 0xC0, 0xC3]), // patch psn check to load saves saves foreign to current account
                    new(0x10061C, [0xCD]), // ^ (thanks to GRModSave_Username) different patch
                    new(0x0FF359, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // something something patches...
                    new(0x0FD33C, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // don't even remember doing this
                    new(0x0FCA14, [0x90, 0x90]), // nevah jump
                    new(0x0FCCC3, [0x90, 0xE9]) // always jump
                ],
                // Libc patches for save directory
                [
                    new(0x12, 0x78D60), // opendir
                    new(0x20, 0x78C10), // readdir
                    new(0x2E, 0x78920), // closedir
                    new(0x3C, 0x7A9E0), // strcpy
                ],
                // Libc patches for images
                [
                    new(0x16, 0x60D00), // fopen
                    new(0x38, 0x60F90), // fread
                    new(0x5B, 0x00470)  // fclose
                ]
            }
        },
        {
            "4.03", new List<Patch>[]
            {
                // Shellcore patches
                [
                    new(0x172DFBA, [0x00]), // 'sce_sdmemory' patch
                    new(0x17255B6, [0x00]), // 'sce_sdmemory1' patch
                    new(0x17255C4, [0x00]), // 'sce_sdmemory2' patch
                    new(0x17BE5A4, [0x00]), // 'sce_sdmemory3' patch
                    new(0xB04C50, [0x48, 0x31, 0xC0, 0xC3]), // verify keystone patch
                    new(0x102C80, [0x31, 0xC0, 0xC3]), // transfer mount permission patch eg mount foreign saves with write permission
                    new(0x18BF30, [0x31, 0xC0, 0xC3]), // patch psn check to load saves saves foreign to current account
                    new(0x10625D, [0xCD]), // ^ (thanks to GRModSave_Username) different patch
                    new(0x104FA9, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // something something patches...
                    new(0x10300C, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // don't even remember doing this
                    new(0x1026E4, [0x90, 0x90]), // nevah jump
                    new(0x102993, [0x90, 0xE9]) // always jump
                ],
                // Libc patches
                [
                    new(0x12, 0x7A270), // opendir
                    new(0x20, 0x7A120), // readdir
                    new(0x2E, 0x79E30), // closedir
                    new(0x3C, 0x7BEC0)  // strcpy
                ]
            }
        },
        {
            "5.02", new List<Patch>[]
            {
                // Shellcore patches
                [
                    new(0x1836258, [0x00]), // 'sce_sdmemory' patch
                    new(0x182D32F, [0x00]), // 'sce_sdmemory1' patch
                    new(0x182D33D, [0x00]), // 'sce_sdmemory2' patch
                    new(0x18CE44C, [0x00]), // 'sce_sdmemory3' patch
                    new(0xBCE640, [0x48, 0x31, 0xC0, 0xC3]), // verify keystone patch
                    new(0x113790, [0x31, 0xC0, 0xC3]), // transfer mount permission patch eg mount foreign saves with write permission
                    new(0x1AAE40, [0x31, 0xC0, 0xC3]), // patch psn check to load saves saves foreign to current account
                    new(0x117624, [0xE9, 0x0C, 0x00, 0x00, 0x00]), // ^ (thanks to GRModSave_Username) different patch
                    new(0x115CE8, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // something something patches...
                    new(0x113B1C, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // don't even remember doing this
                    new(0x1131F4, [0x90, 0x90]), // nevah jump
                    new(0x1134A3, [0x90, 0xE9]) // always jump
                ],
                // Libc patches
                [
                    new(0x12, 0x76FD0), // opendir
                    new(0x20, 0x76E80), // readdir
                    new(0x2E, 0x76B90), // closedir
                    new(0x3C, 0x78C90)  // strcpy
                ]
            }
        },
        {
            "6.02", new List<Patch>[]
            {
                // Shellcore patches
                [
                    new(0x18F2B0F, [0x00]), // 'sce_sdmemory' patch
                    new(0x18E94BD, [0x00]), // 'sce_sdmemory1' patch
                    new(0x18E94CB, [0x00]), // 'sce_sdmemory2' patch
                    new(0x198F654, [0x00]), // 'sce_sdmemory3' patch
                    new(0xC24780, [0x48, 0x31, 0xC0, 0xC3]), // verify keystone patch
                    new(0x11FDD0, [0x31, 0xC0, 0xC3]), // transfer mount permission patch eg mount foreign saves with write permission
                    new(0x1BFB30, [0x31, 0xC0, 0xC3]), // patch psn check to load saves saves foreign to current account
                    new(0x123B57, [0xE9, 0x0C, 0x00, 0x00, 0x00]), // ^ (thanks to GRModSave_Username) different patch
                    new(0x122228, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // something something patches...
                    new(0x12014E, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // don't even remember doing this
                    new(0x11F824, [0x90, 0x90]), // nevah jump
                    new(0x11FAD2, [0x90, 0xE9]) // always jump
                ],
                // Libc patches
                [
                    new(0x12, 0x71530), // opendir
                    new(0x20, 0x713E0), // readdir
                    new(0x2E, 0x710F0), // closedir
                    new(0x3C, 0x731D0)  // strcpy
                ]
            }
        },
        {
            "7.40", new List<Patch>[]
            {
                // Shellcore patches
                [
                    new(0x1AAD12E, [0x00]), // 'sce_sdmemory' patch
                    new(0x1B0A257, [0x00]), // 'sce_sdmemory1' patch
                    new(0x1AC19ED, [0x00]), // 'sce_sdmemory2' patch
                    new(0x1A66751, [0x00]), // 'sce_sdmemory3' patch
                    new(0xD014D0, [0x48, 0x31, 0xC0, 0xC3]), // verify keystone patch
                    new(0x1204E0, [0x31, 0xC0, 0xC3]), // transfer mount permission patch eg mount foreign saves with write permission
                    new(0x1C8520, [0x31, 0xC0, 0xC3]), // patch psn check to load saves saves foreign to current account
                    new(0x124267, [0xE9, 0x0C, 0x00, 0x00, 0x00]), // ^ (thanks to GRModSave_Username) different patch
                    new(0x122938, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // something something patches...
                    new(0x12085E, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // don't even remember doing this
                    new(0x11FF34, [0x90, 0x90]), // nevah jump
                    new(0x1201E2, [0x90, 0xE9]) // always jump
                ],
                // Libc patches
                [
                    new(0x12, 0x72BF0), // opendir
                    new(0x20, 0x72AA0), // readdir
                    new(0x2E, 0x727D0), // closedir
                    new(0x3C, 0x74830)  // strcpy
                ]
            }
        },
        {
            "8.20", new List<Patch>[]
            {
                // Shellcore patches
                [
                    new(0x1B21F98, [0x00]), // 'sce_sdmemory' patch
                    new(0x1B8298F, [0x00]), // 'sce_sdmemory1' patch
                    new(0x1B37B2D, [0x00]), // 'sce_sdmemory2' patch
                    new(0x1AD8335, [0x00]), // 'sce_sdmemory3' patch
                    new(0xD66930, [0x48, 0x31, 0xC0, 0xC3]), // verify keystone patch
                    new(0x11F1B0, [0x31, 0xC0, 0xC3]), // transfer mount permission patch eg mount foreign saves with write permission
                    new(0x1D2660, [0x31, 0xC0, 0xC3]), // patch psn check to load saves saves foreign to current account
                    new(0x122EAA, [0xE9, 0x05, 0x00, 0x00]), // ^ (thanks to GRModSave_Username) different patch
                    new(0x121598, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // something something patches...
                    new(0x122281, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // don't even remember doing this
                    new(0x11EC24, [0x90, 0x90]), // nevah jump
                    new(0x11EEC7, [0x90, 0xE9]) // always jump
                ],
                // Libc patches
                [
                    new(0x12, 0x760A0), // opendir
                    new(0x20, 0x75F50), // readdir
                    new(0x2E, 0x75C80), // closedir
                    new(0x3C, 0x77D20)  // strcpy
                ]
            }
        },
        {
            "9.60", new List<Patch>[]
            {
                // Shellcore patches
                [
                    new(0x1C18656, [0x00]), // 'sce_sdmemory' patch
                    new(0x1C873BA, [0x00]), // 'sce_sdmemory1' patch
                    new(0x1C02BB0, [0x00]), // 'sce_sdmemory2' patch
                    new(0x1BEC89F, [0x00]), // 'sce_sdmemory3' patch
                    new(0xDD9860, [0x48, 0x31, 0xC0, 0xC3]), // verify keystone patch
                    new(0x1265F0, [0x31, 0xC0, 0xC3]), // transfer mount permission patch eg mount foreign saves with write permission
                    new(0x1E4720, [0x31, 0xC0, 0xC3]), // patch psn check to load saves saves foreign to current account
                    new(0x12A2EA, [0xE9, 0x05, 0x00, 0x00]), // ^ (thanks to GRModSave_Username) different patch
                    new(0x1289D8, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // something something patches...
                    new(0x1296C5, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // don't even remember doing this
                    new(0x126064, [0x90, 0x90]), // nevah jump
                    new(0x126307, [0x90, 0xE9]) // always jump
                ],
                // Libc patches
                [
                    new(0x12, 0x74100), // opendir
                    new(0x20, 0x73FB0), // readdir
                    new(0x2E, 0x73CE0), // closedir
                    new(0x3C, 0x75D80)  // strcpy
                ]
            }
        },
        {
            "10.01", new List<Patch>[]
            {
                // Shellcore patches
                [
                    new(0x1C1B5DA, [0x00]), // 'sce_sdmemory' patch
                    new(0x1C8DC60, [0x00]), // 'sce_sdmemory1' patch
                    new(0x1C05075, [0x00]), // 'sce_sdmemory2' patch
                    new(0x1BEE286, [0x00]), // 'sce_sdmemory3' patch
                    new(0xDCF260, [0x48, 0x31, 0xC0, 0xC3]), // verify keystone patch
                    new(0x1242D0, [0x31, 0xC0, 0xC3]), // transfer mount permission patch eg mount foreign saves with write permission
                    new(0x1E1C50, [0x31, 0xC0, 0xC3]), // patch psn check to load saves saves foreign to current account
                    new(0x127FAE, [0xE9, 0x05, 0x00, 0x00]), // ^ (thanks to GRModSave_Username) different patch
                    new(0x1266F8, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // something something patches...
                    new(0x12736B, [0x90, 0x90, 0x90, 0x90, 0x90, 0x90]), // don't even remember doing this
                    new(0x123D74, [0x90, 0x90]), // nevah jump
                    new(0x123FF2, [0x90, 0xE9]) // always jump
                ],
                // Libc patches
                [
                    new(0x12, 0x73AC0), // opendir
                    new(0x20, 0x73960), // readdir
                    new(0x2E, 0x73690), // closedir
                    new(0x3C, 0x757F0)  // strcpy
                ]
            }
        },
    };

    /// <summary>
    /// Retrieves the shellcore patches for a given firmware version.
    /// </summary>
    /// <param name="firmwareVersion">The firmware version string (e.g., "7.40").</param>
    /// <returns>A list of Patch objects for shellcore, or an empty list if no patches are defined for the version.</returns>
    public static List<Patch> GetShellcorePatches(string firmwareVersion)
    {
        if (FirmwarePatches.TryGetValue(firmwareVersion, out var patches))
        {
            return patches[0]; // Shellcore patches are at index 0
        }
        return [];
    }

    /// <summary>
    /// Retrieves the libc function patches for a given firmware version.
    /// </summary>
    /// <param name="firmwareVersion">The firmware version string (e.g., "7.40").</param>
    /// <returns>A list of Patch objects for libc functions, or an empty list if no patches are defined for the version.</returns>
    public static List<Patch> GetLibcPatches(string firmwareVersion, bool imagePatches = false)
    {
        if (imagePatches)
        {
            // TODO: refactor this
            if (FirmwarePatches.TryGetValue(firmwareVersion, out var imgPatches)) {
                return imgPatches[2];
            }
            return [];
        }
        if (FirmwarePatches.TryGetValue(firmwareVersion, out var patches))
        {
            return patches[1]; // Libc function patches are at index 1
        }
        return [];
    }
}
