# Revit.Async

[![Revit 2021.1.9, 2023, 2025](https://img.shields.io/badge/Revit-2021.1.9%20%7C%202023%20%7C%202025-green.svg)](https://autodesk.com/revit)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![VolocyNazad](https://img.shields.io/badge/VolocyNazad-blue.svg)](https://github.com/VolocyNazad)

> An implementation of asynchronous code execution in the Revit API context.

Revit.Async is a library for performing Revit API operations from modeless windows and other contexts via `ExternalEvent`, with `Task` support.

This repository is a maintained fork of Kennan Chan's MIT-licensed [Revit.Async](https://github.com/KennanChan/Revit.Async) project.

## Supported Revit versions

The package is built and tested for Revit 2021.1.9, 2023.0.0, and 2025.0.0. Revit 2021 and 2023 target net48; Revit 2025 targets net8.0-windows.

## Requirements

- .NET SDK 10.0.103+ (see `global.json`)
- Revit API (package `Revit_All_Main_Versions_API_x64`)

## License

MIT, see [LICENSE.md](LICENSE.md).

## Development documentation

- [Development policy](docs/policies/development.md)
- [Repository guide and technology stack](docs/repository.md)

## Contributing

Read [CONTRIBUTING.md](CONTRIBUTING.md) before submitting changes.
