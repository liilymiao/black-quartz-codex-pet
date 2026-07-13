# Installation / 安装

## macOS / Linux

1. Create the folder `~/.codex/pets/black-quartz/` if it does not exist.
2. Copy `pet/pet.json` and `pet/spritesheet.webp` into that folder.
3. Restart Codex.
4. Select **Black Quartz** from the pet selector.

```bash
mkdir -p ~/.codex/pets/black-quartz
cp pet/pet.json pet/spritesheet.webp ~/.codex/pets/black-quartz/
```

## Windows

Copy both files into the equivalent Codex home directory under:

```text
%USERPROFILE%\.codex\pets\black-quartz\
```

Restart Codex after copying.

## Verification / 校验

Expected SHA-256 for `spritesheet.webp`:

```text
462ae6d3436fc0bfd4dedd472ec4267a867ed7e349bf4bd0545530dd61f558b2
```

The `pet.json` file must contain `"spriteVersionNumber": 2`.

