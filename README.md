# Revit.Async

[![Revit 2021-2027](https://img.shields.io/badge/Revit-2021%E2%80%932027-green.svg)](https://autodesk.com/revit)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![VolocyNazad](https://img.shields.io/badge/VolocyNazad-blue.svg)](https://github.com/VolocyNazad)

> Реализация асинхронного выполнения кода в контексте Revit API.

Revit.Async — библиотека для выполнения операций Revit API из немодальных окон и других контекстов через `ExternalEvent` с поддержкой `Task`.

Основана на MIT-лицензированном проекте [Revit.Async](https://github.com/KennanChan/Revit.Async) Кеннана Чана.

## Поддерживаемые версии Revit

Пакет собирается под версии Revit 2021.1.9, 2023, 2025, 2026, 2027.0.2 (см. конфигурации в `Revit.Async.csproj`), таргетируя `net48` для версий до 2025 и `net8.0-windows` для 2025+.

## Требования

- .NET SDK 10.0.103+ (см. `global.json`)
- Revit API (пакет `Revit_All_Main_Versions_API_x64`)

## Лицензия

MIT, см. [LICENSE.md](LICENSE.md).
