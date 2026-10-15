# Revit.Async

[![Revit 2021-2027](https://img.shields.io/badge/Revit-2021%20--%202027-green.svg)](https://autodesk.com/revit)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![VolocyNazad](https://img.shields.io/badge/VolocyNazad-blue.svg)](https://github.com/VolocyNazad)

> An implementation of asynchronous code execution in the Revit API context.

Revit.Async is a library for performing Revit API operations from modeless windows and other contexts via `ExternalEvent`, with `Task` support.

This repository is a maintained fork of Kennan Chan's MIT-licensed [Revit.Async](https://github.com/KennanChan/Revit.Async) project.

## Supported Revit versions

The package is built and tested for Revit 2021.1.100, 2022.1.80, 2023.1.90, 2024.3.60, 2025.4.60, 2026.4.10, and 2027.3.0. Revit 2021-2024 target net48; Revit 2025-2026 target net8.0-windows; Revit 2027 targets net10.0-windows.

## Requirements

- .NET SDK 10.0.103+ (see `global.json`)
- Revit API (packages `Nice3point.Revit.Api.RevitAPI` / `Nice3point.Revit.Api.RevitAPIUI`, floating `$(MajorRevitVersion).*`)

## License

MIT, see [LICENSE.md](LICENSE.md).

## Development documentation

- [Development policy](docs/policies/development.md)
- [Repository guide and technology stack](docs/repository.md)

## Contributing

Read [CONTRIBUTING.md](CONTRIBUTING.md) before submitting changes.
