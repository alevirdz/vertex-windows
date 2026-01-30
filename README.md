
# VERTEX

Se presenta la siguiente estructura usando MVVM (Model-View-ViewModel).
Vertex es un núcleo para cargar diferentes vistas dinamicamente.

### Estructura

```
Vertex/
├── Vertex.App/       # WPF -> EXE
├── Vertex.Core/      # Modelos, interfaces y utilidades
├── Vertex.Modules/   # Módulos opcionales
└── Vertex.Services/  # Servicios transversales
```

### Estructura detallada


```
Vertex/
│
├── Vertex.App/                # Proyecto WPF -> EXE
│   ├── App.xaml
│   ├── App.xaml.cs
│   ├── MainWindow.xaml        # Contenedor principal
│   ├── MainWindowViewModel.cs
│   ├── Views/                # Ventanas y UserControls
│   │   ├── HomeView.xaml
│   │   └── SettingsView.xaml
│   ├── ViewModels/           # ViewModels de cada ventana
│   ├── Resources/            # Estilos, brushes, themes
│   ├── Services/             # Servicios UI (diálogos, navegación)
│   └── Controls/             # Controles reutilizables
│
├── Vertex.Core/               # Lógica compartida y modelos
│   ├── Models/               # Modelos de datos
│   ├── Enums/
│   ├── Interfaces/           # Contratos para inyección de dependencias
│   └── Utils/                # Helpers generales
│
├── Vertex.Modules/            # Módulos opcionales, desacoplados
│   ├── Dashboard/            # Ejemplo de módulo
│   │   ├── Views/
│   │   ├── ViewModels/
│   │   └── Services/
│   └── Reports/
│       ├── Views/
│       ├── ViewModels/
│       └── Services/
│
├── Vertex.Services/           # Servicios transversales
│   ├── NavigationService.cs   # Manejo de cambio de ventanas
│   ├── LoginService.cs        # Inicio de sesión
│   └── MenuService.cs         # Generación de menú dinámico
│
└── Vertex.Tests/              # Unit tests y tests de integración
```




