# Repository guide

Paths in this document are relative to the repository root.
Read and follow the [development policy](policies/development.md) alongside this guide.

## About

`VolocyNazad.Revit.Async` is a maintained fork of Kennan Chan's MIT-licensed [Revit.Async](https://github.com/KennanChan/Revit.Async). It provides async/await helpers for marshalling
work onto and off of Revit's API context/thread (the Revit API is not
generally thread-safe and most calls must happen on the main Revit
thread). Widely depended upon across the ecosystem, e.g.
`impact.toolkit.revit`'s `VolocyNazad.Revit.Async` reference and
`revit.linter`'s.

## Repository structure

```
.
├── src/
│   └── Revit.Async/            the library
└── tests/
    └── Revit.Async.Tests/
```

## Tech stack

- .NET, built against `Revit_All_Main_Versions_API_x64` (Nice3point's
  multi-version Revit API reference package)
- MinVer (git-tag-based versioning)
- Tests use xUnit.
- Package versions are declared directly in project files; central package management is not used

## Documentation layout

- `AGENTS.md` links to the required repository guidance.
- `docs/policies/development.md` contains the development policy.
- `docs/repository.md` describes the project, repository structure, and technology stack.

The root solution exposes the documentation files under a `docs` solution folder in Visual Studio, preserving their subfolder structure. When adding documentation files, also add them as solution items; solution folders do not automatically include new files.

CI artifact names include the build configuration so each Revit matrix job uploads a distinct artifact. NuGet publishing builds its own packages and does not download these CI artifacts.

The root `global.json` selects stable .NET SDK 10.0 (minimum `10.0.103`, `rollForward: latestFeature`). CI and publishing install the SDK from this file. Additional SDK installations may provide older test runtimes. See the [SDK selection policy](policies/development.md#net-sdk-selection).

## Solution items

The root solution exposes repository-level documents and configuration under `solutionItems`, GitHub files and maintenance scripts in matching subfolders, and documentation under `docs/`. The list is explicit, not a filesystem glob; keep links up to date when files change. See the [solution items policy](policies/development.md#solution-items).
## Repository validation

`scripts/Validate-Repository.ps1` enforces the required repository documents,
their navigation links, and complete, valid Solution Items. The
`.github/workflows/repository-policy.yml` workflow runs it for pushes and pull
requests. See the [development policy](policies/development.md#repository-validation).

## Formatting

The root `.editorconfig` defines the portable formatting baseline. Existing repositories may add stricter C# or analyzer-specific settings. See the [development policy](policies/development.md#formatting-baseline).

## Package dependency contract

The Revit API package remains a transitive compile-time dependency because public members accept and return Autodesk.Revit types. The Revit SDK controls copy-local behavior for host-provided assemblies.
- Package versions are set directly on each PackageReference; this repository does not use central package management.

## Versioning and release tags

The library uses MinVer 8 with stable tags in the `0.0.PATCH` format and no `v` prefix. A tagged commit is built separately for Revit 2021.1.9, 2023.0.0, and 2025.0.0; tag `0.0.8`, for example, produces package versions `2021.0.8`, `2023.0.8`, and `2025.0.8`. Manual publishing accepts exactly one matching tag at HEAD and verifies the package ID and Revit-specific version before pushing to NuGet.
