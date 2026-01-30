# Aria-Sama (Early Alpha)

**Warning: This project is in early beta and should not be used for production or streaming yet. Features and stability are incomplete.**

## Change Log

### [Latest Changes]
- Project builds and runs successfully on .NET 8.0 (`net8.0-windows`).
- Solution file (`vision-sdk.sln`) recreated and project re-added for clean build.
- Cleaned and restored all NuGet packages.
- Fixed XAML file declarations and ensured namespace consistency (`AriaSama`).
- Removed duplicate/legacy project files and ensured only `VtuberNeuroSama` is used.
- Updated project file with `Nullable`, `ImplicitUsings`, and `EnableWindowsTargeting` for .NET 8.0 WPF best practices.
- README updated with build/test instructions and change log.

## Features
- VTuber model upload (coming soon)
- Voice chat (basic demo)
- Planned: Streamlabs Desktop integration

## Platform
- Windows NT (Windows 10/11 recommended)

## Setup Instructions

### Prerequisites
- Node.js (v18 or newer recommended)
- npm (comes with Node.js)
- Windows OS (NT family)
- .NET 8.0 SDK (for WPF app)

### Installation
1. Open a terminal and navigate to the `vtuber-stream-app` directory:
   ```sh
   cd vtuber-stream-app
   ```
2. Install dependencies:
   ```sh
   npm install
   ```

### Running the App
1. Start the development server:
   ```sh
   npm start
   ```
2. Open your browser and go to `http://localhost:3000` (or the URL shown in the terminal).

#### Running the WPF App
1. Open the solution in Visual Studio 2022 or newer.
2. Ensure you have the .NET 8.0 SDK and the "Desktop development with .NET" workload installed.
3. Build and run the `VtuberNeuroSama` project.

## Notes
- This app is under active development. Many features are incomplete or experimental.
- Do not use for real streaming or production purposes yet.
- Feedback and contributions are welcome!

## Roadmap
- VTuber model uploader
- Streamlabs Desktop overlay integration
- Improved voice chat
- UI/UX enhancements

---
VisionVT | 2025
