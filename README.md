# WindowsControlUtilities

A collection of C# & C++ utilities for managing and controlling various device functionalities on Windows, including volume control, Wi-Fi connection management, device enumeration, and taskbar visibility settings. This repository includes scripts for muting/unmuting volume, connecting to secured Wi-Fi networks, listing network interfaces, and enabling/disabling the taskbar, among others.

# Usage / Syntax

## 1. lockout.exe
No command-line arguments required. Run the program to remove Keyboard devices.
```
lockout.exe
```

## 2. lockoutUSB.exe
No command-line arguments required. Run the program to remove USB devices.
```
lockoutUSB.exe
```

## 3. lockoutSystem.exe
No command-line arguments required. Run the program to remove all removable system devices.
```
lockoutSystem.exe
```

## 4. lockoutHID.exe
No command-line arguments required. Run the program to remove HID devices.
```
lockoutHID.exe
```

## 5. initDevices.exe 
No command-line arguments required. Run the program to re-scan and connect removable devices.
```
initDevices.exe
```

## 6. ConnectToWifi.exe 
<SSID> <password> required to connect to network
```
wifiConnectSecured.exe "YourSSID" "YourPassword"
```
