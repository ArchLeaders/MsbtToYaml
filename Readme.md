# MSBT to YAML

[![License](https://img.shields.io/badge/License-MIT-blue.svg?logo=github&logoColor=5751ff&labelColor=2A2C33&color=5751ff&style=for-the-badge)](https://github.com/ArchLeaders/MsbtToYaml/blob/master/License.md) [![Downloads](https://img.shields.io/github/downloads/ArchLeaders/MsbtToYaml/total?label=downloads&logo=github&logoColor=37c75e&labelColor=2A2C33&color=37c75e&style=for-the-badge)](https://github.com/ArchLeaders/MsbtToYaml/releases) [![Latest](https://img.shields.io/github/v/tag/ArchLeaders/MsbtToYaml?label=Release&logo=github&logoColor=324fff&color=324fff&labelColor=2A2C33&style=for-the-badge)](https://github.com/ArchLeaders/MsbtToYaml/releases/latest)

Simple CLI tool to convert MSBT files to/from YAML

- [MSBT to YAML](#msbt-to-yaml)
- [Usage](#usage)
  - [Single Input/Output](#single-inputoutput)
    - [Examples](#examples)
  - [Multiple Inputs/Outputs](#multiple-inputsoutputs)
    - [Example](#example)
- [Install](#install)
  - [Windows (x64)](#windows-x64)
  - [Windows (Arm64)](#windows-arm64)
  - [Linux (x64)](#linux-x64)
  - [Linux (arm64)](#linux-arm64)
  - [MacOS (x64)](#macos-x64)
  - [MacOS (Arm64)](#macos-arm64)
  - [Build From Source](#build-from-source)

# Usage

> [!NOTE]
> *Only `.yaml` and `.yml` extensions are recognized as YAML files. Everything else is assumed to be an MSBT file.*

## Single Input/Output

```powershell
MsbtToYaml <input> [-o|--output OUTPUT]
```

### Examples

```powershell
MsbtToYaml "D:\Mals\Attachment.msbt"
```

```powershell
MsbtToYaml "D:\Mals\Attachment.msbt" -o "D:\Output\Custom.yml"
```

## Multiple Inputs/Outputs

```powershell
MsbtToYaml <input> [-o|--output OUTPUT] <input> [-o|--output OUTPUT] ...
```

### Example


```powershell
MsbtToYaml "D:\Mals\Attachment.msbt" "D:\Mals\Npc.msbt"
```

```powershell
MsbtToYaml "D:\Mals\Attachment.msbt" -o "D:\Output\Custom.yml" "D:\Mals\Npc.msbt"
```

```powershell
MsbtToYaml "D:\Mals\Attachment.msbt" -o "D:\Output\Custom.yml" "D:\Mals\Npc.msbt" -o "D:\Output\Custom-Npc.yml" 
```

# Install

## Windows (x64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for Windows x64.
- Download the [latest win-x64 release](https://github.com/ArchLeaders/MsbtToYaml/releases/latest/download/MsbtToYaml-win-x64.zip)

## Windows (Arm64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for Windows Arm64.
- Download the [latest arm-x64 release](https://github.com/ArchLeaders/MsbtToYaml/releases/latest/download/MsbtToYaml-win-arm64.zip)

## Linux (x64)

- Install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) x64 runtime for your linux distribution.
- Download the [latest linux-x64 release](https://github.com/ArchLeaders/MsbtToYaml/releases/latest/download/MsbtToYaml-linux-x64.zip)

## Linux (arm64)

- Install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) Arm64 runtime for your linux distribution.
- Download the [latest linux-arm64 release](https://github.com/ArchLeaders/MsbtToYaml/releases/latest/download/MsbtToYaml-linux-arm64.zip)

## MacOS (x64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for MacOS x64.
- Download the [latest osx-x64 release](https://github.com/ArchLeaders/MsbtToYaml/releases/latest/download/MsbtToYaml-osx-x64.zip)

## MacOS (Arm64)

- Download and install the latest [.NET](https://dotnet.microsoft.com/en-us/download/dotnet/latest) runtime for MacOS Arm64.
- Download the [latest osx-arm64 release](https://github.com/ArchLeaders/MsbtToYaml/releases/latest/download/MsbtToYaml-osx-arm64.zip)

## Build From Source

```powershell
git clone "https://github.com/ArchLeaders/MsbtToYaml"
dotnet build MsbtToYaml
```