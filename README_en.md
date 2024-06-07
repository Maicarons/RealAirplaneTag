# RealAirplaneTag

[中文](README.md)

This project aims to augment aircraft models within a simulation context by incorporating key identifiers such as flight numbers and aircraft models, enhancing the realism of the virtual aviation experience.

## Objectives

- [x] Implement flight number and aircraft model text overlays.
- [x] Adjust aircraft scale based on the specified aircraft model.
- [ ] Utilize actual flight numbers and aircraft models for added authenticity.

## Configuration

- **Framework**: .NET Core 6.0
- **IDE**: JetBrains Rider, with support for automated test execution via included `.run` scripts.

## Compilation Instructions

Execute the following commands in your terminal or command prompt to set up and build the project:

```bash
dotnet restore  # Restores dependencies required for the project.
dotnet build    # Compiles the project into a DLL.
```

## Acknowledgments

Gratitude goes out to the `Npa` team and their remarkable work on the `MiniAirways` game, which serves as inspiration for this project.