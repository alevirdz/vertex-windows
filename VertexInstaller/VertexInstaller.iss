;Configuración para compilador Inno 6.7.0
[Setup]
AppName=Vertex
AppVersion=1.0.0
AppPublisher=Vertex Inc.
AppPublisherURL=https://vertex.com.mx
DefaultDirName={pf}\Vertex
DefaultGroupName=Vertex
OutputBaseFilename=VertexInstaller
Compression=lzma
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64
WizardStyle=modern
UninstallDisplayIcon={app}\Vertex.App.exe
UninstallDisplayName=Vertex
Uninstallable=yes
AllowNoIcons=yes
DisableProgramGroupPage=yes
DisableReadyPage=yes
WizardImageFile=ResourcesSetup\10032965.bmp
WizardSmallImageFile=ResourcesSetup\10032965.bmp
SetupIconFile=ResourcesSetup\icono_instalador.ico



[Languages]
Name: "Spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Files]
; Copia todos los archivos de tu build net8.0-windows
Source: "C:\Users\isaac.rodriguez\Documents\www\vertex-windows\Vertex\bin\Release\net8.0-windows\*.*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
; Icono en el menú inicio
Name: "{group}\Vertex"; Filename: "{app}\Vertex.App.exe"
; Icono en escritorio
Name: "{commondesktop}\Vertex"; Filename: "{app}\Vertex.App.exe"

[Run]
; Ejecutar al finalizar la instalación (normal)
Filename: "{app}\Vertex.App.exe"; Description: "Launch Vertex"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
; Elimina todos los archivos al desinstalar
Type: filesandordirs; Name: "{app}"
