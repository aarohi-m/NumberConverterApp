# NumberConverterApp
A simple, expressive web app for converting between Decimal, Binary, Octal, Hex, and ASCII  built with C# ASP.NET MVC.

#Architecture
 ┌───────────────────────────────────────────┐
 │                  Client                    │
 │  ───────────────────────────────────────   │
 │  Razor Views (HTML/CSS) + Minimal JS        │
 └───────────────────────────────────────────┘
                    │   ↑
        HTTP GET/POST│   │ Rendered Views
                    ↓   │
 ┌───────────────────────────────────────────┐
 │              Controllers                   │
 │  HomeController → Handles input/output     │
 └───────────────────────────────────────────┘
                    │
                    ↓
 ┌───────────────────────────────────────────┐
 │                Models                      │
 │  ConversionViewModel → Stores user data    │
 │  ConverterService → Performs conversions   │
 └───────────────────────────────────────────┘
                    │
                    ↓
 ┌───────────────────────────────────────────┐
 │           Conversion Logic Layer           │
 │  Decimal ⇄ Binary ⇄ Octal ⇄ Hex ⇄ ASCII    │
 │  Algorithms in C#                          │
 └───────────────────────────────────────────┘



 #How It Works
- User Input:
A number is entered along with its current format (e.g., Decimal).
- Controller Processing:
- The request hits HomeController.
- Data is mapped into ConversionViewModel.
- Backend Conversion Logic:
- ConverterService applies the correct conversion algorithm.
- Results for all other formats are generated in one pass.
- View Rendering:
- Razor View displays the converted results in a responsive table.
- Output:
- The user instantly sees equivalent values in Binary, Octal, Hex, and ASCII.


# Clone repository
git clone https://github.com/aarohi-m/NumberConverterApp.git

# Open in Visual Studio, restore NuGet packages, and run
