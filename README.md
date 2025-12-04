# WinModal ![DLL Release](https://img.shields.io/github/v/tag/fahmirizalbudi/winmodal?label=DLL%20Release)

A lightweight and customizable modal dialog library for **C# Windows Forms**. **WinModal** provides an easy way to display modal windows with an overlay, animations, and customizable options—without rewriting complex UI logic each time.

## Features

* Simple and minimal implementation for WinForms
* Overlay background
* Centered modal window display
* Optional animations (fade-in, fade-out)
* Static class usage for easy integration
* Clean separation between parent form, modal form, and overlay

## Installation

### 1. Add the DLL file to your project

Download the latest compiled DLL from the Releases page.
After downloading, add the DLL as a project reference through your IDE so your application can access the WinModal API.
Once referenced, the library becomes available throughout your WinForms project.

### 2. Import the Namespace

```csharp
using WinModal;
```

## Usage Example

### Basic Modal Display

This example shows how to display a modal form on top of any parent form.
The library handles overlay creation, positioning, and modal behavior automatically.

```csharp
var modal = new FormModalExample();
WinModal.WinModal.Show(this, modal);
```

### Using Options

You can customize modal behavior using `WinModalOptions`.
Options allow you to configure transition, draggable, and overlay.

```csharp
var options = new WinModalOptions
{
    Transition = true,
    Draggable = true,
    Overlay = true
};

var modal = new FormModalExample();
WinModal.WinModal.Show(this, modal, options);
```

### Closing the Modal Programmatically

Inside your modal form, you can close it using:

```csharp
this.Close();
```

WinModal will automatically clean up the overlay and restore the parent form’s state.

## Usage

The latest version of WinModal is available here:
[Download](https://github.com/fahmirizalbudi/winmodal/releases/latest)

## License

This project is released under the MIT License, allowing free use, modification, distribution, and integration in both personal and commercial applications.
See the LICENSE file for complete details regarding permissions and limitations.
