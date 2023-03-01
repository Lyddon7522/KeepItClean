# KeepItClean



# Getting Started

Ensure you've installed all necessary prerequisites before running the project.

## Prerequisites
- .NET 7 SDK (Installing Visual Studio 2022 and selecting .NET 7 in the modules will do this, or you can go to their website and download the SDK via their instructions)
- IIS Express
- Docker (in Linux mode)
- WASM tools (can be installed via dotnet workload install wasm-tools in the command line)

## Compiling CSS

The web project uses [Tailwind](https://tailwindcss.com/docs/installation) for its styling, utilizing their Tailwind CLI in order to compile a stylesheet. In order to run the CLI, do the following:

- Open a terminal
- Navigate to the `tailwindcss.exe` executable.
- Run `./tailwindcss -i ./wwwroot/css/app.css -o ./wwwroot/css/tailwind.css --watch`

TODO: Set this up in a workflow, including `--minify`

# Project Resources

- Authn/z: https://console.firebase.google.com
- AWS: 