# Godot Procedural Space Generation

![Godot Version](https://img.shields.io/badge/Godot-4.x-blue)

This project demonstrates a procedural space generation system built in Godot Engine, focused on creating a seemingly infinite grid of stars. Each star can be interacted with to reveal the planets orbiting them. Using Godot's PCG32-based random number generator with seeded values, the system generates a massive yet memory-efficient infinite space that can be explored and interacted with in real-time. Shaders are employed to enhance the visual appeal, generating beautiful textures for stars and planets.

## Features

- **Procedural Star Generation**: Dynamically create star fields with adjustable density and patterns.
- **Planetary Systems**: Generate planets with varying sizes, colors, and orbital paths.
- **Procedural Textures with Shaders**: Uses shaders and noise to generate textures for stars and planets, including landmasses, animated water, and planet spin.
- **Godot 4.x Compatibility**: Built using the latest version of the Godot Engine, taking advantage of advanced features like the Vulkan renderer.

## Getting Started

### Prerequisites

- **Godot Engine (C#)**: Version 4.x or higher.
- A system capable of running Godot (Windows, macOS, Linux).

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/thetornadotitan/godot-procedural-space-generation.git
   ```

### Open the project in Godot:

1. Launch Godot.
2. Click **Import** and select the project folder.

### Run the project:

- Press the **Play** button in the Godot editor to start the simulation.

### Usage

- Adjust procedural generation parameters by modifying the scripts attached to key nodes:
  - **Star Density**: Modify the density of stars in `StarGenerator.gd`.
  - **Planetary Properties**: Customize size, orbit, and colors in `PlanetGenerator.gd`.

## License

This project is CC0.
