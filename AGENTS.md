# AGENTS.md

## Policy

The stack documented below is the default and takes priority over
whatever an agent might otherwise reach for. Prefer what's already in use
over introducing an alternative. If a deviation seems necessary, say so
explicitly to the user and get confirmation before adding it.

If a change affects the folder structure or the tech stack (a new/removed project, a new dependency, a version bump worth recording, a new convention), update this file accordingly as part of the same change - don't leave it to a later pass.

Before changing existing tests or writing new ones, ask the user first – confirm what should be covered and how (or that the change is trivial enough not to need it) rather than deciding unilaterally.

Commit messages follow Conventional Commits (`<type>(<scope>): <description>`, e.g. `feat(manifest): ...`, `fix(...): ...`, `docs(agents): ...`, `test(...): ...`, `chore(...): ...`, `refactor(...): ...`) - scope optional but preferred when it clarifies what changed.

## About

Source for `VolocyNazad.Revit.Async`: async/await helpers for marshalling
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
- Tests: **xunit.v3** (unlike the classic xunit v2 used by the sibling
  `toolkit.revit.context` / `.events` / `.mediatR` /
  `.transaction-cache` repos)
- Central package management is present but explicitly turned off
  (`ManagePackageVersionsCentrally = false` in `Directory.Packages.props`)
  - package versions are set per-`<PackageReference>`
