# VS Code Extension for Viewing PlantUML Diagrams

## Recommended Extension: PlantUML

### Installation Steps:

1. **Open VS Code**
2. **Go to Extensions** (Ctrl+Shift+X or click the Extensions icon in the sidebar)
3. **Search for "PlantUML"**
4. **Install "PlantUML" by jebbs** (Most popular, maintained extension)

### Alternative Extensions:

- **PlantUML Preview** - Another good option
- **Markdown Preview Mermaid Support** - If you want to use Mermaid diagrams instead

## How to Use PlantUML Extension:

### Method 1: Preview in Side Panel
1. Open any `.puml` file in VS Code
2. Press `Alt+D` (Windows/Linux) or `Option+D` (Mac) to preview
3. Or right-click in the file and select "Preview PlantUML"

### Method 2: Export to Image
1. Open the `.puml` file
2. Press `Ctrl+Shift+P` (Command Palette)
3. Type "PlantUML: Export Current Diagram"
4. Choose format: PNG, SVG, or PDF
5. Select save location

### Method 3: Live Preview
1. Open a `.puml` file
2. Press `Ctrl+Shift+P`
3. Type "PlantUML: Preview Current Diagram"
4. A preview window will open showing the diagram

## Requirements:

The PlantUML extension requires Java to be installed on your system.

### Installing Java (if not installed):

1. **Download Java JDK** from: https://www.oracle.com/java/technologies/downloads/
   - Or use OpenJDK: https://adoptium.net/
2. **Install Java**
3. **Restart VS Code** after installation

### Verify Java Installation:
- Open terminal in VS Code (Ctrl+`)
- Type: `java -version`
- Should show Java version if installed correctly

## Quick Start:

1. **Install PlantUML extension** in VS Code
2. **Install Java** (if not already installed)
3. **Open any `.puml` file** from the UML_Diagrams folder
4. **Press Alt+D** to preview the diagram
5. **Enjoy viewing your UML diagrams!**

## Tips:

- **Auto-preview**: The extension can auto-update the preview as you edit
- **Export all**: Use "PlantUML: Export All Diagrams" to export all diagrams at once
- **Syntax highlighting**: The extension provides syntax highlighting for PlantUML
- **Error checking**: It will show errors if your PlantUML syntax is incorrect

## Troubleshooting:

If preview doesn't work:
1. Check if Java is installed: `java -version` in terminal
2. Restart VS Code after installing Java
3. Check VS Code output panel for PlantUML errors
4. Try the command palette method instead of keyboard shortcut

## Alternative: Online Viewer

If you prefer not to install Java, you can use the online viewer:
1. Open the `.puml` file
2. Copy all the content
3. Go to: http://www.plantuml.com/plantuml/uml/
4. Paste the content
5. View the diagram online

