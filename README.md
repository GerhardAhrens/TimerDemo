# Timer und Demo

![NET](https://img.shields.io/badge/NET-10.0-green.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)
![VS2022](https://img.shields.io/badge/Visual%20Studio-2026-white.svg)
![Version](https://img.shields.io/badge/Version-1.0.2026.0-yellow.svg)]

Das Beispiel soll zeigen, welche Timervarianten es unter NET 10 gibt und wozu diese am besten geeignet sind.

|Timer Typ|Eigenschaft|Hinweis|
|:-|:--|:-|
|System.Windows.Threading.DispatcherTimer|Läuft im UI-Thread, geeignet für UI-Updates|Kann bei höherer Last die UI blockieren|
|System.Threading.Timer|Läuft in einem Threadpool-Thread, geeignet für wiederkehrende Aufgaben|
|System.Timers.Timer|Läuft in einem separaten Thread, geeignet für Hintergrundaufgaben|
|System.Windows.Forms.Timer|Läuft im UI-Thread, geeignet für Windows Forms Anwendungen|Wird im WPF Demo nicht weiter dargestellt|
